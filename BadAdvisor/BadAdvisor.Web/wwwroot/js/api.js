const apiGet = (route, successCallback, errorCallback) => {
    $.ajax({ 
        url: route,
        method: 'GET',
        success: successCallback,
        error: (err) => {
            if (errorCallback) {
                errorCallback();
            }
            alert('Sorry, an Error occurred. Try again later');
            console.log(err);
        }
    });
}
