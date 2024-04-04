using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Auth
{
    public class TokenModel
    {
        /// <summary>
        /// Gửi token
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// trả về token
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Thời gian hết hạn token
        /// </summary>
        public DateTime? Expiration { get; set;}
    }
}
