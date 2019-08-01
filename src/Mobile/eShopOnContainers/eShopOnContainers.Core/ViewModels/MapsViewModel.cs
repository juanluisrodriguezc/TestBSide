using eShopOnContainers.Core.Models.Catalog;
using eShopOnContainers.Core.Models.Location;
using eShopOnContainers.Core.Services.Catalog;
using eShopOnContainers.Core.Services.Location;
using eShopOnContainers.Core.Services.Settings;
using eShopOnContainers.Core.Validations;
using eShopOnContainers.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace eShopOnContainers.Core.ViewModels
{
    public class MapsViewModel : ViewModelBase
    {
        private ObservableCollection<LocationBrand> _locationBrands;

        private ISettingsService _settingsService;
        private ILocationService _locationService;

        public ObservableCollection<LocationBrand> LocationBrands
        {
            get { return _locationBrands; }
            set
            {
                _locationBrands = value;
                RaisePropertyChanged(() => LocationBrands);
            }
        }
                
        public ICommand AddMapCommand => new Command(AddMap);

        private void AddMap()
        {
            NavigationService.NavigateToAsync<MapsAddViewModel>();
        }

        public MapsViewModel(ISettingsService settingsService, ILocationService locationService)
        {
            _settingsService = settingsService;
            _locationService = locationService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            LocationBrands = await _locationService.GetCatalogLocationBrandAsync();

            MessagingCenter.Unsubscribe<MapsAddViewModel, LocationBrand>(this, MessageKeys.AddMapBrand);
            MessagingCenter.Subscribe<MapsAddViewModel, LocationBrand>(this, MessageKeys.AddMapBrand, async (sender, arg) =>
            {
                AddMapItemAsync(arg);
            });

            await base.InitializeAsync(navigationData);
        }

        private void AddMapItemAsync(LocationBrand item)
        {
            var exist = from pro in LocationBrands.Where(i => i.Latitude == item.Latitude && i.Longitude == item.Longitude) select pro;

            if (exist.Count() <= 0)
            {
                LocationBrands.Add(item);
            }
        }

    }
}
