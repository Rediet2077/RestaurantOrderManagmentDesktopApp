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

        private async void SettingsForm_Load(object sender, EventArgs e)
        {
            UIHelper.SetRoundedRegion(panelMain, 20);
            UIHelper.ApplyModernButton(btnSave, UIHelper.SuccessColor);

            await LoadSettingsAsync();
            UIHelper.ApplyTheme(this);
        }

        private async System.Threading.Tasks.Task LoadSettingsAsync()
        {
            try
            {
                var settings = await ApiClient.GetSettingsAsync();
                if (settings != null)
                {
                    txtRestName.Text = settings.RestaurantName;
                    cmbCurrency.SelectedItem = settings.Currency;
                    chkDarkMode.Checked = (settings.DarkMode == "True");
                }
            }
            catch (Exception ex) { MessageBox.Show("Load Error: " + ex.Message); }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UIHelper.IsDarkMode = chkDarkMode.Checked;
                UIHelper.RestaurantName = txtRestName.Text;
                UIHelper.Currency = cmbCurrency.SelectedItem?.ToString() ?? "Birr (ETB)";

                bool success = await ApiClient.SaveSettingsAsync(
                    UIHelper.RestaurantName,
                    UIHelper.Currency,
                    UIHelper.IsDarkMode.ToString()
                );

                if (success)
                {
                    // Apply theme to current form
                    UIHelper.ApplyTheme(this);

                    // Find top level form and apply theme
                    Form? parent = this.FindForm();
                    if (parent != null)
                    {
                        UIHelper.ApplyTheme(parent);
                        var method = parent.GetType().GetMethod("ApplyStyles", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                        if (method != null) method.Invoke(parent, null);
                    }

                    UIHelper.ShowToast("Settings saved successfully!");
                }
                else
                {
                    UIHelper.ShowToast("Failed to save settings.", true);
                }
            }
            catch (Exception ex) { MessageBox.Show("Save Error: " + ex.Message); }
        }
    }
}
