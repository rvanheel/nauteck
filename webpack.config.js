const path = require('path');
module.exports = {
    entry: {
        "Account.Index": { import: path.resolve(__dirname, 'TypeScript', 'account', 'index.ts'), filename: '../nauteck.web/wwwroot/js/account/index.js' },
        "Account.Login": { import: path.resolve(__dirname, 'TypeScript', 'account', 'login.ts'), filename: '../nauteck.web/wwwroot/js/account/login.js' },
        "Agenda.Edit": { import: path.resolve(__dirname, 'TypeScript', 'agenda', 'edit.ts'), filename: '../nauteck.web/wwwroot/js/agenda/edit.js' },
        "Agenda.Index": { import: path.resolve(__dirname, 'TypeScript', 'agenda', 'index.ts'), filename: '../nauteck.web/wwwroot/js/agenda/index.js' },
        "Client.Edit": { import: path.resolve(__dirname, 'TypeScript', 'client', 'edit.ts'), filename: '../nauteck.web/wwwroot/js/client/edit.js' },
        "Home.Index": { import: path.resolve(__dirname, 'TypeScript', 'home', 'index.ts'), filename: '../nauteck.web/wwwroot/js/home/index.js' },
        "Invoice.Edit": { import: path.resolve(__dirname, 'TypeScript', 'invoice', 'edit.ts'), filename: '../nauteck.web/wwwroot/js/invoice/edit.js' },
        "Quotation.Edit": { import: path.resolve(__dirname, 'TypeScript', 'quotation', 'edit.ts'), filename: '../nauteck.web/wwwroot/js/quotation/edit.js' },
        "Status.Index": { import: path.resolve(__dirname, 'TypeScript', 'status', 'index.ts'), filename: '../nauteck.web/wwwroot/js/status/index.js' },
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
