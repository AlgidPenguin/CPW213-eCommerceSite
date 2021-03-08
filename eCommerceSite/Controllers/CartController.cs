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
            const string CartCookie = "CartCookie";

            // Get product from database
            Product p = await ProductDb.GetProductAsync(_context, id);

            // Get existing cart items
            string existingItems = _httpContext.HttpContext.Request.Cookies[CartCookie];

            List<Product> cartProducts = new List<Product>();

            if(existingItems != null)
            {
                cartProducts = JsonConvert.DeserializeObject<List<Product>>(existingItems);
            }

            // Add current product to existing cart
            cartProducts.Add(p);

            // Add product list to the cart cookie
            string data = JsonConvert.SerializeObject(cartProducts);

            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            _httpContext.HttpContext.Response.Cookies.Append(CartCookie, data, options);

            // Redirect back to previous page
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Summary()
        {
            // Display all products currently in the shopping cart cookie
            string cookieData = _httpContext.HttpContext.Request.Cookies["CartCookie"];

            List<Product> cartProducts = JsonConvert.DeserializeObject<List<Product>>(cookieData);

            return View(cartProducts);
        }
    }
}
