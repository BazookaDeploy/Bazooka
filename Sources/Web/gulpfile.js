/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var webpackOrig = require('webpack');
var webpack = require('gulp-webpack');
var sass = require('gulp-sass');

gulp.task('javascript', function () {
    // place code for your default task here
    return gulp.src('App/index.js')
               .pipe(webpack({
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
               }))
               .pipe(gulp.dest('App/'));
});

gulp.task('javascript-prod', function () {
    // place code for your default task here
    return gulp.src('App/index.js')
               .pipe(webpack({
                   cache: true,
                   entry: './App/index',
                   output: {
                       filename: './App/browser-bundle.js'
                   },
                   plugins: [
                     new webpackOrig.DefinePlugin({
                         'process.env': {
                             // This has effect on the react lib size
                             'NODE_ENV': JSON.stringify('production'),
                         }
                     }),
                     new webpackOrig.optimize.UglifyJsPlugin(),
                     new webpackOrig.optimize.OccurenceOrderPlugin(),
                     new webpackOrig.optimize.DedupePlugin()
                   ],
                   module: {
                       loaders: [
                       { test: /\.js$/, loader: 'babel-loader', exclude: /node_modules/ }
                       ]
                   }
               }))
               .pipe(gulp.dest('App/'));
});

gulp.task("css", function () {
    gulp.src('./Content/main.scss')
      .pipe(sass().on('error', sass.logError))
      .pipe(gulp.dest('./Content/'));
});

gulp.task("css-prod", function () {
    gulp.src('./Content/main.scss')
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(gulp.dest('./Content/'));
});

gulp.task("default", ["javascript", "css"]);
gulp.task("build", ["javascript-prod", "css-prod"]);