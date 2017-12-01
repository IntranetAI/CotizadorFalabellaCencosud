using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cotizador_Falabella.ModuloPresupuesto.Controller;
using Cotizador_Falabella.ModuloPresupuesto.Model;

namespace Cotizador_Falabella.ModuloPresupuesto.View
{
    public partial class Detalle_PPTO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"].ToString();
                Controller_Cotizador controlpre = new Controller_Cotizador();
                Cotizador p = controlpre.BuscarPresupuestoxID(Convert.ToInt32(id));
                lblCatalogo.Text = p.NombrePresupuesto.ToString();
                txtTablaDetalle.Text = PrePrensa(p.PaginasInt, p.PaginasTap, p.EntradasxFormatos, p.Formato, p.GramajeInterior, p.GramajeTapas, p.PapelInterior, p.PapelTap, p.Encuadernacion, p.Tiraje1.Cantidad.ToString(),
                                        Convert.ToInt32(id), p.QuintoColor, p.BarnizUV, p.Laminado, p.BarnizAcuosoTap, p.BarnizAcuosoInt, "0", p.Segmentos, p.Empresa);
            }
            catch
            {
            }
        }

        public string PrePrensa(int PagInterior, int Pagtapa, int EntradasxFormato, string Formato, string GramajeInt1, string GramajeTapa1, string Papelinterior, string PapelTapa, string Encuadernacion,
            string Tiraje1, int IDPPTO, string QuintoColor, string UV, string Laminado, string BarnizAcuosoTapa, string BarnizAcuosoInt, string CantidaddeVersionesSodimac, string Segmento, string Empresa)
        {
            string Tabla1 = "";
            if (Tiraje1 != "")
            {
                Presupuestador presupuestadorView = new Presupuestador();
                string[] Detalle1 = CreacionTablaDetalle(Presupuestador.CalcularPreciosPrensa(PagInterior, Pagtapa, EntradasxFormato, Formato, GramajeInt1, GramajeTapa1, Papelinterior, PapelTapa, Encuadernacion, Tiraje1, QuintoColor, UV,
                    Laminado, BarnizAcuosoTapa, BarnizAcuosoInt, CantidaddeVersionesSodimac, Segmento, Empresa), Tiraje1);
                Tabla1 = Detalle1[0];
            }
           

            return Tabla1;
        }


        public string[] CreacionTablaDetalle(string[] detalle, string Tiraje)
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
                                 (Convert.ToDouble(detalle[52]) / Convert.ToDouble(Tiraje.Replace(".", "")))) * 100) / 100);
            PrecioNetoTotal = PrecioNetoFijo + (PrecioNetoVariable * Convert.ToDouble(Tiraje.Replace(".", "")));
            double PrecioUnitario = (Math.Ceiling((PrecioNetoTotal / Convert.ToDouble(Tiraje)) * 100) / 100);
            int EntradasUV = 0;
            if ((detalle[35].ToString() != "Seleccione Barniz UV...") &&(detalle[35].ToString() != "Seleccionar"))
            {
                EntradasUV = 1;
            }
            int EntradasLaminado = 0;
            if ((detalle[40].ToString() != "Seleccione Laminado...") &&(detalle[40].ToString() != "Seleccionar"))
            {
                EntradasLaminado = 1;
            }
            string TablaDetalle =
                "<table class='table' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; "+
                        " width: 650pt;'> "+
                        "<colgroup>"+
                            "<col style='mso-width-source: userset; mso-width-alt: 1865; width: 38pt' width='51' />"+
                            "<col span='6' style='mso-width-source: userset; mso-width-alt: 4022; width: 83pt' "+
                             "   width='110' />"+
                        "</colgroup>" +
                    "<tr height='19'>"+
                            "<th class='xl88' width='110'>" +
                                "Costos"+
                            "</th>"+
                            "<th class='xl88' width='50'>"+
                                "# Entradas"+
                            "</th>"+
                            "<th class='xl88' width='110'>"+
                                "CF"+
                            "</th>"+
                            "<th class='xl88' width='110'>"+
                                "CV"+
                            "</th>"+
                            "<th class='xl88' width='110'>"+
                                "Total CF"+
                            "</th>"+
                            "<th class='xl88' width='110'>"+
                                "Total CV"+
                            "</th>"+
                            "<th class='xl89' width='110'>"+
                                "Total Final"+
                            "</th>"+
                        "</tr>"+
            #region Impresion interior
                    "<tr height='17'>"+
                            "<th class='xl156' height='17' colspan='7'>Impresión Interior 4x4 Colores</th>"+
                    "</tr>"+
                    "<tr height='17'>" +
                        "<td>16 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[1] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[0] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[5]), 2).ToString() + "</td>" +

                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[1]) * Convert.ToInt32(detalle[0])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round((Convert.ToDouble(detalle[1]) * Convert.ToDouble(detalle[5])), 2).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" + 
                                ((Convert.ToInt32(detalle[1]) * Convert.ToInt32(detalle[0]))  +
                                (Math.Round((Convert.ToDouble(detalle[1]) * Convert.ToDouble(detalle[5])), 2) * Convert.ToDouble(Tiraje))
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr height='17'>" +
                        "<td>12 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[2] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[0] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[6]), 2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[2]) * Convert.ToInt32(detalle[0])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round((Convert.ToDouble(detalle[2]) * Convert.ToDouble(detalle[6])), 2).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[2]) * Convert.ToInt32(detalle[0])) +
                                (Math.Round((Convert.ToDouble(detalle[2]) * Convert.ToDouble(detalle[6])), 2) * Convert.ToDouble(Tiraje))
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr height='17'>" +
                        "<td>8 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[3] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[0] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[7]), 2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[3]) * Convert.ToInt32(detalle[0])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round((Convert.ToDouble(detalle[3]) * Convert.ToDouble(detalle[7])), 2).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[3]) * Convert.ToInt32(detalle[0])) +
                                (Math.Round((Convert.ToDouble(detalle[3]) * Convert.ToDouble(detalle[7])), 2) * Convert.ToDouble(Tiraje))
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr height='17'>" +
                        "<td>4 Pág </td>" +
                        "<td style='text-align: right;'>" + detalle[4] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[0] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[8]), 2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[4]) * Convert.ToInt32(detalle[0])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round((Convert.ToDouble(detalle[4]) * Convert.ToDouble(detalle[8])), 2).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[4]) * Convert.ToInt32(detalle[0])) +
                                (Math.Round((Convert.ToDouble(detalle[4]) * Convert.ToDouble(detalle[8])), 2) * Convert.ToDouble(Tiraje))
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
            #endregion
            #region Barniz Acuoso Int
 "<tr>" +
                        "<th colspan='7' scope='row'>Barniz Acuoso Parejo</th>" +
                    "</tr>" +
                    "<tr height='17'>" +
                        "<td>16 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[1] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[9] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[10] + "</td>" +

                        "<td align='right' class='xl95'>" + detalle[9] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[10] + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[1]) * Convert.ToInt32(detalle[9])) +
                                (Math.Round((Convert.ToDouble(detalle[1]) * Convert.ToDouble(detalle[10])), 2) * Convert.ToDouble(Tiraje))
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr height='17'>" +
                        "<td>12 Pág </td>" +
                        "<td align='right' class='xl95'>" + detalle[2] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[11] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[12] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[11] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[12] + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[2]) * Convert.ToInt32(detalle[11])) +
                                (Math.Round((Convert.ToDouble(detalle[2]) * Convert.ToDouble(detalle[12])), 2) * Convert.ToDouble(Tiraje))
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr height='17'>" +
                        "<td>8 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[3] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[13] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[14] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[13] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[14] + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[3]) * Convert.ToInt32(detalle[13])) +
                                (Math.Round((Convert.ToDouble(detalle[3]) * Convert.ToDouble(detalle[14])), 2) * Convert.ToDouble(Tiraje))
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr height='17'>" +
                        "<td>4 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[4] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + detalle[15] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + detalle[15] + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Math.Round((Convert.ToDouble(detalle[4]) * Convert.ToDouble(detalle[15])), 2) * Convert.ToDouble(Tiraje))
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
            #endregion
            #region Tapas
 "<tr>" +
                        "<th colspan='7' scope='row'>Tapas</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Impresión Tapa</td>" +
                        "<td align='right' class='xl95'>" + detalle[16] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[17] + "</td>" +
                        "<td align='right' class='xl95'>" + Math.Round(Convert.ToDouble(detalle[18]), 2).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" + (Convert.ToInt32(detalle[16]) * Convert.ToInt32(detalle[17])).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" + Math.Round(Convert.ToDouble(detalle[18]), 2).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[16]) * Convert.ToInt32(detalle[17])) +
                                (Math.Round(Convert.ToDouble(detalle[18]), 2) * Convert.ToDouble(Tiraje)
                                )).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td class='xl95'>" + detalle[19].Replace("Seleccionar", "Sin Quinto Color") + "</td>" +//nombre del quinto color
                        "<td align='right' class='xl95'>" + detalle[20] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[21] + "</td>" +
                        "<td align='right' class='xl95'>" + Math.Round(Convert.ToDouble(detalle[22]), 2).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" + (Convert.ToInt32(detalle[20]) * Convert.ToInt32(detalle[21])).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" + Math.Round(Convert.ToDouble(detalle[22]), 2).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[20]) * Convert.ToInt32(detalle[21])) +
                                (Math.Round(Convert.ToDouble(detalle[22]), 2) * Convert.ToDouble(Tiraje)
                                )).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Plisado Tapa</td>" +
                        "<td style='text-align: right;'>" + detalle[23] + "</td>" +
                        "<td style='text-align: right;'>" + detalle[24] + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[25]), 2).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + (Convert.ToInt32(detalle[23]) * Convert.ToInt32(detalle[24])).ToString() + "</td>" +
                        "<td style='text-align: right;'>" + Math.Round(Convert.ToDouble(detalle[25]), 2).ToString() + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[23]) * Convert.ToInt32(detalle[24])) +
                                (Math.Round(Convert.ToDouble(detalle[25]), 2) * Convert.ToDouble(Tiraje)
                                )).ToString()
                                + "</td>" +
                    "</tr>" +
            #endregion
            #region Encuadernación
 "<tr>" +
                        "<th colspan='7' scope='row'>Encuadernación</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>" + detalle[26] + "</td>" +//nombre de la encuadernacion
                        "<td align='right' class='xl95'>" + detalle[27] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[28] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[29] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[28] + "</td>" +
                        "<td align='right' class='xl95'>" + detalle[29] + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[28]) * Convert.ToInt32(detalle[27])) +
                                (Math.Round(Convert.ToDouble(detalle[29]), 2) * Convert.ToDouble(Tiraje)
                                )).ToString()
                                + "</td>" +
                    "</tr>" +
            #endregion
            #region preprensa
 "<tr>" +
                        "<th colspan='7' scope='row'>Pre Prensa</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>16 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[1] + "</td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[30]).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[30]) * Convert.ToDouble(detalle[1])).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[30]) * Convert.ToDouble(detalle[1])).ToString("N2") + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>12 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[2] + "</td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[31]).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[31]) * Convert.ToDouble(detalle[2])).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[31]) * Convert.ToDouble(detalle[2])).ToString("N2") + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>8 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[3] + "</td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[32]).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[32]) * Convert.ToDouble(detalle[3])).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[32]) * Convert.ToDouble(detalle[3])).ToString("N2") + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>4 Pág </td>" +
                        "<td style='text-align:right;'>" + detalle[4] + "</td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[33]).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[33]) * Convert.ToDouble(detalle[4])).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[33]) * Convert.ToDouble(detalle[4])).ToString("N2") + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Tapa </td>" +
                        "<td style='text-align:right;'>1</td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[34]).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[34]) * 1).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[34]) * 1).ToString("N2") + "</td>" +
                    "</tr>" +
            #endregion
            #region Terminacion
 "<tr>" +
                        "<th colspan='7' scope='row'>Terminacion Tapa</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>" + detalle[35].Replace("Seleccione Barniz UV...", "Sin Barniz UV").Replace("Seleccionar", "Sin Barniz UV") + "</td>" +
                        "<td style='text-align:right;'>" + EntradasUV.ToString() + "</td>" +
                        "<td style='text-align:right;'>" + detalle[36] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[37] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[36] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[37] + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[36]) * Convert.ToInt32(EntradasUV)) +
                                (Math.Round(Convert.ToDouble(detalle[37]), 2) * Convert.ToDouble(Tiraje)
                                )).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Barniz Acuoso</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[42] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[43] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[42] + "</td>" +
                        "<td style='text-align:right;'>" + detalle[43] + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[42])) +
                                (Math.Round(Convert.ToDouble(detalle[43]), 2) * Convert.ToDouble(Tiraje)
                                )).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>" + detalle[40].Replace("Seleccione Laminado...", "Sin Laminado").Replace("Seleccionar", "Sin Laminado") + "</td>" +
                        "<td style='text-align:right;'>" + EntradasLaminado + "</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[41] + "</td>" +
                        "<td style='text-align:right;'></td>" +
                        "<td style='text-align:right;'>" + detalle[41] + "</td>" +
                        "<td align='right' class='xl95'>" +
                                
                                (Math.Round(Convert.ToDouble(detalle[41]), 2) * Convert.ToDouble(Tiraje)
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
            #endregion
            #region Despacho
 "<tr>" +
                        "<th colspan='7' scope='row'>Embalaje y Despacho</th>" +
                    "</tr>" +
                     "<tr>" +
                        "<td>Costo Fijo Despacho</td>" +
                        "<td align='right' class='xl95'>1</td>" +
                        "<td style='text-align:right;'>" + detalle[44] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + detalle[44] + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + detalle[44] + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>CMC</td>" +
                        "<td align='right' class='xl95'>1</td>" +
                        "<td style='text-align:right;'>" + detalle[45] + "</td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[46]).ToString("N2") + "</td>" +
                        "<td style='text-align:right;'>" + detalle[45] + "</td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[46]).ToString("N2") + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[45])) +
                                (Math.Round(Convert.ToDouble(detalle[46]), 2) * Convert.ToDouble(Tiraje)
                                )).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Pallets</td>" +
                        "<td align='right' class='xl95'>1</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[47]).ToString("N2") + "</td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + Convert.ToDouble(detalle[47]).ToString("N2") + "</td>" +
                        "<td align='right' class='xl95'>" +
                                (Math.Round(Convert.ToDouble(detalle[47]), 2) * Convert.ToDouble(Tiraje)
                                ).ToString()
                                + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Flete</td>" +
                        "<td align='right' class='xl95'>1</td>" +
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
                        "<td align='right' class='xl95'>1</td>" +
                        "<td style='text-align:right;'>" + detalle[49] + "</td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[51]) / Convert.ToDouble(Tiraje.Replace(".", ""))).ToString("N2") + "</td>" +
                        "<td style='text-align:right;'>" + detalle[49] + "</td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[51]) / Convert.ToDouble(Tiraje.Replace(".", ""))).ToString("N2") + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[49])) +
                                ((Convert.ToDouble(detalle[51]) / Convert.ToDouble(Tiraje.Replace(".", ""))) * Convert.ToDouble(Tiraje)
                                )).ToString("N2")
                                + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Tapa</td>" +
                        "<td align='right' class='xl95'>1</td>" +
                        "<td style='text-align:right;'>" + detalle[50] + "</td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[52]) / Convert.ToDouble(Tiraje.Replace(".", ""))).ToString("N2") + "</td>" +
                        "<td style='text-align:right;'>" + detalle[50] + "</td>" +
                        "<td style='text-align:right;'>" + (Convert.ToDouble(detalle[52]) / Convert.ToDouble(Tiraje.Replace(".", ""))).ToString("N2") + "</td>" +
                        "<td align='right' class='xl95'>" +
                                ((Convert.ToInt32(detalle[50])) +
                                ((Convert.ToDouble(detalle[52]) / Convert.ToDouble(Tiraje.Replace(".", ""))) * Convert.ToDouble(Tiraje)
                                )).ToString("N2")
                                + "</td>" +
                    "</tr>" +

            #endregion
            #region Totales
 "<tr>" +
                        "<th  scope='row'>Precio Neto</th>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td></td>" +
                        "<td style='text-align:right;'>" + PrecioNetoFijo.ToString("N0") + "</td>" +
                        "<td style='text-align:right;'>" + PrecioNetoVariable.ToString("N0") + "</td>" +
                        "<td style='text-align:right;'>" + PrecioNetoTotal.ToString("N0") + "</td>" +
                    "</tr>";
            #endregion
            return new[] { TablaDetalle, PrecioNetoTotal.ToString(), PrecioUnitario.ToString(), detalle[53], detalle[54] };
        }
        
    }
}