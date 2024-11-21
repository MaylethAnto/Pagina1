using Pagina1.Dtos;
using Pagina1.Servicios;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pagina1.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly AuthService _authService;

        public RegisterPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var cedula = CedulaEntry.Text;
            var nombre = NombreEntry.Text;
            var correo = CorreoEntry.Text;
            var contrasena = ContraseñaEntry.Text;
            var confirmarContrasena = ConfirmarContraseñaEntry.Text;
            var rolSeleccionado = RolPicker.SelectedItem?.ToString();

            // Validación simple
            if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena) ||
                string.IsNullOrEmpty(confirmarContrasena) || string.IsNullOrEmpty(rolSeleccionado))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
                return;
            }

            if (contrasena != confirmarContrasena)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            var registerDto = new RegisterDto
            {
                Cedula = cedula,
                Nombre = nombre,
                Correo = correo,
                Contrasena = contrasena,
                Rol = rolSeleccionado
            };

            var result = await _authService.Register(registerDto);
            if (result != null)
            {
                await DisplayAlert("Éxito", "¡Registro exitoso!", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", "Hubo un problema al registrar el usuario", "OK");
            }
        }
    }
}