using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController<Department>
    {
        /// <summary>
        /// Service về phòng ban
        /// </summary>
        /// CreatedBy: Nguyễn Văn Trúc(1/12/2023)
        #region Contructor
        public DepartmentController(IBaseRepository<Department> baseRepository, IBaseService<Department> baseService) : base(baseRepository, baseService)
        {
        }
        #endregion
    }
}
