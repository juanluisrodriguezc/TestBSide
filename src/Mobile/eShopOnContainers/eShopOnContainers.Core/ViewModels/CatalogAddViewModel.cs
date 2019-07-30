using eShopOnContainers.Core.Models.Catalog;
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
    public class CatalogAddViewModel : ViewModelBase
    {
        private CatalogItem _catalogItem;
        private ICatalogService _catalogService;
        private ISettingsService _settingsService;

        private ObservableCollection<CatalogBrand> _brands;
        private CatalogBrand _brand;
        private ObservableCollection<CatalogType> _types;
        private CatalogType _type;
        private int _id { get; set; }

        private ValidatableObject<string> _name;
        private ValidatableObject<string> _description;
        private ValidatableObject<string> _price;
        private bool _isValidAll;        

        
        public CatalogItem CatalogItem
        {
            get { return _catalogItem; }
            set { _catalogItem = value; }
        }

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

        public ObservableCollection<CatalogType> Types
        {
            get { return _types; }
            set
            {
                _types = value;
                RaisePropertyChanged(() => Types);
            }
        }

        public CatalogType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged(() => Type);
            }
        }

        public ValidatableObject<string> Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);               
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

        public ValidatableObject<string> Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                RaisePropertyChanged(() => Price);
            }
        }

        public bool IsValidAll
        {
            get { return _isValidAll; }
            set { _isValidAll = value;
                RaisePropertyChanged(() => IsValidAll);
            }
        }
      
        public ICommand ValidateNameCommand => new Command(() => ValidateName());

        public ICommand ValidateDescriptionCommand => new Command(() => ValidateDescription());

        public ICommand ValidatePriceCommand => new Command(() => ValidatePrice());

        public ICommand AddCatalogCommand => new Command(() => AddCatalog());

        public CatalogAddViewModel(ISettingsService settingsService, ICatalogService catalogService)
        {
            _settingsService = settingsService;
            _catalogService = catalogService;

            _catalogItem = new CatalogItem()
            {
                PictureUri = Device.RuntimePlatform != Device.UWP ? "noimage.png" : "Assets/noimage.png",
            };

            _name = new ValidatableObject<string>();
            _name.IsValid = false;
            _description = new ValidatableObject<string>();
            _description.IsValid = false;
            _price = new ValidatableObject<string>();
            _price.IsValid = false;

            _isValidAll = false;
            
            AddValidations();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            _catalogItem.Id = ((int)navigationData).ToString();
            await LoadCatalogs();
            
        }

        private async Task LoadCatalogs()
        {            
            Brands = await _catalogService.GetCatalogBrandAsync();
            Brand = Brands[0];
            Types = await _catalogService.GetCatalogTypeAsync();
            Type = Types[0];
        }

        private void Validate()
        {            
            IsValidAll = Name.IsValid && Description.IsValid & Price.IsValid;
        }

        private bool ValidateName()
        {
            _name.Validate();
            Validate();
            return _name.Validate();
        }

        private bool ValidateDescription()
        {
            _description.Validate();
            Validate();
            return _description.Validate();
        }

        private bool ValidatePrice()
        {
            _price.Validate();
            Validate();
            return _price.Validate();
        }

        private void AddValidations()
        {
            _name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });
            _description.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A description is required." });
            _price.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A price is required." });
            _price.Validations.Add(new IsDecimalRule<string> { ValidationMessage = "The price needs to be a decimal type value" });
        }

        private void AddCatalog()
        {
            if (_isValidAll)
            {
                _catalogItem.Name = Name.Value;
                _catalogItem.Description = Description.Value;
                _catalogItem.Price = decimal.Parse(Price.Value);
                _catalogItem.CatalogBrandId = Brand.Id;
                _catalogItem.CatalogBrand = Brand.Brand;
                _catalogItem.CatalogTypeId = Type.Id;
                _catalogItem.CatalogType = Type.Type;

                // Add new item to Catalog
                MessagingCenter.Send(this, MessageKeys.AddCatalog, _catalogItem);

                DialogService.ShowAlertAsync("Added to the Catalog", "Catalog", "OK");

                //Redirect to MainPage
                NavigationService.NavigateToAsync<MainViewModel>();
            }
        }
    }
}
