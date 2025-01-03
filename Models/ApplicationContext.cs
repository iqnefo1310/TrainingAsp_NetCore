using Microsoft.EntityFrameworkCore;
using PERT_2.Models.DB;

namespace PERT_2.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options) : base(options)
        {
            
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
    }
}
