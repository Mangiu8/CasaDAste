using Microsoft.Ajax.Utilities;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace CasaDAste
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Se la pagina non è un postback (quindi è la prima volta che viene caricata)
            if (!IsPostBack)
            {
                // Mostra un messaggio di benvenuto con un'immagine e un'icona
                // (SweetAlert è una libreria di alert personalizzati)
                // (https://sweetalert2.github.io/)
                string script = @"
                    Swal.fire({
                        imageUrl: 'https://www.shutterstock.com/image-vector/ancient-roman-gladiator-sword-slave-600nw-2100757492.jpg',
                        imageAlt: 'schiavo',
                        title: 'Benvenuto!',
                        text: 'Ogni riferimento a cose e persone è puramente casuale, forse... nessuno schiavo è stato ""maltrattato"".',
                        icon: 'warning',
                        confirmButtonText: 'Entra'
                    });";
                // Registra lo script per farlo eseguire quando la pagina è completamente caricata 
                // (in pratica quando è pronto a mostrare la pagina all'utente) 
                // ClientScript è un oggetto che permette di eseguire script lato client (javascript)
                // RegisterStartupScript è un metodo di ClientScript che permette di registrare uno script per farlo eseguire quando la pagina è completamente caricata
                // this.GetType() è il tipo dell'oggetto corrente (la pagina) 
                // "swal" è il nome dello script
                // script è il codice javascript da eseguire definito sopra
                // true indica che lo script deve essere eseguito anche se la pagina è stata caricata con un postback
                ClientScript.RegisterStartupScript(this.GetType(), "swal", script, true);
            }
        }

        // Accedi o crea account 
        // Se l'utente è un admin lo reindirizza alla pagina di amministrazione
        // Se l'utente è un utente normale lo reindirizza alla home
        // Se l'utente non esiste o la password è sbagliata, mostra un messaggio di errore
        // Se l'utente non esiste, crea un nuovo utente
        // Se l'utente esiste, mostra un messaggio di errore
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Connessione al database 
            string Utente = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(Utente);

            // se il testo del bottone è "Accedi"
            // (dipende dal button con ID CreaAccount che all'onclick modifica il testo da "Accedi" a "Crea Account") 
            if (Button1.Text == "Accedi")
            {
                try
                {
                    conn.Open();

                    SqlCommand command1 = new SqlCommand();
                    command1.Connection = conn;
                    command1.CommandText = "Select * from Utenti"; // Query per selezionare tutti gli utenti dal database
                    SqlDataReader reader1 = command1.ExecuteReader();

                    // Se l'utente è un admin lo reindirizza alla pagina di amministrazione 
                    // Se l'utente è un utente normale lo reindirizza alla home
                    while (reader1.Read())
                    {
                        // Se l'utente non esiste o la password è sbagliata, mostra un messaggio di errore
                        if (reader1["Username"].ToString() == TextBox1.Text && reader1["Psw"].ToString() == TextBox2.Text && reader1["Administrator"].ToString() == "True")
                        {
                            Session["User"] = "admin";
                            Response.Redirect("BackOffice.aspx");
                        }
                        // Se l'utente non esiste, mostra un messaggio di errore
                        else if (reader1["Username"].ToString() == TextBox1.Text && reader1["Psw"].ToString() == TextBox2.Text && reader1["Administrator"].ToString() == "False")
                        {
                            Session["User"] = "user";
                            Response.Redirect("Home.aspx");
                        }
                        // Se l'utente non esiste, mostra un messaggio di errore
                        else
                        {
                            Label1.Text = "Username o Password errati";
                        }
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
            else // Altrimenti se il testo del bottone è "Crea account"
            {
                // Crea un nuovo utente 
                // Prima controlla se il campo username o password sono vuoti, se sì, mostra un messaggio di errore
                if (TextBox1.Text.IsNullOrWhiteSpace() || TextBox2.Text.IsNullOrWhiteSpace())
                {
                    Label1.Text = "Inserisci un username e una password per creare l'account";
                }
                else // Se il campo username o password non sono vuoti, crea un nuovo utente
                {
                    // Tenta di aprire la connessione al database e inserire un nuovo utente
                    try
                    {
                        conn.Open();

                        // istanzia un nuovo comando chiamato command1 per inserire un nuovo utente nel database
                        // con i valori inseriti nei campi di testo della pagina
                        SqlCommand command1 = new SqlCommand
                        {
                            Connection = conn,
                            CommandText = "INSERT INTO Utenti (Username, Psw, Administrator) " +
                            "VALUES (@Username, @Psw, @Admin);"
                        };

                        // aggiunge i parametri al comando command1
                        // i valori dei parametri sono presi dai campi di testo della pagina
                        command1.Parameters.AddWithValue("@Username", TextBox1.Text);
                        command1.Parameters.AddWithValue("@Psw", TextBox2.Text);
                        command1.Parameters.AddWithValue("@Admin", 0);

                        // esegue il comando command1 (****ExecuteNonQuery perché è un comando di tipo INSERT)
                        command1.ExecuteNonQuery();
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

        // Cambia il testo del bottone da "Accedi" a "Crea account" e conseguente logica
        protected void CreaAccount_Click(object sender, EventArgs e)
        {
            Button1.Text = "Crea account";
            Button1.CssClass = "px-2 btn mt-3 btn-success";
        }
    }
}