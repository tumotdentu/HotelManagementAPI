using HotelManagementAPI.Datas.Models;

namespace HotelManagementAPI.Datas.ViewModels
{
    public class LoginRes
    {
        public LoginRes()
        {
        }
        public LoginRes(string accessToken, Manager user)
        {
            AccessToken = accessToken;
            User = user;
        }
        public LoginRes(string accessToken, Customer user)
        {
            AccessToken = accessToken;
            Customer = user;
        }
        /// <summary>
        /// Bearer Token dùng để truy xuất API
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Thông tin người dùng
        /// </summary>
        public Manager User { get; set; }
        public Customer Customer { get; set; }
    }
}
