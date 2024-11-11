namespace veterinaria.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}
    private void GoToPage(ContentPage page)
    {
        this.Navigation.PushAsync(page);

    }

    private void Gotoregister(object sender, EventArgs e)
    {
        GoToPage(new Pages.Register());
    }

    private void Gotologin(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage(); // MainPage es tu página con FlyoutPage
    }


}