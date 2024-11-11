namespace veterinaria.Pages;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}
    private void GoToPage(ContentPage page)
    {
        this.Navigation.PushAsync(page);
    }

    private void Gotologin(object sender, EventArgs e)
    {
        GoToPage(new Pages.Login());
    }
}