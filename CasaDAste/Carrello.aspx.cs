using System;
using System.Collections.Generic;

namespace CasaDAste
{
    public partial class Carrello1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LegareArticoliCarrello();
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
            else
            {
                Response.Write("Carrello vuoto");
            }

        }
    }
}