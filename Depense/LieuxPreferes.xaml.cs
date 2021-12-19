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
    public partial class LieuxPreferes : ContentPage
    {
        public LieuxPreferes()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var conn = new SQLiteConnection(App.CheminBD))
            {
                ListeDesLieux.ItemsSource = null;
                ListeDesLieux.ItemsSource = conn.Table<MonLieu>().ToList().Where(l => l.UtilisateurId == Auth.RetourerIdentifiantUtilisateur() );
            }

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NouveauLieu());
        }

      

        private void btnSModifier_Clicked(object sender, EventArgs e)
        {
            MonLieu lieuSelectione = ListeDesLieux.SelectedItem as MonLieu;
            if (lieuSelectione != null)
            {
                 Navigation.PushAsync(new NouveauLieu(lieuSelectione));
            }
            
        }

        private void btnVoir_Clicked(object sender, EventArgs e)
        {
            MonLieu lieuSelectione = ListeDesLieux.SelectedItem as MonLieu;
            if (lieuSelectione != null)
            {
                Navigation.PushAsync(new Carte(lieuSelectione));
            }

        }
    }
}