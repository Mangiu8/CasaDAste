using System;
using System.Collections.Generic;
using System.Linq;

namespace CasaDAste
{
    public partial class Carrello1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LegareArticoliCarrello(); // Lega i prodotti presenti nel carrello al repeater per visualizzarli nella pagina
                CalcolaTotaleSpesa(); // Calcola il totale della spesa e lo visualizza nella pagina 
            }
        }
        // Metodo per legare i prodotti presenti nel carrello al repeater per visualizzarli nella pagina
        // Non riceve parametri in quanto recupera il carrello dalla sessione
        // Non restituisce nulla in quanto lega il carrello al repeater
        private void LegareArticoliCarrello()
        {
            if (Session["Carrello"] != null) // Verifico che il carrello sia presente in sessione
            {
                // Recupero il carrello dalla sessione e lo lego al repeater per visualizzarlo nella pagina 
                var carrello = (List<Carrello>)Session["Carrello"];
                rptCarrello.DataSource = carrello; // Imposto la sorgente dati del repeater
                rptCarrello.DataBind(); // Databind del repeater per visualizzare i dati 
            }

        }


        // Metodo per gestire l'evento click del pulsante "Elimina" presente nel repeater
        // Riceve come parametri l'oggetto che ha generato l'evento e le informazioni relative all'evento
        // Non restituisce nulla in quanto elimina un prodotto dal carrello
        protected void btnElimina_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandName == "Elimina") // Se il comando passato è "Elimina" allora elimino il prodotto dal carrello
            {
                // Recupero il nome del prodotto da eliminare dal comando passato dal pulsante "Elimina" attraverso l'evento
                string nomeProdottoDaEliminare = e.CommandArgument.ToString();

                if (Session["Carrello"] != null) // Verifico che il carrello sia presente in sessione
                {
                    var carrello = (List<Carrello>)Session["Carrello"]; // Recupero il carrello dalla sessione

                    // Rimuovo il prodotto dal carrello in base al nome del prodotto passato come argomento al comando
                    // Attraverso il metodo RemoveAll rimuovo tutti gli elementi del carrello che hanno come nome il nome del prodotto da eliminare
                    // In questo modo posso eliminare più prodotti con lo stesso nome dal carrello
                    carrello.RemoveAll(p => p.Nome == nomeProdottoDaEliminare);

                    LegareArticoliCarrello(); // Rilego il carrello al repeater per visualizzare i prodotti aggiornati
                    CalcolaTotaleSpesa(); // Ricalcolo il totale della spesa e lo visualizzo nella pagina
                }
            }
        }

        // Metodo per gestire l'evento click del pulsante "Acquista" presente nella pagina
        // Non riceve parametri in quanto recupera il carrello dalla sessione
        // Scrive il totale della spesa nella label "lblTotale" 
        private void CalcolaTotaleSpesa()
        {
            if (Session["Carrello"] != null) // Verifico che il carrello sia presente in sessione
            {
                // Recupero il carrello dalla sessione e calcolo il totale della spesa
                var carrello = (List<Carrello>)Session["Carrello"];
                // Calcolo il totale della spesa sommando i prezzi di tutti i prodotti presenti nel carrello
                double totale = carrello.Sum(item => item.Prezzo);

                lblTotale.Text = $"Totale: {totale} Berries";
            }
            else
            {
                lblTotale.Text = "Totale: 0,00 Berries";
            }
        }
    }
}