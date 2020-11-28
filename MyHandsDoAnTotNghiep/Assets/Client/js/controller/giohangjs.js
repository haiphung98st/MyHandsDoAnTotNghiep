var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnDatHang').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });
        $('#btnCapNhat').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    SoLuong: $(item).val(),
                    SanPham: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/GioHangItem/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/GioHangItem/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
        $('input[name="option_payment"]').off('click').on('click', function () {
            if ($(this).val() == 'COD') {
                $('.boxContent').hide();
                $('#hdnFormOFPayment').val($(this).val()) ;
            }
            else if ($(this).val() == 'NL') {
                $('.boxContent').hide();
                $('#nganluongpayment').show();
                $('#hdnFormOFPayment').val($(this).val());

            }
            else if ($(this).val() == 'ATM_ONLINE') {
                $('.boxContent').hide();
                $('#bankpayment').show();
                $('#hdnFormOFPayment').val($(this).val());
                $('input[name="bankcode"]').off('click').on('click', function () {
                    $('#hdnBankCode').val($(this).prop('id'));
                });
            }
            else {
                $('.boxContent').hide();
                $('#hdnFormOFPayment').val($(this).val()) ;


            }
        });
    }
}
cart.init();