using eShopOnContainers.Core.Models.Catalog;
using eShopOnContainers.Core.Services.Catalog;
using eShopOnContainers.Core.Services.Settings;
using eShopOnContainers.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace eShopOnContainers.Core.ViewModels
{
    public class CatalogDetailsViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ICatalogService _catalogService;

        private CatalogItem _catalog;
        private bool _isDetailsSite;

        public ICommand AddCatalogItemCommand => new Command(AddCatalogItem);

        public ICommand EnableDetailsSiteCommand => new Command(EnableDetailsSite);

        public CatalogDetailsViewModel(ISettingsService settingsService, ICatalogService catalogService)
        {
            _settingsService = settingsService;
            _catalogService = catalogService;
        }

        public CatalogItem Catalog
        {
            get => _catalog;
            set
            {
                _catalog = value;
                RaisePropertyChanged(() => Catalog);
            }
        }

        public bool IsDetailsSite
        {
            get => _isDetailsSite;
            set
            {
                _isDetailsSite = value;
                RaisePropertyChanged(() => IsDetailsSite);
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is CatalogItem)
            {
                IsBusy = true;
                // Get catalog by item
                Catalog = await _catalogService.GetCatalogByItem((CatalogItem)navigationData);
                IsBusy = false;
            }
        }

        private void AddCatalogItem()
        {
            // Add new item to Basket
            MessagingCenter.Send(this, MessageKeys.AddProduct, _catalog);

            //Redirect to MainPage
            NavigationService.NavigateToAsync<MainViewModel>();
        }

        private void EnableDetailsSite()
        {
            IsDetailsSite = true;
        }
    }
}
