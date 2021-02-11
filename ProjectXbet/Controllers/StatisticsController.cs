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
        private readonly ITipRepository tipRepository;
        private readonly StatisticsViewModel viewModel;

        public StatisticsController(IStatisticsRepository repository, ILeagueRepository leagueRepository, ITipRepository tipRepository)
        {
            this.repository = repository;
            this.leagueRepository = leagueRepository;
            this.tipRepository = tipRepository;
            viewModel = new StatisticsViewModel();
        }

        // This method fills ViewModel objects with data retrieved from Repository
        public async Task<IActionResult> CreateViewModel(int TipTypeId)
        {
            ViewData["TipType"] = TipTypeId;
            viewModel.TipStats = await repository.GetTipStats(TipTypeId);
            viewModel.Predictions = await repository.GetPredictionsPrevious(TipTypeId);
            viewModel.LeagueStats = await repository.GetLeagueStats(TipTypeId);
            viewModel.TipTypeStats = await repository.GetTipTypeStats(TipTypeId);
            viewModel.ControllerName = "Statistics";
            viewModel.TipTypeId = TipTypeId;
            return View("Index", viewModel);
        }

        [Route("stats")]
        [HttpGet]
        public async Task<IActionResult> Index(int TipTypeId)
        {
            return await CreateViewModel(TipTypeId);
        }
        //viewodel objects fill

        [Route("league-stats")]
        [HttpGet]
        public async Task<IActionResult> ShowLeagueStats(int TipTypeId, int LeagueId)
        {
            decimal TotalPlayed = await leagueRepository.GetLeagueTotalPlayed(LeagueId, TipTypeId);
            decimal Wins = await leagueRepository.GetLeagueWins(LeagueId, TipTypeId);
            decimal Odds = await leagueRepository.GetLeagueAverageOdds(LeagueId, TipTypeId);
            decimal Percentage = PercentageCalculator.CalculatePercentage(TotalPlayed, Wins);
            decimal Roi = PercentageCalculator.CalculateRoi(TotalPlayed, Wins, Odds);

            LeagueViewModel lVM = new LeagueViewModel { 
               LeagueTotalPlayed = TotalPlayed,
               LeagueWins = Wins,
               LeaguePercentage = Percentage,
               LeagueAverageOdds = Odds,
               LeagueRoi = Roi,
               League = await leagueRepository.GetLeagueByIdAsync(LeagueId),
               Predictions = await leagueRepository.GetPredictionsByLeagueAndTipType(LeagueId, TipTypeId),
               TipStats = await repository.GetTipStatsByLeague(TipTypeId, LeagueId)
            };
            ViewData["TipTypeId"] = TipTypeId;
            return View("League", lVM);
        }

        [Route("league-tip-stats")]
        [HttpGet]
        public async Task<IActionResult> ShowTipsByLeagueAndTip(int TipTypeId, int TipId, int LeagueId)
        {
            decimal TotalPlayed = await leagueRepository.GetLeagueTotalPlayedByTip(LeagueId, TipId);
            decimal Wins = await leagueRepository.GetLeagueWinsByTip(LeagueId, TipId);
            decimal Odds = await leagueRepository.GetLeagueAverageOddsByTip(LeagueId, TipId);
            decimal Percentage = PercentageCalculator.CalculatePercentage(TotalPlayed, Wins);
            decimal Roi = PercentageCalculator.CalculateRoi(TotalPlayed, Wins, Odds);


            LeagueTipDetailedViewModel VM = new LeagueTipDetailedViewModel
            {
                Tip = await tipRepository.GetTipByTipId(TipId),
                League = await leagueRepository.GetLeagueByIdAsync(LeagueId),
                LeagueTotalPlayed = TotalPlayed,
                LeagueWins = Wins,
                LeagueAverageOdds = Odds,
                LeaguePercentage = Percentage,
                LeagueRoi = Roi,
                Predictions = await leagueRepository.GetPredictionsByLeagueAndTip(LeagueId, TipId)
            };
            ViewData["TipTypeId"] = TipTypeId;
            return View("LeagueTipDetailed", VM);
        }

        [Route("tip-stats")]
        [HttpGet]
        public async Task <IActionResult> ShowPredictionsByTip(int TipTypeId, int TipId)
        {
            TipViewModel tVM = new TipViewModel
            {
                Tip = await tipRepository.GetTipByTipId(TipId),
                Predictions = await repository.GetPredictionsByTipId(TipId),
                TipStats = await repository.GetTipStatsByTipId(TipId),
                LeagueStats = await repository.GetLeagueStatsByTip(TipId)
            };
            ViewData["TipTypeId"] = TipTypeId;
            return View("Tip", tVM);
        }


        

    }
}
