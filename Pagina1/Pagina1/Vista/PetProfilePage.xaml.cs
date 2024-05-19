using Xamarin.Forms;
using Pagina1.Modelo;
using Pagina1.VistaModelo;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;

namespace Pagina1.Vista
{
    public partial class PetProfilePage : ContentPage
    {
        public PetProfilePage()
        {
            InitializeComponent();
            BindingContext = new PetProfileViewModel();
        }

        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            var options = new PickOptions
            {
                PickerTitle = "Select Pet Image"
            };

            FilePickerFileType fileType = null; // Inicializa la variable fileType

            // Crear un nuevo tipo de archivo dependiendo de la plataforma
            if (Device.RuntimePlatform == Device.Android)
            {
                fileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.Android, new[] { "image/png" } }
        });
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                fileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.png" } }
        });
            }

            // Verifica si fileType se inicializó correctamente
            if (fileType != null)
            {
                // Agrega el tipo de archivo a las opciones
                options.FileTypes = fileType;

                // Utiliza las opciones para elegir el archivo
                var file = await FilePicker.PickAsync(options);

                if (file != null)
                {
                    var stream = await file.OpenReadAsync();
                    var viewModel = (PetProfileViewModel)BindingContext;
                    viewModel.PetPhotoSource = ImageSource.FromStream(() => stream);
                }
            }
        }



        public class PetProfileViewModel : BaseViewModel
        {
            private ImageSource _petPhotoSource;
            private string _petName = "Lucky";
            private string _petAgeAndWeight = "5 años, 18 kg";
            private string _ownerName = "Nombre del Dueño";
            private string _petAge;
            private string _petWeight;

            public ImageSource PetPhotoSource
            {
                get => _petPhotoSource;
                set => SetProperty(ref _petPhotoSource, value);
            }

            public string PetName
            {
                get => _petName;
                set => SetProperty(ref _petName, value);
            }

            public string PetAgeAndWeight
            {
                get => _petAgeAndWeight;
                set => SetProperty(ref _petAgeAndWeight, value);
            }

            public string OwnerName
            {
                get => _ownerName;
                set => SetProperty(ref _ownerName, value);
            }

            public string PetAge
            {
                get => _petAge;
                set
                {
                    SetProperty(ref _petAge, value);
                    UpdatePetAgeAndWeight();
                }
            }

            public string PetWeight
            {
                get => _petWeight;
                set
                {
                    SetProperty(ref _petWeight, value);
                    UpdatePetAgeAndWeight();
                }
            }

            private void UpdatePetAgeAndWeight()
            {
                if (!string.IsNullOrEmpty(PetAge) && !string.IsNullOrEmpty(PetWeight))
                {
                    PetAgeAndWeight = $"{PetAge} años, {PetWeight} kg";
                }
            }
        }
    }
}