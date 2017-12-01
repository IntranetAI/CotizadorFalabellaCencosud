using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Cotizador_Cencosud.ModuloPresupuesto.Model;

namespace Cotizador_Cencosud.ModuloPresupuesto.Controller
{
    public class Controller_Tarifas
    {
        public int BuscarPaginasxPliegoFormato(string Formato, int CantidadPaginas)
        {
            int Resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "TarImpInt";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Paginas", CantidadPaginas);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Resultado = Convert.ToInt32(reader["Costo"].ToString());
                    }

                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return Resultado;
        }

        public double TarifaCostoImpresion(string Paginas, string Doblez, string Maquina, string TipoCosto)
        {
            double Resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "TarImpInt";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Paginas", Paginas);
                    cmd.Parameters.AddWithValue("@Doblez", Doblez);
                    cmd.Parameters.AddWithValue("@Maquina", Maquina);
                    cmd.Parameters.AddWithValue("@TipoCosto", TipoCosto);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Resultado = Convert.ToDouble(reader["Costo"].ToString());
                    }

                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return Resultado;
        }

        public Tarifa_Papel TarifaCostoPapel(int Gramaje, string Componente, string Maquina, string TipoPapel, string Empresa, string Formato)
        {
            Tarifa_Papel papel = new Tarifa_Papel();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Carga_CostoPapel";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Gramaje", Gramaje);
                    cmd.Parameters.AddWithValue("@Componente", Componente);
                    cmd.Parameters.AddWithValue("@Maquina", Maquina);
                    cmd.Parameters.AddWithValue("@NombreTipoPapel", TipoPapel);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    cmd.Parameters.AddWithValue("@Formato", Formato);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        papel.PrecioPapel = Convert.ToDouble(reader["PrecioPapel"].ToString());
                        papel.TarifaMermaFija = Convert.ToInt32(reader["CostoFijoPapel"].ToString());
                        papel.TarifaMermaVariable = Convert.ToDouble(reader["CostoVariablePapel"].ToString());
                    }

                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return papel;
        }

        public int TarifaEncuadernacion(string TipoEnc, string proceso)
        {
            int result = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "TarEncuader";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Tipo", TipoEnc);
                    cmd.Parameters.AddWithValue("@Proc", proceso);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = Convert.ToInt32(reader["Costo"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return result;
        }

        public double TarifaCostoImpresionTapaBarniz(int Gramaje, string Formato)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "TarifaImpresionTapasBarniz";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Gramaje", Gramaje);
                    cmd.Parameters.AddWithValue("@Formato", Formato);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["CostoBarniz"].ToString());
                    }
                }
                catch
                {

                }
            }
            con.CerrarConexion();
            return resultado;
        }
    }
}