using DD.Library.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Library.Model
{
	public class LibraryDbContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Consts.ConnectionstringDb);
        }
        public LibraryDbContext()
        {
           // Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Wardrobe> Wardrobes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wardrobe>().HasData(
                new Wardrobe[]
                {
                new Wardrobe { Id=1,Name="Wardrobe1"},
                new Wardrobe { Id=2,Name="Wardrobe2"},
                new Wardrobe { Id=3,Name="Wardrobe3"}
                });
            modelBuilder.Entity<Author>().HasData(
    new Author[]
    {
                new Author { Id=1,FirstName="Тест",LastName="Тестович", Patronymic="Тестер"},
                new Author { Id=2,FirstName="Тест2",LastName="Тестович2", Patronymic="Тестер2"},
                new Author { Id=3,FirstName="Тест3",LastName="Тестович3"}
    }); ;
            modelBuilder.Entity<Book>().HasData(
    new Book[]
    {
                new Book { Id=1,Name="Книга 1", WardrobeId=1,AuthorId=1},
                new Book { Id=2, Name="Книга 2",WardrobeId=2,AuthorId=2},
                new Book { Id=3, Name="Книга 3",WardrobeId=2,AuthorId=3},
                new Book { Id=4, Name="Книга 4",WardrobeId=2,AuthorId=1}
    });
        }
    }
}
