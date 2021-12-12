using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Depense.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Auth;

[assembly : Dependency(typeof(Depense.Droid.Dependances.Auth))]
namespace Depense.Droid.Dependances
{
    public class Auth : IAuth
    {
        public async Task<bool> ConnecterUtilisateur(string adresseCourriel, string motDePasse)
        {
            try
            {
                await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(adresseCourriel, motDePasse);
                return true;
            }
            catch (FirebaseAuthInvalidUserException erreur)
            {
                throw new Exception(erreur.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException erreur)
            {
                throw new Exception(erreur.Message);
            }
            catch (Exception ex)
            {
                // il faut enregistrer la vraie exception quelque part
                // ne masquez jamais la vraie exception comme ça
                throw new Exception("Une erreur est survenue");
            }


        }

        public async Task<bool> CreerUtilisateur(string adresseCourriel, string motDePasse)
        {
            try
            {
                await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(adresseCourriel, motDePasse);
                return true;
            }
            catch (FirebaseAuthUserCollisionException erreur)
            {
                throw new Exception(erreur.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException erreur)
            {
                throw new Exception(erreur.Message);
            }
            catch (Exception ex)
            {
                // il faut enregistrer la vraie exception quelque part
                // ne masquez jamais la vraie exception comme ça
                throw new Exception("Une erreur est survenue");
            }

        }

        public string RetourerIdentifiantUtilisateur()
        {
            return FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public bool UtilisateurAuthentifie()
        {
            return FirebaseAuth.Instance.CurrentUser != null;
        }
    }
}