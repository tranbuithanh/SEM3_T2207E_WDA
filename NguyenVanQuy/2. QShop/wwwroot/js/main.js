import { alertSimple, cartDisplay } from './utility.js';

$('.btn-popup-cancel').click(function (e) {
  $('.modal-popup').hide();
});

$('#recharge-by-phone-card').click(function (e) {
  $('#modal-phone-card').show();
});

$('#recharge-by-bank').click(function (e) {
  $('#modal-bank').show();
});

$('.modal-popup').click(function (e) {
  if (e.target.className === 'modal-popup') {
    $(this).hide();
  }
});

// In your Javascript (external .js resource or <script> tag)
$(document).ready(function () {
  $('.select-phone-card-type').select2({
    minimumResultsForSearch: Infinity,
    placeholder: 'Chọn loại thẻ',
  });
  $('.select-phone-card-denomination').select2({
    minimumResultsForSearch: Infinity,
    placeholder: 'Chọn mệnh giá',
  });
});

// Recharge Bank
$('#btn-recharge-bank').click(function (e) {
  $.ajax({
    type: 'POST',
    contentType: 'application/json; charset=utf-8',
    url: '/profile/rechargebank',
    success: function (response) {
      $('.modal-popup').hide();
      if (response == 'Ok') {
        alertSimple('Chuyển khoản thành công +500.000VNĐ', 'reload');
      } else {
        alertSimple('Chuyển khoản không thành công', 'reload');
      }
    },
  });
});

// Validate
$('#rechargeCardForm').validate({
  rules: {
    Type: 'required',
    Denomination: 'required',
    serial: {
      required: true,
      minlength: 10,
    },
    code: {
      required: true,
      minlength: 10,
    },
  },
  messages: {
    Type: '*Bạn chưa chọn loại thẻ',
    Denomination: '*Bạn chưa chọn mệnh giá.',
    serial: {
      required: '*Serial không được để trống.',
      minlength: '*Serial không đúng(10 ký tự).',
    },
    code: {
      required: '*Mã thẻ không được để trống.',
      minlength: '*Mã thẻ không đúng(10 ký tự).',
    },
  },
  submitHandler: function (form) {
    let data = {
      Type: $('#phone-card-type').find(':selected').val(),
      Denomination: $('#phone-card-denomination').find(':selected').val(),
      Serial: $('#serial').val(),
      Code: $('#code').val(),
    };
    $.ajax({
      type: 'POST',
      contentType: 'application/json; charset=utf-8',
      url: '/profile/rechargecard',
      data: JSON.stringify(data),
      success: function (response) {
        $('.modal-popup').hide();
        if (response == 'Ok') {
          alertSimple('Nạp thẻ thành công', 'reload');
        } else {
          alertSimple('Nạp thẻ không thành công', 'reload');
        }
      },
    });
  },
});
