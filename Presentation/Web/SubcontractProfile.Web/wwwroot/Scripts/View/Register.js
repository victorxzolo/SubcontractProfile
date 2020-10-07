var tbLocation;
var tbRevenue;
var tbaddressstep2;
var tbaddressstep5;
var url = null;
$(document).ready(function () {
    url = $("#controllername").data("url");
    var step = "";

    $("#smartwizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {
        //alert("You are on step "+stepNumber+" now");
        step = stepNumber+1;
        if (stepPosition === 'first') {
            $("#prev-btn").addClass('disabled');
        } else if (stepPosition === 'final') {
           // $("#next-btn").addClass('disabled');
            $("#next-btn").addClass('disabled');
            $("#next-btn").hide();
            $('#btnregis').show();
            BindDataStep5();
            
        } else {
            $("#prev-btn").removeClass('disabled');
            $("#next-btn").removeClass('disabled');
            $("#next-btn").show();
            $('#btnregis').hide();
        }
    });

    // Toolbar extra buttons
    //var btnFinish = $('<button></button>').text('Finish')
    //                                 .addClass('btn btn-info')
    //                                 .on('click', function(){ alert('Finish Clicked'); });
    //var btnCancel = $('<button></button>').text('Cancel')
    //                                 .addClass('btn btn-danger')
    //                                 .on('click', function(){ $('#smartwizard').smartWizard("reset"); });


    // Smart Wizard
    $('#smartwizard').smartWizard({
        selected: 0,
        theme: 'arrows',
        transitionEffect: 'fade',
        showStepURLhash: true,


    });

    $('.sw-btn-group').hide();

    // External Button Events
    $("#reset-btn").on("click", function () {
        // Reset wizard

        $('#smartwizard').smartWizard("reset");
        $('.sw-btn-group').hide();
        return true;
    });

    $("#prev-btn").on("click", function () {
        // Navigate previous
        $('#smartwizard').smartWizard("prev");
        return true;
    });

    $("#next-btn").on("click", function () {
        // Navigate next

        if (step == 1) {
            if ($('#chktypeN').is(":checked")) {
                var resultUser= ValidateUser();
                var forms = document.getElementsByClassName('needs-validation-newregister');
                var validation = Array.prototype.filter.call(forms, function (form) {
                    if (Validate(".form-control.inputValidation", ".custom-control-input.inputValidation"
                        , ".custom-select.inputValidation", ".custom-file-input.inputValidation") || resultUser
                    ) {
                        event.preventDefault();
                        event.stopPropagation();
                        
                    }
                    else if (!Comparepassword())
                    {
                        return false;
                    }
                    else {
                        Loading();
                        BindRegion();
                        BindAddressType();
                        BindDDLprovince();
                        BindDDLdistrict();
                        BindDDLsubdistrict();
                        inttbAddress2();
                        $('#smartwizard').smartWizard("next");
                       
                        return true;
                       
                    }
                    form.classList.add('was-validated');
                });
            }
            else if ($('#chktypeD').is(":checked")) {
                var resultUser = ValidateUser();
                var forms = document.getElementsByClassName('needs-validation-dealer');
                var validation = Array.prototype.filter.call(forms, function (form) {
                    if (Validate(".form-control.inputValidationdealer", ".custom-control-input.inputValidationdealer"
                        , ".custom-select.inputValidationdealer", ".custom-file-input.inputValidationdealer") || resultUser
                    ) {
                        event.preventDefault();
                        event.stopPropagation();

                    }
                    else if (!Comparepassword()) {
                        return false;
                    }
                    else {
                        Loading();
                        BindRegion();
                        BindAddressType();
                        BindDDLprovince();
                        BindDDLdistrict();
                        BindDDLsubdistrict();
                        inttbAddress2();
                        $('#smartwizard').smartWizard("next");
                       
                        return true;
                      

                    }
                    form.classList.add('was-validated');
                });
            }
        }
        else if (step == 2) {
            //var forms = document.getElementsByClassName('needs-validation-step2-table');
            //var validation = Array.prototype.filter.call(forms, function (form) {
                if (tbaddressstep2.data().count()==0) {
                    //event.preventDefault();
                    //event.stopPropagation();
                    bootbox.alert({
                        title: "System Information",
                        message: localizedData.JSErrorAddress,
                        size: "small",
                        callback: function (result) {
                            console.log('This was logged in the callback: ' + result);
                        }
                    });
                }
                else {
                    Loading();
                    $('#smartwizard').smartWizard("next");

                    return true;

                }
                //form.classList.add('was-validated');
            //});
        }
        else if (step == 3) {
            var forms = document.getElementsByClassName('needs-validation-step3');
            var validation = Array.prototype.filter.call(forms, function (form) {
                if (Validate(".form-control.inputValidationContract", ".custom-control-input.inputValidationContract"
                    , ".custom-select.inputValidationContract", ".custom-file-input.inputValidationContract") ||
                    (isEmail($('#txtcompany_Email').val()) || isEmail($('#txtcontract_email').val()))
                ) {
                    event.preventDefault();
                    event.stopPropagation();

                }
                else {
                    Loading();
                    BindDDLBank();
                    BindDDLCompanyType();
                    BindDDlBankAccountType();
                   
                    $('#smartwizard').smartWizard("next");
                    
                    return true;

                }
                form.classList.add('was-validated');
            });
        }
        else if (step == 4) {

            var forms = document.getElementsByClassName('needs-validation-step4');
            var validation = Array.prototype.filter.call(forms, function (form) {
                if (Validate(".form-control.inputValidationBank", ".custom-control-input.inputValidationBank"
                    , ".custom-select.inputValidationBank",".custom-file-input.inputValidationBank")) {
                    event.preventDefault();
                    event.stopPropagation();

                }
                else {
                    Loading();
                    inttbAddress5();
                    $('#smartwizard').smartWizard("next");
                    return true;

                }
                form.classList.add('was-validated');
            });
        }

        
        
        
       
    });


    $('#btnTestNas').click(function () {
        $.ajax({
            type: "POST",
            async: false,
            url: "/CompanyProfile/TestNAS",
            dataType: "json",
            //data: { location_id: lo_id },
            success: function (data) {

                console.log(data);
            },
            error: function (xhr) {
                Loading(0);
                console.log(xhr);
            }
        });
    });


/*Step1*/
    BindDDLTitle();
    inttbRevenue();
    inittaLocation();
    $('#btnsearchlocation').click(function () {
        $('#Searchlocation').modal('show');
        $('#txtlocationcodemodal').val($('#txtlocationcode').val());
        $("#btn_search_modal").trigger("click");
    });

    $('#btn_reset_modal').click(function () {
        ClearDataModalLocation();
    });

    $('#tblocationModal tbody').on('click', 'tr', function () {
       // $(this).toggleClass('selected');
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tbLocation.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    $('#chktypeN').on("change", function () {
        if ($(this).attr("value") == "NewSubContract") {
            $("#divdealer").hide('slow');
            $('#divnewsubcontract').show('slow');
        }
    });

    $('#btn_select_location').click(function () {
      Loading();
        var value = tbLocation.rows('.selected').data();
        var lo_id = value[0].location_id;

        var urlLocation = url.replace('Action', 'GetLocationSession');

        $.ajax({
            type: "POST",
            async: false,
            url: urlLocation,
            dataType: "json",
            data: { location_id: lo_id},
            success: function (data) {
                Loading(0);
                if (data.Response.Status) {
                    $('#txtlocationcode').val(data.LocationListModel[0].outLocationCode);
                    $('#txtlocationname').val(data.LocationListModel[0].outLocationName);
                    $('#txtdistribution').val(data.LocationListModel[0].outDistChn);
                    $('#txtchannelsalegroup').val(data.LocationListModel[0].outChnSales);
                    $('#txttax_id_dealer').val(data.LocationListModel[0].outTaxId);
                    $('#txtcompany_alias_dealer').val(data.LocationListModel[0].outCompanyShortName);

                    if ($('#ddlprefixcompany_name_th_dealer option:selected').text() == data.LocationListModel[0].outTitle) {
                        $('#ddlprefixcompany_name_th_dealer').text(data.LocationListModel[0].outTitle).change();
                    } 

                    $('#txtcompany_name_th_dealer').val(data.LocationListModel[0].outCompanyName);

                    if ($('#ddlprefixcompany_name_en_dealer option:selected').text() == data.LocationListModel[0].outTitle) {
                        $('#ddlprefixcompany_name_en_dealer').text(data.LocationListModel[0].outTitle).change();
                    } 

                    $('#txtcompany_name_en_dealer').val();
                    $('#txtwt_name_dealer').val(data.LocationListModel[0].outWTName);

                    if (data.LocationListModel[0].outVatType == "VAT") {
                        $('#chkvat_typeT_dealer').prop('checked', true);
                    }
                    else if (data.LocationListModel[0].outVatType == "NON_VAT"){
                        $('#chkvat_typeE_dealer').prop('checked', true);
                    }

                    //if (data.LocationListModel[0].ASCList != null) {
                    //    var dataASC = data.LocationListModel[0].ASCList;
                    //    $('#txtasccode').val(dataASC[0].outASCCode);
                    //}

                    //if (data.locationListModel[0].addressLocationList != null) {
                    //    var stuff = [];
                    //    jQuery.each(data.locationListModel[0].addressLocationList, function (i, val) {
                    //        var address_type_name = "";
                    //        $(':checkbox').each(function (i) {

                    //            if (val.outAddressType == $(this).val()) {
                    //                address_type_name = $(this).parent().text().trim();
                    //            }
                    //        });
                    //        var data = {
                    //            AddressTypeId: val.outAddressType,
                    //            address_type_name: address_type_name,
                    //            Country: val.outCountry,
                    //            ZipCode: val.outZipcode,
                    //            HouseNo: val.outHouseNo,
                    //            Moo: val.outMoo,
                    //            VillageName: val.outMooban,
                    //            Building: val.outBuilding,
                    //            Floor: val.outFloor,
                    //            RoomNo: val.outRoom,
                    //            Soi: val.outSoi,
                    //            Road: val.outStreet,
                    //            SubDistrictId: 0,
                    //            sub_district_name: val.outTumbol,
                    //            DistrictId: 0,
                    //            district_name: val.outAmphur,
                    //            ProvinceId: 0,
                    //            province_name: val.outProvince,
                    //            RegionId: 0,
                    //            outFullAddress: val.outFullAddress,
                    //            location_code: $('#txtlocationcode').val()
                    //        }
                    //        stuff.push(data);
                    //    });
                    //    SaveDaftAddress(stuff);
                    //}
                }

            },
            error: function (xhr) {
                Loading(0);
                console.log(xhr);
             }
        });

      

        ClearDataModalLocation();
        $('#Searchlocation').modal('hide');

    });

    $('#chktypeD').on("change", function () {
        if ($(this).attr("value") == "Dealer") {
            $("#divnewsubcontract").hide('slow');
            $("#divdealer").show('slow');
        }
    });

    $('#btn_search_modal').click(function () {

        var forms = document.getElementsByClassName('needs-validation-modalLocation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            if (Validate(".form-control.inputValidationlocationModal", ".custom-control-input.inputValidationlocationModal")) {
                event.preventDefault();
                event.stopPropagation();
            }
            else {
                //inittaLocation();
                tbLocation.ajax.reload();

            }
            form.classList.add('was-validated');
        });
    });

    $('#txtcreatepass, #txtconfirmpass').on('keyup', function () {
        if ($('#txtcreatepass').val() == $('#txtconfirmpass').val()) {
            $('#message').html('Matching').css('color', 'green');
        } else
            $('#message').html('Not Matching').css('color', 'red');
    });

    $('#btnsearchrevenue').click(function () {
        $('#SearchRevenue').modal('show');
        $('#txtsearchrevenue').val($('#txttax_id').val());
        $("#btn_search_revenue_modal").trigger("click");
    });
    $('#btn_reset_revenue_modal').click(function () {
        ClearDataModalRevenue()
    });
    $('#btn_search_revenue_modal').click(function () {
        tbRevenue.ajax.reload();
        //inttbRevenue();
    });
    $('#tbrevenueModal tbody').on('click', 'tr', function () {
        // $(this).toggleClass('selected');
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tbRevenue.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
    $('#btn_select_revenue').click(function () {
        var value = tbRevenue.rows('.selected').data();
        var titlename = value[0].vtitleName;
        var companyname = value[0].vName;
        var tax_id = value[0].vNID;

            $('#txtcompany_name_th').val(companyname);
        $('#txttax_id').val(tax_id);
        $("#ddlprefixcompany_name_th option").filter(function () {
            //may want to use $.trim in here
            return $(this).text() == titlename;
        }).prop('selected', true);
        ClearDataModalRevenue();
        $('#SearchRevenue').modal('hide');
    });

    $('#txtcreateEmail').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#erroremail').show();
        }
        else {
            $('#erroremail').hide();
        }
    });

    $("#txttax_id").keyup(function () {
        CheckKeyUps("txttax_id", "[0-9]");
    });
   

 

/*************************************/


/*Step2*/
    //BindDDLprovince();
    //BindDDLdistrict();
    //BindDDLsubdistrict();


    $('#btnaddaddress').click(function () {
       
        var stuff = [];
        var countchk = [];

        $(':checkbox:checked').each(function (i) {
           
            //val[i] = $(this).parent().text().trim();

            var data = {
                AddressTypeId: $(this).val(),
                address_type_name: $(this).parent().text().trim(),
                Country: $('#ddlcountry option').filter(':selected').val(),
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
                 location_code: $('#txtlocationcode').val()
            }
            stuff.push(data);
            countchk.push($(this).val());
            
        });


        var forms = document.getElementsByClassName('needs-validation-step2');
        var validation = Array.prototype.filter.call(forms, function (form) {
            var failed = false;

            if ($("[name='address_type_id']:checked").length == 0) {
                $("[name='address_type_id']").attr('required', true);
                failed = true;
            }
            else {
                $("[name='address_type_id']").attr('required', false);
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

    $('#ddlzone').change(function(){

        BindDDLprovince($('#ddlzone option').filter(':selected').val());
    });

    $('#ddlprovince').change(function () {

        BindDDLdistrict($('#ddlprovince option').filter(':selected').val());
    });
    $('#ddldistrict').change(function () {
        BindDDLsubdistrict($('#ddldistrict option').filter(':selected').val());
    });

    $('#tbaddressstep2').on('click', 'tbody .edit_btn', function () {
        var data_row = tbaddressstep2.row($(this).closest('tr')).data();
        tbaddressstep2.row($(this).closest('tr')).remove().draw();//เดี๋ยวกลับมาทำ 

        var urlAddress = url.replace('Action', 'GetDaftAddress');

        $.ajax({
            type: "POST",
            async: false,
            url: urlAddress,
            data: { AddressId: data_row.addressId },
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

                    $.each(data.response, function () {
                        $('#ddlcountry').val(this.Country)

                        $('#ddlprovince').val(this.ProvinceId)
                        $('#ddlprovince').trigger('change');

                        $('#ddldistrict').val(this.DistrictId)
                        $('#ddldistrict').trigger('change');

                        $('#ddlsubdistrict').val(this.SubDistrictId)

                        $('#ddlzipcode').val(this.ZipCode)

                        $('#ddlzone').val(this.RegionId)

                        $('#txthomenumber').val(this.HouseNo)
                        $('#txtVillageNo').val(this.Moo)
                        $('#txtvillage').val(this.VillageName)
                        $('#txtbuilding').val(this.Building)
                        $('#txtfloor').val(this.Floor)
                        $('#txtroom').val(this.RoomNo)
                        $('#txtsoi').val(this.Soi)
                        $('#txtroad').val(this.Road)

                        var addr_type_id = this.AddressTypeId
                        $(':checkbox').each(function (i) {
                           
                            if (addr_type_id == $(this).val()) {
                                $(this).prop('checked', true);
                            }
                        });

                        
                    });

                  
                }
                else {
                    bootbox.alert({
                        title: "System Information",
                        message: data.message,
                        size: "small",
                        callback: function (result) {
                            console.log('This was logged in the callback: ' + result);
                        }
                    });
                }
            },
            error: function (xhr) {
                //Loading(0);
                console.log(xhr);
            }
        });
    });

    $('#tbaddressstep2').on('click', 'tbody .delete_btn', function () {
        var data_row = tbaddressstep2.row($(this).closest('tr')).data();

        var urlAddress = url.replace('Action', 'DeleteDaftAddress');

        $.ajax({
            type: "POST",
            async: false,
            url: urlAddress,
            data: { AddressId: data_row.addressId },
            dataType: "json",
            success: function (data) {
                if (data.status) {
                    var val = []
                        val= ConcatstrAddress(data.response);
                    tbaddressstep2.clear().draw();
                    BindDatatable(tbaddressstep2, val);
                }
                else {
                    bootbox.alert({
                        title: "System Information",
                        message: data.message,
                        size: "small",
                        callback: function (result) {
                            console.log('This was logged in the callback: ' + result);
                        }
                    });
                }
            },
            error: function (xhr) {
                //Loading(0);
                console.log(xhr);
               }
        });
    });

    $('#btnresetaddress').click(function () {
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


/*************************************/

/*Step3*/
    $('#txtcompany_Email').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errorcompany_Email').show();
        }
        else {
            $('#errorcompany_Email').hide();
        }
    });
    $('#txtcontract_email').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errorcontract_email').show();
        }
        else {
            $('#errorcontract_email').hide();
        }
    });
    $('#txtdept_of_install_email').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errorinstall_email').show();
        }
        else {
            $('#errorinstall_email').hide();
        }
    });
    $('#txtdept_of_mainten_email').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errormainten_email').show();
        }
        else {
            $('#errormainten_email').hide();
        }
    });
    $('#txtdept_of_Account_email').on('keypress', function () {
        if (isEmail(this.value)) {
            $('#errorAccount_email').show();
        }
        else {
            $('#errorAccount_email').hide();
        }
    });


/*************************************/

/*Step4*/
    

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

    $('#company_certified_file').change(function () {

        uploadFiles('company_certified_file')
    });

    $('#commercial_registration_file').change(function () {
        uploadFiles('commercial_registration_file')
    });

    $('#vat_registration_certificate_file').change(function () {
        uploadFiles('vat_registration_certificate_file')
    });

    $('#ddlBankname').change(function () {
        var v_bankcode = $('#ddlBankname option').filter(':selected').val();
        $('#txtbank_Code').val(v_bankcode);
    });

  

/*Step5*/

  


    $('#btnregis').click(function (){
        Loading();
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
        var username = null;
        var password = null;



        if ($("#chktypeN").is(":checked")) {
            chksubcontract_type = $("#chktypeN").val();
           // distribution_channel = $('#ddldistribution option').filter(':selected').val();
            //channel_sale_group = $('#ddlchannelsalegroup option').filter(':selected').val();
            tax_id = $('#txttax_id').val();
            company_alias = $('#txtcompany_alias').val();
            company_title_name_th = $('#ddlprefixcompany_name_th option').filter(':selected').val();
            company_name_th = $('#txtcompany_name_th').val();
            company_title_name_en = $('#ddlprefixcompany_name_en option').filter(':selected').val();
            company_name_en = $('#txtcompany_name_en').val();
            wt_name = $('#txtwt_name').val();
            vat_type = $('#chkvat_typeT').is(':checked') ? $('#chkvat_typeT').val() : $('#chkvat_typeE').val();
            username = $('#txtcreateuser').val();
            password = $('#txtconfirmpass').val();
        }
        else if ($("#chktypeD").is(":checked")) {
            chksubcontract_type = $("#chktypeD").val();
            distribution_channel = $('#txtdistribution').val();
            channel_sale_group = $('#txtchannelsalegroup').val();
            tax_id = $('#txttax_id_dealer').val();
            company_alias = $('#txtcompany_alias_dealer').val();
            company_title_name_th = $('#ddlprefixcompany_name_th_dealer option').filter(':selected').val();
            company_name_th = $('#txtcompany_name_th_dealer').val();
            company_title_name_en = $('#ddlprefixcompany_name_en_dealer option').filter(':selected').val();
            company_name_en =$('#txtcompany_name_en_dealer').val();
            wt_name = $('#txtwt_name_dealer').val();
            vat_type = $('#chkvat_typeT_dealer').is(':checked') ? $('#chkvat_typeT_dealer').val() : $('#chkvat_typeE_dealer').val();
            username = $('#txtcreateuser').val();
            password = $('#txtconfirmpass').val();
        }
        var straccname = $('#ddlaccount_Name option').filter(':selected').val() != '' ?
            $('#ddlaccount_Name option').filter(':selected').text() + ' ' + $('#txtaccount_Name').val() : $('#txtaccount_Name').val()


        var fData = new FormData();
        fData.append("CompanyNameTh", company_name_th);
        fData.append("CompanyNameEn", company_name_en);
        fData.append("CompanyAlias", company_alias);
        fData.append("DistributionChannel", distribution_channel);
        fData.append("ChannelSaleGroup", channel_sale_group);
        fData.append("TaxId", tax_id);
        fData.append("WtName", wt_name);
        fData.append("VatType", vat_type);
        fData.append("CompanyEmail", $('#txtcompany_Email').val());
        fData.append("ContractName", $('#txtcontract_name').val());
        fData.append("ContractPhone", $('#txtcontract_phone').val());
        fData.append("ContractEmail", $('#txtcontract_email').val());

        fData.append("BankCode", $('#ddlBankname option').filter(':selected').val());
        fData.append("BankName", $('#ddlBankname option').filter(':selected').text());
        fData.append("AccountNumber", $('#txtaccount_Number').val());
        fData.append("AccountName", straccname);
        fData.append("BranchCode", $('#txtbranch_Code').val());
        fData.append("BranchName", $('#txtbranch_Name').val());
        fData.append("DeptOfInstallName", $('#txtdept_of_install_name').val());
        fData.append("DeptOfMaintenName", $('#txtdept_of_mainten_name').val());
        fData.append("DeptOfAccountName", $('#txtdept_of_Account_name').val());
        fData.append("DeptOfInstallPhone", $('#txtdept_of_install_phone').val());
        fData.append("DeptOfMaintenPhone", $('#txtdept_of_mainten_phone').val());
        fData.append("DeptOfAccountPhone", $('#txtdept_of_Account_phone').val());
        fData.append("DeptOfInstallEmail", $('#txtdept_of_install_email').val());
        fData.append("DeptOfMaintenEmail", $('#txtdept_of_mainten_email').val());
        fData.append("DeptOfAccountEmail", $('#txtdept_of_Account_email').val());
        fData.append("LocationCode", $('#txtlocationcode').val());
        fData.append("LocationNameTh", $('#txtlocationname').val());
        fData.append("LocationNameEn", $('#txtlocationname').val());
        fData.append("BankAccountTypeId", $('#ddlbank_account_type option').filter(':selected').val());
        fData.append("SubcontractProfileType", chksubcontract_type);
        fData.append("CompanyTitleThId", company_title_name_th);
        fData.append("CompanyTitleEnId", company_title_name_en);
        fData.append("User_name", username);
        fData.append("Password", password);

        fData.append("FileCompanyCertified", $('#company_certified_file').get(0).files[0]);
        fData.append("FileCommercialRegistration", $('#commercial_registration_file').get(0).files[0]);
        fData.append("FileVatRegistrationCertificate", $('#vat_registration_certificate_file').get(0).files[0]);

        fData.append("CompanyCertifiedFile", $('#lbcompany_certified_file').text());
        fData.append("CommercialRegistrationFile", $('#lbcommercial_registration_file').text());
        fData.append("VatRegistrationCertificateFile", $('#lbvat_registration_certificate_file').text());

        //var data = {
        //    //CompanyId
        //    //CompanyCode
        //    //CompanyName
        //    CompanyNameTh: company_name_th,
        //    CompanyNameEn: company_name_en,
        //    CompanyAlias: company_alias,
        //    DistributionChannel: distribution_channel,
        //    ChannelSaleGroup: channel_sale_group,
        //    //VendorCode: $('#txtvendercode').val(),
        //    TaxId: tax_id,
        //    WtName: wt_name,
        //    VatType: vat_type,
        //    CompanyEmail: $('#txtcompany_Email').val(),
        //    ContractName: $('#txtcontract_name').val(),
        //    ContractPhone: $('#txtcontract_phone').val(),
        //    ContractEmail: $('#txtcontract_email').val(),

        //    BankCode: $('#ddlBankname option').filter(':selected').val(),
        //    BankName: $('#ddlBankname option').filter(':selected').text(),
        //    AccountNumber:$('#txtaccount_Number').val(),
        //    AccountName: straccname,//$('#ddlaccount_Name option').filter(':selected').val(),
        //   // AttachFile
        //    BranchCode: $('#txtbranch_Code').val(),
        //    BranchName: $('#txtbranch_Name').val(),

        //    DeptOfInstallName: $('#txtdept_of_install_name').val(),
        //    DeptOfMaintenName: $('#txtdept_of_mainten_name').val(),
        //    DeptOfAccountName: $('#txtdept_of_Account_name').val(),

        //    DeptOfInstallPhone: $('#txtdept_of_install_phone').val(),
        //    DeptOfMaintenPhone: $('#txtdept_of_mainten_phone').val(),
        //    DeptOfAccountPhone: $('#txtdept_of_Account_phone').val(),

        //    DeptOfInstallEmail: $('#txtdept_of_install_email').val(),
        //    DeptOfMaintenEmail: $('#txtdept_of_mainten_email').val(),
        //    DeptOfAccountEmail: $('#txtdept_of_Account_email').val(),

        //    LocationCode: $('#txtlocationcode').val(),
        //    LocationNameTh: $('#txtlocationname').val(),
        //    LocationNameEn: $('#txtlocationname').val(),

        //    BankAccountTypeId: $('#ddlbank_account_type option').filter(':selected').val(),
        //    SubcontractProfileType: chksubcontract_type,
        //    CompanyTitleThId: company_title_name_th,
        //    CompanyTitleEnId: company_title_name_en,
        //    User_name: username,
        //    Password: password
        //    //ASCCode: $('#txtasccode').val()

        //}
        var urlNewRegister = url.replace('Action', 'NewRegister');
        
        $.ajax({
            type: "POST",
            async: false,
            url: urlNewRegister,
            data: fData,
            processData: false,
            contentType: false,
            success: function (data) {
                Loading(0);
                console.log(data)
                if (data.Response.Status) {
                    bootbox.alert({
                        title: "System Information",
                        message: data.Response.Message,
                        size: "small",
                        callback: function (result) {
                            console.log('This was logged in the callback: ' + result);
                        }
                    });
                    var urlredirec = url.replace('Action', 'Login');
                    window.location.href = urlredirec;
                }
                else {
                    bootbox.alert({
                        title: "System Information",
                        message: data.Response.Message,
                        size: "small",
                        callback: function (result) {
                            console.log('This was logged in the callback: ' + result);
                        }
                    });
                }
            },
            error: function (xhr) {
                Loading(0);
                console.log(xhr);
            }
        });
    });



/*************************************/
});

function inittaLocation() {

    var urlSearchLocation = url.replace('Action', 'SearchLocation');

     tbLocation = $('#tblocationModal').DataTable({
        ordering: true,
        select: true,
        retrieve: true,
        paging: true,
        pagingType: "full_numbers",
        destroy: true,
        searching: false,
        //pageLength: 10,
        proccessing: true,
        serverSide: true,
        dom: 'rt<"float-left"p><"float-left"l><"float-right"i>',
        ajax: {
            type: "POST",
            async: false,
            url: urlSearchLocation,
            data: {
                asc_code: function () { return $('#txtasccodemodal').val() },
                asc_mobile_no: function () { return $('#txtmobilenomodal').val() },
                id_Number: function () { return $('#txtidnumbermodal').val() },
                location_code: function () { return $('#txtlocationcodemodal').val() },
                sap_code: function () { return $('#txtsapcodemodal').val() },
                user_id: function () { return $('#txtuseridmodal').val() }
            },
            dataType: "json",
            error: function (xhr, status, error) {
                Loading(0);
                console.log(xhr);
            }

        },
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        //lengthChange: false,
        columns: [
            { "data": "outCompanyName" },
            { "data": "outCompanyShortName" },
            { "data": "outTaxId" },
            { "data": "outLocationCode" },
            { "data": "outLocationName" },
            { "data": "outDistChn" },
            { "data": "outChnSales" },
            { "data": "location_id", "visible": false }
        ],
        language: {
            infoEmpty: "No items to display",
            lengthMenu: "_MENU_ items per page",
            zeroRecords: "Nothing found",
            info: "_START_ - _END_  of _TOTAL_  items",
            infoFiltered: "",
            paginate: {
                previous: "<",
                next: ">",
                last: ">|",
                first: "|<"
            }
        }
    });
}

function inttbRevenue() {

    var urlGetRevenue = url.replace('Action', 'GetRevenue');

    tbRevenue = $('#tbrevenueModal').DataTable({
        ordering: true,
        select: true,
        retrieve: true,
        paging: true,
        pagingType: "full_numbers",
        destroy: true,
        searching: false,
        //pageLength: 10,
        proccessing: true,
        serverSide: true,
        dom: 'rt<"float-left"p><"float-left"l><"float-right"i>',
        ajax: {
            type: "POST",
            async: false,
            url: urlGetRevenue,
            data: {
                tIN: function () { return $('#txtsearchrevenue').val() }
            },
            dataType: "json",
            error: function (xhr) {
                Loading(0);
                console.log(xhr);
            }

        },
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        //lengthChange: false,
        columns: [

            { "data": "vNID" },
            { "data": "vName" },
            { "data": "vBranchName" },
            { "data": "outConcataddr" },
            { "data": "vtitleName", "visible": false }

        ],
        language: {
            infoEmpty: "No items to display",
            lengthMenu: "_MENU_ items per page",
            zeroRecords: "Nothing found",
            info: "_START_ - _END_  of _TOTAL_  items",
            infoFiltered: "",
            paginate: {
                previous: "<",
                next: ">",
                last: ">|",
                first: "|<"
            }
        }
    });
}

function ClearDataModalRevenue() {
    $('#txtsearchrevenue').val('');
    tbRevenue.ajax.reload();
}

function BindDatatable(table, datamodel) {
    //console.log("BindDatatable");
    //console.log(datamodel);
    //table.data(datamodel);

    table.rows.add(datamodel).draw();

}

function ClearDataModalLocation() {
    $('#txtasccodemodal').val('')
    $('#txtmobilenomodal').val('')
    $('#txtlocationcodemodal').val('')
    $('#txtidnumbermodal').val('')
    $('#txtsapcodemodal').val('')
    $('#txtuseridmodal').val('')
    tbLocation.ajax.reload();
}

function Comparepassword() {
    if (($('#txtcreatepass').val() != "" && $('#txtconfirmpass').val() != "") && ($('#txtcreatepass').val() == $('#txtconfirmpass').val())) {
        return true
    } else {
        return false
    }

}


function inttbAddress2() {

    var urlSearchAddress = url.replace('Action', 'SearchAddress');

     tbaddressstep2 = $('#tbaddressstep2').DataTable({
        ordering: true,
        order: [[1, "asc"]],
        select: true,
        retrieve: true,
        //pagingType: "full_numbers",
        destroy: true,
        searching: false,
        proccessing: true,
        serverSide: true,
        paging: false,
        //lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        dom: 'rt<"float-right"i>',
        ajax: {
            type: "POST",
            async: false,
            url: urlSearchAddress,
            dataSrc: function (data) {
                Loading(0);
                var val = []
                val = ConcatstrAddress(data.data);
                data.data = val;
                return data.data;
            },
            error: function (xhr) {
                Loading(0);
                console.log(xhr);
            }

        },
        columns: [
            { "data": "addressId", "visible": false, "width": "0%"},
            { "data": "address_type_id", "visible": false, "width": "0%" },
            { "data": "address_type", orderable: true, "width": "30%"},
            { "data": "address", orderable: true, "width": "50%" },
            {
                "width": "10%",
                "targets": -2,
                "data": null,
                orderable: false,
                "defaultContent": "<button class='btn-border btn-green edit_btn'><i class='fa fa-edit icon'></i><span>" + localizedData.JSEdit+"</span></button>"
            },
            {
                "width": "10%",
                "targets": -1,
                "data": null,
                orderable: false,
                "defaultContent": "<button class='btn-border btn-black delete_btn'><i class='fa fa-trash icon'></i><span>" + localizedData.JSDelete +"</span></button>"
            }
        ],
        language: {
            infoEmpty: "No items to display",
            //lengthMenu: "_MENU_ items per page",
            zeroRecords: "Nothing found",
            info: "_START_ - _END_  of _TOTAL_  items",
            //infoFiltered: "",
            //paginate: {
            //    previous: "<",
            //    next: ">",
            //    last: ">|",
            //    first: "|<"
            //}

        }
     });


    tbaddressstep2.columns.adjust().draw();
}

function uploadFiles(inputId) {
    Loading();
    var input = document.getElementById(inputId);
    //var files = input.files;
    var files = input.files[0];
    var formData = new FormData();
    var id = "";
    var typefile = "";
    //switch (inputId) {
    //    case "company_certified_file": id = $('#hdupfilecompany_certified').val();
    //        typefile = "CompanyCertifiedFile";
    //        break;
    //    case "commercial_registration_file": id = $('#hdupfilecommercial_registration').val();
    //        typefile = "CommercialRegistrationFile";
    //        break;
    //    case "vat_registration_certificate_file": id = $('#hdupfilevat_registration_certificate').val();
    //        typefile = "VatRegistrationCertificateFile";
    //        break;
    //}
    //for (var i = 0; i != files.length; i++) {
    //    formData.append("files", files[i]);
    //    formData.append("fid", id);
    //    formData.append("type_file", typefile);
    //}
    formData.append("files", files);

    //var urlUploadFile = url.replace('Action', 'UploadFile');
    var urlUploadFile = url.replace('Action', 'CheckFileUpload');
    $.ajax(
        {
            type: "POST",
            url: urlUploadFile,
            processData: false,
            contentType: false,
            dataType: "json",
            data: formData,
            success: function (data) {
                Loading(0);
                console.log(data);
                if (data.status) {
                    switch (inputId) {
                        case "company_certified_file": $('#hdupfilecompany_certified').val(data.file_id); break;
                        case "commercial_registration_file": $('#hdupfilecommercial_registration').val(data.file_id); break;
                        case "vat_registration_certificate_file": $('#hdupfilevat_registration_certificate').val(data.file_id); break;
                    }
                    bootbox.alert({
                        title: "System Information",
                        message: data.message,
                        size: "small",
                        callback: function (result) {
                            console.log('This was logged in the callback: ' + result);
                        }
                    });
                }
                else {

                    bootbox.alert({
                        title: "System Information",
                        message: data.message,
                        size: "small",
                        callback: function (result) {
                            console.log('This was logged in the callback: ' + result);
                        }
                    });
                    switch (inputId) {
                        case "company_certified_file": $('#company_certified_file').val('');
                            $('#lbcompany_certified_file').text(localizedData.JSChooseFile); break;
                        case "commercial_registration_file": $('#commercial_registration_file').val('');
                            $('#lbcommercial_registration_file').text(localizedData.JSChooseFile); break;
                        case "vat_registration_certificate_file": $('#vat_registration_certificate_file').val('');
                            $('#lbvat_registration_certificate_file').text(localizedData.JSChooseFile); break;
                    }
                }

            },
            error: function (xhr) {
                Loading(0);
                console.log(xhr);
            }
        }
    );
}

function inttbAddress5() {

    tbaddressstep5 = $('#tbaddressstep5').DataTable({
        ordering: true,
        order: [[1, "asc"]],
        select: true,
        retrieve: true,
        paging: false,
        destroy: true,
        searching: false,
        //scrollY: 400,
        processing: true,
        //scrollY: false,
        //lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        columns: [
            { "data": "address_type_id", "visible": false },
            { "data": "address_type", orderable: true, },
            { "data": "address", orderable: true, }
        ],
        language: {
            infoEmpty: "No items to display",
            //lengthMenu: "_MENU_ items per page",
            zeroRecords: "Nothing found",
            info: "_START_ - _END_  of _TOTAL_  items",
            //infoFiltered: "",
            //paginate: {
            //    previous: "<",
            //    next: ">",
            //    last: ">|",
            //    first: "|<"
            //}

        }
    });
}

function BindDataAddress() {

    var urlGetDaftAddress = url.replace('Action', 'GetDaftAddress');

    Loading();
    $.ajax({
        type: "POST",
        async: false,
        url: urlGetDaftAddress,
        data: { address_type_id: null },
        dataType: "json",
        success: function (data) {
            Loading(0);
            if (data.status) {
                var val = []
                val = ConcatstrAddress(data.response);
                tbaddressstep5.clear().draw();
                BindDatatable(tbaddressstep5, val);
            }
            else {
                bootbox.alert({
                    title: "System Information",
                    message: data.message,
                    size: "small",
                    callback: function (result) {
                        console.log('This was logged in the callback: ' + result);
                    }
                });
            }
        },
        error: function (xhr) {
            Loading(0);
            console.log(xhr);
        }
    });
}

function BindDataStep5() {

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

    if ($("#chktypeN").is(":checked")) {
        chksubcontract_type = $("#chktypeN").parent().text().trim();

        //distribution_channel = $('#ddldistribution option').filter(':selected').text();
        // channel_sale_group = $('#ddlchannelsalegroup option').filter(':selected').text();

        tax_id = $('#txttax_id').val();
        company_alias = $('#txtcompany_alias').val();

        company_title_name_th = $('#ddlprefixcompany_name_th option').filter(':selected').val() != '' ?
                                $('#ddlprefixcompany_name_th option').filter(':selected').text():'';
        company_name_th = $('#txtcompany_name_th').val();

        company_title_name_en = $('#ddlprefixcompany_name_en option').filter(':selected').val() != '' ?
                            $('#ddlprefixcompany_name_en option').filter(':selected').text():'';
        company_name_en = $('#txtcompany_name_en').val();

        wt_name = $('#txtwt_name').val();
        vat_type = $('#chkvat_typeT').is(':checked') ? $('#chkvat_typeT').parent().text().trim() : $('#chkvat_typeE').parent().text().trim();

        
        //$('#lbdistribution_channel').text(distribution_channel);
        //$('#lbchannel_sale_group').text(channel_sale_group);
        $('#lbtax_id').text(tax_id);
        $('#lbcompany_alias').text(company_alias);
        $('#lbcompany_name_th').text(company_title_name_th + ' ' + company_name_th);
        $('#lbcompany_name_en').text(company_title_name_en + ' ' + company_name_en);
        $('#lbwt_name').text(wt_name);
        $('#vat_type').text(vat_type);

        $('#divnewsubcontract5').show('slow');
        $("#divdealer5").hide('slow');
    }
    else if ($("#chktypeD").is(":checked")) {
        chksubcontract_type = $('#chktypeD').parent().text().trim();

        distribution_channel = $('#txtdistribution').val();
        channel_sale_group = $('#txtchannelsalegroup').val();

        tax_id = $('#txttax_id_dealer').val();
        company_alias = $('#txtcompany_alias_dealer').val();

        company_title_name_th = $('#ddlprefixcompany_name_th_dealer option').filter(':selected').val() != '' ?
                                $('#ddlprefixcompany_name_th_dealer option').filter(':selected').text():'';
        company_name_th = $('#txtcompany_name_th_dealer').val();

        company_title_name_en = $('#ddlprefixcompany_name_en_dealer option').filter(':selected').val() != '' ?
                            $('#ddlprefixcompany_name_en_dealer option').filter(':selected').text():'';
        company_name_en = $('#txtcompany_name_en_dealer').val();

        wt_name = $('#txtwt_name_dealer').val();
        vat_type = $('#chkvat_typeT_dealer').is(':checked') ? $('#chkvat_typeT_dealer').parent().text().trim() : $('#chkvat_typeE_dealer').parent().text().trim();
        $('#lblocationcode').text($('#txtlocationcode').val());
        $('#lbdistributionDealer').text(distribution_channel);
        $('#lblocationname').text($('#txtlocationname').val());
        $('#lbchannelsalegroupDealer').text(channel_sale_group);
        //$('#lbvendercode').text($('#txtvendercode').val());
       // $('#lbasccode').text($('#txtasccode').val());

        $('#lbtax_idDealer').text(tax_id);
        $('#lbcompany_aliasDealer').text(company_alias);
        $('#lbcompany_name_thDealer').text(company_title_name_th + ' ' + company_name_th);
        $('#lbcompany_name_enDealer').text(company_title_name_en + ' ' + company_name_en);
        $('#lbwt_nameDealer').text(wt_name);
        $('#vat_typeDealer').text(vat_type);

        $("#divnewsubcontract5").hide('slow');
        $("#divdealer5").show('slow');
    }
    $('#lbsubcontract_profile_type').text(chksubcontract_type);
   

    $('#lbcompany_Email').text($('#txtcompany_Email').val());
    $('#lbcontract_name').text($('#txtcontract_name').val());
    $('#lbcontract_phone').text($('#txtcontract_phone').val());
    $('#lbcontract_email').text($('#txtcontract_email').val());

    $('#lbdept_of_install_name').text($('#txtdept_of_install_name').val());
    $('#lbdept_of_install_phone').text($('#txtdept_of_install_phone').val());
    $('#lbdept_of_install_email').text($('#txtdept_of_install_email').val());

    $('#lbdept_of_mainten_name').text($('#txtdept_of_mainten_name').val());
    $('#lbdept_of_mainten_phone').text($('#txtdept_of_mainten_phone').val());
    $('#lbdept_of_mainten_email').text($('#txtdept_of_mainten_email').val());

    $('#lbdept_of_Account_name').text($('#txtdept_of_Account_name').val());
    $('#lbdept_of_Account_phone').text($('#txtdept_of_Account_phone').val());
    $('#lbdept_of_Account_email').text($('#txtdept_of_Account_email').val());

    $('#lbbank_Name').text($('#ddlBankname option').filter(':selected').text());
    $('#lbbranch_Name').text($('#txtbranch_Name').val());
    $('#lbbank_account_type_id').text($('#ddlbank_account_type option').filter(':selected').text());
    $('#lbaccount_Number').text($('#txtaccount_Number').val());

    var straccname = $('#ddlaccount_Name option').filter(':selected').val() != '' ?
        $('#ddlaccount_Name option').filter(':selected').text() + ' ' + $('#txtaccount_Name').val() : $('#txtaccount_Name').val()

    $('#lbaccount_Name').text(straccname);
    $('#lbbusiness_type').text($('#ddlbank_account_type option').filter(':selected').text());

    var strcer = $('#lbcompany_certified_file').text();
    $('#lbcompany_certified_file_5').text(strcer);
    var strreg = $('#lbcommercial_registration_file').text();
    $('#lbcommercial_registration_file_5').text(strreg);
    var strvat = $('#lbvat_registration_certificate_file').text();
    $('#lbvat_registration_certificate_file_5').text(strvat);


    BindDataAddress();
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

    Loading();
    $.ajax({
        type: "POST",
        async: false,
        url: urlSaveDaftAddress,
        data: { daftdata: stuff },
        dataType: "json",
        success: function (data) {
            Loading(0);
            if (data.status) {

                //var val = []
                //val = ConcatstrAddress(data.response);
                //tbaddressstep2.clear().draw();
                //BindDatatable(tbaddressstep2, val);
                tbaddressstep2.ajax.reload();
                tbaddressstep2.columns.adjust().draw();
            }
            else {
                bootbox.alert({
                    title: "System Information",
                    message: data.message,
                    size: "small",
                    callback: function (result) {
                        console.log('This was logged in the callback: ' + result);
                    }
                });

            }

        },
        error: function (xhr) {
            Loading(0);
            console.log(xhr);
        }
    });
}


function BindDDLprovince(regionid) {

    var urlDDLsubcontract_profile_province = url.replace('Action', 'DDLsubcontract_profile_province');

    Loading();
    $.ajax({
        type: "POST",
        async: false,
        url: urlDDLsubcontract_profile_province,
        data: { region_id: regionid },
        dataType: "json",
        success: function (data) {
            Loading(0);
            if (data != null) {
                $('#ddlprovince').empty();
                $.each(data.responseprovince, function () {
                    $('#ddlprovince').append($('<option></option>').val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }
           

        },
        error: function (xhr) {
            Loading(0);
            console.log(xhr);
        }
    });
}

function BindDDLdistrict(province) {
    var urlDDLsubcontract_profile_district = url.replace('Action', 'DDLsubcontract_profile_district');
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
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function BindDDLsubdistrict(district) {
    var urlDDLsubcontract_profile_sub_district = url.replace('Action', 'DDLsubcontract_profile_sub_district');
    $.ajax({
        type: "POST",
        async: false,
        url: urlDDLsubcontract_profile_sub_district,
        data: { district_id: district},
        dataType: "json",
        success: function (data) {
           
            if (data != null) {
                $('#ddlsubdistrict').empty();
                $('#ddlzipcode').empty();

                $.each(data.responsesubdistrict, function () {

                    $('#ddlsubdistrict').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
                $.each(data.responsezipcode, function () {
                    $('#ddlzipcode').append($("<option></option>").val(this.Value).text(this.Text));
                })

                $('#ddlzipcode').val('')
            }
          

        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function BindDDLTitle() {

    var urlDDLTitle = url.replace('Action', 'DDLTitle');
    //Loading();
    $.ajax({
        type: "POST",
        //async: false,
        url: urlDDLTitle,
        dataType: "json",
        success: function (data) {
            //Loading(0);
            if (data != null) {

                $('#ddlprefixcompany_name_th').empty();
                $('#ddlprefixcompany_name_en').empty();

                $('#ddlprefixcompany_name_th_dealer').empty();
                $('#ddlprefixcompany_name_en_dealer').empty();
                if (data.responsetitleTH.length > 0) {
                    $.each(data.responsetitleTH, function (index, value) {

                        $('#ddlprefixcompany_name_th').append($('<option></option>').val(value.Value == "0" ? "" : value.Value).text(value.Text));
                        $('#ddlprefixcompany_name_en').append($('<option></option>').val(this.Value == "0" ? "" : this.Value).text(this.Text));

                        $('#ddlprefixcompany_name_th_dealer').append($('<option></option>').val(value.Value == "0" ? "" : value.Value).text(value.Text));
                        $('#ddlprefixcompany_name_en_dealer').append($('<option></option>').val(this.Value == "0" ? "" : this.Value).text(this.Text));
                    });
                }
                else if (data.responsetitleEN.length > 0) {
                    $.each(data.responsetitleEN, function (index, value) {

                        $('#ddlprefixcompany_name_th').append($('<option></option>').val(this.Value == "0" ? "" : this.Value).text(this.Text));
                        $('#ddlprefixcompany_name_en').append($('<option></option>').val(value.Value == "0" ? "" : value.Value).text(value.Text));

                        $('#ddlprefixcompany_name_th_dealer').append($('<option></option>').val(this.Value == "0" ? "" : this.Value).text(this.Text));
                        $('#ddlprefixcompany_name_en_dealer').append($('<option></option>').val(value.Value == "0" ? "" : value.Value).text(value.Text));
                    });
                }
               

                
            }


        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function BindRegion() {
    Loading();
    var urlDDLsubcontract_profile_Region = url.replace('Action', 'DDLsubcontract_profile_Region');
    $.ajax({
        type: "POST",
        //async: false,
        url: urlDDLsubcontract_profile_Region,
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {
            Loading(0);
            if (data != null) {
                $('#ddlzone').empty();

                $.each(data.responseregion, function () {
                    $('#ddlzone').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr) {
            Loading(0);
            console.log(xhr);
        }
    });
}

function BindDDLBank() {
    var urlDDLBank = url.replace('Action', 'DDLBank');
    Loading();
    $.ajax({
        type: "POST",
       // async: false,
        url: urlDDLBank,
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {
            Loading(0);
            if (data != null) {
                $('#ddlBankname').empty();

                $.each(data.responsebank, function () {
                    $('#ddlBankname').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr) {
            Loading(0);
            console.log(xhr);
        }
    });
}

function BindDDLCompanyType() {
    Loading();
    var urlDDLCompanyType = url.replace('Action', 'DDLAccountName');
    $.ajax({
        type: "POST",
        //async: false,
        url: urlDDLCompanyType,
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {
            Loading(0);
            if (data != null) {
                $('#ddlaccount_Name').empty();

                $.each(data.responsecompanytype, function () {
                    $('#ddlaccount_Name').append($("<option></option>").val(this.Value == "0" ? "" : this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr) {
            Loading(0);
            console.log(xhr);
        }
    });
}

function BindAddressType() {
    var urlGetAddressType = url.replace('Action', 'GetAddressType');
    Loading();
    $.ajax({
        type: "POST",
        async: false,
        url: urlGetAddressType,
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {
         
            Loading(0);
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
        error: function (xhr) {
            Loading(0);
            console.log(xhr);
        }
    });
}

function BindDDlBankAccountType() {
    Loading();
    var urlGetDataBankAccountType = url.replace('Action', 'GetDataBankAccountType');
    $.ajax({
        type: "POST",
        //async: false,
        url: urlGetDataBankAccountType,
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {
            Loading(0);
            if (data != null) {
                $('#ddlbank_account_type').empty();

                $.each(data.response, function () {
                    $('#ddlbank_account_type').append($("<option></option>").val(this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr) {
            Loading(0);
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

function CheckUsername() {

    var urlCheckUsername = url.replace('Action', 'CheckUsername');

     var jqXHR = $.ajax({
        type: "POST",
        async: false,
         url: urlCheckUsername,
        data: { username: $('#txtcreateuser').val() },
        dataType: "json",
        //success: function (res) {
        //    Loading(0);
        //    handleResponse(res)
        //},
        //error: function (xhr, status, error) {
        //    Loading(0);
        //    clearForEdit();
        //}
     });
     return jqXHR.responseJSON;
}

function Validate(formcontrol, custom,customselect,cutomupload) {

    var hasError = false;

    $(formcontrol).each(function () {
        var $this = $(this);
        var fieldvalue = $this.val();
        var type = $this.attr("type");
        var tag = $this[0].tagName;
        
        if (tag == "INPUT" && type=="text") {
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
        else if (tag == "INPUT" && type=='radio') {
            if (!fieldvalue) {

                hasError = true;
            }
        }
        else if (tag == "INPUT" && type=='checkbox') {
            if (!fieldvalue) {

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
        var fieldvalue = $this.val();
        if ($this.is('input')) {
            if (fieldvalue == "") {

                hasError = true;
            }
        }
    });//customupload
    return hasError;
}

function ValidateUser() {
    var hasError = false;
    var resultusername = CheckUsername();
        if (resultusername.Status) {
            $('.user').hide();
        }
        else {
            $('.user').text(resultusername.Message);
            $('.user').show();
        }
    var hasUser = resultusername.Status;

    var forms = document.getElementsByClassName('needs-validation-user');
    var validation = Array.prototype.filter.call(forms, function (form) {
        if ($('#txtcreateuser').val() == "" || isEmail($('#txtcreateEmail').val()) || $('#txtcreatepass').val() == "" || $('#txtconfirmpass').val() == "" ||
            !hasUser
        )
        {
            event.preventDefault();
            event.stopPropagation();
            hasError = true;
        }
        else {
            hasError = false;

        }
        form.classList.add('was-validated');
    });

    return hasError;
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

