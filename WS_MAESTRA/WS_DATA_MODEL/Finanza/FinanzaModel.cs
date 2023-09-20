using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS_DATA_MODEL.RespuestaBD;

namespace WS_DATA_MODEL.Finanza
{

    #region Modelo login
    public class EntradaLoginFinanzaModel
    {
        public string usuario { get; set; } = "";
        public string clave { get; set; } = "";
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaLoginFinanzaModel
    {
        public SalidaVigenciaModel salidaLoginVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    public class SalidaDatoUsuarioModel
    {
        public string nombres { get; set; } = "";
        public string apellidoPaterno { get; set; } = "";
        public string apellidoMaterno { get; set; } = "";
        public int rut { get; set; } = 0;
    }
    #endregion

    #region Modelo resumen general
    public class EntradaDatosGeneralUsuarioModel
    {
        public int rutUsuario { get; set; } = 0;
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaDatosGeneralUsuarioModel
    {
        public SalidaVigenciaModel salidaUsuarioVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    public class SalidaDatosBaseUsuarioModel
    {
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public int rut { get; set; }
        public string correo { get; set; }
        public string nombreCompleto { get; set; }
        public string montoTarjetaCredito { get; set; }
        public string montoTarjetaDebito { get; set; }
        public int montoBruto { get; set; }
        public int montoLiquido { get; set; }
        public string tipoSueldo { get; set; }

    }
    #endregion

    #region Modelo datos sueldo
    public class EntradaDatosSueldoUsuarioModel
    {
        public int rutUsuario { get; set; } = 0;
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaDatosSueldoUsuarioModel
    {
        public SalidaVigenciaModel salidaSueldoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    public class SalidaSueldoUsuarioModel
    {
        public int idSueldo { get; set; }
        public int montoLiquido { get; set; }
        public int montoBruto { get; set; }
        public string tipoSueldo { get; set; }
    }
    #endregion

    #region Modelo datos tarjetas
    public class EntradaDatosTarjetaUsuarioModel
    {
        public int idTarjeta { get; set; }
        public int rutUsuario { get; set; } = 0;
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaDatosTarjetaUsuarioModel
    {
        public SalidaVigenciaModel salidaTarjetaVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    public class SalidaTarjetaUsuarioModel
    {
        public string TARJETA { get; set; } = "";
        public string BANCO { get; set; } = "";
        public int CUPO_TARJETA { get; set; } = 0;
        public string TIPO_CAMBIO { get; set; } = "";
        public DateTime VIGENCIA_INICIO { get; set; }
        public DateTime VIGENCIA_TERMINO { get; set; }
        public int ID_TARJETA { get; set; }
    }
    #endregion

    #region Modelo tipos de sueldo
    public class EntradaDatosTipoSueldoModel
    {
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaDatosTipoSueldoModel
    {
        public SalidaVigenciaModel salidaTipoSueldoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    public class SalidaTipoSueldoModel
    {
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
    }
    #endregion

    #region Modelos para modificar sueldo
    public class EntradaModificaDatosSueldoModel
    {
        public int idSueldo { get; set; }
        public int montoLiquido { get; set; }
        public int montoBruto { get; set; }
        public int tipoSueldo { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaModificaDatosSueldoModel
    {
        public SalidaVigenciaModel salidaModificarSueldoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    public class SalidaModificaSueldoModel
    {

    }
    #endregion

    #region Modelos tipos de cupos
    public class EntradaDatosTipoCupoModel
    {
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaTipoCupoModel
    {
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class SalidaDatosCupoModel
    {
        public SalidaVigenciaModel salidaTipoCupoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }
    #endregion

    #region Modelos tipos de tarjeta
    public class EntradaDatosTipoTarjetaModel
    {
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaTipoTarjetaModel
    {
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class SalidaDatosTarjetaModel
    {
        public SalidaVigenciaModel salidaTipoTarjetaVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }
    #endregion

    #region Modelos bancos existentes
    public class EntradaDatosBancoModel
    {
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaBancoModel
    {
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class SalidaDatosBancoModel
    {
        public SalidaVigenciaModel salidaBancoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }
    #endregion

    #region Modelos tipos de moneda
    public class EntradaDatosTipoMonedaModel
    {
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaDatosTipoMonedaModel
    {
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class SalidaTipoMonedaModel
    {
        public SalidaVigenciaModel salidaTpoMonedaVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }
    #endregion

    #region Modelos para modificar tarjetas (eliminar luego de crear post/put)
    public class EntradaDatosTarjetaModificarModel
    {
        public string funcion { get; set; }
        public int rut { get; set; }
        public int tipoTarjeta { get; set; }
        public int banco { get; set; }
        public int tipoCupo { get; set; }
        public int cupoTarjeta { get; set; }
        public int moneda { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaTermino { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaDatosTarjetaModificarModel
    {

    }

    public class SalidaTarjetaModificarModel
    {
        public SalidaVigenciaModel salidaModificarTarjetaVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }
    #endregion

    #region Modelo gastos existentes
    public class EntradaDatosGastosModel
    {
        public int rutUsuario { get; set; } = 0;
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaDatosGastosModel
    {
        public string TIPO_CUENTA { get; set; }
        public string CUENTA { get; set; }
        public int MONTO { get; set; }
        public DateTime FECHA_GASTO { get; set; }
        public string TIPO_TARJETA { get; set; }
    }

    public class SalidaGastosModel
    {
        public SalidaVigenciaModel salidaDatosGastosVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }
    #endregion

    #region Modelo ingresos existentes
    public class EntradaDatosIngresoModel
    {
        public int rutUsuario { get; set; } = 0;
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaDatosIngresoModel
    {
        public int MONTO_INGRESO { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public string TIPO_INGRESO { get; set; }
        public int ID_INGRESO { get; set; }
    }

    public class SalidaIngresoModel
    {
        public SalidaVigenciaModel salidaDatosIngresoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }
    #endregion

    #region Modelos para grabar sueldo

    public class EntradaGrabarDatosSueldoModel
    {
        public int rut { get; set; }
        public int montoLiquido { get; set; }
        public int montoBruto { get; set; }
        public int tipoSueldo { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaGrabaDatosSueldoModel
    {

    }

    public class SalidaGrabaSueldoModel
    {
        public SalidaVigenciaModel salidaGrabaSueldoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para eliminar sueldo

    public class EntradaEliminarDatosSueldoModel
    {
        public int idSueldo { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaEliminarDatosSueldoModel
    {

    }

    public class SalidaEliminarSueldoModel
    {
        public SalidaVigenciaModel salidaEliminaSueldoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para grabar tarjeta

    public class EntradaGrabarDatosTarjetaModel
    {
        public int rut { get; set; }
        public int tipoTarjeta { get; set; }
        public int banco { get; set; }
        public int tipoCupo { get; set; }
        public int cupoTarjeta { get; set; }
        public int moneda { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaTermino { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaGrabarDatosTarjetaModel
    {

    }

    public class SalidaGrabarTarjetaModel
    {
        public SalidaVigenciaModel salidaDatosTarjetaVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para modificar tarjeta

    public class EntradaModificaDatosTarjetaModel
    {
        public int rut { get; set; }
        public int tipoTarjeta { get; set; }
        public int banco { get; set; }
        public int tipoCupo { get; set; }
        public int cupoTarjeta { get; set; }
        public int moneda { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaTermino { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaModificaDatosTarjetaModel
    {

    }

    public class SalidaModificaTarjetaModel
    {
        public SalidaVigenciaModel salidaModificaTarjetaVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para eliminar tarjeta

    public class EntradaEliminaDatosTarjetaModel
    {
        public int idTarjeta { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaEliminaDatosTarjetaModel
    {

    }

    public class SalidaEliminaTarjetaModel
    {
        public SalidaVigenciaModel salidaEliminaTarjetaVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos tipos de ingresos

    public class EntradaDatosTiposIngresoModel
    {
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaDatosTiposIngresoModel
    {
        public int ID { get; set; }
        public string DESCRIPCION { get; set; } 
    }

    public class SalidaTiposIngresoModel
    {
        public SalidaVigenciaModel salidaTiposIngresoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para crear nuevo ingreso

    public class EntradaGrabarDatosIngresoModel
    {
        public int rut { get; set; }
        public int monto { get; set; }
        public DateTime fechaIngreso { get; set; }
        public int tipoIngreso { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaGrabarDatosIngresoModel
    {

    }

    public class SalidaGrabarIngresoModel
    {
        public SalidaVigenciaModel salidaGrabarIngresoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para crear nuevo gasto

    public class EntradaGrabarDatosGastosModel
    {
        public int rut { get; set; }
        public int tipoTarjeta { get; set; }
        public string descripcionGasto { get; set; }
        public int monto { get; set; }
        public DateTime fechaGasto { get; set; }
        public int tipoCuenta { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaGrabarDatosGastosModel
    {

    }

    public class SalidaGrabarGastosModel
    {
        public SalidaVigenciaModel salidaGrabarGastosVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }


    #endregion

    #region Modelos

    public class EntradaResumenDatosGastosModel
    {
        public int rut { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaResumenDatosGastosModel
    {
        public string tipoGasto { get; set; }
        public string detalleGasto { get; set; }
        public int montoGasto { get; set; }
        public DateTime fechaGasto { get; set; }
        public string tipoTarjeta { get; set; }
        public int idGasto { get; set; }
    }

    public class SalidaResumenGastosModel
    {
        public SalidaVigenciaModel salidaResumenGastosVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para tipos de gastos 

    public class EntradaTipoGastoDatosModel
    {
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaTipoGastoDatosModel
    {
        public int idTipoGasto { get; set; }
        public string descripcion { get; set; }
    }

    public class SalidaTipoGastoModel
    {
        public SalidaVigenciaModel salidaTipoGastoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para tarjetas del usuario

    public class EntradaTarjetasDatosModel
    {
        public int rut { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaTarjetasDatosModel
    {
        public int idTarjeta { get; set; }
        public string descripcion { get; set; }
    }

    public class SalidaTarjetasModel
    {
        public SalidaVigenciaModel salidaTarjetasVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para modificar gasto

    public class EntradaModificaGastoDatosModel
    {
        public string descripcion { get; set; }
        public int monto { get; set; }
        public DateTime fechaGasto { get; set; }
        public int tipoCuenta { get; set; }
        public int idGasto { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaModificaGastoDatosModel
    {

    }

    public class SalidaModificaGastoModel
    {
        public SalidaVigenciaModel salidaGastosVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para eliminar gasto

    public class EntradaEliminarGastoDatosModel
    {
        public int idGasto { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaEliminarGastoDatosModel
    {

    }

    public class SalidaEliminarGastoModel
    {
        public SalidaVigenciaModel salidaEliminarGastosVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para actualizar ingreso

    public class EntradaModificaDatosIngresoModel
    {
        public int monto { get; set; }
        public DateTime fechaIngreso { get; set; }
        public int tipoIngreso { get; set; }
        public int idIngreso { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaModificaDatosIngresoModel
    {

    }

    public class SalidaModificaIngresoModel
    {
        public SalidaVigenciaModel salidaModificaIngresoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

    #region Modelos para eliminar ingreso

    public class EntradaEliminarIngresoDatosModel
    {
        public int idIngreso { get; set; }
        public string sistema { get; set; } = "";
        public string metodo { get; set; } = "";
        public string tipoLogin { get; set; } = "";
    }

    public class SalidaEliminarIngresoDatosModel
    {

    }

    public class SalidaEliminarIngresoModel
    {
        public SalidaVigenciaModel salidaEliminarIngresoVigenciaModel { get; set; }
        public RespuestaBDModel respuestaBDModel { get; set; }
    }

    #endregion

}
