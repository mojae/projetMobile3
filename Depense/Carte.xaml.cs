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

            if (statut == PermissionStatus.Granted)
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
            using (var conn = new SQLiteConnection(App.CheminBD))
            {
                var lieux = conn.Table<MonLieu>().ToList().Where(l => l.UtilisateurId == Auth.RetourerIdentifiantUtilisateur()).ToList();
                var lieuxConnus = lieux.Where(l => l.Categorie == "Connus").ToList();
                var lieuxDesires = lieux.Where(l => l.Categorie == "Désirés").ToList();
                var lieuxVisites = lieux.Where(l => l.Categorie == "Visités").ToList();

                var config = conn.Table<EntConfiguration>().ToList().FirstOrDefault(c => c.UtilisateurId == Auth.RetourerIdentifiantUtilisateur());
                if(config != null)
                {    
                        if (config.Switch1 == false && config.Switch2 == false && config.Switch3 == true && lieuxVisites != null)
                        {
                            carteLocalisation.Pins.Clear();
                            AjouterPins(lieuxVisites);
                        }

                        if (config.Switch1 == false && config.Switch2 == true && config.Switch3 == false && lieuxDesires != null)
                        {
                            carteLocalisation.Pins.Clear();
                            AjouterPins(lieuxDesires);
                        }

                        if (config.Switch1 == false && config.Switch2 == true && config.Switch3 == true && lieuxVisites != null && lieuxDesires != null)
                        {
                            carteLocalisation.Pins.Clear();
                            AjouterPins(lieuxDesires);
                            AjouterPins(lieuxVisites);
                        }
                        if (config.Switch1 == true && config.Switch2 == false && config.Switch3 == false && lieuxConnus != null)
                        {
                            carteLocalisation.Pins.Clear();
                            AjouterPins(lieuxConnus);
                        }
                        if (config.Switch1 == true && config.Switch2 == false && config.Switch3 == true && lieuxConnus != null && lieuxVisites != null)
                        {
                            carteLocalisation.Pins.Clear();
                            AjouterPins(lieuxConnus);
                            AjouterPins(lieuxVisites);
                        }
                        if (config.Switch1 == true && config.Switch2 == true && config.Switch3 == false && lieuxConnus != null && lieuxDesires != null)
                        {
                            carteLocalisation.Pins.Clear();
                            AjouterPins(lieuxConnus);
                            AjouterPins(lieuxDesires);
                        }

                        if (config.Switch1 == true && config.Switch2 == true && config.Switch3 == true && lieux != null)
                        {
                            carteLocalisation.Pins.Clear();
                            AjouterPins(lieux);
               
                        }
                        if (config.Switch1 == false && config.Switch2 == false && config.Switch3 == false)
                        {
                            carteLocalisation.Pins.Clear();
                   
                        }



                }
            

            }
        }

        private void AjouterPins(List<MonLieu>lieux)
        {
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
                    catch (Exception ex) { }
                }

        }

       
    }
}