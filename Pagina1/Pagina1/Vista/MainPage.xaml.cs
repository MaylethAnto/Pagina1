using Pagina1.Servicios;
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
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;

        public MainPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

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
            // Navigate to the page for adding a new pet
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