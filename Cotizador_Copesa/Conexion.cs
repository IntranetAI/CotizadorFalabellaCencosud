using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Cotizador_Copesa
{
    public class Conexion
    {
        SqlConnection sqlconection = null;

        public SqlCommand AbrirConexionPPTO()
        {
            try
            {
                string conectionstring =
                    ConfigurationManager.ConnectionStrings["PPTO_Copesa"].ToString();
                sqlconection = new SqlConnection(conectionstring);
                sqlconection.Open();

                SqlCommand cmd = sqlconection.CreateCommand();
                return cmd;

            }
            catch
            {
                return null;
            }
        }

        public void CerrarConexion()
        {
            sqlconection.Close();
        }
    }
}