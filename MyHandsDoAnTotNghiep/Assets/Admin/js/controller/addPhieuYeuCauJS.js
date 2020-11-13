var phieuyeucau = {
    init: function () {
        phieuyeucau.regEvents();
    },
    regEvents: function () {
        $('#btnThemPhieuYeuCau').off('click').on('click', function () {
            window.location.href = "/them-phieu";
        });
        $('#btnCapNhat').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var productYClist = [];
            $.each(listProduct, function (i, item) {
                productYClist.push({
                    iSoLuongYC: $(item).val(),
                    sanPham: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/PhieuYeuCauNhap/Update',
                data: { productModel: JSON.stringify(productYClist) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Admin/PhieuYeuCauNhap/Create";
                    }
                }
            })
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/PhieuYeuCauNhap/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Admin/PhieuYeuCauNhap/Create";
                    }
                }
            })
        });
        $('#ddlncc').off('change').on('change', function () {

            var id = $(this).val();
            $("#hdnNCC").text($(this).find("option:selected").val());
            $("#hdnNCC").val($(this).find("option:selected").val());

            //$("#ddlncc").val($(this).find("option:selected").text());
            //var test = $("#ddlncc option:selected").text();
            //var nccName = $("#ddlncc option:selected").text();
           // var nccValue = $("#ddlncc option:selected").val();

            //alert(nccValue);
        });
        $('#ddlSanPham').off('change').on('change', function () {

            var id = $(this).val();
            //$("#ddlncc").val($(this).find("option:selected").text());
            //var test = $("#ddlncc option:selected").text();
            var sanPhamName = $("#ddlSanPham option:selected").text();
            var sanPhamValue = $("#ddlSanPham option:selected").val();
            window.location.href = "/them-phieu-yc?IDSanPham=" + sanPhamValue+"&soluong=1";
        });
    }
}
phieuyeucau.init();