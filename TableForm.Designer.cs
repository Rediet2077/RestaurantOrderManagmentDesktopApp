namespace RestaurantDesktopApp
{
    partial class TableForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvTables;
        private ComboBox cmbStatus;
        private Button btnUpdateStatus;
        private Label lblTitle;
        private Panel headerPanel;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(780, 60);
            this.headerPanel.TabIndex = 10;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(211, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Table Management";
            // 
            // dgvTables
            // 
            this.dgvTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTables.BackgroundColor = System.Drawing.Color.White;
            this.dgvTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTables.EnableHeadersVisualStyles = false;
            this.dgvTables.Location = new System.Drawing.Point(30, 80);
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTables.Size = new System.Drawing.Size(400, 300);
            this.dgvTables.TabIndex = 0;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbStatus.Items.AddRange(new object[] {
            "Available",
            "Reserved",
            "Occupied"});
            this.cmbStatus.Location = new System.Drawing.Point(30, 410);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 28);
            this.cmbStatus.TabIndex = 1;
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnUpdateStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateStatus.FlatAppearance.BorderSize = 0;
            this.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStatus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStatus.Location = new System.Drawing.Point(250, 405);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(150, 35);
            this.btnUpdateStatus.TabIndex = 2;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = false;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(780, 530);
            this.Controls.Add(this.btnUpdateStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.dgvTables);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TableForm";
            this.Text = "Table Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
