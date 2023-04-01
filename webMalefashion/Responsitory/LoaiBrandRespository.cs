using webMalefashion.Models;
namespace webMalefashion.Responsitory
{
    public class LoaiBrandRespository : ILoaiBrandResponsitory
    {
        private readonly MalefashionContext _context;
        public LoaiBrandRespository(MalefashionContext context)
        {
            _context = context;
        }

        public Manufacturer Add(Manufacturer manufacturerId)
        {
            _context.Manufacturers.Add(manufacturerId);
            _context.SaveChanges();
            return manufacturerId;
        }

        public Manufacturer Delete(Manufacturer manufacturerId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Manufacturer> GetAllLoaiBrand()
        {
            return _context.Manufacturers;
        }

        public Manufacturer GetLoaiBrand(Manufacturer manufacturerId)
        {
            return _context.Manufacturers.Find(manufacturerId);
        }

        public Manufacturer Update(Manufacturer manufacturerId)
        {
            _context.Update(manufacturerId);
            _context.SaveChanges();
            return manufacturerId;
        }

        //category
        public Category Add(Category categoryId)
        {
            _context.Categories.Add(categoryId);
            _context.SaveChanges();
            return categoryId;
        }

        public Category Delete(Category categoryId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Category> GetAllLoaiCategory()
        {
            return _context.Categories;
        }

        public Category GetLoaiCategory(Category categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public Category Update(Category categoryId)
        {
            _context.Update(categoryId);
            _context.SaveChanges();
            return categoryId;
        }

        Manufacturer ILoaiBrandResponsitory.GetLoaiCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
