


 var tbSearchconfirmpayment;

$(document).ready(function () {

    $("#ConfirmPayment").hide();
    $("#btnconfirmpayment").hide();

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
    //document.getElementById("payment_date").flatpickr({
    //    enableTime: false,
    //    dateFormat: "d/m/Y",
    //    defaultDate: "today"
    //});
    //document.getElementById("payment_datetime").flatpickr({
    //    enableTime: true,
    //    noCalendar: true,
    //    dateFormat: "H:i",
    //    defaultDate: "00:00"
    //});

    //window.addEventListener('load', function () {

    //    var formsSearchPayment = document.getElementsByClassName('SearchPaymentvalidation');
    //    var invalidSearch = $('.SearchPaymentvalidation .invalid-feedback');
    //    Array.prototype.filter.call(formsSearchPayment, function (form) {
    //        form.addEventListener('submit', function (event) {
    //            if (form.checkValidity() === false) {
    //                event.preventDefault();
    //                event.stopPropagation();
    //                invalidSearch.css('display', 'block');

    //            } else {

    //                invalidSearch.css('display', 'none');
    //                Searchpayment();
    //            }
    //        }, false);
    //    });

    //}, false);

    //window.addEventListener('load', function () {
    //    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    //    var forms = document.getElementsByClassName('ConfirmPaymentvalidation');

    //    // Loop over them and prevent submission
    //    var validation = Array.prototype.filter.call(forms, function (form) {
    //        form.addEventListener('submit', function (event) {
    //            var PaymentID = document.getElementById('btnupdatepayment');
    //            if (form.checkValidity() === false) {
    //                event.preventDefault();
    //                event.stopPropagation();
    //            }
    //            else {
    //                UpdatePayment(PaymentID.value);
    //            }

    //            form.classList.add('was-validated');
    //        }, false);
    //    });
    //}, false);

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

    $('#btnconfirmpayment').click(function () {
        var forms = document.getElementsByClassName('needs-validation-confirmpayment"');
             var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                var PaymentID = document.getElementById('btnupdatepayment');
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                else {
                    UpdatePayment(PaymentID.value);
                }

                form.classList.add('was-validated');
            }, false);
        });
    });


    function Searchpayment() {
          tbSearchconfirmpayment.ajax.reload();
    }



});

function inittbSearchconfirmpayment() {

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
            url: "/Payment/Searchconfirmpayment",
            dataType: "json",
            data: {
                Paymentno: function () { return $('#paymentno').val(); },
                Paymentrequesttraningno: function () { return $('#paymentrequesttraningno').val(); },
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
                    if (data.Status == 'Y') {
                        action = "View";
                        status = "Y";
                    }
                    else {
                        action = "Confrim";
                        status = "N";
                    }
                    return '<a href="javascript:void(0);" onclick="GetIdpayment(\'' + data.PaymentId + '\',\'' + status + '\')"><u>' + action + '</u></a>';
                },

            },
            { "data": "PaymentNo" },
            { "data": "Request_no" },
            {
                "data": null, render: function (data, type, row, meta) {
                    var strRequest_date =data.Request_date!=null? data.Request_date.substring(0, 10):"";
                    return strRequest_date;
                },
            },
            {
                "data": null, render: function (data, type, row, meta) {
                    var strPaymentDatetime = data.PaymentDatetime!=null?data.PaymentDatetime.substring(0, 10):"";
                    return strPaymentDatetime;
                },
            },
            {
                "data": null, render: function (data, type, row, meta) {
                    var datapaymentchannal = data.PaymentChannal!=null? GetpaymentchannalById(data.PaymentChannal):"";
                    return datapaymentchannal;
                },
            },
            { "data": "BankTransfer" },
            { "data": "AmountTransfer" },
            { "data": "ContactName" },
            { "data": "ContactPhoneNo" },
            {
                "data": null, render: function (data, type, row) {
                    if (data.Status == "Y") {
                        return "Paid";
                    }
                    else {
                        return "No Paid";
                    }
                }
            }
        ],
        "order": [[0, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }
    });
}

function getDropDownPaymentChannal() {
    $.ajax({
        type: "GET",
        url: "/Payment/paymentchannal",
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

    $.ajax({
        type: "POST",
        url: "/Payment/confirmpayment",
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

//function load_datapayment(data) {
//    var dataset = '';
//    $.each(data, function (index, payment) {

//        var Runningno = index + 1;
//        var statusonclick = '';
//        var status = '';
//        var action = '';
//        if (payment.status == "Y") {
//            status = "Paid";
//            action = "View";
//            statusonclick = "viewInsertpayment";
//        }
//        else {
//            status = "No Paid";
//            action = "Confirm";
//            statusonclick = "confirmInsertpayment";
//        }
//        dataset += '<tr>' +
//            '<td><a href="javascript:void(0);" onclick="GetIdpayment(\'' + payment.paymentId + '\',\'' + payment.status + '\')"><u>' + action + '</u></a></td>' +
//            '<td>' + Runningno + '</td>' +
//            '<td>' + payment.paymentId + '</td>' +
//            '<td>' + payment.request_no + '</td>' +
//            '<td>' + payment.request_date + '</td>' +
//            '<td>' + payment.paymentDatetime + '</td>' +
//            '<td>' + payment.paymentChannal + '</td>' +
//            '<td>' + payment.bankTransfer + '</td>' +
//            '<td>' + payment.amountTransfer + '</td>' +
//            '<td>' + payment.contactName + '</td>' +
//            '<td>' + payment.contactPhoneNo + '</td>' +
//            '<td>' + status + '</td>' +
//            '</tr>'
//    })
//    console.log(dataset);
//    $("#tbSearchconfirmpayment").append(dataset);


//}

function GetIdpayment(paymentId, status) {

    $.ajax({
        type: "GET",
        url: "/Payment/GetByPaymentId/?id=" + paymentId,
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {

            if (status == 'N') {
                SetTextStatusN(response.Data);
            }
            else if (status == 'Y') {
                SetTextStatusY(response.Data);
            }
        },
        failure: function (msg) {
        }
    });

 



}


function SetTextStatusN(data) {
    console.log(data);
    $("#ConfirmPayment").modal('show');
    //$("#btnconfirmpayment").show('slow');

    $('#btnClearDataModal').show();
    $('#btnconfirmpayment').show();

    $("#Request_no").text(data.Request_no);
    $("#Payment_no").text(data.PaymentNo);
    $("#Company_name").text(data.companyNameTh);
    $("#address").text(data.companyAddress);
    $("#Tax_id").text(data.taxId);
    $("#Contact_name").val(data.ContactName);
    $("#Contact_email").val(data.ContactEmail);
    $("#Contact_phone_no").val(data.ContactPhoneNo);
    document.getElementById("amount_transfer").style.backgroundColor = "white";
    Getpaymentchannal();
    GetDetailpaymentchannal();
    GetBankTransfer();

}

function SetTextStatusY(data) {
    console.log(data);
    $("#ConfirmPayment").modal('show');
    //$("#btnconfirmpayment").show('slow');
    $('#btnClearDataModal').hide();
    $('#btnconfirmpayment').hide();
    var textdropdrowpaymentchanel = GetpaymentchannalById(data.PaymentChannal);
    var textpaymentchannal = '<option value="">' + textdropdrowpaymentchanel + '</option>';
    var strpaymentDate = data.PaymentDatetime.substring(0, 10).split("-");
    var strpaymentTime = data.PaymentDatetime.substring(11, 16);
    var covertstrDate = strpaymentDate[2] + '/' + (strpaymentDate[1]) + '/' + strpaymentDate[0];

    $("#Request_no").text(data.Request_no);
    $("#Payment_no").text(data.PaymentNo);
    $("#Company_name").text(data.companyNameTh);
    $("#address").text(data.companyAddress);
    $("#Tax_id").text(data.taxId);

    $("#Contact_name").val(data.ContactName);
    $("#Contact_email").val(data.ContactEmail);
    $("#Contact_phone_no").val(data.ContactPhoneNo);
    $("#payment_channal option").remove();
    $('#payment_channal').append(textpaymentchannal);
    GetDetailpaymentchannal();

    //$("#textpayment_channal").val("0001");
    document.getElementById("amount_transfer").style.backgroundColor = "lightgray";
    $("#amount_transfer").val(data.AmountTransfer);
    $('#bank_transfer').append('<option value="' + data.BankTransfer + '">' + data.BankTransfer + ' </option>');
    $("#bank_branch").val(data.BankBranch);
   // $("#nameslip_attach_file").html(data.SlipAttachFile);
    $("#remark").val(data.Remark);


    $("#Contact_name").attr('disabled', 'disabled');
    $("#Contact_email").attr('disabled', 'disabled');
    $("#Contact_phone_no").attr('disabled', 'disabled');
    $("#payment_channal").attr('disabled', 'disabled');

    $("#textpayment_channal").attr('disabled', 'disabled');
    $("#Detailpaymentchannal input").attr('disabled', 'disabled');
    $('#payment_date').attr('disabled', 'disabled');
    $('#payment_datetime').prop('disabled', 'disabled');
    $("#amount_transfer").attr('disabled', 'disabled');
    $('#bank_transfer').attr('disabled', 'disabled');
    $("#bank_branch").attr('disabled', 'disabled');;
    $("#slip_attach_file").attr('disabled', 'disabled');
    $("#remark").attr('disabled', 'disabled');

}

function UpdatePayment(PaymentId) {
    var payment_date = document.querySelector("#payment_date")._flatpickr;
    var payment_datetime = document.querySelector("#payment_datetime")._flatpickr;

    var Datepayment_date = new Date(payment_date.selectedDates);
    var Datepayment_datetime = new Date(payment_datetime.selectedDates);

    var strpayment_date = Datepayment_date.getFullYear() + "-" + (Datepayment_date.getMonth() + 1) + "-" + Datepayment_date.getDate();
    var strpayment_datetime = Datepayment_datetime.toString().split(' ');


    var datetimeformat = strpayment_date + " " + strpayment_datetime[4];
    console.log(datetimeformat);

    var data = new FormData();
    data.append("PaymentId", PaymentId);
    data.append("Request_no", $('#Request_no').text());
    data.append("PaymentNo", $('#Payment_no').text());
    data.append("ContactName", $('#Contact_name').val());
    data.append("ContactEmail", $('#Contact_email').val());
    data.append("ContactPhoneNo", $('#Contact_phone_no').val());
    data.append("PaymentChannal", $('#payment_channal').val());
    data.append("PaymentDatetime", datetimeformat);
    data.append("AmountTransfer", $("#amount_transfer").val());
    data.append("BankTransfer", $('#bank_transfer').val());
    data.append("BankBranch", $('#bank_branch').val());
    data.append("FileSilp", $('#slip_attach_file').get(0).files[0]);
    data.append("Remark", $('#remark').val());
    data.append("Status", 'Y');
    $.ajax({
        type: "PUT",
        url: "/Payment/Updatepayment",
        data: data,
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {
            if (response.Status) {
                bootbox.alert({
                    message: "แจ้งโอนเงินสำเร็จ",
                    callback: function () {
                        $("#ConfirmPayment").hide('fast');
                        GetIdpayment(PaymentId, 'Y');
                    }
                })
            }
            else {
                bootbox.alert({
                    message: "แจ้งโอนเงินไม่สำเร็จ : " + response.Date,

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
    Getpaymentchannal();
    GetDetailpaymentchannal();
    $("payment_date").val('');
    $("payment_datetime").val('');
    $("#amount_transfer").val('');
    GetBankTransfer();
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

    $.ajax({
        type: "POST",
        url: "/Payment/BindDataTypeTransfer",
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {
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
    $.ajax({
        type: "GET",
        url: "/Payment/paymentchannal",
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

    $.ajax({
        type: "POST",
        url: "/Payment/GetDataBankPayment",
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {
            if (data != null) {
                $.each(data.response, function () {
                    var strtext = this.dropdown_text;
                    var strvalue = this.dropdown_value;
                    var str = "<div class='custom-control custom-radio custom-control-inline'>\
                        <input type='radio' class='custom-control-input inputValidation' name='optionsRadios' id='rd"+ strvalue + "' value='" + strvalue + "' checked required/>\
                        <label class='custom-control-label' for='rd"+ strvalue + "'>" + strtext + "</label></div >"
                    $('#Detailpaymentchannal').append(str);


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

    $.ajax({
        type: "POST",
        url: "/Payment/DDLBank",
        processData: false,
        contentType: false,
        async: false,
        success: function (response) {
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