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

namespace RevitTestControl.CVC
{
    public partial class CVCForm : Form
    {
        public CVCForm()
        {
            InitializeComponent();
        }

        private void CVCForm_Load(object sender, EventArgs e)
        {
            bool first = true;
            string connectionString = "SERVER=10.192.12.5;DATABASE=TEST;UID=ssegard;PASSWORD=cesi";
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM VmcMinutes ORDER BY TimeStamp DESC LIMIT 1";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!first)
                            return;
                        first = true;
                        string TimeStamp = ((DateTime)reader["TimeStamp"]).ToString("dd-MM HH") + "h";
                        this.txt_T_Ext.Text = reader["T_Ext"].ToString() + " °C";
                        this.txt_T_Reprise.Text = reader["T_Reprise"].ToString() + " °C";
                        this.txt_T_Soufflage.Text = reader["T_Souflage"].ToString() + " °C";

                        this.txt_P_Reprise.Text = reader["P_Reprise"].ToString() + " Pa";
                        this.txt_P_Soufflage.Text = reader["P_Soufflage"].ToString() + " Pa";

                        this.txt_Fan_Reprise.Text = reader["Fan_Reprise"].ToString() + " %";
                        this.txt_Fan_Soufflage.Text = reader["Fan_Soufflage"].ToString() + " %";
                    }
                }
            }
        }
    }
}
