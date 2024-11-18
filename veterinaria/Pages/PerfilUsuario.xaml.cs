using System;
using Microsoft.Maui.Controls;

namespace veterinaria.Pages
{
    public partial class PerfilUsuario : ContentPage
    {
        public PerfilUsuario()
        {
            InitializeComponent();
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
