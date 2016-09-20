$(document).ready(function() {

	function reloadTable() {
		var order = $('.js-books-table').data('order');
		var title = $('.js-books-table').data('title');
		$.ajax({
			type: "POST",
			url: '/home/books',
			data: {
				'order': order,
				'title': title
			},
			success: function(data) {
				$('.js-books-table').html(data);

			},
			dataType: 'html'
		});
	}

	$.reloadTable = reloadTable;

	$('.book-table_container').on('click', '.js-sortable', function() {
		var booksTable = $('.js-books-table');
		var currentOrder = booksTable.data('order');
		var currentTitle = booksTable.data('title');
		var title = $(this).data('title');

		var order = title === currentTitle ? (currentOrder === "asc" ? "desc" : "asc") : "asc";
		booksTable.data('order', order);
		booksTable.data('title', title);

		var params = jQuery.param({ order: order, title: title });
		var currentUrl = window.location.protocol + '//' + window.location.host + window.location.pathname;
		window.history.pushState(params, null, currentUrl + '?' + params);

		reloadTable();
	});

	$('.book-table_container').on('click', '.js-remove', function () {
		var id = $(this).data('id');
		var title = $(this).data('title');
		if (confirm('Are you sure you want to delete the book ' + title)) {
			$.ajax({
				type: "POST",
				url: 'home/delete',
				data: {
					'id': id
				},
				success: function () {
					reloadTable(null, null);
				}
			});
		}

	});

	$(".fancybox").fancybox();
});