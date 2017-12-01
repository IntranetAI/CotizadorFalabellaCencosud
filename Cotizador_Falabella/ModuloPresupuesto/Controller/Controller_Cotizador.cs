using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Falabella.ModuloPresupuesto.Model;
using System.Data.SqlClient;
using Cotizador_Falabella.ModuloUsuario.Controller;
using Cotizador_Falabella.ModuloUsuario.Model;

namespace Cotizador_Falabella.ModuloPresupuesto.Controller
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
                    cmd.CommandText = "PPTO_ListarFormatos";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cotizador p = new Cotizador();
                        p.Formato = reader["nombre_Formato"].ToString();
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

        public List<Cotizador> List_Papel(string Empresa, string Tipo)
        {
            List<Cotizador> lista = new List<Cotizador>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "PPTO_ListarPapeles";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    cmd.Parameters.AddWithValue("@Tipo", Tipo);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Cotizador p = new Cotizador();
                        p.PapelInterior = reader["TipoPapel"].ToString();
                        p.GramajeInterior = reader["gramaje"].ToString();
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

        public List<Cotizador> Listar_GramajePapel(string Papel, string Tipo, string Empresa, string Encuadernacion)
        {
            List<Cotizador> lista = new List<Cotizador>();
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "PPTO_ListarGramaje";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Tipo", Papel);
                    cmd.Parameters.AddWithValue("@Componente", Tipo);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cotizador p = new Cotizador();
                        int Gramajepapel = Convert.ToInt32(reader["gramaje"].ToString());
                        p.GramajeInterior = reader["gramaje"].ToString();
                        if (Encuadernacion == "Entapado Hotmelt" || Encuadernacion == "Entapado Pur")
                        {
                            //if(Gramajepapel>=90)validacion creada por alan el dia 16-08-2017
                            if (Gramajepapel >= 130)
                            {
                                lista.Add(p);
                            }
                        }
                        else if (Encuadernacion == "2 Corchete al lomo")
                        {
                            //if(Gramajepapel>=130)
                            //{
                            //validacion creada por alan el dia 16-08-2017
                                lista.Add(p);
                            //}
                        }
                        else
                        {
                            lista.Add(p);
                        }

                       
                        
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
                        t.NombreSumplifacado = reader["simpli"].ToString();
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

        public int GuardarPresupuesto(string NombrePresupuesto, int PagInterior, int PagTapa, string Formato, string GramajeInt1, string GramajeTapa1, string PapelInterior, string PapelTapa, string Encuadernacion,
            string MaquinaInterior, string MaquinaTapas, string Usuario, string Empresa, string Tiraje1, string Tiraje2, string Tiraje3, string Costo1, string Costo2, string Costo3, string CostoU1, string CostoU2,
            string CostoU3, string Segmento, string BarnizAcuosoTapa, string BarnizAcuosoInt, string QuintoColor, string UV, string Laminado)
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
                    cmd.Parameters.AddWithValue("@GramajeTapa1", GramajeTapa1.Replace("Seleccione Gramaje Papel...", "0"));
                    cmd.Parameters.AddWithValue("@PapelInterior", PapelInterior);
                    cmd.Parameters.AddWithValue("@PapelTapa", PapelTapa.Replace("Seleccione Tipo Papel...", ""));
                    cmd.Parameters.AddWithValue("@Encuadernacion", Encuadernacion);
                    cmd.Parameters.AddWithValue("@MaquinaInterior", MaquinaInterior);
                    cmd.Parameters.AddWithValue("@MaquinaTapas", MaquinaTapas);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    cmd.Parameters.AddWithValue("@Tiraje1", Tiraje1.Replace(".", ""));
                    cmd.Parameters.AddWithValue("@Costo1", Costo1.Replace(",", ""));
                    cmd.Parameters.AddWithValue("@CostoU1", Convert.ToDouble(CostoU1));
                    cmd.Parameters.AddWithValue("@Segmento", Segmento);
                    cmd.Parameters.AddWithValue("@BarnizAcuosoInt", BarnizAcuosoInt);
                    cmd.Parameters.AddWithValue("@BarnizAcuosoTap", BarnizAcuosoTapa);
                    cmd.Parameters.AddWithValue("@QuintoColor", QuintoColor.Replace("Seleccionar",""));
                    cmd.Parameters.AddWithValue("@UV", UV.Replace("Seleccione Barniz UV...", ""));
                    cmd.Parameters.AddWithValue("@Laminado", Laminado.Replace("Seleccione Laminado...", ""));
                    

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
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
                    cmd.Parameters.AddWithValue("@IDPPTO", id);
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
                        cot.Usuario_Creador = reader["Usuario"].ToString();
                        cot.Segmentos = reader["Segmento"].ToString();
                        cot.EntradasxFormatos = Convert.ToInt32(reader["EntradasxFormatos"].ToString());

                        cot.BarnizAcuosoInt = reader["BarnizAcuosoInt"].ToString();
                        cot.BarnizAcuosoTap = reader["BarnizAcuosoTap"].ToString();
                        cot.QuintoColor = reader["QuintoColorTap"].ToString();
                        cot.BarnizUV = reader["BarnizUVTap"].ToString();
                        cot.Laminado = reader["LaminadoTap"].ToString();


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

        public string ListarPPTO_Estado(int Estado, string Usuario)
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
                    cmd.Parameters.AddWithValue("@Usuario",Usuario);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string Edit = "<a href='javascript:openPDF(" + reader["id_Presupuestos"].ToString() + ");' ><img src='../../Estructura/Image/pdf-icon.jpg' width='20px' /></a>";
                        if (reader["perfil"].ToString() == "Admin")
                        {
                            Edit += "<a href='javascript:levantarPopupDetalle(" + reader["id_Presupuestos"].ToString() + ");'><img src='../../Estructura/Image/icono%20detalle.jpg' width='20px'/></a>";
                        }
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