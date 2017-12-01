using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cotizador_Falabella.ModuloUsuario.Controller;
using Cotizador_Falabella.ModuloUsuario.Model;
using Cotizador_Falabella.ModuloPresupuesto.Controller;
using System.Web.Services;
using Cotizador_Falabella.ModuloPresupuesto.Model;
using System.Web.Script.Serialization;

namespace Cotizador_Falabella.ModuloPresupuesto.View
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
                CargarPaginas();
                CargarQuintoColor();
                CargarBarnizTerminaciones();
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
                CargarPapeles(user.Empresa);
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

        public void CargarPaginas()
        {
            List<int> lista = new List<int>();
            for (int i= 0; i < 600; i+=4)
            {
                lista.Add(i);
            }
            ddlPaginas.DataSource = lista;
            ddlPaginas.DataBind();
        }

        public void CargarPapeles(string Empresa)
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            ddlPapel.DataSource = preControl.List_Papel(Empresa, "Interior").Select(o=>o.PapelInterior).Distinct().ToList();
            //ddlPapel.DataTextField = "PapelInterior";
            //ddlPapel.DataValueField = "PapelInterior";
            ddlPapel.DataBind();
            ddlPapel.Items.Insert(0, new ListItem("Seleccione Tipo Papel...", "Seleccione Tipo Papel..."));
            ddlPapTapas.DataSource = preControl.List_Papel(Empresa, "Tapa").Select(o => o.PapelInterior).Distinct().ToList();
            //ddlPapTapas.DataTextField = "PapelInterior";
            //ddlPapTapas.DataValueField = "PapelInterior";
            ddlPapTapas.DataBind();
            ddlPapTapas.Items.Insert(0, new ListItem("Seleccione Tipo Papel...", "Seleccione Tipo Papel..."));
        }

        public void CargarQuintoColor()
        {
            Controller_Tarifas controlTarifa = new Controller_Tarifas();
            ddlQuintocolor.DataSource = controlTarifa.Listar_QuintoColor();
            ddlQuintocolor.DataTextField = "NombrePapel";
            ddlQuintocolor.DataValueField = "NombrePapel";
            ddlQuintocolor.DataBind();
            ddlQuintocolor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public void CargarBarnizTerminaciones()
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            List<Terminaciones> lista = preControl.Listar_Terminaciones();
            ddlBarnizUV.DataSource = lista.Where(o => o.NombreSumplifacado == "UV").ToList();
            ddlBarnizUV.DataTextField = "NombreTerminacion";
            ddlBarnizUV.DataValueField = "NombreTerminacion";
            ddlBarnizUV.DataBind();
            ddlBarnizUV.Items.Insert(0, new ListItem("Seleccione Barniz UV...", "0"));
            ddlLaminado.DataSource = lista.Where(o => o.NombreSumplifacado == "Laminado").ToList();
            ddlLaminado.DataTextField = "NombreTerminacion";
            ddlLaminado.DataValueField = "NombreTerminacion";
            ddlLaminado.DataBind();
            ddlLaminado.Items.Insert(0, new ListItem("Seleccione Laminado...", "0"));
        }

        [WebMethod]
        public static string CantidadPaginasInterior(string Encuadernacion)
        {
            List<int> lista = new List<int>();
            int iniciofor = 0;
            if (Encuadernacion == "Entapado Hotmelt" || Encuadernacion == "Entapado Pur")
            {
                iniciofor = 48;
            }
            for (int i = iniciofor; i < 600; i += 4)
            {
                lista.Add(i);
            }
            
            JavaScriptSerializer jscript = new JavaScriptSerializer();
            return jscript.Serialize(lista);
        }

        [WebMethod]
        public static string LimitesPapelEncuadernacion(string Encuadernacion, string Empresa)
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            List<string> lista1 = new List<string>();
            List<string> lista2 = new List<string>();
            if (Encuadernacion == "Entapado Hotmelt" || Encuadernacion == "Entapado Pur")
            {
                lista1 = preControl.List_Papel(Empresa, "Interior").Where(o => Convert.ToInt32(o.GramajeInterior) >= 130).Select(o => o.PapelInterior).Distinct().ToList();
            }
            else if (Encuadernacion == "2 Corchete al lomo")
            {
                lista1 = preControl.List_Papel(Empresa, "Interior").Select(o => o.PapelInterior).Distinct().ToList();
            }
            else
            {
                lista1 = preControl.List_Papel(Empresa, "Interior").Select(o => o.PapelInterior).Distinct().ToList();
            }

            int contador = 1;
            foreach (string Papel in lista1)
            {
                if (contador == 1)
                {
                    lista2.Insert(0, "Seleccione Tipo Papel...");

                }
                lista2.Insert(contador, Papel);
                contador++;
            }
            JavaScriptSerializer jscript = new JavaScriptSerializer();
            return jscript.Serialize(lista2);
        }

        [WebMethod]
        public static string Gramage_Papel(string Papel, string Empresa, string Encuadernacion, string Componente)
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            List<Cotizador> lista = preControl.Listar_GramajePapel(Papel, Componente, Empresa, Encuadernacion);
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
        public static string GramajeMinTapa_Papel(string Empresa, string Encuadernacion, int Gramaje)
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            List<string> lista1 = new List<string>();
            List<string> lista2 = new List<string>();
            if (Encuadernacion == "Entapado Hotmelt" || Encuadernacion == "Entapado Pur")
            {
                lista1 = preControl.List_Papel(Empresa, "Tapa").Where(o => Convert.ToInt32(o.GramajeInterior) >= 130).Where(y => Convert.ToInt32(y.GramajeInterior) >= Gramaje).Select(o => o.PapelInterior).Distinct().ToList();
            }
            else if (Encuadernacion == "2 Corchete al lomo")
            {
                lista1 = preControl.List_Papel(Empresa, "Tapa").Where(y => Convert.ToInt32(y.GramajeInterior) >= Gramaje).Select(o => o.PapelInterior).Distinct().ToList();
            }
            else
            {
                lista1 = preControl.List_Papel(Empresa, "Tapa").Where(y => Convert.ToInt32(y.GramajeInterior) >= Gramaje).Select(o => o.PapelInterior).Distinct().ToList();
            }

            int contador = 1;
            foreach (string Papel in lista1)
            {
                if (contador == 1)
                {
                    lista2.Insert(0, "Seleccione Tipo Papel...");

                }
                lista2.Insert(contador, Papel);
                contador++;
            }
            JavaScriptSerializer jscript = new JavaScriptSerializer();
            return jscript.Serialize(lista2);
        }

        [WebMethod]
        public static string[] PrePrensa(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion,
            string Tiraje1, string Tiraje2, string Tiraje3, string QuintoColor, string UV, string Laminado, string BarnizAcuosoTapa, string BarnizAcuosoInt, string CantidaddeVersionesSodimac, string Segmento, string Empresa)
        {
            string Tabla1 ="";
            string PrecioTotal1 = "";
            string PrecioUnit1 = "";
            if (Tiraje1 != "")
            {
                string[] Detalle1 = CreacionTablaDetalle(CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje1, QuintoColor, UV,
                    Laminado, BarnizAcuosoTapa, BarnizAcuosoInt, CantidaddeVersionesSodimac, Segmento, Empresa), Tiraje1);
                Tabla1 = Detalle1[0];
                PrecioTotal1 = Math.Ceiling(Convert.ToDouble(Detalle1[1])).ToString("N0");
                PrecioUnit1 = Detalle1[2];
            }
            string Tabla2 = "";
            string PrecioTotal2 = "";
            string PrecioUnit2 = "";
            //if (Tiraje2 != "")
            //{
            //    string[] Detalle2 = CreacionTablaDetalle(CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje2, QuintoColor, UV,
            //        Laminado, BarnizAcuosoTapa, BarnizAcuosoInt, CantidaddeVersionesSodimac, Segmento, Empresa), Tiraje2);
            //    Tabla2 = Detalle2[0];
            //    PrecioTotal2 = Math.Ceiling(Convert.ToDouble(Detalle2[1])).ToString("N0");
            //    PrecioUnit2 = Detalle2[2];
            //}
            string Tabla3 = "";
            string PrecioTotal3 = "";
            string PrecioUnit3 = "";
            //if (Tiraje3 != "")
            //{
            //    string[] Detalle3 = CreacionTablaDetalle(CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje3, QuintoColor, UV,
            //        Laminado, BarnizAcuosoTapa, BarnizAcuosoInt, CantidaddeVersionesSodimac, Segmento, Empresa), Tiraje3);
            //    Tabla3 = Detalle3[0];
            //    PrecioTotal3 = Math.Ceiling(Convert.ToDouble(Detalle3[1])).ToString("N0");
            //    PrecioUnit3 = Detalle3[2];
            //}

            return new[] { Tabla1, PrecioTotal1, PrecioUnit1, "", Tabla2, PrecioTotal2, PrecioUnit2, "", Tabla3, PrecioTotal3, PrecioUnit3, "" };
        }

        public static string[] CreacionTablaDetalle(string[] detalle, string Tiraje)
        {
            double PrecioNetoFijo = 0;
            double PrecioNetoVariable = 0;
            double PrecioNetoTotal = 0;
            PrecioNetoFijo = (Convert.ToDouble(detalle[1]) * Convert.ToDouble(detalle[0])) + // Impresion Interior
                             (Convert.ToDouble(detalle[2]) * Convert.ToDouble(detalle[0])) +
                             (Convert.ToDouble(detalle[3]) * Convert.ToDouble(detalle[0])) +
                             (Convert.ToDouble(detalle[4]) * Convert.ToDouble(detalle[0])) +
                             Convert.ToDouble(detalle[17]) + Convert.ToDouble(detalle[21]) + Convert.ToDouble(detalle[24]) +//Tapa
                             Convert.ToDouble(detalle[28]) + //Encuadernacion
                             (Convert.ToDouble(detalle[30]) * Convert.ToDouble(detalle[1])) + //preprensa
                             (Convert.ToDouble(detalle[31]) * Convert.ToDouble(detalle[2])) +
                             (Convert.ToDouble(detalle[32]) * Convert.ToDouble(detalle[3])) +
                             (Convert.ToDouble(detalle[33]) * Convert.ToDouble(detalle[4])) +
                             (Convert.ToDouble(detalle[34]) * 1) +
                             (Convert.ToDouble(detalle[36]) + Convert.ToDouble(detalle[42])) + //Terminacion Tapa
                             Convert.ToDouble(detalle[44]) + Convert.ToDouble(detalle[45]) + Convert.ToDouble(detalle[48]) +//Despacho
                             Convert.ToDouble(detalle[49]) + Convert.ToDouble(detalle[50]); //Papel

            PrecioNetoVariable = (Math.Ceiling(((Math.Ceiling(((Convert.ToDouble(detalle[1]) * Convert.ToDouble(detalle[5])) + // Impresion Interior
                                 (Convert.ToDouble(detalle[2]) * Convert.ToDouble(detalle[6])) +
                                 (Convert.ToDouble(detalle[3]) * Convert.ToDouble(detalle[7])) +
                                 (Convert.ToDouble(detalle[4]) * Convert.ToDouble(detalle[8])) +
                                 Convert.ToDouble(detalle[18]) + Convert.ToDouble(detalle[22]) + Convert.ToDouble(detalle[25]) +//Tapa
                                 Convert.ToDouble(detalle[29]) + //Encuadernacion
                                 (Convert.ToDouble(detalle[37]) + Convert.ToDouble(detalle[43]) + Convert.ToDouble(detalle[41])) + //Terminacion tapa
                                 Convert.ToDouble(detalle[46]) + Convert.ToDouble(detalle[47])) * 100) / 100) + //Despacho
                                 (Convert.ToDouble(detalle[51]) / Convert.ToDouble(Tiraje.Replace(".", ""))) + //Papel
                                 (Convert.ToDouble(detalle[52]) / Convert.ToDouble(Tiraje.Replace(".", ""))))*100)/100);
            PrecioNetoTotal = PrecioNetoFijo + (PrecioNetoVariable * Convert.ToDouble(Tiraje.Replace(".", "")));
            double PrecioUnitario = (Math.Ceiling((PrecioNetoTotal / Convert.ToDouble(Tiraje)) * 100) / 100);
            int EntradasUV = 0;
            if(detalle[35].ToString()!="Seleccione Barniz UV...")
            {
                EntradasUV = 1;
            }
            int EntradasLaminado = 0;
            if (detalle[40].ToString() != "Seleccione Laminado...")
            {
                EntradasLaminado = 1;
            }
            string TablaDetalle = 
                "<table class='table table-hover'>" +
                "<thead>" +
                    "<tr>" +
                        "<th>Costos</th>" +
                        "<th># Entradas</th>" +
                        "<th>CF</th>" +
                        "<th>CV</th>" +
                        "<th>Total CF</th>" +
                        "<th>Total CV</th>" +
                        "<th>Total Final</th>" +
                    "</tr>" +
                "</thead>" +
                "<tbody>" +
            #region Impresion interior
                    "<tr>" +
                        "<th colspan='7' scope='row'>Impresión Interior 4x4 Colores</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>16 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[1] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[0] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[5]),2).ToString() + "</td>" +

                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[1]) * Convert.ToInt32(detalle[0])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round((Convert.ToDouble(detalle[1]) * Convert.ToDouble(detalle[5])),2).ToString() + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>"+
                        "<td>12 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[2] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[0] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[6]),2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[2]) * Convert.ToInt32(detalle[0])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round((Convert.ToDouble(detalle[2]) * Convert.ToDouble(detalle[6])),2).ToString() + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>8 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[3] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[0] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[7]),2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[3]) * Convert.ToInt32(detalle[0])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round((Convert.ToDouble(detalle[3]) * Convert.ToDouble(detalle[7])),2).ToString() + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>4 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[4] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[0] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[8]),2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[4]) * Convert.ToInt32(detalle[0])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round((Convert.ToDouble(detalle[4]) * Convert.ToDouble(detalle[8])),2).ToString() + "</td>" +
                        "<td></td>" +
                    "</tr>" +
            #endregion
            #region Barniz Acuoso Int
                    "<tr>" +
                        "<th colspan='7' scope='row'>Barniz Acuoso Parejo</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>16 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[1] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[9] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[10] + "</td>" +

                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>12 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[2] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[11] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[12] + "</td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>8 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[3] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[13] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[14] + "</td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>4 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[4] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + detalle[15] + "</td>" +

                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                    "</tr>" +
            #endregion
            #region Tapas
                    "<tr>" +
                        "<th colspan='7' scope='row'>Tapas</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Impresión Tapa</td>" +
                        "<td style='text-align: right;'>" + detalle[16] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[17] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[18]), 2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[16]) * Convert.ToInt32(detalle[17])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[18]), 2).ToString() + "</td>" +
                        "<td style='text-align: right;'></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>" + detalle[19].Replace("Seleccionar","Sin Quinto Color") + "</td>" +//nombre del quinto color
                        "<td style='text-align: right;'>" + detalle[20] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[21] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[22]),2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[20]) * Convert.ToInt32(detalle[21])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[22]), 2).ToString() + "</td>" +
                        "<td style='text-align: right;'></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Plisado Tapa</td>" +
                        "<td style='text-align: right;'>" + detalle[23] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[24] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[25]),2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[23]) * Convert.ToInt32(detalle[24])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[25]), 2).ToString() + "</td>" +
                        "<td style='text-align: right;'></td>" +
                    "</tr>" +
            #endregion
            #region Encuadernación
                    "<tr>" +
                        "<th colspan='7' scope='row'>Encuadernación</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>"+detalle[26]+"</td>" +//nombre de la encuadernacion
                        "<td style='text-align:right;'>" + detalle[27] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[28] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[29] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[28] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[29] + "</td>" +
                        "<td></td>" +
                    "</tr>" +
            #endregion
            #region preprensa
                    "<tr>" +
                        "<th colspan='7' scope='row'>Pre Prensa</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>16 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[1] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[30] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[30]) * Convert.ToDouble(detalle[1])).ToString() + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[30]) * Convert.ToDouble(detalle[1])).ToString() + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>12 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[2] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[31] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[31]) * Convert.ToDouble(detalle[2])).ToString() + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[31]) * Convert.ToDouble(detalle[2])).ToString() + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>8 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[3] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[32] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[32]) * Convert.ToDouble(detalle[3])).ToString() + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[32]) * Convert.ToDouble(detalle[3])).ToString() + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>4 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[4] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[33] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[33]) * Convert.ToDouble(detalle[4])).ToString() + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[33]) * Convert.ToDouble(detalle[4])).ToString() + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Tapa </td>" +
                        "<td style='text-align:right;'>1</td>" +
                        "<td style='text-align:right;'>" + detalle[34] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[34]) * 1).ToString() + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[34]) * 1).ToString() + "</td>" +
                    "</tr>" +
            #endregion
            #region Terminacion
                    "<tr>" +
                        "<th colspan='7' scope='row'>Terminacion Tapa</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>" + detalle[35].Replace("Seleccione Barniz UV...","Sin Barniz UV") + "</td>" +
                        "<td style='text-align:right;'>" + EntradasUV.ToString() + "</td>" +
                        "<td style='text-align:right;'>" + detalle[36] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[37] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[36] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[37] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Barniz Acuoso</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[42] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[43] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[42] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[43] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>" + detalle[40].Replace("Seleccione Laminado...","Sin Laminado") + "</td>" +
                        "<td style='text-align:right;'>" + EntradasLaminado + "</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[41] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[41] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                    "</tr>" +
            #endregion
            #region Despacho
                    "<tr>" +
                        "<th colspan='7' scope='row'>Embalaje y Despacho</th>" +
                    "</tr>" +
                     "<tr>" +
                        "<td>Costo Fijo Despacho</td>" +
                        "<td>1</td>" +
                        "<td style='text-align:right;'>"+detalle[44]+"</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + detalle[44] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + detalle[44] + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>CMC</td>" +
                        "<td>1</td>" +
                        "<td style='text-align:right;'>" + detalle[45] + "</td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[46]).ToString("N2") + "</td>" +
                        "<td style='text-align:right;'>" + detalle[45] + "</td>" +
                        "<td style='text-align:right;'>" +  Convert.ToDouble(detalle[46]).ToString("N2") + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Pallets</td>" +
                        "<td>1</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[47]).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[47]).ToString("N2") + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Flete</td>" +
                        "<td>1</td>" +
                        "<td style='text-align:right;'>" + detalle[48] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + detalle[48] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + detalle[48] + "</td>" +
                    "</tr>" +
            #endregion
            #region Papel
                    "<tr>" +
                        "<th colspan='7' scope='row'>Papel</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Interior</td>" +
                        "<td>1</td>" +
                        "<td style='text-align:right;'>" + detalle[49] + "</td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[51]) / Convert.ToDouble(Tiraje.Replace(".",""))).ToString("N2") + "</td>" +
                        "<td style='text-align:right;'>" + detalle[49] + "</td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[51]) / Convert.ToDouble(Tiraje.Replace(".", ""))).ToString("N2") + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Tapa</td>" +
                        "<td>1</td>" +
                        "<td style='text-align:right;'>" + detalle[50] + "</td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[52]) / Convert.ToDouble(Tiraje.Replace(".", ""))).ToString("N2") + "</td>" +
                        "<td style='text-align:right;'>" + detalle[50] + "</td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[52]) / Convert.ToDouble(Tiraje.Replace(".", ""))).ToString("N2") + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                
            #endregion
            #region Totales
                    "<tr>" +
                        "<th  scope='row'>Precio Neto</th>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + PrecioNetoFijo.ToString() + "</td>" +
                        "<td style='text-align:right;'>" + PrecioNetoVariable.ToString() + "</td>" +
                        "<td style='text-align:right;'>" + PrecioNetoTotal.ToString()+ "</td>" +
                    "</tr></tbody>";
            #endregion
            return new[] { TablaDetalle, PrecioNetoTotal.ToString(), PrecioUnitario.ToString(), detalle[53], detalle[54]};
        }

        public static string[] CalcularPreciosPrensa(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion,
            string Tiraje, string QuintoColor, string UV, string Laminado, string BarnizAcuosoTapa, string BarnizAcuosoInt, string CantidaddeVersionesSodimac, string Segmento, string Empresa)
        {
            int CantidaddeVersiones  = 0;
            if (CantidaddeVersionesSodimac != "")
            {
                CantidaddeVersiones = Convert.ToInt32(CantidaddeVersionesSodimac);
            }

            string MaquinaInterior = "Rotativa";
            string MaquinaTapa = "Plana";
            if (Tiraje != "")
            {
                Tiraje = Tiraje.Replace(".", "");
            }
            else
            {
                Tiraje = "0";
            }
            int GramajeInt = 0;
            if ("Seleccione Gramaje Papel..." != GramajeInt1 && GramajeInt1 != "")
            {
                GramajeInt = Convert.ToInt32(GramajeInt1.Replace(" grs", ""));
                if (GramajeInt >= 140)
                {
                    MaquinaInterior = "Plana";
                }
            }
            int GramajeTapa = 0;
            if ("Seleccione Gramaje Papel..." != GramajeTapa1 && GramajeTapa1!="")
            {
                GramajeTapa = Convert.ToInt32(GramajeTapa1.Replace(" grs", ""));
            }
            if (Encuadernacion != "No")
            {
                if (Encuadernacion == "Hotmelt")
                {
                    Encuadernacion = "Entapado Hotmelt";
                }
            }
            #region EntradasxFormato
            int EntradasPag16 = 0;
            int EntradasPag12 = 0;
            int EntradasPag8 = 0;
            int EntradasPag4 = 0;
            int cantidadfaltante = 0;
            switch (EntradasxFormato)
            {
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
                        EntradasPag4 = 2;
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
            Controller_Tarifas controlTarifa = new Controller_Tarifas();
            #region Impresion interior
            int CostoFijoInterior = Convert.ToInt32(controlTarifa.TarifaCostoImpresion("Impresion Interior 4/4", "", MaquinaInterior, "Fijo",Empresa));
            double TarifaCostoVariableImpresionInterior = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Impresion Interior 4/4", "", MaquinaInterior, "Variable", Empresa));
            
            double DesintercalarRotativa = 1.5;
            if (MaquinaInterior == "Plana")
            {
                DesintercalarRotativa = 0;
            }

            double CostoVariablePag16 = 0;
            double CostoVariablePag12 = 0;
            double CostoVariablePag8 = 0;
            double CostoVariablePag4 = 0;

            switch (EntradasxFormato)
            {
                case 16:
                    CostoVariablePag16 = TarifaCostoVariableImpresionInterior;
                    CostoVariablePag8 = (((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(8) / Convert.ToDouble(16))) + DesintercalarRotativa);
                    CostoVariablePag4 = (((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(4) / Convert.ToDouble(16))) + DesintercalarRotativa)+0.5;
                    break;
                case 12:
                    CostoVariablePag12 = TarifaCostoVariableImpresionInterior;
                    CostoVariablePag8 = (((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(8) / Convert.ToDouble(12))) + DesintercalarRotativa);
                    CostoVariablePag4 = (((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(4) / Convert.ToDouble(12))) + DesintercalarRotativa)+0.5;
                    break;
                case 8:
                    CostoVariablePag8 = TarifaCostoVariableImpresionInterior;
                    CostoVariablePag4 = (((TarifaCostoVariableImpresionInterior) * (Convert.ToDouble(4) / Convert.ToDouble(8))) + DesintercalarRotativa);
                    break;
                default:
                    break;
            }
            #endregion
            #region Barniz Acuoso int
            int TarifaCostoFijoBarnizInt = 0;
            double TarifaCostoVariableBarnizAcuosoInt = Convert.ToDouble(controlTarifa.TarifaCostoBarnizAcuoso(GramajeInt, Formato, MaquinaInterior, Empresa));
            int CostoFijo16Barniz = 0;
            int CostoFijo12Barniz = 0;
            int CostoFijo8Barniz = 0;
            double CostoVari16Barniz = 0;
            double CostoVari12Barniz = 0;
            double CostoVari8Barniz = 0;
            double CostoVari4Barniz = 0;
            if (BarnizAcuosoInt != "No")
            {
                TarifaCostoFijoBarnizInt = Convert.ToInt32(controlTarifa.TarifaCostoImpresion("Barniz Acuoso Parejo", "", MaquinaInterior, "Fijo", Empresa));
                switch (EntradasxFormato)
                {
                    case 16:
                        CostoFijo16Barniz = TarifaCostoFijoBarnizInt;
                        CostoVari16Barniz = TarifaCostoVariableBarnizAcuosoInt;
                        CostoVari8Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(8) / Convert.ToDouble(16))) * 100) / 100);
                        CostoVari4Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(4) / Convert.ToDouble(16))) * 100) / 100);
                        break;
                    case 12:
                        CostoFijo12Barniz = TarifaCostoFijoBarnizInt;
                        CostoVari12Barniz = TarifaCostoVariableBarnizAcuosoInt;
                        CostoVari8Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(8) / Convert.ToDouble(12))) * 100) / 100);
                        CostoVari4Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(4) / Convert.ToDouble(12))) * 100) / 100);
                        break;
                    case 8:
                        CostoFijo8Barniz = TarifaCostoFijoBarnizInt;
                        CostoVari8Barniz = TarifaCostoVariableBarnizAcuosoInt;
                        CostoVari4Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(4) / Convert.ToDouble(8))) * 100) / 100);
                        break;
                }
            }
            #endregion
            #region Tapas
            int EntradasTapa = 0;
            int ImpresionTapaFijo = 0;
            double ImpresionTapaVariable = 0;
            int EstadasQuintoColor = 0;
            int QuintoColorFijo = 0;
            double QuintoColorVariable = 0;
            int EntradasPlizadoTapas = 0;
            int PlizadoTapaFijo = 0;
            double PlizadoTapaVariable = 0;
            if (Pagtapa > 0)
            {
                EntradasTapa = 1;
                ImpresionTapaFijo = Convert.ToInt32(controlTarifa.TarifaCostoImpresion("Impresión Tapa 4/4", "", MaquinaTapa, "Fijo", Empresa));
                ImpresionTapaVariable = controlTarifa.TarifaImpresionTapaVariable(Formato);

                if (QuintoColor != "Seleccionar")
                {
                    EstadasQuintoColor = 1;
                    QuintoColorFijo = Convert.ToInt32(controlTarifa.TarifaCostoImpresion("5° Color", "", MaquinaTapa, "Fijo", Empresa));
                    QuintoColorVariable = controlTarifa.TarifaCostoImpresion(QuintoColor, "", MaquinaTapa, "Variable", Empresa);
                }
                if (GramajeTapa >= 300)
                {
                    EntradasPlizadoTapas = 1;
                    PlizadoTapaFijo = Convert.ToInt32(controlTarifa.TarifaCostoImpresion("Plizado Tapa", "", MaquinaTapa, "Fijo", Empresa));
                    PlizadoTapaVariable = controlTarifa.TarifaPlizadoTapaVariable(Formato,Pagtapa, Empresa);
                }
            }
            #endregion
            #region Encuadernacion
            int cantidadEntradasEnc = 0;
            int valorEncuaFijo = 0;
            double valorEncuaVari = 0;
            if (Encuadernacion != "No")
            {
                cantidadEntradasEnc = EntradasPag16 + EntradasPag12+ EntradasPag8 + EntradasPag4;
                valorEncuaFijo = Convert.ToInt32(controlTarifa.TarifaEncuadernacion(Encuadernacion, cantidadEntradasEnc, "Fijo",Empresa));
                valorEncuaVari = controlTarifa.TarifaEncuadernacion(Encuadernacion, cantidadEntradasEnc, "Variable", Empresa);
            }
            #endregion
            #region Prepresa
            double TarifaPrePrensa = controlTarifa.TarifaPreprensa("PrePrensa", Formato, Empresa);
            double PrePrensa16 = 0;
            double PrePrensa12 = 0;
            double PrePrensa08 = 0;
            double PrePrensa04 = 0;
            double PrePrensaTP = 0;

            switch (EntradasxFormato)
            {
                case 16:
                    PrePrensa16 = TarifaPrePrensa;
                    PrePrensa08 = (TarifaPrePrensa * (Convert.ToDouble(8) / Convert.ToDouble(16)));
                    PrePrensa04 = (TarifaPrePrensa * (Convert.ToDouble(4) / Convert.ToDouble(16)));
                    break;
                case 12:
                    PrePrensa12 = TarifaPrePrensa;
                    PrePrensa08 = (TarifaPrePrensa * (Convert.ToDouble(8) / Convert.ToDouble(12)));
                    PrePrensa04 = (TarifaPrePrensa * (Convert.ToDouble(4) / Convert.ToDouble(12)));
                    break;
                case 8:
                    PrePrensa08 = TarifaPrePrensa;
                    PrePrensa04 = (TarifaPrePrensa * (Convert.ToDouble(4) / Convert.ToDouble(8)));
                    break;
            }
            if (Pagtapa > 0)
            {
                PrePrensaTP = controlTarifa.TarifaPreprensaTapa("PrePrensa", Empresa) * 4;
            }
            #endregion
            #region Terminaciones Tapa
            int BarnizUVFijo = 0;
            double BarnizUVVaraible = 0;
            int GlitterFijo = 0;
            double GlitterVariable = 0;
            double LaminadoVariable = 0;
            int BarnizAcuosoFijo = 0;
            double BarnizAcuosoVariable = 0;

            if (UV != "Seleccionar")
            {
                BarnizUVFijo = Convert.ToInt32(controlTarifa.TarifaTerminacionTapasSelectivo(UV, Formato, "Fijo",Empresa));
                if (UV == "Selectivo")
                {
                    BarnizUVVaraible = controlTarifa.TarifaTerminacionTapasSelectivo(UV, Formato, "Variable", Empresa);
                }
                else
                {
                    BarnizUVVaraible = controlTarifa.TarifaTerminacionTapasMtCuadrados(UV, Formato, Pagtapa, Empresa);
                }
            }
            if (Laminado != "Selecionar")
            {
                LaminadoVariable = controlTarifa.TarifaTerminacionTapasMtCuadrados(Laminado, Formato, Pagtapa, Empresa);
            }
            if (BarnizAcuosoTapa != "No")
            {
                BarnizAcuosoFijo = Convert.ToInt32(controlTarifa.TarifaTerminacionTapasSelectivo("Barniz Acuoso", Formato, "Fijo", Empresa));
                BarnizAcuosoVariable = controlTarifa.TarifaTerminacionTapasMtCuadrados("Barniz Acuoso", Formato, Pagtapa, Empresa);
            }
            #endregion
            #region Embalaje y Despacho
            int Despacho = Convert.ToInt32(controlTarifa.TarifaDespacho("Despacho","Fijo",CantidaddeVersiones));
            int CMCFijo = Convert.ToInt32(controlTarifa.TarifaDespacho("CMC","Fijo",CantidaddeVersiones));
            
            #region CostoFleteMedios
            string[] formatosplit = Formato.Split('x');
            double Espesor8pagInt = 0;
            double Espesor8PagInt_Revista = 0;
            double PesoEjemplarInt = 0;
            int FleteTotal = 0;
            double CostoCMCVariable = 0;
            double CostoPallet = 0;
            if (PagInterior != 0 && Papelinterior != "Seleccione Tipo Papel..." && GramajeInt > 0)
            {
                Espesor8pagInt = (controlTarifa.TarifaDespacho_Espesor(Papelinterior, GramajeInt)) / 10;
                Espesor8PagInt_Revista = (PagInterior / (Convert.ToDouble(8 / Espesor8pagInt)));
                PesoEjemplarInt = ((Convert.ToDouble(formatosplit[0]) / 1000) * (Convert.ToDouble(formatosplit[1]) / 1000) * (Convert.ToDouble(GramajeInt) / 1000) * (Convert.ToDouble(PagInterior) / 2));

                double Espesor8pagTap = 0;
                double Espesor8PagTap_Revista = 0;
                double PesoEjemplarTap = 0;
                if (Pagtapa != 0 && PapelTapa != "Seleccione Tipo Papel..." && GramajeTapa > 0)
                {
                    Espesor8pagTap = (controlTarifa.TarifaDespacho_Espesor(PapelTapa, GramajeTapa)) / 10;
                    Espesor8PagTap_Revista = (Pagtapa / Convert.ToDouble(8 / Espesor8pagTap));
                    PesoEjemplarTap = ((Convert.ToDouble(formatosplit[0]) / 1000) * (Convert.ToDouble(formatosplit[1]) / 1000) * (Convert.ToDouble(GramajeTapa) / 1000) * (Convert.ToDouble(Pagtapa) / 2));
                }
                double EspesorRevista = Espesor8PagInt_Revista + Espesor8PagTap_Revista;


                double PesoEjemplar = PesoEjemplarInt + PesoEjemplarTap;

                int AlturaPaquete = Convert.ToInt32(controlTarifa.TarifaDespacho_Limites("AlturaPaquete", Empresa));
                int EjemplaresxPaquetes = Convert.ToInt32(Math.Floor(AlturaPaquete / EspesorRevista));
                int CantidadEfectivaxPaquete = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(EjemplaresxPaquetes) / 10) * 10);
                double AlturaEfectivaPaquete = EspesorRevista * CantidadEfectivaxPaquete;
                int Paquete_BasePallet = Convert.ToInt32(controlTarifa.TarifaDespacho_Limites("Paquete_BasePallet", Empresa));
                int PesoMaxPallet = Convert.ToInt32(controlTarifa.TarifaDespacho_Limites("PesoMaxPallet", Empresa));
                int AlturaMaxPallet = Convert.ToInt32(controlTarifa.TarifaDespacho_Limites("AlturaMaxPallet", Empresa));
                int Paquete_alturaPallet = 0;
                if (PesoEjemplar > 0)
                {
                    if ((Convert.ToInt32(Math.Ceiling(PesoMaxPallet / PesoEjemplar / CantidadEfectivaxPaquete / Paquete_BasePallet)) * AlturaEfectivaPaquete) < AlturaMaxPallet)
                    {
                        Paquete_alturaPallet = Convert.ToInt32(Math.Ceiling(PesoMaxPallet / PesoEjemplar / CantidadEfectivaxPaquete / Paquete_BasePallet));
                    }
                    else
                    {
                        Paquete_alturaPallet = Convert.ToInt32(Math.Ceiling(AlturaMaxPallet / AlturaEfectivaPaquete));
                    }
                }
                int CantidadPaquetesxPallet = Paquete_BasePallet * Paquete_alturaPallet;
                int CantidadEjemplaresxPallet = CantidadEfectivaxPaquete * CantidadPaquetesxPallet;
                Vehiculo Vehiculo_Rampla = controlTarifa.TarifaDespacho_Vehiculo("Rampla", Empresa);
                Vehiculo Vehiculo_Camion34 = controlTarifa.TarifaDespacho_Vehiculo("Camion 3/4", Empresa);
                Vehiculo Vehiculo_Furgon = controlTarifa.TarifaDespacho_Vehiculo("Furgon", Empresa);
                int Cap_Rampla = Vehiculo_Rampla.Capacidad;
                int Cap_Camion34 = Vehiculo_Camion34.Capacidad;
                Medios medio = controlTarifa.TarifaDespacho_Medios(Empresa);
                double PorcentajeMedios = medio.ProcentajeMedios;
                int CantidadPallet = 0;
                if (CantidadEjemplaresxPallet > 0)
                {
                    CantidadPallet = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(Tiraje) * PorcentajeMedios) / CantidadEjemplaresxPallet));
                }
                int Rampla = CantidadPallet / Cap_Rampla;
                int Camion34 = 0;
                int Sobra = CantidadPallet - (Rampla * Cap_Rampla);
                if (Sobra > 10)
                {
                    Rampla++;
                    Sobra = 0;
                }
                else
                {
                    Camion34 = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Sobra) / Cap_Camion34));
                }
                int CostoRampla = Vehiculo_Rampla.Costo_Flete;
                int CostoCamion34 = Vehiculo_Camion34.Costo_Flete;
                int CostoFleteaMedios = ((Rampla * CostoRampla) + (Camion34 * CostoCamion34));
            #endregion
                #region CostoFleteLocalSantiago
                int CostoFurgon = Vehiculo_Furgon.Costo_Flete;
                int CostoFleteLocalSantiago = 0;
                switch (Empresa)
                {
                    case "Falabella":
                        int LocalStgABC1 = medio.Cant_Stg_abc1;
                        int LocalStgC2C3 = medio.Cant_Stg_c2c3;
                        switch (Segmento)
                        {
                            case "C2C3":
                                CostoFleteLocalSantiago = (LocalStgC2C3) * CostoFurgon;
                                break;
                            case "ABC 1":
                                CostoFleteLocalSantiago = (LocalStgABC1) * CostoFurgon;
                                break;
                            default:
                                CostoFleteLocalSantiago = (medio.Cant_Santiago) * CostoFurgon;
                                break;
                        }
                        break;
                    case "Tottus":
                        CostoFleteLocalSantiago = (medio.Cant_Santiago) * CostoFurgon;
                        break;
                    case "Sodimac": break;
                }

                FleteTotal = CostoFleteLocalSantiago + CostoFleteaMedios;
                #endregion
                #region CMC Variable
                int CantidadPaqueteOT = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Tiraje) / CantidadEfectivaxPaquete / 100)) * 100;
                double PVPMargen = controlTarifa.TarifaDespacho_CostoProduccion(Empresa);
                CostoCMCVariable = (CantidadPaqueteOT * PVPMargen) / Convert.ToDouble(Tiraje);
                #endregion
                #region Pallet
                int MargenPallet = controlTarifa.TarifaDespacho_CostoEmbalaje(Empresa);
                CostoPallet = (CantidadPallet * MargenPallet) / Convert.ToDouble(Tiraje);
                #endregion
            }
            #endregion
            #region Papel
            #region Interior Fijo
            double IntValorKilos = controlTarifa.TarifapapelPrecioPapel(GramajeInt, MaquinaInterior, "Interior", Papelinterior, Empresa);
            double IntEntradasPapel = controlTarifa.TarifapapelMermaFija(Formato, MaquinaInterior, "Interior", GramajeInt, cantidadEntradasEnc);
            double PapelInteriorFijo = Math.Ceiling(IntValorKilos * IntEntradasPapel);
            
            #endregion
            #region Tapa Fijo
            double TapValorKilos = controlTarifa.TarifapapelPrecioPapel(GramajeTapa, MaquinaTapa, "Tapa", PapelTapa, Empresa);
            double TapEntradasPapel = controlTarifa.TarifapapelMermaFija(Formato, MaquinaTapa, "Tapa", GramajeTapa, Pagtapa);
            double PapelTapaFijo = Math.Ceiling(TapValorKilos * TapEntradasPapel);
            #endregion
            #region Interior variable
            double NumeroPliego = Convert.ToDouble(PagInterior) / Convert.ToDouble(EntradasxFormato);
            double IntTirada = controlTarifa.TarifapapelMermaVariable(Formato, MaquinaInterior, "Interior", GramajeInt, NumeroPliego,Convert.ToInt32(Tiraje));//67962.0605265;
            double PapelInteriorVariable = IntTirada * IntValorKilos;
            #endregion
            #region Tapa Variable
            double TapTirada = controlTarifa.TarifapapelMermaVariable(Formato, MaquinaTapa, "Tapa", GramajeTapa, Pagtapa, Convert.ToInt32(Tiraje)); //7133.671104;
            double PapelTapaVariable = TapTirada * TapValorKilos;
            #endregion
            #endregion
            return new[] { CostoFijoInterior.ToString(),EntradasPag16.ToString(), EntradasPag12.ToString(), EntradasPag8.ToString(), EntradasPag4.ToString(), CostoVariablePag16.ToString(), CostoVariablePag12.ToString(),
                CostoVariablePag8.ToString(),  CostoVariablePag4.ToString(), CostoFijo16Barniz.ToString(), CostoVari16Barniz.ToString(), CostoFijo12Barniz.ToString(), CostoVari12Barniz.ToString(), CostoFijo8Barniz.ToString(),
                CostoVari8Barniz.ToString(), CostoVari4Barniz.ToString(),EntradasTapa.ToString(),ImpresionTapaFijo.ToString(), ImpresionTapaVariable.ToString(), QuintoColor, EstadasQuintoColor.ToString(),
                QuintoColorFijo.ToString(), QuintoColorVariable.ToString(),EntradasPlizadoTapas.ToString(), PlizadoTapaFijo.ToString(), PlizadoTapaVariable.ToString(), Encuadernacion, (1).ToString(), valorEncuaFijo.ToString(),
                valorEncuaVari.ToString(), PrePrensa16.ToString(),PrePrensa12.ToString(), PrePrensa08.ToString(), PrePrensa04.ToString(), PrePrensaTP.ToString(),UV, BarnizUVFijo.ToString(), BarnizUVVaraible.ToString(),
                GlitterFijo.ToString(), GlitterVariable.ToString(),Laminado,LaminadoVariable.ToString(), BarnizAcuosoFijo.ToString(), BarnizAcuosoVariable.ToString(), Despacho.ToString(), CMCFijo.ToString(), CostoCMCVariable.ToString(),
                CostoPallet.ToString(), FleteTotal.ToString(), PapelInteriorFijo.ToString(), PapelTapaFijo.ToString(), PapelInteriorVariable.ToString(), PapelTapaVariable.ToString(), MaquinaInterior, MaquinaTapa};
        }

        [WebMethod]
        public static string[] GuardarPresupuesto(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion,
            string Tiraje1, string Tiraje2, string Tiraje3, string QuintoColor, string UV, string Laminado, string BarnizAcuosoTapa, string BarnizAcuosoInt, string CantidaddeVersionesSodimac, string Segmento, string Empresa, 
            string NombrePres, string usuario)
        {
            Controller_Cotizador controlpres = new Controller_Cotizador();
            //string Tabla1 ="";
            string PrecioTotal1 = "";
            string PrecioUnit1 = "";
            string MaquinaInt = "";
            string MaquinaTap = "";
            if (Tiraje1 != "")
            {
                string[] PrecioPrensa1 = CreacionTablaDetalle(CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje1, QuintoColor, UV,
                        Laminado, BarnizAcuosoTapa, BarnizAcuosoInt, CantidaddeVersionesSodimac, Segmento, Empresa), Tiraje1);
                //Tabla1 = PrecioPrensa1[0];
                PrecioTotal1 = Math.Ceiling(Convert.ToDouble(PrecioPrensa1[1])).ToString("N0");
                PrecioUnit1 = PrecioPrensa1[2];
                MaquinaInt = PrecioPrensa1[3];
                MaquinaTap = PrecioPrensa1[4];
            }
            string PrecioTotal2 = "";
            string PrecioUnit2 = "";
            //if (Tiraje2 != "")
            //{
            //    string[] PrecioPrensa2 = CreacionTablaDetalle(CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje2, QuintoColor, UV,
            //            Laminado, BarnizAcuosoTapa, BarnizAcuosoInt, CantidaddeVersionesSodimac, Segmento, Empresa), Tiraje2);
            //    PrecioTotal2 = Math.Ceiling(Convert.ToDouble(PrecioPrensa2[1])).ToString("N0");
            //    PrecioUnit2 = PrecioPrensa2[2];
            //}
            string PrecioTotal3 = "";
            string PrecioUnit3 = "";
            //if (Tiraje3 != "")
            //{
            //    string[] PrecioPrensa3 = CreacionTablaDetalle(CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje3, QuintoColor, UV,
            //            Laminado, BarnizAcuosoTapa, BarnizAcuosoInt, CantidaddeVersionesSodimac, Segmento, Empresa),Tiraje3);
            //    PrecioTotal3 = Math.Ceiling(Convert.ToDouble(PrecioPrensa3[1])).ToString("N0");
            //    PrecioUnit3 = PrecioPrensa3[2];
            //}
            int idpres = controlpres.GuardarPresupuesto(NombrePres, PagInterior, Pagtapa, Formato, GramajeInt1.Replace(" grs", ""), GramajeTapa1.Replace(" grs", ""), Papelinterior, PapelTapa, Encuadernacion, MaquinaInt,
                    MaquinaTap, usuario, Empresa, Tiraje1, Tiraje2, Tiraje3, PrecioTotal1, PrecioTotal2, PrecioTotal3, PrecioUnit1, PrecioUnit2, PrecioUnit3, Segmento, BarnizAcuosoTapa,
                    BarnizAcuosoInt, QuintoColor, UV, Laminado);
            if (idpres>0)
            {
                return new[] { "OK", idpres.ToString() };
            }
            else
            {
                return new[] { "Error al crear el registro, intentelo mas tarde" };
            }
        }
    }
}