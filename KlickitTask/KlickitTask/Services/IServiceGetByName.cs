namespace KlickitTask.Services
{
    public interface IServiceGetByName<T>
    {
        public ICollection<T> GetAllByName(string word);
    }
}
