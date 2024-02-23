using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace CasaDAste
{
    public partial class Dettaglio : System.Web.UI.Page
    {
        // Array di URL di immagini per mostrare un'immagine random quando si aggiunge un prodotto al carrello
        static public string[] imageUrls =
        {
            "img/frusta.gif",
            "img/simpson.gif",
            "img/django.gif",
            "img/fuoco.gif",
            "img/griffin.gif",
            "img/onepunch.gif",
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            // Esegue uno script javascript per far partire un suono quando la pagina è completamente caricata
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "playAHHH", "playAHHH();", true);
                string IDProdotto = Request.QueryString["id"]; // Ottiene l'ID del prodotto dalla query string

                string Prodotti = ConfigurationManager.ConnectionStrings["Schiavi"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(Prodotti);
                try
                {
                    conn.Open();

                    // Istanzia un nuovo comando SQL chiamato command1
                    // la query seleziona tutti i campi della tabella Prodotti dove l'ID del prodotto è uguale all'ID del prodotto ottenuto dalla query string
                    SqlCommand command1 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = $"Select * from Prodotti where IDProdott = {IDProdotto}"
                    };

                    // Esegue il comando e ottiene i risultati in un oggetto SqlDataReader chiamato reader1
                    SqlDataReader reader1 = command1.ExecuteReader();

                    // If e non while perchè si aspetta solo un risultato (l'ID del prodotto è univoco) 
                    if (reader1.Read())
                    {
                        imgProdotto.Src = reader1.GetString(1);
                        nomeProdotto.InnerText = reader1.GetString(2);
                        descrizioneProdotto.InnerText = reader1.GetString(3);
                        prezzoProdotto.InnerText = reader1.GetDecimal(4).ToString("0.00");
                        razzaProdotto.InnerText = "Razza: " + reader1.GetString(6);
                        quantita.Attributes["max"] = reader1.GetInt32(5).ToString();
                    }

                    reader1.Close();

                    // Istanzia un nuovo comando SQL chiamato command2 
                    // la query seleziona i primi 6 prodotti dalla tabella Prodotti in ordine casuale
                    // (NEWID() è una funzione di SQL Server che restituisce un GUID casuale)
                    SqlCommand command2 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "SELECT TOP 6 * FROM Prodotti ORDER BY NEWID()"
                    };

                    // Esegue il comando e ottiene i risultati in un oggetto SqlDataReader chiamato reader2
                    SqlDataReader reader2 = command2.ExecuteReader();

                    // Associa i risultati al Repeater2
                    Repeater2.DataSource = reader2;

                    // Esegue il databind per mostrare i risultati
                    Repeater2.DataBind();

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

        // Metodo per aggiungere un prodotto al carrello
        // Riceve l'evento click del bottone Aggiungi al carrello, non restituisce nulla e mostra un messaggio di conferma
        protected void AddToCart_Click(object sender, EventArgs e)
        {
            // Crea un nuovo oggetto Carrello
            Carrello item = new Carrello();

            // Assegna i valori dell'immagine, del nome, della razza e del prezzo del prodotto all'oggetto Carrello
            item.Immagine = imgProdotto.Src;
            item.Nome = nomeProdotto.InnerText;
            item.Razza = razzaProdotto.InnerText;
            item.Prezzo = Convert.ToDouble(prezzoProdotto.InnerText);

            // Ottiene lo stato della sessione se esistente, altrimenti crea un nuovo array di oggetti Carrello vuoto 
            List<Carrello> carrello;
            if (Session["Carrello"] == null)
            {
                carrello = new List<Carrello>();
            }
            else
            {
                carrello = (List<Carrello>)Session["Carrello"];
            }

            // Aggiunge l'oggetto Carrello all'array di oggetti Carrello
            carrello.Add(item);

            // Aggiorna lo stato della sessione con l'array di oggetti Carrello aggiornato 
            Session["Carrello"] = carrello;

            // Esegue uno script javascript per far partire un suono quando si aggiunge un prodotto al carrello
            ScriptManager.RegisterStartupScript(this, GetType(), "PlaySound", "PlaySound();", true);

            // Genera un numero casuale per selezionare un'immagine random dall'array di URL di immagini
            Random random = new Random();
            int randomImageIndex = random.Next(0, imageUrls.Length); // Next genera un numero casuale tra 0 e la lunghezza dell'array imageUrls
            string randomImageurl = imageUrls[randomImageIndex];

            // Esegue uno script javascript per mostrare un messaggio di conferma con un'immagine random     
            string script = $@"Swal.fire({{
                title: 'Schiavo aggiunto al carrello💘',
                text: 'Grazie per averci scelto, cucciolo.',
                imageUrl: '{randomImageurl}',
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: 'Immagine di esempio',
            }});";

            // Esegue lo script appena creato 
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", script, true);

        }

        // Metodo per reindirizzare l'utente alla pagina Dettaglio.aspx con l'ID del prodotto come query string
        // Riceve l'evento click del bottone Dettaglio, non restituisce nulla e reindirizza l'utente alla pagina Dettaglio.aspx con l'ID del prodotto come query string
        protected void Button1_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

            if (e.CommandName == "AddToCart")
            {
                string[] args = e.CommandArgument.ToString().Split(';'); // ottieni un array di stringhe divise da ; dal CommandArgument del bottone AddToCart 
                string nomeProdotto = args[0]; // ottieni il nome del prodotto
                double prezzoProdotto = Convert.ToDouble(args[2]); // ottieni il prezzo del prodotto e convertilo in double
                string razzaProdotto = args[1]; // ottieni la razza del prodotto
                string immagineProdotto = args[3]; // ottieni l'immagine del prodotto

                // Crea un nuovo oggetto Carrello
                Carrello nuovoProdotto = new Carrello
                {
                    Nome = nomeProdotto,
                    Prezzo = prezzoProdotto,
                    Razza = razzaProdotto,
                    Immagine = immagineProdotto,
                };

                // Ottiene lo stato della sessione se esistente, altrimenti crea un nuovo array di oggetti Carrello vuoto
                if (Session["Carrello"] == null)
                {
                    Session["Carrello"] = new List<Carrello>();
                }


                // Otteniamo stato della sessione e si pusha, figa.
                List<Carrello> carrello = (List<Carrello>)Session["Carrello"];
                // Aggiunge l'oggetto Carrello all'array di oggetti Carrello
                carrello.Add(nuovoProdotto);

                // Script per far partire un suono quando si aggiunge un prodotto al carrello
                ScriptManager.RegisterStartupScript(this, GetType(), "PlaySound", "PlaySound();", true);

                // Genera un numero casuale per selezionare un'immagine random dall'array di URL di immagini
                Random random = new Random();

                // Next genera un numero casuale tra 0 e la lunghezza dell'array imageUrls
                int randomImageIndex = random.Next(0, imageUrls.Length);

                // Ottiene un'immagine random dall'array di URL di immagini
                string randomImageurl = imageUrls[randomImageIndex];

                // Definisce uno script javascript per mostrare un messaggio di conferma con un'immagine random
                string script = $@"Swal.fire({{
                                    title: 'Schiavo aggiunto al carrello💘',
                                    text: 'Grazie per averci scelto, cucciolo.',
                                    imageUrl: '{randomImageurl}',
                                    imageWidth: 400,
                                    imageHeight: 200,
                                    imageAlt: 'Immagine di esempio',
                                 }});";
                // Esegue lo script appena creato
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", script, true);
            }

        }

        // Metodo per reindirizzare l'utente alla pagina Dettaglio.aspx con l'ID del prodotto come query string
        // Riceve l'evento click del bottone Dettaglio, non restituisce nulla e reindirizza l'utente alla pagina Dettaglio.aspx con l'ID del prodotto come query string
        protected void Dettaglio_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string IDProdott = e.CommandArgument.ToString();
            Response.Redirect($"Dettaglio.aspx?id={IDProdott}");
        }
    }
}