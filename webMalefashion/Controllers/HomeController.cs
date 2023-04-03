using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using webMalefashion.Models;
using X.PagedList;

namespace webMalefashion.Controllers
{
    public class HomeController : Controller
    {
        // MalefashionContext 
        MalefashionContext db = new MalefashionContext();
       
        //private string? pagedlst;
        //private int page;
        private readonly ILogger<HomeController> _logger;
        // private object db;

        public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
        }

        //public IActionResult Index(int? page)
        //{
        //    int pageSize = 4;// số sản phẩm trên 1 trang
        //    int pageNumber = page == null || page < 1 ? 1 : page.Value;

        //    var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
        //    PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);

        //    return View(lst);
        //}
        //csdl
        public IActionResult Index()
        {
            var products = db.Products.Include(p=>p.Options);
            return View(products.ToList());
        }
        public IActionResult SanPhamTheoLoai(int maloai)
        {
            
            List<Product> lstsanpham = db.Products.Where(x=> x.ManufacturerId== maloai).OrderBy(x=> x.Name).Include(p=>p.Options).ToList();
           
            //ViewBag.maloai = maloai;
            return View(lstsanpham);
        }
        public IActionResult SanPhamTheoCategory(int maloai)
        {
            List<Product> lstsanpham = db.Products.Where(x => x.CategoryId == maloai).OrderBy(x => x.Name).Include(p => p.Options).ToList();

            //ViewBag.maloai = maloai;
            return View(lstsanpham);
        }
        public IActionResult SanPhamTheoPrice(decimal priced)
        {
            var products = db.Options.Where(x=>x.Price== priced);
            return View(products.ToList());
        }

        public IActionResult SanPhamTheoSize(string size)
        {
            var products = db.Options.Where(x => x.SizeId == size);
            return View(products.ToList());
        }

        public IActionResult SanPhamTheoColor(string color)
        {
            var products = db.Options.Where(x => x.ColorHex == color);
            return View(products.ToList());
        }

        public IActionResult ShoppingCart()
        {

            return View();
        }
        public IActionResult IndexDetail()
        {
            return View();

        }
        public IActionResult ChiTietSanPham(string maSp)
        {
            var sanpham = db.Products.SingleOrDefault(x => x.Name == maSp);
            var anhsanpham = db.Products.Where(x => x.Name== maSp).ToList();
            ViewBag.anhsanpham = anhsanpham;
            return View(sanpham);

        }
        //public IActionResult SPMenu()
            
        //{
        //    //int pageSize = 12;// số sản phẩm trên 1 trang
        //    //int pageNumber = page == null || page < 1 ? 1 : page.Value;

        //    //var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
        //    //PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);

        //    //return View(lst);

        //    var products = db.Products.Include(p => p.Options);
        //    return View(products.ToList());

        //}
        public IActionResult SPMenu(int? page)
        {
            //int pageSize = 4;// so san pham tren 1 trang
            //int pageNumber = page == null || page < 1 ? 1 : page.Value;
            //var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            var products = db.Products.Include(p => p.Options);
            return View(products.ToList());
            // bieu thua lamda
            //PagedList<Product> pageList = new
            //PagedList<Product>(lstsanpham, pageNumber, pageSize);
            //return View(pageList);
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
