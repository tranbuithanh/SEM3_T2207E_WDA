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
  dataSource: '/admin/accounts/list',
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
    url: '/admin/accounts/list/?page=' + page,
    data: 'data',
    success: function (response) {
      let result = '';
      let items = response;
      items.forEach((item) => {
        result += `<tr>
															<td>${item.id}</td>
															<td style="height : 60px;padding: 0; vertical-align: middle;">
																<img class="h-50" src="${item.game.logo}" alt=""> 
																<span>${item.game.name}</span>
															</td>
															<td>${item.email}</td>
															<td style="height: 60px; padding: 0"><img class="h-100" src="${item.rank.thumbnail}" alt=""></td>
															<td>${item.grade}</td>
															<td>${item.champion}</td>
															<td>${item.primaryPoint}</td>
															<td>${item.secondaryPoint}</td>
															<td>${item.price}</td>
															<td>${item.description}</td>
															<td>
															<div class="d-flex">
															<a class="btn-edit-item text-center" href="/admin/accounts/edit/?id=${item.id}">
															<i class="bi bi-pencil-square"></i>
															</a>
															<a class="btn-delete-item text-center" data-url="/admin/accounts/delete/?id=${item.id}">
															<i class="bi bi-x-lg"></i>
															</a>
															</div>
															</td>
													</tr>`;
      });
      $('#data').html(result);
    },
  });
}
