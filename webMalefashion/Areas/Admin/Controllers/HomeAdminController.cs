using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View();
        }
        [Route("ThemSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPham(Product sanPham)
        {
            if (ModelState.IsValid)
            {
                int a = 0;
                foreach(var index in db.Products)
                {
                    if(index.Id.Equals(sanPham.Id))
                    {
                        Response.WriteAsync("ID bị trùng", default);
                        a = 1;
                    }
                    else
                    {
                        a = 0;
                    }
                }
                if (a == 0)
                {
                    db.Products.Add(sanPham);
                    db.SaveChanges();
                }
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.ToList(), "Id", "Name");
            var sanPham = db.Products.Find(id);
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(Product sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(sanPham);
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
            var detail = db.Products.Where(x => x.Id == id).FirstOrDefault();
            return View(detail);
        }
    }
}
