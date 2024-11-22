using System;
using System.Net.Http.Headers;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace veterinaria.Pages
{
    public partial class PerfilUsuario : ContentPage
    {
        public PerfilUsuario()
        {
            InitializeComponent();
            LoadUserData();
        }

        private async void LoadUserData()
        {
            var token = await SecureStorage.GetAsync("jwt_token"); 
            if (string.IsNullOrWhiteSpace(token))
            {
                await DisplayAlert("Error", "No se encontró el token. Por favor, inicia sesión nuevamente.", "OK");
                return;
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
<<<<<<< HEAD
                var response = await client.GetAsync("https://localhost:7122/api/Usuarios/me"); 
=======
                var response = await client.GetAsync("https://0d75-2800-e2-c180-12c-c80a-9149-df28-ef48.ngrok-free.app/api/Usuarios/me");
>>>>>>> dc8d39abb04c5befaf53ec02e3d7c4a7320bf13b

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var userData = JsonConvert.DeserializeObject<dynamic>(content);

                    
                    NombreUsuarioLabel.Text = userData.nombre_usuario;
                    CorreoUsuarioLabel.Text = userData.correo_ususario;
                }
                else
                {
                    await DisplayAlert("Error", "No se pudieron cargar los datos del usuario.", "OK");
                }
            }
        }
       
        private async void GotoRegistrarMascota(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrarMascota());
        }

<<<<<<< HEAD
       
        private async void OnAgendarCitasTapped(object sender, EventArgs e)
=======
        // Placeholder para Agendar Citas
        private async void GotoAgendarCita(object sender, EventArgs e)
>>>>>>> dc8d39abb04c5befaf53ec02e3d7c4a7320bf13b
        {
            await Navigation.PushAsync(new AgendarCita());
        }

        
        private async void OnStyleServiceTapped(object sender, EventArgs e)
        {
            await DisplayAlert("Estilo de Servicios", "Funcionalidad pendiente de implementar.", "OK");
        }

      
        private async void OnHistoryPetsTapped(object sender, EventArgs e)
        {
            await DisplayAlert("Historial de Mascotas", "Funcionalidad pendiente de implementar.", "OK");
        }
        private async void GotoEditarPerfil(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarPerfil());
        }
    }
}
