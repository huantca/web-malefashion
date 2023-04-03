using Microsoft.AspNetCore.Mvc;
using webMalefashion.Responsitory;

namespace webMalefashion.ViewComponents
{
    public class LoaiPriceMenusViewComponent: ViewComponent
    {
        private readonly ILoaiBrandResponsitory _loaiPrice;
        public LoaiPriceMenusViewComponent(ILoaiBrandResponsitory loaiBrandResponsitory)
        {
            _loaiPrice = loaiBrandResponsitory;
        }
        public IViewComponentResult Invoke()
        {
            var loaiprice = _loaiPrice.GetAllLoaiPrice().OrderBy(x => x.Price);
            //categoty


            return View(loaiprice);

        }




    }
}
