using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.ViewModels.StaticList
{
    public static class StaticListViewModel
    {
        public static IList<string> GetCategoryList = new List<string>()
        {
            "Emploi",
            "Événement",
             "Immobilier",
             "Maison",
            "Multimédia",
            "Mode",
           "Véhicule"

        };

        public static List<string> SignalAnnouceMotifList = new List<string>()
        {
          "Fraude",
          "Doublon",
          "Mauvaise catégorie",
          "Déja vendu",
          "Discrimination",
          "Contrefacon",
          "Autre abus",
        };



        public static List<string> OfferOSearchList = new List<string>()
        {
          "J'offre",
          "Je recherche"
        };
        public static List<string> GetWorkContratTypeList = new List<string>()
        {
            "CDI",
            "CDD",
            "Stage/Alternance",
            "Sans contrat",
            "Autre"
        };

        public static List<string> GetActivitySectortList = new List<string>()
        {
           "Ingénieur",
           "Agriculture",
           "Immobilier",
           "Corps médicale",
           "Enseignement",
           "Hôtellerie/Restauration",
           "Sport",
           "Technique",
           "Maçonnerie",
           "Couture",
           "Sérigraphie",
           "Infographie",
           "Electricien",
           "Electricien",
           "Taximan",
           "Benskineur",
           "Comptable",
           "Service à la personne",
           "Prestation de service",
           "Communication",
           "Commercial",
           "Sécurité",
           "Administration",
           "Autre"
        };

        public static List<string> GetTownCameroonList = new List<string>()
        {
            "Douala",
            "Yaoundé",
            "Garoua",
            "Bamenda",
            "Maroua",
            "Nkongsamba",
            "Bafoussam",
            "Ngaoundéré",
            "Bertoua",
            "Loum",
            "Kumba",
            "Kumbo",
            "Foumban",
            "Mbouda",
            "Dschang",
            "Limbé",
            "Ebolowa",
            "Kousséri",
            "Guider",
            "Meiganga",
            "Yagoua",
            "Mbalmayo",
            "Bafang",
            "Tiko",
            "Bafia",
            "Wum",
            "Kribi",
            "Buea",
            "Sangmélima",
            "Foumbot",
            "Bangangté",
            "Batouri",
            "Banyo",
            "Nkambé",
            "Bali",
            "Mbanga",
            "Mokolo",
            "Melong",
            "Manjo",
            "Garoua-Boulaï",
            "Mora",
            "Kaélé",
            "Tibati",
            "Ndop",
            "Akonolinga",
            "Eséka",
            "Mamfé",
            "Obala",
            "Muyuka",
            "Nanga-Eboko",
            "Abong-Mbang",
            "Fundong",
            "Nkoteng",
            "Fontem",
            "Mbandjock",
            "Touboro",
            "Ngaoundal",
            "Yokadouma",
            "Pitoa",
            "Tombel",
            "Kékem",
            "Magba",
            "Bélabo",
            "Tonga",
            "Maga",
            "Koutaba",
            "Blangoua",
            "Guidiguis",
            "Bogo",
            "Batibo",
            "Yabassi",
            "Figuil",
            "Makénéné",
            "Gazawa",
            "Tcholliré",
            "Edéa"
        };

        public static List<string> GetListApartType = new List<string>
        {
            "Appartement à louer",
            "Entrepot à louer",
            "Boutique à louer",
            "Chambre à louer",
            "studio à louer",
            "Maison à louer",
            "Maison à louer",
            "Bureau à louer",
            "Maison à Vendre",
            "terrain à vendre",
            "Autre"
        };

        public static List<string> GetListFurnitureOrNot = new List<string>
        {
            "Meublé",
            "Non meublé",
        };


        /////////////////////////////For Search  //////////////////////////////////


        public static IList<string> GetCategoryListSearch = new List<string>()
        {
            "Toutes",
            "Emploi",
            "Événement",
             "Immobilier",
             "Maison",
            "Multimédia",
            "Mode",
           "Véhicule"

        };
       
        public static List<string> GetWorkContratTypeListSearch = new List<string>()
        {
            "Tout",
            "CDI",
            "CDD",
            "Stage/Alternance",
            "Sans contrat",
            "Autre"
        };

        public static List<string> GetActivitySectortListSearch = new List<string>()
        {
           "Tout",
           "Ingénieur",
           "Agriculture",
           "Immobilier",
           "Corp médicale",
           "Enseignement",
           "Hôtellerie/Restauration",
           "Sport",
           "Technique",
           "Maçonnerie",
           "Couture",
           "Sérigraphie",
           "Infographie",
           "Electricien",
           "Electricien",
           "Taximan",
           "Bensikineur",
           "Comptable",
           "Service à la personne",
           "Prestation de service",
           "Commnication",
           "Commercial",
           "Sécurité",
           "Administration",
           "Autre"
        };

        public static List<string> GetTownCameroonListSearch = new List<string>()
        {
            "Toutes",
            "Douala",
            "Yaoundé",
            "Garoua",
            "Bamenda",
            "Maroua",
            "Nkongsamba",
            "Bafoussam",
            "Ngaoundéré",
            "Bertoua",
            "Loum",
            "Kumba",
            "Kumbo",
            "Foumban",
            "Mbouda",
            "Dschang",
            "Limbé",
            "Ebolowa",
            "Kousséri",
            "Guider",
            "Meiganga",
            "Yagoua",
            "Mbalmayo",
            "Bafang",
            "Tiko",
            "Bafia",
            "Wum",
            "Kribi",
            "Buea",
            "Sangmélima",
            "Foumbot",
            "Bangangté",
            "Batouri",
            "Banyo",
            "Nkambé",
            "Bali",
            "Mbanga",
            "Mokolo",
            "Melong",
            "Manjo",
            "Garoua-Boulaï",
            "Mora",
            "Kaélé",
            "Tibati",
            "Ndop",
            "Akonolinga",
            "Eséka",
            "Mamfé",
            "Obala",
            "Muyuka",
            "Nanga-Eboko",
            "Abong-Mbang",
            "Fundong",
            "Nkoteng",
            "Fontem",
            "Mbandjock",
            "Touboro",
            "Ngaoundal",
            "Yokadouma",
            "Pitoa",
            "Tombel",
            "Kékem",
            "Magba",
            "Bélabo",
            "Tonga",
            "Maga",
            "Koutaba",
            "Blangoua",
            "Guidiguis",
            "Bogo",
            "Batibo",
            "Yabassi",
            "Figuil",
            "Makénéné",
            "Gazawa",
            "Tcholliré",
            "Edéa"
        };

        public static List<string> GetListApartTypeSearch = new List<string>
        {
            "Tout",
            "Appartement à louer",
            "Entrepot à louer",
            "Boutique à louer",
            "Chambre à louer",
            "studio à louer",
            "Maison à louer",
            "Maison à louer",
            "Bureau à louer",
            "Maison à Vendre",
            "terrain à vendre",
            "Autre"
        };

        public static List<string> GetListFurnitureOrNotSearch = new List<string>
        {
            "Tout",
            "Meublé",
            "Non meublé",
        };
    }
}
