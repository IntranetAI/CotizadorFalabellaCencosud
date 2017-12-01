using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Falabella.ModuloPresupuesto.Model;
using System.Data.SqlClient;

namespace Cotizador_Falabella.ModuloPresupuesto.Controller
{
    public class Controller_Tirajes
    {
        public List<PPTO_Tirajes> ListarTirajes_ID(int id)
        {
            List<PPTO_Tirajes> lista = new List<PPTO_Tirajes>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "ListarTirajes_IDPPTO";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPPTO", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PPTO_Tirajes tirajes = new PPTO_Tirajes();
                        tirajes.IDTiraje = Convert.ToInt32(reader["id_pptoTiraje"].ToString());
                        tirajes.NombreTiraje = reader["NombreTiraje"].ToString();
                        tirajes.Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                        tirajes.TirajeNombreExtendido = tirajes.NombreTiraje + " - Cantidad: " + tirajes.Cantidad.ToString("N0").Replace(",", ".");
                        tirajes.CostoTotal = Convert.ToDouble(reader["CostoTotal"].ToString());
                        tirajes.Costounitario = Convert.ToDouble(reader["CostoUnitario"].ToString());
                        lista.Add(tirajes);
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return lista;
        }
    }
}