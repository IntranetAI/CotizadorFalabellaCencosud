using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Falabella.ModuloPresupuesto.Model;
using System.Data.SqlClient;

namespace Cotizador_Falabella.ModuloPresupuesto.Controller
{
    public class Controller_ValorTrimestre
    {
        public ValorDolar_Trimestral BuscarDolar_ID(int id)
        {
            ValorDolar_Trimestral dolar = new ValorDolar_Trimestral();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "PPTO_BuscarDolarTri_ID";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dolar.NombreTrimestre = reader["NombreTrimestre"].ToString();
                        dolar.ValorTrimestre = Convert.ToDouble(reader["ValorTrimestre"].ToString());
                        dolar.FechaInicio = Convert.ToDateTime(reader["FechaInicio"].ToString());
                        dolar.FechaTermino = Convert.ToDateTime(reader["FechaTermino"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return dolar;
        }
    }
}