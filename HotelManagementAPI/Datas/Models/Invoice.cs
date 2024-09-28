using System;
using System.Collections.Generic;

namespace HotelManagementAPI.Datas.Models
{
    public partial class Invoice
    {
        public string Id { get; set; } = null!;
        public string? BookingId { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
