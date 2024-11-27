using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pagina1.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaseadoresPage : ContentPage
    {
        public PaseadoresPage()
        {
            InitializeComponent();
        }
        private void OnVerMisPerrosClick(object sender, EventArgs e)
        {
            // Navegar a la lista de perros asignados
            // Navigation.PushAsync(new ListaPerrosAsignadosPage());
        }

        private void OnVerSolicitudesClick(object sender, EventArgs e)
        {
            // Navegar a solicitudes de paseo
            // Navigation.PushAsync(new SolicitudesPaseoPage());
        }
    }
}