namespace veterinaria.login_registrer;

public partial class register : ContentPage
{
	public register()
	{
		InitializeComponent();
	}

    private void goToPage(ContentPage page)
    {
        this.Navigation.PushAsync(page);
    }

    private void Gotologin(object sender, EventArgs e)
    {
        goToPage(new login_registrer.Login());
    }

}