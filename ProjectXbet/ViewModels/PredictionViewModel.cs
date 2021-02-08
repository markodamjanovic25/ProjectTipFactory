using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class PredictionViewModel : BaseViewModel
    {
        public int PredictionId { get; set; }
        public decimal Odds { get; set; }

        public decimal Chance { get; set; }

        [DisplayName("Match")]
        public int MatchId { get; set; }

        [DisplayName("Tip")]
        public int TipId { get; set; }

        [DisplayName("Role")]
        public string RoleId { get; set; }

        public List<Match> Matches;
        public List<Tip> Tips;
        public List<Role> Roles;

    }
}
