namespace RevitTestControl.CVC
{
    partial class Clim
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
            this.cmb_Room = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_Error = new System.Windows.Forms.TextBox();
            this.txt_Mode = new System.Windows.Forms.TextBox();
            this.btn_Envoi = new System.Windows.Forms.Button();
            this.chk_UserAction = new System.Windows.Forms.CheckBox();
            this.list_Mode = new System.Windows.Forms.ListBox();
            this.txt_RoomTemp = new System.Windows.Forms.TextBox();
            this.txt_Fan = new System.Windows.Forms.TextBox();
            this.list_Fan = new System.Windows.Forms.ListBox();
            this.txt_SetTemp = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txt_Etat = new System.Windows.Forms.TextBox();
            this.list_Etat = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_Room
            // 
            this.cmb_Room.FormattingEnabled = true;
            this.cmb_Room.Items.AddRange(new object[] {
            "TESLA",
            "LUMIERE",
            "NOBEL",
            "TURING",
            "Batiment",
            "NOBEL2",
            "METRO",
            "LOCAL"});
            this.cmb_Room.Location = new System.Drawing.Point(77, 11);
            this.cmb_Room.Name = "cmb_Room";
            this.cmb_Room.Size = new System.Drawing.Size(237, 24);
            this.cmb_Room.TabIndex = 0;
            this.cmb_Room.SelectedIndexChanged += new System.EventHandler(this.cmb_Room_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Salle";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(329, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_Error
            // 
            this.txt_Error.BackColor = System.Drawing.SystemColors.Control;
            this.txt_Error.Location = new System.Drawing.Point(16, 21);
            this.txt_Error.Name = "txt_Error";
            this.txt_Error.ReadOnly = true;
            this.txt_Error.Size = new System.Drawing.Size(469, 22);
            this.txt_Error.TabIndex = 5;
            // 
            // txt_Mode
            // 
            this.txt_Mode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Mode.Location = new System.Drawing.Point(6, 21);
            this.txt_Mode.Name = "txt_Mode";
            this.txt_Mode.ReadOnly = true;
            this.txt_Mode.Size = new System.Drawing.Size(229, 22);
            this.txt_Mode.TabIndex = 6;
            // 
            // btn_Envoi
            // 
            this.btn_Envoi.Location = new System.Drawing.Point(671, 9);
            this.btn_Envoi.Name = "btn_Envoi";
            this.btn_Envoi.Size = new System.Drawing.Size(85, 30);
            this.btn_Envoi.TabIndex = 7;
            this.btn_Envoi.Text = "Envoyer";
            this.btn_Envoi.UseVisualStyleBackColor = true;
            this.btn_Envoi.Click += new System.EventHandler(this.btn_Envoi_Click);
            // 
            // chk_UserAction
            // 
            this.chk_UserAction.AutoSize = true;
            this.chk_UserAction.Location = new System.Drawing.Point(16, 28);
            this.chk_UserAction.Name = "chk_UserAction";
            this.chk_UserAction.Size = new System.Drawing.Size(348, 21);
            this.chk_UserAction.TabIndex = 9;
            this.chk_UserAction.Text = "Permettre le contrôle du chauffage sur les platines";
            this.chk_UserAction.UseVisualStyleBackColor = true;
            // 
            // list_Mode
            // 
            this.list_Mode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Mode.FormattingEnabled = true;
            this.list_Mode.ItemHeight = 16;
            this.list_Mode.Items.AddRange(new object[] {
            "Cool",
            "Heat",
            "Fan",
            "Auto",
            "Dry"});
            this.list_Mode.Location = new System.Drawing.Point(6, 49);
            this.list_Mode.Name = "list_Mode";
            this.list_Mode.Size = new System.Drawing.Size(229, 132);
            this.list_Mode.TabIndex = 10;
            // 
            // txt_RoomTemp
            // 
            this.txt_RoomTemp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_RoomTemp.Location = new System.Drawing.Point(9, 22);
            this.txt_RoomTemp.Name = "txt_RoomTemp";
            this.txt_RoomTemp.ReadOnly = true;
            this.txt_RoomTemp.Size = new System.Drawing.Size(236, 22);
            this.txt_RoomTemp.TabIndex = 11;
            // 
            // txt_Fan
            // 
            this.txt_Fan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Fan.Location = new System.Drawing.Point(6, 22);
            this.txt_Fan.Name = "txt_Fan";
            this.txt_Fan.ReadOnly = true;
            this.txt_Fan.Size = new System.Drawing.Size(216, 22);
            this.txt_Fan.TabIndex = 12;
            // 
            // list_Fan
            // 
            this.list_Fan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Fan.FormattingEnabled = true;
            this.list_Fan.ItemHeight = 16;
            this.list_Fan.Items.AddRange(new object[] {
            "Low",
            "High",
            "Mid2",
            "Mid1",
            "Auto"});
            this.list_Fan.Location = new System.Drawing.Point(6, 50);
            this.list_Fan.Name = "list_Fan";
            this.list_Fan.Size = new System.Drawing.Size(216, 132);
            this.list_Fan.TabIndex = 13;
            // 
            // txt_SetTemp
            // 
            this.txt_SetTemp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_SetTemp.Location = new System.Drawing.Point(9, 50);
            this.txt_SetTemp.Name = "txt_SetTemp";
            this.txt_SetTemp.Size = new System.Drawing.Size(236, 22);
            this.txt_SetTemp.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Mode);
            this.groupBox1.Controls.Add(this.list_Mode);
            this.groupBox1.Location = new System.Drawing.Point(13, 168);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 194);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_SetTemp);
            this.groupBox2.Controls.Add(this.txt_RoomTemp);
            this.groupBox2.Location = new System.Drawing.Point(265, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 100);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Temperature";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_Fan);
            this.groupBox3.Controls.Add(this.list_Fan);
            this.groupBox3.Location = new System.Drawing.Point(528, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(228, 191);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ventilateur";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txt_Error);
            this.groupBox4.Location = new System.Drawing.Point(265, 45);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(491, 55);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Error";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chk_UserAction);
            this.groupBox5.Location = new System.Drawing.Point(265, 107);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(491, 55);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Actions utilisateurs";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txt_Etat);
            this.groupBox6.Controls.Add(this.list_Etat);
            this.groupBox6.Location = new System.Drawing.Point(22, 46);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(237, 110);
            this.groupBox6.TabIndex = 22;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Etat";
            // 
            // txt_Etat
            // 
            this.txt_Etat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Etat.Location = new System.Drawing.Point(6, 21);
            this.txt_Etat.Name = "txt_Etat";
            this.txt_Etat.ReadOnly = true;
            this.txt_Etat.Size = new System.Drawing.Size(220, 22);
            this.txt_Etat.TabIndex = 6;
            // 
            // list_Etat
            // 
            this.list_Etat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Etat.FormattingEnabled = true;
            this.list_Etat.ItemHeight = 16;
            this.list_Etat.Items.AddRange(new object[] {
            "Stop",
            "Run"});
            this.list_Etat.Location = new System.Drawing.Point(6, 49);
            this.list_Etat.Name = "list_Etat";
            this.list_Etat.Size = new System.Drawing.Size(220, 52);
            this.list_Etat.TabIndex = 10;
            // 
            // Clim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 393);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Envoi);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_Room);
            this.Name = "Clim";
            this.Text = "CVC";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_Room;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_Error;
        private System.Windows.Forms.TextBox txt_Mode;
        private System.Windows.Forms.Button btn_Envoi;
        private System.Windows.Forms.CheckBox chk_UserAction;
        private System.Windows.Forms.ListBox list_Mode;
        private System.Windows.Forms.TextBox txt_RoomTemp;
        private System.Windows.Forms.TextBox txt_Fan;
        private System.Windows.Forms.ListBox list_Fan;
        private System.Windows.Forms.TextBox txt_SetTemp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txt_Etat;
        private System.Windows.Forms.ListBox list_Etat;
    }
}