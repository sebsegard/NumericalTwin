using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Threading;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;

namespace RevitTestControl
{
    public class AppsAvailability
  : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(
          UIApplication a,
          CategorySet b)
        {
            return true;
        }
    }

    public class App : IExternalApplication
    {
        //static PushButtonData _ButtonToChange;
        static PushButton _ButtonToChange;
        static SynchronizationContext _CurrentContect;
        static ExternalEvent _SelectionChangedEvent;
        static ExternalEvent _PeriodicEvent;
        static ExternalEvent _WindowsEvent;



        const bool RaiseOnSelectionEvent = false;
        const bool RaisePeriodicEvent = false;

        // define a method that will create our tab and button
        public static void AddRibbonPanel(UIControlledApplication application)
        {
            RevitSelectionChangedEvent evtSelection = new RevitSelectionChangedEvent();
            _SelectionChangedEvent = ExternalEvent.Create(evtSelection);

            RevitPeriodicEvent evtPeriodic = new RevitPeriodicEvent();
            _PeriodicEvent = ExternalEvent.Create(evtPeriodic);

            RevitWindowsEvent evtWindowPeriodic = new RevitWindowsEvent();
            _WindowsEvent = ExternalEvent.Create(evtWindowPeriodic);

            // Create a custom ribbon tab
            String tabName = "SmartBuilding";
            application.CreateRibbonTab(tabName);

           





            // Get dll assembly path
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            string thisAssemblyDir = System.IO.Directory.GetParent(thisAssemblyPath).FullName;

            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Connection");
            PushButtonData bData = new PushButtonData(
             "Connection",
             "Connection",
             thisAssemblyPath,
             "RevitTestControl.Connection.Connection");
            bData.AvailabilityClassName = "RevitTestControl.AppsAvailability";
            PushButton Button = ribbonPanel.AddItem(bData) as PushButton;
            Button.ToolTip = "Connect to SmartBuilding";
            BitmapImage pbImage = new BitmapImage(new Uri(thisAssemblyDir + "/Resources/Connect.ico"));
            Button.LargeImage = pbImage;
            Button.AvailabilityClassName = "RevitTestControl.AppsAvailability";

            //Add a new Ribbon Panel for Ligh control
            ribbonPanel = application.CreateRibbonPanel(tabName, "Light Control");

            
            bData = new PushButtonData(
              "SwitchOn",
              "Switch On",
              thisAssemblyPath,
              "RevitTestControl.LightControl.SwitchOn");
            bData.AvailabilityClassName = "RevitTestControl.AppsAvailability";
            Button = ribbonPanel.AddItem(bData) as PushButton;
            Button.ToolTip = "Switch On 100% selected element";
           
            pbImage = new BitmapImage(new Uri(thisAssemblyDir + "/Resources/LightOn.ico"));
            Button.LargeImage = pbImage;
          


            bData = new PushButtonData(
             "SwitchOff",
             "Switch Off",
             thisAssemblyPath,
             "RevitTestControl.LightControl.SwitchOff");
            bData.AvailabilityClassName = "RevitTestControl.AppsAvailability";
            Button = ribbonPanel.AddItem(bData) as PushButton;
            Button.ToolTip = "Switch On 0% selected element";
            pbImage = new BitmapImage(new Uri(thisAssemblyDir + "/Resources/LightOff.ico"));
            Button.LargeImage = pbImage;

            bData = new PushButtonData(
           "SetValue",
           "0",
           thisAssemblyPath,
           "RevitTestControl.LightControl.SetValue");

            bData.AvailabilityClassName = "RevitTestControl.AppsAvailability";
            _ButtonToChange = ribbonPanel.AddItem(bData) as PushButton;
            _ButtonToChange.ToolTip = "Set value of selected element";
            pbImage = new BitmapImage(new Uri(thisAssemblyDir + "/Resources/IconAmpoule.ico"));
            _ButtonToChange.LargeImage = pbImage;

            bData = new PushButtonData(
         "Consommation électrique",
        "Consommation électrique",
         thisAssemblyPath,
         "RevitTestControl.LightControl.LightEnergy");

            bData.AvailabilityClassName = "RevitTestControl.AppsAvailability";
            _ButtonToChange = ribbonPanel.AddItem(bData) as PushButton;
            _ButtonToChange.ToolTip = "Consommation électrique";
            pbImage = new BitmapImage(new Uri(thisAssemblyDir + "/Resources/energy.ico"));
            _ButtonToChange.LargeImage = pbImage;


            bData = new PushButtonData(
             "LightConfiguration",
             "Configuration",
             thisAssemblyPath,
             "RevitTestControl.LightControl.Configuration");
            bData.AvailabilityClassName = "RevitTestControl.AppsAvailability";
            Button = ribbonPanel.AddItem(bData) as PushButton;
            Button.ToolTip = "Configure dynamic behaviour for selected element";
            pbImage = new BitmapImage(new Uri(thisAssemblyDir + "/Resources/Configuration.ico"));
            Button.LargeImage = pbImage;


            // Add a new ribbon panel for temperatures
            ribbonPanel = application.CreateRibbonPanel(tabName, "Temperatures");

            bData = new PushButtonData(
            "TempButton",
            "Temperatures",
            thisAssemblyPath,
            "RevitTestControl.Temperatures.Temperature");

            Button = ribbonPanel.AddItem(bData) as PushButton;
            Button.ToolTip = "Get temperature for building";
            pbImage = new BitmapImage(new Uri(thisAssemblyDir + "/Resources/Temp.ico"));
            Button.LargeImage = pbImage;


            // Add a new ribbon panel for temperatures
            ribbonPanel = application.CreateRibbonPanel(tabName, "Windows");

            bData = new PushButtonData(
            "TempWindow",
            "Windows",
            thisAssemblyPath,
            "RevitTestControl.Fenetres.Windows");

            Button = ribbonPanel.AddItem(bData) as PushButton;
            Button.ToolTip = "Get open state of windows";
            pbImage = new BitmapImage(new Uri(thisAssemblyDir + "/Resources/window.ico"));
            Button.LargeImage = pbImage;


            // Add a new ribbon panel
            ribbonPanel = application.CreateRibbonPanel(tabName, "Information");
            
         
            

            // create push button for CurveTotalLength
            PushButtonData b1Data = new PushButtonData(
                "LightControl",
                "Show Element Data",
                thisAssemblyPath,
                "RevitTestControl.TestControl");

            //b1Data.cate
            
             PushButton pb1 = ribbonPanel.AddItem(b1Data) as PushButton;
            pb1.ToolTip = "Get data rom selected element";
            //BitmapImage pb1Image = new BitmapImage(new Uri("pack://application:,,,/RevitTestControl;component/Resources/IconAmpoule.ico"));
            BitmapImage pb1Image = new BitmapImage(new Uri(thisAssemblyDir + "/Resources/Information.png"));
            pb1.LargeImage = pb1Image;

            
        }

        //static MySqlConnection mServer = null;

        //internal static MySqlConnection SqlServer { get { return mServer; } }
        //internal static void ConnectToSql()
        //{
        //    string connectionString = "SERVER=10.192.12.5;DATABASE=TEST;UID=ssegard;PASSWORD=cesi";

        //    mServer = new MySqlConnection(connectionString);
        //    try
        //    {
        //        mServer.Open();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        //When handling errors, you can your application's response based 
        //        //on the error number.
        //        //The two most common error numbers when connecting are as follows:
        //        //0: Cannot connect to server.
        //        //1045: Invalid user name and/or password.
        //        switch (ex.Number)
        //        {
        //            case 0:
        //               System.Windows.Forms.MessageBox.Show("Cannot connect to server.  Contact administrator");
        //                break;

        //            case 1045:
        //                System.Windows.Forms.MessageBox.Show("Invalid username/password, please try again");
        //                break;
        //        }
        //        mServer = null;
        //        return;
        //    }

        //}

        public Result OnShutdown(UIControlledApplication application)
        {
            if(_thr!=null)
                _thr.Abort();

            //if (mServer != null)
            //    mServer.Close();
            // do nothing
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            AddRibbonPanel(application);
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

           
            // call our method that will load up our toolbar
            return Result.Succeeded;
        }

        private void ControlledApplication_DocumentOpened(object sender, Autodesk.Revit.DB.Events.DocumentOpenedEventArgs e)
        {
           
        }

        static Thread _thr = null;
        static UIApplication _uiApplication;

        static internal void StartWatching(UIApplication puiApplication)
        {
            _uiApplication = puiApplication;
            _thr = new Thread(new ThreadStart(CheckSelectionChange));
            _thr.Start();

        
        }


       


        static void CheckSelectionChange()
        {
            Autodesk.Revit.UI.Selection.Selection selection = _uiApplication.ActiveUIDocument.Selection;
            ElementId IdSelection;
            ICollection<ElementId> selectedIds = selection.GetElementIds();
            int PreviousId = 0;
            if (selectedIds.Count == 0)
            {
                IdSelection = null;
                PreviousId = 0;
            }
            else
            {
                IdSelection = selectedIds.ElementAt(selectedIds.Count - 1);
                PreviousId = IdSelection.IntegerValue;
            }

            int i = 0, CurrentId = 0;
            bool IsSecond = true; ;
            while (true)
            {
                Thread.Sleep(500);

                
                if(IsSecond && RaisePeriodicEvent)
                    _PeriodicEvent.Raise();
                IsSecond = !IsSecond;

                selection = _uiApplication.ActiveUIDocument.Selection;
                selectedIds = selection.GetElementIds();
                if (selectedIds.Count == 0)
                {
                    IdSelection = null;
                    CurrentId = 0;
                }
                else
                {
                    IdSelection = selectedIds.ElementAt(selectedIds.Count - 1);
                    CurrentId = IdSelection.IntegerValue;
                }
                if (CurrentId != PreviousId)
                {

                   
                    if(RaiseOnSelectionEvent)
                         _SelectionChangedEvent.Raise();
                  
                    
                    PreviousId = CurrentId;
                    i++;

                    
                }

                
            }
        }

        internal static void RaiseWindowsEvent()
        {
            _WindowsEvent.Raise();
        }



    }
}


