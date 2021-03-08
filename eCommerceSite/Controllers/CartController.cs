using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        
        /// <summary>
        /// Adds a product to the shopping cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id) // Id of product to add to the cart
        {
            // Get product from database
            Product p = await ProductDb.GetProductAsync(_context, id);

            // Add product to cart
            CookieHelper.AddProductToCart(_httpContext, p);

            // Redirect back to previous page
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Summary()
        {
            // Display all products currently in the shopping cart cookie
            return View(CookieHelper.GetCartProducts(_httpContext));
        }
    }
}
