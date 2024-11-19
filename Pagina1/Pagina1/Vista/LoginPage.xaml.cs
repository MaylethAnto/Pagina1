using System;
using Xamarin.Forms;
using System.Net.Mail;
using Pagina1.Modelo; // Asegúrate de tener el namespace correcto para tus páginas
using Pagina1.Vista;
using Pagina1.Servicios;
using Xamarin.Essentials;


namespace Pagina1.Vista
{
    public partial class LoginPage : ContentPage
    {
        private AuthService _authService;

        public LoginPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Usar FindByName para obtener referencias a los Entry
            var emailEntry = this.FindByName<Entry>("EmailEntry");
            var passwordEntry = this.FindByName<Entry>("PasswordEntry");

            // Validaciones de entrada
            if (emailEntry == null || passwordEntry == null)
            {
                await DisplayAlert("Error", "No se encontraron los campos de entrada", "OK");
                return;
            }

            if (string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            if (!IsValidEmail(emailEntry.Text))
            {
                await DisplayAlert("Error", "Por favor ingrese un email válido", "OK");
                return;
            }

            // Intento de login
            bool resultado = await _authService.Login(emailEntry.Text, passwordEntry.Text);

            if (resultado)
            {
                var tipoPerfil = Preferences.Get("TipoPerfil", "");

                switch (tipoPerfil)
                {
                    case "ADMINISTRADOR":
                        await Navigation.PushAsync(new AdminPage());
                        break;
                    case "DUEÑO":
                        await Navigation.PushAsync(new RegistroDuenoPage());
                        break;
                    case "PASEADOR":
                        await Navigation.PushAsync(new PaseadoresPage());
                        break;
                    default:
                        await DisplayAlert("Error", "Perfil no reconocido", "OK");
                        break;
                }
            }
            else
            {
                await DisplayAlert("Error", "Credenciales inválidas", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}