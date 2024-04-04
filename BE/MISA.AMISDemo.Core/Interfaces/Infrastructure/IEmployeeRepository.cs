using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Hàm giúp sắp xếp danh sách nhân viên giảm dần theo mã
        /// </summary>
        /// <returns>
        /// Trả về danh sách nhân viên giảm dần theo mã
        /// </returns>
        /// CreateBy: NVTruc(28/12/2023)
        public IEnumerable<Employee> SortDecrease();

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <param name="pageIndex">Vị trí bản ghi</param>
        /// <param name="text">Tìm kiểm theo Mã, Họ tên, Số điện thoại</param>
        /// <returns>Trả về số lượng bản ghi</returns>
        /// CreatedBy: NVTruc(26/12/2023)
        IEnumerable<EmployeeDTOs> GetPaging(int pageSize, int pageIndex, string text);

        /// <summary>
        /// Lấy tất cả danh sách nhân viên và liên kết với chức vụ và phòng ban
        /// </summary>
        /// <returns>
        /// Danh sách nhân viên sau khi liên kết
        /// </returns>
        /// CreateBy: NVTruc(28/12/2023)
        public IEnumerable<EmployeeDTOs> GetEmployees();

        /// <summary>
        /// Kiểm sô điện thoại tồn tại chưa
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns>
        /// false: Số điện thoại chưa tồn tại
        /// true: Số điện thoại đã tồn tại</returns>
        /// CreateBy: NVTruc(28/12/2023)
        public bool CheckDuplicatePhoneNumber(string phoneNumber);

        /// <summary>
        /// Tìm kiếm theo mã, họ tên, số điện thoại
        /// </summary>
        /// <param name="text">Nội dung muốn tìm kiếm</param>
        /// <returns>Trả về những bản ghi tìm kiếm</returns>
        /// CreateBy: NVTruc(28/12/2023)
        IEnumerable<Employee> Search(string text);
    }
}
