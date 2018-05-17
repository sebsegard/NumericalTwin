using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RevitTestControl.LightControl.API;

using Autodesk.Revit.DB;
//using Autodesk.Revit.UI;
//using Autodesk.Revit.ApplicationServices;
//using Autodesk.Revit.UI.Selection;
//using Autodesk.Windows;
//using Autodesk.Revit.Attributes;

namespace RevitTestControl.LightControl
{
    public partial class ConfigurationForm : System.Windows.Forms.Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();
            this.groupBox1.Visible = false;
        }


        public void LoadTree()
        {
            try
            {

                Areas Areas = PhilipsRestApi.GetAreas();

                foreach (API.Area area in Areas.Area_List)
                {
                    TreeNode nd = new TreeNode(area.name);
                    nd.Tag = area;
                    this.treeView1.Nodes.Add(nd);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(this, "Are you sure ?", "confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.OK)
                LightManager.Clear();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public LightFixture Fixture { get; set; }


        private void btn_Apply_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode.Tag is API.Area)
            {
                Fixture.AreaId = Convert.ToInt32(this.txtArea.Text);
                Fixture.LuminaireId = LightFixture.UNKNOW_LIGHT_ID;
            }
            else
            {
                Fixture.AreaId = Convert.ToInt32(this.txtArea.Text);
                Fixture.LuminaireId = Convert.ToInt32(this.txt_Luminaire.Text);
            }
           
            LightManager.SaveXml();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            //this.propertyGrid1.SelectedObject = e.Node.Tag;

            if (e.Node.Tag == null)
                return;

            if (e.Node.Tag is API.Area)
            {
                API.Area area = e.Node.Tag as API.Area;
                this.txtArea.Text = area.areaID.ToString();
                this.txt_Luminaire.Text = "";

                this.groupBox1.Visible = true;
                if (e.Node.Nodes.Count == 0)
                {
                    ExpandArea(e.Node, e.Node.Tag as API.Area);
                   
                }

                cmb_AreaOff.Items.Clear();
                cmb_AreaOn.Items.Clear();
                AreaLevel[] levels = PhilipsRestApi.GetAreaLevels(area.areaID);
                foreach (AreaLevel lvl in levels)
                {
                    cmb_AreaOff.Items.Add(lvl);
                    cmb_AreaOn.Items.Add(lvl);
                }
            }
            else if (e.Node.Tag is Luminaire)
            {
                this.groupBox1.Visible = false;
                Luminaire lum = e.Node.Tag as Luminaire;
                API.Area area = e.Node.Parent.Tag as API.Area;
                this.txtArea.Text = area.areaID.ToString();
                this.txt_Luminaire.Text = lum.luminaireID.ToString();
            }

        }

        void ExpandArea(TreeNode node, API.Area pArea)
        {
            Luminaire[] lums = PhilipsRestApi.GetLuminerLevels(pArea);

            foreach (Luminaire lum in lums)
            {
                TreeNode nd = new TreeNode(lum.luminaireID.ToString());
                nd.Tag = lum;
                node.Nodes.Add(nd);
            }
           

        }
    }
}
