﻿using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;
using HotelManagementAPI.Shared.Repositories;

namespace HotelManagementAPI.Repositories
{
    public interface IRoomRepo : INewBaseRepository<Room,string>
    {
    }
}
