using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Database.Services.Base;
using Domain;

namespace Database.Services.Base
{
    public interface IRepoResult
    {
        IEnumerable<Exception> Errors { get; } 

        bool Success { get; }
    }

    public interface IRepoResult<out TResult> : IRepoResult
    {
        TResult Value { get; }
    }

    public abstract class RepoBase<TContext> : IDisposable 
        where TContext : DbContext
    {
        private readonly TContext _context;

        protected RepoBase(TContext context)
        {
            _context = context;
        }

        protected IRepoResult<TResult> Wrap<TResult>(Func<TContext, TResult> action, DbContextTransaction transaction = null, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var moreActionsInTransaction = false;
            if (transaction == null)
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    moreActionsInTransaction = true;
                }
                else
                {
                    _context.Database.BeginTransaction(isolationLevel);
                }
            }
            else
            {
                _context.Database.UseTransaction(transaction.UnderlyingTransaction);
            }

            try
            {
                var result = action(_context);
                if (!moreActionsInTransaction)
                {
                    _context.Database.CurrentTransaction.Commit();
                }
                return new RepoResult<TResult>(result);
            }
            catch (Exception ex)
            {
                _context.Database.CurrentTransaction.Rollback();
                return new RepoResult<TResult>(new[] {ex});
            }
            finally
            {
                _context.Database.CurrentTransaction.Dispose();
            }
        } 


        public void Dispose()
        {
            _context.Dispose();    
        }
    }

    internal class RepoResult<TResult> : IRepoResult<TResult>
    {
        public RepoResult(TResult result)
        {
            Value = result;
        }

        public RepoResult(IEnumerable<Exception> exceptions)
        {
            Errors = exceptions;
        }

        public IEnumerable<Exception> Errors { get; private set; }
        public bool Success { get { return !Errors.Any(); } }
        public TResult Value { get; private set; }
    }

}

namespace Domain
{
    public class One
    {
        public One(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }

}