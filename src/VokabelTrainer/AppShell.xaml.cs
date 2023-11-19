using VokabelTrainer.Services;

namespace VokabelTrainer;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

    private void Shell_Loaded(object sender, EventArgs e)
    {
        bool isDarkTheme = CommonServices.Instance.Settings.Load().IsDarkTheme;
         App.Current.UserAppTheme = isDarkTheme ?  AppTheme.Dark : AppTheme.Light;
    }
}
