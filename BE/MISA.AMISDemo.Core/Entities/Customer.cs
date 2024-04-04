using MISA.AMISDemo.Core.Const;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.MISAAttribute;
using MISA.AMISDemo.Core.MISAEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    /// <summary>
    /// Khách hàng
    /// </summary>
    public class Customer:BaseEntities
    {
        #region Property
        /// <summary>
        /// Khoá chính: Id khách hàng
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Khoá ngoại: Id nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.CustomerCode)]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ tên khách hàng
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.FullName)]
        public string FullName { get; set; }

        /// <summary>
        /// Số điện thoại khách hàng
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email khách hàng
        /// </summary>
        [ProppertyName(MISAConst.Email)]
        public string Email { get; set; }

        /// <summary>
        /// Giới tính khách hàng: 0-Nữ, 1-Nam, 2-Khác
        /// </summary>
        public Gender? Gender { get; set; }


        /// <summary>
        /// Ngày sinh
        /// </summary>
        [ValidateDate]
        [ProppertyName(MISAConst.DateOfBirth)]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        public string? IdentificationCard { get; set; }

        /// <summary>
        /// Ngày cấp số căn cước công dân
        /// </summary>
        [ValidateDate]
        [ProppertyName(MISAConst.DateOfIdentityCard)]
        public DateTime? DateOfIdentityCard { get; set; }

        /// <summary>
        /// Số tiền nợ
        /// </summary>
        public double? DebitAmount { get; set; }

        /// <summary>
        /// Khoá ngoại: Id công ty khách hàng
        /// </summary>
        public Guid? CustomerCompanyId { get; set; }
        #endregion
    }
}
