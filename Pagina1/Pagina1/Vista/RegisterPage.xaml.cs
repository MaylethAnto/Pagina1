using Pagina1.Dtos;
using Pagina1.Servicios;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pagina1.Modelo;
using Xamarin.Essentials;

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

            // Configuración inicial de visibilidad
            ClaveMaestraStack.IsVisible = false;
            DireccionCelularStack.IsVisible = false;

            // Configurar el Picker de roles
            RolPicker.SelectedIndexChanged += OnRoleChanged;
        }


        private void OnRoleChanged(object sender, EventArgs e)
        {
            if (RolPicker.SelectedItem == null) return;

            var selectedRole = RolPicker.SelectedItem.ToString();

            // Actualizar visibilidad basada en el rol
            ClaveMaestraStack.IsVisible = selectedRole == "Administrador";
            DireccionCelularStack.IsVisible = selectedRole != "Administrador";
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCamposBasicos()) return;

                var cedula = CedulaEntry.Text.Trim();
                var nombre = NombreEntry.Text.Trim();
                var usuario = UsuarioEntry.Text.Trim();
                var correoBase = CorreoEntry.Text.Trim();
                var dominioCorreo = CorreoPicker.SelectedItem?.ToString() ?? "";
                var correoCompleto = $"{correoBase}{dominioCorreo}";
                var contrasena = ContraseñaEntry.Text;
                var rolSeleccionado = RolPicker.SelectedItem?.ToString();

                Debug.WriteLine($"Intentando registrar usuario con rol: {rolSeleccionado}");
                Debug.WriteLine($"Correo completo: {correoCompleto}");

                if (rolSeleccionado == "Administrador")
                {
                    await RegistrarAdministrador(cedula, nombre, usuario, correoCompleto, contrasena);
                }
                else
                {
                    await RegistrarUsuario(cedula, nombre, usuario, correoCompleto, contrasena, rolSeleccionado);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en OnRegisterButtonClicked: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                await DisplayAlert("Error", $"Error inesperado: {ex.Message}", "OK");
            }
        }

        private bool ValidarCamposBasicos()
        {
            try
            {
                // Validar campos requeridos
                if (string.IsNullOrWhiteSpace(CedulaEntry.Text) ||
                    string.IsNullOrWhiteSpace(NombreEntry.Text) ||
                    string.IsNullOrWhiteSpace(UsuarioEntry.Text) ||
                    string.IsNullOrWhiteSpace(CorreoEntry.Text) ||
                    string.IsNullOrWhiteSpace(ContraseñaEntry.Text) ||
                    string.IsNullOrWhiteSpace(ConfirmarContraseñaEntry.Text) ||
                    RolPicker.SelectedItem == null ||
                    CorreoPicker.SelectedItem == null)
                {
                    DisplayAlert("Error", "Por favor, complete todos los campos requeridos", "OK");
                    return false;
                }

                // Validar cédula
                if (CedulaEntry.Text.Length != 10 || !CedulaEntry.Text.All(char.IsDigit))
                {
                    DisplayAlert("Error", "La cédula debe tener 10 dígitos numéricos", "OK");
                    return false;
                }

                // Validar contraseñas
                if (ContraseñaEntry.Text != ConfirmarContraseñaEntry.Text)
                {
                    DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                    return false;
                }

                Debug.WriteLine("Validación básica exitosa");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en ValidarCamposBasicos: {ex.Message}");
                return false;
            }
        }

        private async Task RegistrarAdministrador(string cedula, string nombre, string usuario,string correo, string contrasena)
        {
            try
            {
                Debug.WriteLine("Iniciando registro de administrador");

                // Validar la clave maestra
                var claveMaestra = ClaveMaestraEntry?.Text;

                if (string.IsNullOrWhiteSpace(claveMaestra))
                {
                    await DisplayAlert("Error", "La clave maestra y el usuario administrador son requeridos", "OK");
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
                    UsuarioAdministrador = usuario,
                    CorreoAdministrador = correo,
                    ContrasenaAdministrador = contrasena,
                    ClaveMaestra = claveMaestra
                };

                Debug.WriteLine("Enviando datos al servicio de registro");
                var result = await _authService.RegistrarAdministradorAsync(adminDto);

                if (result)
                {
                    await DisplayAlert("Éxito", "¡Administrador registrado exitosamente!", "OK");
                    LimpiarFormulario();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo registrar el administrador", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en RegistrarAdministrador: {ex.Message}");
                await DisplayAlert("Error", $"Error al registrar administrador: {ex.Message}", "OK");
            }
        }

        private async Task RegistrarUsuario(string cedula, string nombre, string usuario,string correo, string contrasena, string rol)
        {
            Preferences.Set("CedulaUsuario", cedula);
            try
            {
                Debug.WriteLine("Iniciando registro de usuario normal");
                // Validar celular para roles no administrador
                if (string.IsNullOrWhiteSpace(CelularEntry.Text) ||
                    CelularEntry.Text.Length != 10 ||
                    !CelularEntry.Text.All(char.IsDigit))
                {
                    await DisplayAlert("Error", "El número celular debe tener 10 dígitos numéricos", "OK");
                    return;
                }

                var direccion = DireccionEntry.Text?.Trim();
                if (string.IsNullOrWhiteSpace(direccion))
                {
                    await DisplayAlert("Error", "La dirección es obligatoria", "OK");
                    return;
                }

                var registerDto = new RegistrarUsuarioDto
                {
                    Cedula = cedula,
                    Nombre = nombre,
                    Usuario = usuario,
                    Correo = correo,
                    Contrasena = contrasena,
                    Rol = rol,
                    Direccion = direccion,
                    Celular = CelularEntry.Text.Trim()
                };

                Debug.WriteLine("Enviando datos al servicio de registro de usuario");
                var result = await _authService.RegistrarUsuarioAsync(registerDto);

                if (result)
                {
                    await DisplayAlert("Éxito", "¡Usuario registrado exitosamente!", "OK");
                    LimpiarFormulario();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo registrar el usuario", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en RegistrarUsuario: {ex.Message}");
                await DisplayAlert("Error", $"Error al registrar usuario: {ex.Message}", "OK");
            }
        }

        private void LimpiarFormulario()
        {
            try
            {
                CedulaEntry.Text = string.Empty;
                NombreEntry.Text = string.Empty;
                UsuarioEntry.Text = string.Empty;
                CorreoEntry.Text = string.Empty;
                ContraseñaEntry.Text = string.Empty;
                ConfirmarContraseñaEntry.Text = string.Empty;
                DireccionEntry.Text = string.Empty;
                CelularEntry.Text = string.Empty;
                ClaveMaestraEntry.Text = string.Empty;
                RolPicker.SelectedItem = null;
                CorreoPicker.SelectedItem = null;

                // Restablecer visibilidad
                ClaveMaestraStack.IsVisible = false;
                DireccionCelularStack.IsVisible = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en LimpiarFormulario: {ex.Message}");
            }
        }
    }
}