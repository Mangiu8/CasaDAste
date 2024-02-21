using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace CasaDAste
{
    public partial class Dettaglio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string IDProdotto = Request.QueryString["id"];

                string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(Prodotti);
                try
                {
                    conn.Open();

                    SqlCommand command1 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = $"Select * from Prodotti where IDProdott = {IDProdotto}"
                    };
                    SqlDataReader reader1 = command1.ExecuteReader();

                    if (reader1.Read())
                    {
                        imgProdotto.Src = reader1.GetString(1);
                        nomeProdotto.InnerText = reader1.GetString(2);
                        descrizioneProdotto.InnerText = reader1.GetString(3);
                        prezzoProdotto.InnerText = reader1.GetDecimal(4).ToString("0.00");
                        razzaProdotto.InnerText = "Razza: " + reader1.GetString(6);
                        quantita.Attributes["max"] = reader1.GetInt32(5).ToString();
                    }
                    reader1.Close();

                    SqlCommand command2 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "SELECT TOP 6 * FROM Prodotti ORDER BY NEWID()"
                    };
                    SqlDataReader reader2 = command2.ExecuteReader();
                    Repeater2.DataSource = reader2;
                    Repeater2.DataBind();

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

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            Carrello item = new Carrello();
            item.Immagine = imgProdotto.Src;
            item.Nome = nomeProdotto.InnerText;
            item.Razza = razzaProdotto.InnerText;
            item.Prezzo = Convert.ToDouble(prezzoProdotto.InnerText);

            List<Carrello> carrello;
            if (Session["Carrello"] == null)
            {
                carrello = new List<Carrello>();
            }
            else
            {
                carrello = (List<Carrello>)Session["Carrello"];
            }

            carrello.Add(item);

            Session["Carrello"] = carrello;
            ScriptManager.RegisterStartupScript(this, GetType(), "PlaySound", "PlaySound();", true);

        }
        protected void Button1_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
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
                    Immagine = immagineProdotto,
                };

                if (Session["Carrello"] == null)
                {
                    Session["Carrello"] = new List<Carrello>();
                }

                // Otteniamo stato della sessione e si pusha, figa.
                List<Carrello> carrello = (List<Carrello>)Session["Carrello"];
                carrello.Add(nuovoProdotto);

                ScriptManager.RegisterStartupScript(this, GetType(), "PlaySound", "PlaySound();", true);

            }

        }
        protected void Dettaglio_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string IDProdott = e.CommandArgument.ToString();
            Response.Redirect($"Dettaglio.aspx?id={IDProdott}");
        }
    }
}