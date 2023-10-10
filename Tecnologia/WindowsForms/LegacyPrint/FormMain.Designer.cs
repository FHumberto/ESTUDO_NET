namespace LegacyPrint
{
    partial class FormMain
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
            this.BtnAction = new System.Windows.Forms.Button();
            this.LbFirstName = new System.Windows.Forms.Label();
            this.TxtFirstName = new System.Windows.Forms.TextBox();
            this.TxtLastName = new System.Windows.Forms.TextBox();
            this.LbLastName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnAction
            // 
            this.BtnAction.Location = new System.Drawing.Point(76, 119);
            this.BtnAction.Name = "BtnAction";
            this.BtnAction.Size = new System.Drawing.Size(164, 33);
            this.BtnAction.TabIndex = 0;
            this.BtnAction.Text = "Ação";
            this.BtnAction.UseVisualStyleBackColor = true;
            this.BtnAction.Click += new System.EventHandler(this.BtnAction_Click);
            // 
            // LbFirstName
            // 
            this.LbFirstName.AutoSize = true;
            this.LbFirstName.Location = new System.Drawing.Point(99, 55);
            this.LbFirstName.Name = "LbFirstName";
            this.LbFirstName.Size = new System.Drawing.Size(35, 13);
            this.LbFirstName.TabIndex = 1;
            this.LbFirstName.Text = "Nome";
            // 
            // TxtFirstName
            // 
            this.TxtFirstName.Location = new System.Drawing.Point(140, 52);
            this.TxtFirstName.Name = "TxtFirstName";
            this.TxtFirstName.Size = new System.Drawing.Size(100, 20);
            this.TxtFirstName.TabIndex = 2;
            // 
            // TxtLastName
            // 
            this.TxtLastName.Location = new System.Drawing.Point(140, 78);
            this.TxtLastName.Name = "TxtLastName";
            this.TxtLastName.Size = new System.Drawing.Size(100, 20);
            this.TxtLastName.TabIndex = 4;
            // 
            // LbLastName
            // 
            this.LbLastName.AutoSize = true;
            this.LbLastName.Location = new System.Drawing.Point(73, 81);
            this.LbLastName.Name = "LbLastName";
            this.LbLastName.Size = new System.Drawing.Size(61, 13);
            this.LbLastName.TabIndex = 3;
            this.LbLastName.Text = "Sobrenome";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 199);
            this.Controls.Add(this.TxtLastName);
            this.Controls.Add(this.LbLastName);
            this.Controls.Add(this.TxtFirstName);
            this.Controls.Add(this.LbFirstName);
            this.Controls.Add(this.BtnAction);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LegacyPrint";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAction;
        private System.Windows.Forms.Label LbFirstName;
        private System.Windows.Forms.TextBox TxtFirstName;
        private System.Windows.Forms.TextBox TxtLastName;
        private System.Windows.Forms.Label LbLastName;
    }
}

