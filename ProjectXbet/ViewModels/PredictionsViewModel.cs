using DataAccessLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class PredictionsViewModel : BaseViewModel
    {
        public ICollection<Prediction> Predictions;
        public Dictionary<int, Prediction> PredictionBoxes;
        public ICollection<Prediction> PredictionsPrevious;


        public PredictionsViewModel()
        {
            Predictions = new List<Prediction>();
            PredictionBoxes = new Dictionary<int, Prediction>();
            PredictionsPrevious = new List<Prediction>();
        }
    }
}
