var request = require('request');

var options = {
    url: 'http://www.cepaberto.com/api/v2/ceps.json?cep=40010000 [www.cepaberto.com]',
    headers: {
        'Authorization': 'Token token=YOUR_TOKEN'
    }
};

function callback(error, response, body) {
    if (!error && response.statusCode == 200) {
        var info = JSON.parse(body);
        console.log(info);
    }
}
request(options, callback);