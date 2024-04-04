using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerCompanyController : BaseController<CustomerCompany>
    {
        /// <summary>
        /// Service về công ty
        /// </summary>
        /// CreatedBy: Nguyễn Văn Trúc(1/12/2023)
        #region Contructor
        public CustomerCompanyController(IBaseRepository<CustomerCompany> baseRepository, IBaseService<CustomerCompany> baseService) : base(baseRepository, baseService)
        {
        }
        #endregion
    }
}
