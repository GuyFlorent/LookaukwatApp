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
        public string DateCommand { get { return CommandDate.ToString(); } }
        

        private string DeliveredMessage()
        {
            if (IsDelivered)
            {
                return "Commande livrée le " + DeliveredDate.ToString();
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
