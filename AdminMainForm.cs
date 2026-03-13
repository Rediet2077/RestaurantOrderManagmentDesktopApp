using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponent();
        }

        private void btnManageMenu_Click(object sender, EventArgs e)
        {
            LoadForm(new Menu_Form());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            LoadForm(new ReportForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void LoadForm(Form f)
        {
            if (this.mainPanel.Controls.Count > 0)
                this.mainPanel.Controls.RemoveAt(0);
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }
    }
}
