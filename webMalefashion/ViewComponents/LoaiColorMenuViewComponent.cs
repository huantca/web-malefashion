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
            var loaicolor = _loaiColor.GetAllLoaiColor().OrderBy(x => x.SizeId);
            //


            return View(loaicolor);

        }




    }
}
