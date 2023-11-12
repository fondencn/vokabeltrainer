using VokabelTrainer.Model.Api;
using VokabelTrainer.Services;
using VokabelTrainer.View;

namespace VokabelTrainer;

public partial class MainPage : ContentPage
{	public MainPage()
	{
		InitializeComponent();
	}


    private void UpdateHelloMessage()
    {
        string username = CommonServices.Instance.Settings.Load().Username;
        this.lblHello.Text = String.IsNullOrEmpty(username) ? "Hello, unknown user, please switch to settings and enter your username." : "Hello, " + username + "!";
    }

    private async void ButtonStartTraining_Clicked(object sender, EventArgs e)
    {
        await CommonServices.Instance.Navigation.Navigate<StartNewLessonPage>();
    }

    private async void ButtonSettings_Clicked(object sender, EventArgs e)
    {
        await  CommonServices.Instance.Navigation.Navigate<SettingsPage>();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        UpdateHelloMessage();
    }
}

