using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CasaDAste
{
    public partial class Home : System.Web.UI.Page
    {
        static public List<Carrello> carrello = new List<Carrello>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(Prodotti);
                try
                {
                    conn.Open();

                    SqlCommand command1 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "Select * from Prodotti"
                    };
                    SqlDataReader reader1 = command1.ExecuteReader();

                    DataTable dataTable = new DataTable();

                    dataTable.Load(reader1);

                    Repeater1.DataSource = dataTable;
                    Repeater1.DataBind();
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


        protected void Dettaglio_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string IDProdott = e.CommandArgument.ToString();
            Response.Redirect($"Dettaglio.aspx?id={IDProdott}");
        }

        protected void Button1_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                string[] args = e.CommandArgument.ToString().Split(';');
                string nomeProdotto = args[0];
                double prezzoProdotto = Convert.ToDouble(args[1]);
                string razzaProdotto = args[2];

                Carrello nuovoProdotto = new Carrello
                {
                    Nome = nomeProdotto,
                    Prezzo = prezzoProdotto,
                    Razza = razzaProdotto,
                };

                if (Session["Carrello"] == null)
                {
                    Session["Carrello"] = new List<Carrello>();
                }

                // Otteniamo stato della sessione e si pusha, figa.
                List<Carrello> carrello = (List<Carrello>)Session["Carrello"];
                carrello.Add(nuovoProdotto);

                Response.Redirect("Carrello.aspx");
            }
        }
    }
}