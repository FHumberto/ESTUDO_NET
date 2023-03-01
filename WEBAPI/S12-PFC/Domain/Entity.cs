using Flunt.Notifications;

namespace S12_PFC.Domain;

public abstract class Entity : Notifiable<Notification>
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string EditedBy { get; set; }
    public DateTime EditedOn { get; set; }
}
