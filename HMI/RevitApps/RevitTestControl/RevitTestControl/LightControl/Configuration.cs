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
    public class Configuration : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiApp = commandData.Application;



            UIDocument doc = uiApp.ActiveUIDocument;
            //commandData.View.
            IWin32Window revit_window = new JtWindowHandle(ComponentManager.ApplicationWindow);


            Selection selection = doc.Selection;


            ICollection<ElementId> selectedIds = selection.GetElementIds();
            Element SelectedElement = null, FamilleElement = null;

            if (0 == selectedIds.Count)
            {
                // If no elements selected.
                Autodesk.Revit.UI.TaskDialog.Show("Revit", "You haven't selected any elements.");
                return Result.Succeeded;
            }
            else if (1 == selectedIds.Count)
            {

                foreach (ElementId id in selectedIds)
                {
                    SelectedElement = doc.Document.GetElement(id);
                    FamilleElement = doc.Document.GetElement(SelectedElement.GetTypeId());
                }

                //TaskDialog.Show("Revit", info);
            }
            else if (1 < selectedIds.Count)
            {
                // If no elements selected.
                Autodesk.Revit.UI.TaskDialog.Show("Revit", "You have selected more than 1 elements.");
                return Result.Succeeded;
            }

            if (SelectedElement != null)
            {

                API.LightFixture fix = API.LightManager.GetLight(SelectedElement.UniqueId);

                ConfigurationForm frm = new ConfigurationForm();
                frm.LoadTree();
                frm.Fixture = fix;
                frm.ShowDialog(revit_window);
            }
            return Result.Succeeded;
        }
    }
}

