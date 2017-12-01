using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Copesa.ModuloPresupuesto.Model;
using System.Data.SqlClient;
using Cotizador_Copesa.ModuloUsuario.Controller;
using Cotizador_Copesa.ModuloUsuario.Model;

namespace Cotizador_Copesa.ModuloPresupuesto.Controller
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
                        if (Encuadernacion == "Entapado Hotmelt")
                        {
                            if (Gramajepapel >= 130)
                            {
                                lista.Add(p);
                            }
                        }
                        else if (Encuadernacion == "2 Corchete al lomo")
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

        public int GuardarPresupuesto(string NombrePresupuesto, int PagInterior, int PagTapa, string Formato, string GramajeInt1, string GramajeTapa1, string PapelInterior, string PapelTapa, string Encuadernacion,
            string MaquinaInterior, string MaquinaTapas, string Usuario, string Empresa, string Tiraje, string Costo, string CostoU, string BarnizAcuosoTapa, string QuintoColor, string UV, string Laminado, string Embolsado)
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
                    cmd.Parameters.AddWithValue("@PagTapa", ((PagTapa>0) ? PagTapa.ToString() : null));
                    cmd.Parameters.AddWithValue("@Formato", Formato);
                    cmd.Parameters.AddWithValue("@GramajeInt1", GramajeInt1);
                    cmd.Parameters.AddWithValue("@GramajeTapa1", ((GramajeTapa1!="Seleccione Gramaje Papel...") ? GramajeTapa1 : null));
                    cmd.Parameters.AddWithValue("@PapelInterior", PapelInterior);
                    cmd.Parameters.AddWithValue("@PapelTapa", ((PapelTapa!="Seleccione Tipo Papel...") ? PapelTapa : null));
                    cmd.Parameters.AddWithValue("@Encuadernacion", Encuadernacion);
                    cmd.Parameters.AddWithValue("@MaquinaInterior", MaquinaInterior);
                    cmd.Parameters.AddWithValue("@MaquinaTapas", ((PagTapa>0) ? MaquinaTapas : null));
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Empresa", Empresa);
                    cmd.Parameters.AddWithValue("@Tiraje", Tiraje.Replace(".", ""));
                    cmd.Parameters.AddWithValue("@Costo", Costo.Replace(",", ""));
                    cmd.Parameters.AddWithValue("@CostoU", Convert.ToDouble(CostoU));
                    cmd.Parameters.AddWithValue("@BarnizAcuosoTap", (BarnizAcuosoTapa!="No") ? BarnizAcuosoTapa: null);
                    cmd.Parameters.AddWithValue("@QuintoColor", ((QuintoColor != "Seleccione Quinto Color...") ? QuintoColor : null)); 
                    cmd.Parameters.AddWithValue("@UV", ((UV!="Seleccione Barniz UV...") ? UV: null));
                    cmd.Parameters.AddWithValue("@Laminado", ((Laminado!="Seleccione Laminado...") ? Laminado : null));
                    cmd.Parameters.AddWithValue("@Embolsado", (Embolsado!=("Seleccione Embolsado ") ? Embolsado : null));

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

        public string ListarPPTO_Estado(int Estado, string Usuario)
        {
            string resultado = "[";
            Conexion con = new Conexion();
            SqlCommand cmd = con.AbrirConexionPPTO();
            if (cmd != null)
            {
                try
                {
                    cmd.CommandText = "PPTO_ListarPresupuestos";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Estado", Estado);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
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
                        cot.PaginasTap = (reader["PaginasTap"].ToString()!="") ? Convert.ToInt32(reader["PaginasTap"].ToString()) : 0;
                        cot.PapelTap = reader["PapelTap"].ToString();
                        cot.GramajeTapas = reader["GramajeTap"].ToString();
                        cot.Empresa = reader["Empresa"].ToString();
                        cot.Encuadernacion = reader["Encuadernacion"].ToString();
                        cot.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString());
                        cot.EstadoPPTO = Convert.ToInt32(reader["Estado"].ToString());
                        cot.Usuario_Creador = reader["Usuario"].ToString();
                        cot.EntradasxFormatos = Convert.ToInt32(reader["EntradasxFormatos"].ToString());
                        cot.Tiraje = Convert.ToInt32(reader["Tiraje"].ToString());
                        cot.BarnizAcuosoTap = reader["BarnizAcuosoTap"].ToString();
                        cot.QuintoColor = reader["QuintoColorTap"].ToString();
                        cot.BarnizUV = reader["BarnizUVTap"].ToString();
                        cot.Laminado = reader["LaminadoTap"].ToString();
                        cot.TotalNeto = Convert.ToInt32(reader["TotalNeto"].ToString());
                        cot.PrecioUnitario = Convert.ToDouble(reader["PrecioUnitario"].ToString());

                        Controller_Usuario controlUser = new Controller_Usuario();
                        Usuario personal = controlUser.BuscarPersonalComercial_Empresa(cot.Empresa);
                        cot.PersonalComercial = personal;

                        cot.ValorUFActual = new ValorUF () { Valor = Convert.ToDouble(reader["ValorUF"].ToString())};


                    }
                }
                catch
                {
                }
            }
            con.CerrarConexion();
            return cot;
        }
    }
}