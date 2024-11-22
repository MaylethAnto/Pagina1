using Pagina1.Dtos;
using Pagina1.Servicios;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pagina1.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly AuthService _authService;
        private const string MASTER_PASSWORD = "TuClaveSecretaUnica2024$"; 

        public RegisterPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        // Se llama cuando el rol cambia
        private void OnRoleChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            var selectedRole = picker.SelectedItem.ToString();

            // Mostrar/ocultar los campos según el rol seleccionado
            if (selectedRole == "Administrador")
            {
                ClaveMaestraStack.IsVisible = true;
                DireccionCelularStack.IsVisible = false;
            }
            else
            {
                ClaveMaestraStack.IsVisible = false;
                DireccionCelularStack.IsVisible = true;
            }
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var cedula = CedulaEntry.Text?.Trim();
            var nombre = NombreEntry.Text?.Trim();
            var correoBase = CorreoEntry.Text?.Trim();
            var contrasena = ContraseñaEntry.Text;
            var confirmarContrasena = ConfirmarContraseñaEntry.Text;
            var rolSeleccionado = RolPicker.SelectedItem?.ToString();
            var dominioCorreoSeleccionado = CorreoPicker.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(cedula) || string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(correoBase) || string.IsNullOrWhiteSpace(contrasena) ||
                string.IsNullOrWhiteSpace(confirmarContrasena) || string.IsNullOrWhiteSpace(rolSeleccionado) ||
                string.IsNullOrWhiteSpace(dominioCorreoSeleccionado))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
                return;
            }

            if (cedula.Length != 10 || !cedula.All(char.IsDigit))
            {
                await DisplayAlert("Error", "La cédula debe tener 10 dígitos numéricos", "OK");
                return;
            }

            var correo = correoBase + dominioCorreoSeleccionado;

            var celular = CelularEntry.Text?.Trim();
            if (rolSeleccionado != "Administrador" && (string.IsNullOrWhiteSpace(celular) || celular.Length != 10 || !celular.All(char.IsDigit)))
            {
                await DisplayAlert("Error", "El número celular debe tener 10 dígitos numéricos", "OK");
                return;
            }

            if (contrasena != confirmarContrasena)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            try
            {
                if (rolSeleccionado == "Administrador")
                {
                    // Eliminar el maxLength, o ponerlo a una longitud mayor según lo desees
                    var claveMaestra = (await DisplayPromptAsync("Clave Maestra", "Ingrese la clave maestra para continuar", "Aceptar", "Cancelar", keyboard: Keyboard.Default))?.Trim();

                    if (string.IsNullOrWhiteSpace(claveMaestra))
                    {
                        await DisplayAlert("Cancelado", "El registro de administrador fue cancelado", "OK");
                        return;
                    }

                    if (claveMaestra != MASTER_PASSWORD)
                    {
                        await DisplayAlert("Error", "La clave maestra es incorrecta", "OK");
                        return;
                    }

                    var adminDto = new RegistrarAdministradorDto
                    {
                        CedulaAdministrador = cedula,
                        NombreAdministrador = nombre,
                        CorreoAdministrador = correo,
                        ContrasenaAdministrador = contrasena
                    };

                    var resultAdmin = await _authService.RegistrarAdministradorAsync(adminDto);
                    if (resultAdmin)
                    {
                        await DisplayAlert("Éxito", "¡Administrador registrado exitosamente!", "OK");
                        LimpiarFormulario();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Hubo un problema al registrar el administrador.", "OK");
                    }
                }
                else
                {
                    string direccion = string.Empty;
                    string numeroCelular = string.Empty;

                    if (rolSeleccionado != "Administrador")
                    {
                        direccion = await DisplayPromptAsync("Dirección Requerida", "Por favor, ingrese su dirección", "Confirmar", "Cancelar");
                        if (string.IsNullOrWhiteSpace(direccion))
                        {
                            await DisplayAlert("Error", "La dirección es obligatoria para los roles de Dueño o Paseador", "OK");
                            return;
                        }

                        numeroCelular = celular;
                    }

                    var registerDto = new RegistrarUsuarioDto
                    {
                        Cedula = cedula,
                        Nombre = nombre,
                        Correo = correo,
                        Contrasena = contrasena,
                        Rol = rolSeleccionado,
                        Direccion = direccion,
                        Celular = numeroCelular
                    };

                    var result = await _authService.Register(registerDto);
                    if (result)
                    {
                        await DisplayAlert("Éxito", "¡Usuario registrado exitosamente!", "OK");
                        LimpiarFormulario();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Hubo un problema al registrar el usuario.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error inesperado: {ex.Message}\n{ex.StackTrace}", "OK");
            }
        }


        private void LimpiarFormulario()
        {
            CedulaEntry.Text = string.Empty;
            NombreEntry.Text = string.Empty;
            CorreoEntry.Text = string.Empty;
            ContraseñaEntry.Text = string.Empty;
            ConfirmarContraseñaEntry.Text = string.Empty;
            RolPicker.SelectedItem = null;
        }
    }
}
