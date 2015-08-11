var webpack = require("webpack");

module.exports = {
    cache: true,
    entry: './App/index',
    output: {
        filename: './App/browser-bundle.js'
    },
    module: {
        loaders: [
        { test: /\.js$/, loader: 'babel-loader', exclude: /node_modules/ }
        ]
    }
};
