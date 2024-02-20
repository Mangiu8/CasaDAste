using System;
using System.Configuration;
using System.Data.SqlClient;

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

                    while (reader1.Read())
                    {
                        imgProdotto.Src = reader1.GetString(1);
                        nomeProdotto.InnerText = reader1.GetString(2);
                        descrizioneProdotto.InnerText = reader1.GetString(3);
                        prezzoProdotto.InnerText = "Prezzo: " + reader1.GetDecimal(4).ToString("0.00");
                        razzaProdotto.InnerText = reader1.GetString(6);
                        quantita.Attributes["max"] = reader1.GetInt32(5).ToString();
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

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            //string productName = nomeProdotto.InnerText;
            //string productRace = razzaProdotto.InnerText;
            //string productDescription = descrizioneProdotto.InnerText;





            //CartItem newItem = new CartItem(productName, productRace, productDescription);


            //if (Session["Cart"] == null)
            //{
            //    List<CartItem> cartItems = new List<CartItem>();
            //    cartItems.Add(newItem);
            //    Session["Cart"] = cartItems;
            //}
            //else
            //{
            //    List<CartItem> cartItems = (List<CartItem>)Session["Cart"];
            //    cartItems.Add(newItem);
            //}

            Response.Redirect("Carrello.aspx");
        }
    }
}