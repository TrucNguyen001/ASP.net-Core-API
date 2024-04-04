import axios from "axios";
import helper from "../helper/helper";
const URL = `${helper.URL}api/v1/`;

const apiService = {
  /**
   * Hàm dùng để phân trang
   * @param {Số lượng trang} pageSize
   * @param {Vị trí trang} pageNumber
   * @param {Thông tin tìm kiêm} InfoSearch
   * @returns danh sách bản ghi tìm thấy
   * @author: Nguyễn Văn Trúc(4/3/2024)
   */
  async loadFilter(pageSize, pageNumber, infoSearch) {
    let url = `${URL}Employee/paging?pageSize=${pageSize}&pageIndex=${pageNumber}&text=${infoSearch}`;
    try {
      return await axios
        .get(url)
        .then((response) => {
          return response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
      throw error;
    }
  },

  /**
   * Hàm tải file về
   * @param {Danh sách tải về} listEmployeeExport
   * @author: Nguyễn Văn Trúc(4/3/2024)
   */
  async downloadFileExcel(listEmployeeExport) {
    let url = `${URL}Employee/ExportFile`;
    try {
      await axios
        .post(url, listEmployeeExport, {
          responseType: helper.DownLoadFile.ResponseType,
        })
        .then(function (response) {
          if (!response || !response.data) {
            return;
          }

          let url = window.URL.createObjectURL(new Blob([response.data]));
          let link = document.createElement(helper.DownLoadFile.Tag);
          link.href = url;
          link.setAttribute(
            helper.DownLoadFile.Atr,
            helper.DownLoadFile.NameFile
          );
          document.body.appendChild(link);
          link.click();
          window.URL.revokeObjectURL(url);
        })
        .catch(function (error) {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Xoá nhiều bản ghi
   * @param {Tên bản ghi} nameRecord
   * @param {Danh sách mã muốn xoá} misaEntityIds
   * @returns Trả về 1 nếu xoá thành công
   * @author: Nguyễn Văn Trúc(4/3/2024)
   */
  async deleteMultiple(nameRecord, misaEntityIds) {
    try {
      let url = `${URL}${nameRecord}/multipleDelete`;
      return await axios
        .delete(url, {
          data: misaEntityIds,
        })
        .then((response) => {
          return response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Xoá 1 bản ghi
   * @param {Tên bản ghi} nameRecord
   * @param {Id muốn xoá} misaEntityId
   * @returns Trả về 1 nếu xoá thành công
   * @author: Nguyễn Văn Trúc(4/3/2024)
   */
  async delete(nameRecord, misaEntityId) {
    try {
      let url = `${URL}${nameRecord}/${misaEntityId}`;
      return await axios
        .delete(url)
        .then((response) => {
          return response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * lấy giá trị theo đường dẫn
   * @returns Danh sách giá trị lấy được
   * @author: Nguyễn Văn Trúc(4/3/2024)
   */
  async get(path) {
    let url = `${URL}${path}`;
    try {
      return await axios
        .get(url)
        .then((response) => {
          return response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Đăng bản ghi
   * @param {Đường dẫn} path
   * @param {Bản ghi} record
   * @returns Trả về 1 nếu đăng thành công
   * Nếu là login, refresh token: Trả về thông tin như token, refresh token, expiration
   * @author: Nguyễn Văn Trúc(4/3/2024)
   */
  async post(path, record) {
    try {
      let url = `${URL}${path}`;
      return await axios
        .post(url, record)
        .then(function (response) {
          return response.data;
        })
        .catch(function (error) {
          console.error(error);
        });
    } catch (error) {
      console.error(error);
    }
  },

  /**
   * Cập nhật bản ghi
   * @param {Tên bản ghi} nameRecord
   * @param {Bản ghi muốn sửa} record
   * @param {Id bản ghi muốn sửa} recordId
   * @returns Trả về 1 nếu sửa thành công
   * @author: Nguyễn Văn Trúc(4/3/2024)
   */
  async update(nameRecord, recordId, record) {
    try {
      let url = `${URL}${nameRecord}/${recordId}`;
      return await axios
        .put(url, record)
        .then(function (response) {
          return response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Lấy bản ghi theo Id
   * @param {Tên bản ghi} nameRecord
   * @param {Thông tin bản ghi} misaEntityInfo
   * @returns Trả về bản ghi lấy được
   * @author: Nguyễn Văn Trúc(4/3/2024)
   */
  async getByInfo(nameRecord, misaEntityInfo) {
    try {
      let url = `${URL}${nameRecord}/${misaEntityInfo}`;
      return await axios
        .get(url)
        .then((response) => {
          return response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
    }
  },

  /**
   *
   * @param {Biến bool dùng kiểm tra có commit hay không} isCommit
   * @param {file thêm} file
   * @returns Trả về danh sách bản ghi thêm
   * @author: Nguyễn Văn Trúc(4/3/2024)
   */
  async importFile(path, isCommit, file) {
    try {
      let url = `${URL}${path}/${isCommit}`;
      // Tạo formData
      let formData = new FormData();
      formData.append("fileImport", file);

      // Gửi yêu cầu đến api
      return axios
        .post(url, formData)
        .then((response) => {
          return response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    } catch (error) {
      console.log(error);
    }
  },
};
export default apiService;
