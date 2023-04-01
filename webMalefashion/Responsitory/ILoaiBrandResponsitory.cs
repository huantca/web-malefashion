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
        Manufacturer GetLoaiCategory(Category category);
        IEnumerable<Category> GetAllLoaiCategory();

    }
}
