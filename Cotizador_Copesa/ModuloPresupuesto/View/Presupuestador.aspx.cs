using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cotizador_Copesa.ModuloUsuario.Controller;
using Cotizador_Copesa.ModuloUsuario.Model;
using Cotizador_Copesa.ModuloPresupuesto.Controller;
using Cotizador_Copesa.ModuloPresupuesto.Model;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace Cotizador_Copesa.ModuloPresupuesto.View
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
                CargarBarnizTerminaciones();
            }
        }

        public void CargarUsuario(int id)
        {
            Controller_Usuario controlu = new Controller_Usuario();
            Usuario user = controlu.BuscarUsuario_ID(id);
            if (user.NombreCompleto != null)
            {
                ValorUF();
                if (user.Perfil != "Admin")
                {
                    LabelPrimerPrecio.Visible = false;
                    DIVPrecio.Visible = false;
                }
                CargarPapeles(user.Empresa);
            }
            else
            {
                Response.Redirect("../../index.aspx");
            }
        }

        public void ValorUF()
        {
            Controller_Tarifa preControl = new Controller_Tarifa();
            lblCostoQ.Text = preControl.ValorUF().ToString("N2");
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
            for (int i = 0; i < 600; i += 4)
            {
                lista.Add(i);
            }
            ddlPaginas.DataSource = lista;
            ddlPaginas.DataBind();
        }

        public void CargarPapeles(string Empresa)
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            ddlPapel.DataSource = preControl.List_Papel(Empresa, "Interior").Select(o => o.PapelInterior).Distinct().ToList();
            ddlPapel.DataBind();
            ddlPapel.Items.Insert(0, new ListItem("Seleccione Tipo Papel...", "Seleccione Tipo Papel..."));
            ddlPapTapas.DataSource = preControl.List_Papel(Empresa, "Tapa").Select(o => o.PapelInterior).Distinct().ToList();
            ddlPapTapas.DataBind();
            ddlPapTapas.Items.Insert(0, new ListItem("Seleccione Tipo Papel...", "Seleccione Tipo Papel..."));
        }
        
        public void CargarBarnizTerminaciones()
        {
            Controller_Tarifa preControl = new Controller_Tarifa();
            List<Terminaciones> lista = preControl.Listar_Terminaciones();
            ddlBarnizUV.DataSource = lista.Where(o => o.TipoTerm == "Barniz UV").ToList();
            ddlBarnizUV.DataTextField = "NombreTerminacion";
            ddlBarnizUV.DataValueField = "NombreTerminacion";
            ddlBarnizUV.DataBind();
            ddlBarnizUV.Items.Insert(0, new ListItem("Seleccione Barniz UV...", "0"));
            ddlLaminado.DataSource = lista.Where(o => o.TipoTerm == "Laminado").ToList();
            ddlLaminado.DataTextField = "NombreTerminacion";
            ddlLaminado.DataValueField = "NombreTerminacion";
            ddlLaminado.DataBind();
            ddlLaminado.Items.Insert(0, new ListItem("Seleccione Laminado...", "0"));
            ddlQuintocolor.DataSource = lista.Where(o => o.TipoTerm == "Quinto Color").ToList();
            ddlQuintocolor.DataTextField = "NombreTerminacion";
            ddlQuintocolor.DataValueField = "NombreTerminacion";
            ddlQuintocolor.DataBind();
            ddlQuintocolor.Items.Insert(0, new ListItem("Seleccione Quinto Color...", "0"));
            ddlEmbolsado.DataSource = lista.Where(o => o.TipoTerm == "Embolsado").ToList();
            ddlEmbolsado.DataTextField = "NombreTerminacion";
            ddlEmbolsado.DataValueField = "NombreTerminacion";
            ddlEmbolsado.DataBind();
            ddlEmbolsado.Items.Insert(0, new ListItem("Seleccione Alzado ", "0"));
            ddlAlzado.DataSource = lista.Where(o => o.TipoTerm == "Alzado").ToList();
            ddlAlzado.DataTextField = "NombreTerminacion";
            ddlAlzado.DataValueField = "NombreTerminacion";
            ddlAlzado.DataBind();
            ddlAlzado.Items.Insert(0, new ListItem("Seleccione Alzado ", "0"));
            ddlInsercionManual.DataSource = lista.Where(o => o.TipoTerm == "Inserción manual").ToList();
            ddlInsercionManual.DataTextField = "NombreTerminacion";
            ddlInsercionManual.DataValueField = "NombreTerminacion";
            ddlInsercionManual.DataBind();
            ddlInsercionManual.Items.Insert(0, new ListItem("Seleccione Inserción manual ", "0"));
            ddlFajado.DataSource = lista.Where(o => o.TipoTerm == "Fajado").ToList();
            ddlFajado.DataTextField = "NombreTerminacion";
            ddlFajado.DataValueField = "NombreTerminacion";
            ddlFajado.DataBind();
            ddlFajado.Items.Insert(0, new ListItem("Seleccione Fajado ", "0"));
            ddlAdhesivo.DataSource = lista.Where(o => o.TipoTerm == "Adhesivo").ToList();
            ddlAdhesivo.DataTextField = "NombreTerminacion";
            ddlAdhesivo.DataValueField = "NombreTerminacion";
            ddlAdhesivo.DataBind();
            ddlAdhesivo.Items.Insert(0, new ListItem("Seleccione Adhesivo ", "0"));
            ddlPegado.DataSource = lista.Where(o => o.TipoTerm == "Pegado").ToList();
            ddlPegado.DataTextField = "NombreTerminacion";
            ddlPegado.DataValueField = "NombreTerminacion";
            ddlPegado.DataBind();
            ddlPegado.Items.Insert(0, new ListItem("Seleccione Pegado ", "0"));
            
        }

        [WebMethod]
        public static string CantidadPaginasInterior(string Encuadernacion)
        {
            List<int> lista = new List<int>();
            int iniciofor = 0;
            if (Encuadernacion == "Entapado Hotmelt")
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
            string Tiraje, string QuintoColor, string UV, string Laminado, string BarnizAcuosoTapa, string ValorUF, string Embolsado, string AlzadoElementoPlano, string EmbolsadoMailaRev, string DesembolsadoSimple,
            string Alzado, string InsercionManual, string Pegado, string Fajado, string Adhesivo, string SuministroCajas, string InsercionElemCajaSellado, string Enzunchado, string PegadoSticker)
        {
            string Tabla1 = "";
            string PrecioTotal1 = "";
            string PrecioUnit1 = "";
            Controller_Tarifa preControl = new Controller_Tarifa();
            double ValorUFs = preControl.ValorUF();
            if (Tiraje != "")
            {
                string[] Detalle1 = CreacionTablaDetalle(CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje, QuintoColor, UV,
                    Laminado, BarnizAcuosoTapa, ValorUFs, Embolsado, AlzadoElementoPlano, EmbolsadoMailaRev, DesembolsadoSimple, Alzado, InsercionManual, Pegado, Fajado, Adhesivo, SuministroCajas, InsercionElemCajaSellado,
                    Enzunchado, PegadoSticker), Tiraje);
                Tabla1 = Detalle1[0];
                PrecioTotal1 = Math.Ceiling(Convert.ToDouble(Detalle1[1])).ToString("N0");
                PrecioUnit1 = Detalle1[2];
            }
            

            return new[] { Tabla1, PrecioTotal1, PrecioUnit1, "" };
        }

        public static string[] CreacionTablaDetalle(string[] detalle, string Tiraje)
        {
            double PrecioNetoFijo = 0;
            double PrecioNetoVariable = 0;
            double PrecioNetoTotal = 0;

            //EntradasPag16.ToString(), 0
            //CostoFijoPag16.ToString(),1
            //CostoVariablePag16.ToString(),2
            //EntradasPag12.ToString(),3
            //CostoFijoPag12.ToString(),4
            //CostoVariablePag12.ToString(), 5
            //EntradasPag8.ToString(), 6
            //CostoFijoPag8.ToString(),7
            //CostoVariablePag8.ToString(), 8
            //EntradasPag4.ToString(), 9
            //CostoFijoPag4.ToString(),10
            //CostoVariablePag4.ToString(),11
            //EntradasTapa.ToString(), 12
            //ImpresionTapaFijo.ToString(), 13
            //ImpresionTapaVariable.ToString(), 14
            //valorEncuaFijo.ToString(),15
            //valorEncuaVari.ToString(), 16
            //EntradasQuintoColor.ToString(),17
            //QuintoColorFijo.ToString(), 18
            //QuintoColorVari.ToString(), 19
            //BarnizAcuosoFijo.ToString(),20
            //BarnizAcuosoVari.ToString(), 21
            //EmbolsadoVari.ToString(), 22
            //LaminadoVari.ToString(),23
            //BarnizUVFijo.ToString(), 24
            //BarnizUVVari.ToString(), 25
            //EntradasPlizadoTapas.ToString(), 26
            //PlizadoTapaFijo.ToString(), 27
            //PlizadoTapaVari.ToString()28

            PrecioNetoFijo = (Convert.ToDouble(detalle[1]) + // Impresion Interior
                             Convert.ToDouble(detalle[4]) +
                             Convert.ToDouble(detalle[7]) +
                             Convert.ToDouble(detalle[10]) +
                             Convert.ToDouble(detalle[13])+//Tapa
                             Convert.ToDouble(detalle[15]) + //Encuadernacion
                             Convert.ToDouble(detalle[18]) + 
                             Convert.ToDouble(detalle[20]) + //BarnizAcuosoFijo
                             Convert.ToDouble(detalle[24]) + //BarnizUVFijo
                             (Convert.ToDouble(detalle[27]) * Convert.ToDouble(detalle[26])));

            PrecioNetoVariable = (Convert.ToDouble(detalle[2]) + // Impresion Interior
                             Convert.ToDouble(detalle[5]) +
                             Convert.ToDouble(detalle[8])+
                             Convert.ToDouble(detalle[11])+
                             Convert.ToDouble(detalle[14]) +//Tapa
                             Convert.ToDouble(detalle[16]) + //Encuadernacion
                             Convert.ToDouble(detalle[19]) + 
                             Convert.ToDouble(detalle[21]) + //BarnizAcuosoFijo
                             Convert.ToDouble(detalle[25]) + //BarnizUVFijo
                             Convert.ToDouble(detalle[28]));

            PrecioNetoTotal = PrecioNetoFijo + (PrecioNetoVariable* Convert.ToDouble(Tiraje.Replace(".",string.Empty)));

            double PrecioUnitario = (Math.Ceiling((PrecioNetoTotal / Convert.ToDouble(Tiraje.Replace(".", string.Empty))) * 100) / 100);
            //int EntradasUV = 0;
            //if (detalle[35].ToString() != "Seleccione Barniz UV...")
            //{
            //    EntradasUV = 1;
            //}
            //int EntradasLaminado = 0;
            //if (detalle[40].ToString() != "Seleccione Laminado...")
            //{
            //    EntradasLaminado = 1;
            //}
            string TablaDetalle =
                "<table class='table table-bordered table-hover'>" +
                "<thead>" +
                    "<tr>" +
                        "<th>Costos</th>" +
                        "<th># Entradas</th>" +
                        "<th>C.Fijo</th>" +
                        "<th>C.Variable</th>" +
                        "<th>Total CF</th>" +
                        "<th>Total CV</th>" +
                        "<th>Total Final</th>" +
                    "</tr>" +
                "</thead>" +
                "<tbody>" +
            #region Impresion interior
 "<tr>" +
                        "<th colspan='7' scope='row'>Impresión Pliegos de Interior</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>16 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[0] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[1] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[2] + "</td>" +

                        "<td style='text-align: right;'>" + detalle[1] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[2] + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>12 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[3] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[4] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[5] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[4] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[5] + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>8 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[6] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[7] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[8] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[7] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[8] + "</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>4 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[9] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[10] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[11] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[10] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[11] + "</td>" +
                        "<td></td>" +
                    "</tr>" +
            #endregion
            #region Tapas
 "<tr>" +
                        "<th colspan='7' scope='row'>Impresión Tapa</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Impresión Tapa</td>" +
                        "<td style='text-align: right;'>" + detalle[12] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[13] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[14] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[13] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[14] + "</td>" +
                        "<td style='text-align: right;'></td>" +
                    "</tr>" +
                   
            #endregion
            #region Encuadernación
 "<tr>" +
                        "<th colspan='7' scope='row'>Encuadernación</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>NombreEncuadernacion</td>" +//nombre de la encuadernacion
                        "<td style='text-align:right;'>" + ((detalle[15]!="0") ? "1":"0")  + "</td>" +
                        "<td style='text-align:right;'>" + detalle[15] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[16] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[15] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[16] + "</td>" +
                        "<td></td>" +
                    "</tr>" +
            #endregion
            #region Otros Impresión
                     "<tr>" +
                        "<th colspan='7' scope='row'>Otros Impresión</th>" +
                    "</tr>" +
                     "<tr>" +
                        "<td>Nombre Quinto Color</td>" +//nombre del quinto color
                        "<td style='text-align: right;'>" + detalle[17] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[18] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[19] + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToDouble(detalle[18]) * Convert.ToDouble(detalle[17])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToDouble(detalle[19]) * Convert.ToDouble(detalle[17])).ToString() + "</td>" +
                        "<td style='text-align: right;'></td>" +
                    "</tr>" +
                   "<tr>" +
                        "<td>Barniz Acuoso</td>" +
                        "<td style='text-align:right;'>"+((detalle[20]!="0")?"1":"0")+"</td>" +
                        "<td style='text-align:right;'>" + detalle[20] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[21] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[20] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[21] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                    "</tr>" +
            #endregion
            #region Terminacion
 "<tr>" +
                        "<th colspan='7' scope='row'>Procesos Mecánicos</th>" +
                    "</tr>" +
                     "<tr>" +
                        "<td>Nombre Embolsado</td>" +
                        "<td style='text-align:right;'>" + ((detalle[22]!="0") ? "1":"0") + "</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[22] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[22] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Nombre Laminado</td>" +
                        "<td style='text-align:right;'>" + ((detalle[23]!="0") ? "1":"0") + "</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[23] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[23] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Nombre Barniz UV</td>" +
                        "<td style='text-align:right;'>" + ((detalle[24]!="0") ? "1":"0") + "</td>" +
                        "<td style='text-align:right;'>" + detalle[24] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[25] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[24] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[25] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                    "</tr>" +
                     "<tr>" +
                        "<td>Troquelado</td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                    "</tr>" +
                     "<tr>" +
                        "<td>Plisado Tapa</td>" +
                        "<td style='text-align:right;'>" + ((detalle[27]!="0") ? "1":"0") + "</td>" +
                        "<td style='text-align: right;'>" + detalle[27] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[28] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[27] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[28] + "</td>" +
                        "<td style='text-align: right;'></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Corte Frontal en Guillotina</td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                        "<td style='text-align: right;'></td>" +
                    "</tr>" +
                    
                    
                    
            #endregion
            #region Totales
 "<tr>" +
                        "<th  scope='row'>Total Neto</th>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + PrecioNetoFijo.ToString() + "</td>" +
                        "<td style='text-align:right;'>" + PrecioNetoVariable.ToString() + "</td>" +
                        "<td style='text-align:right;'>" + PrecioNetoTotal.ToString() + "</td>" +
                    "</tr>"+
                    
                    "<tr>" +
                        "<th  scope='row'>Precio Unitario Neto</th>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + PrecioUnitario.ToString() + "</td>" +
                    "</tr></tbody>";
            #endregion
            return new[] { TablaDetalle, PrecioNetoTotal.ToString(), PrecioUnitario.ToString(), detalle[29], detalle[30]};
        }

        public static string[] CalcularPreciosPrensa(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion,
            string Tiraje, string QuintoColor, string UV, string Laminado, string BarnizAcuosoTapa, double ValorUF, string Embolsado, string AlzadoElementoPlano, string EmbolsadoMailaRev, string DesembolsadoSimple,
            string Alzado, string InsercionManual, string Pegado, string Fajado, string Adhesivo, string SuministroCajas, string InsercionElemCajaSellado, string Enzunchado, string PegadoSticker)
        {
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
            if ("Seleccione Gramaje Papel..." != GramajeTapa1 && GramajeTapa1 != "")
            {
                GramajeTapa = Convert.ToInt32(GramajeTapa1.Replace(" grs", ""));
            }
            if (Encuadernacion != "No")
            {
                if (Encuadernacion == "Hotmelt")
                {
                    Encuadernacion = "Hotmelt";
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
            Controller_Tarifa controlTarifa = new Controller_Tarifa();
            #region Impresion Interior Nuevo

            double CostoFijoPag16 = 0;
            double CostoFijoPag12 = 0;
            double CostoFijoPag8 = 0;
            double CostoFijoPag4 = 0;

            double CostoVariablePag16 = 0;
            double CostoVariablePag12 = 0;
            double CostoVariablePag8 = 0;
            double CostoVariablePag4 = 0;

            List<Impresion> lista = controlTarifa.ListarImpresion(MaquinaInterior);
            string PaginasTapas = "";
            switch (EntradasxFormato)
            {
                case 16: CostoFijoPag16 = EntradasPag16 * Math.Ceiling(lista.Where(x => (x.TipoCosto == "Fijo") && (x.Paginas == "16 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF);
                    CostoFijoPag12 = EntradasPag12 * Math.Ceiling(lista.Where(x => (x.TipoCosto == "Fijo") && (x.Paginas == "12 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF);
                    CostoFijoPag8 = EntradasPag8 * Math.Ceiling(lista.Where(x => (x.TipoCosto == "Fijo") && (x.Paginas == "8 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF);
                    CostoFijoPag4 = EntradasPag4 * Math.Ceiling(lista.Where(x => (x.TipoCosto == "Fijo") && (x.Paginas == "4 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF);
                    CostoVariablePag16 = EntradasPag16 * (Math.Ceiling(((lista.Where(x => (x.TipoCosto == "Variable") && (x.Paginas == "16 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                    CostoVariablePag12 = EntradasPag12 * (Math.Ceiling(((lista.Where(x => (x.TipoCosto == "Variable") && (x.Paginas == "12 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                    CostoVariablePag8 = EntradasPag8 * (Math.Ceiling(((lista.Where(x => (x.TipoCosto == "Variable") && (x.Paginas == "8 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                    CostoVariablePag4 = EntradasPag4 * (Math.Ceiling(((lista.Where(x => (x.TipoCosto == "Variable") && (x.Paginas == "4 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                    PaginasTapas = "16 Paginas";
                    break;
                case 12: CostoFijoPag12 = EntradasPag12 * Math.Ceiling(lista.Where(x => (x.TipoCosto == "Fijo") && (x.Paginas == "12 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF);
                    CostoFijoPag8 = EntradasPag8 * Math.Ceiling(lista.Where(x => (x.TipoCosto == "Fijo") && (x.Paginas == "8 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF);
                    CostoFijoPag4 = EntradasPag4 * Math.Ceiling(lista.Where(x => (x.TipoCosto == "Fijo") && (x.Paginas == "4 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF);
                    CostoVariablePag12 = EntradasPag12 * (Math.Ceiling(((lista.Where(x => (x.TipoCosto == "Variable") && (x.Paginas == "12 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                    CostoVariablePag8 = EntradasPag8 * (Math.Ceiling(((lista.Where(x => (x.TipoCosto == "Variable") && (x.Paginas == "8 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                    CostoVariablePag4 = EntradasPag4 * (Math.Ceiling(((lista.Where(x => (x.TipoCosto == "Variable") && (x.Paginas == "4 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                    PaginasTapas = "12 Paginas";
                    break;
                case 8: CostoFijoPag8 = EntradasPag8 * Math.Ceiling(lista.Where(x => (x.TipoCosto == "Fijo") && (x.Paginas == "8 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF);
                    CostoFijoPag4 = EntradasPag4 * Math.Ceiling(lista.Where(x => (x.TipoCosto == "Fijo") && (x.Paginas == "4 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF);
                    CostoVariablePag8 = EntradasPag8 * (Math.Ceiling(((lista.Where(x => (x.TipoCosto == "Variable") && (x.Paginas == "8 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                    CostoVariablePag4 = EntradasPag4 * (Math.Ceiling(((lista.Where(x => (x.TipoCosto == "Variable") && (x.Paginas == "4 Paginas")).Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                    PaginasTapas = "8 Paginas";
                    break;
                default: break;
            }

            #endregion
            #region Barniz Acuoso int
            //int TarifaCostoFijoBarnizInt = 0;
            //double TarifaCostoVariableBarnizAcuosoInt = Convert.ToDouble(controlTarifa.TarifaCostoBarnizAcuoso(GramajeInt, Formato, MaquinaInterior, Empresa));
            //int CostoFijo16Barniz = 0;
            //int CostoFijo12Barniz = 0;
            //int CostoFijo8Barniz = 0;
            //double CostoVari16Barniz = 0;
            //double CostoVari12Barniz = 0;
            //double CostoVari8Barniz = 0;
            //double CostoVari4Barniz = 0;
            //if (BarnizAcuosoInt != "No")
            //{
            //    TarifaCostoFijoBarnizInt = Convert.ToInt32(controlTarifa.TarifaCostoImpresion("Barniz Acuoso Parejo", "", MaquinaInterior, "Fijo", Empresa));
            //    switch (EntradasxFormato)
            //    {
            //        case 16:
            //            CostoFijo16Barniz = TarifaCostoFijoBarnizInt;
            //            CostoVari16Barniz = TarifaCostoVariableBarnizAcuosoInt;
            //            CostoVari8Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(8) / Convert.ToDouble(16))) * 100) / 100);
            //            CostoVari4Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(4) / Convert.ToDouble(16))) * 100) / 100);
            //            break;
            //        case 12:
            //            CostoFijo12Barniz = TarifaCostoFijoBarnizInt;
            //            CostoVari12Barniz = TarifaCostoVariableBarnizAcuosoInt;
            //            CostoVari8Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(8) / Convert.ToDouble(12))) * 100) / 100);
            //            CostoVari4Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(4) / Convert.ToDouble(12))) * 100) / 100);
            //            break;
            //        case 8:
            //            CostoFijo8Barniz = TarifaCostoFijoBarnizInt;
            //            CostoVari8Barniz = TarifaCostoVariableBarnizAcuosoInt;
            //            CostoVari4Barniz = (Math.Ceiling((TarifaCostoVariableBarnizAcuosoInt * (Convert.ToDouble(4) / Convert.ToDouble(8))) * 100) / 100);
            //            break;
            //    }
            //}
            #endregion
            #region Tapas Nuevo
            int EntradasTapa = 0;
            double ImpresionTapaFijo = 0;
            double ImpresionTapaVariable = 0;
            int EntradasQuintoColor = 0;
            double QuintoColorFijo = 0;
            double QuintoColorVari = 0;
            int EntradasPlizadoTapas = 0;
            double PlizadoTapaFijo = 0;
            double PlizadoTapaVari = 0;

            List<Terminaciones> listaCostoTerm = controlTarifa.Listar_Terminaciones();
            if (Pagtapa > 0)
            {
                EntradasTapa = 1;
                

                List<Impresion> lista2 = controlTarifa.ListarImpresion(MaquinaTapa).Where(x=>x.Paginas == PaginasTapas).ToList();
                ImpresionTapaFijo = Math.Ceiling(lista2.Where(x => x.TipoCosto == "Fijo").Select(x=>x.Costo).FirstOrDefault() * ValorUF);
                ImpresionTapaVariable = (Math.Ceiling(((lista2.Where(x => x.TipoCosto == "Variable").Select(x => x.Costo).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0) / Pagtapa;

                if (QuintoColor != "Seleccionar")
                {
                    EntradasQuintoColor = 1;
                    QuintoColorFijo = Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == QuintoColor).Select(x => x.CostoFijo).FirstOrDefault()*ValorUF);
                    QuintoColorVari = (Math.Ceiling(((listaCostoTerm.Where(x => x.NombreTerminacion == QuintoColor).Select(x => x.CostoVari).FirstOrDefault() * ValorUF) / 1000.0) * 100.0) / 100.0);
                }
                if (GramajeTapa >= 300)
                {
                    EntradasPlizadoTapas = 1;
                    PlizadoTapaFijo = listaCostoTerm.Where(x => x.NombreTerminacion == "Plizado").Select(x => x.CostoFijo).FirstOrDefault();
                    PlizadoTapaVari = listaCostoTerm.Where(x => x.NombreTerminacion == "Plizado").Select(x => x.CostoVari).FirstOrDefault();
                }
            }
            #endregion
            #region Encuadernacion Nuevo
            double valorEncuaFijo = 0;
            double valorEncuaVari = 0;
            if (Encuadernacion != "No")
            {
                Encuadernacion ListEnc = controlTarifa.ListarEncuadernacion(Encuadernacion);
                valorEncuaFijo = Math.Ceiling(ListEnc.ValorFijoEnc * ValorUF);
                valorEncuaVari = (Math.Ceiling(((ListEnc.ValorVariEnc * ValorUF) / 1000.0) * 100.0) / 100.0);
            }
            #endregion
            #region Terminaciones Nuevas
            double BarnizUVFijo = (UV != "Seleccionar") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == UV).Select(x => x.CostoFijo).FirstOrDefault()) : 0;
            double BarnizUVVari = (UV != "Seleccionar") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == UV).Select(x => x.CostoVari).FirstOrDefault()) : 0;
            double EmbolsadoVari = (Embolsado != "Seleccionar") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == Embolsado).Select(x => x.CostoVari).FirstOrDefault()) : 0;
            double LaminadoVari = (Laminado != "Seleccionar") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == Laminado).Select(x => x.CostoVari).FirstOrDefault()) : 0;
            double BarnizAcuosoFijo = ((Pagtapa > 0) && (BarnizAcuosoTapa!="No")) ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == "Barniz Acuoso (solo tiro)").Select(x => x.CostoFijo).FirstOrDefault() *ValorUF) : 0;
            double BarnizAcuosoVari = ((Pagtapa > 0) && (BarnizAcuosoTapa!="No")) ? Math.Ceiling(((listaCostoTerm.Where(x => x.NombreTerminacion == "Barniz Acuoso (solo tiro)").Select(x => x.CostoVari).FirstOrDefault() *ValorUF)/1000.0)*100.0)/100.0 : 0;


            #endregion
            #region Manualidades
            double AlzadoElementoPlanoVari = (AlzadoElementoPlano == "Si") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == "Alzados").Select(x => x.CostoFijo).FirstOrDefault()) : 0;
            double EmbolsadoMailaRevVari = (EmbolsadoMailaRev == "Si") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == "Embolsado manual").Select(x => x.CostoFijo).FirstOrDefault()) : 0;
            double DesembolsadoSimpleVari = (DesembolsadoSimple == "Si") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == "Desembolsado simple").Select(x => x.CostoFijo).FirstOrDefault()) : 0;
            double AlzadoVari = (Alzado != "Seleccione Alzado") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == Alzado).Select(x => x.CostoVari).FirstOrDefault()) : 0;
            double InsercionManualVari = (InsercionManual != "Seleccione Inserción manual ") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == InsercionManual).Select(x => x.CostoVari).FirstOrDefault()) : 0;
            double PegadoVari = (Pegado!= "Seleccione Pegado") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == Pegado).Select(x => x.CostoVari).FirstOrDefault()) : 0;
            double FajadoVari = (Fajado != "Seleccione Fajado") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == Fajado).Select(x => x.CostoVari).FirstOrDefault()) : 0;
            double PegadoStickerVari = (PegadoSticker != "Seleccione") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == "Pegado de Sticker").Select(x => x.CostoVari).FirstOrDefault()) : 0;
            double SuministroCajasVari = (SuministroCajas != "Seleccione") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == "Suministro de cajas").Select(x => x.CostoVari).FirstOrDefault()) : 0;//Mal Calculado
            double InsercionElemCajaSelladoVari = (InsercionElemCajaSellado != "Seleccione") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == "Caja y sellado de caja").Select(x => x.CostoVari).FirstOrDefault()) : 0;//Mal Calculado
            double EnzunchadoVari = (Enzunchado != "Seleccione") ? Math.Ceiling(listaCostoTerm.Where(x => x.NombreTerminacion == "Paquete manualidad").Select(x => x.CostoVari).FirstOrDefault()) : 0;//Mal Calculado


            #endregion
            return new[] {  EntradasPag16.ToString(), CostoFijoPag16.ToString(),CostoVariablePag16.ToString(), EntradasPag12.ToString(), CostoFijoPag12.ToString(),CostoVariablePag12.ToString(), 
                            EntradasPag8.ToString(), CostoFijoPag8.ToString(),CostoVariablePag8.ToString(), EntradasPag4.ToString(), CostoFijoPag4.ToString(),CostoVariablePag4.ToString(),
                            EntradasTapa.ToString(), ImpresionTapaFijo.ToString(), ImpresionTapaVariable.ToString(), valorEncuaFijo.ToString(), valorEncuaVari.ToString(), EntradasQuintoColor.ToString(),
                            QuintoColorFijo.ToString(), QuintoColorVari.ToString(), BarnizAcuosoFijo.ToString(), BarnizAcuosoVari.ToString(), EmbolsadoVari.ToString(), LaminadoVari.ToString(),
                            BarnizUVFijo.ToString(), BarnizUVVari.ToString(), EntradasPlizadoTapas.ToString(), PlizadoTapaFijo.ToString(), PlizadoTapaVari.ToString(),
                            MaquinaInterior, MaquinaTapa, AlzadoElementoPlanoVari.ToString(), EmbolsadoMailaRevVari.ToString(), DesembolsadoSimpleVari.ToString(), AlzadoVari.ToString(), 
                            InsercionManualVari.ToString(), PegadoVari.ToString(), FajadoVari.ToString(), PegadoStickerVari.ToString(), SuministroCajasVari.ToString(), InsercionElemCajaSelladoVari.ToString(), 
                            EnzunchadoVari.ToString(), Encuadernacion, QuintoColor, Embolsado, Laminado, UV
                            };
        }

        [WebMethod]
        public static string[] GuardarPresupuesto(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion,
            string Tiraje, string QuintoColor, string UV, string Laminado, string BarnizAcuosoTapa,
            string NombrePres, string usuario, double ValorUF, string Embolsado, string AlzadoElementoPlano, string EmbolsadoMailaRev, string DesembolsadoSimple,
            string Alzado, string InsercionManual, string Pegado, string Fajado, string Adhesivo, string SuministroCajas, string InsercionElemCajaSellado, string Enzunchado, string PegadoSticker)
        {
            Controller_Cotizador controlpres = new Controller_Cotizador();
            //string Tabla1 ="";
            string PrecioTotal1 = "";
            string PrecioUnit1 = "";
            string MaquinaInt = "";
            string MaquinaTap = "";
            Controller_Tarifa preControl = new Controller_Tarifa();
            double ValorUFs = preControl.ValorUF();
            if (Tiraje != "")
            {
                string[] PrecioPrensa1 = CreacionTablaDetalle(CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje, QuintoColor, UV,
                        Laminado, BarnizAcuosoTapa, ValorUFs, Embolsado, AlzadoElementoPlano, EmbolsadoMailaRev, DesembolsadoSimple, Alzado, InsercionManual, Pegado, Fajado, Adhesivo, SuministroCajas, InsercionElemCajaSellado,
                    Enzunchado, PegadoSticker), Tiraje);

                PrecioTotal1 = PrecioPrensa1[1];
                PrecioUnit1 = PrecioPrensa1[2];
                MaquinaInt = PrecioPrensa1[3];
                MaquinaTap = PrecioPrensa1[3];
            }
            
            int idpres = controlpres.GuardarPresupuesto(NombrePres, PagInterior, Pagtapa, Formato, GramajeInt1.Replace(" grs", ""), GramajeTapa1.Replace(" grs", ""), Papelinterior, PapelTapa, Encuadernacion, MaquinaInt,
                    MaquinaTap, usuario, "Copesa", Tiraje, PrecioTotal1, PrecioUnit1, BarnizAcuosoTapa, QuintoColor, UV, Laminado, Embolsado);
            if (idpres > 0)
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