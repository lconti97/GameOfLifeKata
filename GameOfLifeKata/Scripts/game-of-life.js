function something() {
    var request_url = 'http://' + window.location.host + '/api/GameOfLife/GetNextGeneration';

    var request = $.ajax({
        url: request_url,
        data: pars,
        dataType: 'json',
        processData: true
    });

    request.done(function (data) {
        api_response = data;
        if (typeof (callback) === 'function') {
            callback(data);
        }
    });
    request.fail(function (data) {
        api_response = data;
        if (typeof (callback) === 'function') {
            callback(data);
        }
    });
}