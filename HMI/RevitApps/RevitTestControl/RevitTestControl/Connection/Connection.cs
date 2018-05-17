
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;
using Autodesk.Windows;
using Autodesk.Revit.Attributes;
using MySql.Data.MySqlClient;


namespace RevitTestControl.Connection
{
    [Transaction(TransactionMode.ReadOnly)]
    public class Connection : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
         

            //lauch thread for element selection changed detection
            UIApplication uiApp = commandData.Application;
            App.StartWatching(uiApp);

           // App.ConnectToSql();
            LightControl.API.PhilipsRestApi.Init();
            Fenetres.WindowsConfigMngt.Init();
           

            return Result.Succeeded;
        }


    }
}
