using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using webMalefashion.Models;
using X.PagedList;

namespace webMalefashion.Controllers
{
    public class HomeController : Controller
    {
        // MalefashionContext 
        MalefashionContext db = new MalefashionContext();
       
        private string? pagedlst;
        private int page;
        private readonly ILogger<HomeController> _logger;
        // private object db;

        public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
        }

        public IActionResult Index(int? page)       
        {
            int pageSize = 4;// số sản phẩm trên 1 trang
            int pageNumber = page == null || page < 1 ? 1 : page.Value;

            var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }
            public IActionResult ShoppingCart()
        {

            return View();
        }
        public IActionResult SPMenu(int? page)
            
        {
            int pageSize = 4;// số sản phẩm trên 1 trang
            int pageNumber = page == null || page < 1 ? 1 : page.Value;

            var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);

            return View(lst);
          
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}