<template>
  <div class="img-background">
    <div class="language-nation">
      <div @click="showLanguage" class="change-language">
        <div
          :class="{
            icon_vn: resource.MLogin.VN === 'VN',
            icon_en: resource.MLogin.VN === 'EN',
          }"
          class="icon-nation"
        ></div>
        <div class="language">{{ resource.MLogin.Language }}</div>
      </div>
      <div v-show="showChangeLanguage" class="select-language">
        <div @click="changeLanguageVN">Tiếng Việt</div>
        <div @click="changeLanguageEN">English</div>
      </div>
    </div>
    <div>
      <div class="form-login">
        <div class="form-login-header">
          <div class="icon-amis-platform2"></div>
        </div>
        <div class="form-login-body">
          <div class="login-account">
            <input
              :placeholder="resource.MLogin.Account"
              class="m-input"
              type="text"
              v-model="accountLogin.Account"
              ref="input_account"
              id="account"
              :class="{ error_account: errors.account !== null }"
              @click="this.blackenInput('account')"
            />
            <p v-show="errors.account !== null" class="error-account">
              {{ errors.account }}
            </p>
          </div>
          <div class="login-password">
            <div class="input-icon">
              <div
                @click="togglePassword"
                class="icon-pass icon-show-pass"
                :class="{
                  icon_show_pass: iconPass === false,
                  icon_hide_pass: iconPass === true,
                }"
              ></div>
              <input
                :placeholder="resource.MLogin.Password"
                class="m-input"
                :type="isPassword ? 'password' : 'text'"
                v-model="accountLogin.PassWord"
                id="password"
                @keydown.enter="login"
                @click="this.blackenInput('password')"
                :class="{ error_password: errors.password !== null }"
              />
              <p v-show="errors.password !== null" class="error-password">
                {{ errors.password }}
              </p>
            </div>
          </div>
          <div
            @click="recoverPassword"
            :class="{ is_error_password: errors.password !== null }"
            class="forget-password"
          >
            {{ resource.MLogin.ForgetPassword }}
          </div>
          <div @click="login" class="login">
            {{ resource.MLogin.Login }}
          </div>
        </div>
        <div class="form-login-footer">
          <div class="login-with">
            <div class="line-right"></div>
            <div class="form-login-footer-title">
              {{ resource.MLogin.LoginWith }}
            </div>
            <div class="line-right"></div>
          </div>
          <div class="icon-logo-login">
            <div class="icon-login icon-google"></div>
            <div class="icon-login icon-apple"></div>
            <div class="icon-login icon-microsoft"></div>
          </div>
        </div>
      </div>
      <div class="copy-right">{{ resource.MLogin.CopyRight }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "LoginApp",
  data() {
    return {
      account: "",
      password: "",
      iconPass: true,
      isPassword: true,
      errors: {
        account: null,
        password: null,
      },
      accountLogin: {},
      showChangeLanguage: false,
    };
  },
  methods: {
    /**
     * Lấy lại mật khẩu
     * @author Nguyễn Văn Trúc (10/3/2024)
     */
    recoverPassword() {
      this.$router.push(this.helper.Router.ForgetPassword);
    },
    /**
     * Ẩn hiển mật khẩu
     * @author Nguyễn Văn Trúc (16/2/2024)
     */
    togglePassword() {
      this.iconPass = !this.iconPass;
      this.isPassword = !this.isPassword;
    },
    /**
     * Kiểm tra tài khoản mật khẩu đã nhập chưa
     * @author Nguyễn Văn Trúc (17/2/2024)
     */
    checkAccount() {
      let me = this;
      let check = true;
      if (me.accountLogin.Account === "") {
        me.errors.account = me.resource.ErrorLogin.AccountNotEmpty;
        check = false;
      } else {
        me.errors.account = null;
      }
      if (me.accountLogin.PassWord === "") {
        me.errors.password = me.resource.ErrorLogin.PasswordNotEmpty;
        check = false;
      } else {
        me.errors.password = null;
      }
      return check;
    },
    /**
     * Bôi đen ô input search khi click vào
     * @param {Id truyền vào} id
     * @author: Nguyễn Văn Trúc (15/3/2024)
     */
    blackenInput(id) {
      try {
        document.getElementById(id).select();
        id === "account"
          ? (this.errors.account = null)
          : id === "password"
          ? (this.errors.password = null)
          : "";
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Thay đổi thành tiếng việt
     * @author Nguyễn Văn Trúc (16/3/2024)
     */
    changeLanguageVN() {
      localStorage.setItem("language", "VN");
      this.showChangeLanguage = false;
      this.$router.go();
    },
    /**
     * Thay đổi thành tiếng anh
     * @author Nguyễn Văn Trúc (16/3/2024)
     */
    changeLanguageEN() {
      localStorage.setItem("language", "EN");
      this.showChangeLanguage = false;
      this.$router.go();
    },
    /**
     * Hiển thị chọn ngôn ngữ
     * @author Nguyễn Văn Trúc (16/3/2024)
     */
    showLanguage() {
      this.showChangeLanguage = !this.showChangeLanguage;
    },
    /**
     * Hàm đăng nhập
     * @author Nguyễn Văn Trúc (17/2/2024)
     */
    async login() {
      try {
        let me = this;
        me.common.showLoading();
        me.checkAccount();
        if (me.checkAccount() === true) {
          let response = await me.apiService.post(
            me.helper.MApi.LoginAccount,
            me.accountLogin
          );
          localStorage.setItem("token", response.AccessToken);
          localStorage.setItem("refreshToken", response.RefreshToken);
          localStorage.setItem("expiration", response.Expiration);
          localStorage.setItem("isLogin", true);
          localStorage.setItem("email", this.accountLogin.Account);

          // Phân tích AccessToken thành các phần tử
          let tokenParts = localStorage.getItem("token").split(".");
          let payload = JSON.parse(atob(tokenParts[1]));

          // Truy cập thông tin từ payload của token
          let userName = payload.UserName;
          //let roles = payload.Roles;
          localStorage.setItem("userName", userName);

          this.$router.push(this.helper.Router.Employee);
        }
        me.common.showLoading(false);
      } catch (error) {
        console.log(error);
      } finally {
        this.common.showLoading(false);
      }
    },
    /**
     * auto focus vào ô đầu tiên khi trang hiển thị
     * @author: Nguyễn Văn Trúc (3/3/2024)
     */
    autoFocus: function () {
      try {
        this.$refs.input_account.focus();
      } catch (error) {
        console.log(error);
      }
    },
  },
  /**
   * Tạo đối tượng đăng nhập
   * @author Nguyễn Văn Trúc (18/2/2024)
   */
  created() {
    this.accountLogin = {
      Account: "",
      PassWord: "",
    };
    localStorage.setItem("token", "");
    localStorage.setItem("refreshToken", "");
    localStorage.setItem("expiration", "");
    localStorage.setItem("userName", "");
    localStorage.setItem("isLogin", false);

    // Gán email cho accout sau khi lấy lại mật khẩu xong
    if (localStorage.getItem("email")) {
      this.accountLogin.Account = localStorage.getItem("email");
    }
  },

  mounted() {
    let me = this;
    this.autoFocus();
    // Khi có lỗi api
    me.emitter.on("errorLogin", (value) => {
      me.common.showLoading(false);
      me.errors.password = value;
    });
  },
};
</script>

<style scoped>
.error_account {
  border: 1px solid red;
}
.error_password {
  border: 1px solid red;
}

.is_error_password {
  margin-top: 90px;
}
.error-account,
.error-password {
  font-size: 12px;
  color: #ff1d1d;
  height: 20px;
  line-height: 20px;
}
</style>
