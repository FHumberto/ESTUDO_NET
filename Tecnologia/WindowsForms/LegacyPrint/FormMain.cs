using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegacyPrint
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void BtnAction_Click(object sender, EventArgs e)
        {
            string FirstName = TxtFirstName.Text;
            string LastName = TxtLastName.Text;

            if(FirstName == "" || LastName == "")
            {
                MessageBox.Show("Please enter a first and last name.");
            }
            else
            {
                MessageBox.Show($"{TxtFirstName.Text} {TxtLastName.Text}");
            }
        }
    }
}
