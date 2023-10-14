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
        button2 = new Button();
        button3 = new Button();
        SuspendLayout();
        // 
        // BtnSetores
        // 
        BtnSetores.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        BtnSetores.Location = new Point(200, 100);
        BtnSetores.Name = "BtnSetores";
        BtnSetores.Size = new Size(80, 40);
        BtnSetores.TabIndex = 0;
        BtnSetores.Text = "Setores";
        BtnSetores.UseVisualStyleBackColor = true;
        BtnSetores.Click += BtnSetores_Click;
        // 
        // BtnProdutos
        // 
        BtnProdutos.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        BtnProdutos.Location = new Point(200, 146);
        BtnProdutos.Name = "BtnProdutos";
        BtnProdutos.Size = new Size(80, 40);
        BtnProdutos.TabIndex = 1;
        BtnProdutos.Text = "Produtos";
        BtnProdutos.UseVisualStyleBackColor = true;
        BtnProdutos.Click += BtnProdutos_Click;
        // 
        // button2
        // 
        button2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        button2.Location = new Point(200, 192);
        button2.Name = "button2";
        button2.Size = new Size(80, 40);
        button2.TabIndex = 2;
        button2.Text = "Setores";
        button2.UseVisualStyleBackColor = true;
        // 
        // button3
        // 
        button3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        button3.Location = new Point(200, 238);
        button3.Name = "button3";
        button3.Size = new Size(80, 40);
        button3.TabIndex = 3;
        button3.Text = "Setores";
        button3.UseVisualStyleBackColor = true;
        // 
        // FrmMenu
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(button3);
        Controls.Add(button2);
        Controls.Add(BtnProdutos);
        Controls.Add(BtnSetores);
        Name = "FrmMenu";
        Text = "Menu";
        ResumeLayout(false);
    }

    #endregion

    private Button BtnSetores;
    private Button BtnProdutos;
    private Button button2;
    private Button button3;
}
