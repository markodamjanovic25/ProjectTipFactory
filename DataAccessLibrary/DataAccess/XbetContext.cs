using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess
{
    public class XbetContext : DbContext
    {
        public XbetContext(DbContextOptions options) 
            :base(options)
        {
        }
    }
}
