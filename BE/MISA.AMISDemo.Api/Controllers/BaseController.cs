using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.ErrorsServe;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Core.ValidateException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using MISA.AMISDemo.Infrastructure.Repository;
using MISA.AMISDemo.Core.DTOs;

namespace MISA.AMISDemo.Api.Controllers
{
    /// <summary>
    /// Class Base
    /// </summary>
    /// CreatedBy: Nguyễn Văn Trúc(1/12/2023)
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<MISAEntity> : ControllerBase where MISAEntity : class
    {
        #region Fields
        IBaseRepository<MISAEntity> _baseRepository;
        IBaseService<MISAEntity> _baseService;
        #endregion


        #region Constructor
        public BaseController(IBaseRepository<MISAEntity> baseRepository, IBaseService<MISAEntity> baseService) 
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>
        /// 200: Nếu có dữ liệu
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        /// CreatedBy: NVTruc(26/12/2023)
        [HttpGet]
        public IActionResult Get()
        {
            var misaEntitys = _baseRepository.GetAll();
            return StatusCode(200, misaEntitys);
        }

        /// <summary>
        /// Lấy dữ liệu theo Id
        /// </summary>
        /// <param name="misaEntityId">Ib bản ghi muốn tìm</param>
        /// <returns>
        /// 200: Nếu có dữ liệu
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        /// CreatedBy: NVTruc(26/12/2023)
        [HttpGet("{misaEntityId}")]
        public IActionResult GetById(Guid misaEntityId)
        {
            var misaEntity = _baseRepository.GetById(misaEntityId);
            return StatusCode(200, misaEntity);
        }

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <returns>
        /// <param name="misaEntity">Bản ghi</param>
        /// 201: Thêm dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        /// CreatedBy: NVTruc(26/12/2023)
        [HttpPost("post")]
        public IActionResult Post(MISAEntity misaEntity)
        {

            // Validate data
            var res = _baseService.InsertService(misaEntity);
            
            // Add data in databases
            var res2 = _baseRepository.Insert(misaEntity);
            return StatusCode(201, res);
        }

        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="misaEntity">Bản ghi</param>
        /// <param name="misaEntityId">Ib bản ghi muốn sửa</param>
        /// <returns>
        /// 200: Sửa dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        /// CreatedBy: NVTruc(26/12/2023)
        [HttpPut("put/{misaEntityId}")]
        public IActionResult Put(MISAEntity misaEntity, Guid misaEntityId)
        {
            // Validate data
            var res = _baseService.UpdateService(misaEntity, misaEntityId);
           
            // Add data in databases
            var res2 = _baseRepository.Update(misaEntity, misaEntityId);
            return StatusCode(200, res);
        }

        /// <summary>
        /// Xoá dữ liệu
        /// </summary>
        /// <param name="misaEntityId">Ib bản ghi muốn xoá</param>
        /// <returns>
        /// 200: Xoá dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        /// CreatedBy: NVTruc(26/12/2023)
        [HttpDelete("delete/{misaEntityId}")]
        public IActionResult Delete(Guid misaEntityId)
        {
            var misaEntity = _baseRepository.Delete(misaEntityId);
            return StatusCode(200, misaEntity);
        }

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="misaEntityIds">Id những bản ghi muốn xoá</param>
        /// <returns>
        /// 200: Xoá dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        /// CreatedBy: NVTruc(2/1/2024)
        [HttpDelete("delete/multipleDelete/")]
        public IActionResult MultipleDelete(List<Guid> misaEntityIds)
        {
            var misaEntity = _baseRepository.MultipleDelete(misaEntityIds);
            return StatusCode(200, misaEntity);
        }

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="records">Danh sách bản ghi muốn thêm</param>
        /// <returns>
        /// 201: Xoá dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        ///  CreatedBy: Nguyễn Văn Trúc(8/3/2024)
        [HttpPost("multiplePost")]
        public IActionResult MultiplePost(List<MISAEntity> records)
        {
            var misaEntity = _baseRepository.MultiplePost(records);
            return StatusCode(201, misaEntity);
        }
        #endregion
    }
}
