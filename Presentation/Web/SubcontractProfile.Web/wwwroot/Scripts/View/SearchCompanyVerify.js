var oTable;
var url = null;


$(document).ready(function () {
    url = $("#controllername").data("url");
   // BindDDLStatus();
    inittbSearchResult();

    $('#regisdatefrom').datetimepicker({
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
    $('#regisdateto').datetimepicker({
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

    $("#regisdatefrom").on("dp.change", function (e) {
        $('#regisdateto').data("DateTimePicker").minDate(e.date);
    });
    $("#regisdateto").on("dp.change", function (e) {

        $('#regisdatefrom').data("DateTimePicker").maxDate(e.date);
    });


    $('#btnclear').click(function () {

        $('#txttaxid').val('');
        $('#txtcompanyname').val('');
        $('#ddldistribution').val('');
        $('#ddlchannelsalegroup').val('');

        $('#dateregisfrom').val('');
        //$('#regisdatefrom').data("DateTimePicker").date(moment(new Date(), 'DD/MM/YYYY'));
        $('#dateregisto').val('');
       // $('#regisdateto').data("DateTimePicker").date(moment(new Date(), 'DD/MM/YYYY'));
        $('#txtvendercode').val('');
        inittbSearchResult();

    });
    $('#btnSearch').click(function () {
        inittbSearchResult();
        //var dd = $("#dateregisfrom").val()
        //console.log(dd);
    });
    var urlGetCompany = url.replace('Action', 'GetCompany');
    $('#txtcompanyname').autocomplete({
        //lookup:
            //fruits,
        lookup:function (query, done) {
            $.ajax({
                type: "POST",
                url: urlGetCompany,
                data: { companyname: query },
                dataType: "json",
                async: false,
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
                    console.log(xhr);
                }
            });
        },
        minChars: 1,
        onSelect: function (suggestion) {
            $('#selection').html('You selected: ' + suggestion.value);
        },
        showNoSuggestionNotice: true,
        noSuggestionNotice: 'Sorry, no matching results',
    });

    $('#btnverifycompany').click(function () {
        var company = $('#tbSearchComVerifyResult tr input:radio[name="optionsRadios"]:checked').val();
        if (company != undefined && company != "") {
            var urlRedir = url.replace('Action', 'CompanyVerify');
            window.location.href = urlRedir+'?companyid=' + company;
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


    $('#txttaxid').keyup(function () {
        CheckKeyUps("txttaxid", "[0-9]");
    });




    function BindDDLStatus() {
        var urlDDLStatus = url.replace('Action', 'DDLStatus');
        $.ajax({
            type: "POST",
            url: urlDDLStatus,
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
    function inittbSearchResult() {
        var urlSearch = url.replace('Action', 'Search');
        oTable = $('#tbSearchComVerifyResult').DataTable({
            processing: true, // for show progress bar
            serverSide: true, // for process server side
            //filter: false, // this is for disable filter (search box)
            //orderMulti: true, // for disable multiple column at once
            pageLength: 10,
            order: [],
            //autoWidth: true,
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
                url: urlSearch,
                type: "POST",
                datatype: "JSON",
                //dataSrc:"",
                data: function (d) {
                    var type = "";
                    if ($('#rdoCustomerTypeAll').is(":checked")) {
                        type = "All";
                    }
                    else if ($('#rdoCustomerTypeSub').is(":checked")) {
                        type = $("#rdoCustomerTypeSub").val();
                    }
                    else if ($('#rdoCustomerTypeDealer').is(":checked")) {
                        type = $('#rdoCustomerTypeDealer').val();
                    }
                    d.SubcontractProfileType = type,
                        d.TaxId = $("#txttaxid").val(),
                        d.CompanyName = $("#txtcompanyname").val(),
                        //d.DistributionChannel = $('#ddldistribution option').filter(':selected').val(),
                        //d.ChannelSaleGroup = $('#ddlchannelsalegroup option').filter(':selected').val(),
                        d.RegisterDateFrom = $("#dateregisfrom").val(),
                        d.RegisterDateTo = $("#dateregisto").val(),
                        d.VendorCode = $("#txtvendercode").val(),
                        d.Status = $('#ddlStatus option').filter(':selected').val()
                }
            },
            columns: [

                {
                    data: null, className: "text-center", render: function (data, type, row) {
                        return "<input type='radio' class='checkbox-column' name='optionsRadios' id='chktype' value='" + row.CompanyId + "'>";
                    }
                    // orderable: false,
                    //className: 'text-center'
                },
                { "data": "CompanyId", "visible": false },        
                {
                    data: null, render: function (data, type, row) {
                        return "<span class='badge outline-badge-secondary shadow-none'>" + row.Status + "</span>";
                    }

                },
                { "data": "SubcontractProfileType" },
                { "data": "TaxId" },
                { "data": "CompanyNameTh"},
                { "data": "CompanyNameEn"},
                { "data": "VendorCode" },
            
                {
                    "data": "RegisterDate", render: function (data) {
                        var strCreateDate = "";
                        if (data != null) {
                            var date = new Date(data);
                            var sMonth = padValue(date.getMonth() + 1);
                            var sDay = padValue(date.getDate());
                            var sYear = date.getFullYear();
                            var sHour = date.getHours();
                            var sMinute = padValue(date.getMinutes());
                            //var sAMPM = "AM";

                            strCreateDate = sDay + '/' + sMonth + '/' + sYear + ' ' + sHour + ':' + sMinute;
                        }
                        return strCreateDate;
                    }
                },
                {
                    "data": "verified_date", render: function (data) {
                        var strCreateDate = "";
                        if (data != null) {
                            var date = new Date(data);
                            var sMonth = padValue(date.getMonth() + 1);
                            var sDay = padValue(date.getDate());
                            var sYear = date.getFullYear();
                            var sHour = date.getHours();
                            var sMinute = padValue(date.getMinutes());
                            //var sAMPM = "AM";

                            strCreateDate = sDay + '/' + sMonth + '/' + sYear + ' ' + sHour + ':' + sMinute;
                        }
                        return strCreateDate;
                    } }
                //,{ "data": "UpdateBy" }

            ],
            "order": [[0, "desc"]],
            "stripeClasses": [],
            drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

        });

    }
});

function padValue(value) {
    return (value < 10) ? "0" + value : value;
}

function CheckKeyUps(id, RexStr) {
    var strKey = $("#" + id).val();
    var strBuilder = "";
    var filter = new RegExp(RexStr);
    for (var i = 0; i < strKey.length; i++) {
        if (filter.test(strKey.substr(i, 1))) {
            strBuilder += strKey.substr(i, 1);
        }
    }
    $("#" + id).val(strBuilder);
}