using FoodApp.Models;
using FoodApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FoodApp.Viewmodels
{
    public class ItemsByCategoryViewmodel : BaseViewmodel
    {
        #region properties
        public INavigation Navigation;
        private IProductService _productService;

        private Category _parametersReceived;

        public Category ParametersReceived
        {
            get => _parametersReceived;
            set => SetProperty(ref _parametersReceived, value);
        }

        private ObservableCollection<Product> _itemsList;

        public ObservableCollection<Product> ItemsList
        {
            get => _itemsList;
            set => SetProperty(ref _itemsList, value);
        }

        #endregion

        #region constructor
        public ItemsByCategoryViewmodel(INavigation navigation, Dictionary<string, object> parameters)
        {
            Navigation = navigation;
            _productService = new ProductService();
            if (parameters.TryGetValue("CategoryItem", out object category) && category is Category)
            {
                ParametersReceived = (Category)category;
            }
            LoadItems();
            BackProductPageCommand = new Command(ExecuteBackProductPageCommand);
        }
        #endregion

        #region commands
        public ICommand BackProductPageCommand { get; set; }
        #endregion

        #region methods
        private void LoadItems()
        {
            if (ParametersReceived != null)
            {
                var products = _productService.GetProducts();
                var filterProducts = products.Where(p => p.Category.NameCategory == ParametersReceived.NameCategory).ToList();
                ItemsList = new ObservableCollection<Product>(filterProducts);
            }
        }

        private async void ExecuteBackProductPageCommand()
        {
            await Navigation.PopAsync();
        }
        #endregion


    }
}
