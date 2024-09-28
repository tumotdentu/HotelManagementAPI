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
using HotelManagementAPI.Shared.Utils;
using System.Security.Claims;

namespace HotelManagementAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet(BaseConst.Route.PROFILE)]
        public async Task<ActionResult<BaseResponse<Manager>>> Profile(CancellationToken cancellationToken)
        {
            // Lấy user ID từ token
            var userId = User.FindFirstValue("id");
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new BaseResponse<Manager>
                {
                    Code = ResStatusConst.Code.UNAUTHORIZED,
                    Message = "Unauthorized"
                });
            }

            // Truyền userId vào phương thức của service nếu cần
            var result = await _managerService.Profile(userId, cancellationToken);

            return Ok(result);
        }

        [HttpPost(BaseConst.Route.CHANGEPASSWORD)]
        public async Task<ActionResult<BaseResponse<Manager>>> ChangePassword([FromBody] ChangePassword model, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue("id");
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new BaseResponse<Manager>
                {
                    Code = ResStatusConst.Code.UNAUTHORIZED,
                    Message = "Unauthorized"
                });
            }
            var result = await _managerService.ChangePassword(userId, model, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Tạo mới
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Manager>>> Create([FromBody] Manager model, CancellationToken cancellationToken)
        {
            var result = await _managerService.Create(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        [HttpPut]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Manager>>> Update([FromRoute] string id, [FromBody] Manager model, CancellationToken cancellationToken)
        {
            model.Id = id;
            var result = await _managerService.Update(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Xóa 
        /// </summary>
        [HttpDelete]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Manager>>> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _managerService.Delete(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        [HttpGet]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Manager>>> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _managerService.GetById(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<Manager>>>> GetList(CancellationToken cancellationToken)
        {
            var result = await _managerService.GetList(cancellationToken);
            return Ok(result);
        }
    }
}
