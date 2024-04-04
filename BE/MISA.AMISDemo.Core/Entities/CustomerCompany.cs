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
    /// Công ty khách hàng
    /// </summary>
    public class CustomerCompany : BaseEntities
    {
        #region Property
        /// <summary>
        /// Khoá chính: Id nhóm khách hàng
        /// </summary>
        public Guid CustomerCompanyId { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        [NotEmpty]
        [ProppertyName(MISAConst.CustomerCompanyName)]
        public string CustomerCompanyName { get; set; }
        #endregion
    }
}
