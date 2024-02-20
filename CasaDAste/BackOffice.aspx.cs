using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CasaDAste
{
    public partial class BackOffice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(Prodotti);
                try
                {
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Prodotti ORDER BY IDProdott DESC", conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gridViewBackOffice.DataSource = dataTable;
                    gridViewBackOffice.DataBind();
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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Modifica";
            btnAdd.CommandArgument = (sender as LinkButton).CommandArgument;
            int itemId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(Prodotti);

            try
            {
                conn.Open();
                SqlCommand command1 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "Select * from Prodotti where iDProdott = @ID"
                };
                command1.Parameters.AddWithValue("@ID", itemId);
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    immagine.Text = reader1.GetString(1);
                    nome.Text = reader1.GetString(2);
                    descrizione.Text = reader1.GetString(3);
                    prezzo.Text = reader1.GetDecimal(4).ToString();
                    quantita.Text = reader1.GetInt32(5).ToString();
                    razza.Text = reader1.GetString(6);
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(Prodotti);

            try
            {
                conn.Open();
                SqlCommand command1 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "DELETE FROM Prodotti WHERE iDProdott = @ID"
                };

                int itemId = Convert.ToInt32((sender as LinkButton).CommandArgument);

                command1.Parameters.AddWithValue("@ID", itemId);

                command1.ExecuteNonQuery();
                Response.Write("Prodotto eliminato correttamente");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
                // refresh the page
                Response.Redirect(Request.RawUrl);
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(Prodotti);

            if (btnAdd.Text != "Modifica")
            {
                try
                {
                    conn.Open();
                    SqlCommand command1 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "INSERT INTO Prodotti (Img, Nome, Descrizione, Prezzo, QuantitaDisponibile, Razza)" +
                                      "VALUES (@Img, @Nome, @Descrizione, @Prezzo, @QuantitaDisponibile, @Razza)"
                    };

                    command1.Parameters.AddWithValue("@Img", immagine.Text);
                    command1.Parameters.AddWithValue("@Nome", nome.Text);
                    command1.Parameters.AddWithValue("@Descrizione", descrizione.Text);
                    command1.Parameters.AddWithValue("@Prezzo", prezzo.Text);
                    command1.Parameters.AddWithValue("@QuantitaDisponibile", quantita.Text);
                    command1.Parameters.AddWithValue("@Razza", razza.Text);

                    command1.ExecuteNonQuery();
                    Response.Write("Prodotto aggiunto correttamente");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    conn.Close();
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand command1 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "Update Prodotti " +
                                      "SET Img = @Img, Nome = @Nome, Descrizione = @Descrizione, Prezzo = @Prezzo, QuantitaDisponibile = @QuantitaDisponibile, Razza = @Razza " +
                                      "WHERE iDProdott = @ID"
                    };
                    command1.Parameters.AddWithValue("@Img", immagine.Text);
                    command1.Parameters.AddWithValue("@Nome", nome.Text);
                    command1.Parameters.AddWithValue("@Descrizione", descrizione.Text);
                    command1.Parameters.AddWithValue("@Prezzo", prezzo.Text);
                    command1.Parameters.AddWithValue("@QuantitaDisponibile", quantita.Text);
                    command1.Parameters.AddWithValue("@Razza", razza.Text);
                    int itemId = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    command1.Parameters.AddWithValue("@ID", itemId);
                    command1.ExecuteNonQuery();
                    Response.Write("Prodotto modificato correttamente");
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