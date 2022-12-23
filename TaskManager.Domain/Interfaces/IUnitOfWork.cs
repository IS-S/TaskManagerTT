using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Domain.IRepositories
{
    public interface IUnitOfWork
    {
        IRepoBase<SiTask> RepoTasks { get; }
        IRepoBase<SiProject> RepoProjects { get; }

        void Commit();
        Task CommitAsync();
        void TransactionBegin();
        void TransactionCommit();
        void TransactionRollback();

    }
}
