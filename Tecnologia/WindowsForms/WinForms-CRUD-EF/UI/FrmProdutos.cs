using Data;

namespace UI;
public partial class FrmProdutos : Form
{
    public FrmProdutos()
    {
        InitializeComponent();
        using DataContext context = new();
        List<Domain.Models.Produtos> produtos = context.Produtos.ToList();
    }
}
