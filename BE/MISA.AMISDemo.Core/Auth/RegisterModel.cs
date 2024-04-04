using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Auth
{
    public class RegisterModel
    {
        /// <summary>
        /// Tài khoản
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber {  get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
    }
}
