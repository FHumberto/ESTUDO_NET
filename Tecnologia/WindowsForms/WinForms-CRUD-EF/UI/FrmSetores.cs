using Data;

namespace UI;
public partial class FrmSetores : Form
{
    public FrmSetores()
    {
        InitializeComponent();
        using DataContext context = new();
        DataGridSetores.DataSource = (from setores in context.Setores select setores).ToList(); // lista os dados
        ConfigurarGrade();
    }

    private void ConfigurarGrade()
    {
        DataGridSetores.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9);
        DataGridSetores.DefaultCellStyle.Font = new Font("Arial", 9);
        DataGridSetores.RowHeadersWidth = 25;

        // coluna idSetor
        DataGridSetores.Columns["idSetor"].HeaderText = "Id";
        DataGridSetores.Columns["idSetor"].Width = 80;
        DataGridSetores.Columns["idSetor"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        DataGridSetores.Columns["idSetor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        // coluna descrição
        DataGridSetores.Columns["descricao"].HeaderText = "Descrição";
        DataGridSetores.Columns["descricao"].Width = 250;
    }
}
