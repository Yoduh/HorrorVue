var mode = process.env.NODE_ENV || 'development';
module.exports = {
  configureWebpack: {
    devtool: (mode === 'development') ? 'source-map' : false,
    mode: mode,
    optimization: {
      splitChunks: {
        minSize: 10000,
        maxSize: 250000,
      }
    },
    performance: {
      hints: false,
      maxEntrypointSize: 512000,
      maxAssetSize: 512000
    }
  },
  transpileDependencies: ["vuetify"]
};
