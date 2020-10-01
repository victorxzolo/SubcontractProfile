

var oTableAddress;
var tbRevenue;
var tbLocation;

var otbtablocation;
var otbtabteam;
var otbtabengineer;
var url = null;
var urlaccount = null;

$(document).ready(function () {
    url = $("#controllername").data("url");
    urlaccount = $("#accountcontrollername").data("url");
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
    BindDDlBankAccountType();

  //  $('#divSignContract').hide();

    getDataById($('#hdCompanyId').val());

    inittbtablocation();
    inittbtabTeam();
    inittbtabengineer();


    $('#contractstartdate').datetimepicker({
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
    $('#contractenddate').datetimepicker({
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
    $('#dateBirthDate').datetimepicker({
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

    $('#datepickerCertificateDAte').datetimepicker({
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

    $('#rdoCompanyType1').on("change", function () {
        //if ($(this).attr("value") == "NewSubContract") {
            $("#divdealer").hide('slow');
            $('#divnewsubcontract').show('slow');
            $('#hdrdtype').val($(this).attr("value"));
        //}
    });

    $('#rdoCompanyType2').on("change", function () {
        //if ($(this).attr("value") == "Dealer") {
            $("#divnewsubcontract").hide('slow');
            $("#divdealer").show('slow');
            $('#hdrdtype').val($(this).attr("value"));
        //}
    });



    $('#inputTax_id').keyup(function () {
        CheckKeyUps("inputTax_id", "[0-9]");
    });

    $('#txttax_id_dealer').keyup(function () {
        CheckKeyUps("txttax_id_dealer", "[0-9]");
    });

    $('#ddlzone').change(function () {

        BindDDLprovince($('#ddlzone option').filter(':selected').val());
    });

    $('#ddlprovince').change(function () {

        BindDDLdistrict($('#ddlprovince option').filter(':selected').val());
    });
    $('#ddldistrict').change(function () {
        BindDDLsubdistrict($('#ddldistrict option').filter(':selected').val());
    });

    //Location
    $('#btnsearchlocation').click(function () {
        $('#Searchlocation').modal('show');
        $('#txtlocationcodemodal').val($('#txtlocationcode').val());
        $("#btn_search_modal").trigger("click");
    });
    $('#btn_search_modal').click(function () {

        var urlSearchLocation = url.replace('Action', 'SearchLocation');

        $.ajax({
            url: urlSearchLocation,
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
        }).fail(function (xhr) {
            console.log(xhr);
        });

    });
    $('#btn_reset_modal').click(function () {
        ClearDataModalLocation();
    });
    $('#tblocationModal').on('click', 'tbody .select_btn', function () {

        var value = tbLocation.row($(this).closest('tr')).data();
        var lo_id = value.location_id;
        var urlGetLocationSession = url.replace('Action', 'GetLocationSession');
        $.ajax({
            type: "POST",
            url: urlGetLocationSession,
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
            error: function (xhr) {
                console.log(xhr);
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
        var urlGetRevenue = url.replace('Action', 'GetRevenue');
        $.ajax({
            url: urlGetRevenue,
            type: "POST",
            async: false,
            data: {
                tIN: function () {
                    return $('#txtsearchrevenue').val()
                }
            },
            datatype: "JSON",
        }).done(function (data) {
            tbRevenue.clear().draw();
            tbRevenue.rows.add(data.data).draw();
        }).fail(function (xhr) {
            console.log(xhr);
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
        var urlGetAddressById = url.replace('Action', 'GetAddressById');
        $.ajax({
            type: "POST",
            url: urlGetAddressById,
            data: { addressID: data_row.addressId },
            dataType: "json",
            success: function (data) {
                $('#hdAddressID').val('');
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

                    $('#hdAddressID').val(data.response.AddressId);
                    $('#ddlcountry').val(data.response.Country)

                    $('#ddlprovince').val(data.response.ProvinceId)
                    $('#ddlprovince').trigger('change');

                    $('#ddldistrict').val(data.response.DistrictId)
                     $('#ddldistrict').trigger('change');

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
            error: function (xhr) {
                console.log(xhr);
            }
        });
    });

    $('#tbAddressResult').on('click', 'tbody .delete_btn', function () {
        var data_row = oTableAddress.row($(this).closest('tr')).data();
        var urlDeleteDaftAddress = url.replace('Action', 'DeleteDaftAddress');
        $.ajax({
            type: "POST",
            url: urlDeleteDaftAddress,
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
            error: function (xhr) {
                console.log(xhr);
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
                CompanyId: $('#hdCompanyId').val(),
                AddressId: $('#hdAddressID').val()
            }
            stuff.push(data);


        });
        var forms = document.getElementsByClassName('needs-validation-step2');
        var validation = Array.prototype.filter.call(forms, function (form) {
            var failed = false;

            if ($("[name='address_type_id']:checked").length == 0) {
                $("[name='address_type_id']").attr('required', true);
                failed = true;
                $("[name='address_type_id']").parent().css({ color: '#e7515a' });
            }
            else {
                $("[name='address_type_id']").attr('required', false);
                $("[name='address_type_id']").parent().removeAttr('style');
            }

            if (Validate(".form-control.inputValidationAddress", ".custom-control-input.inputValidationAddress"
                , ".custom-select.inputValidationAddress", ".custom-file-input.inputValidationAddress")) {
                failed = true;
            }

            if (failed == true) {
                event.preventDefault();
                event.stopPropagation();
            }
            else {
                SaveDaftAddress(stuff);
            }
            //if (Validate(".form-control.inputValidationAddress", ".custom-control-input.inputValidationAddress"
            //           , ".custom-select.inputValidationAddress", ".custom-file-input.inputValidationAddress")) {
            //    event.preventDefault();
            //    event.stopPropagation();
            //}
            //else {
            //    SaveDaftAddress(stuff);

            //}
            form.classList.add('was-validated');
        });

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

    //$('#btnback').click(function () {
    //    window.location.href = '/Registration/SearchCompanyVerify';
    //});

    $('#btnnotapprove').click(function () {

        onSaveCompanyProfileVerified("N");
    });

    $('#btnapprove').click(function () {
        onSaveCompanyProfileVerified("Y");
    });

    $('#btnEdit').click(function () {
        var Error = validateform();

        if (!Error) {
           
            onSaveCompanyProfile($('#lblStatusSub').text());
        }
    });
    


    //Tab Location
    //$('#location-tab').click(function () {
    //    otbtablocation.ajax.reload();
    //});

    $('#tbtablocation').on('click', 'tbody .view_btn', function () {
        var value = otbtablocation.row($(this).closest('tr')).data();
        var LocationId = value.LocationId;

        getDataLocationById(LocationId);

        $('#dataLocationModal').modal('show');
    });


    GetDropDownLocation();
    //Tab Team
    $('#team-tab').click(function () {
        // otbtabteam.ajax.reload();
        $('#lbteamcompanyname').text($('#lbcompanyname').text());
    });
    $('#ddlteamlocation').change(function () {
        otbtabteam.ajax.reload();
    });
    $('#tbtabteam').on('click', 'tbody .view_btn', function () {
        var value = otbtabteam.row($(this).closest('tr')).data();
        var TeamId = value.TeamId;

        getDataTeamById(TeamId);

        $('#datateamModal').modal('show');
    });

    //Tab Engineer
    $('#engineer-tab').click(function () {
        $('#lbengineercompanyname').text($('#lbcompanyname').text());
    });
    BindDDLTeamEngineer();
    GetDropDownBank();
    GetDropDownTitle();
    $('#ddllocationteamengineer').change(function () {
        var data = $('#ddllocationteamengineer option').filter(':selected').val()
        BindDDLTeamEngineer(data);
        otbtabengineer.ajax.reload();
    });
    $('#ddlteamengineer').change(function () {
        otbtabengineer.ajax.reload();
    });

    $('#tbtabengineer').on('click', 'tbody .view_btn', function () {
        var value = otbtabengineer.row($(this).closest('tr')).data();
        var EngineerId = value.EngineerId;

        getDataEngineerById(EngineerId);

        $('#dataEngineerModal').modal("show");
    });
    $('#Location').change(function () {
        getTeamByLocationIdEdit($(this).val());
    });

    $('#mailCompany').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errorcompany_Email').show();
        }
        else {
            $('#errorcompany_Email').hide();
        }
    });
    $('#mailContract').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errorcontract_email').show();
        }
        else {
            $('#errorcontract_email').hide();
        }
    });
    $('#mail1').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errorinstall_email').show();
        }
        else {
            $('#errorinstall_email').hide();
        }
    });
    $('#mail2').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errormainten_email').show();
        }
        else {
            $('#errormainten_email').hide();
        }
    });
    $('#mail3').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errorAccount_email').show();
        }
        else {
            $('#errorAccount_email').hide();
        }
    });
});

function onSaveCompanyProfileVerified(status) {

    var data = new FormData();

    data.append("CompanyId", $('#hdCompanyId').val());

    if (status != "") {
        data.append("status", status);
    }

    data.append("RemarkForSub", $('#txtRemarkForSub').val());

    var urlOnSave = url.replace('Action', 'OnUpdateByVerified');
    $.ajax({
        url: urlOnSave,
        type: 'POST',
        async: false,
        data: data,
        processData: false,
        contentType: false,
        //data: {
        //    model: company,
        //    status: status,
        //    contractstart: $('#datecontractstart').val(),
        //    contractend: $('#datecontractend').val()
        //},
        //dataType: "json",
        success: function (result) {
            if (result.Response.Status) {
                bootbox.alert(result.Response.Message);
            }
            else {
                bootbox.alert(result.Response.Message);

            }
        },
        error: function (xhr) {
            console.log(xhr);
        }

    });

}


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

    var data = new FormData();

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


    data.append("CompanyNameTh", company_name_th);
    data.append("CompanyNameEn", company_name_en);
    data.append("CompanyAlias", company_alias);
    data.append("DistributionChannel", distribution_channel);
    data.append("ChannelSaleGroup", channel_sale_group);
    data.append("TaxId", tax_id);
    data.append("WtName", wt_name);
    data.append("VatType", vat_type);
    data.append("CompanyEmail", $('#mailCompany').val());
    data.append("ContractName", $('#nameContract').val());
    data.append("ContractPhone", $('#telContract').val());
    data.append("ContractEmail", $('#mailContract').val());

    data.append("BankCode", $('#selBankName option').filter(':selected').val());
    data.append("BankName", $('#selBankName option').filter(':selected').text());
    data.append("AccountNumber", $('#codeNumber').val());
    data.append("AccountName", $('#busiName').val());
    data.append("BranchName", $('#nameBranch').val());

    data.append("DeptOfInstallName", $('#name1').val());
    data.append("DeptOfMaintenName", $('#name2').val());
    data.append("DeptOfAccountName", $('#name3').val());

    data.append("DeptOfInstallPhone", $('#tel1').val());
    data.append("DeptOfMaintenPhone", $('#tel2').val());
    data.append("DeptOfAccountPhone", $('#tel3').val());

    data.append("DeptOfInstallEmail", $('#mail1').val());
    data.append("DeptOfMaintenEmail", $('#mail2').val());
    data.append("DeptOfAccountEmail", $('#mail3').val());

    data.append("LocationCode", $('#txtlocationcode').val());
    data.append("LocationNameTh", $('#txtlocationname').val());
    data.append("LocationNameEn", $('#txtlocationname').val());

    data.append("BankAccountTypeId", $('#AccType option').filter(':selected').val());
    data.append("SubcontractProfileType", chksubcontract_type);
    data.append("CompanyTitleThId", company_title_name_th);
    data.append("CompanyTitleEnId", company_title_name_en);
    data.append("CompanyId", $('#hdCompanyId').val());

    if (status != "") {
        data.append("status", status);
    }
 
    data.append("contractstart", $('#datecontractstart').val());
    data.append("contractend", $('#datecontractend').val());

    data.append("FileBookBank", $("#inputuploadbookbank").get(0).files[0]);
    data.append("FileCompanyCertified", $("#inputUploadcertificate").get(0).files[0]);
    data.append("FileCommercialRegistration", $("#inputUploadComRegis").get(0).files[0]);
    data.append("FileVatRegistrationCertificate", $("#inputUpload20").get(0).files[0]);

    data.append("AttachFile", $('#lbuploadbookbank').text());
    data.append("CompanyCertifiedFile", $('#lbuploadcertificate').text());
    data.append("CommercialRegistrationFile", $('#lbuploadComRegis').text());
    data.append("VatRegistrationCertificateFile", $('#lbupload20').text());

    data.append("RemarkForSub", $('#txtRemarkForSub').val());

    data.append("VendorCode", $('#txtvendercode').val())

    var urlOnSave = url.replace('Action', 'OnSave');
    $.ajax({
        url: urlOnSave,
        type: 'POST',
        async: false,
        data: data,
        processData: false,
        contentType: false,
        //data: {
        //    model: company,
        //    status: status,
        //    contractstart: $('#datecontractstart').val(),
        //    contractend: $('#datecontractend').val()
        //},
        //dataType: "json",
        success: function (result) {
            if (result.Response.Status) {
                bootbox.alert(result.Response.Message);
            }
            else {
                bootbox.alert(result.Response.Message);

            }
        },
        error: function (xhr) {
            console.log(xhr);
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
        paging: false,
        "oLanguage": {
            //"oPaginate": { "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>', "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>' },
            //"sInfo": "Showing page _PAGE_ of _PAGES_",
            "sSearch": false,
            "sLengthMenu": "Results :  _MENU_",
        },
        "scrollX": false,
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
                data: null, width: "5%", orderable: false, render: function (data, type, row) {
                    return "<a href='#'class='edit_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='แก้ไข'><svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-edit'><path d='M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7'></path><path d='M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z'></path></svg></a>";
                }
            },
            {
                data: null, width: "5%", orderable: false, render: function (data, type, row) {
                    return "<a href='#' class='delete_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='ลบ'><svg xmlns='http://www.w3.org/2000/svg'width='24' height='24' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg); transform: rotate(360deg);' preserveAspectRatio='xMidYMid meet' viewBox='0 0 24 24'><g fill='none' stroke='#626262' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'><path d='M3 6h18'/><path d='M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2'/></g></svg></a>";
                }
            },
        ],
        "order": [[1, "desc"]],
        "stripeClasses": [],
        //drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

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
        "scrollX": false,
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
        "scrollX": false,
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
    var urlGetDataById = url.replace('Action', 'GetDataById');
    $.ajax({
        url: urlGetDataById,
        type: 'POST',
        data: { 'companyId': companyId },
        dataType: "json",
        async: false,
        cache: false,
        success: function (result) {
            console.log(result)
            if (result != null) {
               
                //if (result.Status == 'Activate') {
                //    $('#divSignContract').show();
                //    $('#btnnotapprove').hide();
                //    $('#btnapprove').hide();
                //} else {
                //    $('#divSignContract').hide();
                //    $('#btnnotapprove').show();
                //    $('#btnapprove').show();
                //}

                
                $('#lblStatusSub').text(result.Status);

                $('#hdCompanyId').val(result.CompanyId);

                $('#txtRemarkForSub').val(result.RemarkForSub); 


                if (result.SubcontractProfileType == "NewSubContract") {
                    $('#rdoCompanyType1').attr('checked', 'checked');  
                    $('#hdrdtype').val(result.SubcontractProfileType);
                    //$('#rdoCompanyType1').trigger('change');

                    $("#inputCompany_alias").val(result.CompanyAlias);
                    $("#inputCompany_name_th").val(result.CompanyNameTh);
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

                    $("#divdealer").hide('slow');
                    $('#divnewsubcontract').show('slow');
                    $('#hdrdtype').val($(this).attr("value"));
                }
                else if (result.SubcontractProfileType == "Dealer") {
                    $('#rdoCompanyType2').attr('checked', 'checked');  
                    $('#hdrdtype').val(result.SubcontractProfileType);
                    //$('#rdoCompanyType2').trigger("click");

                    $('#txtlocationcode').val(result.LocationCode);
                    $('#txtdistribution').val(result.DistributionChannel);
                    $('#txtlocationname').val(result.LocationNameTh);
                    $('#txtchannelsalegroup').val(result.ChannelSaleGroup);
                    $('#txttax_id_dealer').val(result.TaxId);
                    $('#txtcompany_alias_dealer').val(result.CompanyAlias);

                    $('#ddlprefixcompany_name_th_dealer').val(result.CompanyTitleThId);
                    $('#txtcompany_name_th_dealer').val(result.CompanyNameTh);
                    $('#ddlprefixcompany_name_en_dealer').val(result.CompanyTitleEnId);
                    $('#txtcompany_name_en_dealer').val(result.CompanyNameEn);
                    $('#txtwt_name_dealer').val(result.WtName);

                    if (result.VatType == "VAT") {
                        $('#chkvat_typeT_dealer').prop('checked', true);
                    }
                    else if (result.VatType == "NON_VAT") {
                        $('#chkvat_typeE_dealer').prop('checked', true);
                    }

                    $("#divnewsubcontract").hide('slow');
                    $("#divdealer").show('slow');
                    $('#hdrdtype').val($(this).attr("value"));
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

                if (result.ContractStartDate != null) {
                    //var d = new Date(result.ContractStartDate);
                    //var month = d.getMonth();
                    //var day = d.getDate();
                    //var year = d.getFullYear();

                    var date = new Date(result.ContractStartDate);
                    var sMonth = padValue(date.getMonth() + 1);
                    var sDay = padValue(date.getDate());
                    var sYear = date.getFullYear();
                    var strCreateDate = "";

                    strCreateDate = sDay + '/' + sMonth + '/' + sYear;
                    //$('#contractstartdate').data("DateTimePicker").date(moment(new Date(year, month, day), 'DD/MM/YYYY'));
                    $('#datecontractstart').val(strCreateDate);
                }


                if (result.ContractEndDate != null) {
                    //var dateend = new Date(result.ContractEndDate);
                    //var monthend = dateend.getMonth();
                    //var dayend = dateend.getDate();
                    //var yearend = dateend.getFullYear();
                    var dateend = new Date(result.ContractEndDate);
                    var sMonthend = padValue(dateend.getMonth() + 1);
                    var sDayend = padValue(dateend.getDate());
                    var sYearend = dateend.getFullYear();
                    var strCreateDateend = "";
                    strCreateDateend = sDayend + '/' + sMonthend + '/' + sYearend;
                    $('#datecontractend').val(strCreateDateend);
                    //$('#contractenddate').data("DateTimePicker").date(moment(new Date(yearend, monthend, dayend), 'DD/MM/YYYY'));
                }

                $('#txtvendercode').val(result.VendorCode);
       
            }


        },
        error: function (result) {
            console.log(result);
        }

    });
}

function GetAddress(companyId) {
    // inittbAddressResult(companyId);
    var urlGetAddress = url.replace('Action', 'GetAddress');
    $.ajax({
        url: urlGetAddress,
        type: "POST",
        async: false,
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
    }).fail(function (xhr) {
        console.log(xhr);
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
    var urlSaveDaftAddress = url.replace('Action', 'SaveDaftAddress');
    $.ajax({
        type: "POST",
        url: urlSaveDaftAddress,
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
        error: function (result) {

            console.log(result);
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

    var urlUploadFile = url.replace('Action', 'UploadFile');
    $.ajax(
        {
            type: "POST",
            url: urlUploadFile,
            processData: false,
            contentType: false,
            dataType: "json",
            data: formData,
            success: function (data) {


                if (data.status) {
                    switch (inputId) {
                        case "inputUploadcertificate": $('#hdupfilecompany_certified').val(data.response); break;
                        case "inputUploadComRegis": $('#hdupfilecommercial_registration').val(data.response); break;
                        case "inputUpload20": $('#hdupfilevat_registration_certificate').val(data.response); break;
                        case "inputuploadbookbank": $('#hduploadbookbank').val(data.response); break;
                    }
                }
                else {
                    switch (inputId) {
                        case "inputUploadcertificate": $('#hdupfilecompany_certified').val('');
                            $('#lbuploadcertificate').text('เลือกไฟล์');
                            break;
                        case "inputUploadComRegis": $('#hdupfilecommercial_registration').val('');
                            $('#lbuploadComRegis').text('เลือกไฟล์');
                            break;
                        case "inputUpload20": $('#hdupfilevat_registration_certificate').val('');
                            $('#lbupload20').text('เลือกไฟล์');
                            break;
                        case "inputuploadbookbank": $('#hduploadbookbank').val(''); $('#lbuploadbookbank').text('เลือกไฟล์'); break;
                    }

                }
                bootbox.alert(data.message);

            },
            error: function (xhr) {
                console.log(xhr);
            }
        }
    );
}


function BindDDLprovince(regionid) {
    var urlDDLsubcontract_profile_province = urlaccount.replace('Action', 'DDLsubcontract_profile_province');

    $.ajax({
        type: "POST",
        async: false,
        url: urlDDLsubcontract_profile_province,
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
        error: function (result) {

            console.log(result);
        }
    });
}

function BindDDLdistrict(province) {
    var urlDDLsubcontract_profile_district = urlaccount.replace('Action', 'DDLsubcontract_profile_district');
    $.ajax({
        type: "POST",
        async: false,
        url: urlDDLsubcontract_profile_district,
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
        error: function (result) {

            console.log(result);
        }
    });
}

function BindDDLsubdistrict(district) {
    var urlDDLsubcontract_profile_sub_district = urlaccount.replace('Action', 'DDLsubcontract_profile_sub_district');
    $.ajax({
        type: "POST",
        async: false,
        url: urlDDLsubcontract_profile_sub_district,
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
        error: function (result) {

            console.log(result);
        }
    });
}

function BindDDLTitle() {
    var urlDDLTitle = urlaccount.replace('Action', 'DDLTitle');
    $.ajax({
        type: "POST",
        url: urlDDLTitle,
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
        error: function (result) {

            console.log(result);
        }
    });
}

function BindRegion() {
    var urlDDLsubcontract_profile_Region = urlaccount.replace('Action', 'DDLsubcontract_profile_Region');
    $.ajax({
        type: "POST",
        url: urlDDLsubcontract_profile_Region,
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
        error: function (result) {

            console.log(result);
        }
    });
}

function BindDDLBank() {
    var urlDDLBank = urlaccount.replace('Action', 'DDLBank');
    $.ajax({
        type: "POST",
        url: urlDDLBank,
        //data: { province_id: province },
        dataType: "json",
        async: false,
        success: function (data) {

            if (data != null) {
                $('#selBankName').empty();

                $.each(data.responsebank, function () {
                    $('#selBankName').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }


        },
        error: function (result) {

            console.log(result);
        }
    });
}


function BindAddressType() {
    var urlGetAddressType = urlaccount.replace('Action', 'GetAddressType');
    $.ajax({
        type: "POST",
        async: false,
        url: urlGetAddressType,
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
        error: function (result) {

            //clearForEdit();
            console.log(result);
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

    var urlSearchListLocation = url.replace('Action', 'SearchListLocation');
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
        "scrollX": false,
        "language": {
            "zeroRecords": "No data found.",
            "decimal": ",",
            "thousands": "."
        },
        ajax: {
            url: urlSearchListLocation,
            type: "POST",
            async: false,
            data: {
                'companyid': function () {
                    return $('#hdCompanyId').val()
                }
            },
            datatype: "JSON",
            dataSrc: function (datalocation) {
                $('#lbcompanyname').text(datalocation.companynameth);
                return datalocation.data;
            },
            error: function (xhr) {
                console.log(xhr);
            }
        },
        columns: [
            {
                data: null, orderable: false, width: "5%", className: "text-center", render: function (data, type, row) {
                    return "<a href='#'class='view_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='View'>\
                            <svg xmlns='http://www.w3.org/2000/svg' aria-hidden='true' focusable='false' width='1em' height='1em' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg); transform: rotate(360deg);' preserveAspectRatio='xMidYMid meet' viewBox='0 0 26 26'><path d='M4 0C1.8 0 0 1.8 0 4v17c0 2.2 1.8 4 4 4h11c.4 0 .7-.094 1-.094c-1.4-.3-2.594-1.006-3.594-1.906H4c-1.1 0-2-.9-2-2V4c0-1.1.9-2 2-2h6.313c.7.2.687 1.1.687 2v3c0 .6.4 1 1 1h3c1 0 2 0 2 1v1h.5c.5 0 1 .088 1.5.188V8c0-1.1-.988-2.112-2.688-3.813c-.3-.2-.512-.487-.812-.687c-.2-.3-.488-.513-.688-.813C13.113.988 12.1 0 11 0H4zm13.5 12c-3 0-5.5 2.5-5.5 5.5s2.5 5.5 5.5 5.5c1.273 0 2.435-.471 3.375-1.219l.313.313a.955.955 0 0 0 .125 1.218l2.5 2.5c.4.4.975.4 1.375 0l.5-.5c.4-.4.4-1.006 0-1.406l-2.5-2.5a.935.935 0 0 0-1.157-.156l-.281-.313c.773-.948 1.25-2.14 1.25-3.437c0-3-2.5-5.5-5.5-5.5zm0 1.5c2.2 0 4 1.8 4 4s-1.8 4-4 4s-4-1.8-4-4s1.8-4 4-4z' fill='black'/>\
                            </svg></a>";
                }
            },
            { "data": "SubcontractProfileType" },
            { "data": "LocationCode"},
            { "data": "LocationNameTh"},
            { "data": "LocationNameEn" },
            //{ "data": "LocationNameAlias" },
            //{ "data": "StorageLocation"},
            //{ "data": "LocationAddress"},
            { "data": "LocationId", "visible": false }
        ],
        "order": [[2, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

    });
}

function getDataLocationById(locationId) {
    var urlGetDataLocationById = url.replace('Action', 'GetDataLocationById');
    $.ajax({
        url: urlGetDataLocationById,
        type: 'POST',
        async: false,
        data: { 'locationId': locationId },
        dataType: "json",
        success: function (result) {
            console.log(result)
            if (result != null) {

                $("#lblcompanyName").text(result.CompanyNameTh);

                //if (result.SubcontractProfileType == "newsub") {
                //    $('#rdoCompanyTypeNew').prop('checked', true);

                //}
                //else if (result.SubcontractProfileType == "dealer") {
                //    $('#rdoCompanyTypeDealer').prop('checked', true);
                //}

                $('#inputLocationCode').val(result.LocationCode);

                $('#inputLocationAlias').val(result.LocationNameAlias);
                $('#inputLocationNameT').val(result.LocationNameTh);
                $('#inputLocationNameE').val(result.LocationNameEn);
                $('#inputStorage').val(result.StorageLocation);
                $('#inputShip').val(result.ShipTo);
                $('#inoutOutStorage').val(result.OutOfServiceStorageLocation);
                $('#inputVat').val(result.VatBranchNumber);
                $('#inputPhoneNo').val(result.Phone);
                $('#inputMainPhone').val(result.CompanyMainContractPhone);
                $('#inputInstallationsPhone').val(result.InstallationsContractPhone);
                $('#inputMaintenencePhone').val(result.MaintenanceContractPhone);
                $('#inputInventoryPhone').val(result.InventoryContractPhone);
                $('#inputPaymentPhone').val(result.PaymentContractPhone);
                $('#inputEtcPhone').val(result.EtcContractPhone);
                $('#inputCompanyMail').val(result.CompanyGroupMail);
                $('#inputInstallationMail').val(result.InstallationsContractMail);
                $('#inputMaintanenceMail').val(result.MaintenanceContractMail);
                $('#inputInventoryMail').val(result.InventoryContractMail);
                $('#inputPaymentMail').val(result.PaymentContractMail);
                $('#inputEtcMail').val(result.EtcContractMail);

                $('#inputLocationAddress').val(result.LocationAddress);

                $("#inputPostAddress").val(result.PostAddress)
                $("#inputTaxAddress").val(result.TaxAddress)
                $("#inputWTAddress").val(result.WtAddress)

                $('#inputBankCode').val(result.BankCode);
                $('#inputBankName').val(result.BankName);
                $('#inputBankAccountNo').val(result.BankAccountNo);
                $('#inputBankAccountName').val(result.BankAccountName);
                $('#inputBankBranchNo').val(result.BankBranchCode);
                $('#inputBankBranchName').val(result.BankBranchName);
                $('#inputPanaltyPhone').val(result.PenaltyContractPhone);
                $('#inputPanaltyMail').val(result.PenaltyContractMail);
                $('#inputContractPhone').val(result.ContractPhone);
                $('#inputContractMail').val(result.ContractMail);
                $('#hdCompanyId').val(result.CompanyId);

                if (result.BankAttachFile != null) {
                    $('#lbBankAttach').text(result.BankAttachFile);
                    //$('#hdupfileslip').val(data.file_id_Slip);
                    //$('#linkdownload').text(data.SlipAttachFile);
                    // DownloadFileSlip(data.SlipAttachFile);
                }
                else {
                    $('#lbBankAttach').text("เลือกไฟล์");
                }



                //if (result.VatType == "VAT") {
                //    $('#chkvat_typeT').prop('checked', true);
                //}
                //else if (result.VatType == "NON_VAT") {
                //    $('#chkvat_typeE').prop('checked', true);
                //}

                //if (result.isActive == true) {
                //    $("[name=rdoActive]").filter("[value='true']").prop("checked", true);
                //} else {
                //    $("[name=rdoActive]").filter("[value='false']").prop("checked", true);
                //}
            }
        },
        error: function (result) {
            console.log(result);
        }

    });
}

function inittbtabTeam() {
    var urlSearchListTeam = url.replace('Action', 'SearchListTeam');
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
        "scrollX": false,
        "language": {
            "zeroRecords": "No data found.",
            "decimal": ",",
            "thousands": "."
        },
        ajax: {
            url: urlSearchListTeam,
            type: "POST",
            async: false,
            data: {
                locationId: function () { return $('#ddlteamlocation option').filter(':selected').val(); },
                companyid: function () { return $('#hdCompanyId').val(); }
            },
            datatype: "JSON",
            dataSrc: function (data) {

                return data.data;
            },
            error: function (result) {
                console.log(result);
            }
        },
        columns: [
            {
                data: null, width: "5%", orderable: false, className: "text-center", render: function (data, type, row) {
                    return "<a href='#'class='view_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='View'>\
                            <svg xmlns='http://www.w3.org/2000/svg' aria-hidden='true' focusable='false' width='1em' height='1em' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg); transform: rotate(360deg);' preserveAspectRatio='xMidYMid meet' viewBox='0 0 26 26'><path d='M4 0C1.8 0 0 1.8 0 4v17c0 2.2 1.8 4 4 4h11c.4 0 .7-.094 1-.094c-1.4-.3-2.594-1.006-3.594-1.906H4c-1.1 0-2-.9-2-2V4c0-1.1.9-2 2-2h6.313c.7.2.687 1.1.687 2v3c0 .6.4 1 1 1h3c1 0 2 0 2 1v1h.5c.5 0 1 .088 1.5.188V8c0-1.1-.988-2.112-2.688-3.813c-.3-.2-.512-.487-.812-.687c-.2-.3-.488-.513-.688-.813C13.113.988 12.1 0 11 0H4zm13.5 12c-3 0-5.5 2.5-5.5 5.5s2.5 5.5 5.5 5.5c1.273 0 2.435-.471 3.375-1.219l.313.313a.955.955 0 0 0 .125 1.218l2.5 2.5c.4.4.975.4 1.375 0l.5-.5c.4-.4.4-1.006 0-1.406l-2.5-2.5a.935.935 0 0 0-1.157-.156l-.281-.313c.773-.948 1.25-2.14 1.25-3.437c0-3-2.5-5.5-5.5-5.5zm0 1.5c2.2 0 4 1.8 4 4s-1.8 4-4 4s-4-1.8-4-4s1.8-4 4-4z' fill='black'/>\
                            </svg></a>";
                }
            },
            { "data": "SubcontractProfileType"},
            { "data": "LocationNameTh"},
            { "data": "TeamCode" },
            { "data": "TeamNameTh"},
            { "data": "TeamNameEn" },
            //{ "data": "StageLocal", width: "15%" },
            //{ "data": "ShipTo", width: "10%" },
            { "data": "TeamId", "visible": false }
        ],
        "order": [[2, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

    });
}

function getDataTeamById(teamId) {
    var urlGetDataTeamById = url.replace('Action', 'GetDataTeamById');
    $.ajax({
        url: urlGetDataTeamById,
        type: 'POST',
        async: false,
        data: { 'teamId': teamId },
        dataType: "json",
        success: function (result) {
            //  console.log(result)
            if (result != null) {
                //  alert(result.LocationId);
                //  $('#locationName_Th option[value="' + result.LocationId+'"]').prop("selected", true);
                //    $('#locationName_Th').val(result.LocationId).prop('selected', true);

                $("#ddllocationName_Th").val(result.LocationId);
                $('#locationCode').val(result.LocationCode);
                $('#TeamCode').val(result.TeamCode);
                $('#VendorCode').val(result.VendorCode);
                $('#TeamNameT').val(result.TeamNameTh);
                $('#TeamNameE').val(result.TeamNameEn);
                $('#Storage').val(result.StageLocal);
                $('#Ship').val(result.ShipTo);
                $('#OutStorage').val(result.OosStorageLocation);
                $('#WarrantyMA').val(result.WarrantyMa);
                $('#WarrantyInstall').val(result.WarrantyInstall);
                $('#ServiceSkill').val(result.ServiceSkill);
                $('#InstallationPhone').val(result.InstallationsContractPhone);
                $('#MaintenancePhone').val(result.MaintenanceContractPhone);
                $('#EtcPhone').val(result.EtcContractPhone);
                $('#InstallationMail').val(result.InstallationsContractEmail);
                $('#MaintenanceMail').val(result.MaintenanceContractEmail);
                $('#EtcMail').val(result.EtcContractEmail);
                $('#hdTeamId').val(result.TeamId);
                $('#hdTeamCompanyId').val(result.CompanyId);

            }
        },
        error: function (result) {
            console.log(result);
        }

    });
}

function GetDropDownLocation() {
    var urlGetLocationByCompany= url.replace('Action', 'GetLocationByCompany');
    $.ajax({
        type: 'POST',
        async: false,
        url: urlGetLocationByCompany,
        data: { companyid: $('#hdCompanyId').val() },
        dataType: 'json',
        success: function (data) {
            $("#ddllocationName_Th").empty();
            $('#ddlteamlocation').empty();
            $('#ddllocationteamengineer').empty();
            $("#Location").empty();

            //$("#ddllocationName_Th").append('<option value="">--Please Select--</option>');
            $.each(data, function () {
                //$("#ddllocationName_Th").append('<option value="' + result.LocationId + '">' + result.LocationNameTh + '</option>');
                $("#ddllocationName_Th").append($("<option></option>").val(this.Value).text(this.Text));
                $('#ddlteamlocation').append($("<option></option>").val(this.Value).text(this.Text));
                $('#ddllocationteamengineer').append($("<option></option>").val(this.Value).text(this.Text));
                $('#Location').append($("<option></option>").val(this.Value).text(this.Text));
            });

            //$.each(data.response, function () {
            //    $('#ddlteamlocation').append($("<option></option>").val(this.Value).text(this.Text));
            //    $('#ddllocationteamengineer').append($("<option></option>").val(this.Value).text(this.Text));
            //});
        },
        failure: function () {
            //$("#ddllocationName_Th").empty();
            //$("#ddllocationName_Th").append('<option value="">--Please Select--</option>');
        }
    });
}

function inittbtabengineer() {
    var urlSearchListEngineer = url.replace('Action', 'SearchListEngineer');
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
        "scrollX": false,
        "language": {
            "zeroRecords": "No data found.",
            "decimal": ",",
            "thousands": "."
        },
        ajax: {
            url: urlSearchListEngineer,
            type: "POST",
            async: false,
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
                console.log(xhr);
            }
        },
        columns: [
            {
                data: null, orderable: false, width: "5%", className: "text-center", render: function (data, type, row) {
                    return "<a href='#'class='view_btn'  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='View'>\
                            <svg xmlns='http://www.w3.org/2000/svg' aria-hidden='true' focusable='false' width='1em' height='1em' style='-ms-transform: rotate(360deg); -webkit-transform: rotate(360deg); transform: rotate(360deg);' preserveAspectRatio='xMidYMid meet' viewBox='0 0 26 26'><path d='M4 0C1.8 0 0 1.8 0 4v17c0 2.2 1.8 4 4 4h11c.4 0 .7-.094 1-.094c-1.4-.3-2.594-1.006-3.594-1.906H4c-1.1 0-2-.9-2-2V4c0-1.1.9-2 2-2h6.313c.7.2.687 1.1.687 2v3c0 .6.4 1 1 1h3c1 0 2 0 2 1v1h.5c.5 0 1 .088 1.5.188V8c0-1.1-.988-2.112-2.688-3.813c-.3-.2-.512-.487-.812-.687c-.2-.3-.488-.513-.688-.813C13.113.988 12.1 0 11 0H4zm13.5 12c-3 0-5.5 2.5-5.5 5.5s2.5 5.5 5.5 5.5c1.273 0 2.435-.471 3.375-1.219l.313.313a.955.955 0 0 0 .125 1.218l2.5 2.5c.4.4.975.4 1.375 0l.5-.5c.4-.4.4-1.006 0-1.406l-2.5-2.5a.935.935 0 0 0-1.157-.156l-.281-.313c.773-.948 1.25-2.14 1.25-3.437c0-3-2.5-5.5-5.5-5.5zm0 1.5c2.2 0 4 1.8 4 4s-1.8 4-4 4s-4-1.8-4-4s1.8-4 4-4z' fill='black'/>\
                            </svg></a>";
                }
            },
            { "data": "SubcontractProfileType" },
            { "data": "LocationNameTh"},
            { "data": "TeamNameTh"},
            { "data": "StaffNameTh" },
            { "data": "StaffNameEn"},
            //{ "data": "Position", width: "10%" },
            //{ "data": "ContractPhone1", width: "15%" },
            { "data": "EngineerId", "visible": false }
        ],
        "order": [[2, "desc"]],
        "stripeClasses": [],
        drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

    });
}

function BindDDLTeamEngineer(location) {
    var urlDDLTeam = url.replace('Action', 'DDLTeam');
    $.ajax({
        type: "POST",
        url: urlDDLTeam,
        data: {
            companyid: $('#hdCompanyId').val(),
            locationId: location
        },
        dataType: "json",
        success: function (data) {

            if (data != null) {
                $('#ddlteamengineer').empty();

                $.each(data.response, function () {
                    $('#ddlteamengineer').append($("<option></option>").val(this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr) {

            //clearForEdit();
            console.log(xhr);
        }
    });
}

function getDataEngineerById(id) {
    var urlGetDataEngineerById = url.replace('Action', 'GetDataEngineerById');
    $.ajax({
        url: urlGetDataEngineerById,
        type: 'POST',
        data: { 'engineerId': id },
        dataType: "json",
        async:false,
        success: function (result) {
            //  console.log(result)
            if (result != null) {
                getTeamByLocationIdEdit(result.LocationId);
                getDataPersonalById(result.PersonalId);

                $("#Location").val(result.LocationId)
                $("#lblcompanyNameE").text(result.CompanyNameTh);

                //$('#Team option').prop('selected', function () {
                //    return result.TeamId;
                //});

                $('#hdEngineerId').val(result.EngineerId);

                // $('#StaffCode').val(result.StaffCode);
                $('#StaffName').val(result.StaffName);
                $('#StaffNameT').val(result.StaffNameTh);
                $('#StaffNameE').val(result.StaffNameEn);
                $('#Position').val(result.Position);
                $('#CitizenID').val(result.CitizenId);
                $('#tshirtsize').val(result.TshirtSize);
                $('#ContractPhone1').val(result.ContractPhone1);
                $('#ContractPhone2').val(result.ContractPhone2);
                $('#ContractEmail').val(result.ContractEmail);
                $('#VehicleType').val(result.VehicleType);
                $('#VehicleBrand').val(result.VehicleBrand);
                $('#VehicleSerise').val(result.VehicleSerise);
                $('#VehicleColor').val(result.VehicleColor);
                $('#VehicleYear').val(result.VehicleYear);
                $('#VehicleLicense').val(result.VehicleLicensePlate);
                $('#Toolotdr').val(result.ToolOtrd);
                $('#ToolSplicing').val(result.ToolSplicing);

                $("#ddlBankName").val(result.BankName);
                $('#AccNo').val(result.AccountNo);
                $('#AccName').val(result.AccountName);

                $('#hdEngineerCompanyId').val(result.CompanyId);

                if (result.PersonalAttachFile != null) {
                    $('#lbpersonalAttahFile').text(result.PersonalAttachFile);
                }

                if (result.VehicleAttachFile != null) {
                    $('#lbVehicleAttachFile').text(result.VehicleAttachFile);
                }



                $("#Team").val(result.TeamId);

            }
        },
        error: function (result) {
            console.log(result);
        }

    });
}

function getTeamByLocationIdEdit(locationId) {
    var urlGetDataByLocationId = url.replace('Action', 'GetDataByLocationId');
    $.ajax({
        url: urlGetDataByLocationId,
        type: 'POST',
        data: {
            'locationId': locationId,
            'companyid': $('#hdCompanyId').val()
        },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#Team").empty();
            $("#Team").append('<option value="">--Please All--</option>');
            $.each(data, function (id, result) {
                $("#Team").append('<option value="' + result.TeamId + '">' + result.TeamNameTh + '</option>');
            });
        },
        failure: function () {
            $("#Team").empty();
            $("#Team").append('<option value="">--Please All--</option>');
        }
    });
}

function getDataPersonalById(id) {
    var urlGetDataPersonalById = url.replace('Action', 'GetDataPersonalById');
    $.ajax({
        url: urlGetDataPersonalById,
        type: 'POST',
        data: { 'personalId': id },
        dataType: "json",
        async: false,
        success: function (result) {
            if (result != null) {
                //---personal name
                $('#hdPersonalId').val(id);
                $('#CitizenID2').val(result.CitizenId);
                $("#Title").val(result.TitleName);

                // $('#Title').val(result.TitleName);
                $('#FullNameT').val(result.FullNameEn);
                $('#FullNameE').val(result.FullNameTh);
                //$('#BirthDate').val(result.BirthDate);
                if (result.BirthDate) {
                    var d = new Date(result.BirthDate);
                    var month = d.getMonth();
                    var day = d.getDate();
                    var year = d.getFullYear();

                    $('#dateBirthDate').data("DateTimePicker").date(moment(new Date(year, month, day), 'DD/MM/YYYY'));
                }

                //Gender
                if (result.Gender == "M") {
                    //    $('#rdoMale').val(result.Gender);
                    $("#rdoMale").prop("checked", true);
                } else {
                    // $('#rdoFeMale').val(result.Gender);
                    $("#rdoFeMale").prop("checked", true);
                }

                $('#Race').val(result.Race);
                $('#Nationality').val(result.Nationality);
                $('#Religion').val(result.Religion);
                $('#Identity').val(result.IdentityBy);
                $('#ContractEmail2').val(result.ContactEmail);
                $('#IdentiAddress').val(result.IdentityCardAddress);

                $('#PersonalContractPhone1').val(result.ContactPhone1);

                $('#PersonalContractPhone2').val(result.ContactPhone2);
                $('#ContractMail').val(result.ContactEmail);
                $('#WorkPermit').val(result.WorkPermitNo);
                //$('#fileWorkPermitAttach').val(result.WorkPermitAttachFile);
                //$('#fileProfileAttach').val(result.ProfileImgAttachFile);
                // $('#CourseSkill').val(result.CourseSkill);
                $('#Education').val(result.Education);

                if (result.ThListening == "Y") {
                    $('#chkListenTH').prop('checked', true);
                }
                if (result.ThSpeaking == "Y") {
                    $('#chkSpeakingTH').prop('checked', true);
                }

                if (result.ThReading == "Y") {
                    $('#chkReadingTH').prop('checked', true);
                }

                if (result.ThWriting == "Y") {
                    $('#chkWritingTH').prop('checked', true);
                }
                if (result.EnListening == "Y") {
                    $('#chkListenEN').prop('checked', true);
                }
                if (result.EnSpeaking == "Y") {
                    $('#chkSpeakingEN').prop('checked', true);
                }
                if (result.EnReading == "Y") {
                    $('#chkReadingEN').prop('checked', true);
                }
                if (result.EnWriting == "Y") {
                    $('#chkWritingEN').prop('checked', true);
                }

                $('#CertificateType').val(result.CertificateType);
                $('#CertificateNo').val(result.CertificateNo);
                //$('#dateCertificateDate').val(result.CertificateExpireDate);
                //$('#fileCertificateAttach').val(result.CertificateAttachFile);

                if (result.CertificateExpireDate != null) {
                    var d = new Date(result.CertificateExpireDate);
                    var month = d.getMonth();
                    var day = d.getDate();
                    var year = d.getFullYear();

                    $('#datepickerCertificateDAte').data("DateTimePicker").date(moment(new Date(year, month, day), 'DD/MM/YYYY'));
                }

                $("#BankName2").val(result.BankName).change();

                $('#AccNo2').val(result.AccountNumber);
                $('#AccName2').val(result.AccountName);

                if (result.WorkPermitAttachFile != null) {
                    $('#lbfileWorkPermitAttach').text(result.WorkPermitAttachFile);
                }
                if (result.ProfileImgAttachFile != null) {
                    $('#lbfileProfileAttach').text(result.ProfileImgAttachFile);
                }
                if (result.CertificateAttachFile != null) {
                    $('#lbfileCertificateAttach').text(result.CertificateAttachFile);
                }


            }
        },
        error: function (result) {
            console.log(result);
        }

    });
}

function GetDropDownBank() {
    var urlGetBankName = url.replace('Action', 'GetBankName');
    $.ajax({
        type: 'POST',
        url: urlGetBankName,
        // data: {stateid:stateid},
        dataType: 'json',
        success: function (data) {

            $("#ddlBankName").empty();
            $("#ddlBankName").append('<option value="">--Please Select--</option>');
            $("#BankName2").empty();
            $("#BankName2").append('<option value="">--Please Select--</option>');

            $.each(data, function (id, result) {
                $("#ddlBankName").append('<option value="' + result.BankName + '">' + result.BankName + '</option>');
                $("#BankName2").append('<option value="' + result.BankName + '">' + result.BankName + '</option>');
            });
        },
        failure: function () {
            $("#ddlBankName").empty();
            $("#ddlBankName").append('<option value="">--Please All--</option>');
            $("#BankName2").empty();
            $("#BankName2").append('<option value="">--Please All--</option>');
        }
    });
}

function GetDropDownTitle() {
    var urlGetTitleName = url.replace('Action', 'GetTitleName');
    $.ajax({
        type: 'POST',
        url: urlGetTitleName,
        // data: {stateid:stateid},
        dataType: 'json',
        success: function (data) {

            $("#Title").empty();
            $("#Title").append('<option value="">--Please Select--</option>');
            $.each(data, function (id, result) {
                $("#Title").append('<option value="' + result.dropdown_text + '">' + result.dropdown_text + '</option>');
            });
        },
        failure: function () {
            $("#Title").empty();
            $("#Title").append('<option value="">--Please All--</option>');
        }
    });
}

function BindDDlBankAccountType() {
    var urlGetDataBankAccountType = url.replace('Action', 'GetDataBankAccountType');
    $.ajax({
        type: "POST",
        //async: false,
        url: urlGetDataBankAccountType,
        //data: { province_id: province },
        dataType: "json",
        async:false,
        success: function (data) {
            if (data != null) {
                $('#AccType').empty();

                $.each(data.response, function () {
                    $('#AccType').append($("<option></option>").val(this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function isEmail(email) {
    var haserror = false;
    var re = /([A-Z0-9a-z_-][^@])+?@[^$#<>?]+?\.[\w]{2,4}/.test(email);
    if (!re) {
        haserror = true;

    } else {
        haserror = false;

    }
    return haserror;
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


function padValue(value) {
    return (value < 10) ? "0" + value : value;
}