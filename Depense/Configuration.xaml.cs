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
    public partial class Configuration : ContentPage
    {
        private EntConfiguration _configuration;
        public Configuration()
        {
            InitializeComponent();
        }
        public Configuration(EntConfiguration configuration)
        {
            InitializeComponent();
            _configuration = configuration;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var conn = new SQLiteConnection(App.CheminBD))
            {
                var config = conn.Table<EntConfiguration>().ToList().FirstOrDefault(l => l.UtilisateurId == Auth.RetourerIdentifiantUtilisateur());

                if (config != null)
                {
                    switch1.IsToggled = config.Switch1;
                    switch2.IsToggled = config.Switch2;
                    switch3.IsToggled = config.Switch3;
                    LatitudeDegreEntry.Text = config.DegreLatitude.ToString();
                    LongitudeDegreEntry.Text = config.DegreLongitude.ToString();
                }
                else
                {
                    decimal degreLatitude = 0;
                    decimal.TryParse(LatitudeDegreEntry.Text, out degreLatitude);
                    decimal degreLongitude = 0;
                    decimal.TryParse(LatitudeDegreEntry.Text, out degreLongitude);

                    var nouvelleConfig = new EntConfiguration()
                    {
                        UtilisateurId = Auth.RetourerIdentifiantUtilisateur()            ,
                        Switch1 = switch1.IsToggled,
                        Switch2 = switch2.IsToggled,
                        Switch3 = switch3.IsToggled,
                        DegreLatitude = degreLatitude,
                        DegreLongitude = degreLongitude
                    };

                    conn.Insert(nouvelleConfig);

                }

            }

        }

        private void switch1_Toggled(object sender, ToggledEventArgs e)
        {
            //using (var conn = new SQLiteConnection(App.CheminBD))
            //{
            //    var config = conn.Table<EntConfiguration>().ToList().FirstOrDefault(l => l.UtilisateurId == Auth.RetourerIdentifiantUtilisateur());

            //    config.Switch1 = switch1.IsToggled;
            //    conn.Update(config);
            //}
        }

        private void switch2_Toggled(object sender, ToggledEventArgs e)
        {
            //using (var conn = new SQLiteConnection(App.CheminBD))
            //{
            //    var config = conn.Table<EntConfiguration>().ToList().FirstOrDefault(l => l.UtilisateurId == Auth.RetourerIdentifiantUtilisateur());

            //    config.Switch2 = switch2.IsToggled;
            //    conn.Update(config);
            //}

        }

        private void switch3_Toggled(object sender, ToggledEventArgs e)
        {
            //using (var conn = new SQLiteConnection(App.CheminBD))
            //{
            //    var config = conn.Table<EntConfiguration>().ToList().FirstOrDefault(l => l.UtilisateurId == Auth.RetourerIdentifiantUtilisateur());

            //    config.Switch2 = switch2.IsToggled;
            //    conn.Update(config);
            //}


        }

        private void LatitudeDegreEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LongitudeDegreEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}