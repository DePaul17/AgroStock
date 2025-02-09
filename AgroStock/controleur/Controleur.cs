using System;


namespace AgroStock
{
    public class Controleur
    {
        private static Modele unModele = new Modele("localhost", "agrostock_db", "root", "");

        //********************MODELE CRUD ProductCategory***********************
        //AJOUT
        public static void InsertCategory(ProductCategory productCategory)
        {
            unModele.InsertCategory(productCategory);
        }
        //SUPPRESSION
        public static void DeleteCategory(int id)
        {
            unModele.DeleteCategory(id);
        }
        //UPDATE
        public static void UpdateCategory(ProductCategory productCategory)
        {
            unModele.UpdateCategory(productCategory);
        }

        //********************MODELE CRUD ProductSubcategory***********************
        //AJOUT
        public static void InsertSubcategory(ProductSubcategory productSubcategory)
        {
            unModele.InsertSubcategory(productSubcategory);
        }
        //SUPPRESSION
        public static void DeleteSubcategory(int id)
        {
            unModele.DeleteSubcategory(id);
        }
        //UPDATE
        public static void UpdateSubcategory(ProductSubcategory productSubcategory)
        {
            unModele.UpdateSubcategory(productSubcategory);
        }

        //********************MODELE CRUD Product***********************
        //AJOUT
        public static void InsertProduct(Product product)
        {
            unModele.InsertProduct(product);
        }
        //SUPPRESSION
        public static void DeleteProduct(int id)
        {
            unModele.DeleteProduct(id);
        }
        //UPDATE
        public static void UpdateProduct(Product product)
        {
            unModele.UpdateProduct(product);
        }
        //LISTE DES PRODUITS
        public static List<Product> GetAllProducts()
        {
            return unModele.GetAllProducts();
        }
        // LIST LIKE PRODUCT
        public static Product GetProductLike(int idProduct)
        {
            return unModele.GetlikeProduct(idProduct);
        }

        //********************MODELE CRUD CustomerOrder***********************
        //AJOUT
        public static void InsertCustomerOrder(CustomerOrder customerOrder)
        {
            unModele.InsertCustomerOrder(customerOrder);
        }
        //SUPPRESSION
        public static void DeleteCustomerOrder(int id)
        {
            unModele.DeleteCustomerOrder(id);
        }
        //UPDATE
        public static void UpdateCustomerOrder(CustomerOrder customerOrder)
        {
            unModele.UpdateCustomerOrder(customerOrder);
        }

        //********************MODELE CRUD StorageLocation***********************
        //AJOUT
        public static void InsertStorageLocation(StorageLocation storageLocation)
        {
            unModele.InsertStorageLocation(storageLocation);
        }
        //SUPPRESSION
        public static void DeleteStorageLocation(int id)
        {
            unModele.DeleteStorageLocation(id);
        }

        //********************MODELE CRUD Stock*********************************
        //AJOUT
        public static void InsertStock(Stock stock)
        {
            unModele.InsertStock(stock);
        }
        //SUPPRESSION
        public static void DeleteStock(int id)
        {
            unModele.DeleteStock(id);
        }

        //********************MODELE CRUD Historical****************************
        //AJOUT
        public static void InsertHistorical(Historical historical)
        {
            unModele.InsertHistorical(historical);
        }
    }
}
