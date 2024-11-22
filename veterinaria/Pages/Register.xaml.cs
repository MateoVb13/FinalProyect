using System.Xml.Linq;
using veterinaria.Models;
using veterinaria.Services;
namespace veterinaria.Pages;

public partial class Register : ContentPage
{
    private readonly ApiService _apiService;

    public Register()
	{
		InitializeComponent();
        _apiService = new ApiService("https://localhost:7122");

    }
    private void GoToPage(ContentPage page)
    {
        this.Navigation.PushAsync(page);
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        try
        {
            var usuario = new Usuario
            {
                nombre_usuario = nombreEntry.Text,
                correo_ususario = emailEntry.Text,
                telefono_usuario = telefonoEntry.Text,
                direccion_usuario = direccionEntry.Text,
                contraseña_usuario = passwordEntry.Text,
                Roles_idroles = 2  
            };

            bool isRegistered = await _apiService.RegisterAsync(usuario);
            if (isRegistered)
            {
                await DisplayAlert("Éxito", "Usuario registrado exitosamente", "OK");
                
                await Navigation.PushAsync(new Login());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo registrar el usuario", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }





    private void Gotologin(object sender, EventArgs e)
    {
        GoToPage(new Pages.Login());
    }
}