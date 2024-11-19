using Pagina1.Modelo;
using Pagina1.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public RegistroMascotaPage()
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

        // Método para manejar el clic en el botón "Guardar"
        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Validación básica de campos
            if (string.IsNullOrEmpty(NombreEntry.Text) || string.IsNullOrEmpty(RazaEntry.Text) || string.IsNullOrEmpty(EdadEntry.Text) || string.IsNullOrEmpty(PesoEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                return;
            }

            // Crear la mascota
            var canino = new Canino
            {
                nombre_canino = NombreEntry.Text,
                edad_canino = int.Parse(EdadEntry.Text),
                raza_canino = RazaEntry.Text,
                peso_canino = Convert.ToDecimal(PesoEntry.Text),  // Asignamos el peso aquí
                foto_canino = File.ReadAllBytes(_fotoPath)  // Asignamos la ruta de la foto aquí

            };

            // Guardar la mascota en la base de datos a través de la API
            var resultado = await _apiService.GuardarMascotaAsync(canino); // Llama al servicio para guardar la mascota

            if (resultado)
            {
                await DisplayAlert("Éxito", "Mascota registrada con éxito.", "OK");
                // Limpiar los campos después de guardar
                NombreEntry.Text = string.Empty;
                RazaEntry.Text = string.Empty;
                EdadEntry.Text = string.Empty;
                PesoEntry.Text = string.Empty;  // Limpiar el campo de peso
                FotoImagen.Source = null;  // Limpiar la foto
            }
            else
            {
                await DisplayAlert("Error", "Hubo un error al guardar la mascota.", "OK");
            }
        }

        // Método para manejar el clic en el botón "Cancelar"
        private void OnCancelarClicked(object sender, EventArgs e)
        {
            // Limpiar los campos
            NombreEntry.Text = string.Empty;
            RazaEntry.Text = string.Empty;
            EdadEntry.Text = string.Empty;
            PesoEntry.Text = string.Empty;  // Limpiar el campo de peso
            FotoImagen.Source = null;  // Limpiar la foto
        }
    }
}
