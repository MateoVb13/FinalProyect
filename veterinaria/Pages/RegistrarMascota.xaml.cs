
using veterinaria.Models;
using System;

namespace veterinaria.Pages
{
    public partial class RegistrarMascota : ContentPage
    {
        public RegistrarMascota()
        {
            InitializeComponent();

        }

        private async void OnRegistrarMascotaClicked(object sender, EventArgs e)
        {
            // Validaci�n de los campos
            if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
                string.IsNullOrWhiteSpace(TipoAnimalEntry.Text) ||
                string.IsNullOrWhiteSpace(RazaAnimalEntry.Text) ||
                !int.TryParse(EdadEntry.Text, out int edad))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos correctamente.", "OK");
                return;
            }

            // Creaci�n del objeto Mascota con los datos ingresados
            var nuevaMascota = new Mascota
            {
                NombreMascota = NombreEntry.Text,
                EdadMascota = edad,
                FechaNacimiento = FechaNacimientoPicker.Date,
                TipoAnimal = TipoAnimalEntry.Text,
                RazaAnimal = RazaAnimalEntry.Text,
                ClientesIdclientes = 1, // Cambia este ID seg�n la l�gica actual
                UsuariosDuenoIdusuarios = 1 // Cambia este ID seg�n la l�gica actual
            };
        }
    }
}
