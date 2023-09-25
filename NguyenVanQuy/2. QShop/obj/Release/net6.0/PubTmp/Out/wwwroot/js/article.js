import { showDate, showLoader, hideLoader } from './utility.js';
$('#pagination').pagination({
  dataSource: '/articles/list',
  locator: 'items',
  totalNumberLocator: function (response) {
    // you can return totalNumber by analyzing response content
    return response.count;
  },
  pageSize: 9,
  // ajax: {
  //   beforeSend: function () {
  //     $('#data').html('<div class="lds-ring"><div></div><div></div><div></div><div></div></div>');
  //   },
  // },
  callback: function (data, pagination) {
    showPage(pagination.pageNumber);
  },
});

function showPage(page) {
  showLoader();
  $('#data').html('');
  $.ajax({
    type: 'POST',
    contentType: 'application/json; charset=utf-8',
    url: '/articles/list/?page=' + page,
    data: 'data',
    success: function (response) {
      hideLoader();
      let result = '';
      let items = response;
      items.forEach((item) => {
        result += `<div class="posts-col col-12 col-sm-12 col-md-4 col-lg-4">
										<div class="posts-wrap">
											<div class="posts-img">
												<a href="/articles/details/?id=${item.id}">
													<img src="${item.thumbnail}">
												</a>
											</div>
											<span class="posts-date">${showDate(item.createdAt)}</span>
											<span class="posts-name"><a href="/articles/details/?id=${item.id}">${item.title}</a></span>
											<p>${item.description}</p>
											<a href="/articles/details/?id=${item.id}" class="posts-link"><i class="bi bi-chevron-double-right"></i> Đọc thêm</a>
										</div>
									</div>`;
      });
      $('#data').html(result);
    },
  });
}
