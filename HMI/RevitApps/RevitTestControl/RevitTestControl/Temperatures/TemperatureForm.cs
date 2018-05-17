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
using System.Windows.Forms.DataVisualization.Charting;

namespace RevitTestControl.Temperatures
{
    public partial class TemperatureForm : Form
    {
        public TemperatureForm()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dddd dd-MM-yyyy HH:mm";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dddd dd-MM-yyyy HH:mm";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //create curve
            this.chart1.Series.Clear();


            Series sLumiere = this.chart1.Series.Add("Lumiere");
            sLumiere.ChartType = SeriesChartType.Spline;
            sLumiere.BorderWidth = 5;
            sLumiere.XValueType = ChartValueType.Time;

            Series sTesla = this.chart1.Series.Add("Tesla");
            sTesla.ChartType = SeriesChartType.Spline;
            sTesla.BorderWidth = 5;
            sTesla.XValueType = ChartValueType.Time;

            Series sTuring = this.chart1.Series.Add("Turing");
            sTuring.ChartType = SeriesChartType.Spline;
            sTuring.BorderWidth = 5;
            sTuring.XValueType = ChartValueType.Time;

            Series sNobel = this.chart1.Series.Add("Nobel");
            sNobel.ChartType = SeriesChartType.Spline;
            sNobel.BorderWidth = 5;
            sNobel.XValueType = ChartValueType.Time;

            Series sExt = this.chart1.Series.Add("Exterieur");
            sExt.ChartType = SeriesChartType.Spline;
            sExt.XValueType = ChartValueType.Time;
            sExt.BorderWidth = 5;
            string dtMin = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:MM");
            string dtMax = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:MM");
            string oquery = "SELECT * FROM TempHour WHERE TimeStamp BETWEEN '{0}' AND '{1}'";
            string query = string.Format(oquery, dtMin, dtMax);

            /*   MySqlCommand cmd = new MySqlCommand(query, mServer);
               //Create a data reader and Execute the command
               MySqlDataReader dataReader = cmd.ExecuteReader();*/


            string connectionString = "SERVER=10.192.12.5;DATABASE=TEST;UID=ssegard;PASSWORD=cesi";
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = query;
                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string TimeStamp = ((DateTime)reader["TimeStamp"]).ToString("dd-MM HH") + "h";
                        sNobel.Points.AddXY(TimeStamp, reader["tempNobel"]);
                        sTuring.Points.AddXY(TimeStamp, reader["tempTuring"]);
                        sLumiere.Points.AddXY(TimeStamp, reader["tempLumiere"]);
                        sTesla.Points.AddXY(TimeStamp, reader["tempTesla"]);
                        sExt.Points.AddXY(TimeStamp, reader["tempExt"]);
                    }
                }
            }
           

        }

        //MySqlConnection mServer;


        public  void Connect()
        {

            string connectionString = "SERVER=10.192.12.5;DATABASE=TEST;UID=ssegard;PASSWORD=cesi";
            DateTime dtMin = DateTime.Now, dtMax = DateTime.Now;

            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT MIN(TimeStamp) as TimeMin, MAX(TimeStamp) as TimeMax FROM TempHour";
                using (var reader = cmd.ExecuteReader())
                {
                   
                    while (reader.Read())
                    {                   
                           dtMin = (DateTime)reader["TimeMin"];
                            dtMax = (DateTime)reader["TimeMax"];
                    }
                }
            }

            //string query = "SELECT MIN(TimeStamp) as TimeMin, MAX(TimeStamp) as TimeMax FROM TempHour";
            ////Create Command
            //MySqlCommand cmd = new MySqlCommand(query, App.SqlServer);
            ////Create a data reader and Execute the command
            //MySqlDataReader dataReader = cmd.ExecuteReader();

           
           //dataReader.Close();
            this.dateTimePicker1.MinDate = dtMin;
            this.dateTimePicker1.MaxDate = dtMax;

            this.dateTimePicker2.MinDate = dtMin;
            this.dateTimePicker2.MaxDate = dtMax;

            this.dateTimePicker2.Value = this.dateTimePicker2.MaxDate;
            this.dateTimePicker1.Value = this.dateTimePicker2.MaxDate.AddDays(-1);

            button2_Click(null,null);
        }
    }
       
}
