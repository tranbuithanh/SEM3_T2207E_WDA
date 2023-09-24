import { showDateTime } from './utility.js';
$('tbody').on('click', '.btn-delete-item', function (e) {
  e.preventDefault();
  let that = $(this);
  let url = $(this).data('url');
  console.log(url);
  Swal.fire({
    title: 'Xác nhận xoá?',
    text: 'Việc này không thể hoàn tác!',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Xoá!',
    cancelButtonText: 'Huỷ',
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: url,
        success: function (response) {
          if (response == 200) {
            that.closest('tr').hide();
          }
        },
      });
      Swal.fire('Đã xoá!', 'Xoá thành công.', 'success');
    }
  });
});
$('#pagination').pagination({
  dataSource: '/admin/invoices/list',
  locator: 'items',
  totalNumberLocator: function (response) {
    // you can return totalNumber by analyzing response content
    return response.count;
  },
  pageSize: 10,
  ajax: {
    beforeSend: function () {
      $('#data').html('Loading data from QShop ...');
    },
  },
  callback: function (data, pagination) {
    showPage(pagination.pageNumber);
  },
});

function showPage(page) {
  $('#data').html('');
  $.ajax({
    type: 'POST',
    contentType: 'application/json; charset=utf-8',
    url: '/admin/invoices/list/?page=' + page,
    data: 'data',
    success: function (response) {
      let result = '';
      let items = response;
      items.forEach((item) => {
        result += `<tr>
												<td>${item.id}</td>
												<td>${item.user.email}</td>
												<td class="text-end">${item.amount.toLocaleString()}</td>
												<td>${item.coupon.code} - ${item.coupon.description}</td>
												<td class="text-end">${item.totalAmount.toLocaleString()}</td>
												<td>${showDateTime(item.invoiceDate)}</td>
												<td>
													<div class="d-flex">
														<a class="btn-edit-item text-center" href="/admin/invoices/details/?id=${item.id}">
															<i class="bi bi-card-checklist"></i>
														</a>
													</div>
												</td>
											</tr>`;
      });
      $('#data').html(result);
    },
  });
}
