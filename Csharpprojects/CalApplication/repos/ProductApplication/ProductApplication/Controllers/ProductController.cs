using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.Interface;
using ProductApplication.Model;
using ProductApplication.Repository;
using System.Text.RegularExpressions;

namespace ProductApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _repository;

        public ProductController(IProduct repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult create(ProductProperties Product)
        {
            return _repository.Create(Product);
        }
        //[HttpGet]
        //public IActionResult select(int page, int pagesize)
        //{
        //    return _repository.select(page, pagesize);
        //}
        //[HttpGet("search")]
        //public IActionResult selecta(string search)
        //{
        //    return _repository.selectsearch(search);
        //}
        [HttpGet]
        public IActionResult select(string? search,int page, int pagesize)
        {
            return _repository.select(search,page, pagesize);
        }
        [HttpPut]
        public ActionResult update(ProductProperties Product)
        {
            return _repository.update(Product);
        }
        [HttpDelete]
        public IActionResult delete(int Id)
        {
            _repository.delete(Id);
            return NoContent();
        }
    }
}
