using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

namespace RevitTestControl
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void SetRevitInfo(Document pDoc,Element pFamille, Element pEment)
        {
            this.listBox1.Items.Add("name : "+pEment.Name);
            this.listBox1.Items.Add("groupId : "+pEment.GroupId);
            this.listBox1.Items.Add("UniqueId : " + pEment.UniqueId);
            this.listBox1.Items.Add("Category : " + pEment.Category.Name);

            //this.listBox1.Items.Add("Family Id : " + pFamille.UniqueId);

            if (pFamille != null)
            {
                this.listBox1.Items.Add("Family  : " + pFamille.Name);
                this.textBox1.Text = pFamille.UniqueId;
            }

        }
    }
}
