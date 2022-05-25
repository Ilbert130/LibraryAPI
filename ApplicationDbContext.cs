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
        public DbSet<Libro> Libros{get; set;}
        public DbSet<Comentario> Comentarios {get; set;}
        public DbSet<AutorLibro> AutoresLibros {get; set;}

        //This mettod is for create the firstkey of AutorLibro
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutorLibro>().HasKey(al => new {al.AutorId, al.LibroId});
        }
    }
}