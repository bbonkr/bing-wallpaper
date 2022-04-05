const webpack = require('webpack');
const path = require('path');

module.exports = {
    // mode: isDevelpoment() ? 'development' : environmentName,
    // devtool: isDevelpoment() ? 'inline-source-map' : 'hidden-source-map',
    resolve: {
        extensions: ['.ts', '.tsx', '.js', '.jsx'],
    },
    entry: {
        bingImageApp: path.resolve('src/BingImageApp/index'),
    },
    module: {
        rules: [
            {
                test: /\.m?js/,
                resolve: {
                    fullySpecified: false,
                },
            },
            {
                test: /\.tsx?$/,
                exclude: /node_modules/,
                use: ['babel-loader', 'ts-loader'],
            },
            {
                test: /\.css$/,
                use: [{ loader: 'style-loader' }, { loader: 'css-loader' }],
            },
        ],
    },
    plugins: [new webpack.LoaderOptionsPlugin()],
    output: {
        filename: '[name]/[name].bundle.js',
        path: path.join(path.resolve(__dirname, '..'), 'wwwroot', 'js'),
        publicPath: '/js/',
    },
};
