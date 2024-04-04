<template>
  <div class="show-employee" v-if="show">
    <div class="m-form detail-employee">
      <div class="form-header detail-employee-header">
        <div class="employee-header-left">
          <div class="form-title detail-employee-title">
            {{ resource.MDetailEmployee.InfoEmployee }}
          </div>
        </div>
        <div class="employee-header-right">
          <div class="icon icon_question"></div>
          <div
            v-on:click="saveWhenClose"
            title="ESC"
            class="icon icon-close"
          ></div>
        </div>
      </div>
      <div class="form-main detail-employee-main">
        <div class="row1">
          <div class="row1-left">
            <div class="row11">
              <div class="employee-code">
                <MISAInput
                  :inputErrorFirst="nameInputErrorFirst"
                  :nameInput="helper.NameInput.EmployeeCode"
                  v-model="employeeSelect.EmployeeCode"
                  :label="resource.MInput.Code"
                  :required="true"
                  :isError="this.errors.employeeCode"
                  @removeClassErrorInput="this.errors.employeeCode = null"
                ></MISAInput>
              </div>
              <div class="employee-name">
                <MISAInput
                  :nameInput="helper.NameInput.FullName"
                  :required="true"
                  v-model="employeeSelect.FullName"
                  :label="resource.MInput.FullName"
                  :isError="this.errors.fullName"
                  @removeClassErrorInput="this.errors.fullName = null"
                ></MISAInput>
              </div>
            </div>
            <div class="department">
              <MISACombobox
                :name="helper.NameCombobox.Department"
                :apiData="this.apiDepartment"
                :entity="department"
                :entityName="helper.NameCombobox.DepartmentName"
                :entityId="helper.NameCombobox.DepartmentId"
                @emtitySelect="departmentSelected"
                :label="resource.MInput.Department"
                @removeClassErrorInput="this.errors.departmentName = null"
                :required="true"
                :isError="this.errors.departmentName"
                @errorNoFind="errorNoFindDepartment"
              ></MISACombobox>
            </div>
            <div class="position">
              <MISACombobox
                :apiData="this.apiPosition"
                :entityName="helper.NameCombobox.PositionName"
                :entityId="helper.NameCombobox.PositionId"
                @emtitySelect="positionSelected"
                :label="resource.MInput.Position"
                :entityIdSelected="this.employeeSelect.positionId"
                :name="helper.NameCombobox.Position"
                :entity="position"
                @errorNoFind="errorNoFindPosition"
              ></MISACombobox>
            </div>
          </div>
          <div class="row1-right">
            <div class="row21">
              <div class="dateofbirth">
                <MISADate
                  :nameInput="helper.NameInput.DateOfBirth"
                  v-model="employeeSelect.DateOfBirth"
                  :label="resource.MInput.DateOfBirth"
                  :isError="this.errors.dateOfBirth"
                  @removeClassErrorInput="removeErrorBirth"
                  @isNaNDate="isNaNDateOfBirth"
                ></MISADate>
              </div>
              <div class="gender">
                <MISACheckBox
                  v-model="employeeSelect.Gender"
                  :label="resource.MInput.Gender"
                ></MISACheckBox>
              </div>
            </div>
            <div class="row21">
              <div class="id-card">
                <MISAInput
                  :nameInput="helper.NameInput.IdentificationCard"
                  :label="resource.MInput.IdentificationCard"
                  v-model="employeeSelect.IdentificationCard"
                ></MISAInput>
              </div>
              <div class="date-range">
                <MISADate
                  :nameInput="helper.NameInput.RangeDate"
                  v-model="employeeSelect.DateOfIdentityCard"
                  :label="resource.MInput.DateOfIdentityCard"
                  :isError="this.errors.dateOfIdentityCard"
                  @removeClassErrorInput="removeErrorRang"
                  @isNaNDate="isNaNRangeDate"
                ></MISADate>
              </div>
            </div>
            <div class="place-of-issuance">
              <MISAInput
                :nameInput="helper.NameInput.PlaceIdentificationCard"
                v-model="employeeSelect.PlaceIdentificationCard"
                id="address"
                :label="resource.MInput.PlaceIdentificationCard"
              ></MISAInput>
            </div>
          </div>
        </div>
        <div class="row3">
          <div class="address">
            <MISAInput
              :nameInput="helper.NameInput.Address"
              v-model="employeeSelect.Address"
              :label="resource.MInput.Address"
            ></MISAInput>
          </div>
        </div>
        <div class="row4">
          <div class="phone-number">
            <MISAInput
              :nameInput="helper.NameInput.PhoneNumber"
              v-model="employeeSelect.PhoneNumber"
              :label="resource.MInput.PhoneNumber"
            ></MISAInput>
          </div>
          <div class="landline-phone">
            <MISAInput
              :nameInput="helper.NameInput.LandlinePhone"
              v-model="employeeSelect.LandlinePhone"
              :label="resource.MInput.LandlinePhone"
            ></MISAInput>
          </div>
          <div class="email">
            <MISAInput
              :nameInput="helper.NameInput.Email"
              v-model="employeeSelect.Email"
              :label="resource.MInput.Email"
              :isError="this.errors.email"
              @removeClassErrorInput="this.errors.email = null"
            ></MISAInput>
          </div>
        </div>
        <div class="row5">
          <div class="bank-account">
            <MISAInput
              :nameInput="helper.NameInput.BankAccount"
              v-model="employeeSelect.BankAccount"
              :label="resource.MInput.BankAccount"
            ></MISAInput>
          </div>
          <div class="bank-name">
            <MISAInput
              :nameInput="helper.NameInput.BankName"
              v-model="employeeSelect.BankName"
              :label="resource.MInput.BankName"
            ></MISAInput>
          </div>
          <div class="branch">
            <MISAInput
              :nameInput="helper.NameInput.Bank"
              v-model="employeeSelect.Branch"
              :label="resource.MInput.Branch"
            ></MISAInput>
          </div>
        </div>
      </div>
      <div class="form-footer detail-employee-footer">
        <div class="footer-left">
          <button
            v-on:click="hideDetailEmployee"
            class="m-button m-button-cancel"
          >
            {{ resource.MDialog.Destroy }}
          </button>
        </div>
        <div class="footer-right">
          <button
            v-on:click="saveEmployeeAndExit"
            class="m-button m-button-cancel"
            title="CTRL + S"
          >
            {{ resource.MDialog.Store }}
          </button>
          <button
            v-on:click="saveEmployee"
            class="m-button m-button-success btn-success"
            title="CTRL + SHIFT + S"
          >
            {{ resource.MDialog.YesAndSave }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  components: {},
  props: {
    show: Boolean,
    employee: Object,
    statusCode: Number,
  },
  data() {
    return {
      isExitForm: false,
      employeeSelect: {},
      employeeSelectedFirst: {},
      // Object thông báo lỗi
      errors: {
        employeeCode: "",
        fullName: "",
        departmentName: "",
        positionName: "",
        dateOfBirth: "",
        dateOfIdentityCard: "",
        email: "",
      },
      departmentSelect: {},
      apiDepartment: "",
      positionSelect: {},
      apiPosition: "",
      position: {},
      department: {},
      errorDepartment: "",
      errorPosition: "",
      errorNaNDateOfBirth: "",
      errorNaNRangeDate: "",
    };
  },
  methods: {
    removeErrorBirth() {
      this.errors.dateOfBirth = "";
      this.errorNaNDateOfBirth = "";
    },
    removeErrorRang() {
      this.errors.dateOfIdentityCard = "";
      this.errorNaNRangeDate = "";
    },
    /**
     * Hàm ẩn form chi tiết
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    hideDetailEmployee: function () {
      try {
        this.$emit(this.helper.Emit.HideFormDetailEmployee, false);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm Thực hiện valide dữ liệu
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    validateData: function () {
      try {
        let me = this;
        let checkError = true;
        me.errors = {};
        // Kiểm tra employee code
        if (me.common.validateInput(me.employeeSelect.EmployeeCode)) {
          me.errors.employeeCode = me.resource.ErrorEmployee.ContentCode;
          checkError = false;
        }

        // Kiểm tra fullname
        if (me.common.validateInput(me.employeeSelect.FullName)) {
          me.errors.fullName = me.resource.ErrorEmployee.ContentFullName;
          checkError = false;
        }

        me.errors.departmentName = this.errorDepartment;
        // Kiểm tra department
        if (
          me.common.validateInput(me.employeeSelect.DepartmentId) &&
          me.errors.departmentName === ""
        ) {
          me.errors.departmentName =
            me.resource.ErrorEmployee.ContentDepartment;
          checkError = false;
        }
        me.errors.positionName = me.errorPosition;

        if (me.errorNaNDateOfBirth !== "") {
          me.errors.dateOfBirth = me.errorNaNDateOfBirth;
          checkError = false;
        }
        // } else {
        //   checkError = true;
        // }
        // Kiểm tra dateofbirth
        if (me.common.validateInputDate(me.employeeSelect.DateOfBirth)) {
          me.errors.dateOfBirth = me.resource.ErrorEmployee.ContentBirth;
          checkError = false;
        }
        // Kiểm tra dateOfIdentityCard
        if (me.errorNaNRangeDate !== "") {
          me.errors.rangeDate = me.errorNaNRangeDate;
          checkError = false;
        }
        // } else {
        //   checkError = true;
        // }
        if (me.common.validateInputDate(me.employeeSelect.DateOfIdentityCard)) {
          me.errors.rangeDate = me.resource.ErrorEmployee.ContentIdentityCard;
          checkError = false;
        }
        // Kiểm tra email
        if (
          !me.common.checkEmail(me.employeeSelect.Email) &&
          me.employeeSelect.Email !== ""
        ) {
          me.errors.email = me.resource.ErrorEmployee.ContentEmail;
          checkError = false;
        }

        // nếu ko có lỗi
        if (checkError === false) {
          me.common.showDialog(
            me.errors,
            me.resource.ErrorEmployee.Title,
            me.helper.TypeIcon.Warning
          );
        }
        return checkError;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm thêm hoặc sửa nhân viên
     * @author: Nguyễn Văn Trúc(13/3/2024)
     */
    async insertOfUpdateEmployee() {
      console.log(this.employeeSelect);
      try {
        let result = 0;
        if (this.validateData()) {
          this.errors = {};
          if (this.statusCode === this.helper.Status.Insert) {
            this.employeeSelect.CreatedDate = new Date();
            this.employeeSelect.ModifiedDate = null;
            result = await this.apiService.post(
              this.helper.MApi.EmployeeInsert,
              this.employeeSelect
            );
          } else if (this.statusCode === this.helper.Status.Update) {
            this.employeeSelect.ModifiedDate = new Date();
            result = await this.apiService.update(
              this.helper.MApi.EmployeeUpdate,
              this.employeeSelect.EmployeeId,
              this.employeeSelect
            );
          }
          if (result === 1) {
            this.dialogSaveSuccess();
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện lưu nhân viên và giữ form
     * @author: Nguyễn Văn Trúc (13/3/2024)
     */
    saveEmployee() {
      try {
        this.isExitForm = false;
        this.insertOfUpdateEmployee();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Nếu dữ liệu thay đổi hiển thị thông báo có thể lưu hoặc thoát
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    saveWhenClose() {
      console.log(this.employeeSelect);
      console.log(this.employeeSelectedFirst);
      try {
        let me = this;
        if (
          JSON.stringify(me.employeeSelectedFirst) ===
          JSON.stringify(me.employeeSelect)
        ) {
          me.hideDetailEmployee();
        } else {
          me.common.showDialog(
            me.resource.ConfirmClose.Content,
            me.resource.ConfirmClose.Title,
            me.helper.TypeIcon.Question,
            me.helper.Status.SaveWhenClose
          );
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Thực hiện lưu nhân viên và thoát form
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    saveEmployeeAndExit() {
      try {
        this.isExitForm = true;
        this.insertOfUpdateEmployee();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm thực hiện sau khi thao tác(Thêm, Sửa) thành công
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    dialogSaveSuccess() {
      try {
        // Đóng form detail employee
        this.hideDetailEmployee();
        if (!this.isExitForm) {
          this.$emit("reLoadDataEmployee", true);
        }
        // load lại dữ liệu
        this.$emit("loadData", true);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm Thực hiện lấy department lựa chọn
     * @param (phòng ban lựa chọn) deparment
     * @author: Nguyễn Văn Trúc (25/1/2023)
     */
    departmentSelected: function (department) {
      this.departmentSelect = department;
      this.employeeSelect.DepartmentId = this.departmentSelect.DepartmentId;
    },
    /**
     * Hàm Thực hiện lấy position lựa chọn
     * @param(Chức vụ lựa chọn) position
     * @author: Nguyễn Văn Trúc (25/1/2023)
     */
    positionSelected: function (position) {
      this.positionSelect = position;
      this.employeeSelect.PositionId = this.positionSelect.PositionId;
    },
    /**
     * Thực hiện gắn api
     * @author: Nguyễn Văn Trúc (25/1/2023)
     */
    loadApiPositionDepartment() {
      this.apiPosition = this.helper.MApi.Position;
      this.apiDepartment = this.helper.MApi.Department;
    },

    /**
     * Hàm gắn lỗi khi không tìm thấy phòng ban
     * @param {value}: Thông tin lỗi
     * @author: Nguyễn Văn Trúc (16/3/2024)
     */
    errorNoFindDepartment(value) {
      this.errorDepartment = value;
    },

    /**
     * Hàm gắn lỗi khi không tìm thấy chức vụ
     * @param {value}: Thông tin lỗi
     * @author: Nguyễn Văn Trúc (16/3/2024)
     */
    errorNoFindPosition(value) {
      this.errorPosition = value;
    },

    /**
     * Hàm gắn lỗi khi ngày sinh không hợp lệ
     * @param {thông tin lỗi} value
     * @author: Nguyễn Văn Trúc (22/3/2024)
     */
    isNaNDateOfBirth(value) {
      this.errorNaNDateOfBirth = value;
    },

    /**
     * Hàm gắn lỗi khi ngày sinh không hợp lệ
     * @param {thông tin lỗi} value
     * @author: Nguyễn Văn Trúc (22/3/2024)
     */
    isNaNRangeDate(value) {
      this.errorNaNRangeDate = value;
    },
  },
  /**
   * gắn api cho position và department
   * @author: Nguyễn Văn Trúc (9/12/2023)
   */
  created() {
    this.loadApiPositionDepartment();
  },
  /**
   * Hàm Thực hiện khi có thay đổi về show
   * @author: Nguyễn Văn Trúc (9/12/2023)
   */
  watch: {
    async show(value) {
      if (value) {
        let me = this;
        me.errors = {};
        me.employeeSelect = JSON.parse(JSON.stringify(me.employee));
        if (
          me.employeeSelect.Email === undefined ||
          me.employeeSelect.Email === null
        ) {
          me.employeeSelect.Email = "";
        }
        if (me.employeeSelect.Gender === undefined) {
          me.employeeSelect.Gender = 0;
        }
        me.employeeSelectedFirst = JSON.parse(JSON.stringify(me.employee));
        if (
          me.employeeSelectedFirst.Email === undefined ||
          me.employeeSelectedFirst.Email === null
        ) {
          me.employeeSelectedFirst.Email = "";
        }
        if (me.employeeSelectedFirst.Gender === undefined) {
          me.employeeSelectedFirst.Gender = 0;
        }
        if (me.employeeSelect.PositionId) {
          me.position = await me.apiService.getByInfo(
            me.helper.MApi.Position,
            me.employeeSelect.PositionId
          );
        }

        if (me.employeeSelect.DepartmentId) {
          me.department = await me.apiService.getByInfo(
            me.helper.MApi.Department,
            me.employeeSelect.DepartmentId
          );
        }
      }
    },
  },

  /**
   * Hàm thực hiện nhận sự kiện bấm nút
   * @author: Nguyễn Văn Trúc (3/3/2024)
   */
  mounted() {
    let me = this;
    me.emitter.on(me.helper.Emitter.SendEvent, (value) => {
      // esc: Thoát form chi tiết nhân viên
      if (value === me.helper.Status.Exit && me.show === true) {
        me.hideDetailEmployee();
      }

      // CTRL + S: Lưu và cất
      if (value === me.helper.Status.Save && me.show === true) {
        me.saveEmployeeAndExit();
      }

      // CTRL + SHIFT + S : Cất và thêm mới
      if (value === me.helper.Status.SaveAndInsert && me.show === true) {
        me.saveEmployee();
      }
    });

    /**
     * Hàm thực hiện sau khi thêm vs sửa xong(đóng form hoặc không)
     * @author: Nguyễn Văn Trúc(13/3/2024)
     */
    me.emitter.on("Status", (value) => {
      if (value === me.helper.Status.SaveWhenClose) {
        me.saveEmployeeAndExit();
      }

      if (value === me.helper.Status.Exit) {
        me.hideDetailEmployee();
      }
    });
  },
};
</script>
<style scoped></style>
