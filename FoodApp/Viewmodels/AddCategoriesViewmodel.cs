using FoodApp.Models;
using FoodApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FoodApp.Viewmodels
{
    class AddCategoriesViewmodel : BaseViewmodel
    {
        #region properties
        public INavigation Navigation { get; }
        private readonly IProductService _productService;
        private ObservableCollection<Category> _categoriesList;
        public ObservableCollection<Category> CategoriesList
        {
            get => _categoriesList;
            set => SetProperty(ref _categoriesList, value);
        }

        private Category _categorySave;
        public Category CategorySave
        {
            get => _categorySave;
            set => SetProperty(ref _categorySave, value);
        }
        #endregion

        #region constructor
        public AddCategoriesViewmodel(INavigation navigation)
        {
            Navigation = navigation;
            _productService = new ProductService();
            CategoryList();
            BackProductPageCommand = new Command(ExecuteBackProductPageCommand);
            SaveCategoryCommand = new Command(ExecuteSaveCategoryCommand);
        }

        #endregion

        #region commands
        public ICommand BackProductPageCommand { get; set; }
        public ICommand SaveCategoryCommand { get; set; }
        #endregion

        #region methods
        private void CategoryList()
        {
            CategoriesList = new ObservableCollection<Category>
            {

                new Category()
                {
                    NameCategory = "Pizza",
                    Image = "pizza.png"
                },
                new Category()
                {
                    NameCategory = "Hamburguer",
                    Image = "hamburguer.png"
                },
                new Category()
                {
                    NameCategory = "Bolo",
                    Image = "bolo.png"
                },
                new Category()
                {
                    NameCategory = "Sanduiche",
                    Image = "sanduiche.png"
                },
                new Category()
                {
                    NameCategory = "Sushi",
                    Image = "sushi.png"
                },
                new Category()
                {
                    NameCategory = "Torta",
                    Image = "torta.png"
                },
                new Category()
                {
                    NameCategory = "Carne",
                    Image = "carne.png"
                },
                new Category()
                {
                    NameCategory = "Taco",
                    Image = "taco.png"
                },
                new Category()
                {
                    NameCategory = "Sorvete",
                    Image = "sorvete.png"
                },
                new Category()
                {
                    NameCategory = "Panqueca",
                    Image = "panqueca.png"
                },
                new Category()
                {
                    NameCategory = "Mariscos",
                    Image = "mariscos.png"
                },
                new Category()
                {
                    NameCategory = "Macarrão",
                    Image = "macarrao.png"
                },
                new Category()
                {
                    NameCategory = "HotDog",
                    Image = "hotdog.png"
                },
                new Category()
                {
                    NameCategory = "Donnuts",
                    Image = "donnuts.png"
                },
                new Category()
                {
                    NameCategory = "Cupcake",
                    Image = "cupcake.png"
                },
                new Category()
                {
                    NameCategory = "Cookie",
                    Image = "cookie.png"
                },
                new Category()
                {
                    NameCategory = "Cerveja",
                    Image = "cerveja.png"
                },
                new Category()
                {
                    NameCategory = "Caldo",
                    Image = "caldo.png"
                }
            };
        }

        private async void ExecuteSaveCategoryCommand()
        {
            try
            {
                bool categoryExists = _productService.CategoryExists(CategorySave.NameCategory);

                if (categoryExists)
                {
                    await Shell.Current.DisplayAlert("Categoria Existente", "Essa categoria ja foi criada.", "Ok");
                }
                else
                {
                    Category category = new Category()
                    {
                        NameCategory = CategorySave.NameCategory,
                        Image = CategorySave.Image
                    };

                    _productService.AddCategory(category);
                    _productService.UpdateCategory(category);

                    CategorySave = null;

                    ExecuteBackProductPageCommand();
                }
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Opção invalida", "Selecione uma categoria", "Ok");
            }

        }

        private async void ExecuteBackProductPageCommand()
        {
            await Navigation.PopAsync(true);
        }
        #endregion
    }
}