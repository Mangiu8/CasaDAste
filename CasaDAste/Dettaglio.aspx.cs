using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Messaging;

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
                        prezzoProdotto.InnerText = "Prezzo: " +reader1.GetDecimal(4).ToString("0.00") +" Berries";
                        razzaProdotto.InnerText= "Razza: " +reader1.GetString(6);
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
            int qt = Int32.Parse( quantita.Text);
            //crea cookie


            Response.Redirect("Carrello.aspx");
        }
    }
}