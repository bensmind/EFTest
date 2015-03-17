using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace Database
{
    internal class MigrationContext : DbContext, IOneContext, ITwoContext
    {
        public MigrationContext() : base("Name=ConnString")
        {
        }

        public DbSet<OneModel> OneModels { get; set; }
        public DbSet<TwoModel> TwoModels { get; set; }
    }
}
