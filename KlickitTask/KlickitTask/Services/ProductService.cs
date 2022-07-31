using KlickitTask.Data;
using KlickitTask.Models;

namespace KlickitTask.Services
{
    public class ProductService : IService<Product>, IServiceGetByName<Product>
    {
        KlickitTaskEnteties db;
        public ProductService(KlickitTaskEnteties _db)
        {
           db = _db;
        }
        public int Creat(Product product)
        {
        
            db.Add(product);
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
            Product? product = db.products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    db.Remove(product);
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

        public ICollection<Product> GetAll()
        {
            List<Product> products = db.products.ToList();
            return products;
        }

        public ICollection<Product> GetAllByName(string word)
        {
            List<Product> products = db.products.Where(p => p.Name.Contains(word)).ToList();
            return products;

        }

        public Product GetById(int id)
        {
            Product? product = db.products.FirstOrDefault(p => p.Id == id);
            if(product == null)
                return null;
            else
                return product;     
        }

        public int Update(int id, Product product)
        {
            Product? oldProduct = db.products.FirstOrDefault(p => p.Id == id);
            if(product == null)
                return 0;
            else
            {
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.Image = product.Image;
                oldProduct.Price = product.Price;
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
