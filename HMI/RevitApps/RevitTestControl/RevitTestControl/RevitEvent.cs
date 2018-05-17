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


namespace RevitTestControl
{
    class RevitPeriodicEvent : IExternalEventHandler
    {
        //event called vy a thread each second
        public void Execute(UIApplication app)
        {
            Fenetres.InWindows.Animate(app);

        }

        public string GetName()
        {
            return "RevitPeriodicEvent";
        }
    }


        class RevitSelectionChangedEvent : IExternalEventHandler
        {
            public void Execute(UIApplication app)
            {
                //do nothing
            }

            public string GetName()
            {
                return "RevitSelectionChangedEvent";
            }
        }



    class RevitWindowsEvent : IExternalEventHandler
    {
        //event called vy a thread each second
        public void Execute(UIApplication app)
        {
            Fenetres.InWindows.Animate(app);

        }

        public string GetName()
        {
            return "RevitWindowsEvent";
        }
    }

}
