﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

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


            }

            string script = @"Swal.fire({
                        title: 'Schiavo aggiunto al carrello💘',
                        text: 'Grazie per averci scelto, cucciolo.',
                        imageUrl: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQF-IVwNQFbDBhPkPIDm-R55qzbebG03LTQow&usqp=CAU',
                        imageWidth: 400,
                        imageHeight: 200,
                        imageAlt: 'Immagine di esempio',
                    });";

            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", script, true);
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string search = txtRicerca.Value.Trim();
            string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();

            using (SqlConnection conn = new SqlConnection(Prodotti))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "Select * from Prodotti where Nome like'%' + @searchText + '%' OR Razza LIKE '%' + @searchText + '%'"
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
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}