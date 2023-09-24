import { alertSimple, cartDisplay } from './utility.js';

$('.btn-cart-create').click(function () {
  let urlRequest = $(this).data('url');
  $.ajax({
    type: 'GET',
    url: urlRequest,
    success: function (response) {
      if (response == 'ok') {
        alertSimple('Thêm vào giỏ hàng thành công.');
        cartDisplay();
      } else if (response == 'exists') {
        alertSimple('Tài khoản đã tồn tại trong giỏ hàng!');
      } else {
        Swal.fire({
          title: 'Cảnh báo!',
          text: 'Bạn cần đăng nhập trước khi thêm vào giỏ hàng!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Đăng nhập',
          cancelButtonText: 'Huỷ bỏ',
        }).then((result) => {
          if (result.isConfirmed) {
            window.location.href = '/home/login';
          }
        });
      }
    },
  });
});
