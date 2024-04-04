<template>
  <div class="m-show-toast" :class="{ hideDialog: isShowToast }" hidden>
    <div class="m-toast load-data-success">
      <div class="m-toast-left">
        <div class="icon icon_successful"></div>
        <div class="m-toast-content">{{ content }}</div>
      </div>
      <div class="m-toast-right">
        <div
          @click="closeToast"
          class="icon icon-close close-toast-load-data"
        ></div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MToast",
  data() {
    return {};
  },
  props: { isShowToast: Boolean, content: String },
  methods: {
    /**
     * Đóng toast
     * @author: Nguyễn Văn Trúc (24/1/2024)
     */
    closeToast: function () {
      this.$emit("closeToast", false);
    },
  },
};
</script>
<style scoped>
.hideDialog {
  display: block;
}
</style>
