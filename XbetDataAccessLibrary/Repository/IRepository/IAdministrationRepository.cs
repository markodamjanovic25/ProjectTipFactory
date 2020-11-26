using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface IAdministrationRepository
    {
        Task<Prediction> AddPredictionAsync(Prediction p);
        Task AddUserRolePredictionAsync(UserRolePredictions p);
    }
}
