using Flunt.Validations;

namespace S12_PFC.Domain.Products;

public class Category : Entity
{
    public bool Active { get; set; }

    public Category(string name)
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name"); // informa o campo que não pode ser nulo e a propriedade que é obrigatória
        AddNotifications(contract);

        Name = name;
        Active = true;
    }
}
