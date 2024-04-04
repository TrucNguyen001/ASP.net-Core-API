using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Infrastructure
{
    public interface IPositionRepository:IBaseRepository<Position>
    {
        /// <summary>
        /// Lấy dữ liệu theo name
        /// </summary>
        /// <param name="PositionName">Tên Chức danh muốn lấy muốn lấy</param>
        /// <returns>Trả về bản ghi tìm</returns>
        /// CreatedBy: NVTruc(26/12/2023)
        Position GetByName(string positionName);
    }
}
