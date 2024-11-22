using veterinaria.Models;
using veterinaria.Services;

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
        try
        {
            
            if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, ingresa tu correo y contraseña.", "OK");
                return;
            }

            
            var loginDTO = new UsuarioLoginDTO
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            };

            
            var usuario = await _apiService.LoginAsync(loginDTO);

            if (usuario != null)
            {
                
                if (!string.IsNullOrEmpty(usuario.Token))
                {
                    await SecureStorage.SetAsync("jwt_token", usuario.Token);
                    Console.WriteLine("Token almacenado en SecureStorage.");
                }
                else
                {
                    await DisplayAlert("Error", "No se recibió un token válido. Intente nuevamente.", "OK");
                    return;
                }

                
                await SecureStorage.SetAsync("userId", usuario.idusuarios.ToString());

                
                await DisplayAlert("Éxito", $"Bienvenido {usuario.nombre_usuario}", "OK");

                
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                
                await DisplayAlert("Error", "Credenciales incorrectas o problema con el servidor.", "OK");
            }
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Error durante el inicio de sesión: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Detalles del error interno: {ex.InnerException.Message}");
            }

            await DisplayAlert("Error", "Ocurrió un problema al intentar iniciar sesión. Intente nuevamente.", "OK");
        }
    }
}
