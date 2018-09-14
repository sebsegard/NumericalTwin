using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RevitTestControl.LightControl.ConfigurationForm frm = new RevitTestControl.LightControl.ConfigurationForm();
            frm.LoadTree();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RevitTestControl.LightControl.LightEnergyForm frm = new RevitTestControl.LightControl.LightEnergyForm();
            frm.Connect();
            frm.Show();
        }

        private void btn_Vmc_Click(object sender, EventArgs e)
        {
            RevitTestControl.CVC.CVCForm frm = new RevitTestControl.CVC.CVCForm();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RevitTestControl.CVC.Clim frm = new RevitTestControl.CVC.Clim();
            frm.Show();
        }
    }
}
