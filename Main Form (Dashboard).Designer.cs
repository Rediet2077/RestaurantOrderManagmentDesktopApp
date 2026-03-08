namespace RestaurantDesktopApp
{
    partial class Main_Form__Dashboard_
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
            btnManageMenu = new Button();
            btnCreateOrder = new Button();
            btnExit = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnManageMenu
            // 
            btnManageMenu.Location = new Point(108, 53);
            btnManageMenu.Name = "btnManageMenu";
            btnManageMenu.Size = new Size(95, 23);
            btnManageMenu.TabIndex = 0;
            btnManageMenu.Text = "Manage Menu";
            btnManageMenu.UseVisualStyleBackColor = true;
            btnManageMenu.Click += btnManageMenu_Click;
            // 
            // btnCreateOrder
            // 
            btnCreateOrder.Location = new Point(231, 53);
            btnCreateOrder.Name = "btnCreateOrder";
            btnCreateOrder.Size = new Size(109, 23);
            btnCreateOrder.TabIndex = 1;
            btnCreateOrder.Text = "Create Order";
            btnCreateOrder.UseVisualStyleBackColor = true;
            btnCreateOrder.Click += btnCreateOrder_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(179, 99);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(108, 18);
            label1.Name = "label1";
            label1.Size = new Size(238, 15);
            label1.TabIndex = 3;
            label1.Text = "------Restaurant Management System------";
            // 
            // Main_Form__Dashboard_
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btnExit);
            Controls.Add(btnCreateOrder);
            Controls.Add(btnManageMenu);
            Name = "Main_Form__Dashboard_";
            Text = "Main_Form__Dashboard_";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnManageMenu;
        private Button btnCreateOrder;
        private Button btnExit;
        private Label label1;
    }
}