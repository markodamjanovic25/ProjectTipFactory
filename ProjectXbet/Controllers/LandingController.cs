using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ProjectXbet.ViewModels;

namespace ProjectXbet.Controllers
{
    public class LandingController : Controller
    {
        private readonly IPredictionRepository predictionRepository;
        private readonly IStatisticsRepository statisticsRepository;
        private readonly LandingViewModel viewModel;

        public LandingController(IPredictionRepository predictionRepository, IStatisticsRepository statisticsRepository)
        {
            this.predictionRepository = predictionRepository;
            this.statisticsRepository = statisticsRepository;
            viewModel = new LandingViewModel();
        }

        //This method fills view model objects
        public async Task<IActionResult> CreateViewModel()
        {
            viewModel.PredictionsMonotonous = await predictionRepository.GetPredictionsByTipType("Basic", 1);
            viewModel.PredictionsPreviousMonotonous = await statisticsRepository.GetPredictionsPrevious(1);
            viewModel.TipsMonotonous = await statisticsRepository.GetTipStats(1);
            viewModel.LeaguesMonotonous = await statisticsRepository.GetLeagueStats(1);

            viewModel.PredictionsAdventurous = await predictionRepository.GetPredictionsByTipType("Basic", 1);
            viewModel.PredictionsPreviousAdventurous = await statisticsRepository.GetPredictionsPrevious(2);
            viewModel.TipsAdventurous = await statisticsRepository.GetTipStats(2);
            viewModel.LeaguesAdventurous = await statisticsRepository.GetLeagueStats(2);

            viewModel.TipTypeStats.Add(await statisticsRepository.GetTipTypeStats(1));
            viewModel.TipTypeStats.Add(await statisticsRepository.GetTipTypeStats(2));
            viewModel.TipTypeStats.Add(await statisticsRepository.GetTipTypeStats(3));
            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await CreateViewModel();
        }
    }
}
