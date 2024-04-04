using MISA.AMISDemo.Core.Const;
using MISA.AMISDemo.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    /// <summary>
    /// Chức vụ
    /// </summary>
    public class Position:BaseEntities
    {
        #region Property
        /// <summary>
        /// Khoá chính: Id chức vụ
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Mã chức vụ
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.PositionCode)]
        public string PositionCode { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.PositonName)]
        public string PositionName { get; set; }

        /// <summary>
        /// Mô tả chức vụ
        /// </summary>
        public string? Description { get; set; }

        #endregion
    }
}
