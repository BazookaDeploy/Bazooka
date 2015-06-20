var webpack = require("webpack");

module.exports = {
    cache: true,
    entry: './App/index',
    output: {
        filename: './App/browser-bundle.js'
    },
    plugins: [
      new webpack.DefinePlugin({
          'process.env': {
              // This has effect on the react lib size
              'NODE_ENV': JSON.stringify('production'),
          }
      }),
    ],
    module: {
        loaders: [
        { test: /\.js$/, loader: 'jsx-loader?harmony', exclude: /node_modules/ }
        ]
    }
};
