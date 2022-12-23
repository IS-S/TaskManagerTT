using Microsoft.EntityFrameworkCore.Storage;
using TaskManager.DataAccessLayer.Repositories;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.IRepositories;

namespace TaskManager.DataAccessLayer.UnitOfWork
{
    //The Repository Design Pattern 
    //Unit of Work: is used to group one or more CRUD operations into a single transaction so that all operations either pass or fail as one unit
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        //Class to perform CRUD operations with Projects
        private IRepoBase<SiProject>? _repoProjects;

        private IRepoBase<SiTask>? _repoTasks; // instance to work with Tasks directly
        private bool _disposed;
        private readonly TaskManagerDBContext _dbContext;
        private IDbContextTransaction? _dbTransaction;

        //Using the Constructor we are initializing the _dbContext field via injection
        public UnitOfWork(TaskManagerDBContext dbContext) 
        {
            //get DB context for current request
            _dbContext = dbContext;
            //Turn off Lazy Loading. Referenced objects will be loaded only if there is an explicit instruction to do it
            _dbContext.ChangeTracker.LazyLoadingEnabled = false;
        }

        public IRepoBase<SiProject> RepoProjects
        {
            get
            {
                //if empty, load instance from DBContext
                return _repoProjects = _repoProjects ?? new RepoProjects(_dbContext);
            }
        }
        public IRepoBase<SiTask> RepoTasks
        {
            get { return _repoTasks = _repoTasks ?? new RepoTasks(_dbContext); } // if empty, load instance from DBContext
        }
        public void Commit()
        { 
            _dbContext.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void TransactionBegin()
        {
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }
        public void TransactionCommit()
        {
            _dbTransaction?.Commit();
        }
        public void TransactionRollback()
        {
            _dbTransaction?.Rollback();
        }

        #region Dispose
        //TODO: needs more investigation before usage: how it will interact within the scope
        //To free database connections etc. at any time.
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _dbContext.Dispose();
            _disposed = true;
        }
        #endregion 
    }
}
