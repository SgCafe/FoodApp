using FoodApp.Models;
using FoodApp.Services;
using FoodApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FoodApp.Viewmodels
{
    public class ProductViewmodel : BaseViewmodel
    {
        #region properties
        private readonly IProductService _productService;

        private ObservableCollection<Category> _categoriesList;
        public ObservableCollection<Category> CategoriesList
        {
            get => _categoriesList;
            set => SetProperty(ref _categoriesList, value);
        }

        private ObservableCollection<Product> _itemsList;
        public ObservableCollection<Product> ItemsList
        {
            get => _itemsList;
            set => SetProperty(ref _itemsList, value);
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (SetProperty(ref _selectedProduct, value) && value != null)
                {
                    NavigateToDetailPage(value);
                    SelectedProduct = null;
                }
            }
        }

        private Category _selectCategory;

        public Category SelectCategory
        {
            get => _selectCategory;
            set
            {
                if (SetProperty(ref _selectCategory, value) && value != null)
                {
                    NavigationByCategoryPage(value);
                    SelectCategory = null;
                }
            }
        }


        public INavigation Navigation { get; }
        #endregion

        #region constructor
        public ProductViewmodel(INavigation navigation)
        {
            Navigation = navigation;
            _productService = new ProductService();
            LoadCategories();
            LoadItems();
            GoToAddCategoriesCommand = new Command(ExecuteGoToAddCategoriesCommand);
            GoToAddItemsCommand = new Command(ExecuteGoToAddItemsCommand);
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            LoadCategoryCommand = new Command(ExecuteLoadCategoriesCommand);
        }


        #endregion

        #region commands
        public ICommand GoToAddCategoriesCommand { get; set; }
        public ICommand GoToAddItemsCommand { get; set; }
        public ICommand LoadItemsCommand { get; }
        public ICommand LoadCategoryCommand { get; }
        #endregion

        #region methods
        private void LoadCategories()
        {
            var categories = _productService.GetCategories();
            CategoriesList = new ObservableCollection<Category>(categories);
        }

        private void LoadItems()
        {
            var items = _productService.GetProducts();
            ItemsList = new ObservableCollection<Product>(items);
        }

        public void ExecuteLoadItemsCommand()
        {
            var items = _productService.GetProducts();
            ItemsList.Clear();

            foreach (var item in items)
            {
                ItemsList.Add(item);
            }
        }

        public void ExecuteLoadCategoriesCommand()
        {
            var categories = _productService.GetCategories();
            CategoriesList.Clear();
            foreach (var category in categories)
            {
                CategoriesList.Add(category);
            }
        }


        private async void ExecuteGoToAddCategoriesCommand()
        {
            await Navigation.PushAsync(new AddCategoriesPage());
        }

        private async void ExecuteGoToAddItemsCommand()
        {
            await Navigation.PushAsync(new AddItemsPage());
        }

        private async Task NavigateToDetailPage(Product product)
        {
            var parameters = new Dictionary<string, object>
            {
                {"Product", product }
            };

            EditProductPage editPage = new EditProductPage(parameters);
            await Navigation.PushAsync(editPage);
        }

        private async Task NavigationByCategoryPage(Category category)
        {
            var parameters = new Dictionary<string, object>
            {
                {"CategoryItem", category}
            };

            ItemsByCategoryPage itemsByCat = new ItemsByCategoryPage(parameters);
            await Navigation.PushAsync(itemsByCat);
        }
        #endregion

    }
}
