﻿namespace HotelManagementAPI.Datas.ViewModels
{
    public class ChangePassword
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
