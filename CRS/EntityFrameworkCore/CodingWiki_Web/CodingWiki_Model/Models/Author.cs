﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models;
[Table("Authors")]
public class Author
{
    [Key]
    public int Author_Id { get; set; }
    [Required, MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Location { get; set; }
    [NotMapped]
    public string FullName
    {
        get
        {
            // também referido como propriedade computada
            return $"{FirstName} {LastName}";
        }
    }

    public List<Book> Books { get; set; }
}
