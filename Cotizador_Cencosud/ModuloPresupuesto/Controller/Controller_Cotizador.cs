using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Cencosud.ModuloPresupuesto.Model;
using System.Data.SqlClient;
using Cotizador_Cencosud.ModuloUsuario.Modelo;
using Cotizador_Cencosud.ModuloUsuario.Controller;

namespace Cotizador_Cencosud.ModuloPresupuesto.Controller
{
    public class Controller_Cotizador
    {
        public List<Cotizador> Listar_Formato()
        {
            List<Cotizador> lista = new List<Cotizador>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Pre_ListarFormatos";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cotizador p = new Cotizador();
                        p.Formato = reader["nombre"].ToString();
                        p.PaginasInt = Convert.ToInt32(reader["no_paginas_x_pliego"].ToString());
                        lista.Add(p);
                    }

                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return lista;
        }

        public List<Cotizador> Listar_Paginas_Interior(string Doblez)
        {
            List<Cotizador> lista = new List<Cotizador>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Pre_ListarPaginasInterior";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cotizador p = new Cotizador();
                        p.Formato = reader["Cant_Paginas"].ToString();
                        
                        lista.Add(p);
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return lista;
        }

        public List<Cotizador> List_Papel(string Empresa, string Tipo)
        {
            List<Cotizador> lista = new List<Cotizador>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Pre_ListPapel";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    cmd.Parameters.AddWithValue("@Tipo", Tipo);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Cotizador p = new Cotizador();
                        p.PapelInterior = reader["TipoPapel"].ToString();
                        lista.Add(p);
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return lista;
        }

        public List<Cotizador> Listar_GramajePapel(string Papel, string Tipo, string Empresa)
        {
            List<Cotizador> lista = new List<Cotizador>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "Pre_ListGramaje";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Tipo", Papel);
                    cmd.Parameters.AddWithValue("@Componente", Tipo);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cotizador p = new Cotizador();
                        p.GramajeInterior = reader["gramaje"].ToString() + " grs";
                        lista.Add(p);
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return lista;
        }

        public ValorDolar_Trimestral ValorDolarTrimestreActivo()
        {
            ValorDolar_Trimestral valorq = new ValorDolar_Trimestral();
             Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "List_ValTri";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        valorq.NombreTrimestre = reader["NombreTrimestre"].ToString();
                        valorq.ValorTrimestre = Convert.ToDouble(reader["ValorTrimestre"].ToString());
                        valorq.FechaInicio = Convert.ToDateTime(reader["FechaInicio"].ToString());
                        valorq.FechaTermino = Convert.ToDateTime(reader["FechaTermino"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return valorq;
        }

        public int GuardarPresupuesto(string NombrePresupuesto, int PagInterior, int PagTapa, string Formato, string GramajeInt1, string GramajeTapa1, string PapelInterior, string PapelTapa, string Encuadernacion,
            string MaquinaInterior, string MaquinaTapas, string Usuario, string Empresa, string Tiraje1, string Tiraje2, string Tiraje3, string Costo1, string Costo2, string Costo3, string CostoU1, string CostoU2, string CostoU3,
            string Millar1, string Millar2, string Millar3)
        {
            int respuesta = 0;
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "PPTO_InsertPPTO";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombrePresupuesto", NombrePresupuesto.ToUpper());
                    cmd.Parameters.AddWithValue("@PagInterior", PagInterior);
                    cmd.Parameters.AddWithValue("@PagTapa", PagTapa);
                    cmd.Parameters.AddWithValue("@Formato", Formato);
                    cmd.Parameters.AddWithValue("@GramajeInt1", GramajeInt1);
                    cmd.Parameters.AddWithValue("@GramajeTapa1", GramajeTapa1.Replace("Seleccione Gramaje Papel...", ""));
                    cmd.Parameters.AddWithValue("@PapelInterior", PapelInterior);
                    cmd.Parameters.AddWithValue("@PapelTapa", PapelTapa.Replace("Seleccione Tipo Papel...", ""));
                    cmd.Parameters.AddWithValue("@Encuadernacion", Encuadernacion);
                    cmd.Parameters.AddWithValue("@MaquinaInterior", MaquinaInterior);
                    cmd.Parameters.AddWithValue("@MaquinaTapas", MaquinaTapas);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    cmd.Parameters.AddWithValue("@Tiraje1",Tiraje1.Replace(".",""));
                    cmd.Parameters.AddWithValue("@Tiraje2", Tiraje2.Replace(".", ""));
                    cmd.Parameters.AddWithValue("@Tiraje3", Tiraje3.Replace(".", ""));
                    cmd.Parameters.AddWithValue("@Costo1",Costo1);
                    cmd.Parameters.AddWithValue("@CostoU1", CostoU1);
                    cmd.Parameters.AddWithValue("@Millar1", Millar1);
                    if (Tiraje2 != "")
                    {
                        cmd.Parameters.AddWithValue("@Costo2", Costo2);
                        cmd.Parameters.AddWithValue("@CostoU2", CostoU2.Replace("Infinity", "0").Replace("NaN", "0"));
                        cmd.Parameters.AddWithValue("@Millar2", Millar2.Replace("Infinity", "0").Replace("NaN", "0"));
                    }
                    if (Tiraje3 != "")
                    {
                        cmd.Parameters.AddWithValue("@Costo3", Costo3);
                        cmd.Parameters.AddWithValue("@CostoU3", CostoU3.Replace("Infinity", "0").Replace("NaN", "0"));
                        cmd.Parameters.AddWithValue("@Millar3", Millar3.Replace("Infinity", "0").Replace("NaN", "0"));
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        respuesta = Convert.ToInt32(reader["Respuesta"].ToString());
                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return respuesta;
            
        }
        
        public Cotizador BuscarPresupuestoxID(int id)
        {
            Cotizador cot = new Cotizador();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "PPTO_BuscarPPTO_ID";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPPTO",id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cot.ID_Presupuesto = Convert.ToInt32(reader["id_Presupuestos"].ToString());
                        cot.NombrePresupuesto = reader["Nombre_Presupuesto"].ToString();
                        cot.Formato = reader["Formato"].ToString();
                        cot.PaginasInt = Convert.ToInt32(reader["PaginasInt"].ToString());
                        cot.PapelInterior = reader["PapelInt"].ToString();
                        cot.GramajeInterior = reader["GramajeInt"].ToString();
                        cot.PaginasTap = Convert.ToInt32(reader["PaginasTap"].ToString());
                        cot.PapelTap = reader["PapelTap"].ToString();
                        cot.GramajeTapas = reader["GramajeTap"].ToString();
                        cot.Empresa = reader["Empresa"].ToString();
                        cot.Encuadernacion = reader["Encuadernacion"].ToString();
                        cot.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString());
                        cot.EstadoPPTO = Convert.ToInt32(reader["Estado"].ToString());

                        Controller_Usuario controlUser = new Controller_Usuario();
                        Usuario personal = controlUser.BuscarPersonalComercial_Empresa(cot.Empresa);
                        cot.PersonalComercial = personal;

                        Controller_ValorTrimestre controlDolar = new Controller_ValorTrimestre();
                        ValorDolar_Trimestral dolar = controlDolar.BuscarDolar_ID(Convert.ToInt32(reader["Trimestre_ID".ToString()]));
                        cot.ValorDolar = dolar;

                        Controller_Tirajes controlTiraje = new Controller_Tirajes();
                        List<PPTO_Tirajes> lista = controlTiraje.ListarTirajes_ID(cot.ID_Presupuesto);
                        int count = 0;
                        foreach (PPTO_Tirajes t in lista)
                        {
                            if (t.NombreTiraje == "Tiraje 1")
                            {
                                cot.Tiraje1 = t;
                            }
                            else if (t.NombreTiraje == "Tiraje 2")
                            {
                                cot.Tiraje2 = t;
                            }
                            else
                            {
                                cot.Tiraje3 = t;
                            }
                            count++;
                        }
                        
                    }
                }
                catch
                {
                }
            }
            return cot;
        }

        public string ListarPPTO_Estado(int Estado)
        {
            string resultado = "[";
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "PPTO_ListarPPTO_Estado";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Estado", Estado);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string Edit = "<a href='javascript:openPDF(" + reader["id_Presupuestos"].ToString() + ");' ><img src='../../Estructura/Image/pdf-icon.jpg' width='20px' /></a>";
                        if (Estado == 1)
                        {
                            Edit += "<a href='javascript:levantarPopupAprobar(" + reader["id_Presupuestos"].ToString() + ");'><img src='../../Estructura/Image/check.png' width='20px'/></a>";
                        }
                        else
                        {
                            Edit += "<a href='javascript:levantarPopupDespacho(" + reader["id_Presupuestos"].ToString() + ");'><img src='../../Estructura/Image/Despacho.png' width='20px'/></a>";
                        }
                        resultado +=
                            "{_idPresupuestos_:_" + reader["id_Presupuestos"].ToString() + "_,_NombrePresupuesto_:_" + reader["Nombre_Presupuesto"].ToString() + "_,_Formato_:_" + reader["Formato"].ToString() +
                                    "_,_Encuadernacion_:_" + reader["Encuadernacion"].ToString() + "_,_PaginasInt_:_" + reader["PaginasInt"].ToString() + "_,_PaginasTap_:_" + reader["PaginasTap"].ToString() +
                                    "_,_PapelInt_:_" + reader["PapelInt"].ToString() + "_,_PapelTap_:_" + reader["PapelTap"].ToString() + "_,_Editar_:_" + Edit + "_},";
                    }
                    resultado = resultado.Substring(0, resultado.Length - 1) + "]";
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