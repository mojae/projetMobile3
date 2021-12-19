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
    public partial class NouveauLieu : ContentPage
    {
        private MonLieu _lieu;
        private static List<String> listeCategories = new List<string> {"Connus","Désirés","Visités" };
        public NouveauLieu()
        {
            InitializeComponent();
        }

        public NouveauLieu(MonLieu lieu)
        {
            _lieu = lieu;
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var conn = new SQLiteConnection(App.CheminBD))
            {
                

                pickCategorie.ItemsSource = listeCategories;
                

                //Remplir les champs avec les valeurs de la dépense à modifier
                if (_lieu != null)
                {
                    NomEntry.Text = _lieu.Nom ;
                    AdresseEntry.Text = _lieu.Adresse;
                    pickCategorie.SelectedItem = _lieu.Categorie;
                    LatitudeEntry.Text = _lieu.Latitude.ToString();
                    LongitudeEntry.Text = _lieu.Longitude.ToString();
                }
            }
        }


        private void btnEnregistrer_Clicked(object sender, EventArgs e)
        {
            var nom = NomEntry.Text;
            var adresse = AdresseEntry.Text;
            var categorie = pickCategorie.SelectedItem.ToString();
            double latitude = 0;
            double.TryParse(LatitudeEntry.Text, out latitude);
            double longitude = 0;
            double.TryParse(LongitudeEntry.Text, out longitude);
         
            

            if (string.IsNullOrEmpty(nom))
            {
                DisplayAlert("Alert", "Veuillez saisir un nom", "Fermer");
                return;
            }

            if (string.IsNullOrEmpty(LatitudeEntry.Text))
            {
                DisplayAlert("Alert", "Veuillez saisir une Latitude", "Fermer");
                return;
            }

            if (string.IsNullOrEmpty(LongitudeEntry.Text))
            {
                DisplayAlert("Alert", "Veuillez saisir une Longitude", "Fermer");
                return;
            }

            if (string.IsNullOrEmpty(adresse))
            {
                DisplayAlert("Alert", "Veuillez saisir une adresse", "Fermer");
                return;
            }

            if (categorie == null)
            {
                DisplayAlert("Alert", "Veuillez saisir une catégorie", "Fermer");
                return;
            }

            if (_lieu == null)
            {
                using (var conn = new SQLiteConnection(App.CheminBD))
                {
                    var nouveauLieu = new MonLieu()
                    {
                        UtilisateurId = Auth.RetourerIdentifiantUtilisateur(),
                        Nom = nom,
                        Adresse = adresse,
                        Categorie = categorie,
                        Latitude =latitude,
                        Longitude =longitude

                       
                    };

                   
                    conn.Insert(nouveauLieu);
                }
            }
            else
            {
                using (var conn = new SQLiteConnection(App.CheminBD))
                {
                    var lieu = conn.Table<MonLieu>().ToList().FirstOrDefault(x => x.Id == _lieu.Id);
                    if (lieu != null)
                    {
                        lieu.Nom = nom;
                        lieu.Adresse = adresse;
                        lieu.Categorie = categorie;
                        lieu.Latitude = latitude;
                        lieu.Longitude = longitude;
                      
                    }

                    conn.Update(lieu);
                }
            }

            DisplayAlert("Message", "Le Lieu a été enregistrée avec succès", "Fermer");
            Navigation.PopAsync();

        }
    }
}