namespace veterinaria.login_registrer;

public partial class login_registro : ContentPage
{
	public login_registro()
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


    private void Gotoregister(object sender, EventArgs e)
    {
        goToPage(new login_registrer.register());
    }
}