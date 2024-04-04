using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Api.Tests.Service
{
    public class EmployeeServiceTests : EmployeeService
    {
        IEmployeeRepository _employeeRepository;
        IUnitOfWork _unitOfWork;
        public EmployeeServiceTests(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork) : base(employeeRepository, unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Kiểmm tra file Import
        /// </summary>
        /// <param name="fileImport">File Import</param>
        /// <exception cref="NotImplementedException"></exception>
        /// CreatedBy: NVTruc(16/1/2024)
        public void CheckFileImport(IFormFile fileImport)
        {
            base.CheckFileImport(fileImport);
        }

        /// <summary>
        /// Hàm xuất file
        /// </summary>
        /// <returns>
        /// Trả về đối tượng file
        /// </returns>
        /// CreatedBy: NVTruc(16/1/2024)
        public FileExport ExportExcel(List<EmployeeDTOs> list)
        {
            return base.ExportExcel(list);
        }

        /// <summary>
        /// Hàm lấy ra mã nhân viên lớn nhất và tự động tăng
        /// </summary>
        /// <returns>
        /// Trả về mã nhân viên lớn nhất sau khi tự động tăng
        /// </returns>
        /// CreatedBy: NVTruc(27/1/2024)
        public string GetEmployeeCodeBiggest()
        {
            return base.GetEmployeeCodeBiggest();
        }

        /// <summary>
        /// Paging
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <param name="pageIndex">Vị trí trang</param>
        /// <returns>Trả về 1 nếu validate thành công</returns>
        /// <exception cref="MISAValidateException"></exception>
        /// CreatedBy: NVTruc(27/12/2023)
        public int GetPaging(int pageSize, int pageIndex, string text)
        {
            return base.GetPaging(pageSize, pageIndex, text);
        }

        /// <summary>
        /// Hàm import file
        /// </summary>
        /// <param name="fileImport">file import</param>
        ///  <param name="isCommit" Check xem có commit hay không> </param>
        /// <returns>Trả về danh sách nhân viên import</returns>
        /// CreatedBy: NVTruc(9/1/2024)
        public IEnumerable<Employee> ImportEmployee(bool isCommit, IFormFile fileImport)
        {
            return base.ImportEmployee(isCommit, fileImport);
        }

        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>
        /// false: Email không đúng định dạng
        /// true: Email đúng định dạng
        /// </returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public bool IsValidEmail(string email)
        {
            return base.IsValidEmail(email);
        }

        /// <summary>
        /// Chuyển đường dẫn file sang IFormFile
        /// </summary>
        /// <param name="filePath">Chuỗi đường dẫn đến file</param>
        /// <returns>IFormFile sau chuyển đổi</returns>
        /// <exception cref="FileNotFoundException">Thông báo lỗi</exception>
        /// CreatedBy: Nguyễn Văn Trúc(27/2/2024)
        public IFormFile ConvertPathToIFormFile(string filePath)
        {
            // Kiểm tra xem tệp tin có tồn tại không
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(MISAExportFileExcel.NOTFOUND, filePath);
            }

            // Đọc nội dung của tệp tin từ đường dẫn
            byte[] fileBytes = File.ReadAllBytes(filePath);

            //Tạo một MemoryStream từ nội dung của tệp tin
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                // Tạo đối tượng IFormFile từ MemoryStream và tên tệp tin
                var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "fileImport", Path.GetFileName(filePath))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    ContentDisposition = $"form-data; name=\"fileImport\"; filename=\"{Path.GetFileName(filePath)}\"",
                };
                return formFile;
            }
        }

    }
}
