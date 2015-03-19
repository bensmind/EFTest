using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Database.Services.Base;
using Domain;

namespace Database.Services
{
    public interface IOneRepo : IDisposable
    {
        IRepoResult<One> Get(int id);
    }

    public class OneRepo : RepoBase<OneContext>, IOneRepo
    {
        public OneRepo(OneContext context)
            : base(context)
        {
        }

        public IRepoResult<One> Get(int id)
        {
            return Wrap(ctx =>
            {
                var db = ctx.OneModels.SingleOrDefault(om => om.Id == id);

                if (db == null) throw new Exception("Attempt to lookup `OneModel` by non-existant `Id`");

                var domain = DbToDomain(db);
                return domain;
            });
        }

        internal virtual One DbToDomain(OneModel db)
        {
            return new One(db.Id);
        }
    }
}
