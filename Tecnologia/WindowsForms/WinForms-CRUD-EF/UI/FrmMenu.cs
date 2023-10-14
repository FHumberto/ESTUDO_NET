namespace UI;

public partial class FrmMenu : Form
{
    public FrmMenu()
    {
        InitializeComponent();
    }

    private void BtnProdutos_Click(object sender, EventArgs e)
    {
        FrmProdutos frmProdutos = new();
        _ = frmProdutos.ShowDialog();
    }

    private void BtnSetores_Click(object sender, EventArgs e)
    {
        FrmSetores frmSetores = new();
        _ = frmSetores.ShowDialog();
    }
}
