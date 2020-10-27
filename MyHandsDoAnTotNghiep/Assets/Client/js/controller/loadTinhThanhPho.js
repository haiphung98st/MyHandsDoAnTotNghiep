var user = {
    init: function () {

        user.loadProvince();
        user.registerEvent();
    },
    registerEvent: function () {
        $('#ddlTinhThanh').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                user.loadDistrict(parseInt(id));
            }
            else {
                $('#ddlQuanHuyen').html('');
            }
        });
    },
    loadProvince: function () {

        $.ajax({
            url: '/User/LoadTinhThanh',
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn tỉnh thành--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlTinhThanh').html(html);
                }
            }
        })
    },
    loadDistrict: function (id) {
        $.ajax({
            url: '/User/LoadQuanHuyen',
            type: "POST",
            data: { tinhThanhID: id },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn quận huyện--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlQuanHuyen').html(html);
                }
            }
        })
    }
}
user.init();
