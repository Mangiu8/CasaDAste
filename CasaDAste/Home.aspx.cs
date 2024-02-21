using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CasaDAste
{
    public partial class Home : System.Web.UI.Page
    {
        public string[] imageUrls = {
            "img/frusta.gif",
            "img/simpson.gif",
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        protected void LoadProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "SELECT * FROM Prodotti"
                    };

                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    Repeater1.DataSource = dataTable;
                    Repeater1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        protected void Dettaglio_Command(object sender, CommandEventArgs e)
        {
            string IDProdotto = e.CommandArgument.ToString();
            Response.Redirect($"Dettaglio.aspx?id={IDProdotto}");
        }

        protected void Button1_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                string[] args = e.CommandArgument.ToString().Split(';');
                string nomeProdotto = args[0];
                double prezzoProdotto = Convert.ToDouble(args[2]);
                string razzaProdotto = args[1];
                string immagineProdotto = args[3];

                Carrello nuovoProdotto = new Carrello
                {
                    Nome = nomeProdotto,
                    Prezzo = prezzoProdotto,
                    Razza = razzaProdotto,
                    Immagine = immagineProdotto
                };

                List<Carrello> carrello;

                if (Session["Carrello"] == null)
                {
                    carrello = new List<Carrello>();
                    Session["Carrello"] = carrello;
                }
                else
                {
                    carrello = (List<Carrello>)Session["Carrello"];
                }

                carrello.Add(nuovoProdotto);

                // Richiama una funzione JavaScript per riprodurre il suono
                ScriptManager.RegisterStartupScript(this, GetType(), "PlaySound", "PlaySound();", true);
            }
            Random random = new Random();
            int randomImageIndex = random.Next(0, imageUrls.Length);
            string randomImageurl = imageUrls[randomImageIndex];

            string script = $@"Swal.fire({{
                title: 'Schiavo aggiunto al carrello💘',
                text: 'Grazie per averci scelto, cucciolo.',
                imageUrl: '{randomImageurl}',
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: 'Immagine di esempio',
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", script, true);
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string search = txtRicerca.Value.Trim();
            string connectionString = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "SELECT * FROM Prodotti WHERE Nome LIKE '%' + @searchText + '%' OR Razza LIKE '%' + @searchText + '%'"
                    };

                    command.Parameters.AddWithValue("@searchText", search);
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    Repeater1.DataSource = dataTable;
                    Repeater1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
}
