using Microsoft.EntityFrameworkCore;
using TaskManager.Domain;
using TaskManager.Domain.Interfaces;

namespace TaskManager.DataAccessLayer
{
    //Repository Design Pattern
    //A Generic Repository which will service CRUD operations for each Domain entity inherited from SiBaseEntity class:
    public abstract class RepoBase<T> : IRepoBase<T> where T : SiBaseEntity 
    {
        protected readonly TaskManagerDBContext _dbContext;
        protected readonly DbSet<T> _entitiySet;
        public RepoBase(TaskManagerDBContext dbContext)
        {
            _dbContext = dbContext;
            _entitiySet = _dbContext.Set<T>();
        }
        // Add new entity to DB Context
        public void Add(T entity)
        {
            _dbContext.Attach(entity);
        }

        //Get all entities from the database
        //Needed to be virtual to allow overrides later to allow tuning of the query (Includes,...)
        public virtual IEnumerable<T> GetAll()
        {
            var res = _entitiySet;
            return res.AsEnumerable();
        }
        public virtual IQueryable<T> GetAllQuery()
        {
            var res = _entitiySet;
            return res;
        }

        //Get single entity.
        //Needed to be virtual to allow overrides later to allow tuning of the query (Includes,...)
        public virtual IQueryable<T> GetEntity(int id)  
        {       
            var qry = _entitiySet.Where(x => x.Id == id);
            return qry;
        }

        //Removes specified entity
        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        }
        //Updates specified entity
        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }
    }
}
