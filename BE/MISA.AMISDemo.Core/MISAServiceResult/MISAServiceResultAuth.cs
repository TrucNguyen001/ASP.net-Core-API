using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.MISAServiceResult
{
    public class MISAServiceResultAuth
    {
        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// Status
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông báo lỗi cho dev
        /// </summary>
        public string DevMsg { get; set; }
    }
}
