namespace S12_PFC.Endpoints.Order;

// OS DADOS DO CLIENTE JÁ VAI ESTÁ NO TOKEN
public record OrderRequest(List<Guid> ProductIds, string DeliveryAddress);
