
$(document).ready(function () {

    $('#CPF_beneficiario').mask('000.000.000-00', { reverse: true });

    $('#formCadastroBeneficiario').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPostBenef,
            method: "POST",
            data: {
                "Nome": $("#Nome_beneficiario").val(),                
                "CPF": $("#CPF_beneficiario").val(),
                "IDCLIENTE": $("#IDCLIENTE").val()
            },
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastroBeneficiario")[0].reset();
                getBeneficiarios();
            }
        });
    })

    $('#beneficiario').click(function () {
        getBeneficiarios();
        $('#modal_beneficiario').modal();
        return false;
    })

    $(document).on("click", ".excluirBeneficiario", function (e) {
        e.preventDefault();

        var r = confirm('Tem certeza que deseja excluir o beneficiário?');

        var id = $(this).attr('codigo');

        console.log($(this));

        if (r == true) {
            $.ajax({
                url: urlPostExcBene,
                method: "POST",
                data: {
                    "id": id
                },
                error:
                    function (r) {
                        if (r.status == 400)
                            ModalDialog("Ocorreu um erro", r.responseJSON);
                        else if (r.status == 500)
                            ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                    },
                success:
                    function (r) {
                        ModalDialog("Beneficio Excluido com sucesso!", r)
                        $("#formCadastroBeneficiario")[0].reset();
                        getBeneficiarios();
                    }
            });
        }
    });

    function getBeneficiarios() {
        $.ajax({
            url: urlGetBenef,
            method: "POST",
            data: {
                idcliente: $("#IDCLIENTE").val()
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    if (r.Result == 'OK') {
                        

                        var html = '';

                        $.each(r.Records, function (index, value) {
                            console.log(value);
                            html += '<tr>' +
                                '<td> ' + value.CPF + ' </td>' +
                                '<td> ' + value.Nome + ' </td>' +
                                '<td> <button type="submit" class="btn btn-sm btn-primary">Alterar</button> <button type="submit" class="btn btn-sm btn-primary excluirBeneficiario" codigo="' + value.Id+'" >Excluir</button>  </td>';
                        });

                        $('#tbory_benefi').html(html);
                    }
                    
                }
        });
    }
    
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
