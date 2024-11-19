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
            var token = await SecureStorage.GetAsync("jwt_token"); // Recupera el token guardado
            if (string.IsNullOrWhiteSpace(token))
            {
                await DisplayAlert("Error", "No se encontró el token. Por favor, inicia sesión nuevamente.", "OK");
                return;
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("https://379a-2800-e2-c180-12c-55b5-839a-1ecd-16f9.ngrok-free.app/api/Usuarios/me"); // Reemplaza con tu URL

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var userData = JsonConvert.DeserializeObject<dynamic>(content);

                    // Actualiza la interfaz con los datos del usuario
                    NombreUsuarioLabel.Text = userData.nombre_usuario;
                    CorreoUsuarioLabel.Text = userData.correo_ususario;
                }
                else
                {
                    await DisplayAlert("Error", "No se pudieron cargar los datos del usuario.", "OK");
                }
            }
        }
        // Navegar a RegistrarMascota
        private async void GotoRegistrarMascota(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrarMascota());
        }

        // Placeholder para Agendar Citas
        private async void OnAgendarCitasTapped(object sender, EventArgs e)
        {
            await DisplayAlert("Agendar Citas", "Funcionalidad pendiente de implementar.", "OK");
        }

        // Placeholder para Estilo de Servicios
        private async void OnStyleServiceTapped(object sender, EventArgs e)
        {
            await DisplayAlert("Estilo de Servicios", "Funcionalidad pendiente de implementar.", "OK");
        }

        // Placeholder para Historial de Mascotas
        private async void OnHistoryPetsTapped(object sender, EventArgs e)
        {
            await DisplayAlert("Historial de Mascotas", "Funcionalidad pendiente de implementar.", "OK");
        }
    }
}
