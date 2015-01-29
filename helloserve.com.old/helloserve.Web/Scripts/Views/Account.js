var Account = {
    CheckUsername: function () {
        //setTimeout(function () {                     
        $('#UsernameCheck').removeClass("invalid");
        $('#UsernameCheck').removeClass("valid");

        var username = $('#UserName').val();
        if (username == "") {
            $('#UsernameCheck').text("");
            return false;
        }

        $.post('/Account/CheckUsername', { username: username }, function (result) {
            if (result.IsError) {
                $('#UsernameCheck').addClass("invalid");
                $('#UsernameCheck').text(result.Description);
            }
            else {
                $('#UsernameCheck').addClass("valid");
                $('#UsernameCheck').text(result.Description);
            }
        });

        //}, 10);
    },
    ForgotPassword: function () {
        $('#ForgotPassword').text(" Working...");
        var username = $('#UserName').val();
        $('#AccountLogonDiv').load('/Account/ForgotPassword', { username: username });
    }
}