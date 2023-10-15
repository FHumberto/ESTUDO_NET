using Data;

namespace UI;
public partial class FrmProdutos : Form
{
    public FrmProdutos()
    {
        InitializeComponent();
        using DataContext context = new();
        //DataGridProdutos.DataSource = (from produtos in context.Produtos select produtos).ToList(); // lista os dados

        //! JUNÇÃO DAS TABELAS E AGRUPAMENTO
        var lista = from produtos in context.Produtos
                    join setores in context.Setores
                    on produtos.IdSetor equals setores.IdSetor
                    into produtosGrupo
                    from setores in produtosGrupo.DefaultIfEmpty()
                    select new { setores, produtos };

        //! CONFIGURA A ESTRUTURA DA TABELA
        ConfigurarGrade();

        //! ALIMENTA A TABELA COM OS DADOS
        foreach (var item in lista)
        {
            DataGridProdutos.Rows.Add(item.produtos.Id, item.produtos.Descricao, item.produtos.Un, item.produtos.Unitario, item.produtos.IdSetor, item.setores.Descricao);
        }
    }

    private void ConfigurarGrade()
    {
        DataGridProdutos.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9);
        DataGridProdutos.DefaultCellStyle.Font = new Font("Arial", 9);
        DataGridProdutos.RowHeadersWidth = 25;

        DataGridProdutos.Columns.Add("id", "Id");
        DataGridProdutos.Columns["id"].Width = 80;
        DataGridProdutos.Columns["id"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        DataGridProdutos.Columns["id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        DataGridProdutos.Columns.Add("descricao", "Descrição");
        DataGridProdutos.Columns["descricao"].HeaderText = "Descrição";
        DataGridProdutos.Columns["descricao"].Width = 250;

        DataGridProdutos.Columns.Add("un", "Un");
        DataGridProdutos.Columns["un"].Width = 90;
        DataGridProdutos.Columns["un"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        DataGridProdutos.Columns["un"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        DataGridProdutos.Columns["un"].DefaultCellStyle.Format = "N2";

        DataGridProdutos.Columns.Add("unitario", "Unitário");
        DataGridProdutos.Columns["unitario"].Width = 90;
        DataGridProdutos.Columns["unitario"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        DataGridProdutos.Columns["unitario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        DataGridProdutos.Columns["unitario"].DefaultCellStyle.Format = "N2";

        DataGridProdutos.Columns.Add("idSetor", "Id Setor");
        DataGridProdutos.Columns["idSetor"].Width = 80;
        DataGridProdutos.Columns["idSetor"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        DataGridProdutos.Columns["idSetor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        DataGridProdutos.Columns.Add("descricaoSetor", "Descrição do Setor");
        DataGridProdutos.Columns["descricaoSetor"].Width = 200;
        DataGridProdutos.Columns["descricaoSetor"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        DataGridProdutos.Columns["descricaoSetor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }
}
