namespace veterinaria.Pages;

public partial class LoginRegister : ContentPage
{
	public LoginRegister()
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


    private void Gotoregister(object sender, EventArgs e)
    {
        GoToPage(new Pages.Register());
    }

}