using webMalefashion.Models;
namespace webMalefashion.ViewModels
{
    public class HomeProductDetailViewModel
    {
        public Product product { get; set; }
        public List<Option> options { get; set; }
    }
}
