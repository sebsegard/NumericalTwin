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
    public partial class Clim : Form
    {
        static int SmartBuildingRoomIndex = 5;

        public Clim()
        {
            InitializeComponent();
            this.cmb_Room.SelectedIndex = SmartBuildingRoomIndex-1;
        }

        int _CurrentRoom;

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeRoom();
        }

        private void btn_Envoi_Click(object sender, EventArgs e)
        {
            try
            {
                string requete = "INSERT INTO BacnetConsigne VALUES ('',NOW(),'{0}','{1}','{2}','{3}','{4}','{5}')";
                string prohibit = this.chk_UserAction.Checked ? "Permit" : "Prohibit";
                requete = string.Format(requete, _CurrentRoom, this.list_Etat.SelectedItem, this.list_Mode.SelectedItem, this.list_Fan.SelectedItem, prohibit, Convert.ToDouble(this.txt_SetTemp.Text));
                string connectionString = "SERVER=10.192.12.5;DATABASE=TEST;UID=ssegard;PASSWORD=cesi";
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = requete;
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show(this, "La demande est prise en compte et sera aappliquée dans quelques instants", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void cmb_Room_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CurrentRoom = this.cmb_Room.SelectedIndex + 1;
            ChangeRoom();
        }

        private void ChangeRoom()
        {
            if(_CurrentRoom == SmartBuildingRoomIndex)
            {
                this.txt_Error.Text = "NA";
                this.txt_Error.ForeColor = SystemColors.WindowText;
                this.txt_Etat.Text = "NA";
                this.txt_Etat.ForeColor = SystemColors.WindowText;
                this.txt_Fan.Text = "NA";
                this.txt_Mode.Text = "NA";
                this.txt_RoomTemp.Text = "NA";
                this.txt_SetTemp.Text = "22";
                this.list_Etat.SelectedIndex = -1;
                this.list_Fan.SelectedIndex = -1;
                this.list_Mode.SelectedIndex = -1;
            }
            else
            {
                string connectionString = "SERVER=10.192.12.5;DATABASE=TEST;UID=ssegard;PASSWORD=cesi";
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM BacnetValues WHERE IdRoom='"+ _CurrentRoom + "' ORDER BY TimeStamp DESC LIMIT 1";
                    using (var reader = cmd.ExecuteReader())
                    {
                        bool first = true;
                        while (reader.Read())
                        {
                            if (!first)
                                return;
                            first = false;

                            //get data
                            sbyte OnOffSetup = (sbyte)reader["OnOffSetup"];
                            sbyte OnOffState = (sbyte)reader["OnOffState"];
                            sbyte ErrorCode = (sbyte)reader["ErrorCode"];
                            sbyte OperationalModeSetup = (sbyte)reader["OperationalModeSetup"];
                            sbyte OperationalModeState = (sbyte)reader["OperationalModeState"];
                            float RoomTemp = (float)reader["RoomTemp"];
                            float SetTemp = (float)reader["SetTemp"];
                            sbyte ProhibitionOnOff = (sbyte)reader["ProhibitionOnOff"];
                            sbyte FanState = (sbyte)reader["FanState"];
                            sbyte FanSetup = (sbyte)reader["FanSetup"];

                            //transform & display data

                            this.list_Etat.SelectedIndex = OnOffSetup;
                            this.txt_Etat.Text = this.list_Etat.Items[OnOffState].ToString();

                            string[] TabErrors = { "Normal", "Other Erros", "Refrigeration system fault", "Water ysstem error", "air system error", "Electronic system error", "sensor fault", "communication error", "system error" };
                            this.txt_Error.Text = TabErrors[ErrorCode-1];

                            this.list_Mode.SelectedIndex = OperationalModeSetup-1;
                            this.txt_Mode.Text = this.list_Mode.Items[OperationalModeState - 1].ToString();

                            this.txt_RoomTemp.Text = RoomTemp.ToString();
                            this.txt_SetTemp.Text = SetTemp.ToString();

                            this.list_Fan.SelectedIndex = FanSetup-1;
                            this.txt_Fan.Text = this.list_Fan.Items[FanState - 1].ToString();

                            if (ProhibitionOnOff == 0)
                                this.chk_UserAction.Checked = true;
                            else
                                this.chk_UserAction.Checked = false;
                        }
                    }
                }

            }
        }

       
    }
}
