using VokabelTrainer.ViewModel;

namespace VokabelTrainer.View;

public partial class ViewStatisticsPage : ContentPage
{
	public ViewStatisticsPage()
	{
		InitializeComponent();
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		((ViewStatisticsViewModel)this.BindingContext).Load();
    }
}