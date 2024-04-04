using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Services
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>
        /// True: Nếu đúng định dạng
        /// False: Không đúng định dạng
        /// </returns>
        /// CreatedBy: NVTruc(30/12/2023)
        bool IsValidEmail(string email);

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <param name="pageIndex">Vị trí bản ghi</param>
        /// <param name="text">Tìm kiếm theo Mã, Họ tên, Số điện thoại</param>
        /// <returns>Trả 1 nếu lấy dữ liệu thành công</returns>
        /// CreatedBy: NVTruc(25/12/2023)
        int GetPaging(int pageSize, int pageIndex, string text);

        /// <summary>
        /// Kiểm tra file nhập khẩu dữ liệu
        /// </summary>
        /// <param name="fileImport">file Import</param>
        /// createdBy: NVTruc(16/1/2024)
        void CheckFileImport(IFormFile fileImport);

        /// <summary>
        /// Thực hiện nhập khẩu dữ liệu
        /// </summary>
        /// <param name="fileImport"File excel dữ liệu nhập khẩu></param>
        /// <param name="isCommit" Check xem có commit hay không> </param>
        /// <returns>Danh sách khách hàng nhập khẩu</returns>
        /// CreatedBy: NVTruc (17/1/2024)
        IEnumerable<Employee> ImportEmployee(bool isCommit, IFormFile fileImport);

        /// <summary>
        /// Thực hiện xuất thành file excel
        /// </summary>
        /// <returns>
        /// Trả về danh sách nhân viên file excel
        /// </returns>
        /// CreatedBy: NVTruc (26/1/2024)
        FileExport ExportExcel(List<EmployeeDTOs> list);

        /// <summary>
        /// Hàm lấy ra mã nhân viên lớn nhất và tự động tăng
        /// </summary>
        /// <returns>
        /// Trả về mã nhân viên lớn nhất sau khi tự động tăng
        /// </returns>
        /// CreatedBy: NVTruc(27/1/2024)
        public string GetEmployeeCodeBiggest();
    }
}
