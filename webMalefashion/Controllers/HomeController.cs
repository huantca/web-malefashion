using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using webMalefashion.Models;
using X.PagedList;

namespace webMalefashion.Controllers
{
    public class HomeController : Controller
    {
        // MalefashionContext 
        MalefashionContext db = new MalefashionContext();
        private readonly ILogger<HomeController> _logger;
       // private object db;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // int pageSize = 4;// so san pham tren 1 trang
            // int pageNumber = page == null || page < 1 ? 1 : page.Value;
            //  var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            //// string? url = db.Products.ToList()[0].Options.ToList()[0].ImageUrl;
            // // bieu thua lamda
            // PagedList<Product> pageList = new
            // PagedList<Product>(lstsanpham, pageNumber, pageSize);

            // return View(pageList);

            var products= db.Products.Include(p=>p.Options);
            return View(products.ToList());
            //var products = db.Products.ToList(p => p.Options);
            //return View(products);


        }
        public IActionResult ShoppingCart()
        {

            return View();
        }
        public IActionResult SP(int ?page)
        {
            int pageSize = 4;// so san pham tren 1 trang
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            // bieu thua lamda
            PagedList<Product> pageList = new
            PagedList<Product>(lstsanpham, pageNumber, pageSize);
            return View(pageList);
        }
        public IActionResult SanPhamTheoLoai(string manufacturedname)
        {
            List<Manufacturer> lstsanpham = db.Manufacturers.Where(x=>x.Name==manufacturedname).OrderBy(x=>x.Name).ToList();
            return View(lstsanpham);
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