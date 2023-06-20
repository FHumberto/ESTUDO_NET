// See https://aka.ms/new-console-template for more information
using CodingWiki_DataAcess.Data;
using CodingWiki_Model.Models;

Console.WriteLine("Hello, World!");

//! helper method que checa se migrações foram aplicadas e se existe banco
//using (ApplicationDbContext context = new())
//{
//    context.Database.EnsureCreated();
//    if (context.Database.GetPendingMigrations().Count() > 0)
//    {
//        context.Database.Migrate();
//    }
//}

AddBook();
GetAllBooks();

void GetAllBooks()
{
    using ApplicationDbContext context = new ApplicationDbContext();
    List<Book> books = context.Books.ToList();
    foreach (Book? book in books)
    {
        Console.WriteLine($"{book.Title} - {book.ISBN}");
    }
}

void AddBook()
{
    Book book = new()
    {
        Title = "New EF Core Book",
        ISBN = "1231231212",
        Price = 10.93m,
        Publisher_Id = 1
    };
    using ApplicationDbContext context = new ApplicationDbContext();
    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Book> books = context.Books.Add(book);
    context.SaveChanges();
}