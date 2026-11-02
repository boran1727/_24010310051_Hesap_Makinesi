namespace _24010310051_Hesap_Makinesi;
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        Current.FlyoutIsPresented = false;
        await Navigation.PushAsync(new Views.SettingsPage());
    }
}
