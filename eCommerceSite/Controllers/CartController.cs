using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Add(int id) // Id of product to add to the cart
        {
            // Get product from database
            // Add product to the cart cookie

            // Redirect back to previous page
            return View();
        }

        public IActionResult Summary()
        {
            // Display all products currently in the shopping cart cookie
            return View();
        }
    }
}
