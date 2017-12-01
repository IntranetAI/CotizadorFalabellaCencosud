using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cotizador_Cencosud.ModuloPresupuesto.Controller;
using System.Web.Services;
using System.Web.Script.Serialization;
using Cotizador_Cencosud.ModuloPresupuesto.Model;
using Cotizador_Cencosud.ModuloUsuario.Controller;
using Cotizador_Cencosud.ModuloUsuario.Modelo;

namespace Cotizador_Cencosud.ModuloPresupuesto.View
{
    public partial class Presupuestador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                    CargarUsuario(id);
                }
                catch
                {
                    Response.Redirect("../../index.aspx");
                }
                CargarFormatos();
                CargarPagInterior("");
                txtTiraje1.Attributes.Add("onkeypress", "return pulsarTiraje(event);");
                txtTiraje2.Attributes.Add("onkeypress", "return pulsarTiraje(event);");
                txtTiraje3.Attributes.Add("onkeypress", "return pulsarTiraje(event);");
                CargarValorTrimestre();
            }
        }

        public void CargarUsuario(int id)
        {
            Controller_Usuario controlu = new Controller_Usuario();
            Usuario user = controlu.BuscarUsuario_ID(id);
            if (user.NombreCompleto != null)
            {
                if (user.Perfil != "Admin")
                {
                    LabelPrimerPrecio.Visible = false;
                    LabelSegundoPrecio.Visible = false;
                    LabelTercerPrecio.Visible = false;
                    DIVPrecio.Visible = false;
                }
                CargarPapeles("Cencosud");//se debe cambiar por la empresa
            }
            else
            {
                Response.Redirect("../../index.aspx");
            }
        }

        public void CargarFormatos()
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            ddlFormato.DataSource = preControl.Listar_Formato();
            ddlFormato.DataTextField = "Formato";
            ddlFormato.DataValueField = "PaginasInt";
            ddlFormato.DataBind();
            ddlFormato.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public void CargarPagInterior(string Doblez)
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            ddlPaginas.DataSource = preControl.Listar_Paginas_Interior(Doblez);
            ddlPaginas.DataTextField = "Formato";
            ddlPaginas.DataValueField = "Formato";//decia doblez y el codigo del procedimiento lo elimine
            ddlPaginas.DataBind();
            ddlPaginas.Items.Insert(0, new ListItem("0", "0"));
        }

        public void CargarPapeles(string Empresa)
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            ddlPapel.DataSource = preControl.List_Papel(Empresa, "Interior");
            ddlPapel.DataTextField = "PapelInterior";
            ddlPapel.DataValueField = "PapelInterior";
            ddlPapel.DataBind();
            ddlPapel.Items.Insert(0, new ListItem("Seleccione Tipo Papel...", "Seleccione Tipo Papel..."));
            ddlPapTapas.DataSource = preControl.List_Papel(Empresa, "Tapa");
            ddlPapTapas.DataTextField = "PapelInterior";
            ddlPapTapas.DataValueField = "PapelInterior";
            ddlPapTapas.DataBind();
            ddlPapTapas.Items.Insert(0, new ListItem("Seleccione Tipo Papel...", "Seleccione Tipo Papel..."));
        }

        public void CargarValorTrimestre()
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            ValorDolar_Trimestral valorq = preControl.ValorDolarTrimestreActivo();
            lblCostoQ.Text = valorq.ValorTrimestre.ToString();
        }

        [WebMethod]
        public static string Gramage_Papel(string Papel, string Div, string Empresa)
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            List<Cotizador> lista = preControl.Listar_GramajePapel(Papel, Div, Empresa);
            List<Cotizador> lista2 = new List<Cotizador>();
            int contador = 1;
            Cotizador insert1 = new Cotizador();
            insert1.GramajeInterior = "Seleccione Gramaje Papel...";
            lista2.Insert(0, insert1);
            foreach (Cotizador ps in lista)
            {
                Cotizador objst = new Cotizador();
                objst.GramajeInterior = ps.GramajeInterior;
                lista2.Insert(contador, objst);
                contador++;
            }

            JavaScriptSerializer jscript = new JavaScriptSerializer();
            return jscript.Serialize(lista2);
        }

        [WebMethod]
        public static string[] PrePrensa(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion, string Tiraje1, string Tiraje2, string Tiraje3)
        {
            string[] Calcular1 = CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje1);
            string[] Calcular2 = CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje2);
            string[] Calcular3 = CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje3);
            #region CrearTablaTotal

            string TablaDetalle1 = "<table class='table table-hover'>" +
                                        "<thead>" +
                                        "<tr>" +
                                          "<th>#</th>" +
                                          "<th>Costo Fijo</th>" +
                                          "<th>Costo Variable</th>" +
                                          "<th>Totales</th>" +
                                        "</tr>" +
                                      "</thead>" +
                                      "<tbody>" +
                                        
                                        "<tr>" +
                                          "<th scope='row'> Interiores</th>" +
                                          "<td>" + Convert.ToInt32(Calcular1[0]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular1[1]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular1[2]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Tapas</th>" +
                                          "<td>" + Convert.ToInt32(Calcular1[7]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular1[8]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular1[9]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'>Barniz Acuoso</th>" +
                                          "<td>" + Convert.ToInt32(Calcular1[18]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular1[19]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + (Convert.ToDouble(Calcular1[18]) + Convert.ToDouble(Calcular1[19])).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'>Encuadernación</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> " + Encuadernacion + "</th>" +
                                          "<td>" + Convert.ToInt32(Calcular1[4]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular1[5]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular1[6]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'>Papel</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Precio Papel Interior</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular1[3]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Precio Papel Tapas</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular1[10]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Costo Total Catalogo</th>" +
                                          "<td>" + Convert.ToInt32(Calcular1[11]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular1[12]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular1[13]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Costo Unitario</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular1[14].Replace("Infinity", "0").Replace("NaN", "0")).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Costo Variable x Millar</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular1[15].Replace("Infinity", "0").Replace("NaN", "0")).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "</tbody>" +
                                    "</table>";
            string TablaDetalle2 = "";
            string CostoTotal2 = "";
            string CostoUnitario2 = "";
            string MillarAdi2 = "";
            if (Tiraje2 != "")
            {
                TablaDetalle2 = "<table class='table table-hover'>" +
                                        "<thead>" +
                                        "<tr>" +
                                          "<th>#</th>" +
                                          "<th>Costo Fijo</th>" +
                                          "<th>Costo Variable</th>" +
                                          "<th>Totales</th>" +
                                        "</tr>" +
                                      "</thead>" +
                                      "<tbody>" +

                                        "<tr>" +
                                          "<th scope='row'> Interiores</th>" +
                                          "<td>" + Convert.ToInt32(Calcular2[0]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular2[1]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular2[2]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Tapas</th>" +
                                          "<td>" + Convert.ToInt32(Calcular2[7]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular2[8]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular2[9]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'>Barniz Acuoso</th>" +
                                          "<td>" + Convert.ToInt32(Calcular2[18]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular2[19]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + (Convert.ToDouble(Calcular2[18]) + Convert.ToDouble(Calcular2[19])).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'>Encuadernación</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> " + Encuadernacion + "</th>" +
                                          "<td>" + Convert.ToInt32(Calcular2[4]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular2[5]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular2[6]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'>Papel</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Precio Papel Interior</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular2[3]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Precio Papel Tapas</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular2[10]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Costo Total Catalogo</th>" +
                                          "<td>" + Convert.ToInt32(Calcular2[11]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular2[12]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular2[13]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Costo Unitario</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular2[14].Replace("Infinity", "0").Replace("NaN", "0")).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Costo Variable x Millar</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular2[15].Replace("Infinity", "0").Replace("NaN", "0")).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "</tbody>" +
                                    "</table>";
                CostoTotal2 = Convert.ToDouble(Calcular2[13]).ToString("N0").Replace(",", ".");
                CostoUnitario2 = Convert.ToDouble(Calcular2[14]).ToString("N0").Replace(",", ".");
                MillarAdi2 = Convert.ToDouble(Calcular2[15]).ToString("N0").Replace(",", ".");

            }
            string TablaDetalle3 = "";
            string CostoTotal3 = "";
            string CostoUnitario3 = "";
            string MillarAdi3 = "";
            if (Tiraje3 != "")
            {
                TablaDetalle3 = "<table class='table table-hover'>" +
                                        "<thead>" +
                                        "<tr>" +
                                          "<th>#</th>" +
                                          "<th>Costo Fijo</th>" +
                                          "<th>Costo Variable</th>" +
                                          "<th>Totales</th>" +
                                        "</tr>" +
                                      "</thead>" +
                                      "<tbody>" +

                                        "<tr>" +
                                          "<th scope='row'> Interiores</th>" +
                                          "<td>" + Convert.ToInt32(Calcular3[0]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular3[1]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular3[2]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Tapas</th>" +
                                          "<td>" + Convert.ToInt32(Calcular3[7]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular3[8]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular3[9]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'>Barniz Acuoso</th>" +
                                          "<td>" + Convert.ToInt32(Calcular3[18]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular3[19]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + (Convert.ToDouble(Calcular3[18]) + Convert.ToDouble(Calcular3[19])).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'>Encuadernación</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> " + Encuadernacion + "</th>" +
                                          "<td>" + Convert.ToInt32(Calcular3[4]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular3[5]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToInt32(Calcular3[6]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'>Papel</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Precio Papel Interior</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular3[3]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Precio Papel Tapas</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular3[10]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Costo Total Catalogo</th>" +
                                          "<td>" + Convert.ToInt32(Calcular3[11]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular3[12]).ToString("N0").Replace(",", ".") + "</td>" +
                                          "<td>" + Convert.ToDouble(Calcular3[13]).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Costo Unitario</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular3[14].Replace("Infinity", "0").Replace("NaN", "0")).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                          "<th scope='row'> Costo Variable x Millar</th>" +
                                          "<td></td>" +
                                          "<td></td>" +
                                          "<td>" + Convert.ToInt32(Calcular3[15].Replace("Infinity", "0").Replace("NaN", "0")).ToString("N0").Replace(",", ".") + "</td>" +
                                        "</tr>" +
                                        "</tbody>" +
                                    "</table>";
                CostoTotal3 = Convert.ToDouble(Calcular3[13]).ToString("N0").Replace(",", ".");
                CostoUnitario3 = Convert.ToDouble(Calcular3[14]).ToString("N0").Replace(",", ".");
                MillarAdi3 = Convert.ToDouble(Calcular3[15]).ToString("N0").Replace(",", ".");

            }
            #endregion
            return new[] { TablaDetalle1, Convert.ToDouble(Calcular1[13]).ToString("N0").Replace(",", "."), Convert.ToDouble(Calcular1[14].Replace("NaN","0").Replace("Infinity","0")).ToString("N0").Replace(",", "."), 
                            Convert.ToDouble(Calcular1[15].Replace("NaN","0").Replace("Infinity","0")).ToString("N0").Replace(",", "."),TablaDetalle2, CostoTotal2, CostoUnitario2, MillarAdi2,
                           TablaDetalle3, CostoTotal3, CostoUnitario3, MillarAdi3 };
        }

        public static string[] CalcularPreciosPrensa(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion, string Tiraje)
        {
            Controller_Tarifas controlTarifa = new Controller_Tarifas();
            string MaquinaInterior = "Plana"; 
            string MaquinaTapa = "Plana";
            if (Tiraje != "")
            {
                Tiraje = Tiraje.Replace(".", "");
                if (Convert.ToInt32(Tiraje) > 15000)
                {
                    MaquinaInterior = "Rotativa";
                }
            }
            else
            {
                Tiraje = "0";
            }
            int GramajeInt = 0;
            if ("Seleccione Gramaje Papel..." != GramajeInt1)
            {
                GramajeInt = Convert.ToInt32(GramajeInt1.Replace(" grs", ""));
                if (GramajeInt > 169)
                {
                    MaquinaInterior = "Plana";
                }
            }
            int GramajeTapa = 0;
            if ("Seleccione Gramaje Papel..." != GramajeTapa1)
            {
                GramajeTapa = Convert.ToInt32(GramajeTapa1.Replace(" grs", ""));
            }


            #region EntradasxFormato
            int EntradasPag32 = 0;
            int EntradasPag24 = 0;
            int EntradasPag16 = 0;
            int EntradasPag12 = 0;
            int EntradasPag8 = 0;
            int EntradasPag4 = 0;
            int cantidadfaltante = 0;
            switch (EntradasxFormato)
            {
                case 32:
                    EntradasPag32 = (PagInterior / 32);
                    cantidadfaltante = PagInterior - (EntradasPag32 * 32);
                    if (cantidadfaltante >= 16)
                    {
                        EntradasPag16 = 1;
                        cantidadfaltante -= 16;
                        if (cantidadfaltante >= 8)
                        {
                            EntradasPag8 = 1;
                            cantidadfaltante -= 8;
                            if (cantidadfaltante == 4)
                            {
                                EntradasPag4 = 1;
                                cantidadfaltante = 0;
                            }
                        }
                        else if (cantidadfaltante != 0)
                        {
                            EntradasPag4 = 1;
                            cantidadfaltante = 0;
                        }
                    }
                    else
                    {
                        if (cantidadfaltante >= 8)
                        {
                            EntradasPag8 = 1;
                            cantidadfaltante -= 8;
                            if (cantidadfaltante == 4)
                            {
                                EntradasPag4 = 1;
                                cantidadfaltante = 0;
                            }
                        }
                        else if (cantidadfaltante != 0)
                        {
                            EntradasPag4 = 1;
                            cantidadfaltante = 0;
                        }
                    }
                    break;
                case 24:
                    EntradasPag24 = (PagInterior / 24);
                    cantidadfaltante = PagInterior - (EntradasPag24 * 24);
                    if (cantidadfaltante >= 12)
                    {
                        EntradasPag12 = 1;
                        cantidadfaltante -= 12;
                        if (cantidadfaltante >= 8)
                        {
                            EntradasPag8 = 1;
                            cantidadfaltante -= 8;
                            if (cantidadfaltante == 4)
                            {
                                EntradasPag4 = 1;
                                cantidadfaltante = 0;
                            }
                        }
                        else if (cantidadfaltante != 0)
                        {
                            EntradasPag4 = 1;
                            cantidadfaltante = 0;
                        }
                    }
                    else
                    {
                        if (cantidadfaltante >= 8)
                        {
                            EntradasPag8 = 1;
                            cantidadfaltante -= 8;
                            if (cantidadfaltante == 4)
                            {
                                EntradasPag4 = 1;
                                cantidadfaltante = 0;
                            }
                        }
                        else if (cantidadfaltante != 0)
                        {
                            EntradasPag4 = 1;
                            cantidadfaltante = 0;
                        }
                    }
                    break;
                case 16:
                    EntradasPag16 = (PagInterior / 16);
                    cantidadfaltante = PagInterior - (EntradasPag16 * 16);
                    if (cantidadfaltante >= 8)
                    {
                        EntradasPag8 = 1;
                        cantidadfaltante -= 8;
                        if (cantidadfaltante == 4)
                        {
                            EntradasPag4 = 1;
                            cantidadfaltante = 0;
                        }
                    }
                    else if (cantidadfaltante != 0)
                    {
                        EntradasPag4 = 1;
                        cantidadfaltante = 0;
                    }
                    break;
                case 12:
                    EntradasPag12 = (PagInterior / 12);
                    cantidadfaltante = PagInterior - (EntradasPag12 * 12);
                    if (cantidadfaltante >= 8)
                    {
                        EntradasPag8 = 1;
                        cantidadfaltante -= 8;
                        if (cantidadfaltante == 4)
                        {
                            EntradasPag4 = 1;
                            cantidadfaltante = 0;
                        }
                    }
                    else if (cantidadfaltante != 0)
                    {
                        EntradasPag4 = 1;
                        cantidadfaltante = 0;
                    }
                    break;
                case 8:
                    EntradasPag8 = (PagInterior / 8);
                    cantidadfaltante = PagInterior - (EntradasPag8 * 8);
                    if (cantidadfaltante == 4)
                    {
                        EntradasPag4 = 1;
                        cantidadfaltante = 0;
                    }
                    break;
                default:
                    break;

            }
            #endregion

            
            #region Interior
            Tarifa_Papel tarifapapelInterior = controlTarifa.TarifaCostoPapel(GramajeInt, "interior", MaquinaInterior, Papelinterior, "Cencosud", Formato);
            int CostoFijoInterior = Convert.ToInt32(controlTarifa.TarifaCostoImpresion("Impresion Interior 4/4", "", MaquinaInterior, "Fijo"));
            int CostoPapelInteriorFijo = Convert.ToInt32(Convert.ToDouble(tarifapapelInterior.TarifaMermaFija));

            double CostoFijoPag32 = (Math.Ceiling((Convert.ToDouble(CostoFijoInterior) + Convert.ToDouble(CostoPapelInteriorFijo)) / 100) * 100) * Convert.ToDouble(EntradasPag32);
            double CostoFijoPag24 = (Math.Ceiling((Convert.ToDouble(CostoFijoInterior) + Convert.ToDouble(CostoPapelInteriorFijo)) / 100) * 100) * Convert.ToDouble(EntradasPag24);
            double CostoFijoPag16 = (Math.Ceiling((Convert.ToDouble(CostoFijoInterior) + Convert.ToDouble(CostoPapelInteriorFijo)) / 100) * 100) * Convert.ToDouble(EntradasPag16);
            double CostoFijoPag12 = (Math.Ceiling((Convert.ToDouble(CostoFijoInterior) + Convert.ToDouble(CostoPapelInteriorFijo)) / 100) * 100) * Convert.ToDouble(EntradasPag12);
            double CostoFijoPag8 = (Math.Ceiling((Convert.ToDouble(CostoFijoInterior) + Convert.ToDouble(CostoPapelInteriorFijo)) / 100) * 100) * Convert.ToDouble(EntradasPag8);
            double CostoFijoPag4 = (Math.Ceiling((Convert.ToDouble(CostoFijoInterior) + Convert.ToDouble(CostoPapelInteriorFijo)) / 100) * 100) * Convert.ToDouble(EntradasPag4);

            double CostoFijoPrecioVentaInterior = CostoFijoPag32+CostoFijoPag24+CostoFijoPag16+CostoFijoPag12+CostoFijoPag8+CostoFijoPag4;
                        
            #region CostoVariablexEntradas
            double TarifaCostoVariableImpresionInterior = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Impresion Interior 4/4", "", MaquinaInterior, "Variable"));
            double DesintercalarRotativa = 1.5;
            if (MaquinaInterior == "Plana")
            {
                DesintercalarRotativa = 0;
            }
            double CostoVariablePag32 = 0;
            double CostoVariablePag24 = 0;
            double CostoVariablePag16 = 0;
            double CostoVariablePag12 = 0;
            double CostoVariablePag8 = 0;
            double CostoVariablePag4 = 0;

            switch (EntradasxFormato)
            {
                case 32:
                    CostoVariablePag32 = (Math.Ceiling((TarifaCostoVariableImpresionInterior + tarifapapelInterior.TarifaMermaVariable) * 10) / 10) * Convert.ToDouble(EntradasPag32);
                    if (EntradasPag16 > 0)
                    {
                        CostoVariablePag16 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(16) / Convert.ToDouble(32))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(16) / Convert.ToDouble(32)))) * 10)) / 10) * Convert.ToDouble(EntradasPag16);
                    }
                    if (EntradasPag8 > 0)
                    {
                        CostoVariablePag8 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(8) / Convert.ToDouble(32))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(8) / Convert.ToDouble(32)))) * 10)) / 10) * Convert.ToDouble(EntradasPag8);
                    }
                    if (EntradasPag4 > 0)
                    {
                        CostoVariablePag4 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(4) / Convert.ToDouble(32))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(4) / Convert.ToDouble(32)))) * 10)) / 10) * Convert.ToDouble(EntradasPag4);
                    }
                    break;
                case 24:
                    CostoVariablePag24 = (Math.Ceiling((TarifaCostoVariableImpresionInterior + tarifapapelInterior.TarifaMermaVariable) * 10) / 10) * Convert.ToDouble(EntradasPag24);
                    if (EntradasPag12 > 0)
                    {
                        CostoVariablePag12 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(12) / Convert.ToDouble(24))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(12) / Convert.ToDouble(24)))) * 10)) / 10) * Convert.ToDouble(EntradasPag12);
                    }
                    if (EntradasPag8 > 0)
                    {
                        CostoVariablePag8 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(8) / Convert.ToDouble(24))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(8) / Convert.ToDouble(24)))) * 10)) / 10) * Convert.ToDouble(EntradasPag8);
                    }
                    if (EntradasPag4 > 0)
                    {
                        CostoVariablePag4 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(4) / Convert.ToDouble(24))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(4) / Convert.ToDouble(24)))) * 10)) / 10) * Convert.ToDouble(EntradasPag4);
                    }
                    break;
                case 16:
                    CostoVariablePag16 = (Math.Ceiling((TarifaCostoVariableImpresionInterior + tarifapapelInterior.TarifaMermaVariable)*10)/10)* Convert.ToDouble(EntradasPag16);
                    if (EntradasPag8 > 0)
                    {
                        CostoVariablePag8 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(8) / Convert.ToDouble(16))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(8) / Convert.ToDouble(16))))*10))/10)*Convert.ToDouble(EntradasPag8);
                    }
                    if (EntradasPag4 > 0)
                    {
                        CostoVariablePag4 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(4) / Convert.ToDouble(16))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(4) / Convert.ToDouble(16)))) * 10)) / 10) * Convert.ToDouble(EntradasPag4);
                    }
                    break;
                case 12:
                    CostoVariablePag12 = (Math.Ceiling((TarifaCostoVariableImpresionInterior + tarifapapelInterior.TarifaMermaVariable) * 10) / 10) * Convert.ToDouble(EntradasPag12);
                    if (EntradasPag8 > 0)
                    {
                        CostoVariablePag8 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(8) / Convert.ToDouble(12))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(8) / Convert.ToDouble(12)))) * 10)) / 10) * Convert.ToDouble(EntradasPag8);
                    }
                    if (EntradasPag4 > 0)
                    {
                        CostoVariablePag4 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(4) / Convert.ToDouble(12))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(4) / Convert.ToDouble(12)))) * 10)) / 10) * Convert.ToDouble(EntradasPag4);
                    }
                    break;
                case 8:
                    CostoVariablePag8 = (Math.Ceiling((TarifaCostoVariableImpresionInterior + tarifapapelInterior.TarifaMermaVariable) * 10) / 10) * Convert.ToDouble(EntradasPag8);
                    if (EntradasPag4 > 0)
                    {
                        CostoVariablePag4 = ((Math.Ceiling(((((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(4) / Convert.ToDouble(8))) + DesintercalarRotativa) + ((tarifapapelInterior.TarifaMermaVariable) * (Convert.ToDouble(4) / Convert.ToDouble(8)))) * 10)) / 10) * Convert.ToDouble(EntradasPag4);
                    }
                    break;
                default:
                    break;
            }

            double CostoVariablePrecioVentaInterior = (CostoVariablePag32 + CostoVariablePag24 + CostoVariablePag16 + CostoVariablePag12 + CostoVariablePag8 + CostoVariablePag4) * Convert.ToDouble(Tiraje);
            #endregion

            #endregion

            #region Tapas
            int CostoFijoTapa = 0;
            int CostoPapelTapaFijo = 0;
            double CostoImpresionTapaPlisadoFijo = 0;
            double CostoImpresionTapaAcuosoFijo = 0;
            double CostoFijoPrecioVentaInteriorTapa = 0;
            Tarifa_Papel tarifapapelTapa = controlTarifa.TarifaCostoPapel(GramajeTapa, "tapa", MaquinaTapa, PapelTapa, "Cencosud", Formato);
            if (Pagtapa > 0)
            {
                
                CostoFijoTapa = Convert.ToInt32(controlTarifa.TarifaCostoImpresion("Impresion Tapa 4/4", "", MaquinaTapa, "Fijo"));
                CostoPapelTapaFijo = tarifapapelTapa.TarifaMermaFija;
                if (GramajeTapa >= 140)
                {
                    CostoImpresionTapaPlisadoFijo = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Plizado Tapa", "", MaquinaTapa, "Fijo"));
                    CostoImpresionTapaAcuosoFijo = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Barniz Tapa","",MaquinaTapa,"Fijo"));
                }
                CostoFijoPrecioVentaInteriorTapa = (Math.Ceiling((Convert.ToDouble(CostoFijoTapa) + CostoPapelTapaFijo + CostoImpresionTapaPlisadoFijo) / 100) * 100);
            }
            double TarifaCostoVariableImpresionTapa = 0;
            #region CostoVariablexEntradas
            double CostoVariableTapaImp = 0;
            double CostoVarableTapaPap = 0;
            double CostovariableTapaDoblez = 0;
            double CostoImpresionTapaPlisadoVariable = 0;
            double CostoImpresionTapaBarnizVariable = 0;
            if (Pagtapa > 0)
            {
                TarifaCostoVariableImpresionTapa = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Impresion Tapa 4/4", "", MaquinaTapa, "Variable"));
                double CostoPapelTapaVariable = tarifapapelTapa.TarifaMermaVariable;
                switch (EntradasxFormato)
                {
                    case 8:
                        CostoVariableTapaImp = ((TarifaCostoVariableImpresionTapa * Convert.ToDouble(4)) / Convert.ToDouble(2));
                        CostoVarableTapaPap = ((CostoPapelTapaVariable * Convert.ToDouble(4)) / Convert.ToDouble(2));
                        break;
                    case 12:
                        CostoVariableTapaImp = ((TarifaCostoVariableImpresionTapa * Convert.ToDouble(4)) / Convert.ToDouble(3));
                        CostoVarableTapaPap = ((CostoPapelTapaVariable * Convert.ToDouble(4)) / Convert.ToDouble(3));
                        break;
                    default:
                        CostoVariableTapaImp = ((TarifaCostoVariableImpresionTapa * Convert.ToDouble(4)) / Convert.ToDouble(4));
                        CostoVarableTapaPap = ((CostoPapelTapaVariable * Convert.ToDouble(4)) / Convert.ToDouble(4));
                        break;
                }
                if (GramajeTapa < 130)
                {
                    CostovariableTapaDoblez = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Doblez Tapa", "", MaquinaTapa, "Variable"));
                }
                else 
                {
                    if (GramajeTapa >= 140)
                    {
                        CostoImpresionTapaPlisadoVariable = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Plizado Tapa", "", MaquinaTapa, "Variable"));
                    }
                    if (GramajeTapa >= 170)
                    {
                        double CostoBarnizVariable = controlTarifa.TarifaCostoImpresionTapaBarniz(GramajeTapa, Formato);
                        switch (EntradasxFormato)
                        {
                            case 8:
                                CostoImpresionTapaBarnizVariable = ((Math.Ceiling((CostoBarnizVariable / Convert.ToDouble(2)) * 100)) / 100) * Convert.ToDouble(Tiraje);
                                break;
                            case 12:
                                CostoImpresionTapaBarnizVariable = ((Math.Ceiling((CostoBarnizVariable / Convert.ToDouble(3)) * 100)) / 100) * Convert.ToDouble(Tiraje);

                                break;
                            default:
                                CostoImpresionTapaBarnizVariable = ((Math.Ceiling((CostoBarnizVariable / Convert.ToDouble(4)) * 100)) / 100) * Convert.ToDouble(Tiraje);
                                break;
                        }
                    }
                }
            }

            double CostoVariablePrecioVentaTapa = (Math.Ceiling((Convert.ToDouble(CostoVariableTapaImp) + CostoVarableTapaPap + CostovariableTapaDoblez) * 10) / 10) * Convert.ToDouble(Tiraje);
            
            #endregion
            #endregion
            
            #region Encuadernacion

            int valorEncuaFijo = 0;
            double valorEncuaVari = 0;
            double valorEncuadernacion = 0;
            if (Encuadernacion != "No")
            {
                valorEncuaFijo = controlTarifa.TarifaEncuadernacion(Encuadernacion, "Fijo");
                valorEncuaVari = (controlTarifa.TarifaEncuadernacion(Encuadernacion, "Variable") * Convert.ToDouble(Tiraje));
                valorEncuadernacion = (valorEncuaFijo + valorEncuaVari);
            }
            #endregion

            #region Totales

            double CostoCatalogoFijo = (CostoFijoPrecioVentaInterior + CostoFijoPrecioVentaInteriorTapa + valorEncuaFijo + CostoImpresionTapaAcuosoFijo);//CostoTotalManufacturaFijo + CostototalFijoPapel);
            double CostoCatalogoVariable = (CostoVariablePrecioVentaInterior + CostoVariablePrecioVentaTapa + valorEncuaVari + CostoImpresionTapaBarnizVariable);
            double CostoCatalogoFinal = (CostoCatalogoFijo + CostoCatalogoVariable);


            double CostoUnitario = Math.Ceiling(CostoCatalogoFinal / Convert.ToDouble(Tiraje));
            double CostoMillar = (CostoCatalogoVariable / Convert.ToDouble(Tiraje)) * 1000;
            #endregion

            return new[] { CostoFijoPrecioVentaInterior.ToString(), CostoVariablePrecioVentaInterior.ToString(), (CostoFijoPrecioVentaInterior+ CostoVariablePrecioVentaInterior).ToString()
                ,tarifapapelInterior.PrecioPapel.ToString(), valorEncuaFijo.ToString(), valorEncuaVari.ToString(), valorEncuadernacion.ToString()
                ,CostoFijoPrecioVentaInteriorTapa.ToString(), CostoVariablePrecioVentaTapa.ToString(), (CostoFijoPrecioVentaInteriorTapa + CostoVariablePrecioVentaTapa).ToString()
                ,tarifapapelTapa.PrecioPapel.ToString(),CostoCatalogoFijo.ToString(), CostoCatalogoVariable.ToString(), CostoCatalogoFinal.ToString(), CostoUnitario.ToString(), CostoMillar.ToString()
                , MaquinaInterior, MaquinaTapa,CostoImpresionTapaAcuosoFijo.ToString(),CostoImpresionTapaBarnizVariable.ToString()
            };
        }

        [WebMethod]
        public static string[] GuardarPresupuesto(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion,
                string Tiraje1, string Tiraje2, string Tiraje3, string Empresa, string NombrePres, string Usuario)
        {
            Controller_Cotizador controlpres = new Controller_Cotizador();
            string[] PrecioPrensa1 = CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje1);
            string[] PrecioPrensa2 = CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje2);
            string[] PrecioPrensa3 = CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje3);
            int idpres = controlpres.GuardarPresupuesto(NombrePres, PagInterior, Pagtapa, Formato, GramajeInt1.Replace(" grs", ""), GramajeTapa1.Replace(" grs", ""), Papelinterior, PapelTapa, Encuadernacion, PrecioPrensa1[16],
                    PrecioPrensa1[17], Usuario, Empresa,Tiraje1, Tiraje2, Tiraje3, PrecioPrensa1[13], PrecioPrensa2[13], PrecioPrensa3[13], PrecioPrensa1[14], PrecioPrensa2[14], PrecioPrensa3[14], PrecioPrensa1[15], 
                    PrecioPrensa2[15], PrecioPrensa3[15]);
            if (idpres>0)
            {
                return new[] { "OK", idpres.ToString() };
            }
            else
            {
                return new[] {"Error al crear el registro, intentelo mas tarde"};
            }
        }

    }
}