using Microsoft.AspNetCore.Mvc;
using Mini_Shopping_Website.Models;

namespace Mini_Shopping_Website.Controllers
{
    public class ShopController : Controller
    {

        private List<Product>products = new List<Product>
        {
            new Product { Name = "Laptop", Price = 999.99M },
            new Product { Name = "Smartphone", Price = 499.99M },
            new Product { Name = "Headphones", Price = 199.99M }
        };
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            HttpContext.Response.Cookies.Append("Cart", productId.ToString());
            return View();
        }
        [HttpGet]
        public IActionResult ProductList()
        {
            return View(products);
        }
        [HttpGet]
        public IActionResult CartDisplay()
        {
            string data = String.Empty;
            if(HttpContext.Request.Cookies.ContainsKey("Cart"))
            {
                data=HttpContext.Request.Cookies["Cart"];
            }

            return View("CartDisplay", data);
        }
        [HttpPost]
        public IActionResult ClearCart()
        {
            HttpContext.Response.Cookies.Delete("Cart");
            return View("Cart", new List<CartItem>());
        }
    }
}
