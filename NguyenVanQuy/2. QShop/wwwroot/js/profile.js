import { showImagePreview, alertSimple } from './utility.js';

showImagePreview('#thumbnail', '#previewImage');

$('#btn-change-password').click(function (e) {
  $('#modalChangePassword').show();
});

$('.modal-change-password').click(function (event) {
  if (event.target.className === 'modal-change-password') {
    $(this).hide();
  }
});

$('#btn-change-password-cancel').click(function (e) {
  $('#modalChangePassword').hide();
});

// validate signup form on keyup and submit
$('#changePasswordForm').validate({
  rules: {
    passwordOld: {
      required: true,
      minlength: 6,
    },
    passwordNew: {
      required: true,
      minlength: 6,
    },
    passwordRepeat: {
      required: true,
      minlength: 6,
      equalTo: '#passwordNew',
    },
  },
  messages: {
    passwordOld: {
      required: '*Mật khẩu không được để trống!',
      minlength: '*Mật khẩu phải có ít nhất 6 ký tự.',
    },
    passwordNew: {
      required: '*Mật khẩu không được để trống!',
      minlength: '*Mật khẩu phải có ít nhất 6 ký tự.',
    },
    passwordRepeat: {
      required: '*Mật khẩu không được để trống!',
      minlength: '*Mật khẩu phải có ít nhất 6 ký tự.',
      equalTo: '*Mật khẩu không giống nhau.',
    },
  },
  submitHandler: function (form) {
    let data = {
      PasswordOld: $('#passwordOld').val(),
      PasswordNew: $('#passwordNew').val(),
      PasswordRepeat: $('#passwordRepeat').val(),
    };
    $.ajax({
      type: 'POST',
      contentType: 'application/json; charset=utf-8',
      url: '/profile/changepassword',
      data: JSON.stringify(data),
      success: function (response) {
        if (response.status == 200) {
          alertSimple('Đổi mật khẩu thành công.', 'reload');
          $('#modalChangePassword').hide();
        } else {
          alertSimple('Mật khẩu không đúng');
          $('#modalChangePassword').hide();
        }
      },
    });
  },
});
