var comment = {
    init: function () {
        comment.regEvents();
    },
    regEvents: function () {
        $('#btn_ProductCMT').off('click').on('click', function () {
            var sNoiDung = $('#sNoiDungCMT').val();
            var IDSanPham = $('#idSanPham').val();
            $.ajax({
                url: "/Product/AddComment",
                type: 'POST',
                dataType: 'json',
                data: {
                    sNoiDung: sNoiDung,
                    IDSanPham: IDSanPham,
                },
                success: function (res) {
                    if (res.status == true) {
                        window.alert('Thêm thành công');
                        resetForm.resetForm();
                    }
                }
            });
        });
        $('#btn_ContentCMT').off('click').on('click', function () {
            var sNoiDung = $('#sNoiDungCMTBaiViet').val();
            var IDBaiViet = $('#idBaiViet').val();
            $.ajax({
                url: "/BaiViet/AddComment",
                type: 'POST',
                dataType: 'json',
                data: {
                    sNoiDung: sNoiDung,
                    IDBaiViet: IDBaiViet,
                },
                success: function (res) {
                    if (res.status == true) {
                        window.alert('Thêm thành công');
                        resetForm.resetForm2();
                    }
                }
            });
        });

    },
    resetForm: function () {
        $('#sNoiDungCMT').val('');
    },
    resetForm2: function () {
        $('#sNoiDungCMTBaiViet').val('');
    }
}
comment.init();