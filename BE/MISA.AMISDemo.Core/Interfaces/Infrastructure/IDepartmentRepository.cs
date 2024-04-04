using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Infrastructure
{
    public interface IDepartmentRepository:IBaseRepository<Department>
    {
        /// <summary>
        /// Lấy dữ liệu theo name
        /// </summary>
        /// <param name="departmentName">Tên bộ phận muốn lấy muốn lấy</param>
        /// <returns>Trả về bản ghi tìm</returns>
        /// CreatedBy: NVTruc(26/12/2023)
        Department GetByName(string departmentName);
    }
}
