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
    public class BookingDetailController : ControllerBase
    {
        private readonly IBookingDetailService _bookingDetailService;

        public BookingDetailController(IBookingDetailService bookingDetailService)
        {
            _bookingDetailService = bookingDetailService;
        }
        /// <summary>
        /// Tạo mới
        /// </summary>
       
        [HttpPost]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        public async Task<ActionResult<BaseResponse<BookingDetail>>> Create([FromBody] BookingDetail model, CancellationToken cancellationToken)
        {
            var result = await _bookingDetailService.Create(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<BookingDetail>>> Update([FromRoute] string id, [FromBody] BookingDetail model, CancellationToken cancellationToken)
        {
            model.Id = id;
            var result = await _bookingDetailService.Update(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Xóa 
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<BookingDetail>>> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _bookingDetailService.Delete(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<BookingDetail>>> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _bookingDetailService.GetById(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<List<BookingDetail>>>> GetList(CancellationToken cancellationToken)
        {
            var result = await _bookingDetailService.GetList(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [Route("GetByBookingId/{id}")]
        public async Task<ActionResult<BaseResponse<List<BookingDetail>>>> GetByBookingId([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _bookingDetailService.GetByBookingId(id, cancellationToken);
            return Ok(result);
        }
    }
}
