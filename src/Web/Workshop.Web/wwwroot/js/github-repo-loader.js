$.ajax({
    url: "https://api.github.com/users/danieljekov/repos",
    type: 'get',
    datatype: 'json',
    success: function (response) {
        response.forEach(function (repo) {

            if (repo.description === null) {
                repo.description = 'Description missing!';
            };

            if (repo.description.length > 44) {
                repo.description = repo.description.slice(0, 40).concat('..')
            }

            repo.pushed_at = moment(new Date(repo.pushed_at), "YYYY-MM-DDTOHH:mm").fromNow();

            result = `<a target ="_blank" href="${repo.html_url}" class="text-decoration-none col-4 pb-4">
                                     <div class="card border-blue bg-dark">
                                     <div class="card-body">
                                     <h5 class="card-title text-chocolate">${repo.name}</h5>
                                     <p class="card-text text-green">${repo.description}</p>
                                     <p class="card-text text-green"><small class="text-muted">${repo.pushed_at}</small></p>
                                     </div>
                                     </div>
                                     </a>`;
            $("#repo-list").append(result);
        });
    }
});
