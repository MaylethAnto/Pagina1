using Pagina1.Modelo;
using Pagina1.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pagina1.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly ApiService _apiService;

        public RegisterPage()
        {
            InitializeComponent();
            _apiService = new ApiService(); // Suponiendo que tienes una clase ApiService
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var cedula = CedulaEntry.Text;
            var nombre = NombreEntry.Text;
            var correo = CorreoEntry.Text;
            var contraseña = ContraseñaEntry.Text;
            var confirmarContraseña = ConfirmarContraseñaEntry.Text;
            var rol = RolPicker.SelectedItem?.ToString();

            // Validación simple
            if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña) ||
                string.IsNullOrEmpty(confirmarContraseña) || string.IsNullOrEmpty(rol))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
                return;
            }

            if (contraseña != confirmarContraseña)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            var usuario = new Usuario
            {
                Cedula = cedula,
                Nombre = nombre,
                Correo = correo,
                Contraseña = contraseña,
                Rol = rol // Asignamos el rol seleccionado
            };

            var result = await _apiService.RegisterUsuarioAsync(usuario);

            if (result != null)
            {
                await DisplayAlert("Éxito", "¡Registro exitoso!", "OK");
                // Redirigir a otra página después del registro
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", "Hubo un problema al registrar el usuario", "OK");
            }
        }
    }


}