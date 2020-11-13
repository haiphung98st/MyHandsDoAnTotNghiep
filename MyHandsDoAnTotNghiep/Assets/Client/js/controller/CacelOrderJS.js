var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btnActive').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            if (confirm('Bạn muốn hủy đơn hàng này ?')) {
                $.ajax({
                    url: "/Personal/CancelOrder",
                    data: { id: id },
                    dataType: "json",
                    type: "POST",
                    success: function (reponse) {
                        if (reponse.iMaTrangThai == 1) {
                            btn.text('Hủy đơn');
                        } else {
                            btn.text('');
                        }
                    }
                });
            }
        });
    }
}
user.init();