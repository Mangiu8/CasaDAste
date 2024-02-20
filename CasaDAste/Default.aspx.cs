using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace CasaDAste
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Register JavaScript code to show an alert when the page loads
                string script = "alert('Ogni riferimento a cose o persone e puramente casuale');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Utente = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(Utente);
            try
            {
                conn.Open();

                SqlCommand command1 = new SqlCommand();
                command1.Connection = conn;
                command1.CommandText = "Select * from Utenti";
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    if (reader1["Username"].ToString() == TextBox1.Text && reader1["Psw"].ToString() == TextBox2.Text && reader1["Administrator"].ToString() == "True")
                    {
                        Session["Username"] = TextBox1.Text;
                        Response.Redirect("BackOffice.aspx");
                    }
                    else if (reader1["Username"].ToString() == TextBox1.Text && reader1["Psw"].ToString() == TextBox2.Text && reader1["Administrator"].ToString() == "False")
                    {
                        Session["Username"] = TextBox1.Text;
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        Label1.Text = "Username o Password errati";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
    }
}