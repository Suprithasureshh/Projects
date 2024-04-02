using Microsoft.AspNetCore.Mvc;
using ProductApplication.Model;

namespace ProductApplication.Interface
{
    public interface IProduct
    {
        ActionResult Create(ProductProperties Product );
        IActionResult select(string? search,int page, int pagesize);
        //IActionResult selectsearch(string search);
        ActionResult update(ProductProperties Product);
        void delete(int Id);


    }
}
