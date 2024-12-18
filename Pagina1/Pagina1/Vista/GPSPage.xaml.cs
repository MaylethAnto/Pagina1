using Pagina1.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Pagina1.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GPSPage : ContentPage
    {

        public GPSPage()
        {
            InitializeComponent();
            RequestPermissions();
            InitializeMap();

        }

        // metodo para el estado de permisos de la api 
        private async void RequestPermissions()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
        }

        // metodo para inicializar el mapa en la carolina
        private void InitializeMap()
        {
            var laCarolina = new Position(-0.182884, -78.484499);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(laCarolina, Distance.FromKilometers(1)));

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = laCarolina,
                Label = "La Carolina",
                Address = "Quito, Ecuador"
            };
            map.Pins.Add(pin);
        }

        //metodo para el boton de obtener ubicacion
        private void OnSendSmsButtonClicked(object sender, EventArgs e)
        {
            var phoneNumber = "+593959020392"; // Reemplaza con el número del dispositivo GPS
            var message = "777"; // El comando para solicitar la ubicación

            // Llama al método SendSms en MainActivity
            var mainActivity = Xamarin.Forms.DependencyService.Get<IMainActivityService>();
            mainActivity?.SendSms(phoneNumber, message);
        }

        // metodo para obtener la posicion del gps tracker
        public void UpdateMap(double latitude, double longitude)
        {
            var position = new Position(latitude, longitude);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1)));

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "GF-07 Tracker",
                Address = "Current Location"
            };
            map.Pins.Add(pin);
        }
    }
}
