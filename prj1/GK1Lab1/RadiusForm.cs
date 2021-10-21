using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphicsLab1
{
    public partial class RadiusForm : Form
    {
        public double endValue;
        public RadiusForm(double initialValue)
        {
            InitializeComponent();

            endValue = initialValue;
            value.Text = initialValue.ToString();
        }

        private void value_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(value.Text, out endValue))
                this.Close();
            else
                MessageBox.Show("Please enter a valid double value or cancel!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            endValue = -1;
            this.Close();
        }

        private void RadiusForm_Load(object sender, EventArgs e)
        {

        }
    }
}
