using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.MISAEnum
{
    /// <summary>
    /// Giới tính
    /// CreateBy: NVTruc(28/12/2023)
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Nam
        /// </summary>
        MALE = 0,

        /// <summary>
        /// Nữ
        /// </summary>
        FEMALE = 1,

        /// <summary>
        /// Khác
        /// </summary>
        OTHER = 2
    }

    /// <summary>
    /// Tình trạng công việc
    /// CreateBy: NVTruc(28/12/2023)
    /// </summary>
    public enum WorkStatus
    {
        /// <summary>
        /// Đang làm việc
        /// </summary>
        WORKING = 1,

        /// <summary>
        /// Dừng làm việc
        /// </summary>
        STOP = 2
    }

    /// <summary>
    /// Trạng thái bản ghi
    /// CreateBy: NVTruc(28/12/2023)
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        INSERT = 1,

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        UPDATE = 2,
    }
}
