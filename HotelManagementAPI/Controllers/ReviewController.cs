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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        /// <summary>
        /// Tạo mới
        /// </summary>
        //[SystemsAuthorize(BaseConst.RoleCode.ADMIN)]
        [HttpPost]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        public async Task<ActionResult<BaseResponse<Review>>> Create([FromBody] Review model, CancellationToken cancellationToken)
        {
            var result = await _reviewService.Create(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Review>>> Update([FromRoute] string id, [FromBody] Review model, CancellationToken cancellationToken)
        {
            model.Id = id;
            var result = await _reviewService.Update(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Xóa 
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Review>>> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _reviewService.Delete(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Review>>> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _reviewService.GetById(id, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// Lấy theo Id
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [Route("GetByRoomId/{id}")]
        public async Task<ActionResult<BaseResponse<Review>>> GetByRoomId([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _reviewService.GetByRoomId(id, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<List<Review>>>> GetList(CancellationToken cancellationToken)
        {
            var result = await _reviewService.GetList(cancellationToken);
            return Ok(result);
        }
    }
}
