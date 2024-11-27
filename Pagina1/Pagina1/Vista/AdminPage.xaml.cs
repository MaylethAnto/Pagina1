using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pagina1.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminPage : ContentPage
    {
        private readonly HttpClient _cliente;
        private const string URL_BASE = "http://10.0.2.2:5138/api/Administrador";

        public AdminPage()
        {
            InitializeComponent();
            _cliente = new HttpClient();

            // Configurar el Picker
            SeleccionTipoUsuario.Items.Add("Dueños");
            SeleccionTipoUsuario.Items.Add("Caninos");
            SeleccionTipoUsuario.Items.Add("Paseadores");
            SeleccionTipoUsuario.SelectedIndex = 0; // Seleccionar Dueños por defecto

            SeleccionTipoUsuario.SelectedIndexChanged += OnTipoUsuarioSeleccionado;

            CargarDatosIniciales();
        }

        private async void CargarDatosIniciales()
        {
            try
            {
                var caninos = await ObtenerDatos("caninos");
                var duenos = await ObtenerDatos("dueños");
                var paseadores = await ObtenerDatos("paseadores");

                // Configura los ListViews
                ListaCaninos.ItemsSource = caninos;
                ListaDuenos.ItemsSource = duenos;
                ListaPaseadores.ItemsSource = paseadores;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar los datos: {ex.Message}", "Aceptar");
            }
        }

        private async Task<List<object>> ObtenerDatos(string endpoint)
        {
            try
            {
                var respuesta = await _cliente.GetStringAsync($"{URL_BASE}/{endpoint}");
                return JsonConvert.DeserializeObject<List<object>>(respuesta);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al obtener {endpoint}: {ex.Message}", "Aceptar");
                return new List<object>();
            }
        }

        // Evento para manejar la selección de tipo de usuario
        private void OnTipoUsuarioSeleccionado(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            switch (picker.SelectedIndex)
            {
                case 0: // Dueños
                    StackCaninos.IsVisible = false;
                    StackDuenos.IsVisible = true;
                    StackPaseadores.IsVisible = false;
                    break;
                case 1: // Caninos
                    StackCaninos.IsVisible = true;
                    StackDuenos.IsVisible = false;
                    StackPaseadores.IsVisible = false;
                    break;
                case 2: // Paseadores
                    StackCaninos.IsVisible = false;
                    StackDuenos.IsVisible = false;
                    StackPaseadores.IsVisible = true;
                    break;
            }
        }
    }
}