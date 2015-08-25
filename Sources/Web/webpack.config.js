var webpack = require("webpack");

module.exports = {
    cache: true,
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
    new webpack.optimize.UglifyJsPlugin(),
    new webpack.optimize.OccurenceOrderPlugin(),
    new webpack.optimize.DedupePlugin()
    ],
    module: {
        loaders: [
        { test: /\.js$/, loader: 'babel-loader', exclude: /node_modules/ }
        ]
    }
};
