namespace UI;

partial class FrmProdutos
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProdutos));
        toolStrip1 = new ToolStrip();
        ToolStripBtnAdicionar = new ToolStripButton();
        toolStripSeparator1 = new ToolStripSeparator();
        ToolStripBtnEditar = new ToolStripButton();
        toolStripSeparator2 = new ToolStripSeparator();
        ToolStripBtnVisualizar = new ToolStripButton();
        toolStripSeparator3 = new ToolStripSeparator();
        ToolStripBtnExcluir = new ToolStripButton();
        DataGridProdutos = new DataGridView();
        toolStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)DataGridProdutos).BeginInit();
        SuspendLayout();
        // 
        // toolStrip1
        // 
        toolStrip1.Items.AddRange(new ToolStripItem[] { ToolStripBtnAdicionar, toolStripSeparator1, ToolStripBtnEditar, toolStripSeparator2, ToolStripBtnVisualizar, toolStripSeparator3, ToolStripBtnExcluir });
        toolStrip1.Location = new Point(0, 0);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new Size(800, 25);
        toolStrip1.TabIndex = 0;
        toolStrip1.Text = "toolStrip1";
        // 
        // ToolStripBtnAdicionar
        // 
        ToolStripBtnAdicionar.Image = (Image)resources.GetObject("ToolStripBtnAdicionar.Image");
        ToolStripBtnAdicionar.ImageTransparentColor = Color.Magenta;
        ToolStripBtnAdicionar.Name = "ToolStripBtnAdicionar";
        ToolStripBtnAdicionar.Size = new Size(78, 22);
        ToolStripBtnAdicionar.Text = "Adicionar";
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(6, 25);
        // 
        // ToolStripBtnEditar
        // 
        ToolStripBtnEditar.Image = (Image)resources.GetObject("ToolStripBtnEditar.Image");
        ToolStripBtnEditar.ImageTransparentColor = Color.Magenta;
        ToolStripBtnEditar.Name = "ToolStripBtnEditar";
        ToolStripBtnEditar.Size = new Size(57, 22);
        ToolStripBtnEditar.Text = "Editar";
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(6, 25);
        // 
        // ToolStripBtnVisualizar
        // 
        ToolStripBtnVisualizar.Image = (Image)resources.GetObject("ToolStripBtnVisualizar.Image");
        ToolStripBtnVisualizar.ImageTransparentColor = Color.Magenta;
        ToolStripBtnVisualizar.Name = "ToolStripBtnVisualizar";
        ToolStripBtnVisualizar.Size = new Size(76, 22);
        ToolStripBtnVisualizar.Text = "Visualizar";
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        toolStripSeparator3.Size = new Size(6, 25);
        // 
        // ToolStripBtnExcluir
        // 
        ToolStripBtnExcluir.Image = (Image)resources.GetObject("ToolStripBtnExcluir.Image");
        ToolStripBtnExcluir.ImageTransparentColor = Color.Magenta;
        ToolStripBtnExcluir.Name = "ToolStripBtnExcluir";
        ToolStripBtnExcluir.Size = new Size(62, 22);
        ToolStripBtnExcluir.Text = "Excluir";
        // 
        // DataGridProdutos
        // 
        DataGridProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        DataGridProdutos.Dock = DockStyle.Fill;
        DataGridProdutos.Location = new Point(0, 25);
        DataGridProdutos.Name = "DataGridProdutos";
        DataGridProdutos.RowTemplate.Height = 25;
        DataGridProdutos.Size = new Size(800, 425);
        DataGridProdutos.TabIndex = 1;
        // 
        // FrmProdutos
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(DataGridProdutos);
        Controls.Add(toolStrip1);
        Name = "FrmProdutos";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "FrmProdutos";
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)DataGridProdutos).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ToolStrip toolStrip1;
    private ToolStripButton ToolStripBtnAdicionar;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton ToolStripBtnEditar;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton ToolStripBtnVisualizar;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton ToolStripBtnExcluir;
    private DataGridView DataGridProdutos;
}