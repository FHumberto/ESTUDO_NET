﻿using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAcess.Data;
internal class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categoiries { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookDetail> BookDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CodingWiki;TrustServerCertificate=True;Trusted_Connection=True;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(10, 5); // seta a precisão da variável

        modelBuilder.Entity<Book>().HasData
        (
            new Book { BookId = 1, Title = "Spider without Duty", ISBN = "123B12", Price = 10.99m },
            new Book { BookId = 2, Title = "Fortune of time", ISBN = "12123B12", Price = 11.99m }
        );

        var bookList = new Book[]
        {
                    new Book { BookId = 3, Title = "Fake Sunday", ISBN = "77652", Price = 20.99m },
                    new Book { BookId = 4, Title = "Cookie Jar", ISBN = "CC12B12", Price = 25.99m },
                    new Book { BookId = 5, Title = "Cloudy Forest", ISBN = "90392B33", Price = 40.99m }
        };

        modelBuilder.Entity<Book>().HasData(bookList);
    }
}
