using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels.Base;
using HotelManagementAPI.Services;
using HotelManagementAPI.Services.Impl;
using HotelManagementAPI.Utils;
using HotelManagementAPI.Datas.ViewModels;

namespace HotelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        /// <summary>
        /// Tạo mới
        /// </summary>
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [HttpPost(BaseConst.Route.BOOKING)]
        public async Task<ActionResult<BaseResponse<Booking>>> Booking([FromBody] Booking model, CancellationToken cancellationToken)
        {
            var result = await _bookingService.Booking(model, cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [HttpPost(BaseConst.Route.EXTEND)]
        public async Task<ActionResult<BaseResponse<Booking>>> Extend([FromBody] ExtendModel model, CancellationToken cancellationToken)
        {
            var result = await _bookingService.Extend(model, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<BaseResponse<Booking>>> Create([FromBody] Booking model, CancellationToken cancellationToken)
        {
            var result = await _bookingService.Create(model, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// Cập nhật
        /// </summary>
        [HttpPut]
        [Route(BaseConst.Route.ID)]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        public async Task<ActionResult<BaseResponse<Booking>>> Update([FromRoute] string id, [FromBody] Booking model, CancellationToken cancellationToken)
        {
            model.Id = id;
            var result = await _bookingService.Update(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Xóa 
        /// </summary>
        [HttpDelete]
        [Route(BaseConst.Route.ID)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<Booking>>> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _bookingService.Delete(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Booking>>> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _bookingService.GetById(id, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [Route("GetByCustomerId/{id}")]
        public async Task<ActionResult<BaseResponse<Booking>>> GetByCustomerId([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _bookingService.GetByCustomerId(id, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<List<Booking>>>> GetList(CancellationToken cancellationToken)
        {
            var result = await _bookingService.GetList(cancellationToken);
            return Ok(result);
        }
    }
}
