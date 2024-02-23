using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CasaDAste
{
    public partial class Home : System.Web.UI.Page
    {
        // Array di path di immagini per mostrare un'immagine a caso quando si aggiunge un prodotto al carrello
        static public string[] imageUrls = {
            "img/frusta.gif",
            "img/simpson.gif",
            "img/django.gif",
            "img/fuoco.gif",
            "img/griffin.gif",
            "img/onepunch.gif",
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts(); // Carica i prodotti dal database e li mostra nella pagina 
            }
        }

        // Metodo per caricare i prodotti dal database e mostrarli nella pagina
        // (il metodo è protected perché deve essere accessibile solo dalla pagina stessa e dalle pagine derivate)
        // Non restituisce nulla (void) e non accetta parametri
        protected void LoadProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString)) // using 
            {
                try
                {
                    conn.Open();

                    // Istanzia un nuovo oggetto SqlCommand per eseguire una query sul database
                    // La query seleziona tutti i prodotti dalla tabella Prodotti 
                    SqlCommand command = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "SELECT * FROM Prodotti"
                    };

                    // Esegue la query e salva i risultati in un oggetto SqlDataReader chiamato reader
                    SqlDataReader reader = command.ExecuteReader();

                    // Crea un nuovo oggetto DataTable per salvare i risultati della query
                    DataTable dataTable = new DataTable();

                    // Carica i risultati della query (che stanno in reader, oggetto di tipo SqlDataReader) nel DataTable e chiude il reader
                    dataTable.Load(reader);

                    // Assegna il DataTable come sorgente dei dati per il Repeater 
                    Repeater1.DataSource = dataTable;

                    // Databind fa in modo che il Repeater mostri i dati del DataTable 
                    Repeater1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        // Metodo per gestire l'evento click del bottone "Dettaglio"
        // Reinidirizza alla pagina Dettaglio.aspx passando l'ID del prodotto come parametro
        // Accetta due parametri: sender (l'oggetto che ha generato l'evento) e e (l'evento)
        // Non restituisce nulla (void) e non fa altro 
        protected void Dettaglio_Command(object sender, CommandEventArgs e)
        {
            string IDProdotto = e.CommandArgument.ToString(); // ID del prodotto selezionato ottentuo dall'evento click del bottone "Dettaglio" 
            Response.Redirect($"Dettaglio.aspx?id={IDProdotto}"); // Reindirizza alla pagina Dettaglio.aspx passando l'ID del prodotto come parametro
        }

        // Metodo per gestire l'evento click del bottone "Aggiungi al carrello"
        // Fa partire uno script per mostrare un messaggio di conferma con un'immagine a caso
        // Non restituisce nulla (void) e non fa altro
        protected void Button1_Command(object sender, CommandEventArgs e)
        {
            // Se il comando è "AddToCart" (ovvero se è stato cliccato il bottone "Aggiungi al carrello") 
            if (e.CommandName == "AddToCart")
            {
                string[] args = e.CommandArgument.ToString().Split(';'); // Ottiene gli argomenti del comando (IDProdotto;Razza;Prezzo;Nome;Immagine)
                string nomeProdotto = args[0]; // Nome del prodotto
                double prezzoProdotto = Convert.ToDouble(args[2]); // Prezzo del prodotto
                string razzaProdotto = args[1]; // Razza del prodotto
                string immagineProdotto = args[3]; // Immagine del prodotto

                // Crea un nuovo oggetto di tipo Carrello con i valori ottenuti dagli argomenti del comando
                Carrello nuovoProdotto = new Carrello
                {
                    Nome = nomeProdotto,
                    Prezzo = prezzoProdotto,
                    Razza = razzaProdotto,
                    Immagine = immagineProdotto
                };

                // definisci una lista di oggetti di tipo Carrello chiamata carrello e inizializzala a null 
                List<Carrello> carrello;

                if (Session["Carrello"] == null) // se la sessione "Carrello" è vuota e non contiene nessun oggetto di tipo Carrello
                {
                    carrello = new List<Carrello>(); // inizializza la lista carrello come una nuova lista di oggetti di tipo Carrello
                    Session["Carrello"] = carrello; // assegna la lista carrello alla sessione "Carrello"
                }
                else // se la sessione "Carrello" contiene già oggetti di tipo Carrello
                {
                    carrello = (List<Carrello>)Session["Carrello"]; // assegna la sessione "Carrello" alla lista carrello
                }

                // Aggiunge il nuovo prodotto al carrello 
                carrello.Add(nuovoProdotto);

                // Mostra un messaggio di conferma con un'immagine a caso
                // ScriptManager è un oggetto che permette di eseguire script lato client
                // RegisterStartupScript è un metodo di ScriptManager che permette di registrare uno script per farlo eseguire quando la pagina è completamente caricata
                // this.GetType() è il tipo dell'oggetto corrente (la pagina)
                // "PlaySound" è il nome dello script
                // "PlaySound();" è il codice javascript da eseguire
                // true indica che lo script deve essere eseguito anche se la pagina è stata caricata con un postback
                ScriptManager.RegisterStartupScript(this, GetType(), "PlaySound", "PlaySound();", true);
            }

            Random random = new Random(); // Crea un nuovo oggetto di tipo Random per generare numeri casuali 
            // Genera un numero casuale tra 0 e la lunghezza dell'array imageUrls  
            int randomImageIndex = random.Next(0, imageUrls.Length);
            // Ottiene l'URL di un'immagine casuale dall'array imageUrls 
            string randomImageurl = imageUrls[randomImageIndex];

            // Crea uno script per mostrare un messaggio di conferma con un'immagine a caso 
            string script = $@"Swal.fire({{
                title: 'Schiavo aggiunto al carrello💘',
                text: 'Grazie per averci scelto, cucciolo.',
                imageUrl: '{randomImageurl}',
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: 'Immagine di esempio',
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", script, true);
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            // Ottiene il testo inserito nella barra di ricerca e lo salva in una variabile chiamata search, trim toglie gli spazi all'inizio e alla fine del testo
            string search = txtRicerca.Value.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Istanzia un nuovo oggetto SqlCommand per eseguire una query sul database 
                    // La query seleziona tutti i prodotti dalla tabella Prodotti che hanno il nome o la razza simile al testo inserito nella barra di ricerca
                    // Il testo inserito nella barra di ricerca è passato come parametro @searchText per evitare SQL injection 
                    SqlCommand command = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "SELECT * FROM Prodotti WHERE Nome LIKE '%' + @searchText + '%' OR Razza LIKE '%' + @searchText + '%'"
                    };

                    // Aggiunge il parametro @searchText alla query con il valore del testo inserito nella barra di ricerca 
                    command.Parameters.AddWithValue("@searchText", search);

                    // Esegue la query e salva i risultati in un oggetto SqlDataReader chiamato reader
                    SqlDataReader reader = command.ExecuteReader();

                    // Crea un nuovo oggetto DataTable per salvare i risultati della query
                    DataTable dataTable = new DataTable();

                    // Carica i risultati della query (che stanno in reader, oggetto di tipo SqlDataReader) nel DataTable e chiude il reader
                    dataTable.Load(reader);

                    // Assegna il DataTable come sorgente dei dati per il Repeater
                    Repeater1.DataSource = dataTable;
                    // Databind fa in modo che il Repeater mostri i dati del DataTable
                    Repeater1.DataBind();

                    // Script per far partire un suono quando si cerca un prodotto
                    ScriptManager.RegisterStartupScript(this, GetType(), "playMurlok", "playMurlok();", true);
                    // ScriptManager è un oggetto che permette di eseguire script lato client
                    // RegisterStartupScript è un metodo di ScriptManager che permette di registrare uno script per farlo eseguire quando la pagina è completamente caricata
                    // this.GetType() è il tipo dell'oggetto corrente (la pagina)
                    // "playMurlok" è il nome dello script
                    // "playMurlok();" è il codice javascript da eseguire
                    // true indica che lo script deve essere eseguito anche se la pagina è stata caricata con un postback

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
}
