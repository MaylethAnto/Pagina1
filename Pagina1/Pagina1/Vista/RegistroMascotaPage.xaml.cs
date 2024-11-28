using Pagina1.Modelo;
using Pagina1.Servicios;
using System;
using System.IO;
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
                EdadCanino = int.Parse(EdadEntry.Text),
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
                string.IsNullOrWhiteSpace(EdadEntry.Text) ||
                RazaPicker.SelectedItem == null ||
                string.IsNullOrWhiteSpace(PesoEntry.Text))
            {
                DisplayAlert("Error", "Por favor completa todos los campos.", "OK");
                return false;
            }

            if (!int.TryParse(EdadEntry.Text, out _) || !decimal.TryParse(PesoEntry.Text, out _))
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
            EdadEntry.Text = string.Empty;
            RazaPicker.SelectedItem = null;
            PesoEntry.Text = string.Empty;
            FotoImagen.Source = null;
            _fotoPath = null;
        }

        // Cancelar
        private void OnCancelarClicked(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
    }
}
