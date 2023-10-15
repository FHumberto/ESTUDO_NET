namespace UI;

partial class FrmMenu
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        BtnSetores = new Button();
        BtnProdutos = new Button();
        LblTitulo = new Label();
        label1 = new Label();
        SuspendLayout();
        // 
        // BtnSetores
        // 
        BtnSetores.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        BtnSetores.Location = new Point(86, 95);
        BtnSetores.Name = "BtnSetores";
        BtnSetores.Size = new Size(187, 40);
        BtnSetores.TabIndex = 0;
        BtnSetores.Text = "Setores";
        BtnSetores.UseVisualStyleBackColor = true;
        BtnSetores.Click += BtnSetores_Click;
        // 
        // BtnProdutos
        // 
        BtnProdutos.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        BtnProdutos.Location = new Point(86, 141);
        BtnProdutos.Name = "BtnProdutos";
        BtnProdutos.Size = new Size(187, 40);
        BtnProdutos.TabIndex = 1;
        BtnProdutos.Text = "Produtos";
        BtnProdutos.UseVisualStyleBackColor = true;
        BtnProdutos.Click += BtnProdutos_Click;
        // 
        // LblTitulo
        // 
        LblTitulo.AutoSize = true;
        LblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
        LblTitulo.Location = new Point(74, 26);
        LblTitulo.Name = "LblTitulo";
        LblTitulo.Size = new Size(225, 28);
        LblTitulo.TabIndex = 2;
        LblTitulo.Text = "Projeto de Estudo CRUD";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
        label1.Location = new Point(81, 54);
        label1.Name = "label1";
        label1.Size = new Size(208, 25);
        label1.TabIndex = 3;
        label1.Text = "com EntityFramework 8";
        // 
        // FrmMenu
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(364, 261);
        Controls.Add(label1);
        Controls.Add(LblTitulo);
        Controls.Add(BtnProdutos);
        Controls.Add(BtnSetores);
        Name = "FrmMenu";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Menu";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button BtnSetores;
    private Button BtnProdutos;
    private Label LblTitulo;
    private Label label1;
}
