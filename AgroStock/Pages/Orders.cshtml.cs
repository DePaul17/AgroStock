using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroStock.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly Modele modele;

        public List<CustomerOrder> Orders { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CustomerId { get; set; }

        public OrdersModel()
        {
            this.modele = new Modele("localhost", "agrostock_db", "root", "");
        }

        public void OnGet()
        {
            // Récupérer les commandes filtrées
            Orders = modele.SearchOrders(StartDate, EndDate, Status, CustomerId);
        }

        public IActionResult OnPost(int orderId)
        {
            // Supprimer la commande
            modele.DeleteCustomerOrder(orderId);

            // Ajouter dans l'historique
            modele.InsertHistorical(new Historical(1, $"Suppression de la commande #{orderId}"));

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync([FromBody] UpdateStatusModel model)
        {
            if (model == null || model.OrderId <= 0 || string.IsNullOrEmpty(model.NewStatus))
            {
                return BadRequest();
            }

            // Mettre à jour le statut
            modele.UpdateOrderStatus(model.OrderId, model.NewStatus);

            // Ajouter dans l'historique
            modele.InsertHistorical(new Historical(1, $"Modification du statut de la commande #{model.OrderId} en '{model.NewStatus}'"));

            return new JsonResult(new { success = true });
        }
    }

    public class UpdateStatusModel
    {
        public int OrderId { get; set; }
        public string NewStatus { get; set; }
    }
}
