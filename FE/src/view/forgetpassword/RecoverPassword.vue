<template>
  <!-- Bước 1: Nhập email để nhận mã  -->
  <div v-if="page === 1" class="forget-password-body">
    <div>
      <div>
        <div class="form-forget">
          <div class="body-form">
            <div class="header-form">
              <div class="logo-misa"></div>
            </div>
            <div class="recover-password">
              <h2>{{ resource.MForgetPassword.RecoverPassword }}</h2>
            </div>
            <div>
              <p>{{ resource.MForgetPassword.EnterEmail }}</p>
            </div>
            <div class="password">
              <p>{{ resource.MForgetPassword.Password }}</p>
            </div>
            <div class="input-email" style="display: block">
              <input
                v-model="email"
                placeholder="Email"
                class="m-input"
                type="text"
                :class="{ input_error_email: error.length > 0 }"
                id="enter-email"
                @click="common.blackenInput('enter-email')"
                @keydown.enter="getPassword"
                @blur="blurEmail"
              />
              <span class="error">{{ error }}</span>
            </div>
            <div @click="getPassword" class="btn">
              {{ resource.MForgetPassword.RecoverPassword }}
            </div>
          </div>
          <div class="footer-form">
            <div @click="backLogin" class="footer-form-left">
              {{ resource.MForgetPassword.BackLogin }}
            </div>
            <div class="footer-form-right">
              {{ resource.MForgetPassword.UsePhoneNumber }}
            </div>
          </div>
        </div>
      </div>
      <div class="help">
        {{ resource.MForgetPassword.Help }}
        <div class="icon icon-caret-down-small"></div>
      </div>
    </div>
  </div>

  <!-- Bước 2: Nhập mã gửi về email -->
  <div v-if="page === 2" class="forget-password-body">
    <div>
      <div>
        <div class="form-forget">
          <div class="body-form">
            <div class="header-form">
              <div class="logo-misa"></div>
            </div>
            <div class="recover-password">
              <h2>{{ resource.MForgetPassword.RecoverPassword }}</h2>
            </div>
            <div>
              <p>{{ resource.MForgetPassword.CodeAuth }}</p>
            </div>
            <div class="check-email" style="margin: 4px">
              <p>
                <b>{{ this.common.replaceEmailChars(email) }}</b
                >{{ resource.MForgetPassword.CheckEmail }}
              </p>
            </div>
            <div class="password">
              <p>{{ resource.MForgetPassword.EnterInput }}</p>
            </div>
            <div class="input-email" style="display: block">
              <input
                v-model="code"
                placeholder="Mã xác thực"
                class="m-input"
                type="text"
                :class="{ input_error: error.length > 0 }"
                id="code-auth"
                @click="common.blackenInput('code-auth')"
                @keydown.enter="confirmCode"
                @blur="blurEmail"
              />
              <span class="error">{{ error }}</span>
            </div>
            <div @click="confirmCode" class="btn">
              {{ resource.MForgetPassword.Continue }}
            </div>
          </div>
          <div class="footer-form">
            <div @click="backLogin" class="footer-form-left">
              {{ resource.MForgetPassword.BackLogin }}
            </div>
            <div class="footer-form-right">
              {{ resource.MForgetPassword.UsePhoneNumber }}
            </div>
          </div>
        </div>
      </div>
      <div class="help">
        {{ resource.MForgetPassword.Help }}
        <div class="icon icon-caret-down-small"></div>
      </div>
    </div>
  </div>

  <!-- Bước 3: Cập nhật lại mật khẩu -->
  <div v-if="page === 3" class="forget-password-body">
    <div>
      <div>
        <div class="form-forget">
          <div class="body-form">
            <div class="header-form">
              <div class="logo-misa"></div>
            </div>
            <div class="recover-password">
              <h2>
                {{ resource.MForgetPassword.CreateNewPassword }}
              </h2>
            </div>
            <div>
              <p>{{ resource.MForgetPassword.FormatLetters }}</p>
            </div>
            <div class="password">
              <p>{{ resource.MForgetPassword.UpperLower }}</p>
            </div>
            <div
              :class="{
                input_error_password: errorNewPassword.length > 0,
              }"
              class="input-email"
            >
              <div class="input-icon new-password">
                <div
                  @click="toggleNewPassword"
                  class="icon-pass icon-show-pass"
                  :class="{
                    icon_show_pass: iconPassNewPassword === false,
                    icon_hide_pass: iconPassNewPassword === true,
                  }"
                ></div>
                <input
                  :class="{ input_error: errorNewPassword.length > 0 }"
                  v-model="newPassword"
                  :placeholder="resource.MLogin.PasswordNew"
                  class="m-input"
                  :type="!isNewPassword ? 'password' : 'text'"
                  id="new-password"
                  @click="common.blackenInput('new-password')"
                />
                <span class="error">{{ errorNewPassword }}</span>
              </div>
            </div>
            <div
              :class="{
                input_error_password2: errorConfirmNewPassword.length > 0,
              }"
              class="input-email"
            >
              <div class="input-icon confirm-new-password">
                <div
                  @click="toggleConfirmNewPassword"
                  class="icon-pass icon-show-pass"
                  :class="{
                    icon_show_pass: iconPassConfirmNewPassword === false,
                    icon_hide_pass: iconPassConfirmNewPassword === true,
                  }"
                ></div>
                <input
                  :class="{
                    input_error: errorConfirmNewPassword.length > 0,
                  }"
                  v-model="confirmNewPassword"
                  :placeholder="resource.MLogin.PasswordNewConfirm"
                  class="m-input"
                  :type="!isConfirmNewPassword ? 'password' : 'text'"
                  id="confirm-new-password"
                  @click="common.blackenInput('confirm-new-password')"
                  @keydown.enter="createPassword"
                />
                <span class="error">{{ errorConfirmNewPassword }}</span>
              </div>
            </div>
            <div @click="createPassword" class="btn create-password">
              {{ resource.MForgetPassword.CreatePassword }}
            </div>
          </div>
          <div class="footer-form step3-footer">
            <div @click="backLogin" class="footer-form-left">
              {{ resource.MForgetPassword.BackLogin }}
            </div>
          </div>
        </div>
      </div>
      <div class="help">
        {{ resource.MForgetPassword.Help }}
        <div class="icon icon-caret-down-small"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RecoverPassword",
  data() {
    return {
      page: 1,
      email: "",
      error: "",
      code: "",
      errorNewPassword: "",
      errorConfirmNewPassword: "",
      newPassword: "",
      confirmNewPassword: "",
      iconPassNewPassword: true,
      iconPassConfirmNewPassword: true,
      isNewPassword: false,
      isConfirmNewPassword: false,
    };
  },
  methods: {
    /**
     * Quay lại đăng nhập
     * @author: Nguyễn Văn Trúc(10/3/2024)
     */
    backLogin() {
      this.$router.push(this.helper.Router.Login);
    },
    /**
     * Hàm lấy mật khẩu
     * @author: Nguyễn Văn Trúc(10/3/2024)
     */
    async getPassword() {
      try {
        if (this.email === "") {
          this.error = this.resource.MForgetPassword.ErrorEmailEmpty;
          return;
        }
        let check = this.common.checkEmail(this.email);
        if (check) {
          this.common.showLoading();
          let result = await this.apiService.getByInfo(
            this.helper.MApi.SendEmail,
            this.email
          );
          this.error = "";
          if (result === 1) {
            this.error = "";
            this.page = 2;
          }
        } else {
          this.error = this.resource.MForgetPassword.ErrorFormatEmail;
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.common.showLoading(false);
      }
    },

    /**
     * Hàm thực hiện khi blur email
     * @author: Nguyễn Văn Trúc(10/3/2024)
     */
    blurEmail() {
      try {
        if (this.common.checkEmail(this.email) && this.email !== "") {
          this.error = "";
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Xác nhận mã gửi về email
     * @author: Nguyễn Văn Trúc(10/3/2024)
     */
    async confirmCode() {
      try {
        if (this.code === "") {
          this.error = this.resource.MForgetPassword.ErrorCode;
        } else {
          this.error = "";
          this.common.showLoading();
          let result = await this.apiService.getByInfo(
            this.helper.MApi.CheckRecoverCode,
            this.code + "/" + this.email
          );
          result === 1 ? (this.page = 3) : null;
        }
      } catch (error) {
        console.log(error);
      } finally {
        this.common.showLoading(false);
      }
    },

    /**
     * Hàm kiểm tra input nhập
     * @author: Nguyễn Văn Trúc(10/3/2024)
     */
    checkInputPassword() {
      try {
        let check = true;
        if (this.newPassword === "") {
          this.errorNewPassword = this.resource.MForgetPassword.NewPasswordNull;
          check = false;
        } else {
          if (!this.common.checkFormatPassword(this.newPassword)) {
            this.errorNewPassword =
              this.resource.MForgetPassword.ErrorValidPassword;
            check = false;
          } else {
            this.errorNewPassword = "";
          }
        }
        if (this.confirmNewPassword === "") {
          this.errorConfirmNewPassword =
            this.resource.MForgetPassword.ConfirmNewPasswordNull;
          check = false;
        } else {
          if (this.newPassword !== this.confirmNewPassword) {
            this.errorConfirmNewPassword =
              this.resource.MForgetPassword.PasswordDifferent;
            check = false;
          } else {
            this.errorConfirmNewPassword = "";
          }
        }
        return check;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm tạo mật khẩu mới
     * @author: Nguyễn Văn Trúc(10/3/2024)
     */
    async createPassword() {
      try {
        if (this.checkInputPassword()) {
          this.common.showLoading();
          let account = await this.apiService.getByInfo(
            this.helper.MApi.GetByEmail,
            this.email
          );
          account.Password = this.newPassword;
          let result = 0;
          account !== null
            ? (result = await this.apiService.update(
                this.helper.MApi.UpdatePassword,
                account.AccountId,
                account
              ))
            : "";
          this.common.showLoading(false);
          if (result === 1) {
            this.common.showToast(
              this.resource.DialogUpdatePasswordSuccess.Content
            );
            localStorage.setItem(this.helper.LocalStorage.Email, this.email);
            this.$router.push(this.helper.Router.Login);
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Ẩn hiển mật khẩu
     * @author Nguyễn Văn Trúc (16/2/2024)
     */
    toggleNewPassword() {
      this.iconPassNewPassword = !this.iconPassNewPassword;
      this.isNewPassword = !this.isNewPassword;
    },
    /**
     * Ẩn hiển mật khẩu
     * @author Nguyễn Văn Trúc (16/2/2024)
     */
    toggleConfirmNewPassword() {
      this.iconPassConfirmNewPassword = !this.iconPassConfirmNewPassword;
      this.isConfirmNewPassword = !this.isConfirmNewPassword;
    },
  },
  // mounted() {
  //   // Khi có lỗi api
  //   this.emitter.on(this.helper.Emitter.ErrorLogin, (value) => {
  //     this.error = value;
  //   });
  // },
};
</script>

<style scoped>
@import url(../../css/pages/forgetpassword.css);
.error {
  color: red;
  font-size: 14px;
}
.input_error,
.input_error_email {
  border: 1px solid red;
}
.forget-password-body input {
  font-size: 15px;
}
.input-email {
  display: block;
}
.input-icon {
  width: 424px;
}
.confirm-new-password {
  margin-top: 60px;
}
.create-password {
  margin-top: 130px;
}
.input_error_password {
  height: 24px;
}
.input_error_password2 {
  height: 14px;
}
</style>
