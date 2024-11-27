using Pagina1.Modelo;
using Pagina1.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private string _fotoPath;  // Variable para almacenar la ruta de la foto seleccionada

        // Variable para almacenar los datos del dueño
        private Dueno _dueno;

        public RegistroMascotaPage(Dueno dueno = null)
        {
            InitializeComponent();
            _apiService = new ApiService(); // Inicializa tu servicio de API
                                          
        }

        // Método para manejar el clic en el botón "Seleccionar Foto"
        private async void OnSeleccionarFotoClicked(object sender, EventArgs e)
        {
            // Usamos Xamarin.Essentials para seleccionar una foto de la galería
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                // Configura las opciones si es necesario (ej., límites de tamaño, calidad, etc.)
            });

            if (result != null)
            {
                // Obtener la ruta del archivo de imagen
                _fotoPath = result.FullPath;

                // Mostrar la imagen en la interfaz de usuario
                FotoImagen.Source = ImageSource.FromFile(_fotoPath);
            }
        }

        // Evento para manejar el clic en el botón "Guardar"

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Validación de los campos
                if (!ValidarCampos())
                {
                    return;
                }

                // Obtenemos la cédula ingresada en el Entry
                var cedulaDueno = CedulaEntry.Text.Trim();

                // Validar que la cédula no esté vacía
                if (string.IsNullOrEmpty(cedulaDueno))
                {
                    await DisplayAlert("Error", "Por favor ingresa la cédula del dueño", "OK");
                    return;
                }

                // Obtener los datos del dueño
                var dueno = await _apiService.ObtenerDuenoPorCedulaAsync(cedulaDueno);

                // Verificar que el dueño existe
                if (dueno == null)
                {
                    await DisplayAlert("Error", "No se pudo obtener la información del dueño. Verifica la cédula.", "OK");
                    return;
                }

                // Crear el objeto de la mascota
                var nuevaMascota = new Canino
                {
                    nombre_canino = NombreEntry.Text.Trim(),
                    edad_canino = int.Parse(EdadEntry.Text),
                    raza_canino = RazaPicker.SelectedItem.ToString(),
                    peso_canino = decimal.Parse(PesoEntry.Text),
                    foto_canino = _fotoPath != null ? File.ReadAllBytes(_fotoPath) : null,
                    cedula_dueno = cedulaDueno,
                    Dueno = dueno // Asignar la información del dueño
                };

                // Llamar al servicio para guardar la mascota
                var resultado = await _apiService.GuardarMascotaAsync(nuevaMascota);

                if (resultado)
                {
                    await DisplayAlert("Éxito", "¡Mascota registrada exitosamente!", "OK");
                    LimpiarFormulario();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo registrar la mascota", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error inesperado: {ex.Message}", "OK");
            }
        }


        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
                string.IsNullOrWhiteSpace(EdadEntry.Text) ||
                RazaPicker.SelectedItem == null ||
                string.IsNullOrWhiteSpace(PesoEntry.Text) ||
                string.IsNullOrWhiteSpace(_fotoPath))
            {
                DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
                return false;
            }

            if (!int.TryParse(EdadEntry.Text, out _) || !float.TryParse(PesoEntry.Text, out _))
            {
                DisplayAlert("Error", "Edad y peso deben ser valores numéricos válidos", "OK");
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            NombreEntry.Text = string.Empty;
            EdadEntry.Text = string.Empty;
            RazaPicker.SelectedItem = null;
            PesoEntry.Text = string.Empty;
            FotoImagen.Source = null;
            _fotoPath = null;

        }

        private void OnCancelarClicked(object sender, EventArgs e)
        {

        }
    }
}
