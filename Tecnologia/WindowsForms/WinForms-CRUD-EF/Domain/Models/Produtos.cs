namespace Domain.Models;
public class Produtos
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public string? Un { get; set; }
    public decimal Unitario { get; set; }
    public int IdSetor { get; set; }
}
