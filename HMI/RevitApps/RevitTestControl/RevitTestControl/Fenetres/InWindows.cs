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
    public class InWindows 
    {

        static int Counter = 0;
        static bool _Open = false;



        static object _LockObject = new object();
        public static void Animate(UIApplication pAppli)
        {
            /*
            Counter++;
            if (Counter % 5 == 0)
                return;

            Autodesk.Revit.UI.Selection.Selection selection = pAppli.ActiveUIDocument.Selection;
            UIDocument doc = pAppli.ActiveUIDocument;
            ElementId IdSelection;
            ICollection<ElementId> selectedIds = selection.GetElementIds();

            if (selectedIds.Count == 0)
                return;

            Element SelectedElement = null, FamilleElement = null;

            if (0 == selectedIds.Count)
            {
                return;
            }
            else if (1 == selectedIds.Count)
            {

                foreach (ElementId id in selectedIds)
                {
                    SelectedElement = doc.Document.GetElement(id);
                    FamilleElement = doc.Document.GetElement(SelectedElement.GetTypeId());
                }

               
            }
            Parameter AngleParam = null;
            foreach (Parameter param in SelectedElement.Parameters)
            {
                if (param.Definition.Name == "Vent opening percentage")
                    AngleParam = param;
            }

            if (AngleParam == null)
                return;

            using (Transaction t = new Transaction(doc.Document, "Set Parameter"))
            {
                t.Start();
                AngleParam.Set(_Open?0:100);
                _Open = !_Open;
                t.Commit();
            }

          
        */
            Element SelectedElement = null;

            List<WindowsEventData> Events = new List<WindowsEventData>();
            lock (_LockObject)
            {
                foreach (WindowsEventData evt in _WindowsEventToSend)
                    Events.Add(evt);

                _WindowsEventToSend.Clear();
            }
            UIDocument doc = pAppli.ActiveUIDocument;


            if (Events.Count == 0)
                return;

            foreach (WindowsEventData evt in Events)
            {
                SelectedElement = doc.Document.GetElement(evt.Id);
                Parameter AngleParam = null;
                foreach (Parameter param in SelectedElement.Parameters)
                {
                    if (param.Definition.Name == "Vent opening angle")
                        AngleParam = param;
                }

                if (AngleParam == null)
                    return;

                using (Transaction t = new Transaction(doc.Document, "Set Parameter"))
                {
                    t.Start();
                    AngleParam.Set(evt.State ? 45 : 0);
                    t.Commit();
                }
            }



        }

        private static List<WindowsEventData> _WindowsEventToSend = new List<WindowsEventData>();

        internal static void AddWindowsEvent(ElementId pId, bool pState)
        {
            lock(_LockObject)
            {
                _WindowsEventToSend.Add(new WindowsEventData(pId, pState));
            }
        }
    }


    

    public class WindowsEventData
    {
        public ElementId Id { get; set; }
        public bool State { get; set; }

        public WindowsEventData(ElementId pElementId, bool pState)
        {
            Id = pElementId;
            State = pState;
        }
    }
}
