using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelManagementAPI.Datas.Models
{
    public partial class Booking
    {
        public Booking()
        {
            BookingDetails = new HashSet<BookingDetail>();
            Invoices = new HashSet<Invoice>();
        }

        public string Id { get; set; } = null!;
        public string? CustomerId { get; set; }
        public string? RoomId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? NumberOfGuests { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? DepositAmount { get; set; }
        public string? DepositStatus { get; set; }
        public string? Status { get; set; }
        [JsonIgnore]
        public bool? IsExtend { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Room? Room { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
