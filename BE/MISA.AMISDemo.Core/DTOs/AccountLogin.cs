using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs
{
    public class AccountLogin
    {
        /// <summary>
        /// Tài khoản
        /// </summary>
        public string Account {  get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
    }
}
