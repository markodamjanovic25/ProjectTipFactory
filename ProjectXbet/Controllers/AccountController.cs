using System;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectXbet.ViewModels;

namespace ProjectXbet.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        #region Setup
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IAccountRepository accountRepository;
        private readonly IBetRepository betRepository;
        private readonly IMessageRepository messageRepository;
        public AccountViewModel viewModel;

        public AccountController(RoleManager<Role> roleManager, UserManager<User> userManager,
                                SignInManager<User> signInManager, IAccountRepository accountRepository, IBetRepository betRepository, IMessageRepository messageRepository)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountRepository = accountRepository;
            this.betRepository = betRepository;
            this.messageRepository = messageRepository;
            viewModel = new AccountViewModel();
        }
        #endregion

        #region Account Management
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // This method registers and signs in new user
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    UserName = user.UserName,
                    Email = user.Email
                };
                var result = await userManager.CreateAsync(newUser, user.Password);

                if (result.Succeeded)
                {
                    await AddRole(newUser.Id, "Basic"); //adds a Basic Role to new User
                    await accountRepository.StartSubscription(1, newUser.Id); //starts a basic subscription
                    await signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Index", "Prediction");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(user);
        }

        // This method adds Role to created user
        public async Task AddRole(string UserId, string Role)
        {
            var user = await userManager.FindByIdAsync(UserId);
            await userManager.AddToRoleAsync(user, Role);
        }

        public async Task RemoveRole(string UserId, string Role)
        {
            var user = await userManager.FindByIdAsync(UserId);
            await userManager.RemoveFromRoleAsync(user, Role);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        //This method logs user in
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Prediction", new { TipTypeId = 1 });
                }
                ModelState.AddModelError(string.Empty, "Neuspesno!");
            }
            return View(model);
        }

        //This method logs user out
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion

        public async Task<IActionResult> CreateViewModel()
        {
            string UserId = GetUserId();
            string RoleName = GetUserRoleName();
            viewModel.User = await accountRepository.GetUser(UserId);
            viewModel.Role = await accountRepository.GetRole(RoleName);
            viewModel.Tickets = await betRepository.GetUserTickets(UserId);
            viewModel.Invoices = await accountRepository.GetInvoicesByUserId(UserId);
            viewModel.ReceivedMessages = await accountRepository.GetReceivedMessagesByUserId(UserId);
            viewModel.SentMessages = await accountRepository.GetSentMessagesByUserId(UserId);
            if (RoleName != "Basic")
            {
                viewModel.ExpDate = await accountRepository.GetSubscriptionExpDate(UserId);
            }
            return View("Profile", viewModel);
        }

        [Route("account")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            return await CreateViewModel();
        }

        #region Checkout

        [Route("checkout")]
        [HttpGet]
        public async Task<IActionResult> Checkout(int PlanId)
        {
            ViewData["ClientId"] = "AYBqN4OuHgNob-xgAlid2aDweFNmAtodoHkYu8BObmrmZu3BfpSV9IUyfUXzIQcKSzAyJqdWTshCeiHe";
            ViewData["PlanId"] = PlanId;
            Plan p = await accountRepository.GetPlan(PlanId); // get plan by id
            CheckoutViewModel checkoutViewModel = new CheckoutViewModel //create model based on plan from line above
            {
                PlanId = p.PlanId,
                PlanName = p.PlanName,
                MonotonousTipsAmount = p.MonotonousTipsAmount,
                AdventurousTipsAmount = p.AdventurousTipsAmount,
                Price = p.Price
            };
            return View("Checkout", checkoutViewModel);
        }

        [Route("account/subscription/approved/{OrderId}")]
        public async Task SubscriptionApproved(string OrderId, string Role, int PlanId)
        {
            await accountRepository.StartSubscription(PlanId, GetUserId());
            await accountRepository.CreateInvoice(GetUserId());
            await RemoveRole(GetUserId(), GetUserRoleName());
            await AddRole(GetUserId(), Role);
            
        }

        #endregion

        #region Message

        [Route("contact")]
        public IActionResult Contact()
        {
            MessageViewModel viewModel = new MessageViewModel()
            {
                TipTypeId = 0
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Message message = new Message()
                {
                    SenderId = GetUserId(),
                    ReceiverId = "9d9d680b-61d3-40c3-8593-802124323775",
                    Subject = model.Subject,
                    Text = model.Text,
                    IsMessageRead = false
                };
                await messageRepository.AddMessage(message);
                return await CreateViewModel();
            }
            return View("Contact", model);
        }
        #endregion
        


    }
}
