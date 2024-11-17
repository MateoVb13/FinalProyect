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
            _apiService = new ApiService("https://6bc0-2800-e2-c180-12c-608f-5211-ab50-42e7.ngrok-free.app");

        }
        private async void OnRegistrarMascotaClicked(object sender, EventArgs e)
        {
            try
            {
                // Captura los datos del formulario
                var mascota = new RegistrarMascotaDTO
                {
                    nombre_mascota = NombreMascotaEntry.Text,
                    edad_mascota = int.Parse(EdadMascotaEntry.Text),
                    fecha_nacimiento = FechaNacimientoPicker.Date,
                    tipo_animal = TipoAnimalEntry.Text,
                    raza_animal = RazaAnimalEntry.Text,
                    usuarios_dueno_idusuarios = int.Parse(UsuarioIdEntry.Text)
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
