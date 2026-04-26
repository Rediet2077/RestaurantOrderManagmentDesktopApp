namespace RestaurantDesktopApp
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRestName;
        private System.Windows.Forms.TextBox txtRestName;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.ComboBox cmbCurrency;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTheme;
        private System.Windows.Forms.CheckBox chkDarkMode;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRestName = new System.Windows.Forms.Label();
            this.txtRestName = new System.Windows.Forms.TextBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTheme = new System.Windows.Forms.Label();
            this.chkDarkMode = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.chkDarkMode);
            this.panelMain.Controls.Add(this.lblTheme);
            this.panelMain.Controls.Add(this.btnSave);
            this.panelMain.Controls.Add(this.cmbCurrency);
            this.panelMain.Controls.Add(this.lblCurrency);
            this.panelMain.Controls.Add(this.txtRestName);
            this.panelMain.Controls.Add(this.lblRestName);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Location = new System.Drawing.Point(30, 30);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(700, 450);
            this.panelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SYSTEM SETTINGS";
            // 
            // lblRestName
            // 
            this.lblRestName.AutoSize = true;
            this.lblRestName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblRestName.Location = new System.Drawing.Point(40, 100);
            this.lblRestName.Name = "lblRestName";
            this.lblRestName.Size = new System.Drawing.Size(136, 21);
            this.lblRestName.TabIndex = 1;
            this.lblRestName.Text = "Restaurant Name";
            // 
            // txtRestName
            // 
            this.txtRestName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRestName.Location = new System.Drawing.Point(40, 130);
            this.txtRestName.Name = "txtRestName";
            this.txtRestName.Size = new System.Drawing.Size(400, 29);
            this.txtRestName.TabIndex = 2;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblCurrency.Location = new System.Drawing.Point(40, 180);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(135, 21);
            this.lblCurrency.TabIndex = 3;
            this.lblCurrency.Text = "Default Currency";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrency.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Items.AddRange(new object[] {
            "Birr (ETB)",
            "$ (USD)",
            "€ (EUR)",
            "£ (GBP)",
            "¥ (JPY)"});
            this.cmbCurrency.Location = new System.Drawing.Point(40, 210);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(200, 29);
            this.cmbCurrency.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(40, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 45);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "SAVE CHANGES";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTheme
            // 
            this.lblTheme.AutoSize = true;
            this.lblTheme.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTheme.Location = new System.Drawing.Point(40, 260);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(130, 21);
            this.lblTheme.TabIndex = 6;
            this.lblTheme.Text = "Visual Preference";
            // 
            // chkDarkMode
            // 
            this.chkDarkMode.AutoSize = true;
            this.chkDarkMode.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.chkDarkMode.Location = new System.Drawing.Point(40, 290);
            this.chkDarkMode.Name = "chkDarkMode";
            this.chkDarkMode.Size = new System.Drawing.Size(188, 25);
            this.chkDarkMode.TabIndex = 7;
            this.chkDarkMode.Text = "Enable Dark Mode (UI)";
            this.chkDarkMode.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(760, 520);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingsForm";
            this.Text = "System Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
