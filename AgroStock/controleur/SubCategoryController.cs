using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace AgroStock.controleur
{
    public class SubCategoryController : Controller
    {
        private static Controleur _controleur = new Controleur();

        [HttpPost]
        public IActionResult HandleSubcategoryForm(IFormCollection form)
        {
            string action = form["action"]; // "Valider" ou "Modifier"
            string message = "";

            // Récupérer les données du formulaire
            string nom = form["nom"];
            string description = form["description"];
            int idCategory = int.Parse(form["idCategory"]);

            // Contrôle des données
            List<string> donnees = new List<string> { nom, description, idCategory.ToString() };

            if (Controleur.controlerDonnees(donnees))
            {
                if (action == "Valider")
                {
                    // Insertion d'une nouvelle sous-catégorie
                    ProductSubcategory nouvelleSousCat = new ProductSubcategory(nom, description, idCategory, "");
                    Controleur.InsertSubcategory(nouvelleSousCat);
                    message = "Insertion réussie de la sous-catégorie.";
                }
                else if (action == "Modifier")
                {
                    // Modification d'une sous-catégorie existante
                    int id = int.Parse(form["id"]);
                    ProductSubcategory sousCatAModifier = new ProductSubcategory(id, nom, description, idCategory, "");
                    Controleur.UpdateSubcategory(sousCatAModifier);
                    message = "Modification réussie de la sous-catégorie.";
                }

                // Rediriger vers la page des sous-catégories après l'opération
                return RedirectToAction("Index", new { message = message });
            }
            else
            {
                // Afficher un message d'erreur si les données ne sont pas valides
                message = "Veuillez remplir tous les champs.";
                TempData["Message"] = message;
                return RedirectToAction("Index");
            }
        }
    }
}