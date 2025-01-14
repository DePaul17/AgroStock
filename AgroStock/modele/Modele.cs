using System;

using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Diagnostics;
using AgroStockDB;

namespace AgroStock.modele
{
	public class Modele
	{
		private string serveur, bdd, user, mdp;
		private MySqlConnection maConnexion;

		private Modele(string serveur, string bdd, string user, string mdp)
		{
			this.serveur = serveur;
			this.bdd = bdd;
			this.user = user;
			this.mdp = mdp;

			string url = "SslMode=None";
			url += "; server=" + this.serveur;
			url += "; Port=8889";   //Pour Mac
            //url += "; Port=3306"; //Pour Windows
            url += "; Database=" + this.bdd;
			url += "; User=" + this.user;
			url += "; Password=" + this.mdp;

			try {
				this.maConnexion = new MySqlConnection(url);
			}
			catch {
				Debug.WriteLine("Erreur de connexion à l'url");
			}
		}

		//********************MODELE CRUD ProductCategory***********************
		//AJOUT
		public void InsertCategory(ProductCategory uneProductCategory)
		{
			string requete = "call insertCategory(@categoryName, @description);";
            MySqlCommand uneCategory = null;
            try {
				this.maConnexion.Open();
				uneCategory = this.maConnexion.CreateCommand();
				uneCategory.CommandText = requete;
				//Faire la correspondance entre les variables SQL et les données d'une catégorie
				uneCategory.Parameters.AddWithValue("@categoryName", uneProductCategory.CategoryName);
				uneCategory.Parameters.AddWithValue("@description", uneProductCategory.Description);

                //Execution de la requete
                uneCategory.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
        }
		//SUPPRESSION
		public void DeleteCategory(int idCategory) {
			string requete = "call deleteCategory(@idCategory);";
			MySqlCommand uneCategory = null;
			try {
				this.maConnexion.Open();
				uneCategory = this.maConnexion.CreateCommand();
				uneCategory.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une catégorie
                uneCategory.Parameters.AddWithValue("@idCategory", idCategory);

				//Excution de la requete
				uneCategory.ExecuteNonQuery();

				//Fermeture de la connexion
				this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur Execution requete");
			}	
        }

        //********************MODELE CRUD ProductSubcategory********************
        //AJOUT
        public void InsertSubcategory(ProductSubcategory uneProductSubcategory)
        {
            string requete = "call insertSubcategory(@subcategoryName, @description, @categoryId);";
            MySqlCommand uneSubcategory = null;
            try
            {
                this.maConnexion.Open();
                uneSubcategory = this.maConnexion.CreateCommand();
                uneSubcategory.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une sous-catégorie
                uneSubcategory.Parameters.AddWithValue("@subcategoryName", uneProductSubcategory.SubcategoryName);
                uneSubcategory.Parameters.AddWithValue("@description", uneProductSubcategory.Description);
                uneSubcategory.Parameters.AddWithValue("@categoryId", uneProductSubcategory.CategoryId);

                //Execution de la requete
                uneSubcategory.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
        }
        //SUPPRESSION
        public void DeleteSubcategory(int idSubcategory)
        {
            string requete = "call deleteSubcategory(@idSubcategory);";
            MySqlCommand uneSubcategory = null;
            try
            {
                this.maConnexion.Open();
                uneSubcategory = this.maConnexion.CreateCommand();
                uneSubcategory.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une catégorie
                uneSubcategory.Parameters.AddWithValue("@idSubcategory", idSubcategory);

                //Excution de la requete
                uneSubcategory.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur Execution requete");
            }
        }

        //********************MODELE CRUD Product*******************************
        //AJOUT
        public void InsertProduct(Product unProduct)
        {
            string requete = "call insertProduct(@productName, @productionDate, @totalCarbonFootprint, @resourcesUsed, @price, @subcategoryId);";
            MySqlCommand unNewProduct = null;
            try
            {
                this.maConnexion.Open();
                unNewProduct = this.maConnexion.CreateCommand();
                unNewProduct.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'un produit
                unNewProduct.Parameters.AddWithValue("@productName", unProduct.ProductName);
                unNewProduct.Parameters.AddWithValue("@productionDate", unProduct.ProductionDate);
                unNewProduct.Parameters.AddWithValue("@totalCarbonFootprint", unProduct.TotalCarbonFootprint);
                unNewProduct.Parameters.AddWithValue("@resourcesUsed", unProduct.ResourcesUsed);
                unNewProduct.Parameters.AddWithValue("@price", unProduct.Price);
                unNewProduct.Parameters.AddWithValue("@subcategoryId", unProduct.SubcategoryId);

                //Execution de la requete
                unNewProduct.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
        }
        //SUPPRESSION
        public void DeleteProduct(int idProduct)
        {
            string requete = "call deleteProduct(@idProduct);";
            MySqlCommand unNewProduct = null;
            try
            {
                this.maConnexion.Open();
                unNewProduct = this.maConnexion.CreateCommand();
                unNewProduct.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'un produit
                unNewProduct.Parameters.AddWithValue("@idProduct", idProduct);

                //Excution de la requete
                unNewProduct.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur Execution requete");
            }
        }

        //********************MODELE CRUD StorageLocation***********************
        //AJOUT
        public void InsertStorageLocation(StorageLocation unStorageLocation)
        {
            string requete = "call insertStorageLocation(@locationName, @storageType, @maxCapacity);";
            MySqlCommand unLocation = null;
            try
            {
                this.maConnexion.Open();
                unLocation = this.maConnexion.CreateCommand();
                unLocation.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une Emplaxcement
                unLocation.Parameters.AddWithValue("@locationName", unStorageLocation.LocationName);
                unLocation.Parameters.AddWithValue("@storageType", unStorageLocation.StorageType);
                unLocation.Parameters.AddWithValue("@maxCapacity", unStorageLocation.MaxCapacity);

                //Execution de la requete
                unLocation.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
        }
        //SUPPRESSION
        public void DeleteStorageLocation(int idStorageLocation)
        {
            string requete = "call deleteStorageLocation(@idStorageLocation);";
            MySqlCommand unLocation = null;
            try
            {
                this.maConnexion.Open();
                unLocation = this.maConnexion.CreateCommand();
                unLocation.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'un Emplacement
                unLocation.Parameters.AddWithValue("@idStorageLocation", idStorageLocation);

                //Excution de la requete
                unLocation.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur Execution requete");
            }
        }

        //********************MODELE CRUD Stock*********************************
        //AJOUT
        public void InsertStock(Stock unStock)
        {
            string requete = "call insertStock(@productId, @entryDate, @exitDate, @storageLocationId, @quantity);";
            MySqlCommand unNewStock = null;
            try
            {
                this.maConnexion.Open();
                unNewStock = this.maConnexion.CreateCommand();
                unNewStock.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'un stock
                unNewStock.Parameters.AddWithValue("@productId", unStock.ProductId);
                unNewStock.Parameters.AddWithValue("@entryDate", unStock.EntryDate);
                unNewStock.Parameters.AddWithValue("@exitDate", unStock.ExitDate);
                unNewStock.Parameters.AddWithValue("@storageLocationId", unStock.StorageLocationId);
                unNewStock.Parameters.AddWithValue("@quantity", unStock.Quantity);

                //Execution de la requete
                unNewStock.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
        }
        //SUPPRESSION
        public void DeleteStock(int idStock)
        {
            string requete = "call deleteStock(@idStock);";
            MySqlCommand unNewStock = null;
            try
            {
                this.maConnexion.Open();
                unNewStock = this.maConnexion.CreateCommand();
                unNewStock.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une catégorie
                unNewStock.Parameters.AddWithValue("@idStock", idStock);

                //Excution de la requete
                unNewStock.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur Execution requete");
            }
        }

        //********************MODELE CRUD Historical****************************
        //AJOUT
        public void InsertHistorical(Historical unHistorical)
        {
            string requete = "call insertHistorical(@userId, @action);";
            MySqlCommand uneNewHistorical = null;
            try
            {
                this.maConnexion.Open();
                uneNewHistorical = this.maConnexion.CreateCommand();
                uneNewHistorical.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une historique
                uneNewHistorical.Parameters.AddWithValue("@userId", unHistorical.UserId);
                uneNewHistorical.Parameters.AddWithValue("@action", unHistorical.Action);

                //Execution de la requete
                uneNewHistorical.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
        }
    }
}

