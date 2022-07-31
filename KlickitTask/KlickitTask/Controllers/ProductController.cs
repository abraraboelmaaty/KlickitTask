using KlickitTask.Models;
using KlickitTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KlickitTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IService<Product> ProSrvice;
        public ProductController(IService<Product> _ProSrvice)
        {
            ProSrvice = _ProSrvice;
        }

        //getAll
        [HttpGet]
        public ActionResult getAll()
        {
            if (ProSrvice.GetAll().Count > 0)
                return Ok(ProSrvice.GetAll());
            else
                return NotFound();

        }
        //getById
        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            Product? product = ProSrvice.GetById(id);
            if (product == null)
                return NotFound();
            else
                return Ok(product);
        }
        //Create
        [HttpPost]
        public ActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProSrvice.Creat(product);
                    return Created("uri", product);
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
        public ActionResult update(int id,Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProSrvice.Update(id,product);
                    return Ok(product);
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
            int numOfRows  = ProSrvice.Delete(id);
            if (numOfRows <= 0)
                return NotFound();
            else
                return NoContent();
        }
    }
}
