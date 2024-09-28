using System;
using System.Collections.Generic;

namespace HotelManagementAPI.Datas.Models
{
    public partial class Service
    {
        public Service()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
