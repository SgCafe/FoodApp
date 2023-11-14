using FoodApp.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsByCategoryPage : ContentPage
	{
        public ItemsByCategoryPage(Dictionary<string, object> parameters)
        {
            InitializeComponent();
            BindingContext = new ItemsByCategoryViewmodel(Navigation, parameters);
        }
    }
}