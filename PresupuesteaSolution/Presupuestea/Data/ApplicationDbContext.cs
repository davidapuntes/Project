using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presupuestea.Data.Model;

namespace Presupuestea.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    
    }
}
