using System;
using System.Web.UI;

namespace CasaDAste
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LinkHome.Visible = false;
            LinkBackOffice.Visible = false;
            LinkCarrello.Visible = false;
            LinkLogout.Visible = false;
            // Se l'utente non è loggato e la pagina corrente non è Default.aspx, reindirizza l'utente a Default.aspx
            if (Session["User"] == null)
            {
                // Se l'utente non è loggato e la pagina corrente non è Default.aspx, reindirizza l'utente a Default.aspx
                if (Page.AppRelativeVirtualPath != "~/Default.aspx")
                {
                    Response.Redirect("Default.aspx");
                }
            }
            else if (Session["User"].ToString() == "user")
            {
                LinkLogin.Visible = false;
                LinkHome.Visible = true;
                LinkBackOffice.Visible = false;
                LinkCarrello.Visible = true;
                LinkLogout.Visible = true;
            }
            else if (Session["User"].ToString() == "admin")
            {
                LinkLogin.Visible = false;
                LinkHome.Visible = true;
                LinkBackOffice.Visible = true;
                LinkCarrello.Visible = true;
                LinkLogout.Visible = true;
            }
        }


        protected void LinkLogout_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("Default.aspx");
        }
    }
}