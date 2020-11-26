using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ProjectXbet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectXbet.ViewComponents
{
    [ViewComponent(Name = "Betslip")]
    public class BetslipViewComponent : ViewComponent
    {
        private readonly IBetRepository betRepository;

        public BetslipViewComponent(IBetRepository betRepository)
        {
            this.betRepository = betRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Prediction> predictions = await betRepository.GetCurrentBetPredictionsAsync(GetUserId());

            var result = new BetslipViewModel {
                TicketId = betRepository.GetCurrentTicketId(GetUserId()),
                Predictions = predictions,
                TotalOdds = betRepository.GetCurrentTicketOdds(GetUserId())
            };
            return View(result);
        }

        public string GetUserId()
        {
            ClaimsPrincipal currentUser = (ClaimsPrincipal)this.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
