/*global -$ */
'use strict';
var gulp = require('gulp');
var $ = require('gulp-load-plugins')();
var del = require('del');
var runSequence = require('run-sequence');

gulp.task('styles', function () {
  return gulp.src('build/main.scss')
    .pipe($.sourcemaps.init())
    .pipe($.sass({
      outputStyle: 'nested', // libsass doesn't support expanded yet
      precision: 10,
      includePaths: ['.'],
      onError: console.error.bind(console, 'Sass error:')
    }))
    .pipe($.postcss([
      require('autoprefixer-core')({browsers: ['last 1 version']})
    ]))
    //.pipe($.sourcemaps.write())    
    .pipe(gulp.dest('dist/css'));    
});

gulp.task('build', ['styles'], function () {
  return gulp.src('dist/css/*.css')
    .pipe($.csso())    
    .pipe($.rename({
      suffix: '.min'
    }))
    .pipe(gulp.dest('dist/css/'));
});

// Lint JavaScript
gulp.task('jshint', function () {
  return gulp.src('javascripts/**/*.js')    
    .pipe($.jshint())
    .pipe($.jshint.reporter('jshint-stylish'))   
});

gulp.task('clean', del.bind(null, ['dist/css/main.css', 'dist/css/main.min.css'], {dot: true}));

// Build production files, the default task
gulp.task('default', ['clean'], function (cb) {
  runSequence(['jshint'], 'build', cb);
});
