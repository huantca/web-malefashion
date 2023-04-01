using Microsoft.AspNetCore.Mvc;
using webMalefashion.Models;
using webMalefashion.Responsitory;

namespace webMalefashion.ViewComponents
{
    public class LoaiCategoryViewComponent : ViewComponent
    {
        private readonly ILoaiBrandResponsitory _loaiCategory;
        public LoaiCategoryViewComponent(ILoaiBrandResponsitory loaiBrandResponsitory)
        {
            _loaiCategory = loaiBrandResponsitory;
        }
        public IViewComponentResult Invoke()
        {
            var loaicategory = _loaiCategory.GetAllLoaiCategory().OrderBy(x => x.Id);
            //categoty


            return View(loaicategory);

        }

        
    }
}
