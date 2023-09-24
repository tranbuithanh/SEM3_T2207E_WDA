import { alertSimple, cartDisplay } from './utility.js';

let code = '';
$('#btn-checkout').click(function () {
  $('#modal-checkout-first').show();
});

$('.btn-checkout-cancel').click(function () {
  $('.modal-checkout').hide();
});

$('.modal-checkout').click(function (event) {
  if (event.target.className === 'modal-checkout') {
    $(this).hide();
  }
});

$('tbody').on('click', '.btn-delete-cart ', function () {
  let that = $(this);
  let urlRequest = $(this).data('url');
  Swal.fire({
    title: 'Xoá tài khoản khỏi giỏ hàng?',
    text: 'Bạn có chắc muốn xoá tài khoản này khỏi giỏ hàng không!',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Xoá tài khoản',
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        type: 'GET',
        url: urlRequest,
        success: function (response) {
          if (response == 'success') {
            that.closest('tr').hide();
            cartDisplay();
            Swal.fire('Đã xoá!', 'Tài khoản đã được xoá khỏi giỏ hàng.', 'success').then((result) => {
              location.reload();
            });
          }
        },
      });
    }
  });
});

$('#btn-apply-code').click(function (e) {
  code = $('#coupon').val();
  $.ajax({
    type: 'GET',
    contentType: 'application/json; charset=utf-8',
    url: '/coupons/apply/?code=' + code,
    success: function (response) {
      console.log('/coupons/apply/?code=' + code);
      let resultHmtl;
      if (response.status == 200) {
        resultHmtl =
          `<i class="bi bi-check-lg" style="color: var(--green-color);"></i>Đã áp dụng mã giảm giá: ` +
          response.message;
        $('#coupon-discount').text(response.percent + '%');
        totalAmount = Math.round((amount * 1.0 * (100 - response.percent)) / 100);
        $('.total-amount').text(totalAmount.toLocaleString() + ' VNĐ');
        $('#remaining-money').text((yourMoney - totalAmount).toLocaleString() + ' VNĐ');
      } else if (response.status == 201) {
        resultHmtl = `<i class="bi bi-x-lg" style="color: var(--red-color);"></i> ` + response.message;
      } else if (response.status == 202) {
        resultHmtl = `<i class="bi bi-x-lg" style="color: var(--red-color);"></i> ` + response.message;
      } else {
        resultHmtl = `<i class="bi bi-x-lg" style="color: var(--red-color);"></i> ` + response.message;
      }
      $('#code-error').html(resultHmtl);
    },
  });
});

$('#btn-checkout-first').click(function (e) {
  if (yourMoney - totalAmount < 0) {
    $('#remaining-money').html('<span style="color:var(--red-color)">*Tài khoản của bạn không đủ!</span>');
    Swal.fire({
      title: 'Tài khoản không đủ? Nạp thêm',
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: 'Nạp bằng thẻ cào',
      denyButtonText: `Nạp qua bank`,
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        $('#modal-phone-card').show();
      } else if (result.isDenied) {
        $('#modal-bank').show();
      }
    });
  } else {
    $('.modal-checkout').hide();
    console.log('/profile/checkout/?code=' + code);
    $.ajax({
      type: 'get',
      url: '/profile/checkout/?code=' + code,
      success: function (response) {
        if (response == 'Ok') {
          alertSimple('Thanh toán thành công', 'reload');
        }
      },
    });
  }
});
