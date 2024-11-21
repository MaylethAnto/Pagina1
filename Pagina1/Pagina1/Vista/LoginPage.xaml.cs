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
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            string role = rolePicker.SelectedItem.ToString();

            // Validaciones de entrada
            if (usernameEntry == null || passwordEntry == null)
            {
                await DisplayAlert("Error", "No se encuentra registrado", "OK");
                return;
            }

            if (string.IsNullOrEmpty(usernameEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor ingrese usuario y contraseña", "OK");
                return;
            }

            if (!IsValidEmail(usernameEntry.Text))
            {
                await DisplayAlert("Error", "Por favor ingrese un email válido", "OK");
                return;
            }

            // Aquí llamamos a la API para verificar las credenciales
            var result = await LoginService.Login(username, password, role);

            if (result.IsSuccessful)
            {
                // Aquí se navega según el rol
                if (role == "Administrador")
                {
                    await Navigation.PushAsync(new AdminPage());
                }
                else if (role == "Dueño")
                {
                    await Navigation.PushAsync(new DuenoPage());
                }
                else if (role == "Paseador")
                {
                    await Navigation.PushAsync(new PaseadorPage());
                }
            }
            else
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
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