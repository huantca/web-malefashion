using Microsoft.AspNetCore.Mvc;
using webMalefashion.Responsitory;

namespace webMalefashion.ViewComponents
{
    public class LoaiSizeMenuViewComponent: ViewComponent
    {
        private readonly ILoaiBrandResponsitory _loaiSize;
        public LoaiSizeMenuViewComponent(ILoaiBrandResponsitory loaiBrandResponsitory)
        {
            _loaiSize = loaiBrandResponsitory;
        }
        public IViewComponentResult Invoke()
        {
            var loaisize = _loaiSize.GetAllLoaiSize().OrderBy(x => x.SizeId);
            //


            return View(loaisize);

        }




    }
}
