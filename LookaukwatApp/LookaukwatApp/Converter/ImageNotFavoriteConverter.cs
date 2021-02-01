using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace LookaukwatApp.Converter
{
    public  class ImageNotFavoriteConverter : IValueConverter
    {
        /// <summary>
        /// Converts a given path of a EmbeddedResource image to a <see cref="ImageSource"/>.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
         
            var ItemId = System.Convert.ToInt32(value);
            var Liste = Settings.JsonFavoriteList;

            if (!string.IsNullOrWhiteSpace(Liste))
            {
                List<ProductForMobileViewModel> ListFavorites = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(Liste);
                if (ListFavorites.Count > 0)
                {
                    var item = ListFavorites.FirstOrDefault(model => model.id == ItemId);
                    if (item == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
