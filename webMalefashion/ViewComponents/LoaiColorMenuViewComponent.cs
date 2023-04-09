using Microsoft.AspNetCore.Mvc;
using webMalefashion.Responsitory;

namespace webMalefashion.ViewComponents
{
    public class LoaiColorMenuViewComponent: ViewComponent
    {
        private readonly ILoaiBrandResponsitory _loaiColor;
        public LoaiColorMenuViewComponent(ILoaiBrandResponsitory loaiBrandResponsitory)
        {
            _loaiColor = loaiBrandResponsitory;
        }
        public IViewComponentResult Invoke()
        {
            var loaicolor = _loaiColor.GetAllLoaiColor().Select(x => x.ColorHex).Distinct().ToList();
;
            //


            return View(loaicolor);

        }




    }
}
