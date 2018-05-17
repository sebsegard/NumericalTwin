using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RevitTestControl.Fenetres
{
    public partial class WindowForm : Form
    {
        public WindowForm()
        {
            InitializeComponent();

            cmb_cfg_Windows.Items.Clear();

            foreach (WindowsConfig cfg in WindowsConfigMngt.AllWindows)
            {
                cmb_cfg_Windows.Items.Add(cfg);

               
            }
        }

        Timer _Timer;

        //MySqlConnection mServer;
        public void Connect()
        {
            //string connectionString = "SERVER=10.192.12.5;DATABASE=TEST;UID=ssegard;PASSWORD=cesi";

            //mServer = new MySqlConnection(connectionString);

            

            //try
            //{
            //    mServer.Open();
            //}
            //catch (MySqlException ex)
            //{
            //}



            
            //Refresh();

            _Timer = new Timer();
            _Timer.Interval = 1000;
            _Timer.Tick += _Timer_Tick;
            _Timer.Start();

        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            RefreshPanels();
        }

        void RefreshPanels()
        {
            //string requete = "SELECT * FROM FenetreEvents ORDER BY TimeStamp DESC LIMIT 10";
            //MySqlCommand cmd = new MySqlCommand(requete, mServer);
            ////Create a data reader and Execute the command
            //MySqlDataReader dataReader = cmd.ExecuteReader();


            //this.dataGridView1.DataSource = dataReader;
            //bool first = true;
            //while (dataReader.Read())
            //{
            //    if (!first)
            //        continue;

            //    ColorPanel(pan_HallPalier_F1, (sbyte)dataReader["HallPalier_F1"]);
            //    ColorPanel(pan_HallPalier_F2, (sbyte)dataReader["HallPalier_F2"]);
            //    ColorPanel(pan_HallPalier_F3, (sbyte)dataReader["HallPalier_F3"]);

            //    ColorPanel(pan_HallRdc_F1, (sbyte)dataReader["HallRdc_F1"]);
            //    ColorPanel(pan_HallRdc_F2, (sbyte)dataReader["HallRdc_F2"]);
            //    //ColorPanel(pan_HallRdc_F3, (bool)dataReader["pan_HallRdc_F3"]);
            //    ColorPanel(pan_Tesla_F1, (sbyte)dataReader["Tesla_F1"]);
            //    ColorPanel(pan_Tesla_F2, (sbyte)dataReader["Tesla_F2"]);
            //    ColorPanel(pan_Tesla_F3, (sbyte)dataReader["Tesla_F3"]);
            //    ColorPanel(pan_Tesla_F4, (sbyte)dataReader["Tesla_F4"]);

            //    ColorPanel(pan_Turing_F1, (sbyte)dataReader["Turing_F1"]);
            //    ColorPanel(pan_Turing_F2, (sbyte)dataReader["Turing_F2"]);
            //    ColorPanel(pan_Turing_F3, (sbyte)dataReader["Turing_F3"]);

            //    ColorPanel(pan_Nobel_F1, (sbyte)dataReader["Nobel_F1"]);
            //    ColorPanel(pan_Nobel_F2, (sbyte)dataReader["Nobel_F2"]);
            //    ColorPanel(pan_Nobel_F3, (sbyte)dataReader["Nobel_F3"]);

            //    ColorPanel(pan_Lumiere_F1, (sbyte)dataReader["Lumiere_F1"]);
            //    ColorPanel(pan_Lumiere_F2, (sbyte)dataReader["Lumiere_F2"]);
            //    ColorPanel(pan_Lumiere_F3, (sbyte)dataReader["Lumiere_F3"] );

            //    first = false;
            //}
            //dataReader.Close();


            ColorPanel(pan_HallPalier_F1, "HallPalier_F1");
            ColorPanel(pan_HallPalier_F2, "HallPalier_F2");
            ColorPanel(pan_HallPalier_F3, "HallPalier_F3");

            ColorPanel(pan_HallRdc_F1, "HallRdc_F1");
            ColorPanel(pan_HallRdc_F2, "HallRdc_F2");
            //ColorPanel(pan_HallRdc_F3, (bool)dataReader["pan_HallRdc_F3");
            ColorPanel(pan_Tesla_F1, "Tesla_F1");
            ColorPanel(pan_Tesla_F2, "Tesla_F2");
            ColorPanel(pan_Tesla_F3, "Tesla_F3");
            ColorPanel(pan_Tesla_F4, "Tesla_F4");

            ColorPanel(pan_Turing_F1, "Turing_F1");
            ColorPanel(pan_Turing_F2, "Turing_F2");
            ColorPanel(pan_Turing_F3, "Turing_F3");

            ColorPanel(pan_Nobel_F1, "Nobel_F1");
            ColorPanel(pan_Nobel_F2, "Nobel_F2");
            ColorPanel(pan_Nobel_F3, "Nobel_F3");

            ColorPanel(pan_Lumiere_F1, "Lumiere_F1");
            ColorPanel(pan_Lumiere_F2, "Lumiere_F2");
            ColorPanel(pan_Lumiere_F3, "Lumiere_F3");

        }



        void ColorPanel(Panel pPanel,string Name)
        {
            WindowsConfig GoodCfg=null;
            foreach (WindowsConfig cfg in WindowsConfigMngt.AllWindows)
                if (cfg.WindowsName == Name)
                {
                    GoodCfg = cfg;
                    break;
                }
            if (GoodCfg == null)
                return;


            if (GoodCfg.State)
                pPanel.BackColor = Color.LightGreen;
            else
                pPanel.BackColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshPanels();
        }

        private void WindowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mServer.Close();

            _Timer.Stop();
        }

        int _SelectedId;

        public void SetSelectElement(int pElementId)
        {
            this.txt_cfg_Id.Text = pElementId.ToString();
            _SelectedId = pElementId;
            this.cmb_cfg_Windows.Items.Clear();

            foreach(WindowsConfig cfg in WindowsConfigMngt.AllWindows)
            {
                cmb_cfg_Windows.Items.Add(cfg);

                if (cfg.WindowsRevitId == pElementId)
                    cmb_cfg_Windows.SelectedItem = cfg;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (cmb_cfg_Windows.SelectedItem == null)
                return;

            try
            {
                WindowsConfig cfg = (WindowsConfig)cmb_cfg_Windows.SelectedItem;
                cfg.WindowsRevitId = _SelectedId;
                WindowsConfigMngt.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
