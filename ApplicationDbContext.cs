using Microsoft.EntityFrameworkCore;
using PruebeVC.Models;

namespace PruebeVC
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Autor> Autores { get; set; }
    }
}