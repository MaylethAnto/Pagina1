using Pagina1.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<CaninoConDuenoDTO> Caninos { get; set; }

        public PaseadoresPage()
        {
            InitializeComponent();
            Caninos = new ObservableCollection<CaninoConDuenoDTO>();
            CaninosListView.ItemsSource = Caninos;
            CargarCaninos();
        }

        private async void CargarCaninos()
        {
            /*var caninos = await ApiService.ObtenerCaninosConDuenosAsync();
            foreach (var canino in caninos)
            {
                Caninos.Add((CaninoConDuenoDTO)canino);
            }*/
        }

        private async void OnCaninoSeleccionado(object sender, SelectedItemChangedEventArgs e)
        {
           // if (e.SelectedItem is CaninoConDuenoDTO caninoSeleccionado)
            {
                // Aquí puedes registrar el ID del canino en la base de datos
              //  await DisplayAlert("Canino Seleccionado", $"Seleccionaste a {caninoSeleccionado.NombreCanino}", "OK");
                // Opcional: actualiza el paseador con el canino seleccionado
            }
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