using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS_DATA_MODEL.RespuestaBD;

namespace WS_DATA_MODEL.LoginToken
{    

    public class EntradaLoginTokenModel
    {
        public string sistema { get; set; }
        public string metodo { get; set; }
        public string tipoLogin { get; set; }

    }

    public class SalidaPermisoModel
    {
        public LoginVigenciaModel permisoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }

    }

    public class LoginVigenciaModel
    {
        public string vigente { get; set; }
    }



    

}
