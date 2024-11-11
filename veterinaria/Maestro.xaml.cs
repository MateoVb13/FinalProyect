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
        goToPage(new Pages.Login());
    }

    private void Gotologin_registro(object sender, EventArgs e)
    {
        goToPage(new Pages.LoginRegister());
    }

    private void Gotoregister(object sender, EventArgs e)
    {
        goToPage(new Pages.Register());
    }

    private void GotoPerfilUsuario(object sender, EventArgs e)
    {
        goToPage(new Pages.PerfilUsuario());
    }
}