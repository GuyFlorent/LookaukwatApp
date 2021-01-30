using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LookaukwatApp.ViewModels.OtherServices
{
    public class ShareViewModel
    {
        public static async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "J'ai trouvé une annonce qui devrait vous intéresser sur lookaukwat",
                Text = "J'ai trouvé une annonce qui devrait vous intéresser sur lookaukwat "+ Environment.NewLine
            });
        }
    }
}
