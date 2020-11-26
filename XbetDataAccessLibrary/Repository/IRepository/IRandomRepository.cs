using DataAccessLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface IRandomRepository
    {
        Dictionary<int, Prediction> Predictions { get; set; }
        //List<Prediction> GetPredictionBoxes(string UserRoleName, int TipTypeId);
        int GetRandomPredictionId(int TipTypeId);
        Dictionary<int, Prediction> FillPredictionDictionary(List<Prediction> PredictionList);
        //Task AddRandomTicketItem(int TipTypeId, string UserId);


    }
}
