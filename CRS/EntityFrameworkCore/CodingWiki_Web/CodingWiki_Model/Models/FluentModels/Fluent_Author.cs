using CodingWiki_Model.Models.FluentModels;

namespace CodingWiki_Model.Models;

public class Fluent_Author
{
    public int Author_Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Location { get; set; }
    public string FullName
    {
        get
        {
            // também referido como propriedade computada
            return $"{FirstName} {LastName}";
        }
    }

    public List<Fluent_BookAuthorMap> BookAuthorMap { get; set; }
}
