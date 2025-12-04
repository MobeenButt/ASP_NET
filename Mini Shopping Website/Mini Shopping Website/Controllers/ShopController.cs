using Microsoft.AspNetCore.Mvc;
using Mini_Shopping_Website.Models;
using System.Text.Json;

namespace Mini_Shopping_Website.Controllers
{
    public class ShopController : Controller
    {

        private List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99M },
            new Product { Id = 2, Name = "Smartphone", Price = 499.99M },
            new Product { Id = 3, Name = "Headphones", Price = 199.99M }
        };
        [HttpGet]
        public IActionResult ProductList()
        {
            return View(products);
        }
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product=products.FirstOrDefault(p=>p.Id==productId);
            if(product==null)
            {
                return RedirectToAction("ProductList");
            }

            //read existing cookie
            List<Product> cartItems = new List<Product>();
            string existCookie = HttpContext.Request.Cookies["UserCart"];
            if(!string.IsNullOrEmpty(existCookie))
            {
                cartItems = JsonSerializer.Deserialize<List<Product>>(existCookie);
            }
            cartItems.Add(product);

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(2)
            };
            string newCookie = JsonSerializer.Serialize(cartItems);
            HttpContext.Response.Cookies.Append("UserCart", newCookie, options);
            return RedirectToAction("ProductList");
        }
        
        [HttpGet]
        public IActionResult CartDisplay()
        {
            string data = HttpContext.Request.Cookies["UserCart"];
            List<Product> cart = new List<Product>();
            if(!string.IsNullOrEmpty(data))
            {
                cart = JsonSerializer.Deserialize<List<Product>>(data);
            }
            return View(cart);
        }
        [HttpPost]
        public IActionResult ClearCart()
        {
            HttpContext.Response.Cookies.Delete("UserCart");
            return View("CartDisplay");
        }
    }
}
