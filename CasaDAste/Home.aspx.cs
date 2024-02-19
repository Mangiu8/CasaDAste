﻿using System;
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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
      

        protected void Dettaglio_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string IDProdott = e.CommandArgument.ToString();
            Response.Redirect($"Dettaglio.aspx?id={IDProdott}");
        }
    }
}