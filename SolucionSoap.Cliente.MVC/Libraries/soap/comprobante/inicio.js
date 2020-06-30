var listaComprobantesProductos = new Array();
var listaComprobantes = new Array();

$(document).ready(function () {
    listarComprobantes();

    $('#crearComprobante').click(function () {
        crearComprobante()
    });

    $('#guardarComprobante').click(function () {
        guardarComprobante()
    });

    $('#crearComprobanteProducto').click(function () {
        crearComprobanteProducto()
    });

    $('#guardarComprobanteProducto').click(function () {
        guardarComprobanteProducto()
    });

    $('#descuentoComprobante').bind('keyup mouseup', function () {
        calcularTotalComprobante();
    });

    $('#cantidadComprobanteProducto').bind('keyup mouseup', function () {
        calcularTotalComprobanteProducto();
    });

    $('#precioComprobanteProducto').bind('keyup mouseup', function () {
        calcularTotalComprobanteProducto();
    });
});

//region Comprobante
function crearComprobante() {
    limpiarFormularioComprobante();
    crearTablaComprobanteProducto();
    $('#modalComprobanteTitulo').text('Crear Comprobante');
    $('#modalComprobante').modal('show');
}

function editarComprobante(identificadorComprobante) {
    limpiarFormularioComprobante();
    obtenerComprobante(identificadorComprobante);
    $('#modalComprobanteTitulo').text('Editar Comprobante');
    $('#modalComprobante').modal('show');
}

function guardarComprobante() {
    var comprobante = {
        'identificadorComprobante': '0',
        'tipoComprobante': '',
        'vendedorComprobante': '',
        'clienteComprobante': '',
        'fechaComprobante': '',
        'descuentoComprobante': '',
        'impuestoComprobante': '',
        'subTotalComprobante': '',
        'totalComprobante': ''
    };

    comprobante.identificadorComprobante = $('#identificadorComprobante').val();
    comprobante.tipoComprobante = $('#tipoComprobante').val();
    comprobante.vendedorComprobante = $('#vendedorComprobante').val();
    comprobante.clienteComprobante = $('#clienteComprobante').val();
    comprobante.fechaComprobante = new Date();
    comprobante.descuentoComprobante = $('#descuentoComprobante').val();
    comprobante.impuestoComprobante = $('#impuestoComprobante').val();
    comprobante.subTotalComprobante = $('#subTotalComprobante').val();
    comprobante.totalComprobante = $('#totalComprobante').val();

    if (comprobante.identificadorComprobante == 0) {
        $.ajax({
            type: 'POST',
            url: '/Comprobante/Registrar',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(comprobante),
            success: function (response) {
                success(response);
            },
            error: function (response) {
                console.log(response);
            }
        });
    } else {
        $.ajax({
            type: 'POST',
            url: '/Comprobante/Actualizar',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(comprobante),
            success: function (response) {
                success(response);
            },
            error: function (response) {
                console.log(response);
            }
        });
    }

    function success(response) {
        listaComprobantesProductos.forEach(function (elemento) {
            var comprobanteProducto = {
                'identificadorComprobanteProducto': '0',
                'identificadorComprobante': '0',
                'nombreComprobanteProducto': '',
                'cantidadComprobanteProducto': '0',
                'precioComprobanteProducto': '0.00',
                'totalComprobanteProducto': '0.00'
            };

            comprobanteProducto.identificadorComprobanteProducto = '' + elemento.identificadorComprobanteProducto;
            comprobanteProducto.identificadorComprobante = response.IdentificadorRespuesta ? response.IdentificadorRespuesta : elemento.identificadorComprobante;
            comprobanteProducto.nombreComprobanteProducto = elemento.nombreComprobanteProducto;
            comprobanteProducto.cantidadComprobanteProducto = elemento.cantidadComprobanteProducto;
            comprobanteProducto.precioComprobanteProducto = elemento.precioComprobanteProducto;
            comprobanteProducto.totalComprobanteProducto = elemento.totalComprobanteProducto;

            if (elemento.eliminarComprobanteProducto == false) {
                if (~comprobanteProducto.identificadorComprobanteProducto.indexOf('temporal')) {
                    $.ajax({
                        type: 'POST',
                        url: '/ComprobanteProducto/Registrar',
                        cache: false,
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: JSON.stringify(comprobanteProducto),
                        success: function (response) {
                            //console.log(response);
                        },
                        error: function (response) {
                            console.log(response);
                        }
                    });
                } else {
                    $.ajax({
                        type: 'POST',
                        url: '/ComprobanteProducto/Actualizar',
                        cache: false,
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: JSON.stringify(comprobanteProducto),
                        success: function (response) {
                            //console.log(response);
                        },
                        error: function (response) {
                            console.log(response);
                        }
                    });
                }
            } else {
                $.ajax({
                    type: 'POST',
                    url: '/ComprobanteProducto/Eliminar',
                    cache: false,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify(comprobanteProducto),
                    success: function (response) {
                        //console.log(response);
                    },
                    error: function (response) {
                        console.log(response);
                    }
                });
            }

            $('#modalComprobante').modal('hide');
            listarComprobantes();
        });
    }
}

function eliminarComprobante(IdentificadorComprobante) {
    var comprobante = {
        'IdentificadorComprobante': IdentificadorComprobante
    };

    $.ajax({
        type: 'POST',
        url: '/Comprobante/Eliminar',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(comprobante),
        success: function (response) {
            listarComprobantes();
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function obtenerComprobante(identificadorComprobante) {
    $.ajax({
        type: 'GET',
        url: '/Comprobante/Obtener?IdentificadorComprobante=' + identificadorComprobante,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: null,
        success: function (response) {
            $('#identificadorComprobante').val(response.ModeloRespuesta.IdentificadorComprobante);
            $('#numeroComprobante').val(agregarCeros(response.ModeloRespuesta.IdentificadorComprobante, 6));
            $('#tipoComprobante').val(response.ModeloRespuesta.TipoComprobante);
            $('#vendedorComprobante').val(response.ModeloRespuesta.VendedorComprobante);
            $('#clienteComprobante').val(response.ModeloRespuesta.ClienteComprobante);
            $('#descuentoComprobante').val(response.ModeloRespuesta.DescuentoComprobante.toFixed(2));
            $('#impuestoComprobante').val(response.ModeloRespuesta.ImpuestoComprobante.toFixed(2));
            $('#subTotalComprobante').val(response.ModeloRespuesta.SubTotalComprobante.toFixed(2));
            $('#totalComprobante').val(response.ModeloRespuesta.TotalComprobante.toFixed(2));

            listarComprobantesProductosPorIdentificadorComprobante($('#identificadorComprobante').val());
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function listarComprobantes() {
    $.ajax({
        type: 'GET',
        url: '/Comprobante/Listar',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: null,
        success: function (response) {
            listaComprobantes = new Array();

            response.ListaRespuesta.forEach(function (elemento) {
                var comprobante = {
                    'identificadorComprobante': '0',
                    'tipoComprobante': '',
                    'vendedorComprobante': '',
                    'clienteComprobante': '',
                    'fechaComprobante': '',
                    'descuentoComprobante': '',
                    'impuestoComprobante': '',
                    'subTotalComprobante': '',
                    'totalComprobante': ''
                };

                comprobante.identificadorComprobante = elemento.IdentificadorComprobante;
                comprobante.tipoComprobante = elemento.TipoComprobante;
                comprobante.vendedorComprobante = elemento.VendedorComprobante;
                comprobante.clienteComprobante = elemento.ClienteComprobante;
                comprobante.fechaComprobante = moment(new Date(parseInt(elemento.FechaComprobante.replace("/Date(", "").replace(")/", ""), 10))).format("YYYY-MM-DD HH:mm:ss");
                comprobante.descuentoComprobante = elemento.DescuentoComprobante;
                comprobante.impuestoComprobante = elemento.ImpuestoComprobante;
                comprobante.subTotalComprobante = elemento.SubTotalComprobante;
                comprobante.totalComprobante = elemento.TotalComprobante;

                listaComprobantes.push(comprobante);
            });           

            crearTablaComprobante();
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function crearTablaComprobante() {
    $("#tablaComprobante tbody").empty();
    
    var tbody = listaComprobantes.length > 0 ? '' : '<tr class="text-center"><td colspan="7">No hay Comprobantes.</td></tr>';
    
    listaComprobantes.forEach(function (elemento) {
        if (elemento.eliminarComprobante == false || elemento.eliminarComprobante == undefined) {
            tbody += '<tr>';
            tbody += '<td><div class="float-left"><button type="button" class="btn btn-outline-primary btn-sm" onclick="editarComprobante(' + "'" + elemento.identificadorComprobante + "'" + ');"><span class="fa fa-pencil-alt"></span></button></div>';
            tbody += '<div class="float-left"><button type="button" class="btn btn-outline-danger btn-sm" onclick="eliminarComprobante(' + "'" + elemento.identificadorComprobante + "'" + ');"><span class="fa fa-trash"></span></button></div></td>';
            tbody += '<td>' + agregarCeros(elemento.identificadorComprobante, 6) + '</td>';
            tbody += '<td>' + elemento.tipoComprobante + '</td>';
            tbody += '<td>' + elemento.vendedorComprobante + '</td>';
            tbody += '<td>' + elemento.clienteComprobante + '</td>';
            tbody += '<td>' + elemento.fechaComprobante + '</td>';
            tbody += '<td>' + elemento.totalComprobante.toFixed(2) + '</td>';
            tbody += '</tr>';
        }
    });
    
    $("#tablaComprobante tbody").append(tbody);
}

function limpiarFormularioComprobante() {
    $('#identificadorComprobante').val('0');
    $('#numeroComprobante').val('Autogenerado');
    $('#tipoComprobante').val('0');
    $('#vendedorComprobante').val('');
    $('#clienteComprobante').val('');
    $('#descuentoComprobante').val('0.00');
    $('#impuestoComprobante').val('0.00');
    $('#subTotalComprobante').val('0.00');
    $('#totalComprobante').val('0.00');

    listaComprobantesProductos = new Array();

    $('#tipoComprobante').focus();
}

function calcularTotalComprobante() {
    var totalComprobanteProducto = 0;

    listaComprobantesProductos.forEach(function (elemento) {
        if (elemento.eliminarComprobanteProducto == false) {
            totalComprobanteProducto += parseFloat(elemento.totalComprobanteProducto);
        }
    });

    var descuento = $('#descuentoComprobante').val();
    var impuesto = totalComprobanteProducto * (18 / 100);
    var subTotal = (totalComprobanteProducto - impuesto);
    var total = totalComprobanteProducto - descuento;

    $('#impuestoComprobante').val(impuesto.toFixed(2));
    $('#subTotalComprobante').val(subTotal.toFixed(2));
    $('#totalComprobante').val(total.toFixed(2));
}
//endregion Comprobante

//region ComprobanteProducto
function crearComprobanteProducto() {
    limpiarFormularioComprobanteProducto();
    $('#modalComprobanteProductoTitulo').text('Crear Producto');
    $('#modalComprobanteProducto').modal('show');
}

function editarComprobanteProducto(identificadorComprobanteProducto) {
    limpiarFormularioComprobanteProducto();
    $('#identificadorComprobanteProducto').val(identificadorComprobanteProducto);

    listaComprobantesProductos.forEach(function (elemento) {
        if (elemento.identificadorComprobanteProducto == identificadorComprobanteProducto) {
            $('#identificadorComprobanteProducto').val(elemento.identificadorComprobanteProducto);
            $('#nombreComprobanteProducto').val(elemento.nombreComprobanteProducto);
            $('#cantidadComprobanteProducto').val(elemento.cantidadComprobanteProducto);
            $('#precioComprobanteProducto').val(elemento.precioComprobanteProducto);
            $('#totalComprobanteProducto').val(elemento.totalComprobanteProducto);
        }
    });

    $('#modalComprobanteProductoTitulo').text('Editar Producto');
    $('#modalComprobanteProducto').modal('show');
}

function guardarComprobanteProducto() {
    var comprobanteProducto = {
        'identificadorComprobanteProducto' : '0',
        'identificadorComprobante': '0',
        'nombreComprobanteProducto': '',
        'cantidadComprobanteProducto': '0',
        'precioComprobanteProducto': '0',
        'totalComprobanteProducto': '0',
        'eliminarComprobanteProducto': false
    };

    if (~$('#identificadorComprobanteProducto').val().indexOf('temporal') || $('#identificadorComprobanteProducto').val() > '0') {
        listaComprobantesProductos.forEach(function (elemento) {
            if (elemento.identificadorComprobanteProducto == $('#identificadorComprobanteProducto').val()) {
                elemento.nombreComprobanteProducto = $('#nombreComprobanteProducto').val();
                elemento.cantidadComprobanteProducto = $('#cantidadComprobanteProducto').val();
                elemento.precioComprobanteProducto = $('#precioComprobanteProducto').val();
                elemento.totalComprobanteProducto = $('#totalComprobanteProducto').val();
            }
        });
    } else {
        comprobanteProducto.identificadorComprobanteProducto = generarTemporal($('#identificadorComprobanteProducto').val(), listaComprobantesProductos);
        comprobanteProducto.identificadorComprobante = $('#identificadorComprobante').val();
        comprobanteProducto.nombreComprobanteProducto = $('#nombreComprobanteProducto').val();
        comprobanteProducto.cantidadComprobanteProducto = $('#cantidadComprobanteProducto').val();
        comprobanteProducto.precioComprobanteProducto = $('#precioComprobanteProducto').val();
        comprobanteProducto.totalComprobanteProducto = $('#totalComprobanteProducto').val();

        listaComprobantesProductos.push(comprobanteProducto);
    }

    crearTablaComprobanteProducto();
    $('#modalComprobanteProducto').modal('hide');
}

function eliminarComprobanteProducto(identificadorComprobanteProducto) {
    listaComprobantesProductos.forEach(function (elemento) {
        if (elemento.identificadorComprobanteProducto == identificadorComprobanteProducto) {
            elemento.eliminarComprobanteProducto = true;
        }
    });

    crearTablaComprobanteProducto();
}

function listarComprobantesProductosPorIdentificadorComprobante(identificadorComprobante) {
    $.ajax({
        type: 'GET',
        url: '/ComprobanteProducto/ListarPorIdentificadorComprobante?IdentificadorComprobante=' + identificadorComprobante,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: null,
        success: function (response) {
            listaComprobantesProductos = new Array();

            response.ListaRespuesta.forEach(function (elemento) {
                var comprobanteProducto = {
                    'identificadorComprobanteProducto': '0',
                    'identificadorComprobante': '0',
                    'nombreComprobanteProducto': '',
                    'cantidadComprobanteProducto': '0',
                    'precioComprobanteProducto': '0.00',
                    'totalComprobanteProducto': '0.00',
                    'eliminarComprobanteProducto': false
                };

                comprobanteProducto.identificadorComprobanteProducto = elemento.IdentificadorComprobanteProducto;
                comprobanteProducto.identificadorComprobante = elemento.IdentificadorComprobante;
                comprobanteProducto.nombreComprobanteProducto = elemento.NombreComprobanteProducto;
                comprobanteProducto.cantidadComprobanteProducto = elemento.CantidadComprobanteProducto;
                comprobanteProducto.precioComprobanteProducto = elemento.PrecioComprobanteProducto;
                comprobanteProducto.totalComprobanteProducto = elemento.TotalComprobanteProducto;
                comprobanteProducto.eliminarComprobanteProducto = false;

                listaComprobantesProductos.push(comprobanteProducto);
            });

            crearTablaComprobanteProducto();
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function listarComprobantesProductos() {
    $.ajax({
        type: 'GET',
        url: '/ComprobanteProducto/Listar',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: null,
        success: function (response) {
            listaComprobantesProductos = new Array();

            response.ListaRespuesta.forEach(function (elemento) {
                var comprobanteProducto = {
                    'identificadorComprobanteProducto': '0',
                    'identificadorComprobante': '0',
                    'nombreComprobanteProducto': '',
                    'cantidadComprobanteProducto': '0',
                    'precioComprobanteProducto': '0.00',
                    'totalComprobanteProducto': '0.00',
                    'eliminarComprobanteProducto': false
                };

                comprobanteProducto.identificadorComprobanteProducto = elemento.nombreComprobanteProducto;
                comprobanteProducto.identificadorComprobante = elemento.identificadorComprobante;
                comprobanteProducto.nombreComprobanteProducto = elemento.nombreComprobanteProducto;
                comprobanteProducto.cantidadComprobanteProducto = elemento.cantidadComprobanteProducto;
                comprobanteProducto.precioComprobanteProducto = elemento.precioComprobanteProducto;
                comprobanteProducto.totalComprobanteProducto = elemento.totalComprobanteProducto;
                comprobanteProducto.eliminarComprobanteProducto = false;

                listaComprobantesProductos.push(comprobanteProducto);
            });

            crearTablaComprobanteProducto();
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function crearTablaComprobanteProducto() {
    $("#tablaComprobanteProducto tbody").empty();

    var tbody = listaComprobantesProductos.length > 0 ? '' : '<tr class="text-center"><td colspan="5">No hay Productos.</td></tr>';

    listaComprobantesProductos.forEach(function (elemento) {
        if (elemento.eliminarComprobanteProducto == false || elemento.eliminarComprobanteProducto == undefined) {
            tbody += '<tr>';
            tbody += '<td><div class="float-left"><button type="button" class="btn btn-outline-primary btn-sm" onclick="editarComprobanteProducto(' + "'" + elemento.identificadorComprobanteProducto + "'" + ');"><span class="fa fa-pencil-alt"></span></button></div>';
            tbody += '<div class="float-left"><button type="button" class="btn btn-outline-danger btn-sm" onclick="eliminarComprobanteProducto(' + "'" + elemento.identificadorComprobanteProducto + "'" + ');"><span class="fa fa-trash"></span></button></div></td>';
            tbody += '<td>' + elemento.nombreComprobanteProducto + '</td>';
            tbody += '<td>' + elemento.cantidadComprobanteProducto + '</td>';
            tbody += '<td>' + elemento.precioComprobanteProducto + '</td>';
            tbody += '<td>' + elemento.totalComprobanteProducto + '</td>';
            tbody += '</tr>';
        }
    });

    $("#tablaComprobanteProducto tbody").append(tbody);
    calcularTotalComprobante();
}

function limpiarFormularioComprobanteProducto() {
    $('#identificadorComprobanteProducto').val('0');
    $('#nombreComprobanteProducto').val('');
    $('#cantidadComprobanteProducto').val('0');
    $('#precioComprobanteProducto').val('0.00');
    $('#totalComprobanteProducto').val('0.00');

    $('#nombreComprobanteProducto').focus();
}

function calcularTotalComprobanteProducto() {
    var cantidad = $('#cantidadComprobanteProducto').val();
    var precio = $('#precioComprobanteProducto').val();
    var total = cantidad * precio;

    $('#totalComprobanteProducto').val(total.toFixed(2));
}
//region ComprobanteProducto

function generarTemporal(identificador, lista) {
    if (identificador.length > 0) {
        if (identificador.length > 7) {
            return identificador;
        }

        if (identificador == 0) {
            return 'temporal' + lista.length;
        }

        if (identificador > 0) {
            return identificador;
        }
    }
}

function agregarCeros(numero, tamaño) {
    var resultado = '' + numero;
    while (resultado.length < tamaño) {
        resultado = '0' + resultado;
    }
    return resultado;

}