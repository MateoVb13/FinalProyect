namespace veterinaria;

public partial class Maestro : ContentPage
{
	public Maestro()
	{
		InitializeComponent();
	}
    private void goToPage(ContentPage page)
    {
        App.FlyoutPage.Detail.Navigation.PushAsync(page);
        App.FlyoutPage.IsPresented = true;  // Cierra el menú Flyout después de la navegación
    }

    private void Gotologin(object sender, EventArgs e)
    {
        goToPage(new login_registrer.Login());
    }

    private void Gotologin_registro(object sender, EventArgs e)
    {
        goToPage(new login_registrer.login_registro());
    }

    private void Gotoregister(object sender, EventArgs e)
    {
        goToPage(new login_registrer.register());
    }
}