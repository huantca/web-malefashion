

using webMalefashion.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using webMalefashion.Responsitory;
using System.Reflection.Metadata.Ecma335;

namespace webMalefashion.ViewComponents
{
    public class LoaiBrandMenuViewComponent : ViewComponent
    {
        private readonly ILoaiBrandResponsitory _loaiBrand;

        public LoaiBrandMenuViewComponent(ILoaiBrandResponsitory loaiBrandResponsitory)
        {
            _loaiBrand = loaiBrandResponsitory;
        }
        public IViewComponentResult Invoke()
        {
            var loaibrand = _loaiBrand.GetAllLoaiBrand().OrderBy(x => x.Id);
            //categoty
            

            return View(loaibrand);
      
        }
       

    
    

    }
    }
