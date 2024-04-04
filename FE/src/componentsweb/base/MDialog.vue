<template>
  <div class="show-dialog" v-if="isShowDialog">
    <div class="dialog">
      <div class="header-dialog">
        <div class="header-dialog-title">{{ title }}</div>
        <div class="header-dialog-close">
          <div @click="closeDialog" class="icon icon-close cancel-delete"></div>
        </div>
      </div>
      <div class="body-dialog">
        <div
          class="icon"
          :class="{
            icon_question: typeIcon === 'icon_confirm_question',
            icon_info: typeIcon === 'icon_info',
            icon_confirm_warning: typeIcon === 'icon_confirm_warning',
            icon_successful: typeIcon === 'icon_successful',
            icon_error: typeIcon === 'icon_error',
            icon_confirm_delete: typeIcon === 'icon_confirm_delete',
          }"
        ></div>
        <div class="content-dialog">
          <ul v-if="typeof content === 'object'">
            <div v-for="(value, key, index) in content" :key="index">
              <li v-if="value">{{ value }}</li>
            </div>
          </ul>
          <ul v-else>
            {{
              content
            }}
          </ul>
        </div>
      </div>
      <div class="footer-dialog">
        <div class="footer-left">
          <button
            v-show="isShowButtonDestroyDialog"
            @click="deleteDialog"
            class="m-button m-button-cancel"
          >
            {{ resource.MDialog.Destroy }}
          </button>
        </div>
        <div class="dialog-button">
          <button
            @click="closeDialog"
            class="m-button m-button-cancel cancel-delete"
          >
            {{ resource.MDialog.No }}
          </button>
          <button
            @click="acceptDialog"
            class="m-button m-button-success accept-delete"
          >
            {{ resource.MDialog.Yes }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MDialog",
  data() {
    return {
      show: false,
      language: "",
    };
  },
  props: {
    isShowDialog: Boolean,
    title: String,
    content: Object,
    typeIcon: String,
    isShowButtonDestroyDialog: Boolean,
  },
  methods: {
    /**
     * Huỷ dialog
     * @author: Nguyễn Văn Trúc (9/12/2023)
     */
    deleteDialog: function () {
      this.$emit("deleteDialog", false);
    },
    /**
     * Đóng dialog
     * @author: Nguyễn Văn Trúc (11/12/2023)
     */
    closeDialog: function () {
      this.$emit("closeDialog", false);
    },
    /**
     * Chấp nhận dialog
     * @author: Nguyễn Văn Trúc (11/12/2023)
     */
    acceptDialog: function () {
      this.$emit("acceptDialog", false);
    },
  },
  created() {
    this.language = localStorage.getItem("language");
  },
};
</script>
<style scoped>
.hideDialog {
  display: block;
}
.body-dialog {
  margin-bottom: 46px;
}
.body-dialog li {
  /* color: red; */
}
</style>
