using MISA.AMISDemo.Core.Const;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    /// <summary>
    /// Nhóm khách hàng
    /// </summary>
    public class CustomerGroup:BaseEntities
    {
        #region Property
        /// <summary>
        /// Khoá chính: Id nhóm khách hàng
        /// </summary>
        public Guid CustomerGroupId { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.CustomerGroupName)]
        public string CustomerGroupName {  get; set; }
        #endregion
    }
}
