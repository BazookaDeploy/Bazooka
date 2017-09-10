var webpack = require("webpack");
var CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
    cache: true,
    watch:true,
    entry: './App/index',
    output: {
        filename: './App/browser-bundle.js'
    },
    plugins:[
            new webpack.DefinePlugin({
                'process.env': {
                    // This has effect on the react lib size
                    'NODE_ENV': JSON.stringify('production'),
                }
        }),
            new CopyWebpackPlugin([
                {
                    from: 'node_modules/monaco-editor/min/vs',
                    to: 'vs',
                }
            ])
    ],
    module: {
        loaders: [
          {
              test: /.jsx?$/,
              loader: 'babel-loader',
              exclude: /node_modules/,
              query: {
                  presets: ['es2015', 'react']
              }
          }
        ]
    },
};
