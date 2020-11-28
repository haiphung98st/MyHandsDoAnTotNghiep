var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('#btn_change').off('click').on('click', function (e) {
            e.preventDefault();
            var oldpassword = $('#old-error-message');
            var newpassword = $('#new-error-message');
            var renewpassword = $('#renew-error-message');
            oldpassword.hide();
            newpassword.hide();
            renewpassword.hide();
            if (sOldPassword.val() == "") {
                oldpassword.html("Hãy nhập mật khẩu cũ");
                oldpassword.show();
            }
                $.ajax({
                    url: "/Personal/ChangePassword",
                    data: { id: id },
                    dataType: "json",
                    type: "POST",
                    success: function (reponse) {
                        if (reponse == true) {
                            window.location.href = "/gio-hang";
                        }
                    }
                });
            
        });
    }
}
user.init();