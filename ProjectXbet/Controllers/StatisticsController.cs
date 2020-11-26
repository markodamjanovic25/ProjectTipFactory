using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using DataAccessLibrary.Repository.SqlRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectXbet.ViewModels;

namespace ProjectXbet.Controllers
{
    [Authorize]
    public class StatisticsController : BaseController
    {
        private readonly IStatisticsRepository repository;
        private readonly ILeagueRepository leagueRepository;
        private readonly StatisticsViewModel viewModel;

        public StatisticsController(IStatisticsRepository repository, ILeagueRepository leagueRepository)
        {
            this.repository = repository;
            this.leagueRepository = leagueRepository;
            viewModel = new StatisticsViewModel();
        }

        // This method fills ViewModel objects with data retrieved from Repository
        public async Task<IActionResult> CreateViewModel(int TipTypeId)
        {
            ViewData["TipType"] = TipTypeId;
            viewModel.Leagues = await leagueRepository.GetLeagues();
            viewModel.TipStats = await repository.GetTipStats(TipTypeId);
            viewModel.Predictions = await repository.GetPredictionsPrevious(TipTypeId);
            viewModel.LeagueStats = await repository.GetLeagueStats(TipTypeId);
            viewModel.TipTypeStats = await repository.GetTipTypeStats(TipTypeId);
            return View("Index", viewModel);
        }

        [Route("stats")]
        [HttpGet]
        public async Task<IActionResult> Index(int TipTypeId)
        {
            return await CreateViewModel(TipTypeId);
        }
        //viewodel objects fill
        

        

    }
}
