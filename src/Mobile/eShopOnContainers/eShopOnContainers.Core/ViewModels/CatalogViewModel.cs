using eShopOnContainers.Core.Models.Catalog;
using eShopOnContainers.Core.Services.Catalog;
using eShopOnContainers.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace eShopOnContainers.Core.ViewModels
{
    public class CatalogViewModel : ViewModelBase
    {
        private ObservableCollection<CatalogItem> _products;
        private ObservableCollection<CatalogBrand> _brands;
        private CatalogBrand _brand;
        private ObservableCollection<CatalogType> _types;
        private CatalogType _type;
        private ICatalogService _productsService;

        public CatalogViewModel(ICatalogService productsService)
        {
            _productsService = productsService;
        }

        public ObservableCollection<CatalogItem> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                RaisePropertyChanged(() => Products);
            }
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
                RaisePropertyChanged(() => IsFilter);
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
                RaisePropertyChanged(() => IsFilter);
            }
        }

        public bool IsFilter { get { return Brand != null || Type != null; } }

        public ICommand FilterCommand => new Command(async () => await FilterAsync());

		public ICommand ClearFilterCommand => new Command(async () => await ClearFilterAsync());

        public ICommand AddCatalogCommand => new Command(async () => await AddCatalogAsync());

        public ICommand GetCatalogDetailsCommand => new Command<CatalogItem>(async (item) => await GetCatalogDetailsAsync(item));

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            // Get Catalog, Brands and Types
            Products = await _productsService.GetCatalogAsync();
            Brands = await _productsService.GetCatalogBrandAsync();
            Types = await _productsService.GetCatalogTypeAsync();

            IsBusy = false;

            MessagingCenter.Unsubscribe<CatalogAddViewModel, CatalogItem>(this, MessageKeys.AddCatalog);
            MessagingCenter.Subscribe<CatalogAddViewModel, CatalogItem>(this, MessageKeys.AddCatalog, async (sender, arg) =>
            {
                AddCatalogItemAsync(arg);
            });

            await base.InitializeAsync(navigationData);
        }

        private void AddCatalogItemAsync(CatalogItem item)
        {
            var exist = from pro in Products.Where(i => i.Id == item.Id) select pro;

            if (exist.Count() <= 0)
            {
                Products.Add(item);
            }
        }

        private async Task FilterAsync()
        {
            if (Brand == null || Type == null)
            {
                return;
            }

            IsBusy = true;

            // Filter catalog products
            MessagingCenter.Send(this, MessageKeys.Filter);
            Products = await _productsService.FilterAsync(Brand.Id, Type.Id);

            IsBusy = false;
        }

        private async Task ClearFilterAsync()
        {
            IsBusy = true;

            Brand = null;
            Type = null;
            Products = await _productsService.GetCatalogAsync();

            IsBusy = false;
        }

        private async Task GetCatalogDetailsAsync(CatalogItem catalog)
        {
            await NavigationService.NavigateToAsync<CatalogDetailsViewModel>(catalog);
        }

        private async Task AddCatalogAsync()
        {            
            int lastId = int.Parse(Products.Max(i => i.Id));            
            await NavigationService.NavigateToAsync<CatalogAddViewModel>(lastId+1);
        }
    }
}