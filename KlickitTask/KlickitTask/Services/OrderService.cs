using KlickitTask.Data;
using KlickitTask.Models;

namespace KlickitTask.Services
{
    public class OrderService:IService<Order>
    {
        KlickitTaskEnteties db;
        public OrderService(KlickitTaskEnteties _db)
        {
            db = _db;
        }
        public int Creat(Order order)
        {

            db.Add(order);
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
            Order? order = db.orders.FirstOrDefault(o=> o.Id == id);
            if (order == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    db.Remove(order);
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

        public ICollection<Order> GetAll()
        {
            List<Order> orders = db.orders.ToList();
            return orders;
        }


        public Order GetById(int id)
        {
            Order? order = db.orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return null;
            else
                return order;
        }

        public int Update(int id, Order order)
        {
            Order? oldOrder = db.orders.FirstOrDefault(o => o.Id == id);
            if (oldOrder == null)
                return 0;
            else
            {
                oldOrder.OrderAddress = order.OrderAddress;
               

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
