$(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        $("#weather").append("Geolocation is not supported by this browser.");
    }
});

function showPosition(position) {
    var apiUrl = `/api/weather?lat=${position.coords.latitude}&lon=${position.coords.longitude}`;
    $.ajax({
        url: apiUrl,
        type: 'get',
        datatype: 'json',
        success: function (response) {
            result = `<div class="card border-blue " style="background-color: transparent">
                              <div class="text-warning h3">${response.sys.country}, ${response.name}</div>
                              <div class="text-warning h5">Temp ${response.main.temp}°</div>
                              <div class="text-warning h5">Feels like ${response.main.feels_like}°</div>
                              </div>`;
            $("#weather").append(result);
        }
    });
}
