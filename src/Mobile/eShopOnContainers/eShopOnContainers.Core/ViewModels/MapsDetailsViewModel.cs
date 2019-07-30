using eShopOnContainers.Core.Models.Location;
using eShopOnContainers.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace eShopOnContainers.Core.ViewModels
{
    public class MapsDetailsViewModel : ViewModelBase
    {
        private LocationBrand _locationBrand;

        public LocationBrand LocationBrand
        {
            get
            {
                return _locationBrand;
            }
            set
            {
                _locationBrand = value;
                RaisePropertyChanged(() => LocationBrand);
            }
        }

        public MapsDetailsViewModel()
        {
            _locationBrand = new LocationBrand();
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is LocationBrand)
            {
                _locationBrand = ((LocationBrand)navigationData);
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
