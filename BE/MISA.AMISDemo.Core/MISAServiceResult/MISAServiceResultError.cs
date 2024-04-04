using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.ErrorsServe
{
    public class MISAServiceResultError
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
        /// Dữ liệu
        /// </summary>
        public IDictionary? Data { get; set; }

        /// <summary>
        /// Chuỗi lỗi
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();
    }
}
