using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FoodApp.Resources.Converters
{
    public class ConverterToBgColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = $"{value}";

            if (!string.IsNullOrEmpty(text))
            {
                if (text == "Pizza")
                {
                    return "#FFF2D9";
                }
                else if (text == "Hamburguer")
                {
                    return "#EAF1E4";
                }
                else if (text == "Bolo")
                {
                    return "#FBC3B4";
                }
                else if (text == "Sanduiche")
                {
                    return "#FDE7C2";
                }
                else if (text == "Sushi")
                {
                    return "#FDE9EE";

                }
                else if (text == "Torta")
                {
                    return "#F7D6A8";

                }
                else if (text == "Carne")
                {
                    return "#F0BD91";

                }
                else if (text == "Taco")
                {
                    return "#FBF0C1";

                }
                else if (text == "Sorvete")
                {
                    return "#B9E2F6";

                }
                else if (text == "Panqueca")
                {
                    return "#FED1AB";

                }
                else if (text == "Mariscos")
                {
                    return "#FFBFA0";

                }
                else if (text == "Macarrão")
                {
                    return "#FBEFC9";

                }
                else if (text == "HotDog")
                {
                    return "#FCE7C2";

                }
                else if (text == "Donnuts")
                {
                    return "#D2BAB3";

                }
                else if (text == "Cupcake")
                {
                    return "#CBEEFC";

                }
                else if (text == "Cookie")
                {
                    return "#F7D3B6";

                }
                else if (text == "Cerveja")
                {
                    return "#F3CC9F";

                }
                else if (text == "Caldo")
                {
                    return "#EECCAA";

                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}