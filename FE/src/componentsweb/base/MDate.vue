<template>
  <label class="m-label" for="">{{ label }}</label>
  <div class="date-icon">
    <DatePicker
      autocomplete="off"
      inputFormat="dd/MM/yyyy"
      typeable="true"
      v-model="value"
      :class="{ inputErrorBlur: isErrorBlur, inputError: isError }"
      @blur="blurValude"
      v-bind:id="this.nameInput"
      class="m-date"
    />
    <div class="icon_date"></div>
  </div>
  <p class="childError">{{ isError || isErrorBlur }}</p>
</template>
<script>
import moment from "moment";
export default {
  name: "MDate",
  data() {
    return {
      value: "",
      isErrorBlur: null,
    };
  },
  props: {
    label: String,
    modelValue: String,
    isError: String,
    nameInput: String,
  },
  created() {
    if (this.modelValue !== undefined) {
      this.value = new Date(this.modelValue);
    }
  },
  methods: {
    /**
     * Hàm Thực hiện valide dữ liệu khi blur ngày
     * @author: Nguyễn Văn Trúc (12/12/2023)
     */
    blurValude() {
      try {
        let me = this;
        let date = document.getElementById(me.nameInput).value;
        if (date !== "") {
          date = moment(date, ["DD-MM-YYYY", "YYYY-MM-DD"]).toDate();
          if (isNaN(date.getTime())) {
            me.isErrorBlur = `${me.label} ${me.resource.ErrorDate.NotValid}`;
            me.$emit(
              "isNaNDate",
              `${me.label} ${me.resource.ErrorDate.NotValid}`
            );
          } else {
            me.$emit("removeClassErrorInput", false);
            if (date > new Date()) {
              me.isErrorBlur = `${me.label} ${me.resource.ErrorDate.NotGreaterThanCurrentDate}`;
            } else {
              me.isErrorBlur = null;
              me.$emit("removeClassErrorInput", false);
              date = me.common.changeDisplayDate(date);
              date = me.reFormat(date);
            }
          }
        } else {
          me.isErrorBlur = null;
          me.$emit("removeClassErrorInput", false);
        }
        me.$emit("update:modelValue", date);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Thay đổi cách hiển thị
     * @param {ngày hiển thị} date
     * @author: Nguyễn Văn Trúc (17/3/2024)
     */
    reFormat(date) {
      date = date.split("/");
      return `${date[2]}-${date[1]}-${date[0]}`;
    },
  },
};
</script>
<style scoped>
.childError {
  position: absolute;
  color: red;
  font-size: 14px;
}
.icon_date {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -128px -312px;
  width: 16px;
  height: 16px;
  position: absolute;
  right: 10px;
}
.date-icon {
  display: flex;
  align-items: center;
  position: relative;
  width: 292px;
}
</style>
