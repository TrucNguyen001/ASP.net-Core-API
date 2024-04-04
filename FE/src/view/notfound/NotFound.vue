<template>
  <div class="coming-soon-container 404-page">
    <div class="coming-soon-graphic">
      <div class="img-graphic" style="text-align: center">
        <div class="not-found"></div>
      </div>
    </div>
    <div class="coming-soon-title">Oops! Không tìm thấy trang</div>
    <div class="coming-soon-description">
      Đường dẫn đang truy cập đã bị thay đổi hoặc không tồn tại.
    </div>
    <a class="coming-soon-btn" href="/employee">Quay lại trang chủ</a>
  </div>
</template>

<script>
export default {
  name: "NotFound",
};
</script>

<style scoped>
.coming-soon-container {
  min-width: 500px;
  max-width: 800px;
  height: 500px;
  width: 75%;
  height: 500px;
  position: absolute;
  left: 50%;
  top: 100px;
  -ms-transform: translateX(-50%);
  -webkit-transform: translateX(-50%);
  -o-transform: translateX(-50%);
  -moz-transform: translateX(-50%);
  transform: translateX(-50%);
}

.coming-soon-graphic {
  background-position: center;
  background-size: contain;
  min-height: 330px;
  width: 100%;
  background-repeat: no-repeat;
}

.coming-soon-title {
  font-size: 20px;
  line-height: 32px;
  font-weight: bold;
  margin-bottom: 12px;
  text-align: center;
}

.coming-soon-description {
  display: block;
  margin: 0 auto;
  line-height: 24px;
  text-align: center;
  background-repeat: no-repeat;
  background-size: contain;
}

.coming-soon-btn {
  text-align: center;
  margin-top: 16px;
  display: block;
  font-size: 18px;
  font-weight: bold;
  color: #2196f3;
  text-decoration: none;
}
</style>
