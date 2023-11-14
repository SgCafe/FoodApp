﻿using FoodApp.Models;
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
    public class AddItemsViewmodel : BaseViewmodel
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

        private string _nameSave;
        public string NameSave
        {
            get => _nameSave;
            set => SetProperty(ref _nameSave, value);
        }

        private double? _priceSave;
        public double? PriceSave
        {
            get => _priceSave;
            set => SetProperty(ref _priceSave, value);
        }

        private Category _categorySave;
        public Category CategorySave
        {
            get => _categorySave;
            set => SetProperty(ref _categorySave, value);
        }

        #endregion

        #region constructor
        public AddItemsViewmodel(INavigation navigation)
        {
            _productService = new ProductService();
            Navigation = navigation;
            CategoryList();
            BackProductPageCommand = new Command(ExecuteBackProductPageCommand);
            SaveItemsCommand = new Command(ExecuteSaveItemsCommand);
        }

        #endregion

        #region commands
        public ICommand BackProductPageCommand { get; set; }
        public ICommand SaveItemsCommand { get; set; }
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

        private async void ExecuteSaveItemsCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(NameSave) || PriceSave == null || CategorySave.NameCategory == null)
                {
                    await Shell.Current.DisplayAlert("Campos vazios", "Por favor, preencha todos os campos antes de salvar.", "Ok");
                    return;
                }


                var products = _productService.GetProducts();
                var filterProductsEquals = products.Where(p => p.Name == NameSave && p.Category.NameCategory == CategorySave.NameCategory && p.Price == PriceSave);

                if (filterProductsEquals.Any())
                {
                    await Shell.Current.DisplayAlert("Item Existente", "Altere algum dado para salvar", "Ok");
                }
                else
                {

                    Product productItem = new Product
                    {
                        Name = NameSave,
                        Price = PriceSave,
                    };

                    Category CategoryItem;
                    if (_productService.CategoryExists(CategorySave.NameCategory))
                    {
                        CategoryItem = _productService.GetCategories().FirstOrDefault(c => c.NameCategory == CategorySave.NameCategory);
                    }
                    else
                    {
                        CategoryItem = new Category
                        {
                            NameCategory = CategorySave.NameCategory,
                            Image = CategorySave.Image
                        };
                        _productService.AddCategory(CategoryItem);
                    }

                    productItem.CategoryId = CategoryItem.Id;
                    productItem.Category = CategoryItem;
                    _productService.AddProduct(productItem);

                    _productService.UpdateItems(productItem);
                    _productService.UpdateCategory(CategoryItem);

                    NameSave = string.Empty;
                    PriceSave = null;
                    CategorySave = null;

                    ExecuteBackProductPageCommand();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void ExecuteBackProductPageCommand()
        {
            await Navigation.PopAsync(true);
        }


        #endregion
    }
}