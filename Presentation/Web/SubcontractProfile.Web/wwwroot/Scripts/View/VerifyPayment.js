

var oTableAddress;

$(document).ready(function () {
    $('#paymentdatefrom').datepicker({
        format: 'dd/mm/yyyy',
        daysOfWeekHighlighted: "6,0",
        autoclose: true,
        todayHighlight: true,
        clearBtn: true,
    });
    $('#paymentdatefto').datepicker({
        format: 'dd/mm/yyyy',
        daysOfWeekHighlighted: "6,0",
        autoclose: true,
        todayHighlight: true,
        clearBtn: true,
    });

    inittbSearchResult();
    //GetDropDownLocation();
    //BindDDLTeam();

    $('#btnSearch').click(function () {
        inittbSearchResult();
    });
});

function inittbSearchResult() {

    oTable = $('#tbSearchResult').DataTable({
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
                    return "<a href='#' onclick=openModal('edit','" + row.PaymentId + "');return false;  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='แก้ไข'><svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-edit'><path d='M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7'></path><path d='M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z'></path></svg></a>";
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