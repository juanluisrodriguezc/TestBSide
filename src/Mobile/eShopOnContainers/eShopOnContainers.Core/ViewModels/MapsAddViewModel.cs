using eShopOnContainers.Core.Models.Catalog;
using eShopOnContainers.Core.Models.Location;
using eShopOnContainers.Core.Services.Catalog;
using eShopOnContainers.Core.Services.Settings;
using eShopOnContainers.Core.Validations;
using eShopOnContainers.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace eShopOnContainers.Core.ViewModels
{
    class MapsAddViewModel : ViewModelBase
    {
        private ICatalogService _catalogService;
        private ISettingsService _settingsService;

        private ObservableCollection<CatalogBrand> _brands;
        private CatalogBrand _brand;

        private ValidatableObject<string> _latitude;
        private ValidatableObject<string> _longitude;
        private ValidatableObject<string> _description;

        private bool _isValidAll;

        public ObservableCollection<CatalogBrand> Brands
        {
            get { return _brands; }
            set
            {
                _brands = value;
                RaisePropertyChanged(() => Brands);
            }
        }

        public CatalogBrand Brand
        {
            get { return _brand; }
            set
            {
                _brand = value;
                RaisePropertyChanged(() => Brand);
            }
        }

        public ValidatableObject<string> Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                RaisePropertyChanged(() => Latitude);
            }
        }

        public ValidatableObject<string> Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                RaisePropertyChanged(() => Longitude);
            }
        }

        public ValidatableObject<string> Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public bool IsValidAll
        {
            get { return _isValidAll; }
            set
            {
                _isValidAll = value;
                RaisePropertyChanged(() => IsValidAll);
            }
        }

        public ICommand ValidateLatitudeCommand => new Command(() => ValidateLatitude());

        public ICommand ValidateLongitudeCommand => new Command(() => ValidateLongitude());

        public ICommand ValidateDescriptionCommand => new Command(() => ValidateDescription());

        public ICommand AddMapsCommand => new Command(() => AddMap());

        public MapsAddViewModel(ISettingsService settingsService, ICatalogService catalogService)
        {
            _settingsService = settingsService;
            _catalogService = catalogService;

            _latitude = new ValidatableObject<string>();
            _latitude.IsValid = false;
            _longitude = new ValidatableObject<string>();
            _longitude.IsValid = false;
            _description = new ValidatableObject<string>();
            _description.IsValid = false;

            _isValidAll = false;

            AddValidations();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            Brands = await _catalogService.GetCatalogBrandAsync();
            Brand = Brands[0];
        }

        private void Validate()
        {
            IsValidAll = Latitude.IsValid && Longitude.IsValid & Description.IsValid;
        }

        private bool ValidateLatitude()
        {
            _latitude.Validate();
            Validate();
            return _latitude.Validate();
        }

        private bool ValidateLongitude()
        {
            _longitude.Validate();
            Validate();
            return _longitude.Validate();
        }

        private bool ValidateDescription()
        {
            _description.Validate();
            Validate();
            return _description.Validate();
        }

        private void AddValidations()
        {
            _latitude.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A latitude is required." });
            _latitude.Validations.Add(new IsDecimalRule<string> { ValidationMessage = "The latitude needs to be a decimal type value" });

            _longitude.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A longitude is required." });
            _longitude.Validations.Add(new IsDecimalRule<string> { ValidationMessage = "The longitude needs to be a decimal type value" });

            _description.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A description is required." });
        }

        private void AddMap()
        {
            if (_isValidAll)
            {
                LocationBrand locationBrand = new LocationBrand
                {
                    Latitude = double.Parse(Latitude.Value),
                    Longitude = double.Parse(Longitude.Value),
                    IdBrand = Brand.Id,
                    Brand = Brand.Brand,
                    Description = Description.Value
                };

                MessagingCenter.Send(this, MessageKeys.AddMapBrand, locationBrand);

                DialogService.ShowAlertAsync("Map Added", "Maps", "OK");

                //Redirect to Maps
                NavigationService.NavigateToAsync<MainViewModel>();
            }
        }
    }
}
