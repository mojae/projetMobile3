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

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NouveauLieu());
        }

        private void btnEnregistrer_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSModifier_Clicked(object sender, EventArgs e)
        {

        }
    }
}