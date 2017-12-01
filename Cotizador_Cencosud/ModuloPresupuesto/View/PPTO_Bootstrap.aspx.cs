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

namespace Cotizador_Cencosud.ModuloPresupuesto.View
{
    public partial class PPTO_Bootstrap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFormatos();
                CargarPagInterior("");
                CargarPapeles("Cencosud");
                txtTiraje.Attributes.Add("onkeypress", "return pulsarTiraje(event);");
                CargarValorTrimestre();
            }
        }

        public void CargarFormatos()
        {
            Controller_Cotizador preControl = new Controller_Cotizador();
            ddlFormato.DataSource = preControl.Listar_Formato();
            ddlFormato.DataTextField = "Formato";
            ddlFormato.DataValueField = "Paginas";
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
            ddlPapel.DataTextField = "TipoPapel";
            ddlPapel.DataValueField = "TipoPapel";
            ddlPapel.DataBind();
            ddlPapel.Items.Insert(0, new ListItem("Seleccione Tipo Papel...", "Seleccione Tipo Papel..."));
            ddlPapTapas.DataSource = preControl.List_Papel(Empresa, "Tapa");
            ddlPapTapas.DataTextField = "TipoPapel";
            ddlPapTapas.DataValueField = "TipoPapel";
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
        public static string[] PrePrensa(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion, string Tiraje)
        {
            return CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje);
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

            #region Impresion
            #region Impresion Interior
            int CostoFijoInterior = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Impresion Interior 4/4", "", MaquinaInterior, "Fijo")) * (EntradasPag32 + EntradasPag24 + EntradasPag16 + EntradasPag12 + EntradasPag8 + EntradasPag4)) / 100) * 100);
            #region CostoVariablexEntradas
            double CostoVariableInterior = 0;
            double TarifaCostoVariableImpresionInterior = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Impresion Interior 4/4", "", MaquinaInterior, "Variable"));
            double DesintercalarRotativa = 1.5;
            switch (EntradasxFormato)
            {
                case 32:
                    CostoVariableInterior = Convert.ToDouble(EntradasPag32) * TarifaCostoVariableImpresionInterior +
                                    Convert.ToDouble(EntradasPag16) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(16) / Convert.ToDouble(32))) + DesintercalarRotativa) +
                                    Convert.ToDouble(EntradasPag8) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(8) / Convert.ToDouble(32))) + DesintercalarRotativa) +
                                    Convert.ToDouble(EntradasPag4) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(4) / Convert.ToDouble(32))) + DesintercalarRotativa);
                    break;
                case 24:
                    CostoVariableInterior = Convert.ToDouble(EntradasPag24) * TarifaCostoVariableImpresionInterior +
                                    Convert.ToDouble(EntradasPag12) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(12) / Convert.ToDouble(24))) + DesintercalarRotativa) +
                                    Convert.ToDouble(EntradasPag8) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(8) / Convert.ToDouble(24))) + DesintercalarRotativa) +
                                    Convert.ToDouble(EntradasPag4) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(4) / Convert.ToDouble(24))) + DesintercalarRotativa);
                    break;
                case 16:
                    CostoVariableInterior = Convert.ToDouble(EntradasPag16) * TarifaCostoVariableImpresionInterior +
                                    Convert.ToDouble(EntradasPag8) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(8) / Convert.ToDouble(16))) + DesintercalarRotativa) +
                                    Convert.ToDouble(EntradasPag4) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(4) / Convert.ToDouble(16))) + DesintercalarRotativa);
                    break;
                case 12:
                    CostoVariableInterior = Convert.ToDouble(EntradasPag12) * TarifaCostoVariableImpresionInterior +
                                    Convert.ToDouble(EntradasPag8) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(8) / Convert.ToDouble(12))) + DesintercalarRotativa) +
                                    Convert.ToDouble(EntradasPag4) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(4) / Convert.ToDouble(12))) + DesintercalarRotativa);
                    break;
                case 8:
                    CostoVariableInterior = Convert.ToDouble(EntradasPag8) * TarifaCostoVariableImpresionInterior +
                                    Convert.ToDouble(EntradasPag4) * ((TarifaCostoVariableImpresionInterior * (Convert.ToDouble(4) / Convert.ToDouble(8))) + DesintercalarRotativa);
                    break;
                default:
                    break;
            }
            CostoVariableInterior = Math.Round(CostoVariableInterior, 2) * Convert.ToDouble(Tiraje);
            #endregion

            #endregion
            #region impresion Tapas
            int CostoFijoTapa = 0;
            if (Pagtapa > 0)
            {
                CostoFijoTapa = Convert.ToInt32(controlTarifa.TarifaCostoImpresion("Impresion Tapa 4/4", "", MaquinaTapa, "Fijo"));
            }
            double TarifaCostoVariableImpresionTapa = 0;
            #region CostoVariablexEntradas
            double CostoVariableTapa = 0;
            if (Pagtapa > 0)
            {
                TarifaCostoVariableImpresionTapa = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Impresion Tapa 4/4", "", MaquinaTapa, "Variable"));
                switch (EntradasxFormato)
                {
                    case 8:
                        CostoVariableTapa = ((TarifaCostoVariableImpresionTapa * Convert.ToDouble(4)) / Convert.ToDouble(2));
                        break;
                    case 12:
                        CostoVariableTapa = ((TarifaCostoVariableImpresionTapa * Convert.ToDouble(4)) / Convert.ToDouble(3));
                        break;
                    default:
                        CostoVariableTapa = ((TarifaCostoVariableImpresionTapa * Convert.ToDouble(4)) / Convert.ToDouble(4));
                        break;
                }
            }
            CostoVariableTapa = CostoVariableTapa * Convert.ToDouble(Tiraje);
            double CostovariableTapaDoblez = 0;
            double CostoImpresionTapaPlisadoFijo = 0;
            double CostoImpresionTapaPlisadoVariable = 0;
            #region Impresion Tapa Doblez
            if (GramajeTapa < 130 && Pagtapa > 0)
            {
                CostovariableTapaDoblez = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Doblez Tapa", "", MaquinaTapa, "Variable")) * Convert.ToDouble(Tiraje);
            }
            else if (GramajeTapa >= 140 && Pagtapa > 0)
            {
                CostoImpresionTapaPlisadoFijo = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Plizado Tapa", "", MaquinaTapa, "Fijo"));
                CostoImpresionTapaPlisadoVariable = Convert.ToDouble(controlTarifa.TarifaCostoImpresion("Plizado Tapa", "", MaquinaTapa, "Variable")) * Convert.ToDouble(Tiraje);
            }

            #endregion
            #endregion
            #endregion
            #endregion

            #region Papel
            #region Papel Interior
            Tarifa_Papel tarifapapelInterior = controlTarifa.TarifaCostoPapel(GramajeInt, "interior", MaquinaInterior, Papelinterior, "Cencosud", Formato);
            int CostoPapelInteriorFijo = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(tarifapapelInterior.TarifaMermaFija) / Convert.ToInt32(1000)) * 1000) * (EntradasPag32 + EntradasPag24 + EntradasPag16 + EntradasPag12 + EntradasPag8 + EntradasPag4);
            double CostoPapelInteriorVariable = 0;
            switch (EntradasxFormato)
            {
                case 32:
                    CostoPapelInteriorVariable = Convert.ToDouble(EntradasPag32) * tarifapapelInterior.TarifaMermaVariable +
                                    Convert.ToDouble(EntradasPag16) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(16) / Convert.ToDouble(32)))) +
                                    Convert.ToDouble(EntradasPag8) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(8) / Convert.ToDouble(32)))) +
                                    Convert.ToDouble(EntradasPag4) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(4) / Convert.ToDouble(32))));
                    break;
                case 24:
                    CostoPapelInteriorVariable = Convert.ToDouble(EntradasPag24) * tarifapapelInterior.TarifaMermaVariable +
                                    Convert.ToDouble(EntradasPag12) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(12) / Convert.ToDouble(24)))) +
                                    Convert.ToDouble(EntradasPag8) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(8) / Convert.ToDouble(24)))) +
                                    Convert.ToDouble(EntradasPag4) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(4) / Convert.ToDouble(24))));
                    break;
                case 16:
                    CostoPapelInteriorVariable = Convert.ToDouble(EntradasPag16) * tarifapapelInterior.TarifaMermaVariable +
                                    Convert.ToDouble(EntradasPag8) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(8) / Convert.ToDouble(16)))) +
                                    Convert.ToDouble(EntradasPag4) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(4) / Convert.ToDouble(16))));
                    break;
                case 12:
                    CostoPapelInteriorVariable = Convert.ToDouble(EntradasPag12) * tarifapapelInterior.TarifaMermaVariable +
                                    Convert.ToDouble(EntradasPag8) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(8) / Convert.ToDouble(12)))) +
                                    Convert.ToDouble(EntradasPag4) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(4) / Convert.ToDouble(12))));
                    break;
                case 8:
                    CostoPapelInteriorVariable = Convert.ToDouble(EntradasPag8) * tarifapapelInterior.TarifaMermaVariable +
                                    Convert.ToDouble(EntradasPag4) * ((tarifapapelInterior.TarifaMermaVariable * (Convert.ToDouble(4) / Convert.ToDouble(8))));
                    break;
                default:
                    break;
            }
            CostoPapelInteriorVariable = Math.Round(Math.Ceiling(CostoPapelInteriorVariable * 10) / 10, 2) * Convert.ToDouble(Tiraje);
            #endregion
            #region Papel Tapa
            Tarifa_Papel tarifapapelTapa = controlTarifa.TarifaCostoPapel(GramajeTapa, "tapa", MaquinaTapa, PapelTapa, "Cencosud", Formato);
            int CostoPapelTapaFijo = 0;
            double CostoPapelTapaVariable = 0;
            if (Pagtapa > 0)
            {
                CostoPapelTapaFijo = Convert.ToInt32(Math.Ceiling(tarifapapelTapa.TarifaMermaFija / 100.0) * 100);
                CostoPapelTapaVariable = (Math.Ceiling(tarifapapelTapa.TarifaMermaVariable * 10) / 10) * Convert.ToDouble(Tiraje);
            }

            #endregion
            #endregion

            #region Costo de Encuadernacion

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

            double CostoTotalImpresionTapaFijo = (CostoFijoTapa + CostoImpresionTapaPlisadoFijo);
            double CostoTotalImpresionTapaVariable = (CostoVariableTapa + CostovariableTapaDoblez + CostoImpresionTapaPlisadoVariable);
            double CostoTotalImpresionTapaFinal = (CostoTotalImpresionTapaFijo + CostoTotalImpresionTapaVariable);

            double CostoTotalManufacturaFijo = (CostoFijoInterior + CostoTotalImpresionTapaFijo + valorEncuaFijo);
            double CostoTotalManufacturaVariable = (CostoVariableInterior + CostoTotalImpresionTapaVariable + valorEncuaVari);
            double CostoTotalManufacturaFinal = (CostoTotalManufacturaFijo + CostoTotalManufacturaVariable);

            double CostototalFijoPapel = (CostoPapelTapaFijo + CostoPapelInteriorFijo);
            double CostototalVariablePapel = (CostoPapelInteriorVariable + CostoPapelTapaVariable);
            double CostoTotalFinalPapel = (CostototalFijoPapel + CostototalVariablePapel);

            double CostoCatalogoFijo = (CostoTotalManufacturaFijo + CostototalFijoPapel);
            double CostoCatalogoVariable = (CostoTotalManufacturaVariable + CostototalVariablePapel);
            double CostoCatalogoFinal = (CostoCatalogoFijo + CostoCatalogoVariable);

            double porcentajeManufactura = Math.Round((CostoTotalManufacturaFinal / CostoCatalogoFinal) * 100, 0);
            double porcentajePapel = Math.Round((CostoTotalFinalPapel / CostoCatalogoFinal) * 100, 0);
            double CostoUnitario = Math.Ceiling(CostoCatalogoFinal / Convert.ToDouble(Tiraje));
            double CostoMillar = (CostoCatalogoVariable / Convert.ToDouble(Tiraje)) * 1000;
            #endregion

            return new[] { CostoFijoInterior.ToString(), CostoVariableInterior.ToString(), (CostoFijoInterior+ CostoVariableInterior).ToString()
               ,CostoPapelInteriorFijo.ToString(), CostoPapelInteriorVariable.ToString(), tarifapapelInterior.PrecioPapel.ToString(), valorEncuaFijo.ToString(), valorEncuaVari.ToString(), valorEncuadernacion.ToString()
                ,CostoFijoTapa.ToString(), CostoVariableTapa.ToString(), (CostoFijoTapa + CostoVariableTapa).ToString(), CostoPapelTapaFijo.ToString(), CostoPapelTapaVariable.ToString()
                ,tarifapapelTapa.PrecioPapel.ToString(), CostovariableTapaDoblez.ToString(), (CostoPapelInteriorFijo + CostoPapelInteriorVariable).ToString()
                ,(CostoPapelTapaFijo + CostoPapelTapaVariable).ToString(), CostototalFijoPapel.ToString(), CostototalVariablePapel.ToString(), CostoTotalFinalPapel.ToString()
                ,CostoImpresionTapaPlisadoFijo.ToString(), CostoImpresionTapaPlisadoVariable.ToString(), (CostoImpresionTapaPlisadoFijo + CostoImpresionTapaPlisadoVariable).ToString()
                ,CostoTotalImpresionTapaFijo.ToString(), CostoTotalImpresionTapaVariable.ToString(), CostoTotalImpresionTapaFinal.ToString(), porcentajePapel.ToString()+"%"
                ,CostoTotalManufacturaFijo.ToString(), CostoTotalManufacturaVariable.ToString(), CostoTotalManufacturaFinal.ToString(), porcentajeManufactura.ToString()+"%"
                ,CostoCatalogoFijo.ToString(), CostoCatalogoVariable.ToString(), CostoCatalogoFinal.ToString(), CostoUnitario.ToString(), CostoMillar.ToString()
            };
        }

        [WebMethod]
        public static string GuardarPresupuesto(string NombrePres, string Usuario, string Empresa,int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion, string Tiraje)
        {
            Controller_Cotizador controlpres = new Controller_Cotizador();
            string[] PrecioPrensa = CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje);
            //if (controlpres.GuardarPresupuesto(NombrePres, PagInterior, Pagtapa, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, PrecioPrensa[37], PrecioPrensa[38], Usuario, Empresa))
            //{
                return "OK";
            //}
            //else
            //{
            //    return "Error al crear el registro, intentelo mas tarde";
            //}
        }

    }
}