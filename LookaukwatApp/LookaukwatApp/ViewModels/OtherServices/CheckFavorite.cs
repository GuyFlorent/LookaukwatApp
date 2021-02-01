using LookaukwatApp.Helpers;
using LookaukwatApp.Models.MobileModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LookaukwatApp.ViewModels.OtherServices
{
    public class CheckFavorite
    {
        public static bool IsFabvorite(ProductForMobileViewModel item)
        {
            var Liste = Settings.JsonFavoriteList;

            if (!string.IsNullOrWhiteSpace(Liste))
            {


                List<ProductForMobileViewModel> ListFavorites = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(Liste);

                var checkItem = ListFavorites.FirstOrDefault(model => model.id == item.id);

                if (checkItem == null)
                {
                    ListFavorites.Add(item);
                    Settings.JsonFavoriteList = JsonConvert.SerializeObject(ListFavorites);
                    return true;
                }
                else
                {
                    ListFavorites.Remove(checkItem);
                    Settings.JsonFavoriteList = JsonConvert.SerializeObject(ListFavorites);
                    return false;
                }
            }
            else
            {
                List<ProductForMobileViewModel> ListFavorites = new List<ProductForMobileViewModel>() { item };
                Settings.JsonFavoriteList = JsonConvert.SerializeObject(ListFavorites);
                return true;
            }
        }

        public static bool IsFavoriteForConvert(int ItemId)
        {
            var Liste = Settings.JsonFavoriteList;
            List<ProductForMobileViewModel> ListFavorites = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(Liste);

            var checkItem = ListFavorites.FirstOrDefault(model => model.id == ItemId);
            if(checkItem == null)
            {
                return false;
            }else
            {
                return true;
            }

        }

        public static bool IsNotFavoriteForConvert(int ItemId)
        {
            var Liste = Settings.JsonFavoriteList;
            List<ProductForMobileViewModel> ListFavorites = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(Liste);

            var checkItem = ListFavorites.FirstOrDefault(model => model.id == ItemId);
            if (checkItem == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
