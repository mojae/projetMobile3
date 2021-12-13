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
        private List<String> listeCategories = new List<string> {"Prefereés","Souhaités","Visités" };
        public NouveauLieu()
        {
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
                }
            }
        }


        private void btnEnregistrer_Clicked(object sender, EventArgs e)
        {

        }
    }
}