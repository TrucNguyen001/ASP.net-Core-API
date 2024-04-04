<template>
  <div class="import-employee">
    <div class="header-import">
      <div style="margin-top: -4px">
        {{ resource.MImportFile.Title }}
      </div>
    </div>
    <div class="body">
      <!-- Bước 1 -->
      <div v-if="pageIndex === 1" class="page1">
        <div class="header">
          <h3>{{ resource.MImportFile.StepOne }}</h3>
        </div>
        <div class="main">
          <div class="navbar">
            <div class="is-active-import">
              {{ resource.MImportFile.TitleStepOne }}
            </div>
            <div class="no-active">
              {{ resource.MImportFile.TitleStepTwo }}
            </div>
            <div class="no-active">
              {{ resource.MImportFile.TitleStepThree }}
            </div>
          </div>
          <div class="content">
            <div>
              <div style="margin-top: 10px">
                {{ resource.MImportFile.SelectFile }}
              </div>
              <div style="margin: 10px 0; display: inline-flex">
                <input
                  v-model="nameFile"
                  class="m-input"
                  type="text"
                  style="width: 500px"
                  readonly
                />
                <input
                  ref="input_file"
                  type="file"
                  @change="inputFileChange"
                  hidden
                />
                <button
                  @click="importFile"
                  style="margin-left: 20px"
                  class="m-button m-button-cancel"
                >
                  {{ resource.MImportFile.Select }}
                </button>
              </div>
              <div>
                {{ resource.MImportFile.HaveFile }}
                <a href="/Danh_sach_nhan_vien.xlsx">{{
                  resource.MImportFile.Here
                }}</a>
              </div>
            </div>
          </div>
        </div>
        <div class="footer">
          <div class="footer">
            <div class="footer-left">
              <div class="m-button m-button-cancel">
                {{ resource.MImportFile.Help }}
              </div>
            </div>
            <div class="footer-right">
              <div class="m-button m-button-cancel not_active back">
                {{ resource.MImportFile.Back }}
              </div>
              <div
                @click="changePage2"
                class="m-button m-button-cancel continue"
              >
                {{ resource.MImportFile.Continue }}
              </div>
              <div
                class="m-button m-button-cancel"
                @click="redirectToListEmployee"
              >
                {{ resource.MImportFile.Cancel }}
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Bước 2 -->
      <div v-if="pageIndex === 2" class="page2">
        <div class="header">
          <strong>{{ resource.MImportFile.StepTwo }}</strong>
        </div>
        <div class="main">
          <div class="navbar">
            <div class="no-active">{{ resource.MImportFile.TitleStepOne }}</div>
            <div class="is-active-import">
              {{ resource.MImportFile.TitleStepTwo }}
            </div>
            <div class="no-active">
              {{ resource.MImportFile.TitleStepThree }}
            </div>
          </div>
          <div class="content">
            <div class="content-header">
              <div class="content-title">
                {{ numberOfSuccess }} / {{ listEmployeeImport.length }}
                {{ resource.MImportFile.RowValid }}
              </div>
              <div>
                {{ numberOfFail }} / {{ listEmployeeImport.length }}
                {{ resource.MImportFile.RowNoValid }}
              </div>
            </div>
            <div class="content-main">
              <div class="table-employee">
                <table style="width: 2000px" class="m-table tbl-employee">
                  <thead>
                    <tr>
                      <th style="width: 180px">
                        {{ resource.ColumnTable.EmployeeCode }}
                      </th>
                      <th style="padding-left: -14px">
                        {{ resource.ColumnTable.EmployeeName }}
                      </th>
                      <th style="padding-right: 14px">
                        {{ resource.ColumnTable.Gender }}
                      </th>
                      <th style="padding-right: 14px">
                        {{ resource.ColumnTable.DateOfBirth }}
                      </th>
                      <th style="padding-right: 14px">
                        {{ resource.ColumnTable.PositionName }}
                      </th>
                      <th style="padding-right: 14px">
                        {{ resource.ColumnTable.DepartmentName }}
                      </th>
                      <th style="padding-right: 14px">
                        {{ resource.ColumnTable.PhoneNumber }}
                      </th>
                      <th style="padding-right: 14px">
                        {{ resource.ColumnTable.IdentificationCard }}
                      </th>
                      <th style="padding-right: 14px">
                        {{ resource.ColumnTable.BankName }}
                      </th>
                      <th style="padding-right: 14px">
                        {{ resource.ColumnTable.Status }}
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr
                      v-for="employee in listEmployeeImport"
                      :key="employee.EmployeeId"
                    >
                      <td
                        style="text-align: left; padding-left: 15px"
                        class="employee-code"
                      >
                        {{ employee.EmployeeCode }}
                      </td>
                      <td>{{ employee.FullName }}</td>
                      <td>
                        {{ this.common.changeDisplayGender(employee.Gender) }}
                      </td>
                      <td style="text-align: center; padding-left: 0">
                        {{
                          this.common.changeDisplayDate(employee.DateOfBirth)
                        }}
                      </td>
                      <td>
                        {{ employee.PositionName }}
                      </td>
                      <td>
                        {{ employee.DepartmentName }}
                      </td>
                      <td>{{ employee.PhoneNumber }}</td>
                      <td>{{ employee.IdentificationCard }}</td>
                      <td>{{ employee.BankName }}</td>
                      <td
                        :class="{
                          error_record: employee.IsImported === false,
                          success_record: employee.IsImported === true,
                        }"
                      >
                        <p
                          v-for="(error, index) in employee.ImportInvalidErrors"
                          :key="index"
                        >
                          {{ error }}
                        </p>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
              <div class="footer-table">
                {{ resource.MImportFile.DownloadFileFalse }}
                <strong @click="downloadFile(listEmployeeInsertFalse)">
                  {{ resource.MImportFile.Here }}</strong
                >.
              </div>
            </div>
          </div>
        </div>
        <div class="footer">
          <div class="footer-left">
            <div class="m-button m-button-cancel">
              {{ resource.MImportFile.Help }}
            </div>
          </div>
          <div class="footer-right">
            <div @click="changePage1" class="m-button m-button-cancel back">
              {{ resource.MImportFile.Back }}
            </div>
            <div
              class="m-button m-button-no-active continue"
              v-if="numberOfSuccess === 0"
            >
              {{ resource.MImportFile.Continue }}
            </div>
            <div v-else>
              <div
                @click="changePage3"
                class="m-button m-button-cancel continue"
              >
                {{ resource.MImportFile.Continue }}
              </div>
            </div>
            <div
              @click="redirectToListEmployee"
              class="m-button m-button-cancel"
            >
              {{ resource.MImportFile.Cancel }}
            </div>
          </div>
        </div>
      </div>

      <!-- Bước 3 -->
      <div v-if="pageIndex === 3" class="page3">
        <div class="header">
          <strong>{{ resource.MImportFile.StepThree }}</strong>
        </div>
        <div class="main">
          <div class="navbar">
            <div class="no-active">{{ resource.MImportFile.TitleStepOne }}</div>
            <div class="no-active">{{ resource.MImportFile.TitleStepTwo }}</div>
            <div class="is-active-import">
              {{ resource.MImportFile.TitleStepThree }}
            </div>
          </div>
          <div class="content">
            <div>
              <div style="margin-top: 10px">
                <h2 class="title">
                  {{ resource.MImportFile.ResultImport }}
                </h2>
              </div>
              <div style="margin: 10px 0">
                {{ resource.MImportFile.DownloadFile }}
                <strong @click="downloadFile(listEmployeeImport)">{{
                  resource.MImportFile.Here
                }}</strong
                >.
              </div>
              <div>
                <p>
                  {{ resource.MImportFile.RowSuccess }}{{ numberOfSuccess }}
                </p>
                <p>{{ resource.MImportFile.RowError }}{{ numberOfFail }}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="footer">
          <div class="footer-left">
            <div class="m-button m-button-cancel">
              {{ resource.MImportFile.Help }}
            </div>
          </div>
          <div class="footer-right">
            <div></div>
            <div
              @click="redirectToListEmployee"
              class="m-button m-button-cancel"
            >
              {{ resource.MImportFile.Close }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ImportFile",
  data() {
    return {
      listEmployeeImport: [],
      listEmployeeInsertFalse: {},
      numberOfSuccess: 0,
      numberOfFail: 0,
      pageIndex: 1,
      nameFile: "",
      file: null,
      toke: "",
    };
  },
  methods: {
    /**
     * Trở về trang danh sách nhân viên
     * @author: Nguyễn Văn Trúc (31/1/2024)
     */
    redirectToListEmployee() {
      this.$router.push(this.helper.Router.Employee);
    },

    /**
     * Hàm Thực hiện import file excel
     * @author: Nguyễn Văn Trúc (29/1/2024)
     */
    importFile: function () {
      this.$refs["input_file"].click();
    },
    /**Hàm thực hiện kiểm tra xem file chọn đúng định dạng không
     * @param {Tên file truyền vào để kiểm tra} fileName
     * @author: Nguyễn Văn Trúc(30/01/2024)
     */
    isExcelFile(fileName) {
      try {
        // Chuyển đổi tên tệp thành chữ thường
        let lowerCaseFileName = fileName.toLowerCase();
        // Kiểm tra phần mở rộng
        return (
          lowerCaseFileName.endsWith(".xls") ||
          lowerCaseFileName.endsWith(".xlsx")
        );
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm thực hiện lấy file đầu tiên
     * @author: Nguyễn Văn Trúc(30/01/2024)
     */
    inputFileChange() {
      try {
        this.selectedFile = event.target.files[0];
        this.uploadFile(this.selectedFile);
        this.selectedFile = null;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm thực hiện import dữ liệu từ file
     * @param {*file lựa chọn} file
     * @author: Nguyễn Văn Trúc(30/01/2024)
     */
    uploadFile(file) {
      try {
        let me = this;
        if (!me.isExcelFile(file.name)) {
          // Hiển thị dialog
          me.common.showDialog(
            me.resource.ConfirmImportFileError.Content,
            me.resource.ConfirmImportFileError.Title,
            me.helper.TypeIcon.Error
          );
        } else {
          this.nameFile = file.name;
          this.file = file;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Thay đổi trang
     * @author: Nguyễn Văn Trúc(30/01/2024)
     */
    changePage1() {
      this.pageIndex = 1;
    },
    /**
     * load dữ liệu kiểm tra lỗi
     * @author: Nguyễn Văn Trúc(30/01/2024)
     */
    async changePage2() {
      try {
        var me = this;
        if (me.nameFile === "") {
          // Hiển thị dialog
          me.common.showDialog(
            me.resource.NotFoundFile.Content,
            me.resource.NotFoundFile.Title,
            me.helper.TypeIcon.Error
          );
        } else {
          await me.commitData(false);
          if (me.listEmployeeImport.length !== 0) {
            me.pageIndex = 2;
          }
          me.common.showLoading(false);
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Thêm bản ghi vào trong database
     * @author: Nguyễn Văn Trúc(31/01/2024)
     */
    changePage3() {
      try {
        this.commitData(true);
        this.pageIndex = 3;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thông báo thi không có bản ghi nào thành công
     * @author: Nguyễn Văn Trúc(16/03/2024)
     */
    noChangePage3() {
      try {
        this.common.showToast(
          this.resource.MImportFile.ToastError.Content,
          this.helper.TypeIcon.Error
        );
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Gửi api để thêm dữ liệu
     * @param {Biến bool dùng kiểm tra có commit hay không} isCommit
     * @author: Nguyễn Văn Trúc(30/01/2024)
     */
    async commitData(isCommit) {
      try {
        let me = this;
        me.common.showLoading();
        me.listEmployeeImport = await me.apiService.importFile(
          me.helper.MApi.ImportListEmployee,
          isCommit,
          me.file
        );
        me.listEmployeeInsertFalse = me.listEmployeeImport.filter(
          (employee) => employee.IsImported === false
        );
        me.numberOfFail = me.listEmployeeInsertFalse.length;
        me.numberOfSuccess = me.listEmployeeImport.length - me.numberOfFail;
        me.common.showLoading(false);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Tải những bản ghi thất bại
     * @author: Nguyễn Văn Trúc(29/2/2024)
     */
    async downloadFile(listEmployeeExport) {
      this.common.showLoading();
      listEmployeeExport.forEach(function (number) {
        number.Email = "";
      });
      await this.apiService.downloadFileExcel(listEmployeeExport);
      this.common.showLoading(false);
    },
  },
};
</script>

<style scoped>
@import url(../../css/pages/importfile.css);
.page2 strong {
  margin-left: 4px;
}
strong {
  cursor: pointer;
}
.page1 a {
  text-decoration: none;
  color: black;
  font-weight: 700;
}
.navbar div {
  cursor: default;
}
.navbar div:hover {
  background-color: initial;
}
.navbar .is-active:hover {
  background: #66afff;
}
.error_record {
  color: red;
}
.success_record {
  color: green;
}
.m-button {
  display: flex;
  align-items: center;
  justify-content: center;
  border: 1px solid #bdbdbd;
}
.import-employee .navbar {
  padding-top: 0;
  padding-left: 0;
}
.import-employee .navbar > div {
  border-radius: 0;
}
.no-active {
  background: #f5f5f5;
}
.is-active-import {
  background: #66afff;
}
</style>
