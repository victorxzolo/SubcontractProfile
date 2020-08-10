$(document).ready(function () {

    $("#smartwizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {
        //alert("You are on step "+stepNumber+" now");
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
        $('#smartwizard').smartWizard("next");
        return true;
    });





/*Step1*/

    function GetValueSerachLocation() {
        var valueSearch = {
            company_name_th: $('#txtjuristicTmodal').val(),
            company_name_en: $('#txtjuristicEmodal').val(),
            company_alias: $('#txtbussinessmodal').val(),
            company_code: $('#txtbussinesscodemodal').val(),
            location_name_th: $('#txtlocationnameTHmodal').val(),
            location_name_en: $('#txtlocationnameTHmodal').val(),
            location_code: $('#txtlocationcodemodal').val(),
            distribution_channel: $('#ddldistributionModal option').filter(':selected').val(),
            channel_sale_group: $('#ddlchannelsalegroupModal option').filter(':selected').val()

        }
        return valueSearch;
    }
    BindDDLTitle();
    var tbLocation = $('#tblocationModal').DataTable({
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
        ajax: {
            type: "POST",
            url: "/Account/SearchLocation",
            data: {
                asc_code: function () { return $('#txtasccodemodal').val() },
                asc_mobile_no: function () { return $('#txtmobilenomodal').val() },
                id_Number: function () { return $('#txtidnumbermodal').val()},
                location_code: function () { return $('#txtlocationcodemodal').val()},
                sap_code: function () { return $('#txtsapcodemodal').val()},
                user_id: function () { return $('#txtuseridmodal').val()}
            },
            dataType: "json",
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
                showFeedback("error", xhr.responseText, "System Information",
                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
            }

        },
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        //lengthChange: false,
        columns: [
            { "data": "outCompanyName" },
            { "data": "outCompanyShortName" },
            { "data": "outTaxId" },
            { "data":"outLocationCode"},
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
        var value = tbLocation.rows('.selected').data();
        var lo_id = value[0].location_id;
        $.ajax({
            type: "POST",
            url: "/Account/GetLocationSession",
            dataType: "json",
            data: { location_id: lo_id},
            success: function (data) {
                console.log(data)
                if (data.response.status) {
                    $('#txtlocationcode').val(data.locationListModel[0].outLocationCode);
                    $('#txtlocationname').val(data.locationListModel[0].outLocationName);
                    $('#txtdistribution').val(data.locationListModel[0].outDistChn);
                    $('#txtchannelsalegroup').val(data.locationListModel[0].outChnSales);
                    $('#txttax_id_dealer').val(data.locationListModel[0].outTaxId);
                    $('#txtcompany_alias_dealer').val(data.locationListModel[0].outCompanyShortName);

                    if ($('#ddlprefixcompany_name_th_dealer option:selected').text() == data.locationListModel[0].outTitle) {
                        $('#ddlprefixcompany_name_th_dealer').text(data.locationListModel[0].outTitle).change();
                    } 

                    $('#txtcompany_name_th_dealer').val(data.locationListModel[0].outCompanyName);

                    if ($('#ddlprefixcompany_name_en_dealer option:selected').text() == data.locationListModel[0].outTitle) {
                        $('#ddlprefixcompany_name_en_dealer').text(data.locationListModel[0].outTitle).change();
                    } 

                    $('#txtcompany_name_en_dealer').val();
                    $('#txtwt_name_dealer').val(data.locationListModel[0].outWTName);

                    if (data.locationListModel[0].outVatType == "VAT") {
                        $('#chkvat_typeT_dealer').prop('checked', true);
                    }
                    else if (data.locationListModel[0].outVatType == "NON_VAT"){
                        $('#chkvat_typeE_dealer').prop('checked', true);
                    }

                    if (data.locationListModel[0].addressLocationList != null) {
                        var stuff = [];
                        jQuery.each(data.locationListModel[0].addressLocationList, function (i, val) {
                            var address_type_name = "";
                            $(':checkbox').each(function (i) {

                                if (val.outAddressType == $(this).val()) {
                                    address_type_name = $(this).parent().text().trim();
                                }
                            });
                            var data = {
                                AddressTypeId: val.outAddressType,
                                address_type_name: address_type_name,
                                Country: val.outCountry,
                                ZipCode: val.outZipcode,
                                HouseNo: val.outHouseNo,
                                Moo: val.outMoo,
                                VillageName: val.outMooban,
                                Building: val.outBuilding,
                                Floor: val.outFloor,
                                RoomNo: val.outRoom,
                                Soi: val.outSoi,
                                Road: val.outStreet,
                                SubDistrictId: 0,
                                sub_district_name: val.outTumbol,
                                DistrictId: 0,
                                district_name: val.outAmphur,
                                ProvinceId: 0,
                                province_name: val.outProvince,
                                RegionId: 0,
                                outFullAddress: val.outFullAddress,
                                location_code: $('#txtlocationcode').val()
                            }
                            stuff.push(data);
                        });
                        SaveDaftAddress(stuff);
                    }
                }

            },
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
                showFeedback("error", xhr.responseText, "System Information",
                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
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
        
        tbLocation.ajax.reload();
        //$.ajax({
        //    type: "POST",
        //    url: "/Account/SearchLocation",
        //    data: { model: GetValueSerachLocation() },
        //    dataType: "json",
        //    success: function (data) {
        //        tbLocation.clear().draw();
        //        BindDatatable(tbLocation, data.response)

        //    },
        //    error: function (xhr, status, error) {
        //        //Loading(0);
        //        //clearForEdit();
        //        console.log(status);
        //        showFeedback("error", xhr.responseText, "System Information",
        //            "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
        //    }
        //});
    });

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

/*************************************/


/*Step2*/
    BindDDLprovince();
    BindDDLdistrict();
    BindDDLsubdistrict();
    BindRegion();
    var tbaddressstep2 = $('#tbaddressstep2').DataTable({
        ordering: true,
        order: [[1, "asc"]],
        select: true,
        retrieve: true,
        paging: true,
        pagingType: "full_numbers",
        destroy: true,
        searching: false,
        proccessing: true,
        serverSide: true,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        dom: 'rt<"float-left"p><"float-left"l><"float-right"i>',
        ajax: {
            type: "POST",
            url: "/Account/SearchAddress",
            dataSrc: function (data) {
                var val = []
                val = ConcatstrAddress(data.data);
                data.data = val;
                return data.data;
            },
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
                showFeedback("error", xhr.responseText, "System Information",
                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
            }

        },
        columns: [
            { "data": "addressId", "visible": false },
            { "data": "address_type_id", "visible": false},
            { "data": "address_type", orderable: true,},
            { "data": "address", orderable: true,},
            {
                "targets": -2,
                "data": null,
                orderable: false,
                "defaultContent": "<button class='btn-border btn-green edit_btn'><i class='fa fa-edit icon'></i><span>แก้ไข</span></button>"
            },
            {
                "targets": -1,
                "data": null,
                orderable: false,
                "defaultContent": "<button class='btn-border btn-black delete_btn'><i class='fa fa-trash icon'></i><span>ลบ</span></button>"
            }
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

    $('#btnaddaddress').click(function () {
       
        var stuff = [];

        $(':checkbox:checked').each(function (i) {
            //val[i] = $(this).val();
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
            
        });
        SaveDaftAddress(stuff);
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

        $.ajax({
            type: "POST",
            url: "/Account/GetDaftAddress",
            data: { AddressId: data_row.addressId },
            dataType: "json",
            success: function (data) {
                $('#ddlcountry option').filter(':selected').val('')

                $('#ddlprovince option').filter(':selected').val(0)

                $('#ddldistrict option').filter(':selected').val(0)

                $('#ddlsubdistrict option').filter(':selected').val(0)

                $('#ddlzipcode option').filter(':selected').val('')

                $('#ddlzone option').filter(':selected').val('')

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
                        $('#ddlcountry option').filter(':selected').val(this.country)

                        $('#ddlprovince option').filter(':selected').val(this.provinceId)
                        $('#ddlprovince').trigger('change');

                        $('#ddldistrict option').filter(':selected').val(this.districtId)
                        $('#ddldistrict').trigger('change');

                        $('#ddlsubdistrict option').filter(':selected').val(this.subDistrictId)

                        $('#ddlzipcode option').filter(':selected').val(this.zipCode)

                        $('#ddlzone option').filter(':selected').val(this.regionId)

                        $('#txthomenumber').val(this.houseNo)
                        $('#txtVillageNo').val(this.moo)
                        $('#txtvillage').val(this.villageName)
                        $('#txtbuilding').val(this.building)
                        $('#txtfloor').val(this.floor)
                        $('#txtroom').val(this.roomNo)
                        $('#txtsoi').val(this.soi)
                        $('#txtroad').val(this.road)

                        var addr_type_id = this.addressTypeId
                        $(':checkbox').each(function (i) {
                           
                            if (addr_type_id == $(this).val()) {
                                $(this).prop('checked', true);
                            }
                        });

                        
                    });

                  
                }
                else {
                    showFeedback("error", data.message, "System Information",
                        "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");

                }
            },
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
                showFeedback("error", xhr.responseText, "System Information",
                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
            }
        });
    });

    $('#tbaddressstep2').on('click', 'tbody .delete_btn', function () {
        var data_row = tbaddressstep2.row($(this).closest('tr')).data();
        $.ajax({
            type: "POST",
            url: "/Account/DeleteDaftAddress",
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
                    showFeedback("error", data.message, "System Information",
                        "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");

                }
            },
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
                showFeedback("error", xhr.responseText, "System Information",
                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
            }
        });
    });

    function ConcatstrAddress(data) {
        var val = [];

        $.each(data, function () {
            if (this.outFullAddress != null && this.outFullAddress != "") {
                var strdata = {
                    addressId: this.addressId,
                    address_type_id: this.addressTypeId,
                    address_type: this.address_type_name,
                    address: this.outFullAddress
                };

                val.push(strdata);
            }
            else {
                var strnumber = this.houseNo != '' && this.houseNo != null ? this.houseNo : '';
                var strvillage = this.villageName != '' && this.villageName != null ? $('#txtvillage').parent().parent().text().split(":")[0].trim() + ' ' + this.villageName : '';
                var strvillageno = this.moo != '' && this.moo != null ? $('#txtVillageNo').parent().parent().text().split(":")[1].trim() + ' ' + this.moo : '';

                var strbuilding = this.building != '' && this.building != null ? $('#txtbuilding').parent().parent().text().split(":")[0].trim() + ' ' + this.building : '';
                var strfloor = this.floor != '' && this.floor != null ? $('#txtfloor').parent().parent().text().split(":")[0].trim() + ' ' + this.floor : '';
                var strroom = this.roomNo != '' && this.roomNo != null ? $('#txtroom').parent().parent().text().split(":")[1].trim() + ' ' + this.roomNo : '';
                var strsoi = this.soi != '' && this.soi != null ? $('#txtsoi').parent().parent().text().split(":")[0].trim() + ' ' + this.soi : '';

                var strroad = this.road != '' && this.road != null ? $('#txtroad').parent().parent().text().split(":")[0].trim() + ' ' + this.road : '';
                var strsubdistrict = this.subDistrictId != '' && this.subDistrictId != null ? $('#ddlsubdistrict').parent().parent().text().split(":")[0].trim() + ' ' +
                    this.sub_district_name : '';
                var strdistrict = this.districtId != 0 && this.districtId != null ? $('#ddldistrict').parent().parent().text().split(":")[0].trim() + ' ' +
                    this.district_name : '';
                var strprovince = this.provinceId != 0 && this.provinceId != null ? $('#ddlprovince').parent().parent().text().split(":")[0].trim() + ' ' +
                    this.province_name : '';

                var strzipcode = this.zipCode != '' && this.zipCode != null ? $('#ddlzipcode').parent().parent().text().split(":")[0].trim() + ' ' +
                    this.zipCode : '';

                var strdata = {
                    addressId: this.addressId,
                    address_type_id: this.addressTypeId,
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
            url: "/Account/SaveDaftAddress",
            data: { daftdata: stuff },
            dataType: "json",
            success: function (data) {
                if (data.status) {

                    //var val = []
                    //val = ConcatstrAddress(data.response);
                    //tbaddressstep2.clear().draw();
                    //BindDatatable(tbaddressstep2, val);
                    tbaddressstep2.ajax.reload();
                }
                else {
                    showFeedback("error", data.message, "System Information",
                        "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");

                }

            },
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
                showFeedback("error", xhr.responseText, "System Information",
                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
            }
        });
    }

/*************************************/


/*Step3*/

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

    function uploadFiles(inputId) {
        var input = document.getElementById(inputId);
        var files = input.files;
        var formData = new FormData();
        var id = "";
        var typefile = "";
        switch (inputId) {
            case "company_certified_file": id = $('#hdupfilecompany_certified').val();
                typefile = "CompanyCertifiedFile";
                break;
            case "commercial_registration_file": id = $('#hdupfilecommercial_registration').val();
                typefile = "CommercialRegistrationFile";
                break;
            case "vat_registration_certificate_file": id = $('#hdupfilevat_registration_certificate').val();
                typefile = "VatRegistrationCertificateFile";
                break;
        }
        for (var i = 0; i != files.length; i++) {
            formData.append("files", files[i]);
            formData.append("fid", id);
            formData.append("type_file", typefile);
        }
        

        $.ajax(
            {
                type: "POST",
                url: "/Account/UploadFile",
                processData: false,
                contentType: false,
                dataType: "json",
                data: formData,
                success: function (data) {
                    console.log(data);
                    if (data.status) {
                        switch (inputId) {
                            case "company_certified_file": $('#hdupfilecompany_certified').val(data.response); break;
                            case "commercial_registration_file": $('#hdupfilecommercial_registration').val(data.response); break;
                            case "vat_registration_certificate_file": $('#hdupfilevat_registration_certificate').val(data.response); break;
                        }
                        showFeedback("success", data.message, "System Information",
                            "<button type='button' class='btn-border btn-green' data-dismiss='modal' id='btnOKpopup'><i class='fa fa-check icon'></i><span>OK</span></button >")
                    }
                    else {
                        showFeedback("error", data.message, "System Information",
                            "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
                    }
                   
                },
                error: function (xhr, status, error) {
                    //Loading(0);
                    //clearForEdit();
                    console.log(status);
                    showFeedback("error", xhr.responseText, "System Information",
                        "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
                }
            }
        );
    }

/*************************************/



/*Step5*/

    var tbaddressstep5 = $('#tbaddressstep5').DataTable({
        ordering: true,
        order: [[1, "asc"]],
        select: true,
        retrieve: true,
        paging: true,
        destroy: true,
        searching: false,
        //scrollY: 400,
        //processing: true,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        columns: [
            { "data": "address_type_id", "visible": false },
            { "data": "address_type", orderable: true, },
            { "data": "address", orderable: true, }
        ],
        language: {
            infoEmpty: "No items to display",
            lengthMenu: "_MENU_ items per page",
            zeroRecords: "Nothing found",
            info: "_START_ - _END_  of _TOTAL_  items",
            infoFiltered: ""
        }
    });



    $('#btnregis').click(function (){

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
            chksubcontract_type = $("#chktypeN").val();
            distribution_channel = $('#ddldistribution option').filter(':selected').val();
            channel_sale_group = $('#ddlchannelsalegroup option').filter(':selected').val();
            tax_id = $('#txttax_id').val();
            company_alias = $('#txtcompany_alias').val();
            company_title_name_th = $('#ddlprefixcompany_name_th option').filter(':selected').val();
            company_name_th = $('#txtcompany_name_th').val();
            company_title_name_en = $('#ddlprefixcompany_name_en option').filter(':selected').val();
            company_name_en = $('#txtcompany_name_en').val();
            wt_name = $('#txtwt_name').val();
            vat_type= $('#chkvat_typeT').is(':checked') ? $('#chkvat_typeT').val() : $('#chkvat_typeE').val();
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
        }

        var data = {
            //CompanyId
            //CompanyCode
            //CompanyName
            CompanyNameTh: company_name_th,
            CompanyNameEn: company_name_en,
            CompanyAlias: company_alias,
            DistributionChannel: distribution_channel,
            ChannelSaleGroup: channel_sale_group,
           // VendorCode
           // CustomerCode:
            //AreaId
            TaxId: tax_id,
            WtName: wt_name,
            VatType: vat_type,
            //CompanyCertifiedFile 'Upload file'
            //CommercialRegistrationFile 'Upload file'
            //VatRegistrationCertificateFile 'Upload file'
            //ContractAgreementFile
            //DepositAuthorizationLevel
            //DepositPaymentType
            //ContractStartDate
            //ContractEndDate
            //OverDraftDeposit
            //BalanceDeposit
            //CompanyStatus: '',
            //CompanyAddress
            //VatAddress
            CompanyEmail: $('#txtcompany_Email').val(),
            ContractName: $('#txtcontract_name').val(),
            ContractPhone: $('#txtcontract_phone').val(),
            ContractEmail: $('#txtcontract_email').val(),

            BankCode: $('#txtbank_Code').val(),
            BankName: $('#txtbank_Name').val(),
            AccountNumber: $('#txtaccount_Number').val(),
            AccountName:$('#ddlaccount_Name option').filter(':selected').val(),
           // AttachFile
            BranchCode: $('#txtbranch_Code').val(),
            BranchName: $('#txtbranch_Name').val(),

            DeptOfInstallName: $('#txtdept_of_install_name').val(),
            DeptOfMaintenName: $('#txtdept_of_mainten_name').val(),
            DeptOfAccountName: $('#txtdept_of_Account_name').val(),

            DeptOfInstallPhone: $('#txtdept_of_install_phone').val(),
            DeptOfMaintenPhone: $('#txtdept_of_mainten_phone').val(),
            DeptOfAccountPhone: $('#txtdept_of_Account_phone').val(),

            DeptOfInstallEmail: $('#txtdept_of_install_email').val(),
            DeptOfMaintenEmail: $('#txtdept_of_mainten_email').val(),
            DeptOfAccountEmail: $('#txtdept_of_Account_email').val(),

            LocationCode: $('#txtlocationcode').val(),
            LocationNameTh: $('#txtlocationname').val(),
            LocationNameEn: $('#txtlocationname').val(),

            BankAccountTypeId: $('#ddlbank_account_type option').filter(':selected').val(),
            SubcontractProfileType: chksubcontract_type,
            CompanyTitleThId: company_title_name_th,
            CompanyTitleEnId: company_title_name_en,
            //Status
            
            //subcontract_profile_type: chksubcontract_type,
            //LocationCode: $('#txtlocationcode').val(),
            //LocationNameTh: $('#txtlocationname').val(),
            //LocationNameEn: $('#txtlocationname').val(),
            //DistributionChannel: distribution_channel,
            //ChannelSaleGroup: channel_sale_group,
            //tax_id: tax_id,
            //company_alias: company_alias,
            //company_title_name_th: company_title_name_th,
            //company_name_th: company_name_th,
            //company_title_name_en: company_title_name_en,
            //company_name_en: company_name_en,
            //wt_name: wt_name,
            //vat_type: vat_type,
            //company_Email: $('#txtcompany_Email').val(),
            //contract_name: $('#txtcontract_name').val(),
            //contract_phone: $('#txtcontract_phone').val(),
            //contract_email: $('#txtcontract_email').val(),
            //dept_of_install_name: $('#txtdept_of_install_name').val(),
            //dept_of_install_phone: $('#txtdept_of_install_phone').val(),
            //dept_of_install_email: $('#txtdept_of_install_email').val(),
            //dept_of_mainten_name: $('#txtdept_of_mainten_name').val(),
            //dept_of_mainten_phone: $('#txtdept_of_mainten_phone').val(),
            //dept_of_mainten_email: $('#txtdept_of_mainten_email').val(),
            //dept_of_Account_name: $('#txtdept_of_Account_name').val(),
            //dept_of_Account_phone: $('#txtdept_of_Account_phone').val(),
            //dept_of_Account_email: $('#txtdept_of_Account_email').val(),
            //account_Name: $('#ddlaccount_Name option').filter(':selected').val(),
            //branch_Name: $('#txtbranch_Name').val(),
            //branch_Code: $('#txtbranch_Code').val(),
            //bank_account_type_id: $('#ddlbank_account_type option').filter(':selected').val(),
            //company_certified_file: $('#company_certified_file').val(),
            //commercial_registration_file: $('#commercial_registration_file').val(),
            //vat_registration_certificate_file: $('#vat_registration_certificate_file').val(),
        }

        $.ajax({
            type: "POST",
            url: "/Account/NewRegister",
            data: { model: data },
            dataType: "json",
            success: function (data) {
                console.log(data)
                if (data.Response.Status) {
                    showFeedback("success", data.Response.Message, "System Information",
                        "<button type='button' class='btn-border btn-green' data-dismiss='modal' id='btnOKpopup'><i class='fa fa-check icon'></i><span>OK</span></button >");
                }
                else {
                    showFeedback("error", data.Response.Message, "System Information",
                        "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
                }
            },
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
                showFeedback("error", xhr.responseText, "System Information",
                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
            }
        });
    });

    function BindDataAddress() {
        $.ajax({
            type: "POST",
            url: "/Account/GetDaftAddress",
            data: { address_type_id: null },
            dataType: "json",
            success: function (data) {
                console.log(data)

                if (data.status) {
                    var val = []
                    val = ConcatstrAddress(data.response);
                    tbaddressstep5.clear().draw();
                    BindDatatable(tbaddressstep5, val);
                }
                else {
                    showFeedback("error", data.message, "System Information",
                        "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");

                }
            },
            error: function (xhr, status, error) {
                //Loading(0);
                //clearForEdit();
                console.log(status);
                showFeedback("error", xhr.responseText, "System Information",
                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
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
            chksubcontract_type =$("#chktypeN").parent().text().trim();

            distribution_channel = $('#ddldistribution option').filter(':selected').text();
            channel_sale_group = $('#ddlchannelsalegroup option').filter(':selected').text();

            tax_id = $('#txttax_id').val();
            company_alias = $('#txtcompany_alias').val();

            company_title_name_th = $('#ddlprefixcompany_name_th option').filter(':selected').text();
            company_name_th = $('#txtcompany_name_th').val();

            company_title_name_en = $('#ddlprefixcompany_name_en option').filter(':selected').text();
            company_name_en = $('#txtcompany_name_en').val();

            wt_name = $('#txtwt_name').val();
            vat_type = $('#chkvat_typeT').is(':checked') ? $('#chkvat_typeT').parent().text().trim() : $('#chkvat_typeE').parent().text().trim();
        }
        else if ($("#chktypeD").is(":checked")) {
            chksubcontract_type = $('#chktypeD').parent().text().trim();
   
            distribution_channel = $('#txtdistribution').val();
            channel_sale_group = $('#txtchannelsalegroup').val();

            tax_id = $('#txttax_id_dealer').val();
            company_alias = $('#txtcompany_alias_dealer').val();

            company_title_name_th = $('#ddlprefixcompany_name_th_dealer option').filter(':selected').text();
            company_name_th = $('#txtcompany_name_th_dealer').val();

            company_title_name_en = $('#ddlprefixcompany_name_en_dealer option').filter(':selected').text();
            company_name_en = $('#txtcompany_name_en_dealer').val();

            wt_name = $('#txtwt_name_dealer').val();
            vat_type = $('#chkvat_typeT_dealer').is(':checked') ? $('#chkvat_typeT_dealer').parent().text().trim() : $('#chkvat_typeE_dealer').parent().text().trim();
        }

        $('#lbsubcontract_profile_type').text(chksubcontract_type);
        $('#lbdistribution_channel').text(distribution_channel);
        $('#lbchannel_sale_group').text(channel_sale_group);
        $('#lbtax_id').text(tax_id);
        $('#lbcompany_alias').text(company_alias);
        $('#lbcompany_name_th').text(company_title_name_th + ' ' + company_name_th);
        $('#lbcompany_name_en').text(company_title_name_en + ' ' + company_name_en);
        $('#lbwt_name').text(wt_name);
        $('#vat_type').text(vat_type);

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

        $('#lbbank_Name').text($('#txtbank_Name').val());
        $('#lbbranch_Name').text($('#txtbranch_Name').val());
        $('#lbbank_account_type_id').text($('#ddlbank_account_type option').filter(':selected').text());
        $('#lbaccount_Number').text($('#txtaccount_Number').val());
        $('#lbaccount_Name').text($('#ddlaccount_Name option').filter(':selected').text() + ' ' + $('#txtaccount_Name').val());
        $('#lbbusiness_type').text($('#ddlbank_account_type option').filter(':selected').text());


        $('#lbcompany_certified_file').text($('#company_certified_file').val().split("\\").pop());
        
        $('#lbcommercial_registration_file').text($('#commercial_registration_file').val().split("\\").pop());
       
        $('#lbvat_registration_certificate_file').text($('#vat_registration_certificate_file').val().split("\\").pop());


        BindDataAddress();
    }

/*************************************/
});

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
                    $('#ddlprovince').append($('<option></option>').val(this.value).text(this.text));
                });
            }
           

        },
        error: function (xhr, status, error) {
            //Loading(0);
            //clearForEdit();
            console.log(status);
            showFeedback("error", xhr.responseText, "System Information",
                "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
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
                    $('#ddldistrict').append($("<option></option>").val(this.value).text(this.text));
                });
            }
          

        },
        error: function (xhr, status, error) {
            //Loading(0);
            //clearForEdit();
            console.log(status);
            showFeedback("error", xhr.responseText, "System Information",
                "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
        }
    });
}

function BindDDLsubdistrict(district) {
    $.ajax({
        type: "POST",
        url: "/Account/DDLsubcontract_profile_sub_district",
        data: { district_id: district},
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $('#ddlsubdistrict').empty();
                $('#ddlzipcode').empty();

                $.each(data.responsesubdistrict, function () {

                    $('#ddlsubdistrict').append($("<option></option>").val(this.value).text(this.text));
                });
                $.each(data.responsezipcode, function () {

                    $('#ddlzipcode').append($("<option></option>").val(this.value).text(this.text));
                })

                $('#ddlzipcode').val("0")
            }
          

        },
        error: function (xhr, status, error) {
            //Loading(0);
            //clearForEdit();
            console.log(status);
            showFeedback("error", xhr.responseText, "System Information",
                "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
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

                $('#ddlprefixcompany_name_th').append($('<option></option>').val(0).text('Select Title'));
                $('#ddlprefixcompany_name_en').append($('<option></option>').val(0).text('Select Title'));

                $('#ddlprefixcompany_name_th_dealer').append($('<option></option>').val(0).text('Select Title'));
                $('#ddlprefixcompany_name_en_dealer').append($('<option></option>').val(0).text('Select Title'));

                $.each(data.responsetitle, function () {
                    $('#ddlprefixcompany_name_th').append($('<option></option>').val(this.titleId).text(this.titleNameTh));
                    $('#ddlprefixcompany_name_en').append($('<option></option>').val(this.titleId).text(this.TitleNameEn));

                    $('#ddlprefixcompany_name_th_dealer').append($('<option></option>').val(this.titleId).text(this.titleNameTh));
                    $('#ddlprefixcompany_name_en_dealer').append($('<option></option>').val(this.titleId).text(this.titleNameEn));
                });
            }


        },
        error: function (xhr, status, error) {
            //Loading(0);
            //clearForEdit();
            console.log(status);
            showFeedback("error", xhr.responseText, "System Information",
                "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
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
                    $('#ddlzone').append($("<option></option>").val(this.value).text(this.text));
                });
            }


        },
        error: function (xhr, status, error) {
            //Loading(0);
            //clearForEdit();
            console.log(status);
            showFeedback("error", xhr.responseText, "System Information",
                "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
        }
    });
}


function Validate() {
    var errorMessage = $("#idmsAlert");
    var hasError = false;
    $(".form-control.inputValidation").each(function () {
        var $this = $(this);
        var fieldvalue = $this.val();

        if (!fieldvalue) {

            hasError = true;
            $this.addClass("inputError");
            $($this).focusout(function () {
                $(this).addClass('desired');
            });
            errorMessage.show();
            //errorMessage.html("<p>กรุณาระบุข้อมูลให้ครบถ้วน</p>").show();
        }
        if ($this.val() != "") {
            $this.removeClass("inputError");
        } else {
            return true;
        }
    }); //Input

    return hasError; 
}