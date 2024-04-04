<template>
  <div>
    <div @mouseleave="closeForm" class="dropdownlist-icon">
      <div class="dropdownlist-left">
        <input
          style="width: 76px"
          v-model="content"
          class="m-input"
          autocomplete="off"
          type="text"
          id="cbxGender"
          :class="{ errorValidate: error, successValidate: success }"
          readonly
        />
      </div>
      <div @click.self="handleFocusOut" class="dropdownlist-right">
        <div
          @click="ShowList"
          class="icon"
          :class="{
            icon_down_small: iconDown === true,
            icon_chevron_up: iconDown === false,
          }"
        ></div>
      </div>
      <div v-if="showList" class="list-drop">
        <div v-for="item in data" :key="item.key">
          <p
            :class="{
              selected: item.key === rowSelected.key,
              hoverItem:
                item.key == this.indexHover && item.key != rowSelected.key,
            }"
            @click="selectRow(item)"
            @mouseover="this.indexHover = item.key"
            @mouseout="this.indexHover = -1"
          >
            <span class="quality-record">{{ item.value }}</span>
            <i :class="{ icon_Checked: item.key === rowSelected.key }"></i>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MDropdownlist",
  props: { data: Object, name: String },
  data() {
    return {
      indexHover: -1,
      indexSelected: 0,
      error: false,
      success: false,
      rowSelected: {},
      dataSelect: [],
      content: "",
      showList: false,
      iconDown: true,
    };
  },
  methods: {
    /**
     * Đóng form khi hover ra ngoài
     * @author: Nguyễn Văn Trúc (14/3/2024)
     */
    closeForm() {
      this.showList = false;
      this.iconDown = true;
    },
    /**
     * Hàm Click chọn
     * @param {Giá trị lựa chọn} item
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    selectRow(item) {
      if (item.key !== this.rowSelected.key) {
        this.emitter.emit(this.helper.Emitter.ChangePageSize);
      }
      this.iconDown = true;
      // Gắn nội dung bằng key chọn
      this.content = item.value;
      // Gắn ròng đã chọn
      this.rowSelected = item;
      // Đóng list data hiển thị
      this.showList = false;
      this.showListSelect = false;
      // Thay đổi màu border
      this.success = true;
      this.$emit("getPageSize", item.key);
    },
    /**
     * Hàm hiển thị tất cả dữ liệu
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    ShowList: function () {
      // Hiển thị tất cả data
      this.showList = !this.showList;

      this.iconDown = !this.iconDown;
      // Đóng hiển thị data trong mục lựa chọn
      this.showListSelect = false;
    },
  },
  /**
   * Giá trị mặc định khi tạo ra
   * @author: Nguyễn Văn Trúc (9/12/2023)
   */
  created() {
    this.showList = false;
    this.rowSelected = this.data[1];
    this.content = this.data[1].value;
  },
};
</script>
<style scoped>
.list-drop {
  position: absolute;
  margin-top: -232px;
  background: #ffffff;
  width: 76px;
  padding: 8px;
  border-radius: 4px;
  box-shadow: 0 4px 16px rgba(23, 27, 42, 0.24);
}
.list-drop p {
  height: 36px;
  width: 60px;
  padding-left: 40px;
  align-items: center;
  display: flex;
  justify-content: start;
  cursor: pointer;
  border-radius: 4px;
}
.list-drop .quality-record {
  position: absolute;
  left: 14px;
}

.list-drop p:hover {
  background: #b9e2b1;
}

.selected {
  background: #b9e2b1;
}
.hoverItem {
  background: #e0e0e0;
}

.errorValidate {
  border: 1px solid red;
}

/* .successValidate {
  border: 1px solid #50b83c;
} */

.indexSelect {
  background: #e0e0e0;
}

.icon_Checked {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -1225px -358px;
  height: 24px;
  width: 24px;
  position: absolute;
  right: 8px;
}
/* Ngăn chặn bôi đen nội dung của phần tử input khi chạm vào */
input {
  -webkit-user-select: none; /* Safari */
  -moz-user-select: none; /* Firefox */
  -ms-user-select: none; /* IE 10+ */
  user-select: none;
  -webkit-tap-highlight-color: rgba(0, 0, 0, 0); /* Safari, Android, iOS */
  -webkit-touch-callout: none; /* iOS Safari */
}
</style>
