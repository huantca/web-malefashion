using webMalefashion.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using webMalefashion.Respository;

namespace webMalefashion.ViewComponents
{
    public class LoaiBranchMenuViewComponent: ViewComponent
    {
        private readonly ILoaiBranchRepository _loaiBranch;
       // private readonly ILoaiBranchRepository _loaiCategory;
        public LoaiBranchMenuViewComponent(ILoaiBranchRepository loaiBranchRepository)
        {
            _loaiBranch = loaiBranchRepository;
            //_loaiCategory = loaiBranchRepository;
        }
        //public LoaiBranchMenuViewComponent(ILoaiBranchRepository loaiBranchRepository)
        //{
        //    _loaiCategory = loaiBranchRepository;
        //}
        public IViewComponentResult Invoke()
        {
            var loaibranch = _loaiBranch.GetAllLoaiBranch().OrderBy(X => X.Name);
          // var loaicategory = _loaiCategory.GetAllCategories().OrderBy(X => X.Name);
            return View(loaibranch);
        }

        
        //public IViewComponentResult Invoke1()
        //{
        ////    //    //var loaibranch = _loaiBranch.GetAllLoaiBranch().OrderBy(X => X.Name);
        //    var loaicategory = _loaiCategory.GetAllCategories().OrderBy(X => X.Name);
        //    return View(loaicategory);
        //}
        //private IViewComponentResult View(IOrderedEnumerable<Manufacturer> loaibranch, IOrderedEnumerable<Category> loaicategory)
        //{
        //    return (IViewComponentResult)_loaiBranch;
        //}
    }
}
