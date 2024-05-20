using Pagina1.Modelo;
using Pagina1.Controlador;
using System;
using Xamarin.Forms;
using System.IO;

namespace Pagina1.Vista
{
    public partial class RegisterPage : ContentPage
    {
        private UserProfileController userProfileController;
        private bool isPasswordHidden = true;
        private bool isConfirmPasswordHidden = true;

        public RegisterPage()
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserProfile.db3");
            userProfileController = new UserProfileController(dbPath);
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Obtener valores de los campos de entrada
            string nombre = nombreEntry.Text;
            string correoElectronico = correoEntry.Text;
            string contrasena = contrasenaEntry.Text;
            string confirmarContrasena = confirmarContrasenaEntry.Text;

            // Verificar si las contraseñas coinciden
            if (contrasena != confirmarContrasena)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            // Crear un nuevo perfil de usuario
            var newUserProfile = new UserProfile
            {
                Nombre = nombre,
                CorreoElectronico = correoElectronico,
                Contrasena = contrasena
            };

            // Guardar el nuevo perfil de usuario
            await userProfileController.SaveUserProfileAsync(newUserProfile);
            await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");

            // Navegar a la página de inicio o donde corresponda
            await Navigation.PopAsync();
        }

        private void OnTogglePasswordVisibility(object sender, EventArgs e)
        {
            isPasswordHidden = !isPasswordHidden;
            contrasenaEntry.IsPassword = isPasswordHidden;
        }

        private void OnToggleConfirmPasswordVisibility(object sender, EventArgs e)
        {
            isConfirmPasswordHidden = !isConfirmPasswordHidden;
            confirmarContrasenaEntry.IsPassword = isConfirmPasswordHidden;
        }
    }
}
