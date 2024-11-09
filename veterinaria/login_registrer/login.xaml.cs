namespace veterinaria.login_registrer;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}
    private void goToPage(ContentPage page)
    {
        this.Navigation.PushAsync(page);

    }

    private void Gotoregister(object sender, EventArgs e)
    {
        goToPage(new login_registrer.register());
    }

    private void Gotologin(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage(); // MainPage es tu página con FlyoutPage
    }
}