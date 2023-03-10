namespace S12_PFC.Endpoints.Products;

public record ProductRequest (string Name, Guid categoryId, string Description, bool HasStock, bool Active);