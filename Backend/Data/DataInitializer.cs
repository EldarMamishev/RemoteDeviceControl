using Repositories;

namespace Data
{
    public class DataInitializer : IDataInitializer
    {
        private readonly AppDbContext _context;

        public DataInitializer(AppDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();
        }
    }
}