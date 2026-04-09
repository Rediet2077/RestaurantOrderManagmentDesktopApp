using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public class ToastNotification : Form
    {
        private System.Windows.Forms.Timer timer;
        private int duration = 3000;
        private Label lblMessage;
        private Panel accentPanel;

        public ToastNotification(string message, bool isError = false)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Size = new Size(300, 60);
            this.BackColor = UIHelper.IsDarkMode ? UIHelper.DarkSidebar : Color.White;

            accentPanel = new Panel();
            accentPanel.Dock = DockStyle.Left;
            accentPanel.Width = 5;
            accentPanel.BackColor = isError ? UIHelper.DangerColor : UIHelper.SuccessColor;
            this.Controls.Add(accentPanel);

            lblMessage = new Label();
            lblMessage.Text = message;
            lblMessage.ForeColor = UIHelper.IsDarkMode ? UIHelper.DarkText : UIHelper.PrimaryColor;
            lblMessage.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblMessage.Dock = DockStyle.Fill;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblMessage.Padding = new Padding(10);
            this.Controls.Add(lblMessage);

            // Position it at bottom right
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(workingArea.Right - this.Width - 20, workingArea.Bottom - this.Height - 20);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = duration;
            timer.Tick += (s, e) =>
            {
                this.Dispose();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            timer.Start();
            UIHelper.SetRoundedRegion(this, 10);

            // Animation (simple)
            this.Opacity = 0;
            System.Windows.Forms.Timer animTimer = new System.Windows.Forms.Timer();
            animTimer.Interval = 10;
            animTimer.Tick += (s, e_anim) =>
            {
                if (this.Opacity < 1) this.Opacity += 0.1;
                else animTimer.Stop();
            };
            animTimer.Start();
        }

        private void InitializeComponent()
        {

        }
    }
}
