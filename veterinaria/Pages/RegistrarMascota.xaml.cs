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
            _apiService = new ApiService("https://localhost:7122");
        }

        private async void OnRegistrarMascotaClicked(object sender, EventArgs e)
        {
            try
            {
                
                var userId = await SecureStorage.GetAsync("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    await DisplayAlert("Error", "No se pudo obtener el usuario autenticado. Por favor, inicia sesión nuevamente.", "OK");
                    return;
                }

                
                if (string.IsNullOrEmpty(NombreMascotaEntry.Text) ||
                    string.IsNullOrEmpty(TipoAnimalEntry.Text) ||
                    string.IsNullOrEmpty(RazaAnimalEntry.Text))
                {
                    await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                    return;
                }

                
                DateTime fechaNacimiento = FechaNacimientoPicker.Date;
                int edadMascota = CalcularEdad(fechaNacimiento);

                
                if (edadMascota < 0)
                {
                    await DisplayAlert("Error", "La fecha de nacimiento no puede ser en el futuro.", "OK");
                    return;
                }

                
                var mascota = new RegistrarMascotaDTO
                {
                    nombre_mascota = NombreMascotaEntry.Text,
                    fecha_nacimiento = fechaNacimiento,
                    tipo_animal = TipoAnimalEntry.Text,
                    raza_animal = RazaAnimalEntry.Text,
                    usuarios_dueno_idusuarios = int.Parse(userId),
                    edad_mascota = edadMascota 
                };

                
                bool success = await _apiService.RegistrarMascotaAsync(mascota);

                if (success)
                {
                    await DisplayAlert("Éxito", "Mascota registrada correctamente.", "OK");
                    await Navigation.PopAsync(); 
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

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - fechaNacimiento.Year;

            
            if (fechaNacimiento > fechaActual.AddYears(-edad))
            {
                edad--;
            }

            return edad;
        }

        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); 
        }
    }
}
