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
            Latitude = 19.3624289,
            Longitude = -99.1919174,
            IdBrand = 1,
            Brand = "Azure",
            Description = "Calle Pirul 607, Los Alpes, 01010 Ciudad de México, CDMX",
            },

            new LocationBrand {
            Latitude = 19.3970497,
            Longitude = -99.1863099,
            IdBrand = 1,
            Brand = "Azure",
            Description = "Av. Revolución 333, Tacubaya, 11870 Ciudad de México, CDMX",
            },

            new LocationBrand {
            Latitude = 19.4284772,
            Longitude = -99.1298554,
            IdBrand = 1,
            Brand = "Azure",
            Description = "Calle de Venustiano Carranza 119, 06000 Ciudad de México, CDMX",
            },

            new LocationBrand {
            Latitude = 19.4284772,
            Longitude = -99.1298554,
            IdBrand = 2,
            Brand = "Visual Studio",
            Description = "Calle Pradera 24, La Merced, Zona Centro, 15100 Ciudad de México, CDMX",
            },

            new LocationBrand {
            Latitude = 19.393669,
            Longitude = -99.1767865,
            IdBrand = 2,
            Brand = "Visual Studio",
            Description = "Montecito 38, Nápoles, 03810 Ciudad de México, CDMX",
            },

            new LocationBrand {
            Latitude = 19.3938226,
            Longitude = -99.2446396,
            IdBrand = 2,
            Brand = "Visual Studio",
            Description = "Av Canal de Tezontle 1520, Dr Alfonso Ortiz Tirado, 09020 Ciudad de México, CDMX",
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
