using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public static class PercentageCalculator
    {
        public static decimal CalculatePercentage(decimal Total, decimal Wins)
        {
             return (Wins / Total) * 100;
        }
        public static decimal CalculateRoi(decimal Total, decimal Wins, decimal Odds)
        {
            return ((Odds * Wins) - Total) / Total * 100;
        }
    }
}
