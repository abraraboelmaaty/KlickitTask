using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KlickitTask.Models;
using KlickitTask.Services;

namespace KlickitTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        IServiceGetByName<Product> ProSrvice;
        public SearchController(IServiceGetByName<Product> _ProSrvice)
        {
            ProSrvice = _ProSrvice;
        }
        [HttpGet("{s}")]
        public ActionResult getAny(string s)
        {
            List<Product> products = ProSrvice.GetAllByName(s).ToList();
            if (products.Count > 0)
                return Ok(products);
            else
            {
                return NotFound();
            }

        }
    }
}
