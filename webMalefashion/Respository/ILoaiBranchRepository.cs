using webMalefashion.Models;
namespace webMalefashion.Respository


{
    public interface ILoaiBranchRepository
    {
        Manufacturer Add(Manufacturer manufacturerId);

        Manufacturer  Update(Manufacturer manufacturerId);
        Manufacturer Delete(Manufacturer manufacturerId);
        Manufacturer GetLoaiBranch(Manufacturer manufacturerId);
       
        IEnumerable<Manufacturer> GetAllLoaiBranch();
        // Category
        Category Add(Category categoryId);
        Category Update(Category categoryId);
        Category Delete(Category categoryId);
        Category GetLoaiCategory(Category categoryId);
        IEnumerable<Category> GetAllCategories();


    }
}
