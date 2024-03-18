using System;
using System.Collections.Generic;

namespace task4.Model
{
    public partial class Service
    {
        public Service()
        {
            ClientServices = new HashSet<ClientService>();
            ServicePhotos = new HashSet<ServicePhoto>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Cost { get; set; }
        public int DurationInSeconds { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public string? MainImagePath { get; set; }



        public string getCost => ((int) Cost )+ " рублей за " + (DurationInSeconds / 60) + " минут.";

        public string getDiscount => Discount > 0 ? "* скидка " + Discount + "%" : "";

        public virtual ICollection<ClientService> ClientServices { get; set; }
        public virtual ICollection<ServicePhoto> ServicePhotos { get; set; }
    }
}
