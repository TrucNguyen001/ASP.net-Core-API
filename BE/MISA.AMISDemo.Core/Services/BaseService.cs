using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.ValidateException;
using MISA.AMISDemo.Core.MISAAttribute;
using MISA.AMISDemo.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity> where MISAEntity : class
    {
        #region Fileds
        IBaseRepository<MISAEntity> _baseRepository;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        #endregion

        #region
        /// <summary>
        /// Validate thêm dữ liệu
        /// </summary>
        /// <param name="misaEntity">Entity muốn thêm</param>
        /// <returns>Trả về 1 nếu validate thành công</returns>
        /// CreatedBy: NVTruc(27/12/2023)
        public int InsertService(MISAEntity misaEntity)
        {
            CheckValidateLength(misaEntity);
            CheckValidate(misaEntity);
            CheckValidateDate(misaEntity);
            MISAValidate(misaEntity, (int)MISAEnum.Status.INSERT);
            return 1;
        }

        /// <summary>
        /// Validate cập nhật dữ liệu
        /// </summary>
        /// <param name="misaEntity">Entity muốn sửa</param>
        /// <param name="misaEntityId">Id Entity muốn sửa</param>
        /// <returns>Trả về 1 nếu cập nhật thành công</returns>
        /// CreatedBy: NVTruc(27/12/2023)
        public int UpdateService(MISAEntity misaEntity, Guid misaEntityId)
        {
            CheckValidateLength(misaEntity);
            CheckValidate(misaEntity);
            CheckValidateDate(misaEntity);
            MISAValidate(misaEntity, (int)MISAEnum.Status.UPDATE);
            return 1;
        }


        /// <summary>
        /// Validate những dữ liệu bắt buộc nhập
        /// </summary>
        /// <param name="misaEntity">Entity kiểm tra</param>
        /// <exception cref="MISAValidateException"></exception>
        /// CreatedBy: NVTruc(27/12/2023)
        private void CheckValidate(MISAEntity misaEntity)
        {
            var propNoEmpties = typeof(MISAEntity).GetProperties().Where(p => Attribute.IsDefined(p, typeof(NotEmpty)));


            foreach (var prop in propNoEmpties)
            {
                var propValue = prop.GetValue(misaEntity);
                var propName = (prop.GetCustomAttributes(typeof(ProppertyName), true)[0] as ProppertyName).Name;

                var nameDisplay = (propName.Length == 0) ? prop.Name : propName;
                if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                {
                    throw new MISAValidateException($"{nameDisplay} {MISAResourceVN.InformationBlank}");
                }
            }
        }


        /// <summary>
        /// Kiểm tra validate date: Không nhập lớn hơn ngày hiện tại
        /// </summary>
        /// <param name="misaEntity">Entity muốn kiểm tra</param>
        /// <exception cref="MISAValidateException"></exception>
        /// CreatedBy: NVTruc(27/12/2023)
        private void CheckValidateDate(MISAEntity misaEntity)
        {
            var propValidateDate = typeof(MISAEntity).GetProperties().Where(p => Attribute.IsDefined(p, typeof(ValidateDate)));

            foreach (var prop in propValidateDate)
            {
                var propValue = prop.GetValue(misaEntity);

                var propName = (prop.GetCustomAttributes(typeof(ProppertyName), true)[0] as ProppertyName).Name;

                var nameDisplay = (propName.Length == 0) ? prop.Name : propName;

                if (propValue != null)
                {
                    DateTime dateTime = (DateTime)propValue;
                    if (dateTime > DateTime.Now)
                    {
                        throw new MISAValidateException($"{nameDisplay} {MISAResourceVN.InformatonDate}");
                    }
                }
            }
        }

        /// <summary>
        /// Hàm kiểm tra độ dài của của các thuộc tính
        /// </summary>
        /// <param name="misaEntity">Bản ghi muốn kiểm tra</param>
        /// CreatedBy: NVTruc(20/3/2024)
        public void CheckValidateLength(MISAEntity misaEntity)
        {
            var propValidateDate = typeof(MISAEntity).GetProperties().Where(p => Attribute.IsDefined(p, typeof(MaxLength)));

            foreach (var prop in propValidateDate)
            {
                var propValue = prop.GetValue(misaEntity);

                var propLength = (prop.GetCustomAttributes(typeof(MaxLength), true)[0] as MaxLength).Length;

                if(propValue != null && propValue.ToString().Length > propLength)
                {
                    throw new MISAValidateException($"{prop} {MISAResourceVN.MaxLength} {propLength} {MISAResourceVN.Char}");
                }
            }
        }

        /// <summary>
        /// Kiểm tra validate của class cha cho class con bổ sung thêm
        /// </summary>
        /// <param name="misaEntity">Entity muốn kiêm tra</param>
        /// <param name="status">Trạng thái bản ghi</param>
        /// CreatedBy: NVTruc(27/12/2023)
        protected virtual void MISAValidate(MISAEntity misaEntity, int status)
        {
        }
        #endregion
    }
}