$('#myModal').on('hidden.bs.modal', function (e) {
    $('#usersRoleModal').html('');
    $('#modalRoleId').val('');
});

function LoadUsersInGivenRole(roleId) {
    $("#modalRoleId").val(roleId);
    $.ajax({
        url: `/api/role-users/${roleId}`,
        type: 'get',
        datatype: 'json',
        success: function (response) {
            response.forEach(function (user) {
                var result = `<tr id="${user.id}">
                              <td class="font-weight-bold text-green">${user.userName}</td>
                              <td>
                              <button class="btn btn-danger" onclick="RemoveUserFromGivenRole('${user.id}')">Remove</button>
                              </td>
                              </tr>`;

                $("#usersRoleModal").append(result);
            });
        }
    });
}

function RemoveUserFromGivenRole(userId) {
    var roleId = $("#modalRoleId").val();
    $.ajax({
        url: `/api/remove-user-from-given-role?roleId=${roleId}&userId=${userId}`,
        type: 'get',
        datatype: 'json',
        success: function (response) {
            $("#" + userId).remove();
        }
    });
}

function AddUserToRole(userId) {
    var roleId = $("#modalRoleId").val();
    $.ajax({
        url: `/api/add-user-to-role?roleId=${roleId}&userId=${userId}`,
        type: 'get',
        datatype: 'json',
        success: function (response) {
            $("#" + userId).remove();
        }
    });
}

function LoadUsersOutOfGivenRole(roleId) {
    $("#modalRoleId").val(roleId);
    $.ajax({
        url: `/api/users-who-are-not-in-given-role/${roleId}`,
        type: 'get',
        datatype: 'json',
        success: function (response) {
            response.forEach(function (user) {
                var result = `<tr id="${user.id}">
                              <td class="font-weight-bold text-green">${user.userName}</td>
                              <td>
                              <button class="btn btn-success" onclick="AddUserToRole('${user.id}')">Add to the role</button>
                              </td>
                              </tr>`;

                $("#usersRoleModal").append(result);
            });
        }
    });
}
