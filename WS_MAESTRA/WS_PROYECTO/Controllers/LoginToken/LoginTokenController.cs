using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WS_DATA_ACCESS.Token;
using WS_DATA_MODEL.LoginToken;
using WS_PROYECTO.Security;

namespace WS_PROYECTO.Controllers.LoginToken
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/WsLoginTokenApi")]
    public class LoginTokenController : ApiController
    {      

        /// <summary>
        /// Entrega validacion para entrega de TOKEN
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("postObtenerTokenApi")]
        public IHttpActionResult postObtenerTokenApi(EntradaLoginTokenModel login)
        {
            var TOKEN = "";
            DA_TOKEN servicio = new DA_TOKEN();

            List<SalidaPermisoModel> respuestaValidacion = servicio.obtenerToken(login);

            foreach(SalidaPermisoModel respuesta in respuestaValidacion)
            {
                if (respuesta.respuestaBDModel.codigo == -1500)
                {
                    return Unauthorized();
                }
                else if (respuesta.respuestaBDModel.codigo == 200 && respuesta.respuestaBDModel.tipo == "SUCCESS" && respuesta.permisoVigenciaModel.vigente == "S")
                {
                    TOKEN = TokenGenerator.GenerateTokenJwt(login.sistema);
                    return Ok(TOKEN);
                }
            }

            return Ok(TOKEN);
        }
    }
}
