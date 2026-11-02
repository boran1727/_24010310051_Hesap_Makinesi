namespace _24010310051_Hesap_Makinesi.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        string currentTheme = Preferences.Get("AppTheme", "System");
        if (currentTheme == "Light") LightRadio.IsChecked = true;
        else if (currentTheme == "Dark") DarkRadio.IsChecked = true;
        else SystemRadio.IsChecked = true;
    }

    private void OnThemeChanged(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value) return;

        string selectedTheme = "System";
        if (sender == LightRadio) selectedTheme = "Light";
        else if (sender == DarkRadio) selectedTheme = "Dark";

        Preferences.Set("AppTheme", selectedTheme);
        App.ApplyTheme(selectedTheme);
    }
}