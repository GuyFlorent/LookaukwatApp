using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.ViewModels.StaticList
{
    class StaticListEventViewModel
    {
        public static List<string> GetRubriqueEventListt = new List<string>
        {
            "Spectacle",
            "Sport"
        };

        public static List<string> GetTypeEventSportListt = new List<string>
        {
            "Basket-Ball",
            "Boxe",
            "Football",
            "Tennis",
        };

        public static List<string> GetTypeEventSpectacleListt = new List<string>
        {
            "Cinéma",
            "Cirque",
            "Concert",
            "Danse",
            "Soirée dancing",
            "Spectacle de cabarets",
            "Théâtre",
        };


        //For search
        public static List<string> GetRubriqueEventListSearch = new List<string>
        {
            "Toutes",
            "Spectacle",
            "Sport"
        };

        public static List<string> GetTypeEventSportListSearch = new List<string>
        {
            "Tout",
            "Basket-Ball",
            "Boxe",
            "Football",
            "Tennis",
        };

        public static List<string> GetTypeEventSpectacleListSearch = new List<string>
        {
            "Tout",
            "Cinéma",
            "Cirque",
            "Concert",
            "Danse",
            "Soirée dancing",
            "Spectacle de cabarets",
            "Théâtre",
        };
    }
}
