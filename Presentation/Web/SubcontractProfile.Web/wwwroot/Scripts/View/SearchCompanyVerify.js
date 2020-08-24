var oTable;



$(document).ready(function () {
    BindDDLStatus();
    inittbSearchResult();
    $('#btnSearch').click(function () {
        inittbSearchResult();

    });

    $('#btnverifycompany').click(function () {
        var company = $('#tbSearchComVerifyResult tr input:radio[name="optionsRadios"]:checked').val();
        if (company != "") {
            window.location.href = '/Registration/CompanyVerify?companyid=' + company;
        }
        else {
            bootbox.confirm({
                title: "System Information",
                message: "Please select company.",
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
    function BindDDLStatus() {

        $.ajax({
            type: "POST",
            url: "/Registration/DDLStatus",
            dataType: "json",
            success: function (data) {

                if (data != null) {
                    $('#ddlStatus').empty();

                    $.each(data.response, function () {
                        $('#ddlStatus').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                    });
                }


            },
            error: function (xhr, status, error) {
                console.log(xhr);
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
    function inittbSearchResult() {

        oTable = $('#tbSearchComVerifyResult').DataTable({
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
                url: '/Registration/Search',
                type: "POST",
                datatype: "JSON",
                //dataSrc:"",
                data: function (d) {
                    d.SubcontractProfileType = $("#inputLocationCodeMain").val(),
                        d.TaxId = $("#txttaxid").val(),
                        d.CompanyName = $("#txtcompanyname").val(),
                        d.DistributionChannel = $('#ddldistribution option').filter(':selected').val(),
                        d.ChannelSaleGroup = $('#ddlchannelsalegroup option').filter(':selected').val(),
                        d.RegisterDateFrom = $("#regisdatefrom").val(),
                        d.RegisterDateTo = $("#regisdateto").val(),
                        d.VendorCode = $("#txtvendercode").val(),
                        d.Status = $('#ddlStatus option').filter(':selected').val()
                }
            },
            columns: [

                {
                    data: null, width: "10%", className: "text-center", render: function (data, type, row) {
                        return "<input type='radio' class='' name='optionsRadios' id='chktype' value='" + row.CompanyId + "'>";
                    }
                    // orderable: false,
                    //className: 'text-center'
                },
                { "data": "CompanyId", "visible": false },
                { "data": "Status", "width": "20%" },
                { "data": "SubcontractProfileType", "width": "20%" },
                { "data": "TaxId", "width": "20%" },
                { "data": "CompanyNameTh", "width": "20%" },
                { "data": "CompanyNameEn", "width": "20%" },
                { "data": "VendorCode", "width": "20%" },
                { "data": "DistributionChannel", "width": "20%" },
                { "data": "ChannelSaleGroup", "width": "20%" },
                { "data": "RegisterDate", "width": "20%" },
                { "data": "CreateDate", "width": "20%" },
                { "data": "CreateBy", "width": "20%" }

            ],
            "order": [[0, "desc"]],
            "stripeClasses": [],
            drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

        });

    }
});