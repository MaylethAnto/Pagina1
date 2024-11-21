using Pagina1.Modelo;
using Pagina1.Servicios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pagina1.VistaModelo
{
    public class MainPageViewModel
    {
        private readonly ApiService _apiService;

        public MainPageViewModel()
        {
            _apiService = new ApiService();
        }

        public async Task LoadUsuariosAsync()
        {
            var usuarios = await _apiService.GetAsync<List<Usuario>>("usuario"); 
        }
    }

}
