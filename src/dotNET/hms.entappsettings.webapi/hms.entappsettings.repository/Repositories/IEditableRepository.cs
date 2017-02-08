namespace hms.entappsettings.repository.Repositories
{
    public interface IEditableRepository<T> : IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}