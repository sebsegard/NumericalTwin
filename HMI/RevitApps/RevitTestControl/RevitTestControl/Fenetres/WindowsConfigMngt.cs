using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO;
using System.Threading;

namespace RevitTestControl.Fenetres
{
    public static class WindowsConfigMngt
    {
        private const string XmlConfigFileName = "RevitWinMngtConfig.xml";
        private static string XmlFileName
        {
            get
            {
                return "D:\\" + XmlConfigFileName;
            }
        }

        public static void Save()
        {
           
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<XmlLightMngt></XmlLightMngt>");

            foreach (WindowsConfig cfg  in AllWindows)
            {
                if (cfg.WindowsRevitId == WindowsConfig.UNKNOWN_WINDOWS_ID)
                    continue;

                XmlElement ele = doc.CreateElement("windows");

                ele.SetAttribute("Name", cfg.WindowsName);
                ele.SetAttribute("GUID", cfg.WindowsRevitId.ToString());
               
                doc.DocumentElement.AppendChild(ele);
            }
            doc.Save(XmlFileName);
        }

        public static void Load()
        {
            if (!File.Exists(XmlFileName))
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFileName);

            foreach (XmlNode nd in doc.DocumentElement)
            {
                string Name = nd.Attributes["Name"].Value;

                string GUID = nd.Attributes["GUID"].Value;
                
                foreach (WindowsConfig cfg in AllWindows)
                {
                    if (cfg.WindowsName == Name)
                        cfg.WindowsRevitId = Convert.ToInt32(GUID);
                }
            }
        }
    

        public static void Init()
        {
            if (Initialized)
                return;


            AllWindows.Clear();
            AllWindows.Add(new WindowsConfig("HallPalier_F1"));
            AllWindows.Add(new WindowsConfig("HallPalier_F2"));
            AllWindows.Add(new WindowsConfig("HallPalier_F3"));

            AllWindows.Add(new WindowsConfig("HallRdc_F1"));
            AllWindows.Add(new WindowsConfig("HallRdc_F2"));

            AllWindows.Add(new WindowsConfig("Tesla_F1"));
            AllWindows.Add(new WindowsConfig("Tesla_F2"));
            AllWindows.Add(new WindowsConfig("Tesla_F3"));
            AllWindows.Add(new WindowsConfig("Tesla_F4"));

            AllWindows.Add(new WindowsConfig("Turing_F1"));
            AllWindows.Add(new WindowsConfig("Turing_F2"));
            AllWindows.Add(new WindowsConfig("Turing_F3"));

            AllWindows.Add(new WindowsConfig("Lumiere_F1"));
            AllWindows.Add(new WindowsConfig("Lumiere_F2"));
            AllWindows.Add(new WindowsConfig("Lumiere_F3"));

            AllWindows.Add(new WindowsConfig("Nobel_F1"));
            AllWindows.Add(new WindowsConfig("Nobel_F2"));
            AllWindows.Add(new WindowsConfig("Nobel_F3"));
            Load();
            _WindowsWatcherThread = new Thread(new ThreadStart(ThrRefresh));
            _WindowsWatcherThread.Start();

            Initialized = true;
        }

        static Thread _WindowsWatcherThread = null;
        public static void Stop()
        {
            if (_WindowsWatcherThread != null)
                _WindowsWatcherThread.Abort();
        }

        public static void ThrRefresh()
        {
            while(true)
            {

            
                try
                {
                    Refresh();
                }
                catch(Exception ex)
                {
                    //do nothing
                }
                Thread.Sleep(1000);
            }
        }

        private static void Refresh()
        {
            bool HasChange = false;
            bool first = true;
            string connectionString = "SERVER=10.192.12.5;DATABASE=TEST;UID=ssegard;PASSWORD=cesi";
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM FenetreEvents ORDER BY TimeStamp DESC LIMIT 1";
                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (!first)
                            break;
                        first = true;

                        foreach (WindowsConfig cfg in AllWindows)
                        {
                            sbyte pValue = (sbyte)reader[cfg.WindowsName];

                            bool NewValue = (pValue == 0 ? true : false);

                            if (NewValue != cfg.State)
                            {
                                HasChange = true;
                                cfg.State = NewValue;
                                if (cfg.WindowsRevitId != WindowsConfig.UNKNOWN_WINDOWS_ID)
                                    InWindows.AddWindowsEvent(new Autodesk.Revit.DB.ElementId(cfg.WindowsRevitId), NewValue);

                            }
                        }
                    }
                }
            }

            //string requete = ";
            //MySqlCommand cmd = new MySqlCommand(requete, App.SqlServer);
            ////Create a data reader and Execute the command
            //MySqlDataReader dataReader = cmd.ExecuteReader();
            //bool HasChange = false;
            //bool first = true;
            //while (dataReader.Read())
            //{
            //    if (!first)
            //        break;
            //    first = true;

            //    foreach (WindowsConfig cfg in AllWindows)
            //    {
            //        sbyte pValue = (sbyte)dataReader[cfg.WindowsName];

            //        bool NewValue = (pValue == 0 ? true : false);

            //        if(NewValue != cfg.State)
            //        {
            //            HasChange = true;
            //            cfg.State = NewValue;
            //            if(cfg.WindowsRevitId!= WindowsConfig.UNKNOWN_WINDOWS_ID)
            //                InWindows.AddWindowsEvent(new Autodesk.Revit.DB.ElementId(cfg.WindowsRevitId), NewValue);
                        
            //        }
            //    }

            //}
            //dataReader.Close();
            //cmd.c
            if (HasChange)
                App.RaiseWindowsEvent();
        }

        static bool Initialized = false;
        static internal List<WindowsConfig> AllWindows = new List<WindowsConfig>();

      
    }

    
    public  class WindowsConfig
    {

        public const int UNKNOWN_WINDOWS_ID = -1;

        internal string WindowsName { get; private set; }

        internal int WindowsRevitId { get; set; }

        internal bool State { get; set; }

        public WindowsConfig(string pName)
        {
            WindowsName = pName;
            State = false;
            WindowsRevitId = UNKNOWN_WINDOWS_ID;
        }

        public override string ToString()
        {
            return WindowsName;
        }
    }
}
