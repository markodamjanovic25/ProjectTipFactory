using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectXbet.ViewModels;

namespace ProjectXbet.Controllers
{
    [Authorize]
    public class AboutController : BaseController
    {
        BaseViewModel viewModel;
        public AboutController()
        {
            viewModel = new BaseViewModel();
            viewModel.TipTypeId = 0;
        }
        public IActionResult CreateViewModel()
        {
            return View("Index", viewModel);
        }
        [Route("about")]
        public IActionResult Index()
        {
            return CreateViewModel();
        }
        [Route("instructions")]
        public IActionResult Instructions()
        {
            return View(viewModel);
        }
        [Route("accounts")]
        public IActionResult Accounts()
        {
            return View(viewModel);
        }
        [Route("terms")]
        public IActionResult TermsAndConditions()
        {
            return View(viewModel);
        }
    }
}
