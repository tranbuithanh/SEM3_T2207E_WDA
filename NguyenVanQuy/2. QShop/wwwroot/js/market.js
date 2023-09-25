import { alertSimple, cartDisplay, showLoader, hideLoader } from './utility.js';

var totalItems = 0;
var pageSize = 6;
var dataSource = [];
// var game = '@ViewData["slug"]';
var data = { game: game };
// First load
filterAndLoadData(data);

$('#btn-filter').click(function (e) {
  dataSource = [];
  var orderValue = $("input[name='order']:checked").val();
  var championValue = $("input[name='champion']:checked").val();
  var levelValue = $("input[name='level']:checked").val();
  var skinValue = $("input[name='skin']:checked").val();
  var rpValue = $("input[name='rp']:checked").val();
  var essenceValue = $("input[name='essence']:checked").val();
  var priceValue = $("input[name='price']:checked").val();
  var rankValue = getCheckboxValue('rank');
  //console.log(orderValue, championValue, levelValue, skinValue, rpValue, essenceValue, priceValue, rankValue);
  data = {
    Game: game,
    Order: orderValue,
    Champion: championValue,
    Level: levelValue,
    Skin: skinValue,
    Rp: rpValue,
    Essence: essenceValue,
    Price: priceValue,
    Rank: rankValue,
  };
  filterAndLoadData(data);
});

$('#btn-filter-reset').click(function (e) {
  location.reload();
});

function showPage(page) {
  totalItems = $('.card-item').length;
  let start = pageSize * (page - 1);
  let end = start + pageSize;
  $('.card-item').each(function (index, element) {
    var $element = $(element);
    if (index >= start && index < end) {
      $element.show();
    } else {
      $element.hide();
    }
  });
}

function getCheckboxValue(name) {
  var selected = new Array();
  $(`input[type=checkbox][name=${name}]:checked`).each(function () {
    selected.push(this.value);
  });
  return selected;
}

function filterAndLoadData(data) {
  showLoader();
  dataSource = [];
  $.ajax({
    type: 'POST',
    url: '/market/filter',
    data: JSON.stringify(data),
    contentType: 'application/json; charset=utf-8',
    success: function (response) {
      hideLoader();
      $('#accounts-result').html(response);
      totalItems = $('.card-item').length;
      for (let i = 0; i < totalItems; i++) {
        dataSource.push(i);
      }
      $('#pagination').pagination({
        dataSource: dataSource,
        pageSize: pageSize,
        callback: function (data, pagination) {},
        afterPageOnClick: function (element, currentPage) {
          showPage(currentPage);
        },

        afterPreviousOnClick: function (element, currentPage) {
          showPage(currentPage++);
        },

        afterNextOnClick: function (element, currentPage) {
          showPage(currentPage--);
        },
      });
      showPage(1);
    },
  });
}

// Giỏ hàng
$('#accounts-result').on('click', '.btn-cart-create', function () {
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
          title: 'Thông báo!',
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

// Cart count
$('form.accordion-collapse').change(function (e) {
  console.log('change');
  e.preventDefault();
  $(this).prev().find('.group-count').show();
});
