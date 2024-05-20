using Pagina1.Modelo;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Pagina1.VistaModelo
{
    public class PetProfileViewModel : BaseViewModel
    {
        private ImageSource _petPhotoSource;
        private string _petName;
        private string _petBreed;
        private string _petAgeAndWeight;
        private string _ownerName;
        private string _petAge;
        private string _petWeight;
        private bool _isAddingPet;

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

        public string PetBreed 
        {
            get => _petBreed;
            set => SetProperty(ref _petBreed, value);
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

        public bool IsAddingPet
        {
            get => _isAddingPet;
            set => SetProperty(ref _isAddingPet, value);
        }

        private void UpdatePetAgeAndWeight()
        {
            if (!string.IsNullOrEmpty(PetAge) && !string.IsNullOrEmpty(PetWeight))
            {
                PetAgeAndWeight = $"{PetAge} años, {PetWeight} kg, {PetBreed} raza";
            }
        }

        public ObservableCollection<PetProfile> Pets { get; set; }
        public Command AddPetCommand { get; }
        public Command SavePetCommand { get; }

        public PetProfileViewModel()
        {
            Pets = new ObservableCollection<PetProfile>();
            AddPetCommand = new Command(OnAddPet);
            SavePetCommand = new Command(OnSavePet);
        }

        private void OnAddPet()
        {
            IsAddingPet = true;
        }

        private void OnSavePet()
        {
            PetName = string.Empty;
            PetBreed = string.Empty;
            PetAge = string.Empty;
            PetWeight = string.Empty;
            PetPhotoSource = null;
            OnPropertyChanged(nameof(PetName));
            OnPropertyChanged(nameof(PetBreed));
            OnPropertyChanged(nameof(PetAge));
            OnPropertyChanged(nameof(PetWeight));
            OnPropertyChanged(nameof(PetPhotoSource));
        }
    }
}
