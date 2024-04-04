<template>
  <label class="m-label" for=""
    >{{ label }}<i v-if="required" style="color: red">*</i></label
  >
  <input
    class="m-input"
    v-model="value"
    @input="onInput"
    type="text"
    :class="{
      inputErrorBlur: isErrorBlur,
      inputError: isError,
      textRight: textRight,
    }"
    @blur="blurValude"
    :ref="'input_' + this.nameInput"
    v-bind:id="this.nameInput"
    @change="changeValueInput"
    @click="blackenInput"
  />
  <p class="childError">{{ isError || isErrorBlur }}</p>
</template>
<script>
export default {
  name: "MInput",
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
    required: Boolean,
    nameInput: String,
    textRight: Boolean,
  },
  mounted() {
    this.autoFocus();
  },
  created() {
    this.value = this.modelValue;
  },
  methods: {
    /**
     * Cập nhật lại giá trị input
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    changeValueInput() {
      this.$emit("update:modelValue", this.value);
    },
    /**
     * auto focus vào ô đầu tiên khi form hiển thị
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    autoFocus: function () {
      if (this.nameInput === this.helper.NameInput.EmployeeCode) {
        this.$refs["input_" + this.nameInput].focus();
      }
    },
    /**
     * Hàm Thực hiện khi thay đổi dữ liệu trong input
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    onInput() {
      // Không cho nhập quá sô lượng ký tự
      if (
        (this.nameInput === this.helper.NameInput.EmployeeCode ||
          this.nameInput === this.helper.NameInput.IdentificationCard ||
          this.nameInput === this.helper.NameInput.BankAccount ||
          this.nameInput === this.helper.NameInput.PhoneNumber ||
          this.nameInput === this.helper.NameInput.LandlinePhone) &&
        this.value.length > 20
      ) {
        this.value = this.value.substring(0, 20);
      } else if (
        this.nameInput === this.helper.NameInput.Email &&
        this.value.length > 80
      ) {
        this.value = this.value.substring(0, 80);
      } else {
        this.value = this.value.substring(0, 200);
      }

      if (
        this.nameInput === this.helper.NameInput.IdentificationCard ||
        this.nameInput === this.helper.NameInput.BankAccount
      ) {
        this.value = this.value.replaceAll(/[^\d]/g, "");
      } else if (
        this.nameInput === this.helper.NameInput.PhoneNumber ||
        this.nameInput === this.helper.NameInput.LandlinePhone
      ) {
        this.value = this.value.replaceAll(/[a-zA-Z]/g, "");
      }
    },
    /**
     * Hàm Thực hiện valide dữ liệu khi blur input
     * @author: Nguyễn Văn Trúc (10/12/2023)
     */
    blurValude() {
      if (
        this.required === true &&
        (this.value === "" || this.value === undefined || this.value === null)
      ) {
        this.isErrorBlur = this.label + this.resource.MInput.NotEmpty;
      } else {
        this.isErrorBlur = null;
        this.$emit("removeClassErrorInput", false);
      }
    },
    /**
     * Hàm Thực hiện bôi đen khi click vào
     * @author: Nguyễn Văn Trúc (10/12/2023)
     */
    blackenInput() {
      this.$refs["input_" + this.nameInput].select();
    },
  },
};
</script>
<style scoped>
.inputError {
  border: 1px solid red;
}
.inputErrorBlur {
  border: 1px solid red;
}
.childError {
  position: absolute;
  color: red;
  font-size: 14px;
}
.textRight {
  text-align: right;
  padding-right: 20px;
}
</style>
