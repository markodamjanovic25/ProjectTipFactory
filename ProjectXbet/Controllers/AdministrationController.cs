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
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : BaseController
    {
        private readonly IAdministrationRepository administrationRepository;
        private readonly ILeagueRepository leagueRepository;
        private readonly IMatchRepository matchRepository;
        private readonly ITipRepository tipRepository;
        private readonly IPredictionRepository predictionRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IBetRepository betRepository;
        public AdministrationViewModel viewModel;

        public AdministrationController(IAdministrationRepository administrationRepository, ILeagueRepository leagueRepository, 
            IMatchRepository matchRepository, ITipRepository tipRepository, IPredictionRepository predictionRepository, 
            IAccountRepository accountRepository, IBetRepository betRepository)
        {
            this.administrationRepository = administrationRepository;
            this.leagueRepository = leagueRepository;
            this.matchRepository = matchRepository;
            this.tipRepository = tipRepository;
            this.predictionRepository = predictionRepository;
            this.accountRepository = accountRepository;
            this.betRepository = betRepository;
            viewModel = new AdministrationViewModel();
        }

        public async Task<IActionResult> CreateViewModel()
        {
            viewModel.Leagues = await leagueRepository.GetLeagues();
            viewModel.Matches = await matchRepository.GetMatches();
            viewModel.Tips = await tipRepository.GetTips();
            viewModel.Roles = await accountRepository.GetRoles();
            viewModel.Predictions = await predictionRepository.GetPredictionsAll();
            return View("Index", viewModel);
        }

        [Route("administration")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await CreateViewModel();
        }

        //This method creates a new Prediction and adds it to to UserRolePredictions table with specified UserRole
        [HttpPost]
        public async Task<IActionResult> CreatePrediction(AdministrationViewModel model)
        {
            Prediction predictionAdded = await administrationRepository.AddPredictionAsync(new Prediction
            {
                Odds = model.Odds,
                Chance = model.Chance,
                MatchId = model.MatchId,
                TipId = model.TipId
            });

            await administrationRepository.AddUserRolePredictionAsync(new UserRolePredictions
            {
                UserRoleId = model.RoleId,
                PredictionId = predictionAdded.PredictionId
            });

            return await CreateViewModel();
        }

        //This method creates a new Match
        [HttpPost]
        public async Task<IActionResult> CreateMatch(AdministrationViewModel model)
        {
            await matchRepository.AddMatch(new Match
            {
                MatchDateTime = model.MatchDateTime,
                ClubHomeName = model.ClubHomeName,
                ClubAwayName = model.ClubAwayName,
                LeagueId = model.LeagueId
            });
            return await CreateViewModel();
        }

        [HttpPost]
        public async Task<IActionResult> SetPredictionOutcome(int PredictionId, bool IsCorrect, AdministrationViewModel model)
        {
            await predictionRepository.SetPredictionOutcomeAsync(PredictionId, model.ClubHomeGoalsHalf, model.ClubAwayGoalsHalf, model.ClubHomeGoals, model.ClubAwayGoals, IsCorrect);
            await betRepository.RefreshAllTickets(PredictionId);
            return await CreateViewModel();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePrediction(int PredictionId)
        {
            await predictionRepository.DeletePrediction(PredictionId);
            return await CreateViewModel();
        }

        [HttpGet]
        public async Task<IActionResult> EditMatch(int MatchId)
        {
            Match match = await matchRepository.GetMatchByIdAsync(MatchId);
            MatchViewModel matchViewModel = new MatchViewModel
            {
                MatchId = MatchId,
                MatchDateTime = match.MatchDateTime,
                ClubHomeName = match.ClubHomeName,
                ClubAwayName = match.ClubAwayName,
                ClubHomeGoalsHalf = (int)match.ClubHomeGoalsHalf,
                ClubAwayGoalsHalf = (int)match.ClubAwayGoalsHalf,
                ClubHomeGoals = (int)match.ClubHomeGoals,
                ClubAwayGoals = (int)match.ClubAwayGoals,
                LeagueId = match.LeagueId,
                //League = await leagueRepository.GetLeagueByIdAsync(match.LeagueId),
                Leagues = await leagueRepository.GetLeagues()
            };
            return View("Match", matchViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditMatch(MatchViewModel model)
        {
            Match edited = new Match
            {
                MatchId = model.MatchId,
                MatchDateTime = model.MatchDateTime,
                ClubHomeName = model.ClubHomeName,
                ClubAwayName = model.ClubAwayName,
                ClubHomeGoalsHalf = model.ClubHomeGoalsHalf,
                ClubAwayGoalsHalf = model.ClubAwayGoalsHalf,
                ClubHomeGoals = model.ClubHomeGoals,
                ClubAwayGoals = model.ClubAwayGoals,
                LeagueId = model.LeagueId
            };
            await matchRepository.EditMatchAsync(edited);
            return await CreateViewModel();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMatch(int MatchId)
        {
            await matchRepository.DeleteMatchByIdAsync(MatchId);
            return await CreateViewModel();
        }

        [HttpGet]
        public async Task<IActionResult> EditPrediction(int PredictionId)
        {
            Prediction p = await predictionRepository.GetPredictionByIdAsync(PredictionId);
            PredictionViewModel predictionViewModel = new PredictionViewModel() 
            {
                PredictionId = p.PredictionId,
                Odds = p.Odds,
                Chance = p.Chance,
                MatchId = p.MatchId,
                TipId = p.TipId,
                Matches = await matchRepository.GetMatches(),
                Tips = await tipRepository.GetTips(),
                Roles = await accountRepository.GetRoles()
            };
            return View("Prediction", predictionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPrediction(PredictionViewModel model)
        {
            Prediction edited = new Prediction
            {
                PredictionId = model.PredictionId,
                Odds = model.Odds,
                Chance = model.Chance,
                MatchId = model.MatchId,
                TipId = model.TipId
            };
            await predictionRepository.EditPredictionAsync(edited);
            return await CreateViewModel();
        }

        [Route("StatsAdvanced")]
        [HttpGet]
        public async Task<IActionResult> GetOddsInLeaguesStats(int TipTypeId)
        {
            AdminStatsViewModel viewModel = new AdminStatsViewModel();
            viewModel.Odds = await leagueRepository.GetAllOddsByTipType(TipTypeId);
            viewModel.LeagueStatsByOdds = await leagueRepository.GetLeagueStatsByOdds(TipTypeId);
            ViewData["TipTypeId"] = TipTypeId;
            return View("LeagueStatsAdvanced", viewModel);
        }


    }
}
