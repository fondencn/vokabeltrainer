using VokabelTrainer.ViewModel;

namespace VokabelTrainer.View;

public partial class EditWordDatabasePage : ContentPage
{
	public EditWordDatabasePage()
	{
		InitializeComponent();
	}

    private void editWordDatabasePage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		((EditWordDatabaseViewModel)BindingContext).Load();
    }
}