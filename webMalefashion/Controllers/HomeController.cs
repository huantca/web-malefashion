using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webMalefashion.Models;
using webMalefashion.ViewModels;

namespace webMalefashion.Controllers
{
    public class HomeController : Controller
    {
        MalefashionContext db=new MalefashionContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductDetail(int MaSP)
        {
            var sanPham = db.Products.SingleOrDefault(x => x.Id == MaSP);
            var options=db.Options.Where(x=>x.ProductId == MaSP).ToList();
            var homeProductDetaulViewModel = new HomeProductDetailViewModel{
                product=sanPham,
                options=options }
            ;
            //var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == MaSP);
            //var anhSanPham = db.TAnhSps.Where(x => x.MaSp == MaSP).ToList();
            ////TDanhMucSp sp=db.TDanhMucSps.SingleOrDefault(x => x.MaSp==MaSP);

            
            ////db.TAnhSps.Where(x=>x.MaSp==MaSP).ToList();

            return View(homeProductDetaulViewModel);
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