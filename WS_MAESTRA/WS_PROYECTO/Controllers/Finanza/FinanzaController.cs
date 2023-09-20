using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WS_DATA_ACCESS.Finanza;
using WS_DATA_MODEL.Finanza;



namespace WS_PROYECTO.Controllers.Finanza
{
    /// login controller class for authenticate users
    /// </summary>
    //[AllowAnonymous]
    [Authorize]
    [RoutePrefix("api/WsSistemaFinanza")]
    public class FinanzaController : ApiController
    {
        /// <summary>
        /// Valida Login de sistema de Finanza y entrega datos del usuario logeado
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetLoginUsuario")]
        public IHttpActionResult GetLoginUsuario(EntradaLoginFinanzaModel entradaLoginFinanzaModel)
        {

            dynamic resultadoJson = new JObject();
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaDatoUsuarioModel>, List<SalidaLoginFinanzaModel>> tuplaRespuesta = capaDatos.GetLoginUsuario(entradaLoginFinanzaModel);

            List<SalidaDatoUsuarioModel> salidaLogin = tuplaRespuesta.Item1;
            List<SalidaLoginFinanzaModel> salidaDatos = tuplaRespuesta.Item2;

            resultadoJson.Dato = JArray.FromObject(salidaLogin);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaLoginVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));            

            return Ok(resultadoJson);

        }

        /// <summary>
        /// Devuelve datos basicos del usuario
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetResumenUsuarios")]
        public IHttpActionResult GetResumenUsuarios(EntradaDatosGeneralUsuarioModel entradaDatosGeneralUsuarioModel)
        {

            dynamic resultadoJson = new JObject();
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaDatosBaseUsuarioModel>, List<SalidaDatosGeneralUsuarioModel>> tuplaRespuesta = capaDatos.GetResumenUsuarios(entradaDatosGeneralUsuarioModel);

            List<SalidaDatosBaseUsuarioModel> salidaLogin = tuplaRespuesta.Item1;
            List<SalidaDatosGeneralUsuarioModel> salidaDatos = tuplaRespuesta.Item2;

            resultadoJson.Dato = JArray.FromObject(salidaLogin);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaUsuarioVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Devuelve datos basicos del sueldo del usuario
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetResumenSueldos")]
        public IHttpActionResult GetResumenSueldos(EntradaDatosSueldoUsuarioModel entradaDatosSueldoUsuarioModel)
        {

            dynamic resultadoJson = new JObject();
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaSueldoUsuarioModel>, List<SalidaDatosSueldoUsuarioModel>> tuplaRespuesta = capaDatos.GetResumenSueldos(entradaDatosSueldoUsuarioModel);

            List<SalidaSueldoUsuarioModel> salidaLogin = tuplaRespuesta.Item1;
            List<SalidaDatosSueldoUsuarioModel> salidaDatos = tuplaRespuesta.Item2;            

            resultadoJson.Dato = JArray.FromObject(salidaLogin);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaSueldoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }


        /// <summary>
        /// Devuelve datos basicos delas tarjetas del usuario
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetResumenTarjetas")]
        public IHttpActionResult GetResumenTarjetas(EntradaDatosTarjetaUsuarioModel entradaDatosTarjetaUsuarioModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaTarjetaUsuarioModel>, List<SalidaDatosTarjetaUsuarioModel>> tuplaRespuesta = capaDatos.GetResumenTarjetas(entradaDatosTarjetaUsuarioModel);

            List<SalidaTarjetaUsuarioModel> salidaTarjetas = tuplaRespuesta.Item1;
            List<SalidaDatosTarjetaUsuarioModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaTarjetas);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaTarjetaVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Devuelve datos basicos de los tipos de sueldo que se manejan en el sistema
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>        
        [Route("GetTiposSueldo")]
        public IHttpActionResult GetTiposSueldo(EntradaDatosTipoSueldoModel entradaDatosTipoSueldoModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaTipoSueldoModel>, List<SalidaDatosTipoSueldoModel>> tuplaRespuesta = capaDatos.GetTiposSueldo(entradaDatosTipoSueldoModel);

            List<SalidaTipoSueldoModel> salidaTarjetas = tuplaRespuesta.Item1;
            List<SalidaDatosTipoSueldoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaTarjetas);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaTipoSueldoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }


        /// <summary>
        /// Modifica los datos del sueldo de los usuarios
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("PutSueldo")]
        public IHttpActionResult PutSueldo(EntradaModificaDatosSueldoModel entradaModificaDatosSueldoModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaModificaSueldoModel>, List<SalidaModificaDatosSueldoModel>> tuplaRespuesta = capaDatos.PutSueldo(entradaModificaDatosSueldoModel);

            List<SalidaModificaSueldoModel> salidaTarjetas = tuplaRespuesta.Item1;
            List<SalidaModificaDatosSueldoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaTarjetas);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaModificarSueldoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Obtiene datos de los tipos de cupos
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetTiposCupos")]
        public IHttpActionResult GetTiposCupos(EntradaDatosTipoCupoModel entradaDatosTipoCupoModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaTipoCupoModel>, List<SalidaDatosCupoModel>> tuplaRespuesta = capaDatos.GetTiposCupos(entradaDatosTipoCupoModel);

            List<SalidaTipoCupoModel> salidaCupo = tuplaRespuesta.Item1;
            List<SalidaDatosCupoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaCupo);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaTipoCupoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Obtiene datos de los tipos de tarjetas
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetTiposTarjeta")]
        public IHttpActionResult GetTiposTarjeta(EntradaDatosTipoTarjetaModel entradaDatosTipoTarjetaModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaTipoTarjetaModel>, List<SalidaDatosTarjetaModel>> tuplaRespuesta = capaDatos.GetTiposTarjeta(entradaDatosTipoTarjetaModel);

            List<SalidaTipoTarjetaModel> salidaTarjetas = tuplaRespuesta.Item1;
            List<SalidaDatosTarjetaModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaTarjetas);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaTipoTarjetaVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Obtiene datos de los bancos
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetBancos")]
        public IHttpActionResult GetBancos(EntradaDatosBancoModel entradaDatosBancoModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaBancoModel>, List<SalidaDatosBancoModel>> tuplaRespuesta = capaDatos.GetBancos(entradaDatosBancoModel);

            List<SalidaBancoModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaDatosBancoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaBancoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Obtiene datos de tipos de moneda
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("getDatosTipoMoneda")]
        public IHttpActionResult getDatosTipoMoneda(EntradaDatosTipoMonedaModel entradaDatosTipoMonedaModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaDatosTipoMonedaModel>, List<SalidaTipoMonedaModel>> tuplaRespuesta = capaDatos.getDatosTipoMoneda(entradaDatosTipoMonedaModel);

            List<SalidaDatosTipoMonedaModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaTipoMonedaModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaTpoMonedaVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Modifica/crea tarjetas
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("putDatosTarjeta")]
        public IHttpActionResult putDatosTarjeta(EntradaDatosTarjetaModificarModel entradaDatosTarjetaModificarModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaDatosTarjetaModificarModel>, List<SalidaTarjetaModificarModel>> tuplaRespuesta = capaDatos.putDatosTarjeta(entradaDatosTarjetaModificarModel);

            List<SalidaDatosTarjetaModificarModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaTarjetaModificarModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaModificarTarjetaVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Obtiene informacion de los gastos
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("getDatosGasto")]
        public IHttpActionResult getDatosGasto(EntradaDatosGastosModel entradaDatosGastosModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaDatosGastosModel>, List<SalidaGastosModel>> tuplaRespuesta = capaDatos.getDatosGasto(entradaDatosGastosModel);

            List<SalidaDatosGastosModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaGastosModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaDatosGastosVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Graba sueldos de los usuarios
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>    
        /// <response code="300">Servicio funcionando pero con observaciones.</response>        
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetResumenIngresos")]
        public IHttpActionResult GetResumenIngresos(EntradaDatosIngresoModel entradaDatosIngresoModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaDatosIngresoModel>, List<SalidaIngresoModel>> tuplaRespuesta = capaDatos.GetResumenIngresos(entradaDatosIngresoModel);

            List<SalidaDatosIngresoModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaIngresoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaDatosIngresoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Obtiene informacion de los ingresos
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("PostSueldo")]
        public IHttpActionResult PostSueldo(EntradaGrabarDatosSueldoModel entradaGrabarDatosSueldoModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaGrabaDatosSueldoModel>, List<SalidaGrabaSueldoModel>> tuplaRespuesta = capaDatos.PostSueldo(entradaGrabarDatosSueldoModel);

            List<SalidaGrabaDatosSueldoModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaGrabaSueldoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaGrabaSueldoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }


        /// <summary>
        /// Elimina sueldo a partir de la id
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("DeleteSueldo")]
        public IHttpActionResult DeleteSueldo(EntradaEliminarDatosSueldoModel entradaEliminarDatosSueldoModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaEliminarDatosSueldoModel>, List<SalidaEliminarSueldoModel>> tuplaRespuesta = capaDatos.DeleteSueldo(entradaEliminarDatosSueldoModel);

            List<SalidaEliminarDatosSueldoModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaEliminarSueldoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaEliminaSueldoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Crea tarjeta nueva
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("PostTarjeta")]
        public IHttpActionResult PostTarjeta(EntradaGrabarDatosTarjetaModel entradaGrabarDatosTarjetaModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaGrabarDatosTarjetaModel>, List<SalidaGrabarTarjetaModel>> tuplaRespuesta = capaDatos.PostTarjeta(entradaGrabarDatosTarjetaModel);

            List<SalidaGrabarDatosTarjetaModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaGrabarTarjetaModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaDatosTarjetaVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Modifica tarjeta existente
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("PutTarjeta")]
        public IHttpActionResult PutTarjeta(EntradaModificaDatosTarjetaModel entradaModificaDatosTarjetaModel)
        {

            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaModificaDatosTarjetaModel>, List<SalidaModificaTarjetaModel>> tuplaRespuesta = capaDatos.PutTarjeta(entradaModificaDatosTarjetaModel);

            List<SalidaModificaDatosTarjetaModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaModificaTarjetaModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaModificaTarjetaVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Elimina tarjeta existente
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("DeleteTarjeta")]
        public IHttpActionResult DeleteTarjeta(EntradaEliminaDatosTarjetaModel entradaEliminaDatosTarjetaModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaEliminaDatosTarjetaModel>, List<SalidaEliminaTarjetaModel>> tuplaRespuesta = capaDatos.DeleteTarjeta(entradaEliminaDatosTarjetaModel);

            List<SalidaEliminaDatosTarjetaModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaEliminaTarjetaModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaEliminaTarjetaVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Obtiene informacion de los tipos de ingresos
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetTiposIngreso")]
        public IHttpActionResult GetTiposIngreso(EntradaDatosTiposIngresoModel entradaDatosTiposIngresoModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaDatosTiposIngresoModel>, List<SalidaTiposIngresoModel>> tuplaRespuesta = capaDatos.GetTiposIngreso(entradaDatosTiposIngresoModel);

            List<SalidaDatosTiposIngresoModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaTiposIngresoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaTiposIngresoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Graba nuevo ingreso
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("PostIngreso")]
        public IHttpActionResult PostIngreso(EntradaGrabarDatosIngresoModel entradaGrabarDatosIngresoModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaGrabarDatosIngresoModel>, List<SalidaGrabarIngresoModel>> tuplaRespuesta = capaDatos.PostIngreso(entradaGrabarDatosIngresoModel);

            List<SalidaGrabarDatosIngresoModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaGrabarIngresoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaGrabarIngresoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Graba nuevo gasto
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("PostGasto")]
        public IHttpActionResult PostGasto(EntradaGrabarDatosGastosModel entradaGrabarDatosGastosModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaGrabarDatosGastosModel>, List<SalidaGrabarGastosModel>> tuplaRespuesta = capaDatos.PostGasto(entradaGrabarDatosGastosModel);

            List<SalidaGrabarDatosGastosModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaGrabarGastosModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaGrabarGastosVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Muestra resumen de gastos
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetResumenGastos")]
        public IHttpActionResult GetResumenGastos(EntradaResumenDatosGastosModel entradaResumenDatosGastosModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaResumenDatosGastosModel>, List<SalidaResumenGastosModel>> tuplaRespuesta = capaDatos.GetResumenGastos(entradaResumenDatosGastosModel);

            List<SalidaResumenDatosGastosModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaResumenGastosModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaResumenGastosVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Muestra los tipos de gastos
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetTiposGasto")]
        public IHttpActionResult GetTiposGasto(EntradaTipoGastoDatosModel entradaTipoGastoDatosModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaTipoGastoDatosModel>, List<SalidaTipoGastoModel>> tuplaRespuesta = capaDatos.GetTiposGasto(entradaTipoGastoDatosModel);

            List<SalidaTipoGastoDatosModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaTipoGastoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaTipoGastoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Muestra las tarjetas del usuario
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("GetTarjetasUsuario")]
        public IHttpActionResult GetTarjetasUsuario(EntradaTarjetasDatosModel entradaTarjetasDatosModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaTarjetasDatosModel>, List<SalidaTarjetasModel>> tuplaRespuesta = capaDatos.GetTarjetasUsuario(entradaTarjetasDatosModel);

            List<SalidaTarjetasDatosModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaTarjetasModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaTarjetasVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Modifica el gasto del usuario
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("PutGasto")]
        public IHttpActionResult PutGasto(EntradaModificaGastoDatosModel entradaModificaGastoDatosModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaModificaGastoDatosModel>, List<SalidaModificaGastoModel>> tuplaRespuesta = capaDatos.PutGasto(entradaModificaGastoDatosModel);

            List<SalidaModificaGastoDatosModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaModificaGastoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaGastosVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        /// Elimina un gasto del usuario
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("DeleteGasto")]
        public IHttpActionResult DeleteGasto(EntradaEliminarGastoDatosModel entradaEliminarGastoDatosModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaEliminarGastoDatosModel>, List<SalidaEliminarGastoModel>> tuplaRespuesta = capaDatos.DeleteGasto(entradaEliminarGastoDatosModel);

            List<SalidaEliminarGastoDatosModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaEliminarGastoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaEliminarGastosVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        ///Modifica ingreso del usuario
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("PutIngreso")]
        public IHttpActionResult PutIngreso(EntradaModificaDatosIngresoModel entradaModificaDatosIngresoModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaModificaDatosIngresoModel>, List<SalidaModificaIngresoModel>> tuplaRespuesta = capaDatos.PutIngreso(entradaModificaDatosIngresoModel);

            List<SalidaModificaDatosIngresoModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaModificaIngresoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaModificaIngresoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }

        /// <summary>
        ///Modifica ingreso del usuario
        /// </summary>
        /// <remarks>
        /// Define si el servicio convocado posee permisos de llamado 
        /// </remarks>
        /// <response code="401">JWT incorrecto, no tiene permisos para consumir el servicio.</response>              
        /// <response code="200">Servicio funcionando OK.</response>     
        /// <response code="300">Servicio funcionando pero con observaciones.</response>    
        /// <response code="404">No se encuentra el servicio.</response>
        /// <response code="-100">Problema en la comunicación con la BD.</response>
        /// <response code="-200">Problema al consumir servicio.</response>
        [Route("DeleteIngreso")]
        public IHttpActionResult DeleteIngreso(EntradaEliminarIngresoDatosModel entradaEliminarIngresoDatosModel)
        {
            DA_Finanza capaDatos = new DA_Finanza();
            Tuple<List<SalidaEliminarIngresoDatosModel>, List<SalidaEliminarIngresoModel>> tuplaRespuesta = capaDatos.DeleteIngreso(entradaEliminarIngresoDatosModel);

            List<SalidaEliminarIngresoDatosModel> salidaBancos = tuplaRespuesta.Item1;
            List<SalidaEliminarIngresoModel> salidaDatos = tuplaRespuesta.Item2;

            dynamic resultadoJson = new JObject();
            resultadoJson.Dato = JArray.FromObject(salidaBancos);
            resultadoJson.Vigente = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.salidaEliminarIngresoVigenciaModel));
            resultadoJson.Mensaje = JArray.FromObject(salidaDatos.Select(respuesta => respuesta.respuestaBDModel));

            return Ok(resultadoJson);
        }


    }
}
