
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


namespace RevitTestControl.LightControl
{
    [Transaction(TransactionMode.ReadOnly)]
    public class LightEnergy : IExternalCommand
    {
        

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiApp = commandData.Application;
            UIDocument doc = uiApp.ActiveUIDocument;


            IWin32Window revit_window = new JtWindowHandle(ComponentManager.ApplicationWindow);

            try
            {
                LightEnergyForm frm = new LightEnergyForm();
                frm.Connect();
                frm.ShowDialog(revit_window);
            }
            catch
            {
                Autodesk.Revit.UI.TaskDialog.Show("Revit", "Problem !");
                return Result.Succeeded;
            }

            return Result.Succeeded;
        }
    }
}
