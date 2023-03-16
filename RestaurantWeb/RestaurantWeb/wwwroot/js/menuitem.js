/*this function cause displaying of data table,which is copied from datatable.net website*/

$(document).ready(function () {
    $('#DT_load').DataTable({
        "ajax": {  //call API to load data table which is ajax type

            "url": "/api/MenuItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "foodType.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                           <a href="/Admin/MenuItems/upsert?id=${data}" class="btn btn-success text-white mx-2"
                           style="cursor:pointer;width:100px;"><i class="bi bi-pencil-square"></i>Edit</a>
                           <a href="/Admin/MenuItems/upsert?id=${data}" class="btn btn-danger text-white"
                           style="cursor:pointer;width:100px;"><i class="bi bi-trash3-fill"></i></a>
                    </div>`
                },
                "width": "15%"
            }

        ],
        "width":"100%"

    });
});