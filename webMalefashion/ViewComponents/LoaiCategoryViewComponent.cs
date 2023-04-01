using Microsoft.AspNetCore.Mvc;
using webMalefashion.Respository;

namespace webMalefashion.ViewComponents
{
    public class LoaiCategoryViewComponent:ViewComponent
    {
        private readonly ILoaiBranchRepository _loaiCategory;
        public LoaiCategoryViewComponent(ILoaiBranchRepository loaiBranchRepository)
        {
          //  _loaiBranch = loaiBranchRepository;
            _loaiCategory = loaiBranchRepository;
        }
        public IViewComponentResult Invoke()
        {
            //    //    //var loaibranch = _loaiBranch.GetAllLoaiBranch().OrderBy(X => X.Name);
            var loaicategory = _loaiCategory.GetAllCategories().OrderBy(X => X.Name);
            return View(loaicategory);
        }
    }
}
