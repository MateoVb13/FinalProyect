using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using veterinaria.Models;
using veterinaria.Services;

namespace veterinaria.Pages
{
    public partial class AgendarCita : ContentPage
    {
        private List<Mascota> mascotas = new List<Mascota>();
        private List<TipoAtencion> tiposAtencion = new List<TipoAtencion>();

        public AgendarCita()
        {
            InitializeComponent();
            CargarMascotas();
            CargarTiposAtencion();
        }

        private async void CargarMascotas()
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            if (string.IsNullOrWhiteSpace(token))
            {
                await DisplayAlert("Error", "Usuario no autenticado.", "OK");
                return;
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("https://0d75-2800-e2-c180-12c-c80a-9149-df28-ef48.ngrok-free.app/api/Mascotas/mis-mascotas");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    mascotas = JsonConvert.DeserializeObject<List<Mascota>>(content);
                    MascotaPicker.ItemsSource = mascotas.Select(m => m.nombre_mascota).ToList();
                    Console.WriteLine(mascotas.First().nombre_mascota);
                }
            }
        }

        private async void CargarTiposAtencion()
        {
            using (var client = new HttpClient())
            {
                var token = await SecureStorage.GetAsync("jwt_token");
                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await client.GetAsync("https://0d75-2800-e2-c180-12c-c80a-9149-df28-ef48.ngrok-free.app/api/TipoAtencion/nombres");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    var nombresTiposAtencion = JsonConvert.DeserializeObject<List<string>>(content);
                    Console.WriteLine(nombresTiposAtencion);
                    TipoServicioPicker.ItemsSource = nombresTiposAtencion;
                }
                else
                {
                    await DisplayAlert("Error", "No se pudieron cargar los tipos de atención.", "OK");
                }
            }
        }


        private async void OnGuardarCitaClicked(object sender, EventArgs e)
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            if (string.IsNullOrWhiteSpace(token))
            {
                await DisplayAlert("Error", "Usuario no autenticado.", "OK");
                return;
            }

            // Validar que las listas no estén vacías
            if (mascotas == null || !mascotas.Any())
            {
                await DisplayAlert("Error", "No hay mascotas disponibles. Registre una mascota primero.", "OK");
                return;
            }

            if (tiposAtencion == null || !tiposAtencion.Any())
            {
                await DisplayAlert("Error", "No hay tipos de atención disponibles. Intente recargar la página.", "OK");
                return;
            }

            // Validar selección de tipo de atención
            if (TipoServicioPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Debe seleccionar un tipo de atención.", "OK");
                return;
            }

            // Validar selección de mascota
            if (MascotaPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Debe seleccionar una mascota.", "OK");
                return;
            }

            // Crear la cita con datos válidos
            var cita = new
            {
                fecha_apartada = FechaPicker.Date,
                hora_inicio = HoraInicioPicker.Time,
                hora_final = HoraFinPicker.Time,
                tipo_atencion_o_servicio_idatencion_o_servicio = tiposAtencion[TipoServicioPicker.SelectedIndex].idatencion_o_servicio,
                mascotas_idmascotas = mascotas[MascotaPicker.SelectedIndex].idmascotas,
                estados_cita_idestados_cita = 1 // 'Pendiente'
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonData = JsonConvert.SerializeObject(cita);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://0d75-2800-e2-c180-12c-c80a-9149-df28-ef48.ngrok-free.app/api/citas", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Cita agendada correctamente.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo agendar la cita.", "OK");
                }
            }
        }


    }
}
