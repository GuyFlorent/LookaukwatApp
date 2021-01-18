using Plugin.Settings;
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

    }
}
