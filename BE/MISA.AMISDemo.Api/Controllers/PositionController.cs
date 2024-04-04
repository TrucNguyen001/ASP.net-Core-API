using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MISA.AMISDemo.Api.Controllers
{
    /// <summary>
    /// Service về Position
    /// </summary>
    /// CreatedBy: Nguyễn Văn Trúc(1/12/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionController : BaseController<Position>
    {
        #region Contructor
        public PositionController(IBaseRepository<Position> baseRespository, IBaseService<Position> baseService) : base(baseRespository, baseService)
        {
        }
        #endregion
    }
}
