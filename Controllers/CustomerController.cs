using Asp.Versioning;
using CERP.Common;
using CERP.ModelDataTransferObjects.Customers;
using CERP.Models;
using CERP.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CERP.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableRateLimiting("fixed")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _service;
        public CustomerController(ICustomerServices service)
        {
            _service = service;
        }

        [HttpPost("CustomerAdd")]
        [MapToApiVersion("1.0")]
        public async Task<BaseApiResponse> CustomerAdd(CustomerAddInput input)
        {
            return await _service.CustomerAdd(input);
        }

    }
}
