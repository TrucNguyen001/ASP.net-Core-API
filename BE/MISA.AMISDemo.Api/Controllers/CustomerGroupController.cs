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
    /// Service về nhóm khách hàng
    /// </summary>
    /// CreatedBy: Nguyễn Văn Trúc(1/12/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerGroupController : BaseController<CustomerGroup>
    {
        #region Field
        ICustomerGroupRepository _customerGroupRepository;
        ICustomerGroupService _customerGroupService;
        #endregion

        #region Contructor
        public CustomerGroupController(ICustomerGroupRepository customerGroupRepository, ICustomerGroupService customerGroupService) : base(customerGroupRepository, customerGroupService)
        {
            _customerGroupRepository = customerGroupRepository;
            _customerGroupService = customerGroupService;
        }
        #endregion
    }
}
