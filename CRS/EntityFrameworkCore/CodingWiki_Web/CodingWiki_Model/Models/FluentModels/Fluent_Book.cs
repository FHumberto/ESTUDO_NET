using CodingWiki_Model.Models.FluentModels;

namespace CodingWiki_Model.Models;
public class Fluent_Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public decimal Price { get; set; }
    public string PriceRange { get; set; }
    public Fluent_BookDetail BookDetail { get; set; }
    public int Publisher_Id { get; set; }
    public Fluent_Publisher Publisher { get; set; }

    public List<Fluent_BookAuthorMap> BookAuthorMap { get; set; }
}
