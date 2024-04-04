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
    /// Phòng ban
    /// </summary>
    public class Department:BaseEntities
    {
        #region Property
        /// <summary>
        /// Khoá chính: Id phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.DepartmentCode)]
        public string DepartmentCode {  get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.DepartmentName)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả phòng ban
        /// </summary>
        public string? Description { get; set; }
        #endregion
    }
}
