namespace RevitTestControl.LightControl
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btn_CLear = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.txt_Luminaire = new System.Windows.Forms.TextBox();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.txtGuid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_AreaOn = new System.Windows.Forms.ComboBox();
            this.cmb_AreaOff = new System.Windows.Forms.ComboBox();
            this.btn_ApplyAreaConf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btn_CLear);
            this.splitContainer1.Panel2.Controls.Add(this.btn_cancel);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Apply);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Luminaire);
            this.splitContainer1.Panel2.Controls.Add(this.txtArea);
            this.splitContainer1.Panel2.Controls.Add(this.txtGuid);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(843, 483);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(281, 483);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // btn_CLear
            // 
            this.btn_CLear.Location = new System.Drawing.Point(39, 220);
            this.btn_CLear.Name = "btn_CLear";
            this.btn_CLear.Size = new System.Drawing.Size(171, 33);
            this.btn_CLear.TabIndex = 8;
            this.btn_CLear.Text = "Clear all configurations";
            this.btn_CLear.UseVisualStyleBackColor = true;
            this.btn_CLear.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(360, 220);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(84, 33);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(228, 220);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(126, 33);
            this.btn_Apply.TabIndex = 6;
            this.btn_Apply.Text = "Save and apply";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // txt_Luminaire
            // 
            this.txt_Luminaire.Location = new System.Drawing.Point(196, 135);
            this.txt_Luminaire.Name = "txt_Luminaire";
            this.txt_Luminaire.Size = new System.Drawing.Size(239, 22);
            this.txt_Luminaire.TabIndex = 5;
            // 
            // txtArea
            // 
            this.txtArea.Location = new System.Drawing.Point(196, 89);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(239, 22);
            this.txtArea.TabIndex = 4;
            // 
            // txtGuid
            // 
            this.txtGuid.Location = new System.Drawing.Point(196, 44);
            this.txtGuid.Name = "txtGuid";
            this.txtGuid.Size = new System.Drawing.Size(239, 22);
            this.txtGuid.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Luminaire ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Area Id";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fixture GUID";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_ApplyAreaConf);
            this.groupBox1.Controls.Add(this.cmb_AreaOff);
            this.groupBox1.Controls.Add(this.cmb_AreaOn);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(80, 326);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 145);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Area Configuration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "On level : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Off level : ";
            // 
            // cmb_AreaOn
            // 
            this.cmb_AreaOn.FormattingEnabled = true;
            this.cmb_AreaOn.Location = new System.Drawing.Point(97, 34);
            this.cmb_AreaOn.Name = "cmb_AreaOn";
            this.cmb_AreaOn.Size = new System.Drawing.Size(267, 24);
            this.cmb_AreaOn.TabIndex = 2;
            // 
            // cmb_AreaOff
            // 
            this.cmb_AreaOff.FormattingEnabled = true;
            this.cmb_AreaOff.Location = new System.Drawing.Point(97, 64);
            this.cmb_AreaOff.Name = "cmb_AreaOff";
            this.cmb_AreaOff.Size = new System.Drawing.Size(267, 24);
            this.cmb_AreaOff.TabIndex = 3;
            // 
            // btn_ApplyAreaConf
            // 
            this.btn_ApplyAreaConf.Location = new System.Drawing.Point(183, 106);
            this.btn_ApplyAreaConf.Name = "btn_ApplyAreaConf";
            this.btn_ApplyAreaConf.Size = new System.Drawing.Size(181, 33);
            this.btn_ApplyAreaConf.TabIndex = 10;
            this.btn_ApplyAreaConf.Text = "Apply Area configuration";
            this.btn_ApplyAreaConf.UseVisualStyleBackColor = true;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 483);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ConfigurationForm";
            this.Text = "LightingConfig";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_CLear;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_Apply;
        private System.Windows.Forms.TextBox txt_Luminaire;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.TextBox txtGuid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_ApplyAreaConf;
        private System.Windows.Forms.ComboBox cmb_AreaOff;
        private System.Windows.Forms.ComboBox cmb_AreaOn;
    }
}