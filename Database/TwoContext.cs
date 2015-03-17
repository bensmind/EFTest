using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace Database
{
    public interface ITwoContext
    {
        DbSet<TwoModel> TwoModels { get; set; }
    }

    public class TwoContext : DbContext, ITwoContext
    {
        public TwoContext() 
            : base("Name=ConnString")
        {
        }

        public DbSet<TwoModel> TwoModels { get; set; }
    }
}
