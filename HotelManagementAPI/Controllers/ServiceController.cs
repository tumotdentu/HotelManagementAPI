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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        /// <summary>
        /// Tạo mới
        /// </summary>
        //[SystemsAuthorize(BaseConst.RoleCode.ADMIN)]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<Service>>> Create([FromBody] Service model, CancellationToken cancellationToken)
        {
            var result = await _serviceService.Create(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Service>>> Update([FromRoute] string id, [FromBody] Service model, CancellationToken cancellationToken)
        {
            model.Id = id;
            var result = await _serviceService.Update(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Xóa 
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Service>>> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _serviceService.Delete(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Service>>> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _serviceService.GetById(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        public async Task<ActionResult<BaseResponse<List<Service>>>> GetList(CancellationToken cancellationToken)
        {
            var result = await _serviceService.GetList(cancellationToken);
            return Ok(result);
        }
    }
}
