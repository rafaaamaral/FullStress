var table;
var cdRowSelected = 0;
$(document).ready(function () {
    rodarMascaras();
});

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

String.prototype.replaceAll = function (search, replace) {
    //if replace is not sent, return original string otherwise it will
    //replace search string with 'undefined'.
    if (replace === undefined) {
        return this.toString();
    }

    return this.replace(new RegExp('[' + search + ']', 'g'), replace);
};

function montarDataTable() {
    $('.montarTable thead .searchItem').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search" class="column_search"/>');
    });

    $(".btnSelected").hide();
    table = $(".montarTable").DataTable({
        order: [[1, 'asc']]
    });

    $('.montarTable tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            cdRowSelected = 0;
            $(".btnSelected").hide();
            $(".btnHide").show();
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            cdRowSelected = table.row('.selected').data()[0];
            $(".btnSelected").show();
            $(".btnHide").hide();
        }
    });
}

function rodarMascaras() {

    //if ($("#cdCodigo").val() !== 0) {

    //    if ($("#esAtivo").val() === "True") {
    //        $("#esCKAtivo").iCheck('check');
    //    }
    //}

    $('input[type="checkbox"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-green',
        radioClass: 'iradio_minimal-green'
    });

    $(".telefone").inputmask({ "mask": "(99) 99999 9999" });
    //$(".cpf").mask("999.999.999-99");
    $(".data").inputmask({ "mask": "99/99/9999" });
    //$('.hora').mask('99:99');
    //$('.conta').mask('9999999999');
    //$('.centrocusto').mask('9999');

    //$(".hora").timepicker({
    //    showInputs: false
    //});

    $(".select2").select2();
    $('.data').datepicker({
        todayBtn: "linked",
        //language: "es",
        language: "pt",
        autoclose: true,
        todayHighlight: true,
        format: 'dd/mm/yyyy',
        orientation: "bottom auto"
    });

    //$('.anoMes').datepicker({
    //    format: "mm/yyyy",
    //    startView: "months",
    //    minViewMode: "months",
    //    autoclose: true,
    //});

    $('.valor').maskMoney({ prefix: 'R$ ', allowNegative: true, thousands: '.', decimal: ',' });

    //$('.valorPorcentagem').maskMoney({ allowNegative: true, thousands: '.', decimal: ',' });
    //$('.valorSemPrefixo').maskMoney({ allowNegative: true, thousands: '.', decimal: ',' });
    //$('.valorVolume').maskMoney({ allowNegative: true, thousands: '.', decimal: ',', precision: 6 });


    //$('.valor').maskMoney({ allowNegative: true, allowZero: true, affixesStay: false, thousands: '.', decimal: ',', });
    //$('.quantidade').maskMoney({ allowNegative: true, allowZero: true, affixesStay: false, thousands: '.', decimal: ',', precision: 0 });
}

function RetornaAlerta(pMensagem, pTipo) {
    if (pTipo === "W") {
        toastr.warning(pMensagem, "Atenção", {
                positionClass: "toast-top-center",
                timeOut: 5e3,
                closeButton: !0,
                debug: !1,
                newestOnTop: !0,
                progressBar: !0,
                preventDuplicates: !0,
                onclick: null,
                showDuration: "300",
                hideDuration: "1000",
                extendedTimeOut: "1000",
                showEasing: "swing",
                hideEasing: "linear",
                showMethod: "fadeIn",
                hideMethod: "fadeOut",
                tapToDismiss: !1
            });
    }
    else if (pTipo === "D") {
        toastr.error(pMensagem, "Erro", {
            positionClass: "toast-top-center",
            timeOut: 5e3,
            closeButton: !0,
            debug: !1,
            newestOnTop: !0,
            progressBar: !0,
            preventDuplicates: !0,
            onclick: null,
            showDuration: "300",
            hideDuration: "1000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut",
            tapToDismiss: !1
        });
    }
    else if (pTipo === "S") {
        toastr.success(pMensagem, "Sucesso", {
            positionClass: "toast-top-center",
            timeOut: 5e3,
            closeButton: !0,
            debug: !1,
            newestOnTop: !0,
            progressBar: !0,
            preventDuplicates: !0,
            onclick: null,
            showDuration: "300",
            hideDuration: "1000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut",
            tapToDismiss: !1
        });
    }
} //Alerta Tela

var waitingDialog = waitingDialog || (function ($) {
    'use strict';

    var $dialog = $(
        '<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
        '<div class="modal-dialog modal-m">' +
        '<div class="modal-content">' +
        '<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
        '<div class="modal-body">' +
        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
        '</div>' +
        '</div></div></div>');

    return {
        show: function (message, options) {
            // Assigning defaults
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Loading';
            }
            var settings = $.extend({
                dialogSize: 'm',
                progressType: '',
                onHide: null // This callback runs after the dialog was hidden
            }, options);

            // Configuring dialog
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text(message);
            // Adding callbacks
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }
            // Opening dialog
            $dialog.modal();
        },
        hide: function () {
            $dialog.modal('hide');
        }
    };

})(jQuery); //Modal Load