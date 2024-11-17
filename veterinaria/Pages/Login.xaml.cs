using veterinaria.Models;


namespace veterinaria.Pages;

public partial class Login : ContentPage
{
    private readonly ApiService _apiService;

    public Login()
	{
		InitializeComponent();
        _apiService = new ApiService("https://7c42-2800-e2-c180-12c-608f-5211-ab50-42e7.ngrok-free.app"); 

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
            await DisplayAlert("�xito", $"Bienvenido {usuario.nombre_usuario}", "OK");
            // Navegar a la p�gina principal de la aplicaci�n
            Application.Current.MainPage = new MainPage();
        }
        else
        {
            await DisplayAlert("Error", "Credenciales incorrectas", "OK");
        }
    }




}