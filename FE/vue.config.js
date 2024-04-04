const { defineConfig } = require("@vue/cli-service");

module.exports = defineConfig({
  transpileDependencies: true,
  configureWebpack: {
    devtool: "source-map", // Add this line to enable source maps
  },
});
// module.exports = {
//   devServer: {
//     proxy: {
//       "/api": {
//         target: "https://localhost:8888",
//         changeOrigin: true,
//         pathRewrite: {
//           "^/api": "",
//         },
//       },
//     },
//   },
// };
