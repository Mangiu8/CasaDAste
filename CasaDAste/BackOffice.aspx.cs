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
            btnAdd.Text = "Aggiorna";
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
                // refresh the page
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}