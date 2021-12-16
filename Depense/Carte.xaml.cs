using Depense.Helper;
using Depense.Model;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Depense
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carte : ContentPage
    {

        IGeolocator locator = CrossGeolocator.Current;
        public Carte()
        {
            InitializeComponent();

            ObtenirLocalisation();

            locator.PositionChanged += Locator_PositionChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DemarrerLocalisation();
            ObtenirLieux();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ArreterLocalisation();
        }

        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            CentrerCarte(e.Position.Latitude, e.Position.Longitude);
        }

        private async void ObtenirLocalisation()
        {
            var statut = await App.ValiderEtDemanderLocalisation();

            if(statut == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                CentrerCarte(location.Latitude, location.Longitude);
            }
        }

        private void CentrerCarte(double latitude, double longitude)
        {
            var centre = new Xamarin.Forms.Maps.Position(latitude, longitude);

            var span = new MapSpan(centre, 0.01, 0.01);

            carteLocalisation.MoveToRegion(span);
        }

        private async void DemarrerLocalisation()
        {
            await locator.StartListeningAsync(new TimeSpan(0, 0, 1), 100);
        }

        private async void ArreterLocalisation() {
            await locator.StopListeningAsync();
        }

        private void ObtenirLieux()
        {
            using(var conn = new SQLiteConnection(App.CheminBD))
            {
                var lieux = conn.Table<MonLieu>().ToList().Where(l => l.UtilisateurId == Auth.RetourerIdentifiantUtilisateur());
                foreach (var lieu in lieux)
                {
                    try
                    {
                        var positionPin = new Xamarin.Forms.Maps.Position(lieu.Latitude, lieu.Longitude);
                        var pin = new Pin()
                        {
                            Position = positionPin,
                            Label = lieu.Nom,
                            Address = lieu.Adresse,
                            Type = PinType.SavedPin
                        };

                        carteLocalisation.Pins.Add(pin);
                    }
                    catch(Exception ex) { }
                }
            }
        }
    }
}