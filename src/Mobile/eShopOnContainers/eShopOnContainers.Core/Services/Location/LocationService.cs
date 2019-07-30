using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using eShopOnContainers.Core.Helpers;
using eShopOnContainers.Core.Models.Location;
using eShopOnContainers.Core.Services.RequestProvider;

namespace eShopOnContainers.Core.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly IRequestProvider _requestProvider;

        private const string ApiUrlBase = "api/v1/l/locations";

        public LocationService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task UpdateUserLocation(eShopOnContainers.Core.Models.Location.Location newLocReq, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayMarketingEndpoint, ApiUrlBase);

            await _requestProvider.PostAsync(uri, newLocReq, token);
        }

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
    }
}