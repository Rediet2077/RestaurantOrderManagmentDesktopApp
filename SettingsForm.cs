using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UIHelper.SetRoundedRegion(panelMain, 20);
            UIHelper.ApplyModernButton(btnSave, UIHelper.SuccessColor);
            
            // Load current settings (placeholders for now)
            txtRestName.Text = "GASTRO TECH";
            cmbCurrency.SelectedItem = "$ (USD)";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings saved successfully! (Note: Real persistence to DB/Config would be added here)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
