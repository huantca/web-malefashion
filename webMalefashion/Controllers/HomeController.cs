using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using webMalefashion.Models;
using webMalefashion.ViewModels;
using X.PagedList;

namespace webMalefashion.Controllers
{
    public class HomeController : Controller
    {
        // MalefashionContext 
        MalefashionContext db = new();
       
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
        public IActionResult Index(int? page)
        {
            int pageSize = 8;

            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Products.Include(p => p.Options).AsNoTracking().OrderBy(x => x.Name);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult SanPhamTheoLoai(int maloai)
        {
            
            List<Product> lstsanpham = db.Products.Where(x=> x.ManufacturerId== maloai).OrderBy(x=> x.Name).Include(p=>p.Options).ToList();
           
            //ViewBag.maloai = maloai;
            return View(lstsanpham);
        }
        public IActionResult SanPhamTheoCategory(int maloai, int? page)
        {

            int pageSize = 12;// so san pham tren 1 trang
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstsanpham = db.Products.Where(x => x.CategoryId == maloai).OrderBy(x => x.Name).Include(p => p.Options).ToList();
            PagedList<Product> pageList = new PagedList<Product>(lstsanpham, pageNumber, pageSize);

            ViewBag.maloai = maloai;
            return View(pageList);
        }
        public IActionResult SanPhamTheoPrice(decimal priced)
        {
            var products = db.Options.Where(x=>x.Price== priced);
            return View(products.ToList());
        }

        public IActionResult SanPhamTheoSize(string size, int? page)
        {
            int pageSize = 3;// so san pham tren 1 trang
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
             var lstsanpham = db.Options.Where(x => x.SizeId == size);
           // var lstsanpham = db.Products.Where(x => x.SizeI)
            PagedList<Option> pageList = new PagedList<Option>(lstsanpham, pageNumber, pageSize);

            ViewBag.size = size;
            return View(pageList);

        }
        [HttpPost]
        public IActionResult SearchProducts(string price_range)
        {
            // split the price range string into price_from and price_to
            var rangeValues = price_range.Split('-');
            var price_from = decimal.Parse(rangeValues[0]);
            var price_to = decimal.Parse(rangeValues[1]);

            // search products based on price range
            var products = db.Options
                .Where(p => p.Price >= price_from && p.Price <= price_to)
                .ToList();

            // return search results
            return View(products);
        }
        public IActionResult SanPhamTheoColor(string color)
        {
           // int pageSize = 3;// so san pham tren 1 trang
           // int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var products = db.Options.Where(x => x.ColorHex == color);
           // PagedList<Option> pageList = new PagedList<Option>(products, pageNumber, pageSize);

            //ViewBag.color = color;
            return View(products.ToList());
        }

        public IActionResult ShoppingCart()
        {
            var securityToken = new JwtSecurityToken(Request.Cookies["token"]);
            var data = securityToken.Claims.First(c => c.Type == ClaimTypes.UserData).Value;
        
            Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(data);
            customer = db.Customers.First(c => c.Id == customer.Id);
            var cartDetails = db.CartDetails.Where(c => c.CustomerId == customer.Id).ToList();
            return View(cartDetails);
        }
        
        [HttpPut]
        [Authorize]
        [Route("api/cart/update")]
        public IActionResult UpdateCart(int amount, int productId) {
            if (amount < 0) {
                var cartDetail = db.CartDetails.First(c => c.ProductId == productId);
                db.CartDetails.Remove(cartDetail);
                db.SaveChanges();
            }
            else {
                var cartDetail = db.CartDetails.First(c => c.ProductId == productId);
                cartDetail.Amount = amount;
                db.SaveChanges();
            }
            return Ok("cart updated");
        }

        public Product GetProductById(int id) {
            return db.Products.Include(p => p.Options).First(p => p.Id == id);
        }

        [HttpGet]
        [Authorize]
        [Route("api/cart/total")]
        public float GetTotalMoney() {
            var securityToken = new JwtSecurityToken(Request.Cookies["token"]);
            var data = securityToken.Claims.First(c => c.Type == ClaimTypes.UserData).Value;
        
            Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(data);
            customer = db.Customers.First(c => c.Id == customer.Id);
            var cartDetails = db.CartDetails.Where(c => c.CustomerId == customer.Id).ToList();
            float res = 0f;
            foreach (var detail in cartDetails) {
                var product = db.Products.Include(p => p.Options).First(p => p.Id == detail.ProductId);
                res += (float)(detail.Amount * product.Options.ToList()[0].Price);
            }
            return res;
        }
        
        public IActionResult IndexDetail()
        {
            return View();
        }
        public IActionResult ChiTietSanPham(int maSp)
        {
            var sanpham = db.Products.SingleOrDefault(x => x.Id == maSp);
            var options = db.Options.Where(x => x.ProductId == maSp).ToList();
            var homeProductDetaulViewModel = new HomeProductDetailViewModel
            {
                product = sanpham,
                options = options
            };
            return View(homeProductDetaulViewModel);

        }

        public IActionResult SPMenu(int? page)
        {
            int pageSize = 12;// so san pham tren 1 trang
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            //var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            var products = db.Products.Include(p => p.Options);
            //return View(products.ToList());
            // bieu thua lamda
            PagedList<Product> pageList = new
            PagedList<Product>(products, pageNumber, pageSize);
            return View(pageList);
        }

        public IActionResult UserDetails() {
            var securityToken = new JwtSecurityToken(Request.Cookies["token"]);
            var data = securityToken.Claims.First(c => c.Type == ClaimTypes.UserData).Value;
        
            Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(data);
            customer = db.Customers.First(c => c.Id == customer.Id);
            return View(customer);
        }

        [HttpPost]
        [Authorize]
        [Route("api/add-to-cart")]
        public IActionResult AddToCart(int id) {
            // get user
            var securityToken = new JwtSecurityToken(Request.Cookies["token"]);
            var data = securityToken.Claims.First(c => c.Type == ClaimTypes.UserData).Value;
        
            Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(data);
            customer = db.Customers.First(c => c.Id == customer.Id);

            
            CartDetail cartDetail = new CartDetail();
            cartDetail.CustomerId = customer.Id;
            cartDetail.ProductId = id;
            cartDetail.OptionId = db.Products.Include(p => p.Options).ToList()[0].Id;
            cartDetail.Amount = 1;

            db.CartDetails.Add(cartDetail);
            db.SaveChangesAsync();
            return Ok("added to cart" + id);
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