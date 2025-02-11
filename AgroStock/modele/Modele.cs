using System;

using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Diagnostics;
using AgroStock;

namespace AgroStock
{
	public class Modele
	{
		private string serveur, bdd, user, mdp;
		private MySqlConnection maConnexion;

		public Modele(string serveur, string bdd, string user, string mdp)
		{
			this.serveur = serveur;
			this.bdd = bdd;
			this.user = user;
			this.mdp = mdp;

			string url = "SslMode=None";
			url += "; server=" + this.serveur;
			//url += "; Port=8889";   //Pour Mac
            url += "; Port=3306"; //Pour Windows
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
			string requete = "call insertCategory (@categoryName, @description);";
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
                Debug.WriteLine("Erreur execution requete: " + exp.Message);
            }
        }
		//SUPPRESSION
		public void DeleteCategory(int idCategory) {
			string requete = "call deleteCategory (@idCategory);";
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
        //UPDATE
        public void UpdateCategory(ProductCategory uneProductCategory)
        {
            string requete = "call updateCategory (@idCategory, @categoryName, @description);";
            MySqlCommand uneCategory = null;
            try
            {
                this.maConnexion.Open();
                uneCategory = this.maConnexion.CreateCommand();
                uneCategory.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une catégorie
                uneCategory.Parameters.AddWithValue("@idCategory", uneProductCategory.Id);
                uneCategory.Parameters.AddWithValue("@categoryName", uneProductCategory.CategoryName);
                uneCategory.Parameters.AddWithValue("@description", uneProductCategory.Description);
                //Excution de la requete
                uneCategory.ExecuteNonQuery();
                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur Execution requete: " + exp.Message);
            }
        }
        //LISTER LES CATEGORIES
        public List<ProductCategory> GetAllCategories()
        {
            List<ProductCategory> categories = new List<ProductCategory>();
            string requete = "SELECT * FROM v_ListeCategories;";
            MySqlCommand lesCategories = null;
            try
            {
                this.maConnexion.Open();
                lesCategories = this.maConnexion.CreateCommand();
                lesCategories.CommandText = requete;
                MySqlDataReader unReader = lesCategories.ExecuteReader();
                while (unReader.Read())
                {
                    ProductCategory category = new ProductCategory(
                        unReader.GetInt32("CategoryId"),
                        unReader.GetString("CategoryName"),
                        unReader.GetString("Description")
                    );
                    categories.Add(category);
                }
                unReader.Close();
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
            return categories;
        }

        //********************MODELE CRUD ProductSubcategory********************
        //AJOUT
        public void InsertSubcategory(ProductSubcategory uneProductSubcategory)
        {
            string requete = "call insertSubcategory (@subcategoryName, @description, @categoryId);";
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
            string requete = "call deleteSubcategory (@idSubcategory);";
            MySqlCommand uneSubcategory = null;
            try
            {
                this.maConnexion.Open();
                uneSubcategory = this.maConnexion.CreateCommand();
                uneSubcategory.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une sous-catégorie
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
        //UPDATE
        public void UpdateSubcategory(ProductSubcategory uneProductSubcategory)
        {
            string requete = "call updateSubcategory (@idSubcategory, @subcategoryName, @description, @categoryId);";
            MySqlCommand uneSubcategory = null;
            try
            {
                this.maConnexion.Open();
                uneSubcategory = this.maConnexion.CreateCommand();
                uneSubcategory.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une sous-catégorie
                uneSubcategory.Parameters.AddWithValue("@idSubcategory", uneProductSubcategory.Id);
                uneSubcategory.Parameters.AddWithValue("@subcategoryName", uneProductSubcategory.SubcategoryName);
                uneSubcategory.Parameters.AddWithValue("@description", uneProductSubcategory.Description);
                uneSubcategory.Parameters.AddWithValue("@categoryId", uneProductSubcategory.CategoryId);
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
        //LISTER LES SOUS-CATEGORIES
        public List<ProductSubcategory> GetAllSubcategories()
        {
            List<ProductSubcategory> subcategories = new List<ProductSubcategory>();
            string requete = @"
                                SELECT 
                                    ps.Id AS SubcategoryId,
                                    ps.SubcategoryName,
                                    ps.Description,
                                    ps.CategoryId,  -- Ajouter CategoryId
                                    pc.CategoryName AS ParentCategory
                                FROM product_subcategory ps
                                JOIN product_category pc ON ps.CategoryId = pc.Id;";

            MySqlCommand lesSubcategories = null;
            try
            {
                this.maConnexion.Open();
                lesSubcategories = this.maConnexion.CreateCommand();
                lesSubcategories.CommandText = requete;
                MySqlDataReader unReader = lesSubcategories.ExecuteReader();
                while (unReader.Read())
                {
                    ProductSubcategory subcategory = new ProductSubcategory(
                        unReader.GetInt32("SubcategoryId"),       // ID de la sous-catégorie
                        unReader.GetString("SubcategoryName"),    // Nom de la sous-catégorie
                        unReader.GetString("Description"),        // Description de la sous-catégorie
                        unReader.GetInt32("CategoryId"),          // ID de la catégorie parente
                        unReader.GetString("ParentCategory")      // Nom de la catégorie parente
                    );
                    subcategories.Add(subcategory);
                }
                unReader.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + exp.Message);
            }
            finally
            {
                if (this.maConnexion.State == System.Data.ConnectionState.Open)
                {
                    this.maConnexion.Close();
                }
            }
            return subcategories;
        }




        /** public List<ProductSubcategory> GetAllSubcategories()
        {
            List<ProductSubcategory> subcategories = new List<ProductSubcategory>();
            string requete = "SELECT * FROM v_ListeSubcategories;";
            MySqlCommand lesSubcategories = null;
            try
            {
                this.maConnexion.Open();
                lesSubcategories = this.maConnexion.CreateCommand();
                lesSubcategories.CommandText = requete;
                MySqlDataReader unReader = lesSubcategories.ExecuteReader();
                while (unReader.Read())
                {
                    ProductSubcategory subcategory = new ProductSubcategory(
                        unReader.GetInt32("SubcategoryId"),
                        unReader.GetString("SubcategoryName"),
                        unReader.GetString("Description"),
                        unReader.GetString("ParentCategory") //ParentCategory
                    );
                    subcategories.Add(subcategory);
                }
                unReader.Close();
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
            return subcategories;
        } */

        

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
        //UPDATE
        public void UpdateProduct(Product unProduct)
        {
            string requete = "call updateProduct(@productId, @productName, @productionDate, @totalCarbonFootprint, @resourcesUsed, @price, @subcategoryId);";
            MySqlCommand unNewProduct = null;
            try
            {
                this.maConnexion.Open();
                unNewProduct = this.maConnexion.CreateCommand();
                unNewProduct.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'un produit
                unNewProduct.Parameters.AddWithValue("@productId", unProduct.Id);
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

        //SELECTIONER UN PRODUIT
        public Product GetlikeProduct(int idProduct)
        {
            Product product = null;
            string requete = "SELECT * FROM v_liste_products WHERE ProductId = @idProduct;";
            MySqlCommand leProduct = null;
            try
            {
                this.maConnexion.Open();
                leProduct = this.maConnexion.CreateCommand();
                leProduct.CommandText = requete;
                leProduct.Parameters.AddWithValue("@idProduct", idProduct);
                MySqlDataReader unReader = leProduct.ExecuteReader();
                while (unReader.Read())
                {
                    product = new Product(
                        unReader.GetInt32("ProductId"),
                        unReader.GetString("ProductName"),
                        unReader.GetDateTime("ProductionDate"),
                        unReader.GetFloat("TotalCarbonFootprint"),
                        unReader.GetString("ResourcesUsed"),
                        unReader.GetDecimal("Price"),
                        unReader.GetInt32("SubcategoryId")
                    );
                }
                unReader.Close();
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
            return product;
        }
        //LISTER LES PRODUITS
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string requete = "SELECT * FROM v_liste_products;";
            MySqlCommand lesProducts = null;
            try
            {
                this.maConnexion.Open();
                lesProducts = this.maConnexion.CreateCommand();
                lesProducts.CommandText = requete;
                MySqlDataReader unReader = lesProducts.ExecuteReader();
                while (unReader.Read())
                {
                    Product product = new Product(
                        unReader.GetInt32("ProductId"),
                        unReader.GetString("ProductName"),
                        unReader.GetDateTime("ProductionDate"),
                        unReader.GetFloat("TotalCarbonFootprint"),
                        unReader.GetString("ResourcesUsed"),
                        unReader.GetDecimal("Price"),
                        unReader.GetInt32("SubcategoryId")
                    );
                    products.Add(product);
                }
                unReader.Close();
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
            return products;
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
                //Faire la correspondance entre les variables SQL et les données d'un Emplaxcement
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
                //Faire la correspondance entre les variables SQL et les données d'un stock
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

        //********************MODELE CRUD Comandes******************************
        //AJOUT
        public void InsertCustomerOrder(CustomerOrder uneCustomerOrder)
        {
            string requete = "call insertOrder(@orderDate, @customerId, @totalAmount, @status);";
            MySqlCommand uneOrder = null;
            try
            {
                this.maConnexion.Open();
                uneOrder = this.maConnexion.CreateCommand();
                uneOrder.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une commande
                uneOrder.Parameters.AddWithValue("@orderDate", uneCustomerOrder.OrderDate);
                uneOrder.Parameters.AddWithValue("@customerId", uneCustomerOrder.CustomerId);
                uneOrder.Parameters.AddWithValue("@totalAmount", uneCustomerOrder.TotalAmount);
                uneOrder.Parameters.AddWithValue("@status", uneCustomerOrder.Status);

                //Execution de la requete
                uneOrder.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
        }

        //SUPPRESSION
        public void DeleteCustomerOrder(int idOrder)
        {
            string requete = "call deleteOrder(@idOrder);";
            MySqlCommand uneOrder = null;
            try
            {
                this.maConnexion.Open();
                uneOrder = this.maConnexion.CreateCommand();
                uneOrder.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une commande
                uneOrder.Parameters.AddWithValue("@idOrder", idOrder);

                //Excution de la requete
                uneOrder.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erreur Execution requete");
            }
        }
        //UPDATE
        public void UpdateCustomerOrder(CustomerOrder uneCustomerOrder)
        {
            string requete = "call updateOrder(@orderId, @orderDate, @customerId, @totalAmount, @status);";
            MySqlCommand uneOrder = null;
            try
            {
                this.maConnexion.Open();
                uneOrder = this.maConnexion.CreateCommand();
                uneOrder.CommandText = requete;
                //Faire la correspondance entre les variables SQL et les données d'une commande
                uneOrder.Parameters.AddWithValue("@orderId", uneCustomerOrder.OrderId);
                uneOrder.Parameters.AddWithValue("@orderDate", uneCustomerOrder.OrderDate);
                uneOrder.Parameters.AddWithValue("@customerId", uneCustomerOrder.CustomerId);
                uneOrder.Parameters.AddWithValue("@totalAmount", uneCustomerOrder.TotalAmount);
                uneOrder.Parameters.AddWithValue("@status", uneCustomerOrder.Status);

                //Execution de la requete
                uneOrder.ExecuteNonQuery();

                //Fermeture de la connexion
                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete");
            }
        }
        //LIST CUSTOMER ORDER, Lister les commandes client
        
    }
}

