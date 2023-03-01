namespace S12_PFC.Endpoints.Categories;
// PADRÃO DE RESPOSTA
public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
}