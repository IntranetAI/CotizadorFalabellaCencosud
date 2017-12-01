using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Copesa.ModuloPresupuesto.Model;
using System.Data.SqlClient;
using Cotizador_Copesa.ModuloPresupuesto.View;

namespace Cotizador_Copesa.ModuloPresupuesto.Controller
{
    public class Controller_Tarifa
    {
        public double TarifaCostoImpresion(string Paginas, string Doblez, string Maquina, string TipoCosto, string Empresa)
        {
            double Resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Impresion_BuscarCosto";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Paginas", Paginas);
                    cmd.Parameters.AddWithValue("@Doblez", Doblez);
                    cmd.Parameters.AddWithValue("@Maquina", Maquina);
                    cmd.Parameters.AddWithValue("@TipoCosto", TipoCosto);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
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

        public List<Impresion> ListarImpresion(string Maquina)
        {
            List<Impresion> lista = new List<Impresion>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "PPTO_ListarImpresiones";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Maquina", Maquina);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Impresion imp = new Impresion();
                        imp.TipoCosto = reader["TipoCosto"].ToString();
                        imp.Paginas = reader["Paginas"].ToString();
                        imp.Maquina = reader["Maquina"].ToString();
                        imp.Costo = Convert.ToDouble(reader["Costo"].ToString());
                        lista.Add(imp);
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return lista;
        }

        public Encuadernacion ListarEncuadernacion(string NombreEnc)
        {
            Encuadernacion lista = new Encuadernacion();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Encuadernacion_Costos";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreEncuadernacion", NombreEnc);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.ValorFijoEnc = Convert.ToDouble(reader["ValorFijoEnc"].ToString());
                        lista.ValorVariEnc = Convert.ToDouble(reader["ValorVariableEnc"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return lista;
        }

        public List<Terminaciones> Listar_Terminaciones()
        {
            List<Terminaciones> lista = new List<Terminaciones>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "PPTO_ListarTerminacion";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Terminaciones t = new Terminaciones();
                        t.NombreTerminacion = reader["Nombre_Terminacion"].ToString();
                        t.CostoFijo = Convert.ToDouble(reader["CostoFijoTerm"]);
                        t.CostoVari = Convert.ToDouble(reader["CostoVariTerm"]);
                        t.Doblez = reader["Doblez"].ToString();
                        t.TipoTerm = reader["TipoTerminacion"].ToString();
                        lista.Add(t);
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return lista;
        }

        public double ValorUF()
        {
            double Resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_ValorUF";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Resultado = Convert.ToDouble(reader["Valor"].ToString());
                        
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return Resultado;
        }

        public bool IngresoValorUfDelDia(double valor)
        {
            Boolean resultado = false;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "ValorUF_Insert";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Valor",valor);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToBoolean(reader["Resultado"].ToString());
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