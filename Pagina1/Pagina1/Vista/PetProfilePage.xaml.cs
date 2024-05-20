using Xamarin.Forms;
using Pagina1.Modelo;
using Pagina1.VistaModelo;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using Pagina1.Controlador;
using System.Threading;
using System.Threading.Tasks;

namespace Pagina1.Vista
{
    public partial class PetProfilePage : ContentPage
    {
        private PetProfileViewModel viewModel;
        private PetProfileController petProfileController;

        public PetProfilePage()
        {
            InitializeComponent();
            viewModel = new PetProfileViewModel();
            BindingContext = viewModel;
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PetProfiles.db3");
            petProfileController = new PetProfileController(dbPath);
        }

        private async void OnSavePetClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(viewModel.PetName) &&
                !string.IsNullOrWhiteSpace(viewModel.PetAge) &&
                !string.IsNullOrWhiteSpace(viewModel.PetWeight) &&
                !string.IsNullOrWhiteSpace(viewModel.PetBreed))
            {
                var newPetProfile = new PetProfile
                {
                    Nombre = viewModel.PetName,
                    Raza = viewModel.PetBreed,
                    Edad = int.Parse(viewModel.PetAge),
                    Peso = float.Parse(viewModel.PetWeight),
                    FotoBytes = null,
                    RecetasGuardadas = new List<string>(),
                    Veterinario = new List<string>(),
                    Alergias = new List<string>(),
                    EnfermedadesConocidas = new List<string>(),
                    HistorialMedico = string.Empty
                };

                if (viewModel.PetPhotoSource != null)
                {
                    newPetProfile.FotoBytes = GetImageBytes(viewModel.PetPhotoSource);
                }

                await petProfileController.SavePetProfileAsync(newPetProfile);

                viewModel.SavePetCommand.Execute(null);
            }
        }

        private void OnProfileClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PetProfilePage());
        }

        private void OnAddPetClicked(object sender, EventArgs e)
        {
            viewModel.IsAddingPet = true;
        }

        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            var options = new PickOptions
            {
                PickerTitle = "Select Pet Image"
            };

            FilePickerFileType fileType = null;

            if (Device.RuntimePlatform == Device.Android)
            {
                fileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "image/png", "image/jpeg" } }
                });
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                fileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.png", "public.jpeg" } }
                });
            }

            if (fileType != null)
            {
                options.FileTypes = fileType;
                var file = await FilePicker.PickAsync(options);

                if (file != null)
                {
                    var stream = await file.OpenReadAsync();
                    viewModel.PetPhotoSource = ImageSource.FromStream(() => stream);
                }
            }
        }

        private byte[] GetImageBytes(ImageSource imageSource)
        {
            if (imageSource is StreamImageSource streamImageSource)
            {
                using (var stream = streamImageSource.Stream(CancellationToken.None).Result)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
            return null;
        }
    }
}
