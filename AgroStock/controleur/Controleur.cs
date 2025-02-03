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
    }
}
