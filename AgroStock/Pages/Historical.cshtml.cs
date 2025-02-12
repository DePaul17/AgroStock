using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace AgroStock.Pages
{
    public class HistoricalModel : PageModel
    {
        private readonly Modele modele;

        public List<Historical> Historicals { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string Action { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int? UserId { get; set; }

        public HistoricalModel()
        {
            this.modele = new Modele("localhost", "agrostock_db", "root", "");
        }

        public void OnGet()
        {
            // Récupérer l'historique filtré
            Historicals = modele.SearchHistorical(StartDate, EndDate, Action, UserId);
        }
    }
}
