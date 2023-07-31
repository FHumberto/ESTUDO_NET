using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersCatalog.Models.DTOs;

public class UserDto
{
    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
}
