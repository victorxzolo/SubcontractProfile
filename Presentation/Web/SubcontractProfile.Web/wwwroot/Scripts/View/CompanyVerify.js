

var oTableAddress;
var tbRevenue;
var tbLocation;

var otbtablocation;
var otbtabteam;
var otbtabengineer;

$(document).ready(function () {
 
   
    inittbAddressResult();
    inittbRevenue();
    inittblocation();
    BindDDLTitle();
    BindRegion();
    BindDDLBank();
    BindDDLprovince();
    BindDDLdistrict();
    BindDDLsubdistrict();
    //BindAddressType();
    getDataById($('#hdCompanyId').val());

    inittbtablocation();
    inittbtabTeam();
    inittbtabengineer();


    $('#contractstartdate').datepicker({
            format: 'dd/mm/yyyy',
            daysOfWeekHighlighted: "6,0",
            autoclose: true,
            todayHighlight: true,
            clearBtn: true,
        });
    $('#contractenddate').datepicker({
            format: 'dd/mm/yyyy',
            daysOfWeekHighlighted: "6,0",
            autoclose: true,
            todayHighlight: true,
            clearBtn: true,
        });

    $('#rdoCompanyType1').on("change", function () {
        if ($(this).attr("value") == "NewSubContract") {
            $("#divdealer").hide('slow');
            $('#divnewsubcontract').show('slow');
            $('#hdrdtype').val($(this).attr("value"));
        }
    });

    $('#rdoCompanyType2').on("change", function () {
        if ($(this).attr("value") == "Dealer") {
            $("#divnewsubcontract").hide('slow');
            $("#divdealer").show('slow');
            $('#hdrdtype').val($(this).attr("value"));
        }
    });
    //Location
    $('#btnsearchlocation').click(function () {
        $('#Searchlocation').modal('show');
        $('#txtlocationcodemodal').val($('#txtlocationcode').val());
        $("#btn_search_modal").trigger("click");
    });
    $('#btn_search_modal').click(function () {

        $.ajax({
            url: "/Registration/SearchLocation",
            type: "POST",
            data: {
                asc_code: function () { return $('#txtasccodemodal').val() },
                asc_mobile_no: function () { return $('#txtmobilenomodal').val() },
                id_Number: function () { return $('#txtidnumbermodal').val() },
                location_code: function () { return $('#txtlocationcodemodal').val() },
                sap_code: function () { return $('#txtsapcodemodal').val() },
                user_id: function () { return $('#txtuseridmodal').val() }
            },
            datatype: "JSON",
        }).done(function (data) {
            tbLocation.clear().draw();
            tbLocation.rows.add(data.data).draw();
        }).fail(function (xhr, status, error) {
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
        });

    });
    $('#btn_reset_modal').click(function () {
        ClearDataModalLocation();
    });
    $('#tblocationModal').on('click', 'tbody .select_btn', function () {

        var value = tbLocation.row($(this).closest('tr')).data();
        var lo_id = value.location_id;
        $.ajax({
            type: "POST",
            url: "/Registration/GetLocationSession",
            dataType: "json",
            data: { location_id: lo_id },
            success: function (data) {

                if (data.Response.Status) {
                    $('#txtlocationcode').val(data.LocationListModel[0].outLocationCode);
                    $('#txtlocationname').val(data.LocationListModel[0].outLocationName);
                    $('#txtdistribution').val(data.LocationListModel[0].outDistChn);
                    $('#txtchannelsalegroup').val(data.LocationListModel[0].outChnSales);
                    $('#txttax_id_dealer').val(data.LocationListModel[0].outTaxId);
                    $('#txtcompany_alias_dealer').val(data.LocationListModel[0].outCompanyShortName);


                    $("#ddlprefixcompany_name_th_dealer option").filter(function () {
                        return $(this).text() == data.LocationListModel[0].outTitle;
                    }).prop('selected', true);


                    $('#txtcompany_name_th_dealer').val(data.LocationListModel[0].outCompanyName);



                    $("#ddlprefixcompany_name_en_dealer option").filter(function () {
                        return $(this).text() == data.LocationListModel[0].outTitle;
                    }).prop('selected', true);


                    $('#txtcompany_name_en_dealer').val();
                    $('#txtwt_name_dealer').val(data.LocationListModel[0].outWTName);

                    if (data.LocationListModel[0].outVatType == "VAT") {
                        $('#chkvat_typeT_dealer').prop('checked', true);
                    }
                    else if (data.LocationListModel[0].outVatType == "NON_VAT") {
                        $('#chkvat_typeE_dealer').prop('checked', true);
                    }
                }

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



        ClearDataModalLocation();
        $('#Searchlocation').modal('hide');

    });

    //Revenue
    $('#btnsearchrevenue').click(function () {
        $('#SearchRevenue').modal('show');
        $('#txtsearchrevenue').val($('#inputTax_id').val());
        $("#btn_search_revenue_modal").trigger("click");
    });
    $('#btn_reset_revenue_modal').click(function () {
        ClearDataModalRevenue()
    });
    $('#btn_search_revenue_modal').click(function () {
        //tbRevenue.ajax.reload();
        $.ajax({
            url: "/Registration/GetRevenue",
            type: "POST",
            data: {
                tIN: function () {
                    return $('#txtsearchrevenue').val()
                }
            },
            datatype: "JSON",
        }).done(function (data) {
            tbRevenue.clear().draw();
            tbRevenue.rows.add(data.data).draw();
        }).fail(function (xhr, status, error) {
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
        });

    });
    $('#tbrevenueModal').on('click', 'tbody .select_btn', function () {
        var value = tbRevenue.row($(this).closest('tr')).data();
        var titlename = value.vtitleName;
        var companyname = value.vName;

        $('#inputCompany_name_th').val(companyname);
        $("#ddlprefixcompany_name_th option").filter(function () {
            //may want to use $.trim in here
            return $(this).text() == titlename;
        }).prop('selected', true);
        ClearDataModalRevenue();
        $('#SearchRevenue').modal('hide');
    });

    $('#tbAddressResult').on('click', 'tbody .edit_btn', function () {
        var data_row = oTableAddress.row($(this).closest('tr')).data();
        oTableAddress.row($(this).closest('tr')).remove().draw();//เดี๋ยวกลับมาทำ

        $.ajax({
            type: "POST",
            url: "/Registration/GetAddressById",
            data: { addressID: data_row.addressId },
            dataType: "json",
            success: function (data) {
                $('#ddlcountry').val('')

                $('#ddlprovince').val('')

                $('#ddldistrict').val('')

                $('#ddlsubdistrict').val('')

                $('#ddlzipcode').val('')

                $('#ddlzone').val('')

                $('#txthomenumber').val('')
                $('#txtVillageNo').val('')
                $('#txtvillage').val('')
                $('#txtbuilding').val('')
                $('#txtfloor').val('')
                $('#txtroom').val('')
                $('#txtsoi').val('')
                $('#txtroad').val('')


                $(':checkbox:checked').each(function (i) {
                    $(this).prop('checked', false);
                });


                if (data.status) {
                    console.log(data.response);


                    $('#ddlcountry').val(data.response.Country)

                    $('#ddlprovince').val(data.response.ProvinceId)
                    //$('#ddlprovince').trigger('change');

                    $('#ddldistrict').val(data.response.DistrictId)
                    // $('#ddldistrict').trigger('change');

                    $('#ddlsubdistrict').val(data.response.SubDistrictId)

                    $('#ddlzipcode').val(data.response.ZipCode)

                    $('#ddlzone').val(data.response.RegionId)

                    $('#txthomenumber').val(data.response.HouseNo)
                    $('#txtVillageNo').val(data.response.Moo)
                    $('#txtvillage').val(data.response.VillageName)
                    $('#txtbuilding').val(data.response.Building)
                    $('#txtfloor').val(data.response.Floor)
                    $('#txtroom').val(data.response.RoomNo)
                    $('#txtsoi').val(data.response.Soi)
                    $('#txtroad').val(data.response.Road)

                    var addr_type_id = data.response.AddressTypeId
                    $(':checkbox').each(function (i) {

                        if (addr_type_id == $(this).val()) {
                            $(this).prop('checked', true);
                        }
                    });
                }
                else {
                    bootbox.confirm({
                        title: "System Information",
                        message: data.message,
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
            },
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
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
    });

    $('#tbAddressResult').on('click', 'tbody .delete_btn', function () {
        var data_row = oTableAddress.row($(this).closest('tr')).data();
        $.ajax({
            type: "POST",
            url: "/Registration/DeleteDaftAddress",
            data: { AddressId: data_row.addressId },
            dataType: "json",
            success: function (data) {
                if (data.status) {
                    var val = []
                    val = ConcatstrAddress(data.response);
                    oTableAddress.clear();
                    BindDatatable(oTableAddress, val);
                }
                else {
                    bootbox.confirm({
                        title: "System Information",
                        message: data.message,
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
            },
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
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
    });

    $('#btnAdd').click(function () {
        var stuff = [];


        $(':checkbox:checked').each(function (i) {

            //val[i] = $(this).parent().text().trim();

            var data = {
                AddressTypeId: $(this).val(),
                address_type_name: $(this).parent().text().trim(),
                Country: $('#country option').filter(':selected').val(),
                ZipCode: $('#ddlzipcode option').filter(':selected').val(),
                HouseNo: $('#txthomenumber').val(),
                Moo: $('#txtVillageNo').val(),
                VillageName: $('#txtvillage').val(),
                Building: $('#txtbuilding').val(),
                Floor: $('#txtfloor').val(),
                RoomNo: $('#txtroom').val(),
                Soi: $('#txtsoi').val(),
                Road: $('#txtroad').val(),
                SubDistrictId: $('#ddlsubdistrict option').filter(':selected').val(),
                sub_district_name: $('#ddlsubdistrict option').filter(':selected').text(),
                DistrictId: $('#ddldistrict option').filter(':selected').val(),
                district_name: $('#ddldistrict option').filter(':selected').text(),
                ProvinceId: $('#ddlprovince option').filter(':selected').val(),
                province_name: $('#ddlprovince option').filter(':selected').text(),
                RegionId: $('#ddlzone option').filter(':selected').val(),
                location_code: $('#txtlocationcode').val(),
                CompanyId: $('#hdCompanyId').val()
            }
            stuff.push(data);


        });

        SaveDaftAddress(stuff);
    });
    $('#btnClear').click(function () {
        $('#txthomenumber').val('')
        $('#txtVillageNo').val('')
        $('#txtvillage').val('')
        $('#txtbuilding').val('')
        $('#txtfloor').val('')
        $('#txtroom').val('')
        $('#txtsoi').val('')
        $('#txtroad').val('')
        $('#ddlcountry').val('')
        $('#ddlzone').val('')
        $('#ddlprovince').val('')
        $('#ddldistrict').val('')
        $('#ddlsubdistrict').val('')
        $('#ddlzipcode').val('');

        $('#chkAddressType input[type=checkbox]').each(function () {
            $(this).prop('checked', false);

        });
    });


    //Upload file
    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });
    $('#inputUploadcertificate').change(function () {

        uploadFiles('inputUploadcertificate')
    });

    $('#inputUploadComRegis').change(function () {
        uploadFiles('inputUploadComRegis')
    });

    $('#inputUpload20').change(function () {
        uploadFiles('inputUpload20')
    });
    $('#inputuploadbookbank').change(function () {
        uploadFiles('inputuploadbookbank')
    });

    $('#btnback').click(function () {
        window.location.href = '/Registration/SearchCompanyVerify';
    });
    $('#btnnotapprove').click(function () {

        var Error = validateform();

        if (!Error) {
            onSaveCompanyProfile("NotApprove");
        }

        

    });
    $('#btnapprove').click(function () {
        var Error = validateform();

        if (!Error) {
            onSaveCompanyProfile("Approve");
        }

        

    });


    //Tab Location
    //$('#location-tab').click(function () {
    //    otbtablocation.ajax.reload();
    //});


    BindDDLLocationTeam();
    //Tab Team
    $('#team-tab').click(function () {
       // otbtabteam.ajax.reload();
        $('#lbteamcompanyname').text($('#lbcompanyname').text());
    });
    $('#ddlteamlocation').change(function () {
        otbtabteam.ajax.reload();
    });

    //Tab Engineer
    $('#engineer-tab').click(function () {
        $('#lbengineercompanyname').text($('#lbcompanyname').text());
    });
    BindDDLTeamEngineer();
    $('#ddllocationteamengineer').change(function () {
      var data=  $('#ddllocationteamengineer option').filter(':selected').val()
        BindDDLTeamEngineer(data);
        otbtabengineer.ajax.reload();
    });
    $('#ddlteamengineer').change(function () {
        otbtabengineer.ajax.reload();
    });
});


function onSaveCompanyProfile(status) {
    var chksubcontract_type = null;
    var distribution_channel = null;
    var channel_sale_group = null;
    var tax_id = null;
    var company_alias = null;
    var company_title_name_th = null;
    var company_title_name_en = null;
    var company_name_th = null;
    var company_name_en = null;
    var wt_name = null;
    var vat_type = null;
    var company = new Object();

    if ($("#rdoCompanyType1").is(":checked")) {
        chksubcontract_type = $("#rdoCompanyType1").val();
        //distribution_channel = $('#ddldistribution option').filter(':selected').val();
        //channel_sale_group = $('#ddlchannelsalegroup option').filter(':selected').val();

        tax_id = $('#inputTax_id').val();
        company_alias = $('#inputCompany_alias').val();

        company_title_name_th = $('#ddlprefixcompany_name_th option').filter(':selected').val();
        company_name_th = $('#inputCompany_name_th').val();

        company_title_name_en = $('#ddlprefixcompany_name_en option').filter(':selected').val();
        company_name_en = $('#inputCompany_name_en').val();

        wt_name = $('#inputWT_name').val();
        vat_type = $('#chkvat_typeT').is(':checked') ? $('#chkvat_typeT').val() : $('#chkvat_typeE').val();

    }
    else if ($("#rdoCompanyType2").is(":checked")) {
        chksubcontract_type = $("#rdoCompanyType2").val();
        distribution_channel = $('#txtdistribution').val();
        channel_sale_group = $('#txtchannelsalegroup').val();
        tax_id = $('#txttax_id_dealer').val();
        company_alias = $('#txtcompany_alias_dealer').val();
        company_title_name_th = $('#ddlprefixcompany_name_th_dealer option').filter(':selected').val();
        company_name_th = $('#txtcompany_name_th_dealer').val();
        company_title_name_en = $('#ddlprefixcompany_name_en_dealer option').filter(':selected').val();
        company_name_en = $('#txtcompany_name_en_dealer').val();
        wt_name = $('#txtwt_name_dealer').val();
        vat_type = $('#chkvat_typeT_dealer').is(':checked') ? $('#chkvat_typeT_dealer').val() : $('#chkvat_typeE_dealer').val();
    }

    company.CompanyNameTh = company_name_th;
    company.CompanyNameEn = company_name_en;
    company.CompanyAlias = company_alias;
    company.DistributionChannel = distribution_channel;
    company.ChannelSaleGroup = channel_sale_group;
    company.TaxId = tax_id;
    company.WtName = wt_name;
    company.VatType = vat_type;
    company.CompanyEmail = $('#mailCompany').val();
    company.ContractName = $('#nameContract').val();
    company.ContractPhone = $('#telContract').val();
    company.ContractEmail = $('#mailContract').val();

    company.BankCode = $('#selBankName option').filter(':selected').val()
    company.BankName = $('#txtbank_Name').val();
    company.AccountNumber = $('#codeNumber').val();
    company.AccountName = $('#busiName').val();

    //company.BranchCode= $('#txtbranch_Code').val();
    company.BranchName = $('#nameBranch').val();

    company.DeptOfInstallName = $('#name1').val();
    company.DeptOfMaintenName = $('#name2').val();
    company.DeptOfAccountName = $('#name3').val();

    company.DeptOfInstallPhone = $('#tel1').val();
    company.DeptOfMaintenPhone = $('#tel2').val();
    company.DeptOfAccountPhone = $('#tel3').val();

    company.DeptOfInstallEmail = $('#mail1').val();
    company.DeptOfMaintenEmail = $('#mail2').val();
    company.DeptOfAccountEmail = $('#mail3').val();

    company.LocationCode = $('#txtlocationcode').val();
    company.LocationNameTh = $('#txtlocationname').val();
    company.LocationNameEn = $('#txtlocationname').val();

    company.BankAccountTypeId = $('#AccType option').filter(':selected').val();
    company.SubcontractProfileType = chksubcontract_type;
    company.CompanyTitleThId = company_title_name_th;
    company.CompanyTitleEnId = company_title_name_en;
    company.CompanyId = $('#hdCompanyId').val();
    //company.ContractStartDate = $('#datecontractstart').val();
    //company.ContractEndDate = $('#datecontractend').val();

    console.log(company);
    $.ajax({
        url: '/Registration/OnSave',
        type: 'POST',
        data: {
            'model': company,
            'status': status,
            'contractstart': $('#datecontractstart').val(),
            'contractend': $('#datecontractend').val()
        },
        dataType: "json",
        success: function (result) {
            if (result.Response.Status) {
                bootbox.alert(result.Response.Message);
            }
            else {
                bootbox.alert(result.Response.Message);

            }
        },
        error: function (xhr, status, error) {
            bootbox.alert(xhr.responseText);
        }

    });

}


function ClearDataModalLocation() {
    $('#txtasccodemodal').val('')
    $('#txtmobilenomodal').val('')
    $('#txtlocationcodemodal').val('')
    $('#txtidnumbermodal').val('')
    $('#txtsapcodemodal').val('')
    $('#txtuseridmodal').val('')
    tbLocation.clear().draw();
}

function ClearDataModalRevenue() {
    $('#txtsearchrevenue').val('');
    tbRevenue.clear().draw();
}

function BindDatatable(table, datamodel) {
    //console.log("BindDatatable");
    //console.log(datamodel);
    //table.data(datamodel);

    table.rows.add(datamodel).draw();

}

function inittbAddressResult(companyId) {
    oTableAddress = $('#tbAddressResult').DataTable({
        processing: true, // for show progress bar
        //serverSide: true, // for process server side
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
        columns: [


            { "data": "addressId", "visible": false },
            { "data": "address_type_id", "visible": false },
            { "data": "address_type", orderable: true, },
            { "data": "address", orderable: true, },
            {
                data: null, width: "10%", render: function (data, type, row) {
                    return "<a href='#'class='edit_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='แก้ไข'><svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-edit'><path d='M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7'></path><path d='M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z'></path></svg></a>";
                }
            },
            {
                data: null, width: "10%", render: function (data, type, row) {
                    return "<a href='#' class='delete_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='ลบ'><svg xmlns='http://www.w3.org/2000/svg'width='24' height='24' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg); transform: rotate(360deg);' preserveAspectRatio='xMidYMid meet' viewBox='0 0 24 24'><g fill='none' stroke='#626262' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><path d='M3 6h18'/><path d='M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2'/></g></svg></a>";
                }
            },
        ],
        "order": [[1, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

    });
}

function inittblocation() {
    tbLocation = $('#tblocationModal').DataTable({
        processing: true, // for show progress bar
        //serverSide: true, // for process server side
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
        data: [],
        columns: [
            { "data": "outCompanyName" },
            { "data": "outCompanyShortName" },
            { "data": "outTaxId" },
            { "data": "outLocationCode" },
            { "data": "outLocationName" },
            { "data": "outDistChn" },
            { "data": "outChnSales" },
            { "data": "location_id", "visible": false },
            {
                data: null, width: "10%", render: function (data, type, row) {
                    return "<a href='#'class='select_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='เลือก'><svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg);\
                             transform: rotate(360deg); 'preserveAspectRatio='xMidYMid meet' viewBox='0 0 24 24'>\
                                 <g fill = 'none' stroke = '#626262' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><path d='M20 6L9 17l-5-5' /></g></svg></a>";
                }
            }
        ],
        "order": [[1, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }
    });
}

function inittbRevenue() {
    tbRevenue = $('#tbrevenueModal').DataTable({
        processing: true, // for show progress bar
        //serverSide: true, // for process server side
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
        data: [],
        columns: [

            { "data": "vNID" },
            { "data": "vName" },
            { "data": "vBranchName" },
            { "data": "outConcataddr" },
            { "data": "vtitleName", "visible": false },
            {
                data: null, width: "10%", render: function (data, type, row) {
                    return "<a href='#'class='select_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='เลือก'><svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg);\
                             transform: rotate(360deg); 'preserveAspectRatio='xMidYMid meet' viewBox='0 0 24 24'>\
                                 <g fill = 'none' stroke = '#626262' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><path d='M20 6L9 17l-5-5' /></g></svg></a>";
                }
            }

        ],
        "order": [[1, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }
    });
}

function getDataById(companyId) {
    $.ajax({
        url: '/Registration/GetDataById',
        type: 'POST',
        data: { 'companyId': companyId },
        dataType: "json",
        success: function (result) {
            console.log(result)
            if (result != null) {
                $('#hdCompanyId').val(result.CompanyId);
                $("#inputCompany_alias").val(result.CompanyAlias);
                $("#inputCompany_name_th").val(result.CompanyNameTh);

                if (result.SubcontractProfileType == "NewSubContract") {
                    $('#rdoCompanyType1').prop('checked', true);
                    $('#hdrdtype').val(result.SubcontractProfileType);
                    $('#rdoCompanyType1').trigger('change');
                }
                else if (result.SubcontractProfileType == "Dealer") {
                    $('#rdoCompanyType2').prop('checked', true);
                    $('#hdrdtype').val(result.SubcontractProfileType);
                    $('#rdoCompanyType2').trigger('change');
                }

                $('#inputTax_id').val(result.TaxId);
                $('#inputCompany_alias').val(result.CompanyAlias);

                $('#ddlprefixcompany_name_th').val(result.CompanyTitleThId);
                $('#inputCompany_name_th').val(result.CompanyNameTh);

                $('#ddlprefixcompany_name_en').val(result.CompanyTitleEnId);
                $('#inputCompany_name_en').val(result.CompanyNameEn);

                $('#inputWT_name').val(result.WtName);

                if (result.VatType == "VAT") {
                    $('#chkvat_typeT').prop('checked', true);
                }
                else if (result.VatType == "NON_VAT") {
                    $('#chkvat_typeE').prop('checked', true);
                }

                //contract
                $('#mailCompany').val(result.CompanyEmail);
                $('#nameContract').val(result.ContractName);
                $('#mailContract').val(result.ContractEmail);
                $('#telContract').val(result.ContractPhone);

                $('#name1').val(result.DeptOfInstallName);
                $('#tel1').val(result.DeptOfInstallPhone);
                $('#mail1').val(result.DeptOfInstallEmail);

                $('#name2').val(result.DeptOfMaintenName);
                $('#tel2').val(result.DeptOfMaintenPhone);
                $('#mail2').val(result.DeptOfMaintenEmail);

                $('#name3').val(result.DeptOfAccountName);
                $('#tel3').val(result.DeptOfAccountPhone);
                $('#mail3').val(result.DeptOfAccountEmail);

                $('#selBankName').val(result.BankCode);
                $('#nameBranch').val(result.BranchName);
                $('#AccType').val(result.BankAccountTypeId);
                $('#busiName').val(result.AccountName);
                $('#codeNumber').val(result.AccountNumber);

                if (result.AttachFile != null) {
                    $('#lbuploadbookbank').html(result.AttachFile);
                    $('#hduploadbookbank').val(result.file_id_bookbank);
                }


                //$('#businessType option').filter(':selected').val(result.BankCode);

                if (result.CompanyCertifiedFile != null) {
                    $('#lbuploadcertificate').html(result.CompanyCertifiedFile);
                    $('#hdupfilecompany_certified').val(result.file_id_CompanyCertifiedFile);
                }
                if (result.CommercialRegistrationFile != null) {
                    $('#lbuploadComRegis').html(result.CommercialRegistrationFile);
                    $('#hdupfilecommercial_registration').val(result.file_id_CommercialRegistrationFile);
                }
                if (result.VatRegistrationCertificateFile != null) {
                    $('#lbupload20').html(result.VatRegistrationCertificateFile);
                    $('#hdupfilevat_registration_certificate').val(result.file_id_VatRegistrationCertificateFile);
                }

                GetAddress(companyId);
            }


        },
        error: function (result) {

        }

    });
}

function GetAddress(companyId) {
    // inittbAddressResult(companyId);

    $.ajax({
        url: "/Registration/GetAddress",
        type: "POST",
        data: {
            company: function () {
                return $('#hdCompanyId').val()
            }
        },
        datatype: "JSON",
    }).done(function (data) {
        var val = []
        val = ConcatstrAddress(data.data);
        data.data = val;
        oTableAddress.clear().draw();
        oTableAddress.rows.add(data.data).draw();
    }).fail(function (xhr, status, error) {
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
    });
}

function ConcatstrAddress(data) {
    var val = [];

    $.each(data, function () {
        if (this.outFullAddress != null && this.outFullAddress != "") {
            var strdata = {
                addressId: this.AddressId,
                address_type_id: this.AddressTypeId,
                address_type: this.address_type_name,
                address: this.outFullAddress
            };

            val.push(strdata);
        }
        else {
            var strnumber = this.HouseNo != '' && this.HouseNo != null ? this.HouseNo : '';
            var strvillage = this.VillageName != '' && this.VillageName != null ? $('#txtvillage').parent().parent().text().split(":")[0].trim() + ' ' + this.VillageName : '';
            var strvillageno = this.Moo != '' && this.Moo != null ? $('#txtVillageNo').parent().parent().text().split(":")[1].trim() + ' ' + this.Moo : '';

            var strbuilding = this.Building != '' && this.Building != null ? $('#txtbuilding').parent().parent().text().split(":")[0].trim() + ' ' + this.Building : '';
            var strfloor = this.Floor != '' && this.Floor != null ? $('#txtfloor').parent().parent().text().split(":")[0].trim() + ' ' + this.Floor : '';
            var strroom = this.RoomNo != '' && this.RoomNo != null ? $('#txtroom').parent().parent().text().split(":")[1].trim() + ' ' + this.RoomNo : '';
            var strsoi = this.Soi != '' && this.Soi != null ? $('#txtsoi').parent().parent().text().split(":")[0].trim() + ' ' + this.Soi : '';

            var strroad = this.Road != '' && this.Road != null ? $('#txtroad').parent().parent().text().split(":")[0].trim() + ' ' + this.Road : '';
            var strsubdistrict = this.SubDistrictId != '' && this.SubDistrictId != null ? $('#ddlsubdistrict').parent().parent().text().split(":")[0].trim() + ' ' +
                this.sub_district_name : '';
            var strdistrict = this.DistrictId != 0 && this.DistrictId != null ? $('#ddldistrict').parent().parent().text().split(":")[0].trim() + ' ' +
                this.district_name : '';
            var strprovince = this.ProvinceId != 0 && this.ProvinceId != null ? $('#ddlprovince').parent().parent().text().split(":")[0].trim() + ' ' +
                this.province_name : '';

            var strzipcode = this.ZipCode != '' && this.ZipCode != null ? $('#ddlzipcode').parent().parent().text().split(":")[0].trim() + ' ' +
                this.ZipCode : '';

            var strdata = {
                addressId: this.AddressId,
                address_type_id: this.AddressTypeId,
                address_type: this.address_type_name,
                address: strnumber.trim() + ' ' + strvillage.trim() + ' ' + strvillageno.trim() + ' ' + strbuilding.trim() + ' ' + strfloor.trim() + ' ' +
                    strroom.trim() + ' ' + strsoi.trim() + ' ' + strroad.trim() + ' ' + strsubdistrict.trim() + ' ' + strdistrict.trim() + ' ' +
                    strprovince.trim() + ' ' + strzipcode.trim(),
            };

            val.push(strdata);
        }


    });


    return val;
}

function SaveDaftAddress(stuff) {
    $.ajax({
        type: "POST",
        url: "/Registration/SaveDaftAddress",
        data: { daftdata: stuff },
        dataType: "json",
        success: function (data) {

            if (data.status) {
                var val = []
                val = ConcatstrAddress(data.response);
                data.data = val;
                oTableAddress.clear().draw();
                oTableAddress.rows.add(data.data).draw();
            }
            else {
                bootbox.confirm({
                    title: "System Information",
                    message: data.message,
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

        },
        error: function (xhr, status, error) {

            console.log(status);
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
function uploadFiles(inputId) {


    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();
    var id = "";
    var typefile = "";
    var companyid = $('#hdCompanyId').val();
    switch (inputId) {
        case "inputUploadcertificate": id = $('#hdupfilecompany_certified').val();
            typefile = "CompanyCertifiedFile";
            break;
        case "inputUploadComRegis": id = $('#hdupfilecommercial_registration').val();
            typefile = "CommercialRegistrationFile";
            break;
        case "inputUpload20": id = $('#hdupfilevat_registration_certificate').val();
            typefile = "VatRegistrationCertificateFile";
            break;
        case "inputuploadbookbank": id = $('#hduploadbookbank').val();
            typefile = "bookbankfile";
            break;
    }
    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
        formData.append("fid", id);
        formData.append("type_file", typefile);
        formData.append("Company", companyid);
    }


    $.ajax(
        {
            type: "POST",
            url: "/Registration/UploadFile",
            processData: false,
            contentType: false,
            dataType: "json",
            data: formData,
            success: function (data) {


                if (data.status) {
                    switch (inputId) {
                        case "company_certified_file": $('#hdupfilecompany_certified').val(data.response); break;
                        case "commercial_registration_file": $('#hdupfilecommercial_registration').val(data.response); break;
                        case "vat_registration_certificate_file": $('#hdupfilevat_registration_certificate').val(data.response); break;
                        case "bookbank_file": $('#hduploadbookbank').val(data.response); break;
                    }
                    bootbox.confirm({
                        title: "System Information",
                        message: data.message,
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
                else {
                    bootbox.confirm({
                        title: "System Information",
                        message: data.message,
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
        }
    );
}


function BindDDLprovince(regionid) {

    $.ajax({
        type: "POST",
        url: "/Account/DDLsubcontract_profile_province",
        data: { region_id: regionid },
        dataType: "json",
        success: function (data) {

            if (data != null) {
                $('#ddlprovince').empty();
                $.each(data.responseprovince, function () {
                    $('#ddlprovince').append($('<option></option>').val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr, status, error) {

            //clearForEdit();
            console.log(status);
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

function BindDDLdistrict(province) {

    $.ajax({
        type: "POST",
        url: "/Account/DDLsubcontract_profile_district",
        data: { province_id: province },
        dataType: "json",
        success: function (data) {

            if (data != null) {
                $('#ddldistrict').empty();

                $.each(data.responsedistricrt, function () {
                    $('#ddldistrict').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr, status, error) {

            //clearForEdit();
            console.log(status);
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

function BindDDLsubdistrict(district) {

    $.ajax({
        type: "POST",
        url: "/Account/DDLsubcontract_profile_sub_district",
        data: { district_id: district },
        dataType: "json",
        success: function (data) {

            if (data != null) {
                $('#ddlsubdistrict').empty();
                $('#ddlzipcode').empty();

                $.each(data.responsesubdistrict, function () {

                    $('#ddlsubdistrict').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
                $.each(data.responsezipcode, function () {
                    if (this.Text == "กรุณาเลือกรหัสไปรษณีย์") {

                        $('#ddlzipcode').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                    }
                    else {
                        $('#ddlzipcode').append($("<option></option>").val(this.Value).text(this.Text));
                    }


                })

                $('#ddlzipcode').val("")
            }


        },
        error: function (xhr, status, error) {

            //clearForEdit();
            console.log(status);
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

function BindDDLTitle() {

    $.ajax({
        type: "POST",
        url: "/Account/DDLTitle",
        dataType: "json",
        success: function (data) {

            if (data != null) {

                $('#ddlprefixcompany_name_th').empty();
                $('#ddlprefixcompany_name_en').empty();

                $('#ddlprefixcompany_name_th_dealer').empty();
                $('#ddlprefixcompany_name_en_dealer').empty();

                $('#ddlprefixcompany_name_th').append($('<option></option>').val("").text('Select Title'));
                $('#ddlprefixcompany_name_en').append($('<option></option>').val("").text('Select Title'));

                $('#ddlprefixcompany_name_th_dealer').append($('<option></option>').val("").text('Select Title'));
                $('#ddlprefixcompany_name_en_dealer').append($('<option></option>').val("").text('Select Title'));

                $.each(data.responsetitle, function () {
                    //$('#ddlprefixcompany_name_th').append($('<option></option>').val(this.titleId == "0" ? "" : this.titleId).text(this.titleNameTh));
                    //$('#ddlprefixcompany_name_en').append($('<option></option>').val(this.titleId == "0" ? "" : this.titleId).text(this.TitleNameEn));

                    //$('#ddlprefixcompany_name_th_dealer').append($('<option></option>').val(this.titleId == "0" ? "" : this.titleId).text(this.titleNameTh));
                    //$('#ddlprefixcompany_name_en_dealer').append($('<option></option>').val(this.titleId == "0" ? "" : this.titleId).text(this.titleNameEn));

                    $('#ddlprefixcompany_name_th').append($('<option></option>').val(this.CompanyTypeId == "0" ? "" : this.CompanyTypeId).text(this.CompanyTypeNameTh));
                    $('#ddlprefixcompany_name_en').append($('<option></option>').val(this.CompanyTypeId == "0" ? "" : this.CompanyTypeId).text(this.CompanyTypeNameEn));

                    $('#ddlprefixcompany_name_th_dealer').append($('<option></option>').val(this.CompanyTypeId == "0" ? "" : this.CompanyTypeId).text(this.CompanyTypeNameTh));
                    $('#ddlprefixcompany_name_en_dealer').append($('<option></option>').val(this.CompanyTypeId == "0" ? "" : this.CompanyTypeId).text(this.CompanyTypeNameEn));
                });
            }


        },
        error: function (xhr, status, error) {

            //clearForEdit();
            console.log(status);
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

function BindRegion() {

    $.ajax({
        type: "POST",
        url: "/Account/DDLsubcontract_profile_Region",
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {

            if (data != null) {
                $('#ddlzone').empty();

                $.each(data.responseregion, function () {
                    $('#ddlzone').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr, status, error) {

            //clearForEdit();
            console.log(status);
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

function BindDDLBank() {

    $.ajax({
        type: "POST",
        url: "/Account/DDLBank",
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {

            if (data != null) {
                $('#selBankName').empty();

                $.each(data.responsebank, function () {
                    $('#selBankName').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr, status, error) {

            //clearForEdit();
            console.log(status);
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


function BindAddressType() {
    $.ajax({
        type: "POST",
        url: "/Account/GetAddressType",
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {


            if (data != null) {
                $.each(data.responseaddresstype, function () {
                    var strtext = this.Text;
                    var strvalue = this.Value;
                    $('#chkAddressType input[type=checkbox]').each(function () {

                        if ($(this).val() == strvalue) {
                            var id = $(this).attr("id");

                            $('label[for=' + id + ']').text(strtext);
                        }
                    });

                });
            }


        },
        error: function (xhr, status, error) {

            //clearForEdit();
            console.log(status);
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

function validateform() {
    var hasError = true;

   // var forms = document.getElementsByClassName('need-validate-checktyperegister');
    //var validation = Array.prototype.filter.call(forms, function (form) {
    //    if ($('#hdrdtype').val() == "") {
    //        event.preventDefault();
    //        event.stopPropagation();

    //    }
    //    else {
    if ($('#rdoCompanyType1').is(":checked")) {
        var forms = document.getElementsByClassName('needs-validation-newregister');
        var validation = Array.prototype.filter.call(forms, function (form) {
            if (Validate(".form-control.inputValidation", ".custom-control-input.inputValidation"
                , ".custom-select.inputValidation", ".custom-file-input.inputValidation")) {
                event.preventDefault();
                event.stopPropagation();
            }
            else {
                var forms = document.getElementsByClassName('needs-isvalidate');
                var validation = Array.prototype.filter.call(forms, function (form) {
                    if (Validate(".form-control.inputisvalidate", ".custom-control-input.inputisvalidate"
                        , ".custom-select.inputisvalidate", ".custom-file-input.inputisvalidate")) {
                        event.preventDefault();
                        event.stopPropagation();
                        if (ValidateUpload()) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                    }
                    else {

                        hasError = false;
                    }
                    form.classList.add('was-validated');
                });

            }
            form.classList.add('was-validated');
        });

    }
    else if ($('#rdoCompanyType2').is(":checked")) {

        var forms = document.getElementsByClassName('needs-validation-dealer');
        var validation = Array.prototype.filter.call(forms, function (form) {
            if (Validate(".form-control.inputValidationdealer", ".custom-control-input.inputValidationdealer"
                , ".custom-select.inputValidationdealer", ".custom-file-input.inputValidationdealer")) {
                event.preventDefault();
                event.stopPropagation();
            }
            else {
                var forms = document.getElementsByClassName('needs-isvalidate');
                var validation = Array.prototype.filter.call(forms, function (form) {
                    if (Validate(".form-control.inputisvalidate", ".custom-control-input.inputisvalidate"
                        , ".custom-select.inputisvalidate", ".custom-file-input.inputisvalidate")) {
                        event.preventDefault();
                        event.stopPropagation();
                        if (ValidateUpload()) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                    }
                    else {

                        hasError = false;
                    }
                    form.classList.add('was-validated');
                });

            }
            form.classList.add('was-validated');
        });




    }
    else {
        var forms = document.getElementsByClassName('needs-isvalidate');
        var validation = Array.prototype.filter.call(forms, function (form) {
            if (Validate(".form-control.inputisvalidate", ".custom-control-input.inputisvalidate"
                , ".custom-select.inputisvalidate", ".custom-file-input.inputisvalidate")) {
                event.preventDefault();
                event.stopPropagation();
                if (ValidateUpload()) {
                    event.preventDefault();
                    event.stopPropagation();
                }
            }
            else {

                hasError = false;
            }
            form.classList.add('was-validated');
        });
    }
        //}
        //form.classList.add('was-validated');
    //});

    return hasError;
}

function ValidateUpload() {
    var hasError = false;
    if ($('#hduploadbookbank').val() == "") {
        hasError = true;
        $("#inputuploadbookbank").attr('required', 'required');
    }
    else {
        $("#inputuploadbookbank").removeAttr('required');
    }

    if ($('#hdupfilecompany_certified').val() == "") {
        hasError = true;
        $("#inputUploadcertificate").attr('required', 'required');
    }
    else {
        $("#inputUploadcertificate").removeAttr('required');
    }

    if ($('#hdupfilecommercial_registration').val() == "") {
        hasError = true;
        $("#inputUploadComRegis").attr('required', 'required');
    }
    else {
        $("#inputUploadComRegis").removeAttr('required');
    }

    if ($('#hdupfilevat_registration_certificate').val() == "") {
        hasError = true;
        $("#inputUpload20").attr('required', 'required');
    }
    else {
        $("#inputUpload20").removeAttr('required');
    }

    return hasError;
}

function Validate(formcontrol, custom, customselect, cutomupload) {

    var hasError = false;

    $(formcontrol).each(function () {
        var $this = $(this);
        var fieldvalue = $this.val();
        var type = $this.attr("type");
        var tag = $this[0].tagName;

        if (tag == "INPUT" && type == "text") {
            if (fieldvalue == "") {

                hasError = true;
            }
        }
        else if (tag == "INPUT" && type == "password") {
            if (fieldvalue == "") {

                hasError = true;
            }
        }
        else if ($this.is('select')) {
            if (fieldvalue == "0") {

                hasError = true;
            }
        }
        else if (tag == "INPUT" && type == 'radio') {
            if (!fieldvalue) {

                hasError = true;
            }
        }
        else if (tag == "INPUT" && type == 'checkbox') {
            if (!fieldvalue) {

                hasError = true;

            }
        }
        else if (type == "hidden") {
            if (fieldvalue == "") {

                hasError = true;
            }
        }
    }); //Input
    $(custom).each(function () {
        var $this = $(this);

        var type = $this.attr("type");
        var tag = $this[0].tagName;
        var fieldvalue = $this.is(":checked");

        if (tag == "INPUT" && type == 'radio') {
            if (!fieldvalue) {

                hasError = true;

            }
        }
        else if (tag == "INPUT" && type == 'checkbox') {
            if (!fieldvalue) {

                hasError = true;

            }
        }
    });//radio,check box
    $(customselect).each(function () {
        var $this = $(this);

        var type = $this.attr("type");
        var tag = $this[0].tagName;
        var fieldvalue = $this.val();

        if ($this.is('select')) {
            if (fieldvalue == "") {

                hasError = true;
            }
        }
    });//custom selete
    $(cutomupload).each(function () {
        var $this = $(this);
        var fieldvalue = $this.files;
        if ($this.is('input')) {
            var s = $(this).val().split("\\").pop();
            if (fieldvalue == "") {

                hasError = true;
            }
        }
    });//customupload
    return hasError;
}



function inittbtablocation() {
    otbtablocation = $('#tbtablocation').DataTable({
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
        ajax:{
            url: "/Registration/SearchListLocation",
            type: "POST",
            data: {
                companyid: function () {
                    return $('#hdCompanyId').val()
                }
            },
            datatype: "JSON",
            dataSrc: function (data) {
                $('#lbcompanyname').text(data.companynameth);
                return data.data;
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
        },
        columns: [
            {
                data: null, "visible": false, orderable: false, width: "5%", className: "text-center", render: function (data, type, row) {
                    return "<a href='#'class='view_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='View'>\
                            <svg xmlns='http://www.w3.org/2000/svg' aria-hidden='true' focusable='false' width='1em' height='1em' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg); transform: rotate(360deg);' preserveAspectRatio='xMidYMid meet' viewBox='0 0 26 26'><path d='M4 0C1.8 0 0 1.8 0 4v17c0 2.2 1.8 4 4 4h11c.4 0 .7-.094 1-.094c-1.4-.3-2.594-1.006-3.594-1.906H4c-1.1 0-2-.9-2-2V4c0-1.1.9-2 2-2h6.313c.7.2.687 1.1.687 2v3c0 .6.4 1 1 1h3c1 0 2 0 2 1v1h.5c.5 0 1 .088 1.5.188V8c0-1.1-.988-2.112-2.688-3.813c-.3-.2-.512-.487-.812-.687c-.2-.3-.488-.513-.688-.813C13.113.988 12.1 0 11 0H4zm13.5 12c-3 0-5.5 2.5-5.5 5.5s2.5 5.5 5.5 5.5c1.273 0 2.435-.471 3.375-1.219l.313.313a.955.955 0 0 0 .125 1.218l2.5 2.5c.4.4.975.4 1.375 0l.5-.5c.4-.4.4-1.006 0-1.406l-2.5-2.5a.935.935 0 0 0-1.157-.156l-.281-.313c.773-.948 1.25-2.14 1.25-3.437c0-3-2.5-5.5-5.5-5.5zm0 1.5c2.2 0 4 1.8 4 4s-1.8 4-4 4s-4-1.8-4-4s1.8-4 4-4z' fill='black'/>\
                            </svg></a>";
                }
            },
            { "data": "SubcontractProfileType", width: "10%"},
            { "data": "LocationCode", width: "10%"},
            { "data": "LocationNameTh", width: "20%"},
            { "data": "LocationNameEn", width: "20%"},
            { "data": "LocationNameAlias", width: "15%" },
            { "data": "StorageLocation", width: "10%"},
            { "data": "LocationAddress", width: "10%"},
            { "data": "LocationId", "visible": false }
        ],
        "order": [[2, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

    });
}

function inittbtabTeam() {
    otbtabteam = $('#tbtabteam').DataTable({
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
            url: "/Registration/SearchListTeam",
            type: "POST",
            data: {
                locationId: function () { return $('#ddlteamlocation option').filter(':selected').val(); },
                companyid: function () {return $('#hdCompanyId').val();}
            },
            datatype: "JSON",
            dataSrc: function (data) {
              
                return data.data;
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
        },
        columns: [
            {
                data: null, "visible": false, width: "5%", orderable: false, className: "text-center", render: function (data, type, row) {
                    return "<a href='#'class='view_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='View'>\
                            <svg xmlns='http://www.w3.org/2000/svg' aria-hidden='true' focusable='false' width='1em' height='1em' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg); transform: rotate(360deg);' preserveAspectRatio='xMidYMid meet' viewBox='0 0 26 26'><path d='M4 0C1.8 0 0 1.8 0 4v17c0 2.2 1.8 4 4 4h11c.4 0 .7-.094 1-.094c-1.4-.3-2.594-1.006-3.594-1.906H4c-1.1 0-2-.9-2-2V4c0-1.1.9-2 2-2h6.313c.7.2.687 1.1.687 2v3c0 .6.4 1 1 1h3c1 0 2 0 2 1v1h.5c.5 0 1 .088 1.5.188V8c0-1.1-.988-2.112-2.688-3.813c-.3-.2-.512-.487-.812-.687c-.2-.3-.488-.513-.688-.813C13.113.988 12.1 0 11 0H4zm13.5 12c-3 0-5.5 2.5-5.5 5.5s2.5 5.5 5.5 5.5c1.273 0 2.435-.471 3.375-1.219l.313.313a.955.955 0 0 0 .125 1.218l2.5 2.5c.4.4.975.4 1.375 0l.5-.5c.4-.4.4-1.006 0-1.406l-2.5-2.5a.935.935 0 0 0-1.157-.156l-.281-.313c.773-.948 1.25-2.14 1.25-3.437c0-3-2.5-5.5-5.5-5.5zm0 1.5c2.2 0 4 1.8 4 4s-1.8 4-4 4s-4-1.8-4-4s1.8-4 4-4z' fill='black'/>\
                            </svg></a>";
                }
            },
            { "data": "SubcontractType", width: "10%" },
            { "data": "LocationNameTh", width: "20%"},
            { "data": "TeamCode", width: "10%"},
            { "data": "TeamNameTh", width: "15%"},
            { "data": "TeamNameEn", width: "15%"},
            { "data": "StageLocal", width: "15%"},
            { "data": "ShipTo", width: "10%" },
            { "data": "TeamId", "visible": false }
        ],
        "order": [[2, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

    });
}

function BindDDLLocationTeam() {

    $.ajax({
        type: "POST",
        url: "/Registration/DDLLocation",
        data: { companyid: $('#hdCompanyId').val() },
        dataType: "json",
        success: function (data) {

            if (data != null) {
                $('#ddlteamlocation').empty();
                $('#ddllocationteamengineer').empty();

                $.each(data.response, function () {
                    $('#ddlteamlocation').append($("<option></option>").val(this.Value).text(this.Text));
                    $('#ddllocationteamengineer').append($("<option></option>").val(this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr, status, error) {

            //clearForEdit();
            console.log(status);
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

function inittbtabengineer() {
    otbtabengineer = $('#tbtabengineer').DataTable({
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
            url: "/Registration/SearchListEngineer",
            type: "POST",
            data: {
                locationId: function () { return $('#ddllocationteamengineer option').filter(':selected').val(); },
                teamId: function () { return $('#ddlteamengineer option').filter(':selected').val(); },
                companyid: function () { return $('#hdCompanyId').val(); }
            },
            datatype: "JSON",
            dataSrc: function (data) {
               
                return data.data;
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
        },
        columns: [
            {
                data: null, "visible": false, orderable: false, width: "5%", className: "text-center", render: function (data, type, row) {
                    return "<a href='#'class='view_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='View'>\
                            <svg xmlns='http://www.w3.org/2000/svg' aria-hidden='true' focusable='false' width='1em' height='1em' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg); transform: rotate(360deg);' preserveAspectRatio='xMidYMid meet' viewBox='0 0 26 26'><path d='M4 0C1.8 0 0 1.8 0 4v17c0 2.2 1.8 4 4 4h11c.4 0 .7-.094 1-.094c-1.4-.3-2.594-1.006-3.594-1.906H4c-1.1 0-2-.9-2-2V4c0-1.1.9-2 2-2h6.313c.7.2.687 1.1.687 2v3c0 .6.4 1 1 1h3c1 0 2 0 2 1v1h.5c.5 0 1 .088 1.5.188V8c0-1.1-.988-2.112-2.688-3.813c-.3-.2-.512-.487-.812-.687c-.2-.3-.488-.513-.688-.813C13.113.988 12.1 0 11 0H4zm13.5 12c-3 0-5.5 2.5-5.5 5.5s2.5 5.5 5.5 5.5c1.273 0 2.435-.471 3.375-1.219l.313.313a.955.955 0 0 0 .125 1.218l2.5 2.5c.4.4.975.4 1.375 0l.5-.5c.4-.4.4-1.006 0-1.406l-2.5-2.5a.935.935 0 0 0-1.157-.156l-.281-.313c.773-.948 1.25-2.14 1.25-3.437c0-3-2.5-5.5-5.5-5.5zm0 1.5c2.2 0 4 1.8 4 4s-1.8 4-4 4s-4-1.8-4-4s1.8-4 4-4z' fill='black'/>\
                            </svg></a>";
                }
            },
            { "data": "SubcontractType", width: "10%" },
            { "data": "LocationNameTh", width: "20%" },
            { "data": "TeamNameTh", width: "10%" },
            { "data": "StaffNameTh", width: "15%"},
            { "data": "StaffNameEn", width: "15%" },
            { "data": "Position", width: "10%" },
            { "data": "ContractPhone1", width: "15%"},
            { "data": "EngineerId", "visible": false }
        ],
        "order": [[2, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

    });
}

function BindDDLTeamEngineer(location) {

    $.ajax({
        type: "POST",
        url: "/Registration/DDLTeam",
        data: {
            companyid: $('#hdCompanyId').val(),
            locationId: location},
        dataType: "json",
        success: function (data) {

            if (data != null) {
                $('#ddlteamengineer').empty();

                $.each(data.response, function () {
                    $('#ddlteamengineer').append($("<option></option>").val(this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr, status, error) {

            //clearForEdit();
            console.log(status);
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