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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        /// <summary>
        /// Tạo mới
        /// </summary>
        //[SystemsAuthorize(BaseConst.RoleCode.ADMIN)]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<Invoice>>> Create([FromBody] Invoice model, CancellationToken cancellationToken)
        {
            var result = await _invoiceService.Create(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Invoice>>> Update([FromRoute] string id, [FromBody] Invoice model, CancellationToken cancellationToken)
        {
            model.Id = id;
            var result = await _invoiceService.Update(model, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Xóa 
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Invoice>>> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _invoiceService.Delete(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route(BaseConst.Route.ID)]
        public async Task<ActionResult<BaseResponse<Invoice>>> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _invoiceService.GetById(id, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = $"{HotelManagementAPI.Shared.Constants.CustomerRole.Medium},Admin")]
        [Route("GetByCustomerId/{id}")]
        public async Task<ActionResult<BaseResponse<Invoice>>> GetByCustomerId([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _invoiceService.GetByCustomerId(id, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<List<Invoice>>>> GetList(CancellationToken cancellationToken)
        {
            var result = await _invoiceService.GetList(cancellationToken);
            return Ok(result);
        }
    }
}
