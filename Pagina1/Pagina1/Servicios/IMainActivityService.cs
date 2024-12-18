using System;
using System.Collections.Generic;
using System.Text;

namespace Pagina1.Servicios
{
    public interface IMainActivityService
    {
        void SendSms(string phoneNumber, string message);
    }
}
