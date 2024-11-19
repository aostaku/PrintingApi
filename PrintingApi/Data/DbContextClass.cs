using Microsoft.EntityFrameworkCore;
using PrintingApi.Model;

namespace PrintingApi.Data
{
    public class DbContextClass : DbContext
    { 
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<InvoiceDetails> Invoices { get; set; }
    }

}
