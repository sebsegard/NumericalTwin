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

namespace RevitTestControl.Fenetres
{
    [Transaction(TransactionMode.ReadOnly)]
    public class Windows : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument doc = uiApp.ActiveUIDocument;


           // commandData.

            IWin32Window revit_window = new JtWindowHandle(ComponentManager.ApplicationWindow);





            Selection selection = doc.Selection;


            ICollection<ElementId> selectedIds = selection.GetElementIds();
            Element SelectedElement = null, FamilleElement = null;

            if (1 == selectedIds.Count)
            {

                foreach (ElementId id in selectedIds)
                {
                    SelectedElement = doc.Document.GetElement(id);
                    FamilleElement = doc.Document.GetElement(SelectedElement.GetTypeId());
                }

                //TaskDialog.Show("Revit", info);
            }
          


            try
            {
                WindowForm frm = new WindowForm();
                if (SelectedElement != null)
                    frm.SetSelectElement(SelectedElement.Id.IntegerValue);
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
