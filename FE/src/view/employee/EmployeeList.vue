<template>
  <div class="content">
    <div class="content-header">
      <div class="content-title">
        {{ resource.MEmployeeList.ManagerEmployee }}
      </div>
      <div>
        <button
          v-on:click="addEmployee"
          class="m-button m-button-success btn-add-employee"
        >
          {{ resource.MEmployeeList.AddEmployee }}
        </button>
      </div>
    </div>
    <div class="content-main">
      <div class="tbl-header">
        <div
          class="select-employee header-table-left"
          v-show="listEmployeeId.length !== 0"
        >
          <div>
            {{ resource.MEmployeeList.Selected
            }}<strong>{{ this.listEmployeeId.length }}</strong>
          </div>
          <div @click="unSelect" style="cursor: pointer">
            {{ resource.MEmployeeList.UnSelect }}
          </div>
          <div class="button-icon">
            <button @click="deleteEmployees" class="m-button delete-all">
              {{ resource.MEmployeeList.DeleteAll }}
            </button>
            <div class="icon icon-delete-all"></div>
          </div>
          <!-- <div class="delete-records" @mouseleave="closeFormDelete">
            <div class="content">
              {{ resource.MEmployeeList.BatchExecution }}
            </div>
            <div class="delete-employee">
              <div
                @click="toggleDelete"
                class="icon icon-caret-down-small"
              ></div>
              <div
                @click="deleteEmployees"
                :class="{ delete: isDelete }"
                class="delete-employee-child"
              >
                {{ resource.MEmployeeList.Delete }}
              </div>
            </div>
          </div> -->
        </div>
        <div class="tbl-header-employee">
          <div class="input-icon">
            <input
              @click="blackenInputSearch"
              ref="input_search"
              v-model="InfoSearch"
              :placeholder="resource.MEmployeeList.Search"
              type="text"
              id="input_search"
              class="m-input m-input-icon"
              @keydown.enter="enterSearchInput"
            />
            <div @click="searchEmployee" class="icon icon-search"></div>
          </div>
          <div @click="reload" class="load-data icon icon-reload"></div>
          <div class="icon icon-file" @click="redirectToImportEmployee"></div>
          <div @click="downloadFile" class="icon-export"></div>
        </div>
        <div style="margin-right: 20px" class="tbl-header-employee"></div>
      </div>
      <div class="table-employee">
        <table class="m-table tbl-employee">
          <thead>
            <tr>
              <th @click="selectAllRecord(filterObject.pageNumber)">
                <input
                  :checked="
                    listPageSelectAllRecord.includes(filterObject.pageNumber)
                  "
                  class="input-checkbox th fixed-column-left"
                  type="checkbox"
                />
              </th>
              <th>{{ resource.ColumnTable.EmployeeCode }}</th>
              <th>{{ resource.ColumnTable.EmployeeName }}</th>
              <th>{{ resource.ColumnTable.Gender }}</th>
              <th>{{ resource.ColumnTable.DateOfBirth }}</th>
              <th :title="resource.ColumnTable.TitleIdentificationCard">
                {{ resource.ColumnTable.IdentificationCard }}
              </th>
              <th>{{ resource.ColumnTable.PositionName }}</th>
              <th>{{ resource.ColumnTable.DepartmentName }}</th>
              <th>{{ resource.ColumnTable.BankAccount }}</th>
              <th>{{ resource.ColumnTable.BankName }}</th>
              <th :title="resource.ColumnTable.TitleBankAddress">
                {{ resource.ColumnTable.BankAddress }}
              </th>
              <th>
                {{ resource.ColumnTable.Feature }}
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              :class="{
                selected: this.checkRowSelected(employee),
              }"
              v-for="(employee, index) in listEmployee"
              :key="employee.EmployeeId"
              @mouseover="getEmployeeIdWhenMove(employee)"
              @mouseleave="handleMouseLeave"
            >
              <td
                :class="{
                  selected: this.checkRowSelected(employee),
                  background_default: !this.checkRowSelected(employee),
                }"
                @click="selectEmployee(employee.EmployeeId, index)"
              >
                <input
                  :checked="this.checkRowSelected(employee)"
                  class="input-checkbox td fixed-column-left"
                  type="checkbox"
                />
              </td>
              <td>{{ employee.EmployeeCode }}</td>
              <td>{{ employee.FullName }}</td>
              <td>{{ this.common.changeDisplayGender(employee.Gender) }}</td>
              <td style="text-align: center; padding-left: 0">
                {{ this.common.changeDisplayDate(employee.DateOfBirth) }}
              </td>
              <td>{{ employee.IdentificationCard }}</td>
              <td>
                {{ employee.PositionName }}
              </td>
              <td>
                {{ employee.DepartmentName }}
              </td>
              <td>{{ employee.BankAccount }}</td>
              <td>{{ employee.BankName }}</td>
              <td>{{ employee.Branch }}</td>
              <td
                @mouseleave="closeFormDuplicate"
                @click="showUpdate(employee)"
                class="function-update"
                style="text-align: center"
              >
                <div class="parent">
                  <div
                    style="cursor: pointer"
                    @click="detailEmployee(employee)"
                  >
                    {{ resource.MEmployeeList.Update }}
                  </div>
                  <div
                    @click="whenMove(employee)"
                    class="icon icon-caret-down-small"
                  ></div>
                </div>
                <div
                  v-show="
                    employee.EmployeeId === employeeSelected.EmployeeId &&
                    showChildUpdate === true
                  "
                  class="child-update"
                >
                  <div @click="replicationEmployee(employee)">
                    {{ resource.MEmployeeList.Replication }}
                  </div>
                  <div @click="deleteEmployee">
                    {{ resource.MEmployeeList.Delete }}
                  </div>
                  <div>{{ resource.MEmployeeList.Stop }}</div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <MISAPagination
        v-model:pageNumber="filterObject.pageNumber"
        v-model:pageSize="filterObject.pageSize"
        :totalRecord="totalRecord"
      ></MISAPagination>
    </div>
  </div>
  <detail-employee
    :show="isShow"
    :employee="employeeSelected"
    @hideFormDetailEmployee="hideFormDetailEmployee"
    :statusCode="statusCode"
    @loadData="loadData"
    @reLoadDataEmployee="reLoadDataEmployee"
  />
</template>
<script>
import DetailEmployee from "../detailemployee/DetailEmployee.vue";
export default {
  name: "EmployeeList",
  components: { DetailEmployee },
  data() {
    return {
      InfoSearch: "",
      isShow: false,
      listEmployee: {},
      employeeSelected: {},
      employeeId: "",
      employeeCode: "",
      listEmployeeId: [],
      listIndexSelected: [],
      indexSelected: null,
      statusCode: this.helper.Status.Insert,
      totalRecord: 0,
      filterObject: {
        pageNumber: 1,
        pageSize: 20,
      },
      isDelete: true,
      employeeCodeBiggest: "",
      showChildUpdate: false,
      listPageSelectAllRecord: [],
      text: "",
      employeeIdDelete: "",
      // Khởi tạo mảng chứa các EmployeeId từ danh sách nhân viên result
      resultEmployeeIdList: [],
    };
  },
  methods: {
    /**
     * Hàm bỏ chọn tất cả bạn ghi
     * @author: Nguyễn Văn Trúc(15/3/2024)
     */
    unSelect() {
      this.listEmployeeId = [];
      this.listIndexSelected = [];
      this.listPageSelectAllRecord = [];
    },
    /**
     * Load lại dữ liệu
     * @author: Nguyễn Văn Trúc(15/3/2024)
     */
    reload() {
      this.loadData();
    },
    /**
     * Ẩn nút xoá sản phẩm khi hover ra ngoài
     * @author: Nguyễn Văn Trúc(15/3/2024)
     */
    closeFormDelete() {
      this.isDelete = true;
    },
    /**
     * Làm mới lại employeeId khi con trỏ chuột chỉ ra ngoài
     * @author: Nguyễn Văn Trúc(3/3/2024)
     */
    handleMouseLeave() {
      this.employeeCode = "";
    },

    /**
     * Lấy id nhân viên khi con trỏ chuột ở dòng nhân viên ấy
     * @param {nhân viên} employee
     * @author: Nguyễn Văn Trúc(3/3/2024)
     */
    getEmployeeIdWhenMove(employee) {
      this.employeeId = employee.EmployeeId;
      this.employeeCode = employee.EmployeeCode;
      this.employeeSelected = employee;
    },

    /**
     * Đóng form khi hover ra ngoài
     * @author: Nguyễn Văn Trúc(3/3/2024)
     */
    closeFormDuplicate() {
      this.showChildUpdate = false;
    },
    /**
     * Chuyển hướng đến trang thêm danh sách nhân viên bằng excel
     * @author: Nguyễn Văn Trúc (31/1/2024)
     */
    redirectToImportEmployee() {
      this.$router.push(this.helper.Router.File);
    },
    /**
     * Hàm Thực hiện tự động download file excel về
     * @author: Nguyễn Văn Trúc (21/1/2024)
     */
    async downloadFile() {
      let me = this;
      try {
        me.common.showLoading();
        let listAllEmployeeExport = await me.apiService.get(
          me.helper.MApi.ListAllEmployee
        );
        listAllEmployeeExport.forEach(function (number) {
          if (number.Email === undefined || number.Email === null) {
            number.Email = "";
          }
        });
        await me.apiService.downloadFileExcel(listAllEmployeeExport);
        me.common.showLoading(false);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm Thực hiện khi nhấn tìm kiếm
     * @author: Nguyễn Văn Trúc (21/1/2024)
     */
    searchEmployee: function () {
      try {
        this.common.showLoading();
        this.getFilterEmployee();
        this.filterObject.pageNumber = 1;
        this.listPageSelectAllRecord = [];
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm Thực hiện khi nhấn enter
     * @author: Nguyễn Văn Trúc (21/1/2024)
     */
    enterSearchInput: function () {
      try {
        this.common.showLoading();
        this.filterObject.pageNumber = 1;
        this.listPageSelectAllRecord = [];
        this.getFilterEmployee();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm Thực hiện gọi api phân trang
     * @author: Nguyễn Văn Trúc (21/1/2024)
     */
    async getFilterEmployee() {
      try {
        let me = this;
        me.resultEmployeeIdList = [];
        let result = await me.apiService.loadFilter(
          me.filterObject.pageSize,
          me.filterObject.pageNumber,
          me.InfoSearch
        );
        me.text = me.InfoSearch;
        me.listEmployee = result.ListEmployee;
        me.totalRecord = result.ToTalRecord;

        if (Object.keys(this.listEmployee).length !== 0) {
          // Duyệt qua từng đối tượng nhân viên trong result và thu thập EmployeeId
          for (let i = 0; i < me.listEmployee.length; i++) {
            me.resultEmployeeIdList.push(me.listEmployee[i].EmployeeId);
          }

          // Kiểm tra xem tất cả các employeeId của bạn có tồn tại trong danh sách resultEmployeeIdList không
          let allExist = me.resultEmployeeIdList.every((employeeId) =>
            this.listEmployeeId.includes(employeeId)
          );

          if (allExist) {
            this.listPageSelectAllRecord.push(this.filterObject.pageNumber);
          } else {
            // Nếu bỏ chọn 1 trong tất cả các dòng khi chọn tất cả thì xoá bỏ dấu tích chọn tất cả
            this.listPageSelectAllRecord = this.listPageSelectAllRecord.filter(
              (i) => i !== this.filterObject.pageNumber
            );
          }
        }

        me.common.showLoading(false);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm Thực hiện khi huỷ hoặc thoát form chi tiết khách hàng
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    hideFormDetailEmployee: function () {
      this.isShow = false;
      this.indexSelected = null;
    },
    /**
     * Hàm Thực hiện khi nhấn nút thêm mới
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    async addEmployee() {
      try {
        await this.getEmployeeCodeBiggest();
        this.statusCode = this.helper.Status.Insert;
        // Hiển thị form chi tiết khách hàng
        this.isShow = true;
        // Gắn khách hàng gửi đi là khách hàng rỗng
        this.reloadInfoEmployee();
        this.employeeSelected.EmployeeCode = this.employeeCodeBiggest;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Làm mới lại dữ liệu
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    reloadInfoEmployee() {
      this.employeeSelected = {};
    },

    /**
     * Hàm Thực hiện khi nhấn nhân bản nhân viên
     * @param {*nhân viên muốn nhân bản} employee
     * @author: Nguyễn Văn Trúc (22/1/2024)
     */
    async replicationEmployee(employee) {
      try {
        await this.getEmployeeCodeBiggest();
        this.statusCode = this.helper.Status.Insert;
        // Hiển thị form chi tiết khách hàng
        this.isShow = true;
        // // Gắn khách hàng gửi đi là khách hàng rỗng
        this.employeeSelected = employee;
        this.employeeSelected.EmployeeCode = this.employeeCodeBiggest;
        this.employeeSelected.CreatedDate = new Date();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm xem chi tiết khách hàng
     * @param {*nhân viên muốn xem chi tiết} employee
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    detailEmployee: function (employee) {
      try {
        this.statusCode = this.helper.Status.Update;
        // Hiển thị form chi tiết khách hàng
        this.isShow = true;
        // Gắn khách hàng gửi đi là khách hàng click chọn
        this.employeeSelected = employee;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Load lại dữ liệu cho nhân viên
     * @author: Nguyễn Văn Trúc (22/12/2023)
     */
    reLoadDataEmployee: function () {
      this.reloadInfoEmployee();
      this.addEmployee();
    },

    /**
     * Hàm Thực hiện khi nhấn nút Sửa
     * @param {*nhân viên muốn sửa} employee
     * @author: Nguyễn Văn Trúc (22/1/2024)
     */
    showUpdate: function (employee) {
      this.statusCode = this.helper.Status.Update;
      this.employeeSelected = employee;
    },

    /**
     * Hàm Thực hiện khi click  hàng trong table
     * @param {*Id của nhân viên chọn} employeeIdSelected
     * @param {*vị trí chọn} index
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    selectEmployee(employeeIdSelected, index) {
      try {
        // Nếu vị trí đã được chọn
        if (
          this.listEmployeeId.filter(
            (employeeId) => employeeId === employeeIdSelected
          ).length != 0
        ) {
          // huỷ id được chọn
          this.employeeId = null;
          // huỷ vị trí được chọn
          this.indexSelected = null;

          this.listEmployeeId = this.listEmployeeId.filter(
            (employeeId) => employeeId !== employeeIdSelected
          );

          this.listIndexSelected = this.listIndexSelected.filter(
            (i) => i.key !== employeeIdSelected
          );

          // Nếu bỏ chọn 1 trong tất cả các dòng khi chọn tất cả thì xoá bỏ dấu tích chọn tất cả
          this.listPageSelectAllRecord = this.listPageSelectAllRecord.filter(
            (i) => i !== this.filterObject.pageNumber
          );
        }
        // Nếu vị trí chưa đuược chọn
        else {
          let element = {};
          element.key = employeeIdSelected;
          this.listIndexSelected.push(element);
          // Lấy id của khách hàng khi click chọn
          this.employeeId = employeeIdSelected;
          // Lấy vị trị chọn
          this.indexSelected = index;

          this.listEmployeeId.push(employeeIdSelected);
          // Kiểm tra xem tất cả các employeeId của bạn có tồn tại trong danh sách resultEmployeeIdList không
          let allExist = this.resultEmployeeIdList.every((employeeId) =>
            this.listEmployeeId.includes(employeeId)
          );

          if (allExist) {
            this.listPageSelectAllRecord.push(this.filterObject.pageNumber);
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm Thực hiện lấy employeeId
     * @param {*nhân viên muốn lấy Id} employee
     * @author: Nguyễn Văn Trúc (21/1/2024)
     */
    whenMove: function (employee) {
      this.employeeId = employee.EmployeeId;
      this.employeeCode = employee.EmployeeCode;
      this.showChildUpdate = !this.showChildUpdate;
      this.employeeIdDelete = employee.EmployeeId;
    },

    /**
     * Hàm Thực hiện chọn tất cả bản ghi
     * @param {Vị trí trang} pageNumber
     * @author: Nguyễn Văn Trúc (22/1/2024)
     */
    selectAllRecord: function (pageNumber) {
      try {
        if (this.listPageSelectAllRecord.includes(pageNumber)) {
          // Nếu pageNumber đã tồn tại trong mảng, thì xóa nó ra khỏi mảng
          this.listPageSelectAllRecord = this.listPageSelectAllRecord.filter(
            (item) => item !== pageNumber
          );
          this.listEmployeeId = this.listEmployeeId.filter(
            (item) =>
              !this.listEmployee.some(
                (employee) => employee.EmployeeId === item
              )
          );

          this.listIndexSelected = this.listIndexSelected.filter(
            (item) =>
              !this.listEmployee.some(
                (employee) => employee.EmployeeId === item.key
              )
          );
        } else {
          // Nếu pageNumber chưa tồn tại trong mảng, thêm nó vào mảng
          this.listPageSelectAllRecord.push(pageNumber);
          this.listEmployee.forEach((item) => {
            this.listEmployeeId.push(item.EmployeeId);
            this.listEmployeeId = this.common.uniqueArray(this.listEmployeeId);
            let element = {};
            element.key = item.EmployeeId;
            this.listIndexSelected.push(element);
          });
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm Thực hiện khi chọn xoá khách hàng
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    deleteEmployee() {
      try {
        this.common.showDialog(
          `${this.resource.ConfirmDeleteEmployee.ContentDelete} [${this.employeeCode}] ${this.resource.Question.Content}`,
          this.resource.ConfirmDeleteEmployee.Title,
          this.helper.TypeIcon.Warning,
          this.helper.Status.Delete,
          this.helper.MApi.EmployeeDelete,
          this.employeeId
        );
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm Thực hiện load dữ liệu lên bảng
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    loadData: function () {
      try {
        this.common.showLoading();
        this.getFilterEmployee();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm Thực hiện lấy mã nhân viên lớn nhất
     * @author: Nguyễn Văn Trúc (21/1/2024)
     */
    async getEmployeeCodeBiggest() {
      try {
        let me = this;
        if (this.listEmployee == 0) {
          me.employeeCodeBiggest = me.helper.EmployeeCodeDefault;
        } else {
          me.employeeCodeBiggest = await me.apiService.get(
            me.helper.MApi.GetEmployeeCodeBiggest
          );
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm ẩn hiện xoá
     * @author: Nguyễn Văn Trúc (21/1/2024)
     */
    toggleDelete() {
      this.isDelete = !this.isDelete;
    },

    /**
     * Hàm Thực hiện xoá nhiều nhân viên
     * @author: Nguyễn Văn Trúc (22/1/2024)
     */
    deleteEmployees() {
      try {
        this.common.showDialog(
          this.resource.ConfirmDeleteEmployee.ContentDeleteAll,
          this.resource.ConfirmDeleteEmployee.Title,
          this.helper.TypeIcon.Warning,
          this.helper.Status.DeleteMultiple,
          this.helper.MApi.EmployeeDelete,
          this.listEmployeeId
        );
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Bôi đen ô input search khi click vào
     * @author: Nguyễn Văn Trúc (22/1/2024)
     */
    blackenInputSearch() {
      try {
        this.common.blackenInput(this.helper.Ref.InputSearch);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Kiểm tra dòng đã được chọn chưa
     * @returns: true: Được chọn, false: Chưa được chọn
     * @author: Nguyễn Văn Trúc (22/1/2024)
     */
    checkRowSelected(employee) {
      return (
        this.listIndexSelected.filter((i) => i.key === employee.EmployeeId)
          .length != 0
      );
    },
  },
  /**
   * Hàm Thực hiện load lại dữ liệu khi tạo ra
   * @author: Nguyễn Văn Trúc (22/1/2024)
   */
  created() {
    this.loadData();
  },
  /**
   * Hàm thực hiện nhận sự kiện bấm nút
   * @author: Nguyễn Văn Trúc (3/3/2024)
   */
  mounted() {
    let me = this;
    // Nhận dữ liệu từ sự kiện bấm nút
    me.emitter.on(me.helper.Emitter.SendEvent, (value) => {
      // CTRL + D: Xoá
      if (value === me.helper.Status.Delete && me.employeeCode !== "") {
        me.employeeIdDelete = this.employeeId;
        me.deleteEmployee();
      }

      // CTRL + 1 : Thêm mới
      if (value === me.helper.Status.Insert) {
        me.addEmployee();
      }

      // CTRL + 2: Import
      if (value === me.helper.Status.Import) {
        me.$router.push(me.helper.Router.File);
      }

      // CTRL + E: Cập nhật
      if (value === me.helper.Status.Update && me.employeeCode !== "") {
        me.detailEmployee(me.employeeSelected);
      }

      // CTRL + SHIRT + E: Export
      if (value === me.helper.Status.Export) {
        me.downloadFile();
      }

      // Delete
      if (value === me.helper.Status.DeleteMultiple) {
        this.listEmployeeId.length !== 0 ? me.deleteEmployees() : [];
      }
    });

    me.emitter.on("Status", (value) => {
      if (value === me.helper.Status.DeleteMultiple) {
        me.listEmployeeId = [];
        me.listIndexSelected = [];
        me.listPageSelectAllRecord = [];
      }
      if (value === me.helper.Status.Delete) {
        me.listEmployeeId = me.listEmployeeId.filter(
          (item) => item !== me.employeeIdDelete
        );
        me.listIndexSelected = me.listIndexSelected.filter(
          (item) => item.key !== me.employeeIdDelete
        );
      }
      me.loadData();
    });

    // Khi thay đổi số lượng bản ghi trên trang làm mới lại mảng chứa các trang
    me.emitter.on(me.helper.Emitter.ChangePageSize, () => {
      me.listPageSelectAllRecord = [];
    });
  },
  watch: {
    /**
     * Hàm Thực hiện khi phân trang
     * @author: Nguyễn Văn Trúc (21/1/2024)
     */
    filterObject: {
      handler: function hange() {
        // Khi thông tin tìm kiếm bị thay đổi mà phân trang chuyển về trang 1
        if (this.InfoSearch !== this.text) {
          this.filterObject.pageNumber = 1;
        }
        this.common.showLoading();
        this.getFilterEmployee();
      },
      deep: true,
    },
  },
};
</script>
<style scoped>
.selected {
  background: #eeeeee;
}
.background_default {
  background: white;
}
.delete {
  display: none;
}
.isSuccess {
  background: rgb(206, 234, 217);
}

.isError {
  background: #facccc;
}
.icon-file {
  right: 24px;
  position: absolute;
}
.button-icon {
  position: absolute;
  left: 326px;
}
.button-icon button:hover {
  background: #eeeeee;
}
.button-icon button:active {
  background: #bdbdbd;
}
</style>
