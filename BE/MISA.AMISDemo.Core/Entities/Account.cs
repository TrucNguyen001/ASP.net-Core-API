using MISA.AMISDemo.Core.Const;
using MISA.AMISDemo.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class Account
    {
        /// <summary>
        /// Id tài khoản
        /// </summary>
        public Guid AccountId { get; set; }

        [NotEmpty]
        [ProppertyName(MISAConst.Account)]
        /// <summary>
        /// Tài khoản
        /// </summary>
        public string UserName { get; set; }

        [NotEmpty]
        [ProppertyName(MISAConst.Password)]
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password {  get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Quyền hạn
        /// </summary>
        public string? Roles {  get; set; }

        /// <summary>
        /// Số lần truy cập thất bại
        /// </summary>
        public int? AccessFailedCount { get; set; }

        /// <summary>
        /// Chuỗi ngẫu nhiên được tạo ra mỗi khi dữ liệu người dùng được cập nhật
        /// </summary>
        public string? ConcurrencyStamp {  get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại được xác nhận chưa
        /// false: chưa
        /// true: Rồi
        /// </summary>
        public bool? PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Email được xác nhận chưa
        /// false: chưa
        /// true: Rồi
        /// </summary>
        public bool? EmailConfirmed { get; set; }

        /// <summary>
        /// Email theo định dạng
        /// </summary>
        public string? NormalizedEmail { get; set; }

        /// <summary>
        /// Tên tài khoản theo định dạng
        /// </summary>
        public string? NormalizedUserName {  get; set; }

        /// <summary>
        /// Tài khoản bị khoá không
        /// False: Không
        /// True: Có
        /// </summary>
        public bool? LockoutEnabled {  get; set; }

        /// <summary>
        /// Chuỗi ngẫu nhiên tăng độ bảo mật
        /// </summary>
        public string? SecurityStamp { get; set; }

        /// <summary>
        /// Xác nhận 2 lớp
        /// True: Đã có
        /// False: Chưa
        /// </summary>
        public bool? TwoFactorEnabled {  get; set; }

        /// <summary>
        /// Sau khoảng thời gian sẽ khoá tài khoản
        /// </summary>
        public int? LockoutEnd {  get; set; }

        /// <summary>
        /// Làm mới lại AccessToken
        /// </summary>
        public string? RefreshToken {  get; set; }

        /// <summary>
        /// Thời gian tồn tại RefreshToken
        /// </summary>
        public DateTime? RefreshTokenExpiryTime {  get; set; }
    }
}
