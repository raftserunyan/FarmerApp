namespace FarmerApp.Services.IServices
{
    public interface IService<T> where T : class
    {
        List<T> GetAll();
        void Add(T t);
        void Remove(int id);
        void Update(T t);
    }
}