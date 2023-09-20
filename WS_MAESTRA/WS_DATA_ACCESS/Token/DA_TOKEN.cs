using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS_DATA_MODEL.LoginToken;
using WS_DATA_MODEL.RespuestaBD;

namespace WS_DATA_ACCESS.Token
{
    public class DA_TOKEN
    {
        string cadenaConexion = "";

        public List<SalidaPermisoModel> obtenerToken(EntradaLoginTokenModel entrada)
        {
            List<SalidaPermisoModel> salidaLoginTokenModels = new List<SalidaPermisoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("SP_OBTIENE_PERMISO", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_sistema",       SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo",        SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login",    SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido",        SqlDbType.VarChar, 1).Direction             = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo",        SqlDbType.Int, 10).Direction                = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida",   SqlDbType.VarChar, 10).Direction            = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje",       SqlDbType.VarChar, 25).Direction            = ParameterDirection.Output;

                // SETEO DE VARIABLES
                cmd.Parameters["@vi_sistema"].Value     = entrada.sistema;
                cmd.Parameters["@vi_metodo"].Value      = entrada.metodo;
                cmd.Parameters["@vi_tipo_login"].Value  = entrada.tipoLogin;

                // open connection and execute stored procedure
                conn.Open();
                cmd.ExecuteNonQuery();

                salidaLoginTokenModels.Add(new SalidaPermisoModel
                {
                    permisoVigenciaModel = new LoginVigenciaModel { vigente = (string)cmd.Parameters["@vo_valido"].Value },
                    respuestaBDModel = new RespuestaBDModel {   codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                                                                tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                                                                mensaje = (string)cmd.Parameters["@vo_mensaje"].Value }
                });

                conn.Close();

            }
            catch(SqlException ex)
            {
                salidaLoginTokenModels.Add(new SalidaPermisoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(ex)
                    }
                });
            }
            finally
            {
                conn.Close();
            }

            return salidaLoginTokenModels;

        }

        //-------------------------------------------------------------------------------------
        //NUEVOS METODOS
        //-------------------------------------------------------------------------------------

    }
}
