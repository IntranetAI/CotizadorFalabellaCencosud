using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cotizador_Cencosud.ModuloPresupuesto.Controller;
using Cotizador_Cencosud.ModuloPresupuesto.Model;
using System.Web.Services;

namespace Cotizador_Cencosud.ModuloPresupuesto.View
{
    public partial class PPTO_Aprobar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                    Controller_Cotizador controlpres = new Controller_Cotizador();
                    Cotizador cot = controlpres.BuscarPresupuestoxID(id);
                    if (cot.NombrePresupuesto != null)
                    {
                        lblCatalogo.Text = cot.NombrePresupuesto;
                        if (cot.Tiraje1 != null)
                        {
                            lblIDTiraje1.Text = cot.Tiraje1.IDTiraje.ToString();
                            lblCantidad1.Text = cot.Tiraje1.Cantidad.ToString("N0").Replace(",", ".");
                            lblPrecio1.Text = cot.Tiraje1.CostoTotal.ToString("N0").Replace(",", ".");
                        }
                        if (cot.Tiraje2 != null)
                        {
                            lblIDTiraje2.Text = cot.Tiraje2.IDTiraje.ToString();
                            lblCantidad2.Text = cot.Tiraje2.Cantidad.ToString("N0").Replace(",", ".");
                            lblPrecio2.Text = cot.Tiraje2.CostoTotal.ToString("N0").Replace(",", ".");
                        }
                        else
                        {
                            divT2.Visible = false;
                        }
                        if (cot.Tiraje3 != null)
                        {
                            lblIDTiraje3.Text = cot.Tiraje3.IDTiraje.ToString();
                            lblCantidad3.Text = cot.Tiraje3.Cantidad.ToString("N0").Replace(",", ".");
                            lblPrecio3.Text = cot.Tiraje3.CostoTotal.ToString("N0").Replace(",", ".");
                        }
                        else
                        {
                            divT3.Visible = false;
                        }
                    }
                }
                catch
                {

                }
            }
        }

        [WebMethod]
        public static string AprobarPPTO(int id)
        {
            Controller_Tirajes controlT = new Controller_Tirajes();
            if (controlT.AprobarPPTO_ID(id))
            {
                return "OK";
            }
            else
            {
                return "Error";
            }
        }
    }
}