using System;
using Xamarin.Forms;
using System.Net.Mail;
using Pagina1.Modelo; // Asegúrate de tener el namespace correcto para tus páginas
using Pagina1.Vista;
using Pagina1.Servicios; // Asumo que AuthService está en este namespace
using Xamarin.Essentials;

namespace Pagina1.Vista
{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginService _loginService;
        private readonly AuthService _authService;

        public LoginPage()
        {
            InitializeComponent();
            _loginService = new LoginService();  // Para login
            _authService = new AuthService();    // Para registro
        }

        // Acción cuando se presiona el botón de Login
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string nombreUsuario = usernameEntry.Text;
            string contrasena = passwordEntry.Text;

            // Validación básica de campos
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                statusLabel.Text = "Por favor ingresa usuario y contraseña";
                statusLabel.TextColor = Color.Red;
                return;
            }

            // Llamar al servicio de login
            var result = await _loginService.LoginUsuarioAsync(nombreUsuario, contrasena);

            // Mostrar el resultado (mensaje de éxito o error)
            if (result.Contains("Login exitoso"))
            {
                statusLabel.TextColor = Color.Green;
            }
            else
            {
                statusLabel.TextColor = Color.Red;
            }
            statusLabel.Text = result;
        }

        // Acción cuando se presiona el botón de Registro
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        // Método de validación para un correo electrónico
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
