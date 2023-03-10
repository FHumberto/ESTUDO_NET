namespace S12_PFC.Endpoints.Products;

public record ProductRequest(string Name, Guid categoryId, string Description, bool HasStock, decimal Price, bool Active);