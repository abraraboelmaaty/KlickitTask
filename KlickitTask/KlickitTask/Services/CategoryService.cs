using KlickitTask.Data;
using KlickitTask.Models;

namespace KlickitTask.Services
{
    public class CategoryService:IService<Category>
    {
        KlickitTaskEnteties db;
        public CategoryService(KlickitTaskEnteties _db)
        {
            db = _db;
        }
        public int Creat(Category category)
        {

            db.Add(category);
            try
            {
                int row = db.SaveChanges();
                return row;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return -1;
            }
        }

        public int Delete(int id)
        {
            Category? category = db.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    db.Remove(category);
                    int rows = db.SaveChanges();
                    return rows;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    return -1;
                }
            }


        }

        public ICollection<Category> GetAll()
        {
            List<Category> categories = db.categories.ToList();
            return categories;
        }


        public Category GetById(int id)
        {
            Category? category = db.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return null;
            else
                return category;
        }

        public int Update(int id, Category category)
        {
            Category? oldCategory = db.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return 0;
            else
            {
                oldCategory.Name = category.Name;
                oldCategory.Description = category.Description;
                
                try
                {
                    int rows = db.SaveChanges();
                    return rows;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    return -1;
                }
            }
        }
    }
}
