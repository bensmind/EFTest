using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace Database
{
    public interface IOneContext
    {
        DbSet<OneModel> OneModels { get; set; }
    }

    public class OneContext : DbContext, IOneContext
    {
        public OneContext() 
            : base("Name=ConnString")
        {
        }

        public DbSet<OneModel> OneModels { get; set; }
    }
}
