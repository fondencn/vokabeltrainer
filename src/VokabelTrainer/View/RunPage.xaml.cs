using Microsoft.Maui.Platform;
using VokabelTrainer.ViewModel;

namespace VokabelTrainer.View;

public partial class RunPage : ContentPage
{
	public RunPage()
	{
		InitializeComponent();
        this.BindingContextChanged += RunPage_BindingContextChanged;
	}

    private void RunPage_BindingContextChanged(object sender, EventArgs e)
    {
        if(this.BindingContext != null)
        {
            ((RunPageViewModel)BindingContext).GuessChanged += ViewModel_GuessChanged;
        }
    }

    private void ViewModel_GuessChanged(object sender, bool isGuessCorrect)
    {
        if(isGuessCorrect)
        {
            ShowAnimation(this.correctImage);
        } 
        else
        {
            ShowAnimation(this.incorrectImage);
        }
    }

    private async void ShowAnimation(VisualElement control)
    {
        await Task.WhenAll(
            control.FadeTo(1.0, 500, Easing.Default),
            control.RotateTo(control.Rotation + 3 * 360, 1250)
        );

        await Task.Delay(1000);
        await control.FadeTo(0.0, 500, Easing.CubicInOut);
#if ANDROID
        if(control == this.incorrectImage )
        {
            Platform.CurrentActivity.ShowKeyboard(Platform.CurrentActivity.CurrentFocus);
        }
#endif
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