using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace eShopOnContainers.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapsView : ContentPage
	{
		public MapsView ()
		{
            InitializeComponent();          
        }

        protected override void OnAppearing()
        {
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(19.42847, -99.12766), Distance.FromMiles(10)));
            base.OnAppearing();
        }
    }
}