namespace ChromiumUpdater.Ui.WinForms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelLatest = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.buttonRefresh, 1, 1);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // buttonRefresh
            // 
            resources.ApplyResources(this.buttonRefresh, "buttonRefresh");
            this.buttonRefresh.Image = global::ChromiumUpdater.Ui.WinForms.Properties.Resources._112_RefreshArrow_Blue_32x32;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelLatest);
            this.panel1.Name = "panel1";
            // 
            // labelLatest
            // 
            resources.ApplyResources(this.labelLatest, "labelLatest");
            this.labelLatest.Name = "labelLatest";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Image = global::ChromiumUpdater.Ui.WinForms.Properties.Resources._112_RefreshArrow_Blue_32x32;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelLatest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

