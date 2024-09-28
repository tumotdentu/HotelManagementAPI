using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels.Base;
using HotelManagementAPI.Services;
using HotelManagementAPI.Services.Impl;
using HotelManagementAPI.Utils;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        /// <summary>
        /// Tạo mới
        /// </summary>
        //[SystemsAuthorize(BaseConst.RoleCode.ADMIN)]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<Room>>> Create([FromBody] Room model, CancellationToken cancellationToken)
        {
            var result = await _roomService.Create(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Room>>> Update([FromRoute] string id, [FromBody] Room model, CancellationToken cancellationToken)
        {
            model.Id = id;
            var result = await _roomService.Update(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Xóa 
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Room>>> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _roomService.Delete(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Room>>> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _roomService.GetById(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        public async Task<ActionResult<BaseResponse<List<Room>>>> GetList([FromQuery(Name = BaseConst.QueryKey.SEARCH_KEY)] string? searchKey, CancellationToken cancellationToken)
        {
            var result = await _roomService.GetList(searchKey, cancellationToken);
            return Ok(result);
        }
    }
}
