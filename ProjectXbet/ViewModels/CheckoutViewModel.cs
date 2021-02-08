using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class CheckoutViewModel : BaseViewModel
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string MonotonousTipsAmount { get; set; }
        public string AdventurousTipsAmount { get; set; }
        public decimal Price { get; set; }
    }
}
