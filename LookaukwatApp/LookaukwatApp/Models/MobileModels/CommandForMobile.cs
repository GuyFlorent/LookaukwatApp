using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models.MobileModels
{
    public class CommandForMobile
    {
        public int Id { get; set; }
        public int CommandId { get; set; }
        public DateTime? CommandDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsHomeDelivered { get; set; }
        public string Message { get { return DeliveredMessage(); } }
        public string DateCommand { get { return ((DateTime)CommandDate).ToString("dddd, dd MMMM yyyy HH:mm:ss"); } }
        

        private string DeliveredMessage()
        {
            if (IsDelivered)
            {
                return "Livrée le " + ((DateTime)DeliveredDate).ToString("dddd, dd MMMM yyyy HH:mm:ss");
            }
            else
            {
                if (IsHomeDelivered)
                {
                    return "Commande en cours de livraison";
                }
                else
                {
                    return "Commande à venir récupérer en magasin";
                }
            }

        }
    }
}
