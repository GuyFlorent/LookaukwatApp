using LookaukwatApp.Helpers;
using LookaukwatApp.Models;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LookaukwatApp.Services
{
    public class ApiServices
    {
        string Uri = "https://10.1.2.48:45455/";
        public async Task<bool> RegisterAsync(string email, string firstName, string phone, string password, string confirmPassword)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            var model = new RegisterBindingModel()
            {
                Email = email,
                FirstName = firstName,
                Phone = phone,
                Password = password,
                ConfirmPassword = confirmPassword

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(Uri+"api/Account/Register", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(string accessToken, int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.DeleteAsync(Uri + "api/Product/?id=" + id);

            return json.IsSuccessStatusCode;
        }

        public async Task<bool> LogoutASync(string accessToken)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            HttpContent content = new StringContent(null);
            var response = await client.PostAsync("https://192.168.1.66:45458/api/Account/Logout", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> LoginASync(string email, string password)
        {
            string body = "grant_type=password&username=" + email + "&password=" + password;
            //var keyValues = new List<KeyValuePair<string, string>>()
            //{
            //    new KeyValuePair<string, string>("username",email),
            //    new KeyValuePair<string, string>("password",password),
            //    new KeyValuePair<string, string>("grand_type","password")
            //};
            var request = new HttpRequestMessage(HttpMethod.Post, Uri+"Token");
            request.Content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

            //request.Content = new FormUrlEncodedContent(keyValues);

            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            var response = await client.SendAsync(request);
            var jwt = await response.Content.ReadAsStringAsync();
            var joo = JObject.Parse(jwt);
            var accessToken = (string)joo["access_token"];

            if(accessToken != null)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

                 var resp = await client.GetStringAsync(Uri + "api/Product/GetUserInformation");
                ApplicationUser user = JsonConvert.DeserializeObject<ApplicationUser>(resp);
                if (user != null)
                Settings.FirstName = user.FirstName;
                Settings.Phone = user.PhoneNumber;
                Settings.Username = user.Email;

            }
            Debug.WriteLine(jwt);

            return accessToken;
        }

        public async Task<string> ChangePasswordAsync(string accessToken, string oldPassword, string newPassword, string confirmPassword)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            
            var model = new ChangePasswordBindingModel()
            {
               OldPassword = oldPassword,
               NewPassword = newPassword,
               ConfirmPassword = confirmPassword

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri + "api/Account/ChangePassword", content);
            Debug.WriteLine(response);
            var jwt = await response.Content.ReadAsStringAsync();
            var joo = JObject.Parse(jwt);
            IEnumerable<JToken> Message = joo["ModelState"][""].AsEnumerable();
            IEnumerable<JToken> MessageErrorNewAndConfirmPassword = joo["ModelState"]["model.ConfirmPassword"].AsEnumerable();
            IEnumerable<JToken> MessageErrorNewPassword = joo["ModelState"]["model.NewPassword"].AsEnumerable();
            string mess = null;
            if (Message != null)
            {
                mess = Message.First().ToString();
            }
            
            
            //var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return mess;
            
        }

       

        public async Task<string> GetUserNameWithPhoneAsync(string phone)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetUserName/?phone=" + phone);

            var UserName = JsonConvert.DeserializeObject<string>(json);
           
            return UserName;
        }

        public async Task<int> ApartPostAsync(string accessToken, string titleApart, string description, string town, string street, int price, string searchOrAskJob, int roomNumber, int apartSurface, string furnitureOrNot, string type)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            CategoryModel categorie = new CategoryModel() { CategoryName = "Immobilier" };

            //get lat and lon
            var address = $"{street + " " + town + " " + "Cameroun"}";
            var locations = await Geocoding.GetLocationsAsync(address);
            var location = locations?.FirstOrDefault();
            ProductCoordinateModel coor = new ProductCoordinateModel() { };
            if (location != null)
            {

                coor.Lat = location.Latitude.ToString();
                coor.Lon = location.Longitude.ToString();

            }

            var model = new ApartmentRentalModel()
            {
                Title = titleApart,
                Description = description,
                Town = town,
                Street = street,
                Price = price,
                SearchOrAskJob = searchOrAskJob,
                Type = type,
                ApartSurface = apartSurface,
                RoomNumber = roomNumber,
                FurnitureOrNot = furnitureOrNot,
                Category = categorie,
                DateAdd = DateTime.Now,
                Coordinate = coor

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri + "api/Apartment", content);
            Debug.WriteLine(response);
            var jwt = await response.Content.ReadAsStringAsync();
            var joo = JObject.Parse(jwt);
            var id = (int)joo["id"];

            //var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return id;
        }

        public async Task<bool> CheckEmailExistAsync(string newEmail)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            var modele = new ApplicationUser()
            {
                Email = newEmail
            };
            var jsonn = JsonConvert.SerializeObject(modele);

            HttpContent content = new StringContent(jsonn);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var json = await client.PostAsync(Uri + "api/Product/CheckUserEmailExists",content);


            var jwt = await json.Content.ReadAsStringAsync();
            bool result = JsonConvert.DeserializeObject<bool>(jwt);

            return result;
        }

        public async Task<bool> CheckPhoneExistAsync(string newPhone)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            var modele = new ApplicationUser()
            {
                PhoneNumber = newPhone
            };
            var jsonn = JsonConvert.SerializeObject(modele);

            HttpContent content = new StringContent(jsonn);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var json = await client.PostAsync(Uri + "api/Product/CheckUserPhoneExists", content);

            var jwt = await json.Content.ReadAsStringAsync();
            bool result = JsonConvert.DeserializeObject<bool>(jwt);

            return result;
        }

        public async Task<int> VehiculePostAsync(string accessToken, string title, string description, string town, string street, int price, string searchOrAskJob, string rubrique, string brand, string color, string type, string petrol, string state, string firstYear, string year, string mileage, string numberOfDoor, string gearBox, string model)
        {
            HttpClient client;
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            CategoryModel categorie = new CategoryModel() { CategoryName = "Vehicule" };

            //get lat and lon
            var address = $"{street + " " + town + " " + "Cameroun"}";
            var locations = await Geocoding.GetLocationsAsync(address);
            var location = locations?.FirstOrDefault();
            ProductCoordinateModel coor = new ProductCoordinateModel() { };
            if (location != null)
            {

                coor.Lat = location.Latitude.ToString();
                coor.Lon = location.Longitude.ToString();

            }
            
              

            var modele = new VehiculeModel()
            {
                Title = title,
                Description = description,
                Town = town,
                Street = street,
                Price = price,
                SearchOrAskJob = searchOrAskJob,
                RubriqueVehicule = rubrique,
                TypeVehicule = type,
                ColorVehicule = color,
                StateVehicule = state,
                PetrolVehicule = petrol,
                BrandVehicule = brand,
                FirstYearVehicule = firstYear,
                YearVehicule = year,
                GearBoxVehicule = gearBox,
                MileageVehicule = mileage,
                ModelVehicule = model,
                NumberOfDoorVehicule = numberOfDoor,
                Category = categorie,
                DateAdd = DateTime.Now,
                Coordinate = coor

            };

            var json = JsonConvert.SerializeObject(modele);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri + "api/Vehicule", content);
            Debug.WriteLine(response);
            var jwt = await response.Content.ReadAsStringAsync();
            var joo = JObject.Parse(jwt);
            var id = (int)joo["id"];

            //var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return id;
        }

        public async Task<bool> UpdateUserInfoAsync(string oldFirstName, string oldPhone, string newEmail)
        {
            var accessToken = Settings.AccessToken;
            HttpClient client;
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            var modele = new ApplicationUser()
            {
                FirstName = oldFirstName,
                Email = newEmail,
                PhoneNumber = oldPhone

            };

            var json = JsonConvert.SerializeObject(modele);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri + "api/Product/UpdateUserInfo", content);
            Debug.WriteLine(response);
            //var jwt = await response.Content.ReadAsStringAsync();
            //var joo = JObject.Parse(jwt);
            //var id = (int)joo["id"];

            //var accessToken = jwtDynamic.Value<string>("access_token");

           // Debug.WriteLine(jwt);

            return response.IsSuccessStatusCode;
        }

        public async Task<int> HousePostAsync(string accessToken, string title, string description, string town, string street, int price, string searchOrAskJob, string rubrique, string color, string type, string state, string fabricMaterial)
        {
            HttpClient client;
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            CategoryModel categorie = new CategoryModel() { CategoryName = "Maison" };

            //get lat and lon
            var address = $"{street + " " + town + " " + "Cameroun"}";
            var locations = await Geocoding.GetLocationsAsync(address);
            var location = locations?.FirstOrDefault();
            ProductCoordinateModel coor = new ProductCoordinateModel() { };
            if (location != null)
            {

                coor.Lat = location.Latitude.ToString();
                coor.Lon = location.Longitude.ToString();

            }

            var model = new HouseModel()
            {
                Title = title,
                Description = description,
                Town = town,
                Street = street,
                Price = price,
                SearchOrAskJob = searchOrAskJob,
                RubriqueHouse = rubrique,
                TypeHouse = type,
                ColorHouse = color,
                StateHouse = state,
                FabricMaterialeHouse =fabricMaterial,
                Category = categorie,
                DateAdd = DateTime.Now,
                Coordinate = coor

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri + "api/House", content);
            Debug.WriteLine(response);
            var jwt = await response.Content.ReadAsStringAsync();
            var joo = JObject.Parse(jwt);
            var id = (int)joo["id"];

            //var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return id;
        }

        public async Task<int> MultimediaPostAsync(string accessToken, string title, string description, string town, string street, int price, string searchOrAskJob, string rubrique, string brand, string model, string capacity)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            CategoryModel categorie = new CategoryModel() { CategoryName = "Multimedia" };

            //get lat and lon
            var address = $"{street + " " + town + " " + "Cameroun"}";
            var locations = await Geocoding.GetLocationsAsync(address);
            var location = locations?.FirstOrDefault();
            ProductCoordinateModel coor = new ProductCoordinateModel() { };
            if (location != null)
            {

                coor.Lat = location.Latitude.ToString();
                coor.Lon = location.Longitude.ToString();

            }

            var modele = new MultimediaModel()
            {
                Title = title,
                Description = description,
                Town = town,
                Street = street,
                Price = price,
                SearchOrAskJob = searchOrAskJob,
                Type= rubrique,
                Brand = brand,
                Model = model,
                Capacity = capacity,
                Category = categorie,
                DateAdd = DateTime.Now,
                Coordinate = coor

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri + "api/Multimedia", content);
            Debug.WriteLine(response);
            var jwt = await response.Content.ReadAsStringAsync();
            var joo = JObject.Parse(jwt);
            var id = (int)joo["id"];

            //var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return id;
        }

        public async Task<int> ModePostAsync(string accessToken, string title, string description, string town, string street, int price,
            string searchOrAskJob, string rubrique, string brand, string color, string type, string size, string state, string univers)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            CategoryModel categorie = new CategoryModel() { CategoryName = "Mode" };

            //get lat and lon
            var address = $"{street + " " + town + " " + "Cameroun"}";
            var locations = await Geocoding.GetLocationsAsync(address);
            var location = locations?.FirstOrDefault();
            ProductCoordinateModel coor = new ProductCoordinateModel() { };
            if (location != null)
            {

                coor.Lat = location.Latitude.ToString();
                coor.Lon = location.Longitude.ToString();

            }

            var model = new ModeModel()
            {
                Title = title,
                Description = description,
                Town = town,
                Street = street,
                Price = price,
                SearchOrAskJob = searchOrAskJob,
                RubriqueMode = rubrique,
                BrandMode = brand,
                TypeMode = type,
                ColorMode = color,
                SizeMode = size,
                StateMode = state,
                UniversMode = univers,
                Category = categorie,
                DateAdd = DateTime.Now,
                Coordinate = coor

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri + "api/Mode", content);
            Debug.WriteLine(response);
            var jwt = await response.Content.ReadAsStringAsync();
            var joo = JObject.Parse(jwt);
            var id = (int)joo["id"];

            //var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return id;
        }

        public async Task<int> JobPostAsync(string accessToken, string titleJob, string description, string town, string street, int price,
            string searchOrAskJob, string typeContract, string activitySector)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            CategoryModel categorie = new CategoryModel() { CategoryName = "Emploi" };

            //get lat and lon
            var address = $"{street + " " + town + " " + "Cameroun"}";
            var locations = await Geocoding.GetLocationsAsync(address);
            var location = locations?.FirstOrDefault();
            ProductCoordinateModel coor = new ProductCoordinateModel() { };
            if (location != null)
            {

                coor.Lat = location.Latitude.ToString();
                coor.Lon = location.Longitude.ToString();

            }

            var model = new JobModel()
            {
                Title = titleJob,
                Description = description,
                Town = town,
                Street = street,
                Price = price,
                SearchOrAskJob = searchOrAskJob,
                TypeContract = typeContract,
                ActivitySector = activitySector,
                Category = categorie,
                DateAdd = DateTime.Now,
                Coordinate = coor

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri+"api/JobModels", content);
            Debug.WriteLine(response);
            var jwt = await response.Content.ReadAsStringAsync();
            var joo = JObject.Parse(jwt);
            var id = (int)joo["id"];

            //var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return id;
        }

        public async Task<List<ProductForMobileViewModel>> GetUserProductsAsync(string accessToken)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetUserProducts");

            var List = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);
            // var liste = List.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return List;
        }


        public async Task<List<ProductForMobileViewModel>> GetProductsAsync(int pageIndex, int pageSize)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/Product/GetProductScrollView/?pageIndex=" + pageIndex + "&pageSize="+pageSize);

            var List = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);
           // var liste = List.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return List;

        }

        public async Task<List<ProductForMobileViewModel>> GetProductsAsync(string Searchterm)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/Product/GetSearchProducts/?newValue=" + Searchterm);

            var List = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);
            // var liste = List.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return List;

        }
        public async Task<List<ProductForMobileViewModel>> GetProductsAsync()
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/Product");

            var List = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);
            var liste = List.ToList();
            return liste;

        }


        public async Task<int> Get_AllNumber_ProductsAsync()
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/Product/TotalNumberOfProduct");

            var TotalNumberOfProduct = JsonConvert.DeserializeObject<int>(json);

            return TotalNumberOfProduct;

        }


        public async Task<JobModel> GetUniqueJobAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/JobModels/?id=" + id);

            var Job = JsonConvert.DeserializeObject<JobModel>(json);

            return Job;

        }

        public async Task<ModeModelViewModel> GetUniqueModeAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/Mode/?id=" + id);

            var mode = JsonConvert.DeserializeObject<ModeModelViewModel>(json);

            return mode;

        }

        public async Task<ApartModelViewModel> GetUniqueApartAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/Apartment/?id=" + id);

            var apart = JsonConvert.DeserializeObject<ApartModelViewModel>(json);

            return apart;

        }

        public async Task<MultimediaModelViewModel> GetUniqueMultimediaAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/Multimedia/?id=" + id);

            var multi = JsonConvert.DeserializeObject<MultimediaModelViewModel>(json);

            return multi;

        }

        public async Task<HouseModelViewModel> GetUniqueHouseAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/House/?id=" + id);

            var house = JsonConvert.DeserializeObject<HouseModelViewModel>(json);

            return house;

        }

        public async Task<VehiculeModelViewModel> GetUniqueVehiculeAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/Vehicule/?id=" + id);

            var vehicule = JsonConvert.DeserializeObject<VehiculeModelViewModel>(json);

            return vehicule;

        }

        public async Task<int> GetResultAskAndOfferSeachNumberAsync(string categori, string town, string searchOrAsk)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetAskAndOfferSearchNumber/?categori=" + categori + "&town="+ town + "&searchOrAsk=" + searchOrAsk);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }


        public async Task<int> GetResultOfferSeachNumberJobAsync(string categori, string town, string searchOrAskJob, string typeContract, string activitySector, int price)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/JobModels/GetOfferJobSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob + "&price=" + price + "&activitySector=" + activitySector + "&typeContract=" + typeContract);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberModeAsync(string categori, string town, string searchOrAskJob, int price, string rubriqueMode, string typeMode, string brandMode, string universMode, string sizeMode, string state, string colorMode)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Mode/GetOfferModeSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob +  "&price=" + price + "&rubriqueMode=" + rubriqueMode + "&typeMode=" + typeMode + "&brandMode=" + brandMode + "&universMode=" + universMode + "&sizeMode=" + sizeMode + "&state=" + state + "&colorMode=" + colorMode);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberHouseAsync(string categori, string town, string searchOrAskJob, int price, string rubriqueHouse, string typeHouse, string fabricMaterialHouse, string stateHouse, string colorHouse)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/House/GetOfferHouseSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob  + "&price=" + price + "&rubriqueHouse=" + rubriqueHouse + "&typeHouse=" + typeHouse + "&fabricMaterialHouse=" + fabricMaterialHouse + "&stateHouse=" + stateHouse + "&colorHouse=" + colorHouse);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberMultiAsync(string categori, string town, string searchOrAskJob, int price, string multimediaRubrique, string multimediaBrand, string multimediaModel, string multimediaCapacity)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Multimedia/GetOfferMultiSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob +  "&price=" + price + "&multimediaRubrique=" + multimediaRubrique + "&multimediaBrand=" + multimediaBrand + "&multimediaModel=" + multimediaModel + "&multimediaCapacity=" + multimediaCapacity);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberApartAsync(string categori, string town, string searchOrAskJob, int price, int roomNumberAppart, string furnitureOrNotAppart, string typeAppart, int apartSurfaceAppart)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Apartment/GetOfferAppartSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob + "&price=" + price + "&roomNumberAppart=" + roomNumberAppart + "&furnitureOrNotAppart=" + furnitureOrNotAppart + "&typeAppart=" + typeAppart + "&apartSurfaceAppart=" + apartSurfaceAppart);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberVehiculeAsync(string category, string town, string searchOrAskJob, int price,string vehiculeRubrique, string vehiculeBrand, string vehiculeModel, string vehiculeType, string petrol, string year, string mileage, string numberOfDoor, string gearBox, string vehiculestate, string color)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Vehicule/GetOfferVehiculeSearchNumber/?categori=" + category + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob + "&price=" + price + "&vehiculeRubrique=" + vehiculeRubrique + "&vehiculeBrand=" + vehiculeBrand + "&vehiculeModel=" + vehiculeModel + "&vehiculeType=" + vehiculeType + "&petrol=" + petrol + "&year=" + year + "&mileage=" + mileage + "&numberOfDoor=" + numberOfDoor + "&gearBox=" + gearBox + "&vehiculestate=" + vehiculestate + "&color=" + color);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSearchVehiculeAsync(SearchModel userSearchCondition, int pageIndex, int pageSize)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Vehicule/GetOfferVehiculeSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceVehicule + "&vehiculeBrand=" + userSearchCondition.VehiculeBrand + "&vehiculeModel=" + userSearchCondition.VehiculeModel + "&vehiculeType=" + userSearchCondition.VehiculeType + "&petrol=" + userSearchCondition.Petrol + "&year=" + userSearchCondition.Year + "&mileage=" + userSearchCondition.Mileage + "&numberOfDoor=" + userSearchCondition.NumberOfDoor + "&gearBox=" + userSearchCondition.GearBox + "&vehiculestate=" + userSearchCondition.VehiculeState + "&color=" + userSearchCondition.Color + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachModeAsync(SearchModel userSearchCondition, int pageIndex, int pageSize)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Mode/GetOfferModeSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceMode + "&rubriqueMode=" + userSearchCondition.RubriqueMode + "&typeMode=" + userSearchCondition.TypeMode + "&brandMode=" + userSearchCondition.BrandMode + "&universMode=" + userSearchCondition.UniversMode + "&sizeMode=" + userSearchCondition.SizeMode + "&state=" + userSearchCondition.State + "&colorMode=" + userSearchCondition.ColorMode + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }


        public async Task<List<ProductForMobileViewModel>> GetResultAskAndOfferSearchAsync(SearchModel userSearchCondition, int pageIndex, int pageSize)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetAskAndOfferSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAsk=" + userSearchCondition.SearchOrAskJob + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachMultiAsync(SearchModel userSearchCondition, int pageIndex, int pageSize)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Multimedia/GetOfferMultiSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceMulti + "&multimediaRubrique=" + userSearchCondition.MultimediaRubrique + "&multimediaBrand=" + userSearchCondition.MultimediaBrand + "&multimediaModel=" + userSearchCondition.MultimediaModel + "&multimediaCapacity=" + userSearchCondition.MultimediaCapacity + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachHouseAsync(SearchModel userSearchCondition, int pageIndex, int pageSize)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/House/GetOfferHouseSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceHouse + "&rubriqueHouse=" + userSearchCondition.RubriqueHouse + "&typeHouse=" + userSearchCondition.TypeHouse + "&fabricMaterialHouse=" + userSearchCondition.FabricMaterialHouse + "&stateHouse=" + userSearchCondition.StateHouse + "&colorHouse=" + userSearchCondition.ColorHouse + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachApartAsync(SearchModel userSearchCondition, int pageIndex, int pageSize)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Apartment/GetOfferAppartSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceApart + "&roomNumberAppart=" + userSearchCondition.RoomNumberAppart + "&furnitureOrNotAppart=" + userSearchCondition.FurnitureOrNotAppart + "&typeAppart=" + userSearchCondition.TypeAppart + "&apartSurfaceAppart=" + userSearchCondition.ApartSurfaceAppart + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachJobAsync(SearchModel userSearchCondition, int pageIndex, int pageSize)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/JobModels/GetOfferJobSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceJob + "&activitySector=" + userSearchCondition.ActivitySector + "&typeContract=" + userSearchCondition.TypeContract + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }
    }
}
