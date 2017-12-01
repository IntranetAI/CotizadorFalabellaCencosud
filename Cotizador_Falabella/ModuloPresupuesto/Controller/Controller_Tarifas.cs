using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Cotizador_Falabella.ModuloPresupuesto.Model;

namespace Cotizador_Falabella.ModuloPresupuesto.Controller
{
    public class Controller_Tarifas
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
                    cmd.Parameters.AddWithValue("@Empresa",Empresa);
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

        public double TarifaCostoBarnizAcuoso(int Gramaje, string Formato, string Maquina, string Empresa)
        {
            double Resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_BarnizAcuoso_Variable";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Gramaje", Gramaje);
                    cmd.Parameters.AddWithValue("@Maquina", Maquina);
                    cmd.Parameters.AddWithValue("@Formato", Formato);
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

        public double TarifaImpresionTapaVariable(string Formato)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                cmd.CommandText = "Tarifa_ImpresionTapa_variable";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Formato", Formato);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resultado = Convert.ToDouble(reader["CostoVariable"].ToString());
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public List<Tarifa_Papel> Listar_QuintoColor()
        {
            List<Tarifa_Papel> lista = new List<Tarifa_Papel>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                cmd.CommandText = "PPTO_ListarQuintoColor";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tarifa_Papel papel = new Tarifa_Papel();
                    papel.NombrePapel = reader["Paginas"].ToString();
                    lista.Add(papel);
                }
            }
            con.CerrarConexion();
            return lista;
        }

        public double TarifaPlizadoTapaVariable(string Formato, int PagTapas, string Empresa)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                cmd.CommandText = "Tarifa_PlizadoTapa_Variable";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Formato", Formato);
                cmd.Parameters.AddWithValue("@Entradas", PagTapas);
                cmd.Parameters.AddWithValue("@Empresa", Empresa);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resultado = Convert.ToDouble(reader["CostoVariable"].ToString());
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifaEncuadernacion(string NombreEncuadernacion, int CantidadEntradas, string TipoCosto, string Empresa)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Encuadernacion_Costos";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreEncuadernacion", NombreEncuadernacion);
                    cmd.Parameters.AddWithValue("@CantidadEntradas", CantidadEntradas);
                    cmd.Parameters.AddWithValue("@TipoCosto", TipoCosto);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["CostoEncuadernacion"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifaPreprensa(string NombrePrePrensa, string Formato, string Empresa)
        {
            double Resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                cmd.CommandText = "Tarifa_Preprensa_Costo";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombrePreprensa", NombrePrePrensa);
                cmd.Parameters.AddWithValue("@Formato", Formato);
                cmd.Parameters.AddWithValue("@Empresa", Empresa);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Resultado = Convert.ToDouble(reader["CostoPrePrensa"].ToString());
                }
            }
            con.CerrarConexion();
            return Resultado;
        }

        public double TarifaPreprensaTapa(string NombrePrePrensa, string Empresa)
        {
            double Resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                cmd.CommandText = "Tarifa_Preprensa_CostoTapa";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombrePreprensa", NombrePrePrensa);
                cmd.Parameters.AddWithValue("@Empresa", Empresa);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Resultado = Convert.ToDouble(reader["CostoPrePrensa"].ToString());
                }
            }
            con.CerrarConexion();
            return Resultado;
        }

        public double TarifaTerminacionTapasSelectivo(string NombreTerminacion, string Formato, string TipoCosto, string Empresa)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Terminaciones_TapasSelectivo";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Terminacion", NombreTerminacion);
                    cmd.Parameters.AddWithValue("@Formato", Formato);
                    cmd.Parameters.AddWithValue("@TipoCosto", TipoCosto);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["CostoTerminacion"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifaTerminacionTapasMtCuadrados(string NombreTerminacion, string Formato, int Entradas, string Empresa)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Terminaciones_TapasMtCuadrado";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Terminacion", NombreTerminacion);
                    cmd.Parameters.AddWithValue("@Formato", Formato);
                    cmd.Parameters.AddWithValue("@Entrada", Entradas);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["CostoTerminacion"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifaDespacho(string Proceso, string TipoCosto,int CantVersiones)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Despacho_Costos";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Proceso", Proceso);
                    cmd.Parameters.AddWithValue("@TipoCosto",TipoCosto);
                    cmd.Parameters.AddWithValue("@CantVersion",CantVersiones);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["Costo"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifaDespacho_Espesor(string TipoPapel, int Gramaje)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Despacho_Espesor";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoPapel", TipoPapel);
                    cmd.Parameters.AddWithValue("@Gramaje", Gramaje);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["Cant_Espesor_mm"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifaDespacho_Limites(string NombreLimite, string Empresa)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Despacho_Limites";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreLimite", NombreLimite);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["Valor"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public Vehiculo TarifaDespacho_Vehiculo(string TipoVehiculo, string Empresa)
        {
            Vehiculo resultado = new Vehiculo();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Vehiculo_Flete_Cap";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoVehiculo", TipoVehiculo);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado.NombreVehiculo = reader["TipoVehiculo"].ToString();
                        resultado.Costo_Flete = Convert.ToInt32(reader["Costo_Flete"].ToString());
                        resultado.Capacidad = Convert.ToInt32(reader["Capacidad"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public Medios TarifaDespacho_Medios(string Empresa)
        {
            Medios resultado = new Medios();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "TarifaDespacho_Medios";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado.ProcentajeMedios = Convert.ToDouble(reader["Porcentaje_Medios"].ToString());
                        resultado.Cant_Santiago = Convert.ToInt32(reader["Cant_LocalesSantiago"].ToString());
                        resultado.Cant_Stg_abc1 = Convert.ToInt32(reader["Cant_LocalesStg_ABC1"].ToString());
                        resultado.Cant_Stg_c2c3 = Convert.ToInt32(reader["Cant_LocalesStg_C2C3"].ToString());
                        resultado.Cant_Regiones = Convert.ToInt32(reader["Cant_LocalesRegiones"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public int TarifaDespacho_CostoEmbalaje(string Empresa)
        {
            int resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "TarifaDespacho_CostoEmbalaje";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToInt32(reader["CostoEmbalaje"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifaDespacho_CostoProduccion(string Empresa)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "TarifaDespacho_CostoProduccion";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["CostoProduccion"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifapapelMermaFija(string Formato, string Maquina, string TipoComponente, int Gramaje, int Entradas)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Merma_Fijo";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Formato", Formato);
                    cmd.Parameters.AddWithValue("@Maquina", Maquina);
                    cmd.Parameters.AddWithValue("@TipoComponente", TipoComponente);
                    cmd.Parameters.AddWithValue("@Gramaje", Gramaje);
                    cmd.Parameters.AddWithValue("@Entradas", Entradas);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["CostoMermaFija"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifapapelMermaVariable(string Formato, string Maquina, string TipoComponente, int Gramaje, double Entradas,int Tiraje)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Tarifa_Merma_Variable";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Formato", Formato);
                    cmd.Parameters.AddWithValue("@Maquina", Maquina);
                    cmd.Parameters.AddWithValue("@TipoComponente", TipoComponente);
                    cmd.Parameters.AddWithValue("@Gramaje", Gramaje);
                    cmd.Parameters.AddWithValue("@Entradas", Entradas);
                    cmd.Parameters.AddWithValue("@Tiraje", Tiraje);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["CostoMermaVariable"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }
        
        public double TarifapapelPrecioPapel(int Gramaje, string Maquina, string TipoComponente, string NombreTipoPapel, string Empresa)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Carga_CostoPapel";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Gramaje", Gramaje);
                    cmd.Parameters.AddWithValue("@Maquina", Maquina);
                    cmd.Parameters.AddWithValue("@Componente", TipoComponente);
                    cmd.Parameters.AddWithValue("@NombreTipoPapel", NombreTipoPapel);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["CostoPapel"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public double TarifapapelPrecioPapel_idPPTO(int Gramaje, string Maquina, string TipoComponente, string NombreTipoPapel, string Empresa, int IDPPTO)
        {
            double resultado = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Carga_CostoPapel_idPPTO";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Gramaje", Gramaje);
                    cmd.Parameters.AddWithValue("@Maquina", Maquina);
                    cmd.Parameters.AddWithValue("@Componente", TipoComponente);
                    cmd.Parameters.AddWithValue("@NombreTipoPapel", NombreTipoPapel);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    cmd.Parameters.AddWithValue("@IDPPTO", IDPPTO);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = Convert.ToDouble(reader["CostoPapel"].ToString());
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