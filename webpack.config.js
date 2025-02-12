const path = require('path');
module.exports = {
    entry: {
        "Account.Login": { import: path.resolve(__dirname, 'TypeScript', 'account', 'login.ts'), filename: '../nauteck.web/wwwroot/js/account/login.js' },
        "Order.Edit": { import: path.resolve(__dirname, 'TypeScript', 'order', 'edit.ts'), filename: '../nauteck.web/wwwroot/js/order/edit.js' },
    },    
    module: {        
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/,
            },
        ],
    },
    mode: 'production',
    resolve: {
        extensions: ['.ts', '.tsx', '.js'],
    }
};
