<template>
  <div class="header">
    <div class="header-left">
      <div class="icon icon-toggle"></div>
      <div class="icon-logo"></div>
      <div class="name">{{ resource.MHeader.Name }}</div>
    </div>
    <div class="header-right">
      <div class="header-right-start">
        <div class="icon icon-bars"></div>
        <div class="name-company">
          {{ resource.MHeader.NameCompany }}
        </div>
        <div class="icon icon-down-small"></div>
      </div>
      <div class="header-right-end">
        <div class="icon icon-bell"></div>
        <div v-if="userName !== null" class="icon icon-user"></div>
        <div v-if="userName != null" class="name">{{ userName }}</div>
        <div
          style="padding: 0 10px; color: dodgerblue; cursor: pointer"
          v-if="userName === null"
          @click="loginPage"
        >
          {{ resource.MHeader.Login }}
        </div>
        <div>
          <div @click="toggleLogout" class="icon icon-down-small"></div>
          <div
            @click="logoutAccount"
            :class="{ show_logout: isHideLogout }"
            class="logout"
          >
            {{ resource.MHeader.Logout }}
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- Loading hiển thị khi load dữ liệu -->
  <div v-if="showLoading" class="m-loading">
    <div class="icon icon-loading"></div>
  </div>
</template>
<script>
export default {
  name: "HeaderWeb",
  data() {
    return {
      userName: "Admin",
      isHideLogout: true,
      showLoading: false,
    };
  },
  methods: {
    /**
     * Chuyển đến trang đăng nhập
     * @author Nguyễn Văn Trúc(18/2/2024)
     */
    loginPage() {
      this.$router.push("/login");
    },
    /**
     * Ẩn hiện đăng xuất
     * @author Nguyễn Văn Trúc(1/3/2024)
     */
    toggleLogout() {
      this.isHideLogout = !this.isHideLogout;
    },
    /**
     * Đăng xuất tài khoản
     * @author Nguyễn Văn Trúc(1/3/2024)
     */
    logoutAccount() {
      this.showLoading = true;
      this.isHideLogout = true;
      this.removeRefreshToken();
    },

    /**
     * Xoá refresh token khi đăng xuất
     * @author: Nguyễn Văn Trúc(2/3/2024)
     */
    async removeRefreshToken() {
      try {
        await this.apiService.getByInfo(this.helper.MApi.Login, this.userName);
        this.showLoading = false;
        this.$router.push("/login");
      } catch (error) {
        console.log(error);
      }
    },
  },
  /**
   * Lấy giá trị từ local
   * @author Nguyễn Văn Trúc(18/2/2024)
   */
  created() {
    this.userName = localStorage.getItem("userName");
  },
};
</script>
<style scoped>
.logout {
  height: 36px;
  width: 100px;
  border: 1px solid #bdbdbd;
  background: #fafafa;
  border-radius: 4px;
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  margin-left: -80px;
  cursor: pointer;
}
.logout:hover {
  background: #eeeeee;
}
.show_logout {
  display: none;
}
</style>
