

var url = null;
 var tbSearchconfirmpayment;

$(document).ready(function () {
    url = $("#controllername").data("url");
    $("#ConfirmPayment").hide();
    $("#btnconfirmpayment").hide();

    Getpaymentchannal();
    GetDetailpaymentchannal();
    GetBankTransfer();
    GetStatus();

    $('#datereqfrom').datetimepicker({
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
    $('#datereqto').datetimepicker({
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
    $('#datepayfrom').datetimepicker({
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
    $('#datepayto').datetimepicker({
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
    $('#datepayment').datetimepicker({
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
    $('#timepayment').datetimepicker({
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


    inittbSearchconfirmpayment();


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


    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

    $('#slip_attach_file').change(function () {

        CheckFileUpload('slip_attach_file')
    });


    //$('#btnconfirmpayment').click(function () {
    //    var paymentid = $('#hdpaymentId').val();

    //    var forms = document.getElementsByClassName('needs-validation-confirmpayment');
    //    var validation = Array.prototype.filter.call(forms, function (form) {
    //        if ($('#Contact_name').val() == "" || $('#Contact_email').val() == "" || $('#Contact_phone_no').val() == "" ||
    //            $('#payment_channal option').filter(':selected').val() == "" || $('#payment_date').val() == "" || $('#payment_datetime').val() == "" ||
    //            $('#amount_transfer').val() == "" || $('#bank_transfer').val() == "" || ValidateUpload()) {
    //            event.preventDefault();
    //            event.stopPropagation();

    //        }

    //        else {
    //            UpdatePayment(paymentid);
    //            inittbSearchconfirmpayment();
    //            return true;

    //        }
    //        form.classList.add('was-validated');
    //    });
    //});

    $('#btnSearchconfirmpayment').click(function () {
        tbSearchconfirmpayment.ajax.reload();
    });

    $('#btnClearDataModal').click(function () {
        clearData();
    });
    

});



'use strict';
window.addEventListener('load', function () {
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.getElementsByClassName('needs-validation');
    // Loop over them and prevent submission
    var validation = Array.prototype.filter.call(forms, function (form) {
        form.addEventListener('submit', function (event) {

            if (form.checkValidity() === false) {
                event.preventDefault();
                event.stopPropagation();

            } else {
                var paymentid = $('#hdpaymentId').val();

                UpdatePayment(paymentid);
                inittbSearchconfirmpayment();

                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
    });
}, false);


function validateNumberDot(evt) {
    if (evt.keyCode != 8) {
        var theEvent = evt || window.event;
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
        var regex = /[0-9]|\./;
        if (!regex.test(key)) {
            theEvent.returnValue = false;

            if (theEvent.preventDefault)
                theEvent.preventDefault();
        }
    }
}

function clearData() {
  
    $("#Contact_name").val('');
    $("#Contact_email").val('');
    $("#Contact_phone_no").val('');
    $('#amount_transfer').val('');
    $('#bank_transfer').val('');
    $('#bank_branch').val('');
    $('#payment_channal').val('');
    $("#remark").val('');

    $('#payment_date').val('');
    $('#payment_datetime').val('');

    $('#hdupfileslip').val('');
    $('#lbuploadslip').html('Choose file');
    $("#nameslip_attach_file").html('Choose file');
     
    $('#linkdownload').text('');

        
}

function ValidateUpload() {
    var hasError = false;
    if ($('#hdupfileslip').val() == "") {
        hasError = true;
        $("#slip_attach_file").attr('required', 'required');
    }
    else {
        $("#slip_attach_file").removeAttr('required');
    }

    return hasError;
}

function CheckFileUpload(inputId) {

    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }

    var urlCheckFileUpload = url.replace('Action', 'CheckFileUpload');
    $.ajax(
        {
            type: "POST",
            url: urlCheckFileUpload,
            processData: false,
            contentType: false,
            dataType: "json",
            data: formData,
            success: function (data) {
                if (data.status) {
                    $('#hdupfileslip').val(data.file_id);
                }
               
                    bootbox.alert({
                        title: "System Information",
                        message: data.message,
                        size: "small",
                        callback: function (result) {
                            console.log('This was logged in the callback: ' + data.status);
                        }
                    });

            },
            error: function (xhr, status, error) {


                //bootbox.alert({
                //    title: "System Information",
                //    message: "This action is not available.",
                //    size: "small",
                //    callback: function (result) {
                //        console.log('This was logged in the callback: ' + result);
                //    }
                //});
            }
        }
    );
}

function inittbSearchconfirmpayment() {
    var urlSearchconfirmpayment = url.replace('Action', 'Searchconfirmpayment');
    tbSearchconfirmpayment = $('#tbSearchconfirmpayment').DataTable({
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
        "scrollX": true,
        "language": {
            "zeroRecords": "No data found.",
            "decimal": ",",
            "thousands": "."
        },
        ajax: {
            type: "POST",
            url: urlSearchconfirmpayment,
            dataType: "json",
            data: {
                //Paymentno: function () { return $('#paymentno').val(); },
                //Paymentrequesttraningno: function () { return $('#paymentrequesttraningno').val(); },
                Paymantrequestdatefrom: function () { return $('#paymentrequestdatefrom').val(); },
                Paymentrequestdateto: function () { return $('#paymentrequestdateto').val(); },
                Paymentdatefrom: function () { return $('#paymentdatefrom').val();},
                Paymentdateto: function () { return $('#paymentdateto').val(); },
                Paymentstatus: function () { return $('#paymentstatus option').filter(':selected').val(); }

            },
        },
        columns: [
            {
                "data": null, className: "text-center",render: function (data, type, row, meta) {
                    var action = '';
                    var status = '';
                    if (data.Status == 'Paid') {
                        action = "View";
                        status = "Y";
                    }
                    else if (data.Status == 'Approve')
                    {
                        action = "View";
                        status = "A";
                    }
                    else if (data.Status == "Waiting") {
                        action = "Confrim";
                        status = "W";
                    }
                    else if (data.Status == "Not Approve") {
                        action = "Confrim";
                        status = "N";
                    }
                    return '<a href="javascript:void(0);" onclick="GetIdpayment(\'' + data.PaymentId + '\',\'' + status + '\')"><u>' + action + '</u></a>';
                },

            },  
            {
                data: null, render: function (data, type, row) {
                    return "<span class='badge outline-badge-secondary shadow-none'>" + row.Status + "</span>";
                }

            },
            {
                "data": "RequestDate", "render": function (data, type, row) {
                    if (type === "sort" || type === "type") {
                        return data;
                    }
                    if (moment(data).format("DD/MM/YYYY") != 'Invalid date') {
                        return moment(data).format("DD/MM/YYYY");
                    }
                    else {
                        return data;
                    }

                }
            },
            {
                "data": "PaymentDatetime", "render": function (data, type, row) {
                    if (type === "sort" || type === "type") {
                        return data;
                    }
                    if (moment(data).format("DD/MM/YYYY") != 'Invalid date') {
                        return moment(data).format("DD/MM/YYYY");
                    }
                    else {
                        return data;
                    }

                }
            },
           
            { "data": "PaymentChannal" },
            { "data": "BankTransfer" },
            { "data": "AmountTransfer", render: $.fn.dataTable.render.number(',', '.', 2, '') },
            { "data": "ContactName" },
            { "data": "ContactPhoneNo" },
           
        ],
        "order": [[0, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }
    });
}

function getDropDownPaymentChannal() {
    var urlpaymentchannal = url.replace('Action', 'paymentchannal');
    $.ajax({
        type: "GET",
        url: urlpaymentchannal,
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {
            datapayment = response.data;
        },
        failure: function (msg) {
        }
    });
}


function confirmpayment() {


    var data = new FormData();
    data.append("Bank", $('#Bank').val());
    data.append("Date", strdatefrom);
    data.append("Time", $('#Time').val());
    data.append("Balance", $('#Balance').val());
    data.append("SourceBank", $('#SourceBank').val());
    data.append("branch", $('#branch').val());
    data.append("credit", $("#credit").get(0).files[0]);
    data.append("Note", $('#Note').val());
    var urlconfirmpayment = url.replace('Action', 'confirmpayment');
    $.ajax({
        type: "POST",
        url: urlconfirmpayment,
        data: data,
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {

            if (response.data == "true") {



            }
        },
        failure: function (msg) {
        }
    });
}

function GetIdpayment(paymentId, status) {
    var urlGetByPaymentId = url.replace('Action', 'GetByPaymentId');
    $.ajax({
        type: "GET",
        url: urlGetByPaymentId+"?id=" + paymentId,
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {
            $('#hdpaymentId').val(response.Data.PaymentId);

            if (status == 'W' || status == 'N') {
                SetTextStatusN(response.Data);
            }
            else if (status == 'Y' || status =='A') {
                SetTextStatusY(response.Data);
            }
            
        },
        failure: function (msg) {
        }
    });

 



}

function DownloadFileSlip(filename) {
    var urlDownloadfileConfirm = url.replace('Action', 'DownloadfileConfirm');

    $('#linkdownload').attr("href", urlDownloadfileConfirm+'?paymentid=' + $('#hdpaymentId').val() + '&&filename=' + filename);

}



function SetTextStatusN(data) {
    console.log(data);

    clearData();

    $("#ConfirmPayment").modal('show');
    //$("#btnconfirmpayment").show('slow');

    $('#btnClearDataModal').show();
    $('#btnconfirmpayment').show();

    //$("#Request_no").text(data.Request_no);
    //$("#Payment_no").text(data.PaymentNo);
    $("#Company_name").text(data.companyNameTh);
    $("#address").text(data.companyAddress);
    $("#Tax_id").text(data.taxId);
    $("#Contact_name").val(data.ContactName);
    $("#Contact_email").val(data.ContactEmail);
    $("#Contact_phone_no").val(data.ContactPhoneNo);
    $('#amount_transfer').val(data.AmountTransfer);
    $('#bank_transfer').val(data.BankTransfer);
    $('#bank_branch').val(data.BankBranch);
    $('#payment_channal').val(data.PaymentChannal);
    $("#remark").val(data.Remark);

    //var d = new Date(data.PaymentDatetime);
    //var month = d.getMonth();
    //var day = d.getDate();
    //var year = d.getFullYear();
    //var HH = d.getHours();
    //var MM = d.getMinutes();

    //$('#datepayment').data("DateTimePicker").date(moment(new Date(year, month, day), 'DD/MM/YYYY'));
    //$('#timepayment').data("DateTimePicker").date(moment(new Date(year, month, day, HH, MM), 'HH:MM'));

    $('#payment_date').val('');
    $('#payment_datetime').val('');

    //if (data.SlipAttachFile != null) {
    //    $('#lbuploadslip').html(data.SlipAttachFile);
    //    $('#hdupfileslip').val(data.file_id_Slip);
    //    $('#linkdownload').text(data.SlipAttachFile);
    //    DownloadFileSlip(data.SlipAttachFile);
    //}

    EnableControl(false);
   
}

function SetTextStatusY(data) {
    console.log(data);
    $("#ConfirmPayment").modal('show');
    //$("#btnconfirmpayment").show('slow');
    $('#btnClearDataModal').hide();
    $('#btnconfirmpayment').hide();
    //var textdropdrowpaymentchanel = GetpaymentchannalById(data.PaymentChannal);
    //var textpaymentchannal = '<option value="">' + textdropdrowpaymentchanel + '</option>';

    var d = new Date(data.PaymentDatetime);
    var month = d.getMonth();
    var day = d.getDate();
    var year = d.getFullYear();
    var HH = d.getHours();
    var MM = d.getMinutes();

    $('#datepayment').data("DateTimePicker").date(moment(new Date(year, month, day), 'DD/MM/YYYY'));
    $('#timepayment').data("DateTimePicker").date(moment(new Date(year, month, day, HH, MM), 'HH:MM'));

    //$("#Request_no").text(data.Request_no);
    //$("#Payment_no").text(data.PaymentNo);
    $("#Company_name").text(data.companyNameTh);
    $("#address").text(data.companyAddress);
    $("#Tax_id").text(data.taxId);

    $("#Contact_name").val(data.ContactName);
    $("#Contact_email").val(data.ContactEmail);
    $("#Contact_phone_no").val(data.ContactPhoneNo);
    //$("#payment_channal option").remove();
    //$('#payment_channal').append(textpaymentchannal);
    //GetDetailpaymentchannal();

    $("#payment_channal").val(data.PaymentChannal);
    //document.getElementById("amount_transfer").style.backgroundColor = "lightgray";
    $('#amount_transfer').val(data.AmountTransfer);
    $('#bank_transfer').val(data.BankTransfer);
    $('#bank_branch').val(data.BankBranch);
   // $("#nameslip_attach_file").html(data.SlipAttachFile);
    $("#remark").val(data.Remark);

    if (data.SlipAttachFile != null) {
        $('#lbuploadslip').html(data.SlipAttachFile);
        $('#hdupfileslip').val(data.file_id_Slip);
        $('#linkdownload').text(data.SlipAttachFile);
        DownloadFileSlip(data.SlipAttachFile);
    }

    EnableControl(true); //'disabled'

}

function EnableControl(status) {
    $("#Contact_name").attr('disabled', status);
    $("#Contact_email").attr('disabled', status);
    $("#Contact_phone_no").attr('disabled', status);
    $("#payment_channal").attr('disabled', status);

    $("#textpayment_channal").attr('disabled', status);
    $("#Detailpaymentchannal input").attr('disabled', status);
    $('#payment_date').attr('disabled', status);
    $('#payment_datetime').prop('disabled', status);
    $("#amount_transfer").attr('disabled', status);
    $('#bank_transfer').attr('disabled', status);
    $("#bank_branch").attr('disabled', status);;
    $("#slip_attach_file").attr('disabled', status);
    $("#remark").attr('disabled', status);
}

function UpdatePayment(PaymentId) {
    var payment_date = $("#payment_date").val();
    var payment_datetime = $("#payment_datetime").val();
    var data = new FormData();
    data.append("PaymentId",PaymentId);
    //data.append("Request_no", $('#Request_no').text());
    //data.append("PaymentNo", $('#Payment_no').text());
    data.append("ContactName", $('#Contact_name').val());
    data.append("ContactEmail", $('#Contact_email').val());
    data.append("ContactPhoneNo", $('#Contact_phone_no').val());
    data.append("PaymentChannal", $('#payment_channal').val());
    data.append("datetimepayment", payment_date + ' ' + payment_datetime);
    data.append("AmountTransfer", $("#amount_transfer").val());
    data.append("BankTransfer", $('#bank_transfer').val());
    data.append("BankBranch", $('#bank_branch').val());
    data.append("FileSilp", $('#slip_attach_file').get(0).files[0]);
    data.append("Remark ", $('#remark').val());
    data.append("Status", 'Y');
    var urlUpdatepayment = url.replace('Action', 'Updatepayment');
    $.ajax({
        type: "POST",
        url: urlUpdatepayment,
        data: data,
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {
            if (response.Status) {
                bootbox.alert({
                    message: "แจ้งโอนเงินสำเร็จ"
                })

                $("#ConfirmPayment").modal('hide');
            }
            else {
                bootbox.alert({
                    message: "แจ้งโอนเงินไม่สำเร็จ : " + response.Data,

                })

            }
        },
        failure: function (msg) {
        }
    });

}

function clearpayment() {
    $("#Contact_name").val('');
    $("#Contact_email").val('');
    $("#Contact_phone_no").val('');
    $("payment_date").val('');
    $("payment_datetime").val('');
    $("#amount_transfer").val('');
    $("#bank_branch").val('');

    var oldInput = document.getElementById("slip_attach_file");
    var newInput = document.createElement("input");
    newInput.type = "file";
    newInput.id = oldInput.id;
    newInput.name = oldInput.name;
    newInput.className = oldInput.className;
    newInput.style.cssText = oldInput.style.cssText;
    oldInput.parentNode.replaceChild(newInput, oldInput);
    $("#nameslip_attach_file").html('Choose file');

    $("#remark").val('');

}

function clearSearchpayment() {
    $("#paymentno").val('');
    $("#paymentrequesttraningno").val('');
    $("#paymentstatus").val('')

    $('#paymentrequestdatefrom').val('');
    $('#paymentrequestdateto').val('');
    $('#paymentdatefrom').val('');
    $('#paymentdateto').val('');
}

function closepayment() {
    $("#ConfirmPayment").modal('hide');
}

function Getpaymentchannal() {
    var urlBindDataTypeTransfer = url.replace('Action', 'BindDataTypeTransfer');
    $.ajax({
        type: "POST",
        url: urlBindDataTypeTransfer,
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            if (data != null) {

                $('#payment_channal').empty();


                $.each(data.response, function () {
                    $('#payment_channal').append($("<option></option>").val(this.Value).text(this.Text));
                });
            }
            else {
                $('#payment_channal').empty();
                $('#payment_channal').append($("<option></option>").val("").text("Select Payment Transfer"));
            }
        },
        failure: function (msg) {
        }
    });

}

function GetpaymentchannalById(id) {
    var dropdown;
    var strdropdown_text = '';
    var urlpaymentchannal = url.replace('Action', 'paymentchannal');
    $.ajax({
        type: "GET",
        url: urlpaymentchannal,
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {
            dropdown = response.Data;
        },
        failure: function (msg) {
        }
    });
    $.each(dropdown, function (index, data) {
        if (data.dropdown_value = id) {
            strdropdown_text = data.dropdown_text;
        }
    });
    return strdropdown_text;
}

function GetDetailpaymentchannal() {
    var datapayment;
    var strGetDetailpaymentchannal = '';
    var urlGetDataBankPayment = url.replace('Action', 'GetDataBankPayment');
    $.ajax({
        type: "POST",
        url: urlGetDataBankPayment,
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            if (data != null) {
                $.each(data.response, function () {
                    var strtext = this.dropdown_text;
                    var strvalue = this.dropdown_value;
                    var str = "<div class='custom-control custom-radio custom-control-inline'>\
                        <input type='radio' class='custom-control-input inputValidation' name='optionsRadios' id='rd"+ strvalue + "' value='" + strvalue + "' required/>\
                        <label class='custom-control-label' for='rd"+ strvalue + "'>" + strtext + "</label></div >"
                    $('.Detailpaymentchannal').append(str);


                });
            }

        },
        failure: function (msg) {
        }
    });
    //$.each(datapayment, function (index, data) {
    //    strGetDetailpaymentchannal +=
    //        '<div class="col-md-12">' +
    //        '<input class="form-check-input" type="radio" checked>' +
    //        '<label style="color:#808080;font-size:17px;" id="textpayment_channal">"' + data.dropdown_text + '"</label>' +
    //        '</div>';
    //});
    //console.log(strGetDetailpaymentchannal)
    //$("#Detailpaymentchannal").empty();
    //$('#Detailpaymentchannal').append(strGetDetailpaymentchannal);
}

function GetBankTransfer() {
    var urlDDLBank = url.replace('Action', 'DDLBank');
    $.ajax({
        type: "POST",
        url: urlDDLBank,
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            if (data != null) {
                $('#bank_transfer').empty();

                $.each(data.responsebank, function () {
                    $('#bank_transfer').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }

        },
        failure: function (msg) {
            $('#bank_transfer').empty();
            $('#bank_transfer').append($("<option></option>").val("").text("Select Bank"));
        }
    });
}

function GetStatus() {
    var urlGetDataStatus = url.replace('Action', 'GetDataStatus');
    $.ajax({
        type: "POST",
        url: urlGetDataStatus,
        async: false,
        success: function (data) {
            if (data != null) {

                $('#paymentstatus').empty();


                $.each(data.response, function () {
                    $('#paymentstatus').append($("<option></option>").val(this.Value).text(this.Text));
                });
            }
            else {
                $('#paymentstatus').empty();
                $('#paymentstatus').append($("<option></option>").val("").text("Select Status"));
            }
        },
        failure: function (msg) {
        }
    });

}