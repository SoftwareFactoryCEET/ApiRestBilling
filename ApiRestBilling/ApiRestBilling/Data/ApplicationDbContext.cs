using Microsoft.EntityFrameworkCore;

namespace ApiRestBilling.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
    }
}
