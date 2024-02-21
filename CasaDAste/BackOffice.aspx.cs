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
            // Session["User"] = "admin"; //todo: da togliere questa riga
            if (!IsPostBack)
            {
                if (Session["Message"] != null)
                {
                    Label2.Text = Session["Message"].ToString();
                    Session["Message"] = null;
                }
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

        protected void LnkEdit_Click(object sender, EventArgs e)
        {
            BtnUpdate.Text = "Modifica";
            Labelimg.Text = "Modifica l'immagine del tuo schiavo";
            Labelnome.Text = "Modifica il nome del tuo schiavo";
            Labeldescrizione.Text = "Modifica la descrizione del tuo schiavo";
            Labelprezzo.Text = "Modifica il prezzo del tuo schiavo";
            Labelqnt.Text = "Modifica la quantità disponibile del tuo schiavo";
            Labelrazza.Text = "Modifica la razza del tuo schiavo";

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
                    descrizione.Value = reader1.GetString(3);
                    prezzo.Text = reader1.GetDecimal(4).ToString();
                    quantita.Text = reader1.GetInt32(5).ToString();
                    razza.Text = reader1.GetString(6);
                    idPerFavore.Text = reader1.GetInt32(0).ToString();
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

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
            int itemId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlConnection conn = new SqlConnection(Prodotti);

            try
            {
                conn.Open();
                SqlCommand command1 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "DELETE FROM Prodotti WHERE iDProdott = @ID"
                };


                command1.Parameters.AddWithValue("@ID", itemId);

                command1.ExecuteNonQuery();
                Session["Message"] = "Schiavo eliminato correttamente, spero per te che tu abbia nascosto bene il cadavere.";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Session["Message"] = "Errore nell'eliminazione dello schiavo, sparagli, fai prima.";
            }
            finally
            {
                conn.Close();

                Response.Redirect(Request.RawUrl);
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(Prodotti);

            if (BtnUpdate.Text != "Modifica")
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
                    command1.Parameters.AddWithValue("@Descrizione", descrizione.Value);
                    command1.Parameters.AddWithValue("@Prezzo", prezzo.Text);
                    command1.Parameters.AddWithValue("@QuantitaDisponibile", quantita.Text);
                    command1.Parameters.AddWithValue("@Razza", razza.Text);

                    command1.ExecuteNonQuery();
                    Session["Message"] = "Schiavo aggiunto correttamente, ora è tuo compito nutrirlo e crescerlo.";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    Session["Message"] = "Errore nell'aggiunta dello schiavo, forse è meglio se ti dedichi a qualcos'altro.";
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
                    command1.Parameters.AddWithValue("@Descrizione", descrizione.Value);
                    command1.Parameters.AddWithValue("@Prezzo", Convert.ToDecimal(prezzo.Text));
                    command1.Parameters.AddWithValue("@QuantitaDisponibile", Convert.ToInt32(quantita.Text));
                    command1.Parameters.AddWithValue("@Razza", razza.Text);
                    command1.Parameters.AddWithValue("@ID", Convert.ToInt32(idPerFavore.Text));
                    command1.ExecuteNonQuery();
                    Session["Message"] = "Schiavo modificato correttamente, ti prego dimmi che non gli hai cambiato i connotati.";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                    Session["Message"] = "Errore nella modifica dello schiavo, forse è meglio se ti dedichi a qualcos'altro.";
                }
                finally
                {

                    conn.Close();
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
    }
}