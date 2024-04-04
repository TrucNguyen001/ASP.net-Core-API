using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infrastructure.Interfaces
{
    public interface IMISADbContext
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }

        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <typeparam name="Type">Class</typeparam>
        /// <param name="type">Bản ghi</param>
        /// <returns>Trả về 1: Thêm thành công</returns>
        /// CreatedBy: Nguyễn Văn Trúc(1/1/2024)
        int Insert<Type>(Type type);

        /// <summary>
        /// Hàm cập nhật bản ghi
        /// </summary>
        /// <typeparam name="Type">Class</typeparam>
        /// <param name="type">Bản ghi</param>
        /// <returns>Trả về 1: Sửa thành công</returns>
        /// CreatedBy: Nguyễn Văn Trúc(1/1/2024)
        int Update<Type>(Type type, Guid typeId);

        /// <summary>
        /// Xoá dữ liệu theo Id
        /// </summary>
        /// <param name="misaEntityId">Id Entity</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        int Delete<Type>(Guid misaEntityId);

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="misaEntityIds">Id của những bản ghi muốn xoá</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreatedBy: NVTruc(2/1/2024)
        int MultipleDelete<Type>(List<Guid> misaEntityIds);

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="records">Danh sách bản ghi muốn thêm</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreatedBy: Nguyễn Văn Trúc(8/3/2024)
        int MultiplePost<Type>(List<Type> records);
    }
}
