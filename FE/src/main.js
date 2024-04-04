import { createApp } from "vue";
import App from "./App.vue";
import router from "./js/router/router.js";
import DatePicker from "vue3-datepicker";

import MInput from "./componentsweb/base/MInput.vue";
import MDate from "./componentsweb/base/MDate.vue";
import MDialog from "./componentsweb/base/MDialog.vue";
import MCheckBox from "./componentsweb/base/MCheckBox.vue";
import MToast from "./componentsweb/base/MToast.vue";
import MCombobox from "./componentsweb/base/MCombobox.vue";
import MDropDownList from "./componentsweb/base/MDropDownList.vue";
import MPagination from "./componentsweb/base/MPagination.vue";
import MHelper from "./js/helper/helper.js";
import tinyEmitter from "tiny-emitter/instance";
import axios from "axios";
import MApiService from "./js/apiservice/apiservice";
import MCommon from "./js/common/common.js";
import MResource from "./js/helper/resource.js";

import "./js/interceptors/interceptors.js";
import "./js/keyboardevenhandler/keyboardeventhandler.js";

const app = createApp(App);
app.use(DatePicker);

app.component("MISAInput", MInput);
app.component("MISADate", MDate);
app.component("MISACheckBox", MCheckBox);
app.component("MISADialog", MDialog);
app.component("MISAToast", MToast);
app.component("MISACombobox", MCombobox);
app.component("MISAPagination", MPagination);
app.component("MISADropDownList", MDropDownList);
app.component("DatePicker", DatePicker);
app.config.globalProperties.api = axios;
app.config.globalProperties.emitter = tinyEmitter;
app.config.globalProperties.helper = MHelper;
app.config.globalProperties.resource = MResource;
app.config.globalProperties.apiService = MApiService;
app.config.globalProperties.common = MCommon;

app.use(router).mount("#app");
