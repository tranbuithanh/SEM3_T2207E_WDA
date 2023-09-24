export { alertSimple, cartDisplay, showImagePreview, showDate, showLoader, hideLoader };

function alertSimple(title, reload = 'no') {
  let timerInterval;
  Swal.fire({
    title: title,
    // html: 'I will close in <b></b> milliseconds.',
    timer: 1000,
    timerProgressBar: true,
    didOpen: () => {
      Swal.showLoading();
      // const b = Swal.getHtmlContainer().querySelector('b');
      // timerInterval = setInterval(() => {
      //   b.textContent = Swal.getTimerLeft();
      // }, 100);
    },
    willClose: () => {
      clearInterval(timerInterval);
      if (reload == 'reload') {
        location.reload();
      }
    },
  }).then((result) => {
    /* Read more about handling dismissals below */
    if (result.dismiss === Swal.DismissReason.timer) {
    }
  });
}

function cartDisplay() {
  $('#cart-count').show();
  $.ajax({
    type: 'GET',
    url: '/carts/count',
    success: function (response) {
      if (response == 0) {
        $('#cart-count').text('');
      } else {
        if (response > 9) {
          $('#cart-count').text('9+');
        } else {
          $('#cart-count').text(response);
        }
      }
    },
  });
}

function showImagePreview(imageUpload, previewImage) {
  $(imageUpload).change(function () {
    const file = this.files[0];
    if (file) {
      let reader = new FileReader();
      reader.onload = function (event) {
        console.log(event.target.result);
        $(previewImage).attr('src', event.target.result);
      };
      reader.readAsDataURL(file);
    }
  });
}

function showDate(datetime) {
  var date = new Date(datetime);
  var day = date.getDate();
  var month = date.getMonth() + 1;
  var year = date.getFullYear();
  var formattedDate = (day < 10 ? '0' : '') + day + '/' + (month < 10 ? '0' : '') + month + '/' + year;
  return formattedDate;
}

function showLoader() {
  $('#loader').show();
}

function hideLoader() {
  $('#loader').hide();
}
