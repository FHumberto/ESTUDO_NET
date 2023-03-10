namespace S12_PFC.Endpoints.Products;

public record ProductResponse(Guid Id, string Name, string categoryName, string Description, bool HasStock, decimal Price, bool Active);