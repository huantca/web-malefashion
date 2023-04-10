using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using webMalefashion.Models;
using X.PagedList;

namespace webMalefashion.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    
    public class HomeAdminController : Controller
    {
        MalefashionContext db = new MalefashionContext();
        private readonly IWebHostEnvironment _webHost;
        public HomeAdminController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        [Route("")]
        [Route("index")]
        [Route("admin/homeadmin")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);

            return View(lst);
        }

        [Route("DanhSachNhanVien")]
        public IActionResult DanhSachNhanVien(int? page)
        {
            var lstNhanVien = db.Staff.ToList();

            return View(lstNhanVien);
        }

        [Route("ThemSanPham")]
        [HttpGet]
        public IActionResult ThemSanPham()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.ToList(), "Id", "Name");
            Product product = new Product();
            
            product.Options.Add(new Option() { ProductId = 1 });
            product.Id = db.Products.Count();
            return View(product);
        }
        [Route("ThemSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPham(Product sanPham)
        {
            for (int i = 0; i < sanPham.Options.Count; i++)
            {
                _ = UploadFile(sanPham.Options[i].ProductPhoto);
                string uniqueFileName = sanPham.Options[i].ProductPhoto.FileName;
                sanPham.Options[i].ImageUrl = uniqueFileName;
            }
            
            db.Products.Add(sanPham);
            db.SaveChanges();
            return RedirectToAction("DanhMucSanPham");                    
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.ToList(), "Id", "Name");
            Product product = db.Products.Include(e => e.Options)
                .Where(a => a.Id == id)
                .FirstOrDefault();
            return View(product);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(Product sanPham)
        {
            List<Option> options = db.Options.Where(d => d.ProductId == sanPham.Id).ToList();
            db.Options.RemoveRange(options);
            db.SaveChanges();

            //sanPham.Options.RemoveAll(n => n.Id == sanPham.Id);
            for (int i = 0; i < sanPham.Options.Count; i++)
            {
                if (sanPham.Options[i].ProductPhoto != null)
                {
                     _ = UploadFile(sanPham.Options[i].ProductPhoto);
                     string uniqueFileName = sanPham.Options[i].ProductPhoto.FileName;
                     sanPham.Options[i].ImageUrl = uniqueFileName;
                }
                
            }
            
            db.Attach(sanPham);
            db.Entry(sanPham).State = EntityState.Modified;
            db.Options.AddRange(sanPham.Options);
            
            db.SaveChanges();
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");           
        }

        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int id)
        {
            TempData["Message"] = "";
            var option = db.Options.Where(x => x.ProductId == id).ToList();
            if (option.Count > 0)
            {
                TempData["Message"] = "Không xóa được sản phẩm này ";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            var manufacter = db.Manufacturers.Where(x => x.Id == id);
            if (manufacter.Any())
            {
                db.RemoveRange(manufacter);
            }
            var category = db.Categories.Where(x => x.Id == id);
            if (category.Any())
            {
                db.RemoveRange(category);
            }
            db.Remove(db.Products.Find(id));
            db.SaveChanges();
            TempData["Message"] = "Sản phầm này đã được xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }

        [Route("ChiTietSanPham")]
        [HttpGet]
        public IActionResult ChiTietSanPham(int id)
        {
           
            Product product = db.Products.Include(e => e.Options)
                .Where(a => a.Id == id)
                .FirstOrDefault();
            return View(product);
        }
        
        private async Task<bool> UploadFile(IFormFile ufile)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(ufile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\malefashion\img\product", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ufile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }
    }
}

