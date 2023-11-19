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
}