using Flunt.Notifications;
using Flunt.Validations;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System.ComponentModel.DataAnnotations;

namespace TDLS.Models;

public class TodoItem : Notifiable<Notification>
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Tittle { get; set; }
    [MaxLength(250)]
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime EditedOn { get; set; }

    public TodoItem(string tittle, string description)
    {
        Tittle = tittle;
        Description = description;
        IsCompleted = false;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }

    public void EditInfo(string tittle, string description, bool isCompleted)
    {
        Tittle = tittle;
        Description = description;
        IsCompleted = isCompleted;
        EditedOn = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<TodoItem>()
            .IsNotNullOrEmpty(Tittle, "Tittle")
            .IsGreaterOrEqualsThan(Tittle, 3, "Tittle")
            .IsNotNullOrEmpty(Description, "Description");
        AddNotifications(contract);
    }
}
