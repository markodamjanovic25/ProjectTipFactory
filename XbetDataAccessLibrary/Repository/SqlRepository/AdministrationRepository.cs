using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.SqlRepository
{
    public class AdministrationRepository : IAdministrationRepository
    {
        private readonly XbetContext db;

        public AdministrationRepository(XbetContext db)
        {
            this.db = db;
        }

        //This method adds a new prediction and returns it
        public async Task<Prediction> AddPredictionAsync(Prediction p)
        {
            await db.Predictions.AddAsync(p);
            await db.SaveChangesAsync();
            return p;
        }

        //This method adds a new item in UserRolePredictions table
        public async Task AddUserRolePredictionAsync(UserRolePredictions p)
        {
            await db.UserRolePredictions.AddAsync(p);
            await db.SaveChangesAsync();
        }
    }
}
