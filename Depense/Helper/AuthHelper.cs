using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Depense.Helper
{
    public interface IAuth
    {
        Task<bool> CreerUtilisateur(string adresseCourriel, string motDePasse);
        Task<bool> ConnecterUtilisateur(string adresseCourriel, string motDePasse);
        bool UtilisateurAuthentifie();
        string RetourerIdentifiantUtilisateur();

    }
    public class Auth 
    {
        public static IAuth auth = DependencyService.Get<IAuth>();
        public async static Task<bool> CreerUtilisateur(string adresseCourriel, string motDePasse)
        {
            try
            {
                return await auth.CreerUtilisateur(adresseCourriel, motDePasse);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erreur", ex.Message, "Fermer");
                return false;
            }

        }
        public async static Task<bool> ConnecterUtilisateur(string adresseCourriel, string motDePasse)
        {
            try
            {
                return await auth.ConnecterUtilisateur(adresseCourriel, motDePasse);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erreur", ex.Message, "Fermer");
                return false;
            }

        }
        public static bool UtilisateurAuthentifie()
        {
            return auth.UtilisateurAuthentifie();
        }

        public static string RetourerIdentifiantUtilisateur()
        {
            return auth.RetourerIdentifiantUtilisateur();
        }

    }
  
}
