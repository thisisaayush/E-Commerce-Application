using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; } //Categories is Table name in database- SQL Server. 
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category {  CategoryId=1, Name="Action", DisplayOrder=1},
                new Category {  CategoryId=2, Name="SciFi", DisplayOrder=2},
                new Category {  CategoryId=3, Name="History", DisplayOrder=3},
                new Category {  CategoryId=4, Name="Nature", DisplayOrder=3}
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Innovator",
                    Author = "Walter Issacson",
                    Description = "Book about science and Technology",
                    ISBN = "UHD990-T001",
                    listPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 2,
                    Title = "The Beginning of Infinity",
                    Author = "David Deutsh",
                    Description = "Book about science and Technology",
                    ISBN = "UID890-Tf01",
                    listPrice = 50,
                    Price = 45,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 2,
                },
                 new Product
                 {
                     Id = 3,
                     Title = "Singularity Is Near",
                     Author = "Kurzweil",
                     Description = "Book about science and Technology",
                     ISBN = "U09G90-yF11",
                     listPrice = 25,
                     Price = 23,
                     Price50 = 22,
                     Price100 = 20,
                     CategoryId = 3,
                 },
                 new Product
                 {
                     Id = 4,
                     Title = "Homo Deus",
                     Author = "Nolan Harari",
                     Description = "Book about science and Technology",
                     ISBN = "P89G0-dh31",
                     listPrice = 25,
                     Price = 23,
                     Price50 = 22,
                     Price100 = 20,
                     CategoryId = 4,
                 }); ;
        }


    }
}
