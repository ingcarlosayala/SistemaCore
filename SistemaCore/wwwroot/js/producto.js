

var datatable;

$(document).ready(function () {
    CargarTabla();
});

const CargarTabla = () => {
    datatable = $("#tblDato").DataTable({
        "ajax": {
            "url": "/Admin/Productos/ObtenerTodos"
        },
        "columns": [
            {"data": "codigo"},
            {"data": "descripcion"},
            {"data": "categoria.nombre"},
            {"data": "marca.nombre"},
            {
                "data": "precio", "className": "text-end",
                "render": function (data) {
                    var d = data.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                    return d;
                }
            },
            {
                "data": "id",
                "render": function (id) {
                    return `<div class="text-center">
                                <a href="/Admin/Productos/Editar/${id}" class="btn btn-secondary btn-sm"> <i class="fa-solid fa-pen"></i> </a>
                                <a onclick=Eliminar("/Admin/Productos/Eliminar/${id}") class="btn btn-danger btn-sm"> <i class="fa-solid fa-trash-can"></i> </a>
                            </div>`;
                }
            }
        ]
    });
}

const Eliminar = url => {

    Swal.fire({
        title: 'Estas Seguro?',
        text: "No se podra recuperar el registro!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (id) {
                    if (id) {
                        toastr.success(id.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(id.message);
                    }
                }
            });
        }
    })
}