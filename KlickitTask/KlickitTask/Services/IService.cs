namespace KlickitTask.Services
{
    public interface IService<T>
    {
        public ICollection<T> GetAll();
        public T GetById(int id);
        public  int Creat(T entity);
        public int Update(int id,T entity);
        public int Delete(int id);
       
    }
}
