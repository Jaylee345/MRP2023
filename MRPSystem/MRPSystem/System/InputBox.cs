using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRPSystem
{
    public partial class InputBox : Form
    {
        public static string value;//派工模式用-工令
        public InputBox()
        {
            InitializeComponent();
        }
        public InputBox(string val)
        {
            InitializeComponent();
            textBox1.Text = val;
            if (val != "kingrich")
            {
                label1.Visible = false;
                textBox1.ForeColor = Color.Red;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            value = textBox1.Text;
            DialogResult = DialogResult.OK;//這一定要設
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            value = "";
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
