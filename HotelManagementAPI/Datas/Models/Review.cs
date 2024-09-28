using System;
using System.Collections.Generic;

namespace HotelManagementAPI.Datas.Models
{
    public partial class Review
    {
        public string Id { get; set; } = null!;
        public string? CustomerId { get; set; }
        public string? RoomId { get; set; }
        public string? Content { get; set; }
        public int? Rating { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Room? Room { get; set; }
    }
}
