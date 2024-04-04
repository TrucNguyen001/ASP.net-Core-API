<template>
  <div>
    <label class="m-label" for=""
      >{{ label }}<i v-if="required" style="color: red">*</i></label
    >
    <div @mouseleave="closeForm" class="combobox-icon">
      <div class="combobox-left">
        <input
          v-model="content"
          class="m-input"
          autocomplete="off"
          type="text"
          @input="changeInput"
          @blur="checkValidateInput"
          :class="{
            errorBlur: isErrorBlur,
            errorValidate: error,
            errorValidate: isError,
          }"
          @keydown.down="downEvent"
          @keydown.up="upEvent"
          @keydown.enter="enterSelectedRow"
          @click="blackenInput"
          :ref="'combobox_' + this.name"
          v-bind:id="this.name"
          :placeholder="resource.MCombobox.Select"
        />
      </div>
      <p class="childError">{{ isError || isErrorBlur }}</p>
      <div class="combobox-right">
        <div
          @click="showListData"
          class="icon"
          :class="{
            icon_chevron_down: iconDown === true,
            icon_chevron_up: iconDown === false,
          }"
        ></div>
      </div>
      <div
        @mouseover="reOpenWhenBlur"
        v-if="showList && dataSelect.length > 0"
        class="listCombobox"
      >
        <p>
          {{ resource.MCombobox.Select }}
          <i
            :class="{
              icon_Checked: Object.keys(this.rowSelected).length === 0,
            }"
          ></i>
        </p>
        <div v-for="(item, index) in dataSelect" :key="item[entityId]">
          <p
            :class="{
              selected: item[entityId] === rowSelected[entityId],
              hoverItem:
                item[entityId] == this.indexHover &&
                item[entityId] != rowSelected[entityId],
              indexSelect: index === indexSelected,
            }"
            @click="selectRow(item)"
            @mouseover="this.indexHover = item[entityId]"
            @mouseout="this.indexHover = -1"
          >
            {{ item[entityName] }}
            <i :class="{ icon_Checked: rowSelected === item }"></i>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MCombobox",
  props: {
    apiData: String,
    entityName: String,
    entityId: String,
    label: String,
    isError: String,
    required: Boolean,
    entityIdSelected: String,
    name: String,
    entity: Object,
  },
  data() {
    return {
      content: "",
      data: {},
      indexHover: -1,
      indexSelected: 0,
      error: false,
      rowSelected: {},
      dataSelect: [],
      showList: false,
      entityIdChoose: "",
      iconDown: true,
      isErrorBlur: null,
    };
  },
  methods: {
    /**
     * Đóng form hiển thị danh sách lựa chọn
     * @author: Nguyễn Văn Trúc (14/3/2023)
     */
    closeForm() {
      this.showList = false;
      this.iconDown = true;
    },

    /**
     * Mở lại form sau blur
     * @author: Nguyễn Văn Trúc (14/3/2023)
     */
    reOpenWhenBlur() {
      this.$refs["combobox_" + this.name].blur();
      this.showList = true;
      this.$refs["combobox_" + this.name].focus();
    },
    /**
     * Hàm Click chọn
     * @param {item lựa chọn} item
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    selectRow(item) {
      this.indexSelected = 0;
      this.isErrorBlur = null;
      this.iconDown = true;
      // Gắn ròng đã chọn
      this.rowSelected = item;
      // Đóng list data hiển thị
      this.showList = false;

      this.content = item[this.entityName];

      this.$emit("emtitySelect", item);
      this.$emit("removeClassErrorInput", false);
    },
    /**
     * Hàm hiển thị tất cả dữ liệu
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    showListData: function () {
      this.$refs["combobox_" + this.name].focus();
      this.indexSelected = 0;
      this.dataSelect = this.data;
      this.iconDown = !this.iconDown;
      this.showList = !this.showList;
    },
    removeAccentsAndSpecialChars(str) {
      return str
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, "")
        .replace(/[^\w\s]/g, "");
    },
    /**
     * Hàm Thực hiện khi thay đổi dữ liệu trong input
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    changeInput: function () {
      // gắn lại vị trí đang chọn
      this.indexSelected = 0;
      // gắn lại dữ liệu dữ liệu chọn
      this.dataSelect = [];

      // lấy những phần tử chứa kí từ chọn
      this.data.forEach((item) => {
        if (
          this.removeAccentsAndSpecialChars(
            String(item[this.entityName]).toLowerCase()
          ).includes(
            this.removeAccentsAndSpecialChars(this.content.toLowerCase())
          )
        ) {
          this.dataSelect.push(item);
        }
      });

      if (this.dataSelect.length > 0) {
        this.showList = true;
      }

      // Kiểm tra dữ liệu nhập có đúng ko
      let checkSuccess = false;

      this.data.forEach((item) => {
        if (item[this.entityName] === this.content) {
          checkSuccess = true;
        }
      });

      if (checkSuccess === true) {
        this.error = false;
      }
      if (checkSuccess === false) {
        this.error = true;
      }
    },
    /**
     * Kiểm tra giá trị nhập
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    checkValidateInput() {
      try {
        if (
          this.required === true &&
          (this.content === "" ||
            this.content === undefined ||
            this.content === null) &&
          this.showList === false
        ) {
          this.isErrorBlur = this.label + this.resource.MInput.NotEmpty;
        } else if (this.content !== "") {
          this.$emit("removeClassErrorInput", false);
          let check = false;
          this.data.forEach((item) => {
            if (String(item[this.entityName]).trim() === this.content.trim()) {
              check = true;
              this.rowSelected = item;
            }
          });
          if (check === true) {
            this.$emit("emtitySelect", this.rowSelected);
            this.$emit("errorNoFind", "");
            this.isErrorBlur = null;
          } else {
            this.isErrorBlur = this.label + this.resource.MInput.NotFind;
            this.$emit("errorNoFind", this.isErrorBlur);
          }
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.show = false;
      }
    },

    /**
     * Hàm Thực hiện khi nhấn down
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    downEvent: function () {
      let length = this.dataSelect.length;
      if (this.indexSelected < length - 1) {
        this.indexSelected++;
      }
      this.$refs["combobox_" + this.name].scrollTop = 50;
    },
    /**
     * Hàm Thực hiện khi nhấn up
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    upEvent: function () {
      if (this.indexSelected > 0) {
        this.indexSelected--;
      }
    },
    /**
     * Hàm Thực hiện khi nhấn enter
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    enterSelectedRow: function () {
      try {
        if (this.dataSelect.length === 0) {
          this.isErrorBlur = this.label + this.resource.MInput.NotFind;
          this.$emit("errorNoFind", this.isErrorBlur);
        } else {
          this.iconDown = true;
          this.showList = false;
          this.isErrorBlur = null;
          this.error = false;
          this.rowSelected = this.dataSelect[this.indexSelected];
          this.content = this.rowSelected[this.entityName];
          this.$emit("emtitySelect", this.rowSelected);
          this.$emit("removeClassErrorInput", false);
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Load dữ liệU
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    async loadData() {
      try {
        let response = await this.apiService.get(this.apiData);
        this.data = response;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Bôi đen hết khi chọn vào
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    blackenInput() {
      this.showListData();
      this.$refs["combobox_" + this.name].select();
    },
  },
  /**
   * Load dữ liệu khi khởi tại=o hàm
   * @author: Nguyễn Văn Trúc (9/12/2023)
   */
  created() {
    this.loadData();
  },
  watch: {
    entity(value) {
      this.rowSelected = value;
      this.content = this.rowSelected[this.entityName];
    },
    /**
     * Theo dõi sự thay đổi khi nhập
     * @param {giá trị nhập} value
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    content: function (value) {
      if (value === "") {
        this.$emit("emtitySelect", "");
      }
    },
  },
};
</script>
<style scoped>
.selectItem {
  position: absolute;
  width: 610px;
  z-index: 999;
  box-shadow: 0 4px 16px rgba(23, 27, 42, 0.24);
  border-radius: 4px;
}
.childCombobox p,
.listCombobox p {
  height: 36px;
  width: 585px;
  padding-left: 8px;
  align-items: center;
  display: flex;
  cursor: pointer;
  border-radius: 4px;
}
.childCombobox p:hover,
.listCombobox p:hover {
  background: rgba(80, 184, 60, 0.1);
}
.selected {
  background: rgba(80, 184, 60, 0.1);
}

.errorValidate {
  border: 1px solid red;
}

.indexSelect {
  background: #e0e0e0;
}

.childError {
  position: absolute;
  top: 36px;
  color: red;
  font-size: 14px;
}
.icon_Checked {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -1220px -358px;
  height: 24px;
  width: 24px;
  position: absolute;
  right: 16px;
  margin-top: 2px;
}
.errorBlur {
  border: 1px solid red;
}
input::placeholder {
  font-size: 14px;
}
</style>
