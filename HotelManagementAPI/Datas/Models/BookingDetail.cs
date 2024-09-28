using System;
using System.Collections.Generic;

namespace HotelManagementAPI.Datas.Models
{
    public partial class BookingDetail
    {
        public string Id { get; set; } = null!;
        public string? BookingId { get; set; }
        public string? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Status { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Service? Service { get; set; }
    }
}
