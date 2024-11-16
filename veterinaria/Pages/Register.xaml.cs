using System.Xml.Linq;
using veterinaria.Models;
using veterinaria.Pages;
namespace veterinaria.Pages;

public partial class Register : ContentPage
{
    private readonly ApiService _apiService;

    public Register()
	{
		InitializeComponent();
        _apiService = new ApiService("http://localhost:7122"); 

    }
    private void GoToPage(ContentPage page)
    {
        this.Navigation.PushAsync(page);
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var usuario = new Usuario
        {
            nombre_usuario = nombreEntry.Text,
            correo_ususario = emailEntry.Text,  
            telefono_usuario = telefonoEntry.Text,
            direccion_usuario = direccionEntry.Text,
            contraseña_usuario = passwordEntry.Text,
            Roles_idroles = 2  // Puedes definir un rol por defecto, como cliente
        };

        bool isRegistered = await _apiService.RegisterAsync(usuario);
        if (isRegistered)
        {
            await DisplayAlert("Éxito", "Usuario registrado exitosamente", "OK");
            // Navegar a la página de login
            await Navigation.PushAsync(new Login());
        }
        else
        {
            await DisplayAlert("Error", "No se pudo registrar el usuario", "OK");
        }
    }

    private void Gotologin(object sender, EventArgs e)
    {
        GoToPage(new Pages.Login());
    }
}