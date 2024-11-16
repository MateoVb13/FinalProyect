using veterinaria.Models;


namespace veterinaria.Pages;

public partial class Login : ContentPage
{
    private readonly ApiService _apiService;

    public Login()
	{
		InitializeComponent();
        _apiService = new ApiService("https://localhost:7122"); 

    }
    private void GoToPage(ContentPage page)
    {
        this.Navigation.PushAsync(page);

    }

    private void Gotoregister(object sender, EventArgs e)
    {
        GoToPage(new Pages.Register());
    }


    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginDTO = new UsuarioLoginDTO
        {
            Email = emailEntry.Text,
            Password = passwordEntry.Text
        };

        var usuario = await _apiService.LoginAsync(loginDTO);
        if (usuario != null)
        {
            await DisplayAlert("Éxito", $"Bienvenido {usuario.nombre_usuario}", "OK");
            // Navegar a la página principal de la aplicación
            await Navigation.PushAsync(new NavigationPage(new Pages.LoginRegister()));
        }
        else
        {
            await DisplayAlert("Error", "Credenciales incorrectas", "OK");
        }
    }




}