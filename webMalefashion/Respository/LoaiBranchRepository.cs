using webMalefashion.Models;
namespace webMalefashion.Respository
{
    public class LoaiBranchRepository : ILoaiBranchRepository
    {
        private readonly MalefashionContext _context;
        public LoaiBranchRepository(MalefashionContext context)
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
        // Category
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

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<Manufacturer> GetAllLoaiBranch()
        {
            return _context.Manufacturers;
        }

        public Manufacturer GetLoaiBranch(Manufacturer manufacturerId)
        {
            return _context.Manufacturers.Find(manufacturerId);
        }

        public Category GetLoaiCategory(Category categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public Manufacturer Update(Manufacturer manufacturerId)
        {
            _context.Update(manufacturerId);
            _context.SaveChanges();
            return manufacturerId;
        }

        public Category Update(Category categoryId)
        {
            _context.Update(categoryId);
            _context.SaveChanges();
            return categoryId;
        }
    }
}
