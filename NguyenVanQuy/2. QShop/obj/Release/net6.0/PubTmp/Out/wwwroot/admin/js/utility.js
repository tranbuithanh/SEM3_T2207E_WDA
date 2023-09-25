export { alertSimple, cartDisplay, showImagePreview, showDate, showDateTime };

function alertSimple(title) {
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
    },
  }).then((result) => {
    /* Read more about handling dismissals below */
    if (result.dismiss === Swal.DismissReason.timer) {
      console.log('I was closed by the timer');
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

function showDateTime(datetime) {
  var date = new Date(datetime);
  var day = date.getDate();
  var month = date.getMonth() + 1;
  var year = date.getFullYear();
  var hours = date.getHours();
  var minutes = date.getMinutes();
  var amOrPm = hours >= 12 ? 'PM' : 'AM';
  hours = hours % 12 || 12; // Chuyển đổi giờ sang định dạng 12 giờ
  if (month < 10) {
    month = '0' + month;
  }
  if (day < 10) {
    day = '0' + day;
  }
  if (hours < 10) {
    hours = '0' + hours;
  }
  if (minutes < 10) {
    minutes = '0' + minutes;
  }
  var formattedDateTime = month + '/' + day + '/' + year + ' ' + hours + ':' + minutes + ':' + '00 ' + amOrPm;
  return formattedDateTime;
}
