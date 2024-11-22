using veterinaria.Models;
using veterinaria.Services;

namespace veterinaria.Pages;

public partial class Login : ContentPage
{
    private readonly ApiService _apiService;

    public Login()
    {
        InitializeComponent();
        _apiService = new ApiService("https://0d75-2800-e2-c180-12c-c80a-9149-df28-ef48.ngrok-free.app");
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
            // Validar que los campos de entrada no estén vacíos
            if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, ingresa tu correo y contraseña.", "OK");
                return;
            }

            // Crear el DTO para el inicio de sesión
            var loginDTO = new UsuarioLoginDTO
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            };

            // Llamar al servicio de inicio de sesión
            var usuario = await _apiService.LoginAsync(loginDTO);

            if (usuario != null)
            {
                // Almacenar el token JWT en SecureStorage para futuras solicitudes
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

                // Opcional: Guardar el ID del usuario en SecureStorage
                await SecureStorage.SetAsync("userId", usuario.idusuarios.ToString());

                // Mostrar mensaje de bienvenida
                await DisplayAlert("Éxito", $"Bienvenido {usuario.nombre_usuario}", "OK");

                // Navegar a la página principal de la aplicación
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                // Mostrar mensaje de error si las credenciales son incorrectas
                await DisplayAlert("Error", "Credenciales incorrectas o problema con el servidor.", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción inesperada
            Console.WriteLine($"Error durante el inicio de sesión: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Detalles del error interno: {ex.InnerException.Message}");
            }

            await DisplayAlert("Error", "Ocurrió un problema al intentar iniciar sesión. Intente nuevamente.", "OK");
        }
    }
}
