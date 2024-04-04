using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs
{
    internal class EmployeeImport : Employee
    {
        #region Constructor
        public EmployeeImport() {
            ImportInvalidErrors = new List<string>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// Lưu thông báo lỗi
        /// </summary>
        public List<string>? ImportInvalidErrors { get; set; } = new List<string>();

        /// <summary>
        /// true: Thêm được
        /// false: Không thêm được
        /// </summary>
        public bool IsImported { get; set; } = false;

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
        #endregion
    }
}
