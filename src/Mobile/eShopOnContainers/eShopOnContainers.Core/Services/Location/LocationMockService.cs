using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using eShopOnContainers.Core.Models.Location;

namespace eShopOnContainers.Core.Services.Location
{
    public class LocationMockService : ILocationService
    {
        private ObservableCollection<LocationBrand> MockLocationBrands = new ObservableCollection<LocationBrand>
        {
            new LocationBrand {
            Latitude = 19.42847,
            Longitude = -99.12766,
            IdBrand = 1,
            Brand = "Azure",
            ColorPin = "#FF0000"
            }
        };

        public async Task<ObservableCollection<LocationBrand>> GetCatalogLocationBrandAsync()
        {
            await Task.Delay(10);
            return MockLocationBrands;
        }

        public Task UpdateUserLocation(Models.Location.Location newLocReq, string token)
        {
            throw new NotImplementedException();
        }
    }
}
