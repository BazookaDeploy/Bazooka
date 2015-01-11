module.exports = {
    cache: true,
    entry: './App/index',
    output: {
        filename: './App/browser-bundle.js'
    },
    module: {
        loaders: [
        { test: /\.js$/, loader: 'jsx-loader?harmony', exclude: /node_modules/ }
        ]
    }
};
