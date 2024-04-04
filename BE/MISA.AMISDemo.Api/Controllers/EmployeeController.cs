using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.ErrorsServe;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.ValidateException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MISA.AMISDemo.Core.Services;
using demo_infastructure.Repository;
using MISA.AMISDemo.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace MISA.AMISDemo.Api.Controllers
{
    /// <summary>
    /// Service về Position
    /// </summary>
    /// CreatedBy: Nguyễn Văn Trúc(1/12/2023)
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        #region Field
        IEmployeeRepository _employeeReponsitory;
        IEmployeeService _employeeService;
        #endregion

        #region Contructor
        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeRepository, employeeService)
        {
            _employeeReponsitory = employeeRepository;
            _employeeService = employeeService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm thực hiện import file excel
        /// </summary>
        /// <param name="fileImport">file muốn import</param>
        /// <returns>
        /// 201: Thêm dữ liệu thành côngA
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        /// CreatedBy: NVTruc(24/12/2023)
        [HttpPost("import/{isCommit}")]
        public IActionResult Import(bool isCommit, IFormFile fileImport)
        {
            var res = _employeeService.ImportEmployee(isCommit, fileImport);
            return StatusCode(201, res);
        }

        /// <summary>
        /// Hàm thực hiện sắp xếp tất cả bản ghi giảm dần
        /// </summary>
        /// <returns>
        /// 200: Hiển thị dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        /// CreatedBy: NVTruc(24/12/2023)
        [HttpGet("sort")]
        public IActionResult SortDecrease()
        {
            var res = _employeeReponsitory.SortDecrease();
            return StatusCode(200, res);
        }

        /// <summary>
        /// Export tất cả bản ghi sang file excel
        /// </summary>
        /// <returns>
        /// 200: Xuất dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        ///  CreatedBy: NVTruc(24/12/2023)
        [HttpPost("ExportFile")]
        public IActionResult ExportExcel(List<EmployeeDTOs>? list) {
            var result = _employeeService.ExportExcel(list);
            return File(result.FileStream, result.FileContent, result.FileName);
        }

        /// <summary>
        /// Phân trang những bản ghi
        /// </summary>
        /// <param name="pageSize">Số lượng trang</param>
        /// <param name="pageIndex">Vị trí trang</param>
        /// <param name="text">Từ tìm kiếm trong trang</param>
        /// <returns>
        /// 200: Xuất dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        ///</returns>
        /// CreatedBy: NVTruc(24/12/2023)
        [HttpGet("paging")]
        public IActionResult Paging(int pageSize, int pageIndex, string text = "")
        {
            var validatePage = _employeeService.GetPaging(pageSize, pageIndex, text);
            var misaEntitys = _employeeReponsitory.GetPaging(pageSize, pageIndex, text);
            // Lấy ra tổng số bản ghi theo phân trang
            var totalEmployee = _employeeReponsitory.Search(text).Count();

            var result = new
            {
                ToTalRecord = totalEmployee,
                ListEmployee = misaEntitys,
            };

            return StatusCode(200, result);
        }

        /// <summary>
        /// Lấy ra mã nhân viên lớn nhất và tự động tăng 1
        /// </summary>
        /// <returns>
        /// 200: Xuất dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        ///  CreatedBy: NVTruc(24/12/2023)
        [HttpGet("GetEmployeeCode")]
        public IActionResult GetEmployeeCode()
        {
            var result = _employeeService.GetEmployeeCodeBiggest();
            return StatusCode(200, result);
        }

        /// <summary>
        /// Lấy ra danh sách nhân viên sau liên kết
        /// </summary>
        /// <returns>
        /// 200: Xuất dữ liệu thành công
        /// 400: Lỗi nghiệp vụ
        /// 500: Nếu có exception
        /// </returns>
        ///  CreatedBy: NVTruc(24/12/2023)
        [HttpGet("GetEmployees")]
        public IActionResult GetEmployees() {
            var result = _employeeReponsitory.GetEmployees();
            return StatusCode(200, result);
        }
        #endregion
    }
}
