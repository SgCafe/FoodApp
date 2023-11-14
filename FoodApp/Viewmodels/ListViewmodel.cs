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
    public class ListViewmodel : BaseViewmodel
    {
        #region properties
        public INavigation Navigation { get; }

        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
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

        private readonly IProductService _productService;

        private ObservableCollection<Product> _getCategory;
        public ObservableCollection<Product> GetCategory
        {
            get => _getCategory;
            set => SetProperty(ref _getCategory, value);
        }

        private Product _categorySelected;

        public Product CategorySelected
        {
            get => _categorySelected;
            set => SetProperty(ref _categorySelected, value);
        }


        #endregion

        #region constructor
        public ListViewmodel(INavigation navigation)
        {
            _productService = new ProductService();
            Navigation = navigation;
            ListCategoriesPage();
            GoToAddCategoriesCommand = new Command(ExecuteGoToAddCategoriesCommand);
            GetAllCategoriesCommand = new Command(ExecuteGetAllCategoriesCommand);
        }


        #endregion

        #region commands
        public ICommand GoToAddCategoriesCommand { get; set; }
        public ICommand GetAllCategoriesCommand { get; set; }
        #endregion

        #region methods
        private void ListCategoriesPage()
        {
            var categories = _productService.GetCategories();
            Categories = new ObservableCollection<Category>(categories);
        }

        public void ExecuteGetAllCategoriesCommand()
        {
            var categorieslist = _productService.GetCategories();
            Categories.Clear();
            foreach (var category in categorieslist)
            {
                Categories.Add(category);
            }
        }
        private async void ExecuteGoToAddCategoriesCommand()
        {
            await Navigation.PushAsync(new AddCategoriesPage());
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
