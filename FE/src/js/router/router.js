import { createRouter, createWebHistory } from "vue-router";
import employee from "../../componentsweb/MainWeb.vue";
import file from "../../view/importfile/ImportFile.vue";
import login from "../../view/login/LoginApp.vue";
import forgetPassword from "../../view/forgetpassword/RecoverPassword.vue";
import notfound from "../../view/notfound/NotFound.vue";

const routes = [
  {
    path: "/",
    redirect: "/login",
  },
  {
    path: "/login",
    component: login,
  },
  {
    path: "/employee",
    component: employee,
    meta: { requiresAuth: true },
  },
  {
    path: "/file",
    component: file,
    meta: { requiresAuth: true },
  },
  {
    path: "/forget",
    component: forgetPassword,
  },
  {
    path: "/:pathMatch(.*)*",
    component: notfound,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

// Kiểm tra trạng thái đăng nhập trước mỗi lần chuyển route
router.beforeEach((to, from, next) => {
  let loggedIn = localStorage.getItem("isLogin"); // Kiểm tra trạng thái đăng nhập từ local storage

  if (
    to.matched.some((record) => record.meta.requiresAuth) &&
    loggedIn !== "true"
  ) {
    next("/login"); // Nếu cần đăng nhập và chưa đăng nhập, chuyển hướng đến trang đăng nhập
  } else {
    next(); // Nếu đã đăng nhập hoặc không yêu cầu đăng nhập, cho phép truy cập
  }
});

export default router;
