using System;
using System.Collections.Generic;
using System.Linq;

namespace CasaDAste
{
    public partial class Carrello1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LegareArticoliCarrello();
                CalcolaTotaleSpesa();
            }
        }
        private void LegareArticoliCarrello()
        {
            if (Session["Carrello"] != null)
            {
                var carrello = (List<Carrello>)Session["Carrello"];
                rptCarrello.DataSource = carrello;
                rptCarrello.DataBind();
            }

        }

        protected void btnElimina_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandName == "Elimina")
            {
                string nomeProdottoDaEliminare = e.CommandArgument.ToString();

                if (Session["Carrello"] != null)
                {
                    var carrello = (List<Carrello>)Session["Carrello"];
                    carrello.RemoveAll(p => p.Nome == nomeProdottoDaEliminare);

                    LegareArticoliCarrello();
                    CalcolaTotaleSpesa();
                }
            }
        }

        private void CalcolaTotaleSpesa()
        {
            if (Session["Carrello"] != null)
            {
                var carrello = (List<Carrello>)Session["Carrello"];
                double totale = carrello.Sum(item => item.Prezzo);

                lblTotale.Text = $"Totale: {totale} Berries";
            }
            else
            {
                lblTotale.Text = "Totale: 0,00 Berries";
            }
        }
    }
}