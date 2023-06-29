//// See https://aka.ms/new-console-template for more information
//using CodingWiki_DataAcess.Data;
//using CodingWiki_Model.Models;
Console.WriteLine("hello");

//! helper method que checa se migrações foram aplicadas e se existe banco
//using (ApplicationDbContext context = new())
//{
//    context.Database.EnsureCreated();
//    if (context.Database.GetPendingMigrations().Count() > 0)
//    {
//        context.Database.Migrate();
//    }
//}

//AddBook();
//GetBook();
//GetBookById(5);
//GetAllBooks();

//async void DeleteBook()
//{
//    using var context = new ApplicationDbContext();
//    var book = await context.Books.FindAsync(1003);
//    context.Books.Remove(book);
//    await context.SaveChangesAsync();
//}

//void update()
//{
//    try
//    {
//        using ApplicationDbContext context = new();
//        var book = context.Books.Find(1);
//        Console.WriteLine($"{book.Title} - {book.ISBN}");
//        book.ISBN = "777";
//        context.SaveChanges();
//    }
//    catch (Exception e)
//    {

//    }
//}

//void GetBook()
//{
//    try
//    {
//        using ApplicationDbContext context = new();
//        var books = context.Books.ToList().Skip(0).Take(2);
//        foreach (Book? book in books)
//        {
//            Console.WriteLine($"{book.Title} - {book.ISBN}");
//        }
//    }
//    catch (Exception e)
//    {

//    }
//}

//void GetBookById(int id)
//{
//    using ApplicationDbContext context = new();
//    var book = context.Books.Where(b => b.Publisher_Id == id).FirstOrDefault();
//    Console.WriteLine($"{book.Title} - {book.ISBN}");
//}

//void GetAllBooks()
//{
//    using ApplicationDbContext context = new();
//    List<Book> books = context.Books.ToList();
//    foreach (Book? book in books)
//    {
//        Console.WriteLine($"{book.Title} - {book.ISBN}");
//    }
//}

//void AddBook()
//{
//    Book book = new()
//    {
//        Title = "New EF Core Book",
//        ISBN = "1231231212",
//        Price = 10.93m,
//        Publisher_Id = 1
//    };
//    using ApplicationDbContext context = new ApplicationDbContext();
//    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Book> books = context.Books.Add(book);
//    context.SaveChanges();
//}