using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.ErrorsServe;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.ValidateException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MISA.AMISDemo.Api.Controllers
{
    /// <summary>
    /// Service về khách hàng
    /// </summary>
    /// CreatedBy: Nguyễn Văn Trúc(1/12/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : BaseController<Customer>
    {
        #region Field
        ICustomerRepository _customerReponsitory;
        ICustomerService _customerService;
        #endregion

        #region Contructor
        public CustomerController(ICustomerRepository customerRepository, ICustomerService customerService):base(customerRepository, customerService)
        {
            _customerReponsitory = customerRepository;
            _customerService = customerService;
        }
        #endregion
    }
}
