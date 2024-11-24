using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Pagina1.Servicios
{
    //login admin
    public class LoginService
    {
        private static readonly string apiUrl = "http://10.0.2.2:5138/api/Login/Login";

        public async Task<string> LoginUsuarioAsync (string nombreUsuario, string contrasena)
        {
            using (var client = new HttpClient())
            {
                var request = new
                {
                    NombreUsuario = nombreUsuario,
                    Contrasena = contrasena
                };

                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return $"Login exitoso. Rol: {result.rol}";

                }
                else
                {
                    return "Error al iniciar sesión: " + response.ReasonPhrase;
                }
            }
        }

    }
}
