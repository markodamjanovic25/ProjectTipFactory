using DataAccessLibrary.Models;
using ProjectXbet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.Models
{
    public class TipsViewModel
    {
        public int Credits;
        public DateTime DateTime;
        public Prediction prediction;
        public List<Prediction> predictions;

        public TipsViewModel()
        {
            prediction = new Prediction();
            predictions = new List<Prediction>();
        }
    }
}
