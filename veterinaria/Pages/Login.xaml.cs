using veterinaria.Models;
using veterinaria.Services;


namespace veterinaria.Pages;

public partial class Login : ContentPage
{
    private readonly ApiService _apiService;

    public Login()
	{
		InitializeComponent();
        _apiService = new ApiService("https://6bc0-2800-e2-c180-12c-608f-5211-ab50-42e7.ngrok-free.app"); 

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
            Application.Current.MainPage = new MainPage();
        }
        else
        {
            await DisplayAlert("Error", "Credenciales incorrectas", "OK");
        }
    }




}