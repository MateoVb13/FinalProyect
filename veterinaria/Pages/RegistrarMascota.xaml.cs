using veterinaria.Models;
using System;
using veterinaria.Services;

namespace veterinaria.Pages
{
    public partial class RegistrarMascota : ContentPage
    {
        private readonly ApiService _apiService;
        public RegistrarMascota()
        {
            InitializeComponent();
            _apiService = new ApiService("https://0081-2800-e2-c180-12c-608f-5211-ab50-42e7.ngrok-free.app");
        }

        private async void OnRegistrarMascotaClicked(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del usuario autenticado desde SecureStorage
                var userId = await SecureStorage.GetAsync("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    await DisplayAlert("Error", "No se pudo obtener el usuario autenticado. Por favor, inicia sesión nuevamente.", "OK");
                    return;
                }

                // Validar los datos del formulario
                if (string.IsNullOrEmpty(NombreMascotaEntry.Text) ||
                    string.IsNullOrEmpty(EdadMascotaEntry.Text) ||
                    string.IsNullOrEmpty(TipoAnimalEntry.Text) ||
                    string.IsNullOrEmpty(RazaAnimalEntry.Text))
                {
                    await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                    return;
                }

                if (!int.TryParse(EdadMascotaEntry.Text, out int edadMascota))
                {
                    await DisplayAlert("Error", "La edad de la mascota debe ser un número válido.", "OK");
                    return;
                }

                // Captura los datos del formulario
                var mascota = new RegistrarMascotaDTO
                {
                    nombre_mascota = NombreMascotaEntry.Text,
                    edad_mascota = edadMascota,
                    fecha_nacimiento = FechaNacimientoPicker.Date,
                    tipo_animal = TipoAnimalEntry.Text,
                    raza_animal = RazaAnimalEntry.Text,
                    usuarios_dueno_idusuarios = int.Parse(userId) // Asignar el ID del usuario autenticado
                };

                // Llamar al servicio para registrar la mascota
                bool success = await _apiService.RegistrarMascotaAsync(mascota);

                if (success)
                {
                    await DisplayAlert("Éxito", "Mascota registrada correctamente.", "OK");
                    await Navigation.PopAsync(); // Regresa a la pantalla anterior
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al registrar la mascota.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }
    }
}
