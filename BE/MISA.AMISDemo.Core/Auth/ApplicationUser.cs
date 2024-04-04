using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Auth
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Trả về token
        /// </summary>
        public string? RefreshToken {  get; set; }

        /// <summary>
        /// Lưu trữ thời gian hết hạn
        /// </summary>
        public DateTime RefreshTokenExpiryTime {  get; set; }
    }
}
