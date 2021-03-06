﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string Reviews_GooglePlay
        {
            get
            {
                return AppSettings.GetValueOrDefault("Reviews_GooglePlay", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Reviews_GooglePlay", value);
            }
        }
        public static string IdItem_For_Image
        {
            get
            {
                return AppSettings.GetValueOrDefault("IdItem_For_Image", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("IdItem_For_Image", value);
            }
        }
        public static string Products
        {
            get
            {
                return AppSettings.GetValueOrDefault("Products", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Products", value);
            }
        }

        public static string AddressDelivered
        {
            get
            {
                return AppSettings.GetValueOrDefault("AddressDelivered", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AddressDelivered", value);
            }
        }

        public static string ItemPurchase
        {
            get
            {
                return AppSettings.GetValueOrDefault("ItemPurchase", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("ItemPurchase", value);
            }
        }

        public static string JsonSearchSave
        {
            get
            {
                return AppSettings.GetValueOrDefault("JsonSearchSave", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("JsonSearchSave", value);
            }
        }


        public static string JsonFavoriteList
        {
            get
            {
                return AppSettings.GetValueOrDefault("JsonFavoriteList", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("JsonFavoriteList", value);
            }
        }


        public static string ItemUpDateId
        {
            get
            {
                return AppSettings.GetValueOrDefault("ItemUpDateId", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("ItemUpDateId", value);
            }
        }

        public static string CategoryUpDateId
        {
            get
            {
                return AppSettings.GetValueOrDefault("CategoryUpDateId", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("CategoryUpDateId", value);
            }
        }

        public static string SortItemPage
        {
            get
            {
                return AppSettings.GetValueOrDefault("SortItemPage", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("SortItemPage", value);
            }
        }

        public static string NumberOfSearchReasult
        {
            get
            {
                return AppSettings.GetValueOrDefault("NumberOfSearchReasult", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("NumberOfSearchReasult", value);
            }
        }

        public static string SortIResultSearch
        {
            get
            {
                return AppSettings.GetValueOrDefault("SortIResultSearch", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("SortIResultSearch", value);
            }
        }
        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault("Username", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Username", value);
            }
        }
        public static string FirstName
        {
            get
            {
                return AppSettings.GetValueOrDefault("FirstName", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("FirstName", value);
            }
        }

        public static string Phone
        {
            get
            {
                return AppSettings.GetValueOrDefault("Phone", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Phone", value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static DateTime AccessTokenExpiration
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessTokenExpiration", DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessTokenExpiration", value);
            }
        }

    }
}
