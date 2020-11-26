using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repository.SqlRepository
{
    public class RandomRepository : IRandomRepository
    {
        
        public Dictionary<int, Prediction> Predictions { get; set; }

        public RandomRepository()
        {
            this.Predictions = new Dictionary<int, Prediction>();
        }

        public int GetRandomPredictionId(int TipTypeId)
        {
            int predictionIndex;
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            Prediction p = null;
            do
            {
                predictionIndex = rand.Next(0, Predictions.Count);
                p = (Prediction)Predictions[predictionIndex];

            }
            while (p == null);
            Predictions.Remove(predictionIndex);
            return p.PredictionId;
        }

        public Dictionary<int, Prediction> FillPredictionDictionary(List<Prediction> PredictionList)
        {
            Dictionary<int, Prediction> dict = new Dictionary<int, Prediction>();
            int i = 0;
            foreach(var item in PredictionList)
            {
                dict.Add(i++, item);
            }
            return dict;
        }
    }
}
