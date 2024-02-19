using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace CasaDAste
{
    public partial class Home : System.Web.UI.Page
    {
        static public List<Carrello> carrello = new List<Carrello>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(Prodotti);
            try
            {
                conn.Open();

                SqlCommand command1 = new SqlCommand();
                command1.Connection = conn;
                command1.CommandText = "Select * from Prodotti";
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    Repeater1.DataSource = reader1;
                    Repeater1.DataBind();
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
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}