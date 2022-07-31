using KlickitTask.Models;
using KlickitTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KlickitTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IService<Order> OrderSrvice;
        public OrderController(IService<Order> _OrderSrvice)
        {
            OrderSrvice = _OrderSrvice;
        }

        //getAll
        [HttpGet]
        public ActionResult getAll()
        {
            if (OrderSrvice.GetAll().Count > 0)
                return Ok(OrderSrvice.GetAll());
            else
                return NotFound();

        }
        //getById
        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            Order? order = OrderSrvice.GetById(id);
            if (order == null)
                return NotFound();
            else
                return Ok(order);
        }
        //Create
        [HttpPost]
        public ActionResult Add(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OrderSrvice.Creat(order);
                    return Created("uri", order);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException);
                }

            }
            else
            {
                return BadRequest();
            }
        }
        //update
        [HttpPut("{id}")]
        public ActionResult update(int id, Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OrderSrvice.Update(id, order);
                    return Ok(order);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        //Delete
        [HttpDelete("{id}")]
        public ActionResult delete(int id)
        {
            int numOfRows = OrderSrvice.Delete(id);
            if (numOfRows <= 0)
                return NotFound();
            else
                return NoContent();
        }
    }
}
