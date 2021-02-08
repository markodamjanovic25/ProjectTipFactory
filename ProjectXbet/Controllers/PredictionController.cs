using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PredictionController : BaseController
    {
        private readonly IAccountRepository accountRepository;
        private readonly IPredictionRepository predictionRepository;
        private readonly IStatisticsRepository statisticsRepository;
        private readonly IBetRepository betRepository;
        private readonly IRandomRepository randomRepository;
        private readonly PredictionsViewModel viewModel;

        public PredictionController(IAccountRepository accountRepository, IPredictionRepository predictionRepository, 
            IStatisticsRepository statisticsRepository, IBetRepository betRepository, IRandomRepository randomRepository)
        {
            this.accountRepository = accountRepository;
            this.predictionRepository = predictionRepository;
            this.statisticsRepository = statisticsRepository;
            this.betRepository = betRepository;
            this.randomRepository = randomRepository;
            viewModel = new PredictionsViewModel();
        }

        // This method fills ViewModel objects with data retrieved from Repository
        public async Task<IActionResult> CreateViewModel(int TipTypeId)
        {
            //If User has Role linked to any paid Subscription, check if it expired
            if (GetUserRoleName() != "Basic" && GetUserRoleName() != "Administrator")
            {
                await accountRepository.CheckSubscriptionStatus(GetUserId(), await accountRepository.GetSubscriptionId(GetUserId()));
            }
            ViewData["TipType"] = TipTypeId;
            viewModel.Predictions = await predictionRepository.GetPredictionsByTipType(GetUserRoleName(), TipTypeId);
            randomRepository.Predictions = randomRepository.FillPredictionDictionary(await predictionRepository.GetPredictionsByTipType(GetUserRoleName(), TipTypeId));
            viewModel.PredictionBoxes = randomRepository.Predictions;
            viewModel.PredictionsPrevious = await statisticsRepository.GetPredictionsPrevious(TipTypeId);
            viewModel.ControllerName = "Prediction";
            viewModel.TipTypeId = TipTypeId;
            return View("Index", viewModel);
        }

        [Route("tips")]
        [HttpGet]
        public async Task<IActionResult> Index(int TipTypeId)
        {
            return await CreateViewModel(TipTypeId);
        }

        [HttpPost]
        public async Task AddTicketItem(int PredictionId)
        {
            Prediction p = await predictionRepository.GetPredictionByIdAsync(PredictionId);
            await betRepository.AddTicketItem(GetUserId(), p);
        }

        [HttpGet]
        public IActionResult BetslipViewComponent()
        {
            return ViewComponent("Betslip");
        }
        
        [HttpPost]
        public async Task AddRandomTicketItem(int TipTypeId)
        {
            int PredictionId = randomRepository.GetRandomPredictionId(TipTypeId);
            Prediction p = await predictionRepository.GetPredictionByIdAsync(PredictionId);
            await betRepository.AddTicketItem(GetUserId(), p);
        }

        [HttpPost]
        public async Task DeleteTicketItem(int PredictionId)
        {
            await betRepository.DeleteTicketItem(GetUserId(), PredictionId);
        }

        [HttpPost]
        public async Task DeleteTicket(int TicketId)
        {
            await betRepository.DeleteTicketByIdAsync(TicketId);
        }

        [HttpPost]
        public async Task SaveTicket(int TicketId)
        {
            await betRepository.SaveTicketAsync(TicketId);
        }

        public async Task<IActionResult> GetTicketInfo(int TicketId)
        {
            TicketViewModel ticketViewModel = new TicketViewModel();
            ticketViewModel.Ticket = await betRepository.GetTicketByIdAsync(TicketId);
            ticketViewModel.Predictions = await betRepository.GetBetPredictionsByTicketIdAsync(TicketId);
            return View("Ticket", ticketViewModel);
        }
    }
}
