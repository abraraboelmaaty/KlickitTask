using KlickitTask.Models;
using KlickitTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KlickitTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        IService<Category> CatSrvice;
        public CategoryController(IService<Category> _CatSrvice)
        {
            CatSrvice = _CatSrvice;
        }

        //getAll
        [HttpGet]
        public ActionResult getAll()
        {
            if (CatSrvice.GetAll().Count > 0)
                return Ok(CatSrvice.GetAll());
            else
                return NotFound();

        }
        //getById
        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            Category? category = CatSrvice.GetById(id);
            if (category == null)
                return NotFound();
            else
                return Ok(category);
        }
        //Create
        [HttpPost]
        public ActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CatSrvice.Creat(category);
                    return Created("uri", category);
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
        public ActionResult update(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CatSrvice.Update(id, category);
                    return Ok(category);
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
            int numOfRows = CatSrvice.Delete(id);
            if (numOfRows <= 0)
                return NotFound();
            else
                return NoContent();
        }
    }
}
