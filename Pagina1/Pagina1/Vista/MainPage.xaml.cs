using Pagina1.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pagina1.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;

        public MainPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            //CargarDatosCanino();
            
        }

       /* private async void CargarDatosCanino() 
        {
            try
            {
                //consumimos el api para obtener el id del canino
                var canino = await ApiService.GetCaninos(1);
                if (canino != null)
                {
                    //asignamos los datos a los controles
                    NombreCanino.Text = canino.Nombre;
                    EdadCanino.Text = $"Edad: {canino.Edad}";
                    RazaCanino.Text = canino.Raza;
                    PesoCanino.Text = $"Peso: {canino.Peso}";
                    FotoCanino.Source = canino.Foto;
                }
                else
                {
                    await DisplayAlert("Error", "No se encontraros datos del canino.", "OK");

                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un problema: {ex.Message}", "OK");
            }
        }*/

        private async void OnRegistrarMascotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroMascotaPage());
        }

        private async void OnHistorialClinicoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistorialClinicoPage());
        }

        private async void OnGPSClicked(object sender, EventArgs e)
        {
                 
           //navegamos a la interfaz del gps
           await Navigation.PushAsync(new GPSPage());
              
            
        }

       

        private async void OnEjerciciosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EjerciciosPage());
        }

        private async void OnPaseadoresClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaseadoresPage());
        }

        private async void OnRecetasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecetasPage());
        }

        private async void OnAddPetClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroMascotaPage());
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool logout = await DisplayAlert("Cerrar Sesión", "¿Está seguro que desea cerrar sesión?", "Sí", "No");
            if (logout)
            {
                await Navigation.PopToRootAsync();
            }
        }
    }
}