import axios from "axios";
import resource from "../helper/resource.js";
import helper from "../helper/helper.js";
import apiService from "../apiservice/apiservice.js";
import tinyEmitter from "tiny-emitter/instance";
import common from "../common/common.js";
const url = `${helper.URL}api/v1/`;
// Thực trước khi api được gửi đi
axios.interceptors.request.use(async (config) => {
  let accessToken = localStorage.getItem(helper.LocalStorage.Token);
  // Kiểm tra nếu URL không phải là endpoint refresh-token hoặc login
  if (
    config.url !== `${url}${helper.MApi.RefreshToken}` &&
    config.url !== `${url}${helper.MApi.LoginAccount}`
  ) {
    config.headers["Authorization"] = `Bearer ${accessToken}`;
  } else {
    return config;
  }
  let refreshToken = localStorage.getItem(helper.LocalStorage.RefreshToken);
  let expiration = localStorage.getItem(helper.LocalStorage.Expiration);

  let tokenModel = {
    AccessToken: accessToken,
    RefreshToken: refreshToken,
    Expiration: null,
  };

  if (
    config.headers.Authorization && // Kiểm tra xem yêu cầu có chứa Authorization header hay không
    new Date() >= new Date(expiration) // Kiểm tra xem token đã hết hạn hay không
  ) {
    try {
      // Thực hiện refresh token
      let result = await apiService.post(helper.MApi.RefreshToken, tokenModel);

      // Cập nhật token mới và các thông tin liên quan trong local storage
      localStorage.setItem(helper.LocalStorage.Token, result.AccessToken);
      localStorage.setItem(
        helper.LocalStorage.RefreshToken,
        result.RefreshToken
      );
      localStorage.setItem(helper.LocalStorage.Expiration, result.Expiration);

      // Cập nhật Authorization header của yêu cầu với token mới
      config.headers.Authorization = `Bearer ${result.AccessToken}`;
    } catch (error) {
      console.log(error);
    }
  }
  // Trả về config sau khi hoàn thành refresh token
  return config;
});

// Thực khi khi kết quả api trả về
axios.interceptors.response.use(
  (response) => {
    if (
      response.config.url.indexOf("/post") > 0 ||
      response.config.url.indexOf("/put") > 0
    ) {
      common.showToast(resource.ContentSuccess.SaveDataSuccess);
    }

    if (response.config.url.indexOf("/delete") > 0) {
      common.showToast(resource.ContentSuccess.DeleteDataSuccess);
    }
    return response;
  },
  async (error) => {
    if (!error.response) {
      common.showDialog(
        resource.ErrorNetwork.Content,
        resource.ErrorNetwork.Title
      );
    }
    switch (error.response.status) {
      case 400:
        {
          let data = error.response.data.Errors[0];
          if (error.config.url.indexOf(helper.MApi.ForgetPassword) > 0) {
            // Gửi thông báo để hiển thị dưới input
            tinyEmitter.emit(helper.Emitter.ErrorLogin, data);
          }
          common.showDialog(data);
        }
        break;
      case 401:
        if (error.config.url.indexOf(helper.MApi.LoginAccount) >= 0) {
          let data = error.response.data.UserMsg;
          // Gửi thông báo để hiển thị dưới input
          tinyEmitter.emit(helper.Emitter.ErrorLogin, data);
        } else {
          common.showDialog(error.response.data.Errors);
        }
        break;
      case 500:
        common.showDialog(resource.DisConnect.Content);
        break;
      default:
        common.showDialog(resource.MessError.Content);
        break;
    }
  }
);
