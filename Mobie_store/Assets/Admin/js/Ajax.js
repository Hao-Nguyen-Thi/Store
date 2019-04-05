$(document).ready(function () {

    // Javascript method's body can be found in assets/js/demos.js
    demo.initDashboardPageCharts();

    demo.initVectorMap();
});
$(document).ready(function () {
    $('.datatables').DataTable({
        "pagingType": "full_numbers",
        "lengthMenu": [
            [10, 25, 50, -1],
            [10, 25, 50, "All"]
        ],
        responsive: true,
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Search records",
        }

    });
    //status record users
    $('.status_users').off('click').on('click', function (e) {
        e.preventDefault();
        var btn = $(this);
        var id = btn.data('id');
        var pos = btn.parent().parent().find('td:eq(8)');
        $.ajax({
            url: "/Admin/Users/changeStatus",
            data: { 'id': id },
            dataType: "json",
            type: "POST",
            success: function (e) {
                if (e.status == true) {
                    pos.html('<span class="label label-success">Active</span>');
                } else {
                    pos.html('<span class="label label-danger">Deactive</span>');
                }
            }
        });
    });


    //delete record users
    $('.delete_users').off('click').on('click', function (e) {
        e.preventDefault();
        var btn = $(this);
        var id = btn.data('id');
        $.ajax({
            url: "/Admin/Users/deleteUser",
            data: { 'id': id },
            dataType: "json",
            type: "POST",
            success: function (e) {
                $('.row_' + id).remove();
            }
        });
    });

    //delete record category
    $('.delete_cate').off('click').on('click', function (e) {
        e.preventDefault();
        var btn = $(this);
        var id = btn.data('id');
        $.ajax({
            url: "/Admin/Category/deleteCate",
            data: { 'id': id },
            dataType: "json",
            type: "POST",
            success: function (e) {
                $('.row_' + id).remove();
            }
        });
    });

    //status record product
    $('.status_product').off('click').on('click', function (e) {
        e.preventDefault();
        var btn = $(this);
        var id = btn.data('id');
        var pos = btn.parent().parent().find('td:eq(5)');
        $.ajax({
            url: "/Admin/Product/changeStatus",
            data: { 'id': id },
            dataType: "json",
            type: "POST",
            success: function (e) {
                if (e.status == true) {
                    pos.html('<span class="label label-success">Active</span>');
                } else {
                    pos.html('<span class="label label-danger">Deactive</span>');
                }
            }
        });
    });

    //delete record product
    $('.delete_product').off('click').on('click', function (e) {
        e.preventDefault();
        var btn = $(this);
        var id = btn.data('id');
        $.ajax({
            url: "/Admin/Product/deleteProduct",
            data: { 'id': id },
            dataType: "json",
            type: "POST",
            success: function (e) {
                $('.row_' + id).remove();
            }
        });
    });

    //delete record image product
    $('.delete_image_product').off('click').on('click', function (e) {
        e.preventDefault();
        var btn = $(this);
        var id = btn.data('id');
        $.ajax({
            url: "/Admin/Image/deleteImage",
            data: { 'id': id },
            dataType: "json",
            type: "POST",
            success: function (e) {
                $('.row_' + id).remove();
            }
        });
    });


    $('.card .material-datatables label').addClass('form-group');
});