$("#searchInput").keypress(function (event) {
    if (event.keyCode == 13) {
        event.preventDefault();
        SearchLoader();
        return false;
    }
});

function SearchLoader() {
    const boxes = document.querySelectorAll('.search-element-deletable');
    boxes.forEach(box => {
        box.remove();
    });
    $('#result-list').dropdown('show');
    var token = $("[name='__RequestVerificationToken']").val();
    $.ajax({
        url: `/api/search?searchText=${$("#searchInput").val()}`,
        type: 'get',
        datatype: 'json',
        headers: { 'X-CSRF-TOKEN-HEADERNAME': token },
        success: function (response) {
            var counter = 0;
            if (response.users.length !== 0) {
                counter++;
                document.getElementById("result-list").innerHTML = document.getElementById("result-list").innerHTML + `<div id="result-list-users" class="container text-blue search-element-deletable">Users</div>`;
                response.users.forEach(function (repo) {
                    var innermyspan = document.getElementById("result-list-users").innerHTML;
                    document.getElementById("result-list-users").innerHTML = innermyspan + `<a class="dropdown-item search-result text-chocolate search-element-deletable" href = "#"> <img src="${repo.avatarUrl}" alt="Alternate Text"  width="20" height="20" />  ${repo.userName} </a>`;

                });
            }
            if (response.courses.length !== 0) {
                counter++;
                document.getElementById("result-list").innerHTML = document.getElementById("result-list").innerHTML + `<div id="result-list-courses" class="container text-blue search-element-deletable">Courses</div>`;
                response.courses.forEach(function (repo) {
                    var innermyspan = document.getElementById("result-list-courses").innerHTML;
                    document.getElementById("result-list-courses").innerHTML = innermyspan + `<a class="dropdown-item search-result text-chocolate search-element-deletable" href = "#"> ${repo.name} </a>`;

                });
            }
            if (response.topics.length !== 0) {
                counter++;
                document.getElementById("result-list").innerHTML = document.getElementById("result-list").innerHTML + `<div id="result-list-topics" class="container text-blue search-element-deletable">Topics</div>`;
                response.topics.forEach(function (repo) {
                    var innermyspan = document.getElementById("result-list-topics").innerHTML;
                    document.getElementById("result-list-topics").innerHTML = innermyspan + `<a class="dropdown-item search-result text-chocolate search-element-deletable" href = "#"> ${repo.name} </a>`;

                });
            }

            if (counter === 0) {
                var innermyspan = document.getElementById("result-list").innerHTML;
                document.getElementById("result-list").innerHTML = innermyspan + `<div class="dropdown-item search-result text-green search-element-deletable">Тhere are no results</div>`;
            }

        }
    });
}