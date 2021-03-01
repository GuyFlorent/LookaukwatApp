using LookaukwatApp.Helpers;
using LookaukwatApp.Models;
using LookaukwatApp.Models.MobileModels;
using LookaukwatApp.ViewModels;
using LookaukwatApp.ViewModels.Appartment;
using LookaukwatApp.ViewModels.Message;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;

namespace LookaukwatApp.Services
{
    public class ApiServices
    {
        string Uri = "https://lookaukwatapi.azurewebsites.net/";
        //string Uri = "https://lookaukwatapi-st5.conveyor.cloud/";
        CancellationTokenSource cts;
        public async Task<bool> RegisterAsync(string email, string firstName, string phone, string password, string confirmPassword, string parrainValue)
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
                ConfirmPassword = confirmPassword,
                ParrainName = parrainValue

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(Uri+"api/Account/Register", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<TrackingCommandModelViewModel>> GetListTrackingCommand(int commandId)
        {

            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/TrackingCommand/?id=" + commandId);

            var tackings = JsonConvert.DeserializeObject<List<TrackingCommandModelViewModel>>(json);

            return tackings;
        }

        public async Task<CommandModel> GetCommandBillAsync(int CommandId)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Command/?id=" + CommandId);

            var bill = JsonConvert.DeserializeObject<CommandModel>(json);

            return bill;
        }

        public async Task<List<CommandForMobile>> GetUserCommandsAsync( string accessToken)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Command/GetUserCommand");

            var List = JsonConvert.DeserializeObject<List<CommandForMobile>>(json);
            // var liste = List.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return List;
        }

        public async Task<List<ImageProcductModel>> GetImagesAsyn(string itemId)
        {
            var accessToken = Settings.AccessToken;
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetImages/?ProductId=" + itemId);

            var mode = JsonConvert.DeserializeObject<List<ImageProcductModel>>(json);

            return mode;
        }

        public async Task EditApartCritereAsync(string itemId, int price, string searchOrAsk, string type, int roomNumber, string furnitureOrNot, int apartSurface, string accessToken)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            var model = new ApartModelViewModel()
            {
                id = Convert.ToInt32(itemId),
                Price = price,
                SearchOrAsk = searchOrAsk,
                Type = type,
                RoomNumber = roomNumber,
                FurnitureOrNot = furnitureOrNot,
                ApartSurface = apartSurface
               
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PutAsync(Uri + "api/Apartment/?id=" + itemId, content);

            Debug.WriteLine(response);
        }

        public async Task<ApartModelViewModel> GetUniqueApartCritereAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Apartment/GetApartCritere/?id=" + id);

            var apart = JsonConvert.DeserializeObject<ApartModelViewModel>(json);

            return apart;
        }

        public async Task<IDictionary<string, string>> GetListProvidersAsync()
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetProviderList/");

            var ParrainList = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);

            return ParrainList;
        }

        public async Task<IDictionary<string, string>> GetListParrainAsync()
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetParrainList/");

            var ParrainList = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);

            return ParrainList;
        }

        public async Task EditProductWithSamePropertieAsync(string accessToken, string itemId, string title, string description, string town, string street)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

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

            var model = new ProductModel()
            {
                id = Convert.ToInt32(itemId),
                Title = title,
                Description = description,
                Town = town,
                Street = street,
                Coordinate = coor
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PutAsync(Uri + "api/Product/?id=" + itemId, content);

            Debug.WriteLine(response);
        }

        public async Task<int> CommandPostAsync(int id, string payementMethod, int deliveredPrice, int totalPrice_int, string firstName, string lastName, string town, string street, string number, string telephone, double distance, int quantity)
        {
            HttpClient client;
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            DeliveredAdressModel addresse = new DeliveredAdressModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Number = number,
                Street = street,
                Telephone = telephone,
                Town = town
            };

           

            bool checkDeliver = false;
            if(deliveredPrice == 0)
            {
                checkDeliver = false;

            }
            else
            {
                checkDeliver = true;
            }

            CommandModel command = new CommandModel()
            {
                TotalPrice = totalPrice_int,
                Distance = Convert.ToInt32(distance).ToString("N", CultureInfo.CreateSpecificCulture("af-ZA")).Split(',')[0].Trim() + " Km",
                DeliveredAdress = addresse,
                DeliveredPrice = deliveredPrice,
                PayementMethod = payementMethod,
                IsDelivered = false,
                IsDeleteByUser = false,
                IsHomeDelivered = checkDeliver,
            };

            ProductModel product = new ProductModel()
            {
                id = id,
            };
            PurchaseModel purchase = new PurchaseModel()
            {
                
                product = product,
                Quantities = quantity
            };
            List<PurchaseModel> purchases = new List<PurchaseModel>();
            purchases.Add(purchase);

            command.Purchases = purchases;

            var  accessToken = Settings.AccessToken;
            var json = JsonConvert.SerializeObject(command);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri + "api/Command", content);
            Debug.WriteLine(response);
            var jwt = await response.Content.ReadAsStringAsync();
            var joo = JObject.Parse(jwt);//
            var idCommand = (int)joo["CommandId"];

            Debug.WriteLine(jwt);

            return idCommand;
        }

        public void UpdateCallNumber(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json =  client.GetStringAsync(Uri + "api/Product/CallNumber/?id=" + id);

        }

        public void UpdateMessageNumber(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = client.GetStringAsync(Uri + "api/Product/MessageNumber/?id=" + id);

        }


        public async Task<ProductModel> GetProductSameParamAsync(int id)
        {

            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetProductWithSameParm/?id=" + id);

            var prod = JsonConvert.DeserializeObject<ProductModel>(json);

            return prod;
        }

        public async Task EditHouseCritereAsync(string itemId, int price, string searchOrAskJob, string rubrique, string fabricMaterial, string type, string color, string state, string accessToken)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            var model = new HouseModel()
            {
                id = Convert.ToInt32(itemId),
                Price = price,
                SearchOrAskJob = searchOrAskJob,
                RubriqueHouse = rubrique,
                TypeHouse = type,
                ColorHouse = color,
                StateHouse = state,
                FabricMaterialeHouse = fabricMaterial,

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PutAsync(Uri + "api/House/?id=" + itemId, content);

            Debug.WriteLine(response);
        }

        public async Task EditModeCritereAsync(string itemId, int price, string searchOrAskJob, string rubrique, string brand, string type, string univers, string size, string color, string state, string accessToken)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            var model = new ModeModelViewModel()
            {
                id = Convert.ToInt32(itemId),
                Price = price,
                SearchOrAsk = searchOrAskJob,
                Rubrique = rubrique,
                Brand = brand,
                Type = type,
                Univers = univers,
                Color = color,
                State = state,
               Size = size

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PutAsync(Uri + "api/Mode/?id=" + itemId, content);

            Debug.WriteLine(response);
        }

        public async Task EditVehiculeCritereAsync(string itemId, int price, string searchOrAskJob, string rubrique, string brand, string type, string color, string petrol, string year, string mileage, string numberOfDoor, string gearBox, string model, string state, string firstyear, string accessToken)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            var modele = new VehiculeModelViewModel()
            {
                id = Convert.ToInt32(itemId),
                Price = price,
                SearchOrAsk = searchOrAskJob,
                RubriqueVehicule = rubrique,
                BrandVehicule = brand,
                TypeVehicule = type,
                ModelVehicule = model,
                ColorVehicule = color,
                StateVehicule = state,
                YearVehicule = year,
                MileageVehicule = mileage,
                FirstYearVehicule = firstyear,
                GearBoxVehicule = gearBox,
                NumberOfDoorVehicule = numberOfDoor,
                PetrolVehicule = petrol

            };

            var json = JsonConvert.SerializeObject(modele);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PutAsync(Uri + "api/Vehicule/?id=" + itemId, content);

            Debug.WriteLine(response);
        }

        public async Task<VehiculeModelViewModel> GetUniqueVehiculeCritereAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Vehicule/GetVehiculediaCritere/?id=" + id);

            var house = JsonConvert.DeserializeObject<VehiculeModelViewModel>(json);

            return house;
        }

        public async Task<string> GetDeliveredCommandAsync(int CommandId)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Command/DeliveredCommand/?id=" + CommandId);

            var res = JsonConvert.DeserializeObject<string>(json);

            return res;
        }

        public async Task<bool> PostTrackingCommandAsync(int idCommand , string lat, string lon)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
           
            string accessToken = Settings.AccessToken;
           
            var command = new CommandModel
            {
                CommandId = idCommand
            };

            var model = new TrackingCommandModel()
            {
               Command = command,
               Lat = lat,
               Lon = lon
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PostAsync(Uri + "api/TrackingCommand", content);
            Debug.WriteLine(response);
            var jwt = await response.Content.ReadAsStringAsync();
            //var joo = JObject.Parse(jwt);
            //var id = (int)joo["id"];
            var res = JsonConvert.DeserializeObject<bool>(jwt);
            //var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return res;
        }

        public async Task<ModeModelViewModel> GetUniqueModeCritereAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Mode/GetModeCritere/?id=" + id);

            var house = JsonConvert.DeserializeObject<ModeModelViewModel>(json);

            return house;
        }

        public async Task<HouseModelViewModel> GetUniqueHouseCritereAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/House/GetHouseCritere/?id=" + id);

            var house = JsonConvert.DeserializeObject<HouseModelViewModel>(json);

            return house;
        }

        public async Task<JobModelViewModel> GetUniqueJobCritereAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/JobModels/GetJobCritere/?id=" + id);

            var Job = JsonConvert.DeserializeObject<JobModelViewModel>(json);

            return Job;
        }

        public async Task EditJobCritereAsync(string itemId, int price, string searchOrAskJob, string typeContract, string activitySector, string accessToken)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            
            var model = new JobModel()
            {
               id = Convert.ToInt32(itemId),
                Price = price,
                SearchOrAskJob = searchOrAskJob,
                TypeContract = typeContract,
                ActivitySector = activitySector,
              
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PutAsync(Uri + "api/JobModels/?id="+itemId, content);

            Debug.WriteLine(response);
            
        }

        public async Task<bool> DeleteImageAsync(string accessToken, Guid id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.DeleteAsync(Uri + "api/Product/DeleteImages/?ImageId=" + id);

            return json.IsSuccessStatusCode;
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

        public async Task SendMessageToPublisherAsync(ContactEmailUserViewModel contact)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
            var json = JsonConvert.SerializeObject(contact);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(Uri + "api/Product/PostMessage", content);
            Debug.WriteLine(response);
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
            

            if (accessToken != null)
            {
                var accessTokenExpiration = (DateTime)joo[".expires"];
                Settings.AccessTokenExpiration = accessTokenExpiration;

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

            var model = new UserPhoneViewModel()
            {
                phoneNumber = phone
            };
            var jsonn = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(jsonn);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.PostAsync(Uri + "api/Product/GetUserName/",content);
            var jwt = await json.Content.ReadAsStringAsync();
            var UserName = JsonConvert.DeserializeObject<string>(jwt);
           
            return UserName;
        }

        public async Task EditMultimediaCritereAsync(string itemId, int price, string searchOrAskJob, string rubrique, string brand, string model, string capacity, string accessToken)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            var modele = new MultimediaModelViewModel()
            {
                id = Convert.ToInt32(itemId),
                Price = price,
                SearchOrAsk = searchOrAskJob,
                Brand = brand,
                Type = rubrique,
                Model = model,
                Capacity = capacity,
              

            };

            var json = JsonConvert.SerializeObject(modele);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            var response = await client.PutAsync(Uri + "api/Multimedia/?id=" + itemId, content);

            Debug.WriteLine(response);
        }

        public async Task<MultimediaModelViewModel> GetUniqueMultimediaCritereAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Multimedia/GetMultimediaCritere/?id=" + id);

            var house = JsonConvert.DeserializeObject<MultimediaModelViewModel>(json);

            return house;
        }

        public async Task<int> ApartPostAsync(string accessToken, string titleApart, string description, string town, string street, int price, string searchOrAskJob, int roomNumber, int apartSurface, string furnitureOrNot, string type, string provider_Id, int stock)
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
                Coordinate = coor,
                Stock = stock,
                Provider_Id = provider_Id,
                IsActive = true

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

        public async Task<int> VehiculePostAsync(string accessToken, string title, string description, string town, string street, int price, string searchOrAskJob, string rubrique, string brand, string color, string type, string petrol, string state, string firstYear, string year, string mileage, string numberOfDoor, string gearBox, string model, string provider_Id, int stock)
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
                Coordinate = coor,
                Provider_Id = provider_Id,
                Stock = stock,
                IsActive = true

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

        public async Task<int> HousePostAsync(string accessToken, string title, string description, string town, string street, int price, string searchOrAskJob, string rubrique, string color, string type, string state, string fabricMaterial, string provider_Id, int stock)
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
                Coordinate = coor,
                Stock = stock,
                Provider_Id = provider_Id,
                IsActive = true
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

        public async Task<int> MultimediaPostAsync(string accessToken, string title, string description, string town, string street, int price, string searchOrAskJob, string rubrique, string brand, string model, string capacity, string provider_Id, int stock)
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
                Type = rubrique,
                Brand = brand,
                Model = model,
                Capacity = capacity,
                Category = categorie,
                Coordinate = coor,
                Stock = stock,
                Provider_Id = provider_Id,
                IsActive = true
            };

            var json = JsonConvert.SerializeObject(modele);

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
            string searchOrAskJob, string rubrique, string brand, string color, string type, string size, string state, string univers, string provider_Id, int stock)
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
                Coordinate = coor,
                Stock = stock,
                Provider_Id = provider_Id,
                IsActive = true

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
            string searchOrAskJob, string typeContract, string activitySector, string provider_Id, int stock)
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
                Coordinate = coor,
                Stock = stock,
                Provider_Id  =provider_Id,
                IsActive = true

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


        public async Task<List<ProductForMobileViewModel>> GetProductsAsync(int pageIndex, int pageSize, string sortBy)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/Product/GetProductScrollView/?pageIndex=" + pageIndex + "&pageSize="+pageSize + "&sortBy=" + sortBy);

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


        public async Task<JobModelViewModel> GetUniqueJobAsync(int id)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);


            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri+"api/JobModels/?id=" + id);

            var Job = JsonConvert.DeserializeObject<JobModelViewModel>(json);

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

        public async Task<int> GetResultAskAndOfferSeachNumberAsync(string categori, string town, string searchOrAsk, bool isParticulier, bool isLookaukwat)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetAskAndOfferSearchNumber/?categori=" + categori + "&town="+ town + "&searchOrAsk=" + searchOrAsk + "&isParticulier=" + isParticulier + "&isLookaukwat=" + isLookaukwat);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }


        public async Task<int> GetResultOfferSeachNumberJobAsync(string categori, string town, string searchOrAskJob, string typeContract, string activitySector, int price, bool isParticulier, bool isLookaukwat)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/JobModels/GetOfferJobSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob + "&price=" + price + "&activitySector=" + activitySector + "&typeContract=" + typeContract+ "&isParticulier="+ isParticulier+ "&isLookaukwat="+isLookaukwat);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberModeAsync(string categori, string town, string searchOrAskJob, int price, string rubriqueMode, string typeMode, string brandMode, string universMode, string sizeMode, string state, string colorMode, bool isParticulier, bool isLookaukwat)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            rubriqueMode = HttpUtility.UrlEncode(rubriqueMode);
            brandMode = HttpUtility.UrlEncode(brandMode);
            var json = await client.GetStringAsync(Uri + "api/Mode/GetOfferModeSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob +  "&price=" + price + "&rubriqueMode=" + rubriqueMode + "&typeMode=" + typeMode + "&brandMode=" + brandMode + "&universMode=" + universMode + "&sizeMode=" + sizeMode + "&state=" + state + "&colorMode=" + colorMode + "&isParticulier=" + isParticulier + "&isLookaukwat=" + isLookaukwat);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberHouseAsync(string categori, string town, string searchOrAskJob, int price, string rubriqueHouse, string typeHouse, string fabricMaterialHouse, string stateHouse, string colorHouse, bool isParticulier, bool isLookaukwat)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            typeHouse = HttpUtility.UrlEncode(typeHouse);
            var json = await client.GetStringAsync(Uri + "api/House/GetOfferHouseSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob  + "&price=" + price + "&rubriqueHouse=" + rubriqueHouse + "&typeHouse=" + typeHouse + "&fabricMaterialHouse=" + fabricMaterialHouse + "&stateHouse=" + stateHouse + "&colorHouse=" + colorHouse + "&isParticulier=" + isParticulier + "&isLookaukwat=" + isLookaukwat);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberMultiAsync(string categori, string town, string searchOrAskJob, int price, string multimediaRubrique, string multimediaBrand, string multimediaModel, string multimediaCapacity, bool isParticulier, bool isLookaukwat)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            multimediaRubrique = HttpUtility.UrlEncode(multimediaRubrique);
            var json = await client.GetStringAsync(Uri + "api/Multimedia/GetOfferMultiSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob +  "&price=" + price + "&multimediaRubrique=" + multimediaRubrique + "&multimediaBrand=" + multimediaBrand + "&multimediaModel=" + multimediaModel + "&multimediaCapacity=" + multimediaCapacity + "&isParticulier=" + isParticulier + "&isLookaukwat=" + isLookaukwat);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberApartAsync(string categori, string town, string searchOrAskJob, int price, int roomNumberAppart, string furnitureOrNotAppart, string typeAppart, int apartSurfaceAppart, bool isParticulier, bool isLookaukwat)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Apartment/GetOfferAppartSearchNumber/?categori=" + categori + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob + "&price=" + price + "&roomNumberAppart=" + roomNumberAppart + "&furnitureOrNotAppart=" + furnitureOrNotAppart + "&typeAppart=" + typeAppart + "&apartSurfaceAppart=" + apartSurfaceAppart + "&isParticulier=" + isParticulier + "&isLookaukwat=" + isLookaukwat);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<int> GetResultOfferSeachNumberVehiculeAsync(string category, string town, string searchOrAskJob, int price,string vehiculeRubrique, string vehiculeBrand, string vehiculeModel, string vehiculeType, string petrol, int year, int mileage, string numberOfDoor, string gearBox, string vehiculestate, string color, bool isParticulier, bool isLookaukwat)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Vehicule/GetOfferVehiculeSearchNumber/?categori=" + category + "&town=" + town + "&searchOrAskJob=" + searchOrAskJob + "&price=" + price + "&vehiculeRubrique=" + vehiculeRubrique + "&vehiculeBrand=" + vehiculeBrand + "&vehiculeModel=" + vehiculeModel + "&vehiculeType=" + vehiculeType + "&petrol=" + petrol + "&year=" + year + "&mileage=" + mileage + "&numberOfDoor=" + numberOfDoor + "&gearBox=" + gearBox + "&vehiculestate=" + vehiculestate + "&color=" + color + "&isParticulier=" + isParticulier + "&isLookaukwat=" + isLookaukwat);

            int result = JsonConvert.DeserializeObject<int>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSearchVehiculeAsync(SearchModel userSearchCondition, int pageIndex, int pageSize, string sortBy)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Vehicule/GetOfferVehiculeSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceVehicule + "&vehiculeRubrique=" + userSearchCondition.VehiculeRubrique + "&vehiculeBrand=" + userSearchCondition.VehiculeBrand + "&vehiculeModel=" + userSearchCondition.VehiculeModel + "&vehiculeType=" + userSearchCondition.VehiculeType + "&petrol=" + userSearchCondition.Petrol + "&year=" + userSearchCondition.Year + "&mileage=" + userSearchCondition.Mileage + "&numberOfDoor=" + userSearchCondition.NumberOfDoor + "&gearBox=" + userSearchCondition.GearBox + "&vehiculestate=" + userSearchCondition.VehiculeState + "&color=" + userSearchCondition.Color + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&sortBy=" + sortBy + "&isParticulier=" + userSearchCondition.IsParticulier + "&isLookaukwat=" + userSearchCondition.IsLookaukwat);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachModeAsync(SearchModel userSearchCondition, int pageIndex, int pageSize, string sortBy)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            userSearchCondition.RubriqueMode = HttpUtility.UrlEncode(userSearchCondition.RubriqueMode);
            userSearchCondition.BrandMode = HttpUtility.UrlEncode(userSearchCondition.BrandMode);
            var json = await client.GetStringAsync(Uri + "api/Mode/GetOfferModeSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceMode + "&rubriqueMode=" + userSearchCondition.RubriqueMode + "&typeMode=" + userSearchCondition.TypeMode + "&brandMode=" + userSearchCondition.BrandMode + "&universMode=" + userSearchCondition.UniversMode + "&sizeMode=" + userSearchCondition.SizeMode + "&state=" + userSearchCondition.State + "&colorMode=" + userSearchCondition.ColorMode + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&sortBy=" + sortBy + "&isParticulier=" + userSearchCondition.IsParticulier + "&isLookaukwat=" + userSearchCondition.IsLookaukwat);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }


        public async Task<List<ProductForMobileViewModel>> GetResultAskAndOfferSearchAsync(SearchModel userSearchCondition, int pageIndex, int pageSize, string sortBy)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Product/GetAskAndOfferSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAsk=" + userSearchCondition.SearchOrAskJob + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&sortBy=" + sortBy + "&isParticulier=" + userSearchCondition.IsParticulier + "&isLookaukwat=" + userSearchCondition.IsLookaukwat);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<string>> GetVehiculeEquipementModelAsync(string rubrique, string brand)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
           // userSearchCondition.MultimediaRubrique = HttpUtility.UrlEncode(userSearchCondition.MultimediaRubrique);
            var json = await client.GetStringAsync(Uri + "api/Vehicule/GetModel/?rubrique=" + rubrique + "&brand=" + brand);

            var result = JsonConvert.DeserializeObject<List<string>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachMultiAsync(SearchModel userSearchCondition, int pageIndex, int pageSize, string sortBy)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            userSearchCondition.MultimediaRubrique = HttpUtility.UrlEncode(userSearchCondition.MultimediaRubrique);
            var json = await client.GetStringAsync(Uri + "api/Multimedia/GetOfferMultiSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceMulti + "&multimediaRubrique=" + userSearchCondition.MultimediaRubrique + "&multimediaBrand=" + userSearchCondition.MultimediaBrand + "&multimediaModel=" + userSearchCondition.MultimediaModel + "&multimediaCapacity=" + userSearchCondition.MultimediaCapacity + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&sortBy=" + sortBy + "&isParticulier=" + userSearchCondition.IsParticulier + "&isLookaukwat=" + userSearchCondition.IsLookaukwat);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachHouseAsync(SearchModel userSearchCondition, int pageIndex, int pageSize, string sortBy)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
            userSearchCondition.TypeHouse = HttpUtility.UrlEncode(userSearchCondition.TypeHouse);
            var json = await client.GetStringAsync(Uri + "api/House/GetOfferHouseSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceHouse + "&rubriqueHouse=" + userSearchCondition.RubriqueHouse + "&typeHouse=" + userSearchCondition.TypeHouse + "&fabricMaterialHouse=" + userSearchCondition.FabricMaterialHouse + "&stateHouse=" + userSearchCondition.StateHouse + "&colorHouse=" + userSearchCondition.ColorHouse + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&sortBy=" + sortBy + "&isParticulier=" + userSearchCondition.IsParticulier + "&isLookaukwat=" + userSearchCondition.IsLookaukwat);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachApartAsync(SearchModel userSearchCondition, int pageIndex, int pageSize, string sortBy)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/Apartment/GetOfferAppartSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceApart + "&roomNumberAppart=" + userSearchCondition.RoomNumberAppart + "&furnitureOrNotAppart=" + userSearchCondition.FurnitureOrNotAppart + "&typeAppart=" + userSearchCondition.TypeAppart + "&apartSurfaceAppart=" + userSearchCondition.ApartSurfaceAppart + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&sortBy=" + sortBy + "&isParticulier=" + userSearchCondition.IsParticulier + "&isLookaukwat=" + userSearchCondition.IsLookaukwat);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }

        public async Task<List<ProductForMobileViewModel>> GetResultOfferSeachJobAsync(SearchModel userSearchCondition, int pageIndex, int pageSize, string sortBy)
        {
            HttpClient client;

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

            var json = await client.GetStringAsync(Uri + "api/JobModels/GetOfferJobSearch/?categori=" + userSearchCondition.Category + "&town=" + userSearchCondition.Town + "&searchOrAskJob=" + userSearchCondition.SearchOrAskJob + "&price=" + userSearchCondition.PriceJob + "&activitySector=" + userSearchCondition.ActivitySector + "&typeContract=" + userSearchCondition.TypeContract + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&sortBy=" + sortBy + "&isParticulier=" + userSearchCondition.IsParticulier + "&isLookaukwat=" + userSearchCondition.IsLookaukwat);

            var result = JsonConvert.DeserializeObject<List<ProductForMobileViewModel>>(json);

            return result;
        }
    }
}
