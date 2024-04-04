using MISA.AMISDemo.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace MISA.AMISDemo.Infrastructure.MISADatabaseContext
{
    public class MySqlDbContext : IMISADbContext
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
        protected IUnitOfWork UnitOfWork;
        private IUnitOfWork unitOfWork;

        public MySqlDbContext(IConfiguration config, IUnitOfWork unitOfWork)
        {
            Connection = new MySqlConnection(config.GetConnectionString("Database1"));
            UnitOfWork = unitOfWork;
        }

        public MySqlDbContext(string conection, IUnitOfWork unitOfWork)
        {
            Connection = new MySqlConnection(conection);
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <typeparam name="MISAEntity">Class</typeparam>
        /// <param name="misaEntity">Bản ghi muốn thêm</param>
        /// <returns>1 nếu thêm thành công</returns>
        /// CreatedBy: Nguyễn Văn Trúc (1/1/2024)
        public int Insert<MISAEntity>(MISAEntity misaEntity)
        {
            var _nameClass = typeof(MISAEntity).Name;
            string colNameList = "";
            string colPramList = "";

            var props = typeof(MISAEntity).GetProperties();

            DynamicParameters paramet = new DynamicParameters();

            paramet.Add($"@{props[0].Name}", Guid.NewGuid());
            colNameList += $"{props[0].Name},";
            colPramList += $"@{props[0].Name},";

            for (int i = 1; i < props.Length; i++)
            {
                colNameList += $"{props[i].Name},";
                colPramList += $"@{props[i].Name},";

                paramet.Add($"@{props[i].Name}", props[i].GetValue(misaEntity));
            }


            colNameList = colNameList.Substring(0, colNameList.Length - 1);
            colPramList = colPramList.Substring(0, colPramList.Length - 1);

            var sqlCommand = $"INSERT INTO {_nameClass}({colNameList}) VALUES({colPramList})";

            var misaEntityInsert = UnitOfWork.Connection.Execute(sql: sqlCommand, param: paramet, transaction: UnitOfWork.Transaction);

            return 1;
        }

        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="misaEntity">Entity</param>
        /// <param name="misaEntityId">Entity Id</param>
        /// <returns>Trả về 1 nếu sửa thành công</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public int Update<MISAEntity>(MISAEntity misaEntity, Guid misaEntityId)
        {
            var _nameClass = typeof(MISAEntity).Name;
            string stringUpdate = "";

            var props = typeof(MISAEntity).GetProperties();

            DynamicParameters paramet = new DynamicParameters();

            paramet.Add("@misaEntityId", misaEntityId);

            for (int i = 1; i < props.Length; i++)
            {
                stringUpdate += $"{props[i].Name} = @{props[i].Name},";

                paramet.Add($"@{props[i].Name}", props[i].GetValue(misaEntity));
            }


            stringUpdate = stringUpdate.Substring(0, stringUpdate.Length - 1);

            var sqlCommand = $"UPDATE {_nameClass} SET {stringUpdate} WHERE {_nameClass}Id = @misaEntityId";

            var misaEntityUpdate = UnitOfWork.Connection.Execute(sql: sqlCommand, param: paramet, transaction: UnitOfWork.Transaction);
            return 1;
        }

        /// <summary>
        /// Xoá dữ liệu theo Id
        /// </summary>
        /// <param name="misaEntityId">Id Entity</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public int Delete<MISAEntity>(Guid misaEntityId)
        {
            var _nameClass = typeof(MISAEntity).Name;

            var sqlCommand = $"DELETE FROM {_nameClass} WHERE {_nameClass}Id = @misaEntityId";

            DynamicParameters paramet = new DynamicParameters();

            paramet.Add("@misaEntityId", misaEntityId);

            var misaEntity = UnitOfWork.Connection.Execute(sql: sqlCommand, param: paramet, transaction: UnitOfWork.Transaction);
            return 1;
        }

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="misaEntityIds">Id của những bản ghi muốn xoá</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// CreatedBy: NVTruc(2/1/2024)
        public int MultipleDelete<Type>(List<Guid> misaEntityIds)
        {
            var _nameClass = typeof(Type).Name;
            // Tạo danh sách tham số và chuỗi các tham số
            var paramet = new DynamicParameters();
            var parametList = new List<string>();

            // Tạo tham số cho mỗi ID trong danh sách
            for (int i = 0; i < misaEntityIds.Count; i++)
            {
                var parametName = $"@Id{i}";
                paramet.Add(parametName, misaEntityIds[i]);
                parametList.Add(parametName);
            }

            // Tạo câu lệnh SQL sử dụng IN và thực hiện xóa
            var sqlCommand = $"DELETE FROM {_nameClass} WHERE {_nameClass}Id IN ({string.Join(", ", parametList)})";

            var delete = UnitOfWork.Connection.Execute(sql: sqlCommand, param: paramet, transaction: UnitOfWork.Transaction);

            return 1;
        }

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="records">Danh sách bản ghi muốn thêm</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreatedBy: Nguyễn Văn Trúc(8/3/2024)
        public int MultiplePost<Type>(List<Type> records)
        {
            var _nameClass = typeof(Type).Name;
            string colNameList = "";

            var props = typeof(Type).GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                colNameList += $"{props[i].Name},";
            }
            colNameList = colNameList.Substring(0, colNameList.Length - 1);

            var sqlCommand = $"INSERT INTO {_nameClass}({colNameList}) VALUES ";

            DynamicParameters paramet = new DynamicParameters();

            // Gán vị trí 
            var index = 0;
            foreach (var item in records)
            {
                string colPramList = "(";
                colPramList += $"@{props[0].Name}{index},";
                paramet.Add($"@{props[0].Name}{index}", Guid.NewGuid());

                for (int i = 1; i < props.Length; i++)
                {
                    colPramList += $"@{props[i].Name}{index},";
                    paramet.Add($"@{props[i].Name}{index}", props[i].GetValue(item));
                }
                colPramList = colPramList.Substring(0, colPramList.Length - 1);
                colPramList += "),";

                sqlCommand += colPramList;

                // Thay đổi vị trí tránh trùng trong pram
                index++;
            }
            // Xóa dấu phẩy cuối cùng và thêm dấu chấm phẩy vào cuối câu lệnh SQL
            sqlCommand = sqlCommand.TrimEnd(',') + ";";

            var misaEntityInsert = UnitOfWork.Connection.Execute(sql: sqlCommand, param: paramet, transaction: UnitOfWork.Transaction);

            return 1;
        }


    }
}
