using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductApplication.Data;
using ProductApplication.Interface;
using ProductApplication.Model;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProductApplication.Repository
{
    public class ProductRepsitory : ControllerBase, IProduct
    {
        private readonly ProductContext _context;

        public ProductRepsitory(ProductContext context)
        {
            _context = context;
        }
        public ActionResult Create(ProductProperties Product)
        {            
            Regex regex = new Regex(@"[\d]");
           
            if (Product.Product_Name == "")
            {
                return BadRequest("Product name must be filled out");
            }
            else if (Product.Description == "")
            {
                return BadRequest("Product description cannot be null");
            }
          
            else if (!regex.IsMatch(Product.Price))
            {
                return BadRequest("Price should be in number");
            }
            else
            {
                _context.ProductTable.Add(Product);
                _context.SaveChanges();
                return Ok();
            }
        }
     
        public IActionResult select(string? search,int page, int pagesize)
        {
            IQueryable<ProductProperties> Products = _context.ProductTable;
           
            if ((search != null) && (page == 0 || pagesize == 0))
            {
                Products = _context.ProductTable.Where(p => p.Product_Name.ToLower().Contains(search.ToLower()));
                return Ok(Products);
            }
            else if ((page != 0 || pagesize != 0) &&(search==null))
            {
                var pagedProducts = Products.Skip((page - 1) * pagesize).Take(pagesize);
                return Ok(pagedProducts);
            }
            else if (search == null || page == 0 || pagesize == 0)
            {
                return Ok(_context.ProductTable.ToList());
            }
            else
            {
                Products = _context.ProductTable.Where(p => p.Product_Name.ToLower().Contains(search.ToLower()));
                var pagedProducts = Products.Skip((page - 1) * pagesize).Take(pagesize);
                return Ok(pagedProducts);
            }
           
        }
        public ActionResult update(ProductProperties Product)
        {         
            Regex regex = new Regex(@"[\d]");
            if (Product.Product_Name == "")
            {
                return BadRequest("Product name must be filled out");
            }
            else if (Product.Description == "")
            {
                return BadRequest("Product description cannot be null");
            }
           
            else if (!regex.IsMatch(Product.Price))
            {
                return BadRequest("Price should be in number");
            }
            else
            {
                _context.ProductTable.Update(Product);
                _context.SaveChanges();
                return Ok();
            }
        }
        public void delete(int Id)
        {
            var delete = _context.ProductTable.FirstOrDefault(b => b.Id == Id);
            _context.ProductTable.Remove(delete);
            _context.SaveChanges();

        }
    }
}
