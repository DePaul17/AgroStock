using System;

using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Diagnostics;

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
			url += "; Port=8889";
			url += "; Dataase=" + this.bdd;
			url += "; User=" + this.user;
			url += "; Password=" + this.mdp;

			try {
				this.maConnexion = new MySqlConnection(url);
			}
			catch {
				Debug.WriteLine("Erreur de connexion à l'url");
			}
		}

		//MODELE CRUD ProductCategory
		public void InsertCategory (ProductCategory unProductCategory)
		{
			string requete = "call InsertCategory(@categoryName, @); ";

        }

    }
}

