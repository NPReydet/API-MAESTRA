using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS_DATA_MODEL.RespuestaBD
{
    public class RespuestaBDModel
    {
        public Int32 codigo { get; set; }
        public string tipo { get; set; }
        public string mensaje { get; set; }
    }

    public class SalidaVigenciaModel
    {
        public string vigente { get; set; }
    }
}
