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

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        /// <summary>
        /// Tạo mới
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<BaseResponse<Customer>>> Create([FromBody] Customer model, CancellationToken cancellationToken)
        {
            var result = await _customerService.Create(model, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost(BaseConst.Route.REGISTER)]
        public async Task<ActionResult<BaseResponse<Customer>>> Register([FromBody] Customer model, CancellationToken cancellationToken)
        {
            var result = await _customerService.Register(model, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost(BaseConst.Route.LOGIN)]
        public async Task<ActionResult<BaseResponse<LoginRes>>> Login([FromBody] LoginReq model, CancellationToken cancellationToken)
        {
            var result = await _customerService.Login(model, cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [HttpGet(BaseConst.Route.PROFILE)]
        public async Task<ActionResult<BaseResponse<Customer>>> Profile(CancellationToken cancellationToken)
        {
            // Lấy user ID từ token
            var userId = User.FindFirstValue("id");
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new BaseResponse<Customer>
                {
                    Code = ResStatusConst.Code.UNAUTHORIZED,
                    Message = "Unauthorized"
                });
            }

            // Truyền userId vào phương thức của service nếu cần
            var result = await _customerService.Profile(userId, cancellationToken);

            return Ok(result);
        }

        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [HttpPost(BaseConst.Route.CHANGEPASSWORD)]
        public async Task<ActionResult<BaseResponse<Customer>>> ChangePassword([FromBody] ChangePassword model, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue("id");
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new BaseResponse<Customer>
                {
                    Code = ResStatusConst.Code.UNAUTHORIZED,
                    Message = "Unauthorized"
                });
            }
            var result = await _customerService.ChangePassword(userId, model, cancellationToken);

            return Ok(result);
        }
        /// <summary>
        /// Cập nhật
        /// </summary>
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [HttpPut]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Customer>>> Update([FromRoute] string id, [FromBody] Customer model, CancellationToken cancellationToken)
        {
            model.Id = id;
            var result = await _customerService.Update(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Xóa 
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Customer>>> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _customerService.Delete(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Customer>>> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _customerService.GetById(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<Customer>>>> GetList(CancellationToken cancellationToken)
        {
            var result = await _customerService.GetList(cancellationToken);
            return Ok(result);
        }
    }
}
