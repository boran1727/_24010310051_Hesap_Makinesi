using Microsoft.Extensions.DependencyInjection;

namespace _24010310051_Hesap_Makinesi;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        string savedTheme = Preferences.Get("AppTheme", "System");
        ApplyTheme(savedTheme);

        MainPage = new AppShell();
    }

    public static void ApplyTheme(string theme)
    {
        switch (theme)
        {
            case "Light":
                Current.UserAppTheme = AppTheme.Light;
                break;
            case "Dark":
                Current.UserAppTheme = AppTheme.Dark;
                break;
            default:
                Current.UserAppTheme = AppTheme.Unspecified;
                break;
        }
    }
}