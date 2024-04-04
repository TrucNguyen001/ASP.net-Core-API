<template>
  <header-web />
  <div class="main">
    <nav-bar-vue />
    <content-web-vue />
  </div>
</template>
<script>
import ContentWebVue from "./ContentWeb.vue";
import NavBarVue from "./NavBar.vue";
import HeaderWeb from "./HeaderWeb.vue";
export default {
  name: "MainWeb",
  components: { ContentWebVue, NavBarVue, HeaderWeb },
};
</script>
<style></style>
