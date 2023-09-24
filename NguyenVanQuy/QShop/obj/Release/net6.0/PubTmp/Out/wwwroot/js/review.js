import { alertSimple, cartDisplay } from './utility.js';
var totalItems = 0;
var pageSize = 6;
var dataSource = [];
var sortByString = 'Sắp xếp theo';
var sortBy = 'newest';
// First load
filterAndLoadData(sortBy);

//Sort
$('.review-filter').click(function (e) {
  sortBy = $(this).data('filter');
  if (sortBy == 'newest') {
    $('#sortBy-string').text('Mới nhất');
  } else {
    $('#sortBy-string').text('Cũ nhất');
  }
  filterAndLoadData(sortBy);
});

function showPage(page) {
  totalItems = $('.testimonials-wrap').length;
  let start = pageSize * (page - 1);
  let end = start + pageSize;
  $('.testimonials-wrap').each(function (index, element) {
    var $element = $(element);
    if (index >= start && index < end) {
      $element.show();
    } else {
      $element.hide();
    }
  });
}

function filterAndLoadData(sortBy) {
  dataSource = [];
  $.ajax({
    type: 'GET',
    url: '/reviews/filter/?sortBy=' + sortBy,
    data: sortBy,
    contentType: 'application/json; charset=utf-8',
    success: function (response) {
      console.log(response);
      $('.reviews-item').html(response);
      totalItems = $('.testimonials-wrap').length;
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
