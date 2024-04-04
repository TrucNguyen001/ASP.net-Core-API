using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<MISAEntity> where MISAEntity : class
    {

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Trả về toàn bộ bản ghi</returns>
        /// CreatedBy: NVTruc(26/12/2023)
        IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// Lấy dữ liệu theo Id
        /// </summary>
        /// <param name="misaEntityId">Id Entity muốn lấy</param>
        /// <returns>Trả về bản nghi tìm</returns>
        /// CreatedBy: NVTruc(26/12/2023)
        MISAEntity GetById(Guid misaEntityId);

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="misaEntity">Entity</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreatedBy: NVTruc(26/12/2023)
        int Insert(MISAEntity misaEntity);



        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="misaEntity">Entity</param>
        /// <param name="misaEntityId">Id Entity muốn sửa</param>
        /// <returns>Trả về 1 nếu sửa thành công</returns>
        /// CreatedBy: NVTruc(26/12/2023)
        int Update(MISAEntity misaEntity, Guid misaEntityId);

        /// <summary>
        /// Xoá dữ liệu theo Id
        /// </summary>
        /// <param name="misaEntityId">Id Entity muốn xoá</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreatedBy: NVTruc(26/12/2023)
        int Delete(Guid misaEntityId);

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="misaEntityIds">Id của những bản ghi muốn xoá</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreatedBy: NVTruc(2/1/2024)
        int MultipleDelete(List<Guid> misaEntityIds);

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="records">Danh sách bản ghi muốn thêm</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreatedBy: Nguyễn Văn Trúc(8/3/2024)
        int MultiplePost(List<MISAEntity> records);

        /// <summary>
        /// Kiểm tra trùng mã 
        /// </summary>
        /// <param name="misaEntityIdCode">Mã kiểm tra</param>
        /// <returns>
        /// true: Mã đã tồn tại
        /// false: Mã chưa tồn tại
        /// </returns>
        /// CreatedBy: NVTruc(26/12/2023)
        bool CheckDuplicateCode(string misaEntityIdCode);
    }
}
