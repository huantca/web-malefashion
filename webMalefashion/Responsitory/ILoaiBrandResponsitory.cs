using webMalefashion.Models;
namespace webMalefashion.Responsitory
{
    public interface ILoaiBrandResponsitory
    {
        Manufacturer Add(Manufacturer manufacturerId);
        Manufacturer Update(Manufacturer manufacturerId);
        Manufacturer Delete(Manufacturer manufacturerId);
        Manufacturer GetLoaiBrand(Manufacturer manufacturerId);
        IEnumerable<Manufacturer> GetAllLoaiBrand();

        Category Add(Category categoryId);
        Category Update(Category categoryId);
        Category Delete(Category categoryId);
        Category GetLoaiCategory(Category category);
        IEnumerable<Category> GetAllLoaiCategory();

        Option Add(Option optionId);
        Option Update(Option optionId);
        Option Delete(Option optionId);
        Option GetLoaiPrice(Option option);
        IEnumerable<Option> GetAllLoaiPrice();



    }
}
