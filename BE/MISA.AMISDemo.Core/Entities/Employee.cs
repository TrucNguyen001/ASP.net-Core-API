using MISA.AMISDemo.Core.Const;
using MISA.AMISDemo.Core.MISAAttribute;
using MISA.AMISDemo.Core.MISAEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee:BaseEntities
    {
        #region Property
        /// <summary>
        /// Id nhân viên: Khoá chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.EmployeeCode)]
        [MaxLength(20)]
        public string EmployeeCode {  get; set; }

        /// <summary>
        /// Tên đầu nhân viên
        /// </summary>
        [MaxLength(100)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Tên cuối nhân viên
        /// </summary>
        [MaxLength(100)]
        public string? LastName { get; set; }

        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.FullName)]
        [MaxLength(100)]
        public string FullName { get; set; }

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
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(255)]
        public string? Address { get; set; }

        /// <summary>
        /// Căn cước công dân
        /// </summary>
        [MaxLength(25)]
        public string? IdentificationCard { get; set; }

        /// <summary>
        /// Nơi cấp căn cước công dân
        /// </summary>
        [MaxLength(255)]
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
        [NotEmpty]
        [ProppertyName(MISAConst.DepartmentCode)]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Id chức vụ : Khoá ngoại
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Trạng thái công việc 0-Đang làm việc 1-Tạm nghỉ 2-Nghỉ hẳn
        /// </summary>
        public WorkStatus? WorkStatus {  get; set; }

        /// <summary>
        /// Lương
        /// </summary>
        public decimal? Salary { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        [MaxLength(50)]
        public string? LandlinePhone {  get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        [MaxLength(25)]
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [MaxLength(255)]
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        [MaxLength(255)]
        public string? Branch { get; set; }
        #endregion
    }
}
