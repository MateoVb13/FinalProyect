using veterinaria.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;  // Asegúrate de importar el espacio de nombres para colores

namespace veterinaria.Pages
{
    public partial class PerfilUsuario : ContentPage
    {

        public PerfilUsuario()
        {
            InitializeComponent();
        }

     
        // Método para navegar a la página de registro de mascota
        private async void GotoRegistrarMascota(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrarMascota());
        }
    }
}
