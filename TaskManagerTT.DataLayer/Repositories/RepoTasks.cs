using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.DataAccessLayer.Repositories
{
    // Class inherited from Base Repositpry class to have possibility of including linked Projects to each Task
    public class RepoTasks : RepoBase<SiTask> 
    {
        public RepoTasks(TaskManagerDBContext context) : base(context)
        {

        }
        // Get All Tasks instances (overrided to include linked Projects to each task)
        public override IQueryable<SiTask> GetAll() 
        {
            IQueryable<SiTask> qry = base.GetAll().AsQueryable();
            qry = qry.Include(x => x.Project);
            return qry;
        }
        // Get All Project instances (overrided to include linked Tasks to each Project)
        public override IQueryable<SiTask> GetAllQuery() 
        {
            IQueryable<SiTask> qry = base.GetAllQuery();
            qry = qry.Include(x => x.Project);
            return qry;
        }

        // Get Single Tasks instance (overrided to include linked Project to task)
        public override IQueryable<SiTask> GetEntity(int id) 
        {
            var qry = base.GetEntity(id);
            qry = qry.Include(x=>x.Project);
            return qry;
        }
    }
}
