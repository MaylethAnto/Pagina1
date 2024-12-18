using Pagina1.Modelo;
using Pagina1.Servicios;
using System;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pagina1.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroMascotaPage : ContentPage
    {
        private readonly ApiService _apiService;
        private string _fotoPath;

        public RegistroMascotaPage()
        {
            InitializeComponent();
            _apiService = new ApiService();

            // Generar una lista de edades de 1 a 25 años
            var edades = Enumerable.Range(1, 25).ToList();
            EdadPicker.ItemsSource = edades;
        }

        // Evento cuando se selecciona una edad del Picker
        private void OnEdadPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (EdadPicker.SelectedIndex != -1)
            {
                // Obtener la edad seleccionada
                int edadSeleccionada = (int)EdadPicker.SelectedItem;
                // Realiza lo que necesites con la edad seleccionada, por ejemplo:
                DisplayAlert("Edad Seleccionada", $"Has seleccionado {edadSeleccionada} años.", "OK");
            }
        }

        // Seleccionar foto
        private async void OnSeleccionarFotoClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                _fotoPath = result.FullPath;
                FotoImagen.Source = ImageSource.FromFile(_fotoPath);
            }
        }

        // Guardar mascota
        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            string cedula = CedulaDuenoEntry.Text.Trim();
            var dueno = await _apiService.ObtenerDuenoPorCedulaAsync(cedula);

            if (dueno == null)
            {
                await DisplayAlert("Error", "No se encontró un dueño con la cédula proporcionada.", "OK");
                return;
            }

            var nuevaMascota = new Canino
            {
                NombreCanino = NombreEntry.Text.Trim(),
                EdadCanino = (int)EdadPicker.SelectedItem,
                RazaCanino = RazaPicker.SelectedItem.ToString(),
                PesoCanino = decimal.Parse(PesoEntry.Text),
                FotoCanino = !string.IsNullOrEmpty(_fotoPath) ? File.ReadAllBytes(_fotoPath) : null,
                CedulaDueno = dueno.CedulaDueno // Asociamos la cédula del dueño encontrado
            };

            bool resultado = await _apiService.GuardarMascotaAsync(nuevaMascota);

            if (resultado)
            {
                await DisplayAlert("Éxito", "Mascota registrada correctamente.", "OK");
                LimpiarFormulario();

                //dirigir al MainPAge

                App.Current.MainPage = new MainPage();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo registrar la mascota.", "OK");
            }
        }

        // Validación de campos
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(CedulaDuenoEntry.Text) ||
                string.IsNullOrWhiteSpace(NombreEntry.Text) ||
                EdadPicker.SelectedIndex == -1 ||
                RazaPicker.SelectedItem == null ||
                string.IsNullOrWhiteSpace(PesoEntry.Text))
            {
                DisplayAlert("Error", "Por favor completa todos los campos.", "OK");
                return false;
            }

            if (!int.TryParse(PesoEntry.Text, out _))
            {
                DisplayAlert("Error", "Edad y peso deben ser valores numéricos.", "OK");
                return false;
            }

            return true;
        }

        // Limpiar formulario
        private void LimpiarFormulario()
        {
            CedulaDuenoEntry.Text = string.Empty;
            NombreEntry.Text = string.Empty;
            EdadPicker.SelectedIndex = -1;
            RazaPicker.SelectedItem = null;
            PesoEntry.Text = string.Empty;
            FotoImagen.Source = null;
            _fotoPath = null;
        }

        // Cancelar
        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            // Mostrar una alerta de confirmación
            var respuesta = await DisplayAlert(
                "Confirmación",
                "¿Deseas salir de la sesión o dirigirte a otra interfaz?",
                "Salir de sesión",
                "Otra interfaz");

            if (respuesta)
            {
                // Lógica para salir de la sesión (puedes redirigir o cerrar la sesión aquí)
                await DisplayAlert("Sesión", "Has salido de la sesión", "OK");
                // Aquí podrías agregar código para cerrar la sesión o navegar a una página de inicio de sesión
            }
            else
            {
                // Lógica para redirigir a otra interfaz
                await DisplayAlert("Interfaz", "Te dirigirás a otra interfaz", "OK");
                // Aquí puedes navegar a otra interfaz, por ejemplo:
                await Navigation.PushAsync(new MainPage());
            }
        }

    }
}
