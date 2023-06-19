namespace CodingWiki_Model.Models.FluentModels;
public class Fluent_BookAuthorMap
{
    public int Book_Id { get; set; }
    public int Author_Id { get; set; }
    public Fluent_Book Book { get; set; }
    public Fluent_Author Author { get; set; }
}
