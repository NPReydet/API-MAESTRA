using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WS_DATA_MODEL.Finanza;
using WS_DATA_MODEL.RespuestaBD;

namespace WS_DATA_ACCESS.Finanza
{
    public class DA_Finanza
    {
        string cadenaConexion = "";

        public Tuple<List<SalidaDatoUsuarioModel>, List<SalidaLoginFinanzaModel>> GetLoginUsuario(EntradaLoginFinanzaModel entradaLoginFinanzaModel)
        {
            List<SalidaDatoUsuarioModel> salidaDatoUsuarioModel = new List<SalidaDatoUsuarioModel>();
            List<SalidaLoginFinanzaModel> salidaLoginFinanzaModel = new List<SalidaLoginFinanzaModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetLoginUsuario", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_usuario", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@vi_clave", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_usuario"].Value = entradaLoginFinanzaModel.usuario;
                cmd.Parameters["@vi_clave"].Value = entradaLoginFinanzaModel.clave;
                cmd.Parameters["@vi_sistema"].Value = entradaLoginFinanzaModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaLoginFinanzaModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaLoginFinanzaModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaDatoUsuarioModel.Add(new SalidaDatoUsuarioModel // Cambio 3: Utilizar paréntesis normales
                    {
                        nombres = (string)row["CFU_NOMBRES"],
                        apellidoPaterno = (string)row["CFU_APELLIDO_PATERNO"],
                        apellidoMaterno = (string)row["CFU_APELLIDO_MATERNO"],
                        rut = (int)row["CFU_RUT"]
                    });
                }


                salidaLoginFinanzaModel.Add(new SalidaLoginFinanzaModel
                {
                    salidaLoginVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

                conn.Close();

            }
            catch (SqlException sqlException)
            {
                salidaLoginFinanzaModel.Add(new SalidaLoginFinanzaModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
                conn.Close();
            }

            return Tuple.Create(salidaDatoUsuarioModel, salidaLoginFinanzaModel);

        }

        public Tuple<List<SalidaDatosBaseUsuarioModel>, List<SalidaDatosGeneralUsuarioModel>> GetResumenUsuarios(EntradaDatosGeneralUsuarioModel entradaDatosGeneralUsuarioModel)
        {
            List<SalidaDatosBaseUsuarioModel> salidaDatosBaseUsuarioModel = new List<SalidaDatosBaseUsuarioModel>();
            List<SalidaDatosGeneralUsuarioModel> salidaDatosGeneralUsuarioModel = new List<SalidaDatosGeneralUsuarioModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetResumenUsuarios", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 20);
                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut_usuario"].Value = entradaDatosGeneralUsuarioModel.rutUsuario;
                cmd.Parameters["@vi_sistema"].Value = entradaDatosGeneralUsuarioModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosGeneralUsuarioModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosGeneralUsuarioModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);


                foreach (DataRow row in dataTable.Rows)
                {
                    salidaDatosBaseUsuarioModel.Add(new SalidaDatosBaseUsuarioModel // Cambio 3: Utilizar paréntesis normales
                    {
                        nombres = (string)row["NOMBRES"],
                        apellidoPaterno = (string)row["APELLIDO_PATERNO"],
                        apellidoMaterno = (string)row["APELLIDO_MATERNO"],
                        rut = (int)row["RUT"],
                        correo = (string)row["CORREO"],
                        nombreCompleto = (string)row["NOMBRE_COMPLETO"],
                        montoTarjetaCredito = (string)row["MONTO_TARJETA_CREDITO"],
                        montoTarjetaDebito = (string)row["MONTO_TARJETA_DEBITO"],
                        montoBruto = (int)row["SUELDO_BRUTO"],
                        montoLiquido = (int)row["SUELDO_LIQUIDO"],
                        tipoSueldo = (string)row["TIPO_SUELDO"]
                    });
                }

                salidaDatosGeneralUsuarioModel.Add(new SalidaDatosGeneralUsuarioModel
                {
                    salidaUsuarioVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaDatosGeneralUsuarioModel.Add(new SalidaDatosGeneralUsuarioModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaDatosBaseUsuarioModel, salidaDatosGeneralUsuarioModel);
        }

        public Tuple<List<SalidaSueldoUsuarioModel>, List<SalidaDatosSueldoUsuarioModel>> GetResumenSueldos(EntradaDatosSueldoUsuarioModel entradaDatosSueldoUsuarioModel)
        {

            List<SalidaSueldoUsuarioModel> salidaSueldoUsuarioModel = new List<SalidaSueldoUsuarioModel>();
            List<SalidaDatosSueldoUsuarioModel> salidaDatosSueldoUsuarioModel = new List<SalidaDatosSueldoUsuarioModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetResumenSueldos", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 20);
                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut_usuario"].Value = entradaDatosSueldoUsuarioModel.rutUsuario;
                cmd.Parameters["@vi_sistema"].Value = entradaDatosSueldoUsuarioModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosSueldoUsuarioModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosSueldoUsuarioModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaSueldoUsuarioModel.Add(new SalidaSueldoUsuarioModel // Cambio 3: Utilizar paréntesis normales
                    {
                        montoLiquido = (int)row["MONTO_LIQUIDO"],
                        montoBruto = (int)row["MONTO_BRUTO"],
                        tipoSueldo = (string)row["TIPO_SUELDO"],
                        idSueldo = (int)row["ID_SUELDO"]
                    });
                }


                salidaDatosSueldoUsuarioModel.Add(new SalidaDatosSueldoUsuarioModel
                {
                    salidaSueldoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaDatosSueldoUsuarioModel.Add(new SalidaDatosSueldoUsuarioModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaSueldoUsuarioModel, salidaDatosSueldoUsuarioModel);
        }

        public Tuple<List<SalidaTarjetaUsuarioModel>, List<SalidaDatosTarjetaUsuarioModel>> GetResumenTarjetas(EntradaDatosTarjetaUsuarioModel entradaDatosTarjetaUsuarioModel)
        {
            List<SalidaDatosTarjetaUsuarioModel> salidaDatosTarjetaUsuarioModel = new List<SalidaDatosTarjetaUsuarioModel>();
            List<SalidaTarjetaUsuarioModel> salidaTarjetaUsuarioModel = new List<SalidaTarjetaUsuarioModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetResumenTarjetas", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_id_tarjeta", SqlDbType.Int, 20);
                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 20);
                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_id_tarjeta"].Value = entradaDatosTarjetaUsuarioModel.idTarjeta;
                cmd.Parameters["@vi_rut_usuario"].Value = entradaDatosTarjetaUsuarioModel.rutUsuario;
                cmd.Parameters["@vi_sistema"].Value = entradaDatosTarjetaUsuarioModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosTarjetaUsuarioModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosTarjetaUsuarioModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaTarjetaUsuarioModel.Add(new SalidaTarjetaUsuarioModel // Cambio 3: Utilizar paréntesis normales
                    {
                        TARJETA = (string)row["TARJETA"],
                        BANCO = (string)row["BANCO"],
                        CUPO_TARJETA = (int)row["CUPO_TARJETA"],
                        TIPO_CAMBIO = (string)row["TIPO_CAMBIO"],
                        VIGENCIA_INICIO = (DateTime)row["VIGENCIA_INICIO"],
                        VIGENCIA_TERMINO = (DateTime)row["VIGENCIA_TERMINO"], // Cambio 4: Asegurar que VIGENCIA_TERMINO acepte valores nulos
                        ID_TARJETA = (int)row["ID_TARJETA"]
                    });
                }


                salidaDatosTarjetaUsuarioModel.Add(new SalidaDatosTarjetaUsuarioModel
                {
                    salidaTarjetaVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaDatosTarjetaUsuarioModel.Add(new SalidaDatosTarjetaUsuarioModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaTarjetaUsuarioModel, salidaDatosTarjetaUsuarioModel);
        }

        public Tuple<List<SalidaTipoSueldoModel>, List<SalidaDatosTipoSueldoModel>> GetTiposSueldo(EntradaDatosTipoSueldoModel entradaDatosTipoSueldoModel)
        {
            List<SalidaDatosTipoSueldoModel> salidaDatosTipoSueldoModel = new List<SalidaDatosTipoSueldoModel>();
            List<SalidaTipoSueldoModel> salidaTipoSueldoModel = new List<SalidaTipoSueldoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetTiposSueldo", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_sistema"].Value = entradaDatosTipoSueldoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosTipoSueldoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosTipoSueldoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaTipoSueldoModel.Add(new SalidaTipoSueldoModel // Cambio 3: Utilizar paréntesis normales
                    {
                        ID = (int)row["ID"],
                        DESCRIPCION = (string)row["DESCRIPCION"],
                    });
                }


                salidaDatosTipoSueldoModel.Add(new SalidaDatosTipoSueldoModel
                {
                    salidaTipoSueldoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaDatosTipoSueldoModel.Add(new SalidaDatosTipoSueldoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaTipoSueldoModel, salidaDatosTipoSueldoModel);

        }

        public Tuple<List<SalidaModificaSueldoModel>, List<SalidaModificaDatosSueldoModel>> PutSueldo(EntradaModificaDatosSueldoModel entradaModificaDatosSueldoModel)
        {
            List<SalidaModificaSueldoModel> salidaModificaSueldoModel = new List<SalidaModificaSueldoModel>();
            List<SalidaModificaDatosSueldoModel> salidaModificaDatosSueldoModel = new List<SalidaModificaDatosSueldoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_PutSueldo", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_id_sueldo", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_monto_liquido", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_monto_bruto", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_id_tipo_sueldo", SqlDbType.Int, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_id_sueldo"].Value = entradaModificaDatosSueldoModel.idSueldo;
                cmd.Parameters["@vi_monto_liquido"].Value = entradaModificaDatosSueldoModel.montoLiquido;
                cmd.Parameters["@vi_monto_bruto"].Value = entradaModificaDatosSueldoModel.montoBruto;
                cmd.Parameters["@vi_id_tipo_sueldo"].Value = entradaModificaDatosSueldoModel.tipoSueldo;
                cmd.Parameters["@vi_sistema"].Value = entradaModificaDatosSueldoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaModificaDatosSueldoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaModificaDatosSueldoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                salidaModificaDatosSueldoModel.Add(new SalidaModificaDatosSueldoModel
                {
                    salidaModificarSueldoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaModificaDatosSueldoModel.Add(new SalidaModificaDatosSueldoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaModificaSueldoModel, salidaModificaDatosSueldoModel);
        }

        public Tuple<List<SalidaTipoCupoModel>, List<SalidaDatosCupoModel>> GetTiposCupos(EntradaDatosTipoCupoModel entradaDatosTipoCupoModel)
        {
            List<SalidaTipoCupoModel> salidaTipoCupoModel = new List<SalidaTipoCupoModel>();
            List<SalidaDatosCupoModel> salidaDatosCupoModel = new List<SalidaDatosCupoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetTiposCupos", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_sistema"].Value = entradaDatosTipoCupoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosTipoCupoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosTipoCupoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaTipoCupoModel.Add(new SalidaTipoCupoModel // Cambio 3: Utilizar paréntesis normales
                    {
                        ID = (int)row["ID"],
                        DESCRIPCION = (string)row["DESCRIPCION"],
                    });
                }


                salidaDatosCupoModel.Add(new SalidaDatosCupoModel
                {
                    salidaTipoCupoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaDatosCupoModel.Add(new SalidaDatosCupoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaTipoCupoModel, salidaDatosCupoModel);
        }

        public Tuple<List<SalidaTipoTarjetaModel>, List<SalidaDatosTarjetaModel>> GetTiposTarjeta(EntradaDatosTipoTarjetaModel entradaDatosTipoTarjetaModel)
        {
            List<SalidaTipoTarjetaModel> salidaTipoTarjetaModel = new List<SalidaTipoTarjetaModel>();
            List<SalidaDatosTarjetaModel> salidaDatosTarjetaModel = new List<SalidaDatosTarjetaModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("SP_DATOS_TIPO_TARJETA_FINANZA", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_sistema"].Value = entradaDatosTipoTarjetaModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosTipoTarjetaModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosTipoTarjetaModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaTipoTarjetaModel.Add(new SalidaTipoTarjetaModel // Cambio 3: Utilizar paréntesis normales
                    {
                        ID = (int)row["ID"],
                        DESCRIPCION = (string)row["DESCRIPCION"],
                    });
                }


                salidaDatosTarjetaModel.Add(new SalidaDatosTarjetaModel
                {
                    salidaTipoTarjetaVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaDatosTarjetaModel.Add(new SalidaDatosTarjetaModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaTipoTarjetaModel, salidaDatosTarjetaModel);
        }

        public Tuple<List<SalidaBancoModel>, List<SalidaDatosBancoModel>> GetBancos(EntradaDatosBancoModel entradaDatosBancoModel)
        {
            List<SalidaBancoModel> salidaBancoModel = new List<SalidaBancoModel>();
            List<SalidaDatosBancoModel> salidaDatosBancoModel = new List<SalidaDatosBancoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("SP_DATOS_BANCOS_FINANZA", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_sistema"].Value = entradaDatosBancoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosBancoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosBancoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaBancoModel.Add(new SalidaBancoModel // Cambio 3: Utilizar paréntesis normales
                    {
                        ID = (int)row["ID"],
                        DESCRIPCION = (string)row["DESCRIPCION"],
                    });
                }


                salidaDatosBancoModel.Add(new SalidaDatosBancoModel
                {
                    salidaBancoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaDatosBancoModel.Add(new SalidaDatosBancoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaBancoModel, salidaDatosBancoModel);
        }

        public Tuple<List<SalidaDatosTipoMonedaModel>, List<SalidaTipoMonedaModel>> getDatosTipoMoneda(EntradaDatosTipoMonedaModel entradaDatosTipoMonedaModel)
        {
            List<SalidaDatosTipoMonedaModel> salidaDatosTipoMonedaModel = new List<SalidaDatosTipoMonedaModel>();
            List<SalidaTipoMonedaModel> salidaTipoMonedaModel = new List<SalidaTipoMonedaModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("SP_DATOS_TIPO_MONEDA_FINANZA", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_sistema"].Value = entradaDatosTipoMonedaModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosTipoMonedaModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosTipoMonedaModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaDatosTipoMonedaModel.Add(new SalidaDatosTipoMonedaModel // Cambio 3: Utilizar paréntesis normales
                    {
                        ID = (int)row["ID"],
                        DESCRIPCION = (string)row["DESCRIPCION"],
                    });
                }


                salidaTipoMonedaModel.Add(new SalidaTipoMonedaModel
                {
                    salidaTpoMonedaVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaTipoMonedaModel.Add(new SalidaTipoMonedaModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaDatosTipoMonedaModel, salidaTipoMonedaModel);
        }

        public Tuple<List<SalidaDatosTarjetaModificarModel>, List<SalidaTarjetaModificarModel>> putDatosTarjeta(EntradaDatosTarjetaModificarModel entradaDatosTarjetaModificarModel)
        {
            List<SalidaDatosTarjetaModificarModel> salidaDatosTarjetaModificarModel = new List<SalidaDatosTarjetaModificarModel>();
            List<SalidaTarjetaModificarModel> salidaTarjetaModificarModel = new List<SalidaTarjetaModificarModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("SP_PUT_TARJETAS_USUARIO_FINANZA", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_funcion", SqlDbType.VarChar, 25);
                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_tipo_tarjeta", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_banco", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_tipo_cupo", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_cupo_tarjeta", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_moneda", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_fecha_inicio_tarje", SqlDbType.Date, 30);
                cmd.Parameters.Add("@vi_fecha_termino_tarj", SqlDbType.Date, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_funcion"].Value = entradaDatosTarjetaModificarModel.funcion;
                cmd.Parameters["@vi_rut_usuario"].Value = entradaDatosTarjetaModificarModel.rut;
                cmd.Parameters["@vi_tipo_tarjeta"].Value = entradaDatosTarjetaModificarModel.tipoTarjeta;
                cmd.Parameters["@vi_banco"].Value = entradaDatosTarjetaModificarModel.banco;
                cmd.Parameters["@vi_tipo_cupo"].Value = entradaDatosTarjetaModificarModel.tipoCupo;
                cmd.Parameters["@vi_cupo_tarjeta"].Value = entradaDatosTarjetaModificarModel.cupoTarjeta;
                cmd.Parameters["@vi_moneda"].Value = entradaDatosTarjetaModificarModel.moneda;
                cmd.Parameters["@vi_fecha_inicio_tarje"].Value = entradaDatosTarjetaModificarModel.fechaInicio;
                cmd.Parameters["@vi_fecha_termino_tarj"].Value = entradaDatosTarjetaModificarModel.fechaTermino;
                cmd.Parameters["@vi_sistema"].Value = entradaDatosTarjetaModificarModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosTarjetaModificarModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosTarjetaModificarModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                salidaTarjetaModificarModel.Add(new SalidaTarjetaModificarModel
                {
                    salidaModificarTarjetaVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaTarjetaModificarModel.Add(new SalidaTarjetaModificarModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaDatosTarjetaModificarModel, salidaTarjetaModificarModel);
        }

        public Tuple<List<SalidaDatosGastosModel>, List<SalidaGastosModel>> getDatosGasto(EntradaDatosGastosModel entradaDatosGastosModel)
        {
            List<SalidaDatosGastosModel> salidaDatosGastosModel = new List<SalidaDatosGastosModel>();
            List<SalidaGastosModel> salidaGastosModel = new List<SalidaGastosModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("SP_DATOS_GASTOS_FINANZA", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut_usuario"].Value = entradaDatosGastosModel.rutUsuario;
                cmd.Parameters["@vi_sistema"].Value = entradaDatosGastosModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosGastosModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosGastosModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaDatosGastosModel.Add(new SalidaDatosGastosModel // Cambio 3: Utilizar paréntesis normales
                    {
                        TIPO_CUENTA = (string)row["TIPO_CUENTA"],
                        CUENTA = (string)row["CUENTA"],
                        MONTO = (int)row["MONTO"],
                        FECHA_GASTO = (DateTime)row["FECHA_GASTO"],
                        TIPO_TARJETA = (string)row["TIPO_TARJETA"]
                    });
                }


                salidaGastosModel.Add(new SalidaGastosModel
                {
                    salidaDatosGastosVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaGastosModel.Add(new SalidaGastosModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaDatosGastosModel, salidaGastosModel);
        }

        public Tuple<List<SalidaDatosIngresoModel>, List<SalidaIngresoModel>> GetResumenIngresos(EntradaDatosIngresoModel entradaDatosIngresoModel)
        {

            List<SalidaDatosIngresoModel> salidaDatosIngresoModel = new List<SalidaDatosIngresoModel>();
            List<SalidaIngresoModel> salidaIngresoModel = new List<SalidaIngresoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetResumenIngresos", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut_usuario"].Value = entradaDatosIngresoModel.rutUsuario;
                cmd.Parameters["@vi_sistema"].Value = entradaDatosIngresoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosIngresoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosIngresoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaDatosIngresoModel.Add(new SalidaDatosIngresoModel // Cambio 3: Utilizar paréntesis normales
                    {
                        MONTO_INGRESO = (int)row["MONTO_INGRESO"],
                        FECHA_INGRESO = (DateTime)row["FECHA_INGRESO"],
                        TIPO_INGRESO = (string)row["TIPO_INGRESO"],
                        ID_INGRESO = (int)row["ID_INGRESO"]
                    });
                }


                salidaIngresoModel.Add(new SalidaIngresoModel
                {
                    salidaDatosIngresoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaIngresoModel.Add(new SalidaIngresoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaDatosIngresoModel, salidaIngresoModel);
        }

        public Tuple<List<SalidaGrabaDatosSueldoModel>, List<SalidaGrabaSueldoModel>> PostSueldo(EntradaGrabarDatosSueldoModel entradaGrabarDatosSueldoModel)
        {
            List<SalidaGrabaDatosSueldoModel> salidaGrabaDatosSueldoModel = new List<SalidaGrabaDatosSueldoModel>();
            List<SalidaGrabaSueldoModel> salidaGrabaSueldoModel = new List<SalidaGrabaSueldoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_PostSueldo", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_monto_liquido", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_monto_bruto", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_id_tipo_sueldo", SqlDbType.Int, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut_usuario"].Value = entradaGrabarDatosSueldoModel.rut;
                cmd.Parameters["@vi_monto_liquido"].Value = entradaGrabarDatosSueldoModel.montoLiquido;
                cmd.Parameters["@vi_monto_bruto"].Value = entradaGrabarDatosSueldoModel.montoBruto;
                cmd.Parameters["@vi_id_tipo_sueldo"].Value = entradaGrabarDatosSueldoModel.tipoSueldo;
                cmd.Parameters["@vi_sistema"].Value = entradaGrabarDatosSueldoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaGrabarDatosSueldoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaGrabarDatosSueldoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                //DataTable dataTable = new DataTable();
                //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                //sqlDataAdapter.Fill(dataTable);

                salidaGrabaSueldoModel.Add(new SalidaGrabaSueldoModel
                {
                    salidaGrabaSueldoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaGrabaSueldoModel.Add(new SalidaGrabaSueldoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }

            return Tuple.Create(salidaGrabaDatosSueldoModel, salidaGrabaSueldoModel);
        }

        public Tuple<List<SalidaEliminarDatosSueldoModel>, List<SalidaEliminarSueldoModel>> DeleteSueldo(EntradaEliminarDatosSueldoModel entradaEliminarDatosSueldoModel)
        {

            List<SalidaEliminarDatosSueldoModel> salidaEliminarDatosSueldoModel = new List<SalidaEliminarDatosSueldoModel>();
            List<SalidaEliminarSueldoModel> salidaEliminarSueldoModel = new List<SalidaEliminarSueldoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_DeleteSueldo", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_id_sueldo", SqlDbType.Int, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_id_sueldo"].Value = entradaEliminarDatosSueldoModel.idSueldo;
                cmd.Parameters["@vi_sistema"].Value = entradaEliminarDatosSueldoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaEliminarDatosSueldoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaEliminarDatosSueldoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                salidaEliminarSueldoModel.Add(new SalidaEliminarSueldoModel
                {
                    salidaEliminaSueldoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaEliminarSueldoModel.Add(new SalidaEliminarSueldoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaEliminarDatosSueldoModel, salidaEliminarSueldoModel);
        }

        public Tuple<List<SalidaGrabarDatosTarjetaModel>, List<SalidaGrabarTarjetaModel>> PostTarjeta(EntradaGrabarDatosTarjetaModel entradaGrabarDatosTarjetaModel)
        {

            List<SalidaGrabarDatosTarjetaModel> salidaGrabarDatosTarjetaModel = new List<SalidaGrabarDatosTarjetaModel>();
            List<SalidaGrabarTarjetaModel> salidaGrabarTarjetaModel = new List<SalidaGrabarTarjetaModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_PostTarjeta", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_tipo_tarjeta", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_cupo_tarjeta", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_fecha_inicio_tarje", SqlDbType.Date, 30);
                cmd.Parameters.Add("@vi_fecha_termino_tarj", SqlDbType.Date, 30);
                cmd.Parameters.Add("@vi_banco", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_tipo_cupo", SqlDbType.Int, 30);


                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut_usuario"].Value = entradaGrabarDatosTarjetaModel.rut;
                cmd.Parameters["@vi_tipo_tarjeta"].Value = entradaGrabarDatosTarjetaModel.tipoTarjeta;
                cmd.Parameters["@vi_cupo_tarjeta"].Value = entradaGrabarDatosTarjetaModel.cupoTarjeta;
                cmd.Parameters["@vi_fecha_inicio_tarje"].Value = entradaGrabarDatosTarjetaModel.fechaInicio;
                cmd.Parameters["@vi_fecha_termino_tarj"].Value = entradaGrabarDatosTarjetaModel.fechaTermino;
                cmd.Parameters["@vi_banco"].Value = entradaGrabarDatosTarjetaModel.banco;
                cmd.Parameters["@vi_tipo_cupo"].Value = entradaGrabarDatosTarjetaModel.tipoCupo;
                cmd.Parameters["@vi_sistema"].Value = entradaGrabarDatosTarjetaModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaGrabarDatosTarjetaModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaGrabarDatosTarjetaModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                salidaGrabarTarjetaModel.Add(new SalidaGrabarTarjetaModel
                {
                    salidaDatosTarjetaVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaGrabarTarjetaModel.Add(new SalidaGrabarTarjetaModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaGrabarDatosTarjetaModel, salidaGrabarTarjetaModel);
        }

        public Tuple<List<SalidaModificaDatosTarjetaModel>, List<SalidaModificaTarjetaModel>> PutTarjeta(EntradaModificaDatosTarjetaModel entradaModificaDatosTarjetaModel)
        {

            List<SalidaModificaDatosTarjetaModel> salidaModificaDatosTarjetaModel = new List<SalidaModificaDatosTarjetaModel>();
            List<SalidaModificaTarjetaModel> salidaModificaTarjetaModel = new List<SalidaModificaTarjetaModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_PutTarjeta", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_tipo_tarjeta", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_cupo_tarjeta", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_fecha_inicio_tarje", SqlDbType.Date, 30);
                cmd.Parameters.Add("@vi_fecha_termino_tarj", SqlDbType.Date, 30);
                cmd.Parameters.Add("@vi_banco", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_tipo_cupo", SqlDbType.Int, 30);


                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut_usuario"].Value = entradaModificaDatosTarjetaModel.rut;
                cmd.Parameters["@vi_tipo_tarjeta"].Value = entradaModificaDatosTarjetaModel.tipoTarjeta;
                cmd.Parameters["@vi_cupo_tarjeta"].Value = entradaModificaDatosTarjetaModel.cupoTarjeta;
                cmd.Parameters["@vi_fecha_inicio_tarje"].Value = entradaModificaDatosTarjetaModel.fechaInicio;
                cmd.Parameters["@vi_fecha_termino_tarj"].Value = entradaModificaDatosTarjetaModel.fechaTermino;
                cmd.Parameters["@vi_banco"].Value = entradaModificaDatosTarjetaModel.banco;
                cmd.Parameters["@vi_tipo_cupo"].Value = entradaModificaDatosTarjetaModel.tipoCupo;
                cmd.Parameters["@vi_sistema"].Value = entradaModificaDatosTarjetaModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaModificaDatosTarjetaModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaModificaDatosTarjetaModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                salidaModificaTarjetaModel.Add(new SalidaModificaTarjetaModel
                {
                    salidaModificaTarjetaVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaModificaTarjetaModel.Add(new SalidaModificaTarjetaModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaModificaDatosTarjetaModel, salidaModificaTarjetaModel);
        }

        public Tuple<List<SalidaEliminaDatosTarjetaModel>, List<SalidaEliminaTarjetaModel>> DeleteTarjeta(EntradaEliminaDatosTarjetaModel entradaEliminaDatosTarjetaModel)
        {

            List<SalidaEliminaDatosTarjetaModel> salidaEliminaDatosTarjetaModel = new List<SalidaEliminaDatosTarjetaModel>();
            List<SalidaEliminaTarjetaModel> salidaEliminaTarjetaModel = new List<SalidaEliminaTarjetaModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_DeleteTarjeta", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_tarjeta_id", SqlDbType.Int, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_tarjeta_id"].Value = entradaEliminaDatosTarjetaModel.idTarjeta;
                cmd.Parameters["@vi_sistema"].Value = entradaEliminaDatosTarjetaModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaEliminaDatosTarjetaModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaEliminaDatosTarjetaModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                salidaEliminaTarjetaModel.Add(new SalidaEliminaTarjetaModel
                {
                    salidaEliminaTarjetaVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaEliminaTarjetaModel.Add(new SalidaEliminaTarjetaModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaEliminaDatosTarjetaModel, salidaEliminaTarjetaModel);
        }

        public Tuple<List<SalidaDatosTiposIngresoModel>, List<SalidaTiposIngresoModel>> GetTiposIngreso(EntradaDatosTiposIngresoModel entradaDatosTiposIngresoModel)
        {
            List<SalidaDatosTiposIngresoModel> salidaDatosTiposIngresoModel = new List<SalidaDatosTiposIngresoModel>();
            List<SalidaTiposIngresoModel> salidaTiposIngresoModel = new List<SalidaTiposIngresoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetTiposIngreso", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_sistema"].Value = entradaDatosTiposIngresoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaDatosTiposIngresoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaDatosTiposIngresoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaDatosTiposIngresoModel.Add(new SalidaDatosTiposIngresoModel // Cambio 3: Utilizar paréntesis normales
                    {
                        ID = (int)row["ID"],
                        DESCRIPCION = (string)row["DESCRIPCION"]
                    });
                }


                salidaTiposIngresoModel.Add(new SalidaTiposIngresoModel
                {
                    salidaTiposIngresoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaTiposIngresoModel.Add(new SalidaTiposIngresoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaDatosTiposIngresoModel, salidaTiposIngresoModel);
        }

        public Tuple<List<SalidaGrabarDatosIngresoModel>, List<SalidaGrabarIngresoModel>> PostIngreso(EntradaGrabarDatosIngresoModel entradaGrabarDatosIngresoModel)
        {

            List<SalidaGrabarDatosIngresoModel> salidaGrabarDatosIngresoModel = new List<SalidaGrabarDatosIngresoModel>();
            List<SalidaGrabarIngresoModel> salidaGrabarIngresoModel = new List<SalidaGrabarIngresoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_PostIngreso", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_monto", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_fecha_ingreso", SqlDbType.Date, 30);
                cmd.Parameters.Add("@vi_tipo_ingreso", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut_usuario"].Value = entradaGrabarDatosIngresoModel.rut;
                cmd.Parameters["@vi_monto"].Value = entradaGrabarDatosIngresoModel.monto;
                cmd.Parameters["@vi_fecha_ingreso"].Value = entradaGrabarDatosIngresoModel.fechaIngreso;
                cmd.Parameters["@vi_tipo_ingreso"].Value = entradaGrabarDatosIngresoModel.tipoIngreso;
                cmd.Parameters["@vi_sistema"].Value = entradaGrabarDatosIngresoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaGrabarDatosIngresoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaGrabarDatosIngresoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                salidaGrabarIngresoModel.Add(new SalidaGrabarIngresoModel
                {
                    salidaGrabarIngresoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaGrabarIngresoModel.Add(new SalidaGrabarIngresoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaGrabarDatosIngresoModel, salidaGrabarIngresoModel);
        }


        //-----------------

        public Tuple<List<SalidaGrabarDatosGastosModel>, List<SalidaGrabarGastosModel>> PostGasto(EntradaGrabarDatosGastosModel entradaGrabarDatosGastosModel)
        {

            List<SalidaGrabarDatosGastosModel> salidaGrabarDatosGastosModel = new List<SalidaGrabarDatosGastosModel>();
            List<SalidaGrabarGastosModel> salidaGrabarGastosModel = new List<SalidaGrabarGastosModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_PostGasto", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_tarjeta", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_descripcion_gasto", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_monto", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_fecha_gasto", SqlDbType.Date, 30);
                cmd.Parameters.Add("@vi_tipo_gastos", SqlDbType.Int, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut"].Value = entradaGrabarDatosGastosModel.rut;
                cmd.Parameters["@vi_tarjeta"].Value = entradaGrabarDatosGastosModel.tipoTarjeta;
                cmd.Parameters["@vi_descripcion_gasto"].Value = entradaGrabarDatosGastosModel.descripcionGasto;
                cmd.Parameters["@vi_monto"].Value = entradaGrabarDatosGastosModel.monto;
                cmd.Parameters["@vi_fecha_gasto"].Value = entradaGrabarDatosGastosModel.fechaGasto;
                cmd.Parameters["@vi_tipo_gastos"].Value = entradaGrabarDatosGastosModel.tipoCuenta;
                cmd.Parameters["@vi_sistema"].Value = entradaGrabarDatosGastosModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaGrabarDatosGastosModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaGrabarDatosGastosModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                //DataTable dataTable = new DataTable();
                //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                //sqlDataAdapter.Fill(dataTable);

                salidaGrabarGastosModel.Add(new SalidaGrabarGastosModel
                {
                    salidaGrabarGastosVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaGrabarGastosModel.Add(new SalidaGrabarGastosModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaGrabarDatosGastosModel, salidaGrabarGastosModel);
        }

        public Tuple<List<SalidaResumenDatosGastosModel>, List<SalidaResumenGastosModel>> GetResumenGastos(EntradaResumenDatosGastosModel entradaResumenDatosGastosModel)
        {

            List<SalidaResumenDatosGastosModel> salidaResumenDatosGastosModel = new List<SalidaResumenDatosGastosModel>();
            List<SalidaResumenGastosModel> salidaResumenGastosModel = new List<SalidaResumenGastosModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetResumenGastos", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut_usuario", SqlDbType.Int, 20);
                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut_usuario"].Value = entradaResumenDatosGastosModel.rut;
                cmd.Parameters["@vi_sistema"].Value = entradaResumenDatosGastosModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaResumenDatosGastosModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaResumenDatosGastosModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaResumenDatosGastosModel.Add(new SalidaResumenDatosGastosModel // Cambio 3: Utilizar paréntesis normales
                    {
                        tipoGasto = (string)row["TIPO_GASTO"],
                        detalleGasto = (string)row["DETALLE_GASTO"],
                        montoGasto = (int)row["MONTO_GASTO"],
                        fechaGasto = (DateTime)row["FECHA_GASTO"],
                        tipoTarjeta = (string)row["TIPO_TARJETA"],
                        idGasto = (int)row["ID_GASTO"],
                    });
                }

                salidaResumenGastosModel.Add(new SalidaResumenGastosModel
                {
                    salidaResumenGastosVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaResumenGastosModel.Add(new SalidaResumenGastosModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaResumenDatosGastosModel, salidaResumenGastosModel);
        }

        public Tuple<List<SalidaTipoGastoDatosModel>, List<SalidaTipoGastoModel>> GetTiposGasto(EntradaTipoGastoDatosModel entradaTipoGastoDatosModel)
        {

            List<SalidaTipoGastoDatosModel> salidaTipoGastoDatosModel = new List<SalidaTipoGastoDatosModel>();
            List<SalidaTipoGastoModel> salidaTipoGastoModel = new List<SalidaTipoGastoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetTiposGasto", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_sistema"].Value = entradaTipoGastoDatosModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaTipoGastoDatosModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaTipoGastoDatosModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaTipoGastoDatosModel.Add(new SalidaTipoGastoDatosModel // Cambio 3: Utilizar paréntesis normales
                    {
                        idTipoGasto = (int)row["ID"],
                        descripcion = (string)row["DESCRIPCION"]
                    });
                }

                salidaTipoGastoModel.Add(new SalidaTipoGastoModel
                {
                    salidaTipoGastoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaTipoGastoModel.Add(new SalidaTipoGastoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaTipoGastoDatosModel, salidaTipoGastoModel);
        }

        public Tuple<List<SalidaTarjetasDatosModel>, List<SalidaTarjetasModel>> GetTarjetasUsuario(EntradaTarjetasDatosModel entradaTarjetasDatosModel)
        {

            List<SalidaTarjetasDatosModel> salidaTarjetasDatosModel = new List<SalidaTarjetasDatosModel>();
            List<SalidaTarjetasModel> salidaTarjetasModel = new List<SalidaTarjetasModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_GetTarjetasUsuario", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_rut", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_rut"].Value = entradaTarjetasDatosModel.rut;
                cmd.Parameters["@vi_sistema"].Value = entradaTarjetasDatosModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaTarjetasDatosModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaTarjetasDatosModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    salidaTarjetasDatosModel.Add(new SalidaTarjetasDatosModel // Cambio 3: Utilizar paréntesis normales
                    {
                        idTarjeta = (int)row["ID_TARJETA"],
                        descripcion = (string)row["DESCRIPCION"]
                    });
                }

                salidaTarjetasModel.Add(new SalidaTarjetasModel
                {
                    salidaTarjetasVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaTarjetasModel.Add(new SalidaTarjetasModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaTarjetasDatosModel, salidaTarjetasModel);
        }

        public Tuple<List<SalidaModificaGastoDatosModel>, List<SalidaModificaGastoModel>> PutGasto(EntradaModificaGastoDatosModel entradaModificaGastoDatosModel)
        {

            List<SalidaModificaGastoDatosModel> salidaModificaGastoDatosModel = new List<SalidaModificaGastoDatosModel>();
            List<SalidaModificaGastoModel> salidaModificaGastoModel = new List<SalidaModificaGastoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_PutGasto", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_descripcion", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_monto", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_fecha_gasto", SqlDbType.Date, 30);
                cmd.Parameters.Add("@vi_tipo_cuenta", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_id_gasto", SqlDbType.Int, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_descripcion"].Value = entradaModificaGastoDatosModel.descripcion;
                cmd.Parameters["@vi_monto"].Value = entradaModificaGastoDatosModel.monto;
                cmd.Parameters["@vi_fecha_gasto"].Value = entradaModificaGastoDatosModel.fechaGasto;
                cmd.Parameters["@vi_tipo_cuenta"].Value = entradaModificaGastoDatosModel.tipoCuenta;
                cmd.Parameters["@vi_id_gasto"].Value = entradaModificaGastoDatosModel.idGasto;

                cmd.Parameters["@vi_sistema"].Value = entradaModificaGastoDatosModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaModificaGastoDatosModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaModificaGastoDatosModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                salidaModificaGastoModel.Add(new SalidaModificaGastoModel
                {
                    salidaGastosVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });


            }
            catch (SqlException sqlException)
            {
                salidaModificaGastoModel.Add(new SalidaModificaGastoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaModificaGastoDatosModel, salidaModificaGastoModel);
        }

        public Tuple<List<SalidaEliminarGastoDatosModel>, List<SalidaEliminarGastoModel>> DeleteGasto(EntradaEliminarGastoDatosModel entradaEliminarGastoDatosModel)
        {

            List<SalidaEliminarGastoDatosModel> salidaEliminarGastoDatosModel = new List<SalidaEliminarGastoDatosModel>();
            List<SalidaEliminarGastoModel> salidaEliminarGastoModel = new List<SalidaEliminarGastoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_DeleteGasto", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.Add("@vi_id_gasto", SqlDbType.Int, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                               
                cmd.Parameters["@vi_id_gasto"].Value = entradaEliminarGastoDatosModel.idGasto;

                cmd.Parameters["@vi_sistema"].Value = entradaEliminarGastoDatosModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaEliminarGastoDatosModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaEliminarGastoDatosModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                salidaEliminarGastoModel.Add(new SalidaEliminarGastoModel
                {
                    salidaEliminarGastosVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaEliminarGastoModel.Add(new SalidaEliminarGastoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaEliminarGastoDatosModel, salidaEliminarGastoModel);
        }

        public Tuple<List<SalidaModificaDatosIngresoModel>, List<SalidaModificaIngresoModel>> PutIngreso(EntradaModificaDatosIngresoModel entradaModificaDatosIngresoModel)
        {

            List<SalidaModificaDatosIngresoModel> salidaModificaDatosIngresoModel = new List<SalidaModificaDatosIngresoModel>();
            List<SalidaModificaIngresoModel> salidaModificaIngresoModel = new List<SalidaModificaIngresoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_PutIngreso", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@vi_monto", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_fecha_ingreso", SqlDbType.Date, 30);
                cmd.Parameters.Add("@vi_tipo_ingreso", SqlDbType.Int, 30);
                cmd.Parameters.Add("@vi_id_ingreso", SqlDbType.Int, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters["@vi_monto"].Value = entradaModificaDatosIngresoModel.monto;
                cmd.Parameters["@vi_fecha_ingreso"].Value = entradaModificaDatosIngresoModel.fechaIngreso;
                cmd.Parameters["@vi_tipo_ingreso"].Value = entradaModificaDatosIngresoModel.tipoIngreso;
                cmd.Parameters["@vi_id_ingreso"].Value = entradaModificaDatosIngresoModel.idIngreso;

                cmd.Parameters["@vi_sistema"].Value = entradaModificaDatosIngresoModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaModificaDatosIngresoModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaModificaDatosIngresoModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                salidaModificaIngresoModel.Add(new SalidaModificaIngresoModel
                {
                    salidaModificaIngresoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaModificaIngresoModel.Add(new SalidaModificaIngresoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaModificaDatosIngresoModel, salidaModificaIngresoModel);
        }

        public Tuple<List<SalidaEliminarIngresoDatosModel>, List<SalidaEliminarIngresoModel>> DeleteIngreso(EntradaEliminarIngresoDatosModel entradaEliminarIngresoDatosModel)
        {

            List<SalidaEliminarIngresoDatosModel> salidaEliminarIngresoDatosModel = new List<SalidaEliminarIngresoDatosModel>();
            List<SalidaEliminarIngresoModel> salidaEliminarIngresoModel = new List<SalidaEliminarIngresoModel>();

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("usp_DeleteIngreso", conn);

            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.Add("@vi_id_ingreso", SqlDbType.Int, 30);

                cmd.Parameters.Add("@vi_sistema", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_metodo", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vi_tipo_login", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@vo_valido", SqlDbType.VarChar, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_codigo", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_tipo_salida", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                
                cmd.Parameters["@vi_id_ingreso"].Value = entradaEliminarIngresoDatosModel.idIngreso;

                cmd.Parameters["@vi_sistema"].Value = entradaEliminarIngresoDatosModel.sistema;
                cmd.Parameters["@vi_metodo"].Value = entradaEliminarIngresoDatosModel.metodo;
                cmd.Parameters["@vi_tipo_login"].Value = entradaEliminarIngresoDatosModel.tipoLogin;

                conn.Open();
                cmd.ExecuteNonQuery();

                salidaEliminarIngresoModel.Add(new SalidaEliminarIngresoModel
                {
                    salidaEliminarIngresoVigenciaModel = new SalidaVigenciaModel
                    {
                        vigente = (string)cmd.Parameters["@vo_valido"].Value
                    },
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)cmd.Parameters["@vo_codigo"].Value,
                        tipo = (string)cmd.Parameters["@vo_tipo_salida"].Value,
                        mensaje = (string)cmd.Parameters["@vo_mensaje"].Value
                    }
                });

            }
            catch (SqlException sqlException)
            {
                salidaEliminarIngresoModel.Add(new SalidaEliminarIngresoModel
                {
                    respuestaBDModel = new RespuestaBDModel
                    {
                        codigo = (int)-100,
                        tipo = (string)"Error comunicación SQL",
                        mensaje = (string)Convert.ToString(sqlException)
                    }
                });
            }
            finally
            {
                conn.Close();
            }
            return Tuple.Create(salidaEliminarIngresoDatosModel, salidaEliminarIngresoModel);
        }

    }
}
