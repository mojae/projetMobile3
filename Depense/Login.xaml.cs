using Depense.Helper;
using Depense.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Depense
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnConnexion_Clicked(object sender, EventArgs e)
        {
            var adresseCourriel = txtAdresseCourriel.Text;
            var motDePasse = txtMotDePasse.Text;

            if(string.IsNullOrEmpty(adresseCourriel) || string.IsNullOrEmpty(motDePasse))
            {
                DisplayAlert("Alert", "Veuillez saisir une adresse courriel et un mot de passe", "Fermer");
            }
            else
            {
                using (var conn = new SQLiteConnection(App.CheminBD))
                {

                   

                        var succes = await Auth.ConnecterUtilisateur(adresseCourriel, motDePasse);
                        var user = Auth.RetourerIdentifiantUtilisateur();
                        if (succes)
                        {
                            App.Current.MainPage = new NavigationPage(new Main());
                        }

                    
                }
            }
        }

        private void btnCreerCompte_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NouveauCompte());
        }
    }
}