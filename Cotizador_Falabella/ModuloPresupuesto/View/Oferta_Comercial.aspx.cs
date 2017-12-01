using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net.Mime;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Cotizador_Falabella.ModuloPresupuesto.Model;
using Cotizador_Falabella.ModuloPresupuesto.Controller;

namespace Cotizador_Falabella.ModuloPresupuesto.View
{
    public partial class Oferta_Comercial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"].ToString();
                Controller_Cotizador controlpre = new Controller_Cotizador();
                Cotizador p = controlpre.BuscarPresupuestoxID(Convert.ToInt32(id));

                string Tapas = "";
                string PapelTapas = "";
                string Barniz = "";

                switch (p.PaginasTap)
                {
                    case 0:
                        Tapas = "No Considera Tapa.";
                        PapelTapas = "No Considera Tapa.";
                        break;
                    default:
                        Tapas = p.PaginasTap + " páginas impresas a 4/4 colores proceso</td>";
                        PapelTapas = p.PapelTap.Replace("Couché", "Couché Opaco") + " de " + p.GramajeTapas + " grs. Certificado PEFC</td>";
                        if (p.BarnizAcuosoTap!="No")
                        {
                            Barniz = "<tr><td class='style1'><div style='font-weight: bold;' align='right'>Terminación Tapa:</div></td><td colspan='2' class='style1'>Barniz Acuoso Parejo en el " + p.BarnizAcuosoTap + ".</td></tr>";
                        }
                        break;
                }


                switch (p.Empresa.ToUpper())
                {
                    case "FALABELLA": p.Empresa = "Falabella Retail S.A."; break;
                    case "SODIMAC": p.Empresa = "Sodimac Retail S.A."; break;
                    default: p.Empresa = "Tottus Retail S.A."; break;
                }


                //Falta Los barniz Acuosos en Todo

                string formatoExt = "";
                string[] str = p.Formato.Replace(",", ".").Split('x');
                Double fextendAncho = Convert.ToDouble(str[0].Trim()) * 10.0;
                Double fextendLargo = Convert.ToDouble(str[1].Trim()) * 10.0;
                string Formato = fextendAncho.ToString() + " x " + fextendLargo;
                formatoExt = (Convert.ToDouble(fextendAncho) * 2).ToString() + " x " + fextendLargo;

                string encuadernacion = p.Encuadernacion;
                if (encuadernacion == "Hotmelt")
                {
                    encuadernacion = "Entapado hotmelt";
                }
                else if (encuadernacion == "Entapado Pur")
                {
                    encuadernacion = "Entapado PUR";
                }
                else if (encuadernacion == "Corchete")
                {
                    encuadernacion = "Dos corchetes al Lomo";
                }
                else
                {
                    encuadernacion = "No Considera Encuadernación";
                }

                PPTO_Tirajes tiraje1 = p.Tiraje1;
                string Tiraje1Totales = "";
                string TextoCantidad = "";
                string TextoTotalNeto = "";
                if (tiraje1 != null)
                {
                                       Tiraje1Totales = "<tr>" +
                                    "<td style='100px'></td>" +
                                    "<td>" +
                                        "<div style='font-weight: bold;' align='right'>Cantidad de Ejemplares:</div>" +
                                    "</td>" +
                                    "<td>" +
                                        Convert.ToInt32(tiraje1.Cantidad).ToString("N0").Replace(",", ".") +
                                    "</td>" +
                                    "<td style='width: 1%;'>" +
                                        "<div align='right'>" +
                                            "<a style='font-weight: bold;'>" + "Total Neto:" + "</a> $ " + Convert.ToInt32(tiraje1.CostoTotal).ToString("N0").Replace(",", ".") +
                                        "</div>" +
                                    "</td>" +
                                "</tr>";
                }

                string contenido = "<table style='width: 100%;height:100%;' border='1'>" +
                                        "<tr><td>" +
                                                "<table style='width: 100%;' border='0'>" +
                                                    "<tr><td>" +
                                                            "<img " +//height='50px' 
                                                            " alt='Logo AImpresores' src='http://cencosud.aimpresores.cl/Estructura/Image/índice.jpg'" +
                                                                "  />" +
                                                            "<div style='font-size: 8px;'>" +
                                                                "Av. Gladys Marín Millie 6920, Estación Central, Santiago de Chile" +
                                                                "<br />" +
                                                                "Teléfono: (56 2) 2440 5700 / Fax: " + p.PersonalComercial.Fax +
                                                                "<br />info@aimpresores.cl" +

                                                            "</div>" +
                                                        "</td>" +
                                                        "<td align='right'>" +
                                                            "<br /><br /><br /><br />" +
                                                            "<div style='font-weight: bold;' align='center'>PRESUPUESTO " + p.ID_Presupuesto.ToString() + "</div>" +
                                                        "</td></tr></table></td></tr></table>" +
                                                "<table border='1'><tr>" +
                                                        "<td align='center' style='vertical-align:center'>" +
                                                            "<div style='font-weight: bold; font-size: 8px; font-style: italic;'>" +
                                                                "De acuerdo a lo solicitado por ustedes, remitimos el siguiente presupuesto:</div>" +
                                                        "</td></tr></table>" +
        "<div style='font-size: 9px;'>" +
            "<table style='width: 100%;'>" +
                "<tr><td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Empresa:</div></td><td>" +
                        p.Empresa.ToUpper() +
                    "</td><td style='width: 1%;'>" +
                        "<a style='font-weight: bold;'>Fecha: </a>" + p.FechaCreacion.ToString("dd/MM/yyy") +
                    "</td></tr><tr><td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Atención a:</div></td><td>" + p.Usuario_Creador + "</td><td style='width: 1%;'></td></tr>" +
                "<tr>" +
                    "<td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Ejecutivo Directo:</div>" +
                    "</td>" +
                    "<td>" +
                        p.PersonalComercial.NombreCompleto +
                    "</td>" +
                    "<td style='width: 1%;'>" +
                    "</td>" +
                "</tr>" +
                "<tr><td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "E-Mail:</div>" +
                    "</td>" +
                    "<td>" +
                        p.PersonalComercial.Correo +
                    "</td>" +
                    "<td style='width: 1%;'>" +
                    "</td>" +
                "</tr>" +
                "<tr><td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Teléfono:</div>" +
                    "</td>" +
                    "<td>" +
                        p.PersonalComercial.Telefono + " / " + p.PersonalComercial.Celular +
                    "</td>" +
                    "<td style='width: 1%;'>" +
                    "</td>" +
                "</tr>" +

                "<tr>" +
                    "<td colspan='3' align='center'>" +
                        "___________________________________________________________________________________________" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Producto:</div>" +
                    "</td>" +
                    "<td>" +
                        "<a style='font-weight: bold;'>" + p.NombrePresupuesto.ToUpper() + "</a>" +
                    "</td>" +
                    "<td style='width: 1%;'>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Formato Cerrado:</div>" +
                    "</td>" +
                    "<td>" +
                        Formato + " (mm.)" +
                    "</td>" +
                    "<td style='width: 1%;'>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Formato Extendido:</div>" +
                    "</td>" +
                    "<td>" +
                        formatoExt + " (mm.)" +
                    "</td>" +
                    "<td style='width: 1%;'>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "</div>" +
                    "</td>" +
                    "<td>" +
                    "</td>" +
                    "<td style='width: 1%;'>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Extensión:</div>" +
                    "</td>" +
                    "<td colspan='2'>" +
                        p.PaginasInt + " páginas Interiores impresas a 4/4 colores " +
                        "proceso " +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Papel:</div>" +
                    "</td>" +
                    "<td colspan='2'>" +
                        p.PapelInterior.Replace("Couché", "Couché Opaco") + " de " + p.GramajeInterior + " grs. Certificado PEFC" +
                    "</td>" +

                "</tr>" +
                "<tr>" +
                    "<td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Tapa:</div>" +
                    "</td>" +
                    "<td colspan='2'>" +
                    Tapas +
                "</tr>" +
                "<tr>" +
                    "<td class='style1'>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Papel:</div>" +
                    "</td>" +
                    "<td colspan='2' class='style1'>" +
                    PapelTapas +
                "</tr>" + Barniz +
                "<tr>" +
                    "<td>" +
                        "<div style='font-weight: bold;' align='right'>" +
                            "Encuadernación:</div>" +
                    "</td>" +
                    "<td>" +
                        encuadernacion +
                    "</td>" +
                    "<td>" +
                    "</td>" +
                "</tr>" +
                "<tr><td colspan='3'>" +
                    "</td>" +
                "</tr>" +
                "<tr><td colspan='3'>" +
                    "</td>" +
                "</tr>" +
                "<tr>" +

                    "<td colspan='3'>" +
                        "<table>" +
                            Tiraje1Totales + //Tiraje2Totales + Tiraje3Totales +

                        "</table>" +
                    "</td>" +
                "</tr>" +
                
            "</table>" +

        "</div>" + "<br />" +
    "<br />" +
    "<br />" + "<br />" +
    "<br />" +
    "<br />" +
    "<table style='width: 100%;' align='center'>" +
        "<tr>" +
            "<td align='center'>" +
                "__________________________" +
                "<br />" +
                "<a style='font-weight: bold; font-size: 9px;'>ACEPTADO CLIENTE</a>" +
            "</td>" +
            "<td align='center'>" +
                "__________________________" +
                "<br />" +
                "<a style='font-weight: bold; font-size: 9px;'>A IMPRESORES S.A.</a>" +
            "</td>" +
        "</tr>" +
    "</table>" +
    "<br />" +
    "<a style='font-weight: bold; font-size: 9px;'>Precios no Incluyen IVA</a>" +
    "" +
    "<div style='font-size: 8px;' border='1' align='center'>" +
        "<table style='width: 100%; font-size: 8px;' align='center' border='1'>" +
            "<tr>" +
                "<td align='center'>" +
                    "La Validez de este presupuesto es de 30 días, vencido este plazo el presupuesto " +
                    "queda nulo " +
                    "<br />" +
                    "Se acepta una variación de la cantidad solicitada en un rango +/- 5% la cual será " +
                    "facturada al valor del ejemplar adicional" +
                    "<br />" +
                    "Este Presupuesto se mantiene proforma hasta el cierre completo del material proporcionado " +
                    "por el cliente" +
                    "<br />" +
                    "Despacho del volumen total solo a un lugar físico dentro de la Región Metropolitana" +
                    "<br />" +
                    "Si este presupuesto es acertado,se deberá confirmar disponibilidad de máquina y " +
                    "papeles con su ejecutivo directo." +
                    "<br />" +
                    "Precios vigentes para catalogos impresos entre el " + p.ValorDolar.FechaInicio.ToString("dd/MM/yyyy") + " y el " +
                    p.ValorDolar.FechaTermino.ToString("dd/MM/yyyy") + ". Tipo de cambio vigente del periodo: $" + p.ValorDolar.ValorTrimestre.ToString() +
                    " correspondiente al dolar observado promedio informado por el Banco Central de Chile " +
                    "para el mes anterior al inicio del trimestre." +
                "</td>" +
        "</table>";
                //"  </div>" +
                // "<a style='font-weight: bold;font-size:9px;'>                                 En caso de recepción Incompleta o ilegible, favor comunicarse al 56 (2) 2440 5740</a>";
                Document document = new Document(PageSize.A4, 15f, 15f, 20f, 10f);
                PdfWriter.GetInstance(document, new FileStream(Request.PhysicalApplicationPath + "PDF\\" + id + ".- " + p.NombrePresupuesto + ".pdf", FileMode.Create));
                document.Open();
                iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                hw.Parse(new StringReader(contenido));
                document.Close();

                //if (EnviarCorreo(id + ".- " + p.NombrePresupuesto, p.CorreoComercial))
                //{
                Response.Redirect("../../PDF/" + id + ".- " + p.NombrePresupuesto + ".pdf");
                //}
                //Response.Clear();
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("Content-Disposition", "attachment; filename=MySamplePDF");
                //Response.WriteFile(Request.PhysicalApplicationPath + "\\MySamplePDF.pdf");
                //Response.End();
            }
            catch (Exception a)
            {
                string popupScript = "<script language='JavaScript'>alert('A ocurrido el siguiente error:" + a.Message + "');</script>";
                Page.RegisterStartupScript("PopupScript", popupScript);
            }
        }

        //Falta la Estructura
        //Metodo enviar Correo
        public bool EnviarCorreo(string id, string EncargadoEmail)
        {
            /* Carga de PAra la base de Datos*/
            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(EncargadoEmail);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Presupuesto Creado N° " + id.ToString();
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            mmsg.Bcc.Add("juan.beheran@aimpresores.cl"); //Opcional
            DateTime hoy = DateTime.Now;
            string fecha = hoy.ToString("dd/MM/yyyy HH:mm");
            string[] str = fecha.Split('/');
            string dia = str[0];
            string mes = str[1];
            string año = str[2];
            //año = año.Substring(0, 4);
            //string hora = hoy.ToLongTimeString();

            //Cuerpo del Mensaje
            mmsg.Body = "<table style='width:100%;'>" +
            "<tr>" +
                "<td>" +
                    "<img src='http://cotizador2.aimpresores.cl/Images/Logo color lateral.jpg' width='267px'  height='67px' />" +
                    "&nbsp;</td>" +
            "</tr>" +
            "</table>" +
                //termino cargar logo
                "<div>Estimado(a) </div><div>Se a generado un presupuesto.</div><div>Atentamente, </div><div>Equipo de desarrollo A Impresores S.A </div>";
            mmsg.Body = mmsg.Body +
         "</div>" +
        "<br />";// +"</div>";//<td style='width: 168px;border-bottom:1px solid black;'> &nbsp;</td>

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            // Crear el archivo adjunto para el mensaje 
            Attachment data = new Attachment(Request.PhysicalApplicationPath + "PDF\\" + id.ToString() + ".pdf", MediaTypeNames.Application.Octet);
            data.Name = "Presupuesto.pdf";
            // Añadir el adjunto al mensaje 
            mmsg.Attachments.Add(data);
            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress("presupuestador.falabella@aimpresores.cl");//"fecha.produccion@aimpresores.cl");


            /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential("presupuestador.falabella@aimpresores.cl", "PpTT2288-");

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            /*
            cliente.Port = 587;
            cliente.EnableSsl = true;
            */

            cliente.Host = "mail.aimpresores.cl";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
                return true;
                //Label1.Text = "enviado correctamente";
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return false;
                //Aquí gestionamos los errores al intentar enviar el correo
                //Label1.Text = "error al enviar el correo";
            }
        }
    }
}