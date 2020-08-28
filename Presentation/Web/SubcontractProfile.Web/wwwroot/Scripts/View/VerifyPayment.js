
var oTableSearch;

$(document).ready(function () {
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
        format: 'LT',
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

    inittbSearchResult();
    BindDataTypeTransfer();
    BindBankPayment();
    BindDDLBank();
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
        OnSave();
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

});

function inittbSearchResult() {

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
        "scrollX": true,
        "language": {
            "zeroRecords": "No data found.",
            "decimal": ",",
            "thousands": "."
        },
        ajax: {
            url: '/Payment/SearchVerify',
            type: "POST",
            datatype: "JSON",
            //dataSrc:"",
            data: function (d) {
                d.companyNameTh = $("#txtcompanyname").val(),
                    d.taxId = $("#txttaxid").val(),
                    //d.locationid = $('#ddlLocation option').filter(':selected').val(),
                    //d.teamid = $('#ddlTeam option').filter(':selected').val(),
                    d.PaymentNo = $('#txtpayment').val(),
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
            { "data": "PaymentNo", "visible": false },
            { "data": "companyNameTh", "width": "20%" },
            { "data": "taxId", "width": "20%" },
            //{ "data": "LocationNameTh", "width": "20%" },
            //{ "data": "TeamName", "width": "20%" },
            //{ "data": "StaffNameTh", "width": "20%" },
            { "data": "PaymentDatetime", "width": "20%" },
            { "data": "Status", "width": "20%" },
            { "data": "AmountTransfer", "width": "20%" }

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

function BindDataTypeTransfer() {

    $.ajax({
        type: "POST",
        url: "/Payment/BindDataTypeTransfer",
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

    $.ajax({
        type: "POST",
        url: "/Payment/GetDataBankPayment",
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $.each(data.response, function () {
                    var strtext = this.dropdown_text;
                    var strvalue = this.dropdown_value;
                    var str = "<div class='custom-control custom-radio custom-control-inline'>\
                        <input type='radio' class='custom-control-input inputValidation' name='optionsRadios' id='rd"+ strvalue + "' value='" + strvalue+"' checked required/>\
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

    $.ajax({
        type: "POST",
        url: "/Payment/DDLBank",

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
    $.ajax({
        url: '/Payment/GetDataById',
        type: 'POST',
        data: { 'paymentId': id },
        dataType: "json",
        success: function (result) {
            console.log(result)
            if (result != null) {
                $('#hdpaymentId').val(id);

                $("#lblcompanyName").text(result.companyNameTh);
                $('#lbpaymentno').text(result.PaymentNo);
                $('#ddlpaymenttransfer').val(result.PaymentChannal);

                var d = new Date(result.PaymentDatetime);
                var month = d.getMonth();
                var day = d.getDate();
                var year = d.getFullYear();

                $('#dateverify').val(result.PaymentDatetime);

                $('#timepayverify').val(result.PaymentDatetime);
                $('#txtBalance').val(result.AmountTransfer);
                $('#ddlbank').val(result.BankTransfer);
                $('#txtbranch').val(result.BankBranch);

                if (result.SlipAttachFile != null) {
                    $('#lbuploadslip').html(result.SlipAttachFile);
                    $('#hdupfiletransfer').val(result.file_id_Slip);
                    $('#linkdownload').text(result.SlipAttachFile);
                }
                $('#txtremark').val(result.Remark);

                var channel = result.PaymentChannal
                $(':radio').each(function (i) {

                    if (channel == $(this).val()) {
                        $(this).prop('checked', true);
                    }
                });
 
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
    $.ajax({
        url: '/Payment/SaveVerify',
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
            }
            bootbox.confirm({
                title: "System Information",
                message: result.Message,
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Cancel'
                    }
                },
                callback: function (result) {
                    console.log('This was logged in the callback: ' + result);
                }
            });
        },
        error: function (xhr, status, error) {
            bootbox.confirm({
                title: "System Information",
                message: "This action is not available.",
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Cancel'
                    }
                },
                callback: function (result) {
                    console.log('This was logged in the callback: ' + result);
                }
            });
        }

    });
}

function DownloadFileSlip() {
    $.ajax({
        url: '/Payment/GetBlobDownload',
        type: 'POST',
        data: {
            'paymentid': $('#hdpaymentId').val()
        },
        dataType: "json",
        contentType: "application/download",
        success: function (result) {

        },
        error: function (xhr, status, error) {
            bootbox.confirm({
                title: "System Information",
                message: "This action is not available.",
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Cancel'
                    }
                },
                callback: function (result) {
                    console.log('This was logged in the callback: ' + result);
                }
            });
        }

    });
}

//function GetDropDownLocation() {
//    $.ajax({
//        type: 'POST',
//        url: '/Payment/GetLocationByCompany',
//        data: { companyid: $('#txtcompanyname').val() },
//        dataType: 'json',
//        success: function (data) {
//            $("#ddlLocation").empty();

//            $.each(data, function () {
//                $("#ddlLocation").append($("<option></option>").val(this.Value).text(this.Text));
//            });

//        },
//        failure: function () {

//        }
//    });
//}

//function BindDDLTeam(location) {

//    $.ajax({
//        type: "POST",
//        url: "/Payment/DDLTeam",
//        data: {
//            companyid: $('#hdCompanyId').val(),
//            locationId: location
//        },
//        dataType: "json",
//        success: function (data) {

//            if (data != null) {
//                $('#ddlTeam').empty();

//                $.each(data.response, function () {
//                    $('#ddlTeam').append($("<option></option>").val(this.Value).text(this.Text));
//                });
//            }


//        },
//        error: function (xhr, status, error) {

//            //clearForEdit();
//            console.log(status);
//            bootbox.confirm({
//                title: "System Information",
//                message: "This action is not available.",
//                buttons: {
//                    cancel: {
//                        label: '<i class="fa fa-times"></i> Cancel'
//                    }
//                },
//                callback: function (result) {
//                    console.log('This was logged in the callback: ' + result);
//                }
//            });
//        }
//    });
//}