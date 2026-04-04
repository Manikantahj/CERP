using Microsoft.EntityFrameworkCore;

namespace CERP.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected DataContext()
        {
        }

    }
}
