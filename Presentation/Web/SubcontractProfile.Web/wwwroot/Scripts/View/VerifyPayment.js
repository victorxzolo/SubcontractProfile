
var oTableSearch;
var url = null;

$(document).ready(function () {

    url = $("#controllername").data("url");

    $('#paymentdatefrom').datetimepicker({
        format: "DD/MM/YYYY",
        showClear: true,
        showClose: true,
        icons: {
            time: 'fa fa-clock-o',
            date: 'fa fa-calendar',
            up: 'fa fa-chevron-up',
            down: 'fa fa-chevron-down',
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'icon-screenshot',
            clear: 'fa fa-trash',
            close: 'fa fa-time'
        }
    });
    $('#paymentdatefto').datetimepicker({
        format: "DD/MM/YYYY",
        showClear: true,
        showClose: true,
        icons: {
            time: 'fa fa-clock-o',
            date: 'fa fa-calendar',
            up: 'fa fa-chevron-up',
            down: 'fa fa-chevron-down',
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'icon-screenshot',
            clear: 'fa fa-trash',
            close: 'fa fa-time'
        }
    });

    $("#paymentdatefrom").on("dp.change", function (e) {
        $('#paymentdatefto').data("DateTimePicker").minDate(e.date);
    });
    $("#paymentdatefto").on("dp.change", function (e) {

        $('#paymentdatefrom').data("DateTimePicker").maxDate(e.date);
    });

    $('#datetimepicker').datetimepicker({
        format: "DD/MM/YYYY",
        showClear: true,
        showClose: true,
        icons: {
            time: 'fa fa-clock-o',
            date: 'fa fa-calendar',
            up: 'fa fa-chevron-up',
            down: 'fa fa-chevron-down',
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'icon-screenshot',
            clear: 'fa fa-trash',
            close: 'fa fa-time'
        }
    });
    $('#datetimepickerais').datetimepicker({
        format: "DD/MM/YYYY",
        showClear: true,
        showClose: true,
        icons: {
            time: 'fa fa-clock-o',
            date: 'fa fa-calendar',
            up: 'fa fa-chevron-up',
            down: 'fa fa-chevron-down',
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'icon-screenshot',
            clear: 'fa fa-trash',
            close: 'fa fa-time'
        }
    });
    $('#timepicker').datetimepicker({
        format: 'HH:mm',
        locale: 'th',
        showClear: true,
        showClose: true,
        icons: {
            time: 'fa fa-clock-o',
            date: 'fa fa-calendar',
            up: 'fa fa-chevron-up',
            down: 'fa fa-chevron-down',
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'icon-screenshot',
            clear: 'fa fa-trash',
            close: 'fa fa-time'
        }
    });

    var urlUploadFile = url.replace('Action', 'UploadMulti');
    $(".file-loading input").fileinput({
        language: 'th',
        theme: 'fa',
        allowedFileExtensions: ['jpg', 'jpeg', 'bmp', 'gif', 'tif', 'tiff', 'png', 'pdf'],
        maxFileCount: localizedData.ConfigUpload,
        uploadUrl: urlUploadFile,
        uploadAsync: false,
        overwriteInitial: false,
        showRemove: false,
        showUpload: false,
        initialPreviewAsData: true,
        uploadExtraData: function (previewId, index) {
            var input = document.getElementById('input-700');
            var files = input.files[index];
            var formData = new FormData();
            formData.append("files", files);
            return {
                files: files,
                companyid: $('#hdcompanyId').val(),
                paymentid: $('#hdpaymentId').val()
            };

        },
    }).on('fileuploaded', function (event, data, previewId, index) {
        var form = data.form
        var files = data.files
        var extra = data.extra
        var response = data.response
        var reader = data.reader;
        console.log(data);

    }).on('filebatchuploaderror', function (event, data, previewId, index) {
        var form = data.form, files = data.files, extra = data.extra,
            response = data.response, reader = data.reader;
        console.log('File error ' + data.response.Message);
    }).on('filepreremove', function (event, key, jqXHR, data) {
        console.log(event);
        console.log(key);
        console.log(jqXHR);
        console.log(data);
    });


    $('#txttaxid').keyup(function () {
        CheckKeyUps("txttaxid", "[0-9]");
    });

    inittbSearchResult();
    BindDataTypeTransfer();
    BindBankPayment();
    BindDDLBank();
    GetStatus();
    //GetDropDownLocation();
    //BindDDLTeam();

    $('#btnSearch').click(function () {
        inittbSearchResult();
    });
    $('#btnclear').click(function () {
        $('#txtcompanyname').val('');
        $('#txttaxid').val('');
        $('#txtpayment').val('');
        $('#ddlStatus').val('');
        $('#datepaymentfrom').val('');
        $('#datepaymentto').val('');
        oTableSearch.ajax.reload();
    });

    $('#btnSave').click(function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            if ($('#ddlpaymentstatusais option').filter(':selected').val() == "" ) {

                event.preventDefault();
                event.stopPropagation();
            }
            else if ($('#dateverifyais').val() == "")
            {
                event.preventDefault();
                event.stopPropagation();
            }
            else {
                OnSave();
                return true;

            }
            form.classList.add('was-validated');
        });
       
    });

    $('#btnClearModal').click(function () {
        ClearDataModalRevenue();
        //ClearDataModal();
    });
    
    $(document).on('show.bs.modal', '.modal', function () {
        var zIndex = 1040 + (10 * $('.modal:visible').length);
        $(this).css('z-index', zIndex);
        setTimeout(function () {
            $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
        }, 0);
    });

    $(document).on('hidden.bs.modal', '.modal', function () {
        $('.modal:visible').length && $(document.body).addClass('modal-open');
    });

    var urlGetCompany = url.replace('Action', 'GetCompany');
    $('#txtcompanyname').autocomplete({
        lookup: function (query, done) {
            $.ajax({
                type: "POST",
                url: urlGetCompany,
                data: { companyname: query},
                dataType: "json",
                success: function (data) {
                    var suggestions = $.map(data.response, function (v) {
                        return {
                            'value': v.CompanyNameTh, 'data': v.CompanyId
                        };
                    });
                    var result = { 'suggestions': suggestions };
                    done(result);


                },
                error: function (xhr, status, error) {

                }
            });
        },
        minChars: 1,
        onSelect: function (suggestion) {
            $('#selection').html('You selected: ' + suggestion.value);
        },
        showNoSuggestionNotice: true,
        noSuggestionNotice: localizedData.MessageAutocomplete,
    });
});


//'use strict';
//window.addEventListener('load', function () {
//    // Fetch all the forms we want to apply custom Bootstrap validation styles to
//    var forms = document.getElementsByClassName('needs-validation');
//    // Loop over them and prevent submission
//    var validation = Array.prototype.filter.call(forms, function (form) {
//        form.addEventListener('submit', function (event) {

//            if (form.checkValidity() === false) {
//                event.preventDefault();
//                event.stopPropagation();

//            } else {

//                OnSave();
//                inittbSearchResult();

//                event.preventDefault();
//                event.stopPropagation();
//            }
//            form.classList.add('was-validated');
//        }, false);
//    });
//}, false);

function inittbSearchResult() {
    var urlSearchVerify = url.replace('Action', 'SearchVerify');
    oTableSearch = $('#tbSearchResult').DataTable({
        processing: true, // for show progress bar
        serverSide: true, // for process server side
        filter: false, // this is for disable filter (search box)
        orderMulti: false, // for disable multiple column at once
        pageLength: 10,
        order: [],
        autoWidth: false,
        searching: false,
        destroy: true,
        lengthChange: false,
        "oLanguage": {
            "oPaginate": { "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>', "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>' },
            "sInfo": "Showing page _PAGE_ of _PAGES_",
            "sSearch": false,
            "sLengthMenu": "Results :  _MENU_",
        },
        "scrollX": false,
        "language": {
            "zeroRecords": "No data found.",
            "decimal": ",",
            "thousands": "."
        },
        ajax: {
            url: urlSearchVerify,
            type: "POST",
            datatype: "JSON",
            //dataSrc:"",
            data: function (d) {
                d.companyNameTh = $("#txtcompanyname").val(),
                    d.taxId = $("#txttaxid").val(),
                   // d.PaymentNo = $('#txtpayment').val(),
                    d.Status = $('#ddlStatus option').filter(':selected').val(),
                    d.PaymentDatetimeFrom = $("#datepaymentfrom").val(),
                    d.PaymentDatetimeTo = $("#datepaymentto").val()
            }
        },
        columns: [

            {
                data: null, width: "10%", render: function (data, type, row) {
                    return "<a href='#' onclick=openModal('" + row.PaymentId + "');return false;  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='แก้ไข'><svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-edit'><path d='M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7'></path><path d='M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z'></path></svg></a>";
                }
                // orderable: false,
                //className: 'text-center'
            },
            { "data": "companyNameTh", "width": "20%" },
            { "data": "taxId", "width": "20%" },
            {
                "data": "PaymentDatetime", "width": "20%", "render": function (data, type, row) {
                    if (type === "sort" || type === "type") {
                        return data;
                    }
                    if (moment(data).format("DD/MM/YYYY HH:MM") != 'Invalid date') {
                        return moment(data).format("DD/MM/YYYY HH:MM");
                    }
                    else {
                        return data;
                    }

                }

            },
            { "data": "Status", "width": "20%" },
            {
                "data": "AmountTransfer", "width": "20%", "render": function (data, type, row) {
                    if (type === "sort" || type === "type") {
                        return data;
                    }
                    return formatMoney(data);
                }
            }

        ],
        "order": [[0, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

    });

}

function openModal(paymentid) {


    $('#hdpaymentId').val(paymentid);

    getDataById(paymentid);


    $('#VerifyPaymentModal').modal('show');

}

function ClearDataModal() {
    $('#lblcompanyName').text('');
    $('#hdpaymentId').val('');
    //$('#lbpaymentno').text('');
    $('#ddlpaymenttransfer').val('');
    $('#dateverify').val('');
    $('#datetimepicker').data("DateTimePicker").date(moment(new Date(), 'DD/MM/YYYY'));
    $('#timepayverify').val('');
    $('#timepicker').data("DateTimePicker").date(moment(new Date(), 'HH:MM'));

    $('#txtBalance').val('');
    $('#ddlbank').val('');
    $('#txtbranch').val('');

    $('#lbuploadslip').text('Choose file');
    $('#hdupfiletransfer').val('');
    $('#linkdownload').text('');

    $('#txtremark').val('');

    $(':radio').each(function (i) {

        //if (channel == $(this).val()) {
            $(this).prop('checked', false);
        //}
    });
}

function BindDataTypeTransfer() {
    var urlBindDataTypeTransfer= url.replace('Action', 'BindDataTypeTransfer');
    $.ajax({
        type: "POST",
        url: urlBindDataTypeTransfer,
        dataType: "json",
        success: function (data) {
            if (data != null) {

                $('#ddlpaymenttransfer').empty();
           

                $.each(data.response, function () {
                    $('#ddlpaymenttransfer').append($("<option></option>").val(this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr, status, error) {

        }
    });
}

function BindBankPayment() {
    var urlGetDataBankPayment = url.replace('Action', 'GetDataBankPayment');
    $.ajax({
        type: "POST",
        url: urlGetDataBankPayment,
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $.each(data.response, function () {
                    var strtext = this.dropdown_text;
                    var strvalue = this.dropdown_value;
                    var str = "<div class='custom-control custom-radio custom-control-inline'>\
                        <input type='radio' class='custom-control-input inputValidation' name='optionsRadios' id='rd"+ strvalue + "' value='" + strvalue+"' checked/>\
                        <label class='custom-control-label' for='rd"+ strvalue+"'>"+ strtext+"</label></div >"
                    $('.rdbank_payment').append(str);


                });
            }


        },
        error: function (xhr, status, error) {

        }
    });
}

function BindDDLBank() {
    var urlDDLBank = url.replace('Action', 'DDLBank');
    $.ajax({
        type: "POST",
        url: urlDDLBank ,

        dataType: "json",
        success: function (data) {

            if (data != null) {
                $('#ddlbank').empty();

                $.each(data.responsebank, function () {
                    $('#ddlbank').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr, status, error) {

        }
    });
}

function getDataById(id) {
    var urlGetDataById = url.replace('Action', 'GetDataById');
    $.ajax({
        url: urlGetDataById,
        type: 'POST',
        data: { 'paymentId': id },
        dataType: "json",
        success: function (result) {
            ClearDataModal();
            if (result != null) {
                $('#hdpaymentId').val(id);

                $('#hdCompanyId').val(result.Payment.CompanyId);

                $("#lblcompanyName").text(result.Payment.companyNameTh);
                //$('#lbpaymentno').text(result.PaymentNo);
                $('#ddlpaymenttransfer').val(result.Payment.PaymentChannal);

                if (result.Payment.PaymentDatetime != null) {
                    var d = new Date(result.Payment.PaymentDatetime);
                    var month = d.getMonth();
                    var day = d.getDate();
                    var year = d.getFullYear();
                    var HH = d.getHours();
                    var MM = d.getMinutes();

                    $('#datetimepicker').data("DateTimePicker").date(moment(new Date(year, month, day), 'DD/MM/YYYY'));

                    $('#timepicker').data("DateTimePicker").date(moment(new Date(year, month, day, HH, MM), 'HH:MM'));
                }
           

                $('#txtBalance').val(result.Payment.AmountTransfer);
                $('#ddlbank').val(result.Payment.BankTransfer);
                $('#txtbranch').val(result.Payment.BankBranch);

                //if (result.SlipAttachFile != null) {
                //    $('#lbuploadslip').html(result.SlipAttachFile);
                //    $('#hdupfiletransfer').val(result.file_id_Slip);
                //    $('#linkdownload').text(result.SlipAttachFile);
                //    DownloadFileSlip(result.SlipAttachFile);
                //}
                $('#txtremark').val(result.Payment.Remark);

                var channel = result.Payment.PaymentChannal
                if (result.transfer_to_account != null) {
                    $('.rdbank_payment input[name=optionsRadios]').each(function (i) {
                        if (result.transfer_to_account == $(this).val()) {
                            $(this).prop('checked', true);
                        }
                    });
                }

                console.log(result.FileInput);
                if (result.FileInput != null) {
                    $(".file-loading input").fileinput(result.FileInput);
                }
              
            }


        },
        error: function (result) {

        }

    });
}

function ClearDataModalRevenue() {
    $('#hdpaymentId').val('');
    $('#ddlpaymentstatusais').val('');
    $('#dateverifyais').val('');
    $('#txtremarkais').val('');
    oTableSearch.ajax.reload();
    $('#VerifyPaymentModal').modal('hide');
}

function OnSave() {
    var urlSaveVerify = url.replace('Action', 'SaveVerify');
    $.ajax({
        url: urlSaveVerify,
        type: 'POST',
        data: {
            'paymentId': $('#hdpaymentId').val(),
            'status': $('#ddlpaymentstatusais option').filter(':selected').val(),
            'verifydate': $('#dateverifyais').val(),
            'remark_for_sub': $('#txtremarkais').val()
        },
        dataType: "json",
        success: function (result) {

            if (result.Status) {
                ClearDataModalRevenue();
                bootbox.alert({
                    message: result.Message
                })
            }
            else {
                bootbox.alert({
                    message: result.Message + response.Data,

                })
            }
        },
        error: function (xhr, status, error) {
            //bootbox.confirm({
            //    title: "System Information",
            //    message: "This action is not available.",
            //    buttons: {
            //        cancel: {
            //            label: '<i class="fa fa-times"></i> Cancel'
            //        }
            //    },
            //    callback: function (result) {
            //        console.log('This was logged in the callback: ' + result);
            //    }
            //});
        }

    });
}

function DownloadFileSlip(filename) {
    var urlDownloadfile = url.replace('Action', 'DownloadfileConfirm');
    // $('#ItemPreview').attr('src', '/Payment/DownloadCSV?paymentid=' + $('#hdpaymentId').val());
    
    $('#linkdownload').attr("href", urlDownloadfile + '?paymentid=' + $('#hdpaymentId').val() + '&&filename=' + filename + '&&companyId=' + $('#hdCompanyId').val());

}

function GetStatus() {
    var urlGetDataStatus = url.replace('Action', 'GetDataStatus');
    $.ajax({
        type: "POST",
        url: urlGetDataStatus,
        success: function (data) {
            if (data != null) {

                $('#ddlpaymentstatusais').empty();
                $('#ddlStatus').empty();
                             
                $.each(data.response, function () {
                  
                        $('#ddlpaymentstatusais').append($("<option></option>").val(this.Value).text(this.Text));
                        $('#ddlStatus').append($("<option></option>").val(this.Value).text(this.Text));
                
                });
            }

        },
        failure: function (msg) {
        }
    });

}


function formatMoney(number, decPlaces, decSep, thouSep) {
    decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces,
        decSep = typeof decSep === "undefined" ? "." : decSep;
    thouSep = typeof thouSep === "undefined" ? "," : thouSep;
    var sign = number < 0 ? "-" : "";
    var i = String(parseInt(number = Math.abs(Number(number) || 0).toFixed(decPlaces)));
    var j = (j = i.length) > 3 ? j % 3 : 0;

    return sign +
        (j ? i.substr(0, j) + thouSep : "") +
        i.substr(j).replace(/(\decSep{3})(?=\decSep)/g, "$1" + thouSep) +
        (decPlaces ? decSep + Math.abs(number - i).toFixed(decPlaces).slice(2) : "");
}