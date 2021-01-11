using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace LookaukwatApp.ViewModels.OtherServices
{
    public class PhoneDialerViewModel
    {
        public static void PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
                Console.WriteLine(anEx.ToString());
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
