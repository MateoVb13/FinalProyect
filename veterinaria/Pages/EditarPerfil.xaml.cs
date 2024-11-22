using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace veterinaria.Pages
{
    public partial class EditarPerfil : ContentPage
    {
        public EditarPerfil()
        {
            InitializeComponent();
            CargarDatosIniciales();
        }

        private void CargarDatosIniciales()
        {
            // Asume que los datos del usuario están vinculados al contexto
            var usuario = BindingContext as dynamic;
            NombreEntry.Text = usuario?.nombre_usuario ?? "";
            CorreoEntry.Text = usuario?.correo_ususario ?? "";
            DireccionEntry.Text = usuario?.direccion_usuario ?? "";
            TelefonoEntry.Text = usuario?.telefono_usuario ?? "";
            ContrasenaEntry.Text = ""; // No cargar la contraseña actual por seguridad
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            if (string.IsNullOrWhiteSpace(token))
            {
                await DisplayAlert("Error", "Usuario no autenticado.", "OK");
                return;
            }

            var datosActualizados = new
            {
                nombre_usuario = NombreEntry.Text,
                correo_ususario = CorreoEntry.Text,
                direccion_usuario = DireccionEntry.Text,
                telefono_usuario = TelefonoEntry.Text,
                contraseña_usuario = ContrasenaEntry.Text
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonData = JsonConvert.SerializeObject(datosActualizados);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://0d75-2800-e2-c180-12c-c80a-9149-df28-ef48.ngrok-free.app/Usuarios/Actualizar", content);
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Datos actualizados correctamente.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"No se pudieron actualizar los datos. Detalles: {errorContent}", "OK");
                }
            }
        }

        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Regresa a la página anterior
        }
    }
}
