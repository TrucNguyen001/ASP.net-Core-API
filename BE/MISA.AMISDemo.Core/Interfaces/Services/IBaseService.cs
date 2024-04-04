using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="MISAEntity"> Entity</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreatedBy: NVTruc(25/12/2023)
        int InsertService(MISAEntity misaEntity);

        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="misaEntity">Entity</param>
        /// <param name="misaEntityId">Entity Id</param>
        /// <returns>Trả về 1 nếu sửa thành công</returns>
        /// CreatedBy: NVTruc(25/12/2023)
        int UpdateService(MISAEntity misaEntity, Guid misaEntityId);
    }
}