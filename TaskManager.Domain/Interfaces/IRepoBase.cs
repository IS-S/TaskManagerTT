

namespace TaskManager.Domain.Interfaces
{
    //This repository will be implemented by any future entity specific repository we create
    public interface IRepoBase<T> where T : SiBaseEntity
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQuery();
        void Add(T entity);
        public void Remove(T entity);
        public void Update(T entity);
        IQueryable<T> GetEntity(int id);
    }
}
