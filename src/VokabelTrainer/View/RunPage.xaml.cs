using Microsoft.Maui.Platform;
using VokabelTrainer.ViewModel;

namespace VokabelTrainer.View;

public partial class RunPage : ContentPage
{
	public RunPage()
	{
		InitializeComponent();
	}

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
		((RunPageViewModel)BindingContext).EndRun();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
#if ANDROID

	Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
    }
}