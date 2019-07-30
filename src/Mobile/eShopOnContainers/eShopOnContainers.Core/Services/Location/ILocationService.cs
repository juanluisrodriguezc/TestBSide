using eShopOnContainers.Core.Models.Location;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace eShopOnContainers.Core.Services.Location
{    
    public interface ILocationService
    {
        Task UpdateUserLocation(eShopOnContainers.Core.Models.Location.Location newLocReq, string token);
        Task<ObservableCollection<LocationBrand>> GetCatalogLocationBrandAsync();
    }
}