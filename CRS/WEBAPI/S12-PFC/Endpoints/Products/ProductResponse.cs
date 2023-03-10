namespace S12_PFC.Endpoints.Products;

public record ProductResponse(string Name, string categoryName, string Description, bool HasStock, bool Active);