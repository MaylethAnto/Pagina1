using Pagina1.Controlador;
using System;
using Xamarin.Forms;
using System.IO;

namespace Pagina1.Vista
{
    public partial class LoginPage : ContentPage
    {
        private UserProfileController userProfileController;
        private bool isPasswordHidden = true;

        public LoginPage()
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserProfile.db3");
            userProfileController = new UserProfileController(dbPath);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string password = passwordEntry.Text;

            // Verificar credenciales
            var user = await userProfileController.AuthenticateUserAsync(email, password);

            if (user != null)
            {
                // Credenciales válidas, navegar a la página de perfil
                await Navigation.PushAsync(new PetProfilePage());
            }
            else
            {
                // Credenciales inválidas, mostrar un mensaje de error
                await DisplayAlert("Error", "Correo electrónico o contraseña incorrectos", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Navegar a la página de registro
            await Navigation.PushAsync(new RegisterPage());
        }

        private void OnTogglePasswordVisibility(object sender, EventArgs e)
        {
            isPasswordHidden = !isPasswordHidden;
            passwordEntry.IsPassword = isPasswordHidden;
        }
    }
}
