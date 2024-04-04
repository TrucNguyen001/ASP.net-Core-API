using MISA.AMISDemo.Core.Const;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.MISAAttribute;
using MISA.AMISDemo.Core.MISAEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs
{
    public class EmployeeDTOs
    {
        #region Property
        /// <summary>
        /// Id nhân viên: Khoá chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên đầu nhân viên
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Tên cuối nhân viên
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Giới tính nhân viên: 0-Nam 1-Nữ 2-Khác
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Căn cước công dân
        /// </summary>
        public string? IdentificationCard { get; set; }

        /// <summary>
        /// Nơi cấp căn cước công dân
        /// </summary>
        public string? PlaceIdentificationCard { get; set; }

        /// <summary>
        /// Ngày cấp căn cước công dân
        /// </summary>
        public DateTime? DateOfIdentityCard { get; set; }


        /// <summary>
        /// Ngày tham gia
        /// </summary>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public int? MartialStatus { get; set; }

        /// <summary>
        /// Id phòng ban: Khoá ngoại
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Id chức vụ : Khoá ngoại
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Trạng thái công việc 0-Đang làm việc 1-Tạm nghỉ 2-Nghỉ hẳn
        /// </summary>
        public WorkStatus? WorkStatus { get; set; }

        /// <summary>
        /// Lương
        /// </summary>
        public decimal? Salary { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string? LandlinePhone { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string? Branch { get; set; }
        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Lưu thông báo lỗi
        /// </summary>
        public List<string>? ImportInvalidErrors { get; set; } = new List<string>();

        #endregion
    }
}