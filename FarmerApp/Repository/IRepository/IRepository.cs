namespace FarmerApp.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        void Add(T t);
        void Remove(int id);
        void Update(T t);
        T GetById(int id);
    }
}