using Flunt.Validations;

using S12_PFC.Domain.Products;

namespace S12_PFC.Domain.Order;

public class Order : Entity
{
    public string ClientId { get; private set; }
    public List<Product> Products { get; private set; }
    public decimal Total { get; private set; }
    public string DeliveryAddress { get; private set; }

    private Order() { }

    public Order(string clientId, string clientName, List<Product> products, string deliveryAddress)
    {
        ClientId = clientId;
        Products = products;
        DeliveryAddress = deliveryAddress;
        CreatedBy = clientName;
        EditedBy = clientName;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Total = 0;
        foreach (var item in products)
        {
            Total += item.Price;
        }

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Order>()
            .IsNotNull(ClientId, "Client")
            .IsNotNull(Products, "Products");
        AddNotifications(contract);
    }
}
