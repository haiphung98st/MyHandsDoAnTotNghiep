var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btnActive').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/NhaCungCap/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (reponse) {
                    if (reponse.bStatus == true) {
                        btn.text('Active');
                    } else {
                        btn.text('Inactive');
                    }
                }
            });
        });
    }
}
user.init();