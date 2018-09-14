namespace RevitTestControl.CVC
{
    partial class CVCForm
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
            this.txt_Fan_Reprise = new System.Windows.Forms.TextBox();
            this.txt_P_Reprise = new System.Windows.Forms.TextBox();
            this.txt_T_Reprise = new System.Windows.Forms.TextBox();
            this.txt_T_Soufflage = new System.Windows.Forms.TextBox();
            this.txt_Fan_Soufflage = new System.Windows.Forms.TextBox();
            this.txt_P_Soufflage = new System.Windows.Forms.TextBox();
            this.txt_T_Ext = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_Fan_Reprise
            // 
            this.txt_Fan_Reprise.Location = new System.Drawing.Point(115, 71);
            this.txt_Fan_Reprise.Name = "txt_Fan_Reprise";
            this.txt_Fan_Reprise.Size = new System.Drawing.Size(100, 22);
            this.txt_Fan_Reprise.TabIndex = 0;
            // 
            // txt_P_Reprise
            // 
            this.txt_P_Reprise.Location = new System.Drawing.Point(115, 43);
            this.txt_P_Reprise.Name = "txt_P_Reprise";
            this.txt_P_Reprise.Size = new System.Drawing.Size(100, 22);
            this.txt_P_Reprise.TabIndex = 1;
            // 
            // txt_T_Reprise
            // 
            this.txt_T_Reprise.Location = new System.Drawing.Point(302, 137);
            this.txt_T_Reprise.Name = "txt_T_Reprise";
            this.txt_T_Reprise.Size = new System.Drawing.Size(100, 22);
            this.txt_T_Reprise.TabIndex = 2;
            // 
            // txt_T_Soufflage
            // 
            this.txt_T_Soufflage.Location = new System.Drawing.Point(620, 277);
            this.txt_T_Soufflage.Name = "txt_T_Soufflage";
            this.txt_T_Soufflage.Size = new System.Drawing.Size(100, 22);
            this.txt_T_Soufflage.TabIndex = 3;
            // 
            // txt_Fan_Soufflage
            // 
            this.txt_Fan_Soufflage.Location = new System.Drawing.Point(526, 208);
            this.txt_Fan_Soufflage.Name = "txt_Fan_Soufflage";
            this.txt_Fan_Soufflage.Size = new System.Drawing.Size(100, 22);
            this.txt_Fan_Soufflage.TabIndex = 4;
            // 
            // txt_P_Soufflage
            // 
            this.txt_P_Soufflage.Location = new System.Drawing.Point(526, 180);
            this.txt_P_Soufflage.Name = "txt_P_Soufflage";
            this.txt_P_Soufflage.Size = new System.Drawing.Size(100, 22);
            this.txt_P_Soufflage.TabIndex = 5;
            // 
            // txt_T_Ext
            // 
            this.txt_T_Ext.Location = new System.Drawing.Point(560, 23);
            this.txt_T_Ext.Name = "txt_T_Ext";
            this.txt_T_Ext.Size = new System.Drawing.Size(100, 22);
            this.txt_T_Ext.TabIndex = 6;
            // 
            // CVCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::RevitTestControl.Properties.Resources.vmc;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(747, 430);
            this.Controls.Add(this.txt_T_Ext);
            this.Controls.Add(this.txt_P_Soufflage);
            this.Controls.Add(this.txt_Fan_Soufflage);
            this.Controls.Add(this.txt_T_Soufflage);
            this.Controls.Add(this.txt_T_Reprise);
            this.Controls.Add(this.txt_P_Reprise);
            this.Controls.Add(this.txt_Fan_Reprise);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(765, 475);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(765, 475);
            this.Name = "CVCForm";
            this.Text = "VMC";
            this.Load += new System.EventHandler(this.CVCForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Fan_Reprise;
        private System.Windows.Forms.TextBox txt_P_Reprise;
        private System.Windows.Forms.TextBox txt_T_Reprise;
        private System.Windows.Forms.TextBox txt_T_Soufflage;
        private System.Windows.Forms.TextBox txt_Fan_Soufflage;
        private System.Windows.Forms.TextBox txt_P_Soufflage;
        private System.Windows.Forms.TextBox txt_T_Ext;
    }
}