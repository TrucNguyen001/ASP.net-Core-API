using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Infrastructure.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace demo_infastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>, IDisposable where MISAEntity : class
    {
        #region Fileds
        // Chuỗi kết nối
        IMISADbContext _dbContext;
        IUnitOfWork _unitOfWork;

        // Lấy ra tên class
        private string _nameClass;
        #endregion

        #region Constructor
        public BaseRepository(IMISADbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _nameClass = typeof(MISAEntity).Name;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>Trả về tất cả bản ghi</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public IEnumerable<MISAEntity> GetAll()
        {
            var misaEntity = _dbContext.Connection.Query<MISAEntity>($"SELECT * FROM {_nameClass}");

            return misaEntity;
        }

        /// <summary>
        /// Lấy dữ liệu theo Id
        /// </summary>
        /// <param name="misaEntityId">Id Entity</param>
        /// <returns>Trả về bản ghi theo Id</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public MISAEntity GetById(Guid misaEntityId)
        {
            var sqlCommand = $"SELECT * FROM {_nameClass} WHERE {_nameClass}Id = @misaEntityId";

            DynamicParameters paramet = new DynamicParameters();

            paramet.Add("@misaEntityId", misaEntityId);

            var misaEntity = _dbContext.Connection.QueryFirstOrDefault<MISAEntity>(sql: sqlCommand, param: paramet);

            return misaEntity;
        }

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="misaEntity">Entity</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public int Insert(MISAEntity misaEntity)
        {
            var entityInsert = _dbContext.Insert<MISAEntity>(misaEntity);
            return entityInsert;
        }

        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="misaEntity">Entity</param>
        /// <param name="misaEntityId">Entity Id</param>
        /// <returns>Trả về 1 nếu sửa thành công</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public int Update(MISAEntity misaEntity, Guid misaEntityId)
        {
            var entityUpdate = _dbContext.Update<MISAEntity>(misaEntity, misaEntityId);
            return entityUpdate;
        }

        /// <summary>
        /// Xoá dữ liệu theo Id
        /// </summary>
        /// <param name="misaEntityId">Id Entity</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public int Delete(Guid misaEntityId)
        {
            var entityDelete = _dbContext.Delete<MISAEntity>(misaEntityId);
            return entityDelete;
        }



        /// <summary>
        /// ngắt khi thực hiện yêu cầu xong
        /// </summary>
        public void Dispose()
        {
            _dbContext.Connection.Dispose();
        }


        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="misaEntityIds">Id của những bản ghi muốn xoá</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// CreatedBy: NVTruc(2/1/2024)
        public int MultipleDelete(List<Guid> misaEntityIds)
        {
            var entitysDelete = _dbContext.MultipleDelete<MISAEntity>(misaEntityIds);
            return entitysDelete;
        }

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="records">Danh sách bản ghi muốn thêm</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreatedBy: Nguyễn Văn Trúc(8/3/2024)
        public int MultiplePost(List<MISAEntity> records)
        {
            var entitysPost = _dbContext.MultiplePost<MISAEntity>(records);
            return entitysPost;
        }


        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="misaEntity">Mã bản ghi</param>
        /// <returns>
        /// false: Mã chưa tồn tại
        /// true: Mã đã tồn tại</returns>
        /// CreateBy: NVTruc(28/12/2023)
        public bool CheckDuplicateCode(string misaEntityCode)
        {
            var sqlCommand = $"SELECT * FROM {_nameClass} WHERE {_nameClass}Code = @misaEntityCode";

            DynamicParameters paramet = new DynamicParameters();

            paramet.Add("@misaEntityCode", misaEntityCode);

            var misaEntity = _dbContext.Connection.QueryFirstOrDefault(sql: sqlCommand, param: paramet);

            if (misaEntity == null)
            {
                return false;
            }

            return true;

        }

        #endregion
    }
}
