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
        public IActionResult CreateViewModel()
        {
            return View("Index");
        }
        [Route("about")]
        public IActionResult Index()
        {
            return CreateViewModel();
        }
        [Route("instructions")]
        public IActionResult Instructions()
        {
            return View();
        }
        [Route("accounts")]
        public IActionResult Accounts()
        {
            return View();
        }
        [Route("terms")]
        public IActionResult TermsAndConditions()
        {
            return View();
        }
    }
}
