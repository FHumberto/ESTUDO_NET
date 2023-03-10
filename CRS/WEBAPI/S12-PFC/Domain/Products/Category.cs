using Flunt.Validations;

namespace S12_PFC.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; private set; }

    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }

    // Método Responsável pela Validação
    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name") // informa o campo que não pode ser nulo e a propriedade que é obrigatória
            .IsGreaterOrEqualsThan(Name, 3, "Name") // indica que o campo tem que ser maior ou igual a 3 caracteres
            .IsNotNullOrEmpty(CreatedBy, "Name")
            .IsNotNullOrEmpty(EditedBy, "Name");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active, string editedBy)
    {
        Active = active;
        Name = name;
        EditedBy = editedBy;

        Validate();
    }
}