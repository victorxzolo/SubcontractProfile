﻿
    var oTable;
    var oTableAddress;
    var tbRevenue;
var tbLocation;

$(document).ready(function () {
        inittbSearchResult();
        inittbAddressResult();
        inittbRevenue();
        inittblocation();
   
 
    $("#btnSearch").click(function (e) {
        searchdata();
    });

                $("#btnMainClear").click(function (e) {
        clearMainData();
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

                $('#tbAddressResult').on('click', 'tbody .edit_btn', function () {
                    var data_row = oTableAddress.row($(this).closest('tr')).data();
                    oTableAddress.row($(this).closest('tr')).remove().draw();//เดี๋ยวกลับมาทำ

                    $.ajax({
        type: "POST",
                        url: "/CompanyProfile/GetAddressById",
                        data: {addressID: data_row.addressId },
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


                            $('#chkAddressType input[type=checkbox]').each(function () {
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
                                message: xhr.responseText,
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
                        url: "/CompanyProfile/DeleteDaftAddress",
                        data: {AddressId: data_row.addressId },
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
                                message: xhr.responseText,
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


                $('#btnsearchlocation').click(function () {
        $('#Searchlocation').modal('show');
                    $('#txtlocationcodemodal').val($('#txtlocationcode').val());
                    $("#btn_search_modal").trigger("click");
                });
                $('#btn_search_modal').click(function () {

        //var forms = document.getElementsByClassName('needs-validation-modalLocation');
        // var validation = Array.prototype.filter.call(forms, function (form) {
        //if (Validate(".form-control.inputValidationlocationModal", ".custom-control-input.inputValidationlocationModal")) {
        //    event.preventDefault();
        //    event.stopPropagation();
        // }
        //else {
        //tbLocation.ajax.reload();
        //inittblocation();


        // }
        //  form.classList.add('was-validated');
        // });



        $.ajax({
            url: "/CompanyProfile/SearchLocation",
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
                message: xhr.responseText,
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
                        url: "/CompanyProfile/GetLocationSession",
                        dataType: "json",
                        data: {location_id: lo_id },
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

        //clearForEdit();
        console.log(status);
                            showFeedback("error", xhr.responseText, "System Information",
                                "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
                        }
                    });



                    ClearDataModalLocation();
                    $('#Searchlocation').modal('hide');

                });
                //$('#tblocationModal tbody').on('click', 'tr', function () {
        //    // $(this).toggleClass('selected');
        //    if ($(this).hasClass('selected')) {
        //        $(this).removeClass('selected');
        //    }
        //    else {
        //        tbLocation.$('tr.selected').removeClass('selected');
        //        $(this).addClass('selected');
        //    }
        //});
        $('#btn_select_location').click(function () {

            //var value = tbLocation.rows('.selected').data();
            //var lo_id = value[0].location_id;
            //$.ajax({
            //    type: "POST",
            //    url: "/CompanyProfile/GetLocationSession",
            //    dataType: "json",
            //    data: { location_id: lo_id },
            //    success: function (data) {

            //        if (data.Response.Status) {
            //            $('#txtlocationcode').val(data.LocationListModel[0].outLocationCode);
            //            $('#txtlocationname').val(data.LocationListModel[0].outLocationName);
            //            $('#txtdistribution').val(data.LocationListModel[0].outDistChn);
            //            $('#txtchannelsalegroup').val(data.LocationListModel[0].outChnSales);
            //            $('#txttax_id_dealer').val(data.LocationListModel[0].outTaxId);
            //            $('#txtcompany_alias_dealer').val(data.LocationListModel[0].outCompanyShortName);

            //            if ($('#ddlprefixcompany_name_th_dealer option:selected').text() == data.LocationListModel[0].outTitle) {
            //                $('#ddlprefixcompany_name_th_dealer').text(data.LocationListModel[0].outTitle).change();
            //            }

            //            $('#txtcompany_name_th_dealer').val(data.LocationListModel[0].outCompanyName);

            //            if ($('#ddlprefixcompany_name_en_dealer option:selected').text() == data.LocationListModel[0].outTitle) {
            //                $('#ddlprefixcompany_name_en_dealer').text(data.LocationListModel[0].outTitle).change();
            //            }

            //            $('#txtcompany_name_en_dealer').val();
            //            $('#txtwt_name_dealer').val(data.LocationListModel[0].outWTName);

            //            if (data.LocationListModel[0].outVatType == "VAT") {
            //                $('#chkvat_typeT_dealer').prop('checked', true);
            //            }
            //            else if (data.LocationListModel[0].outVatType == "NON_VAT") {
            //                $('#chkvat_typeE_dealer').prop('checked', true);
            //            }
            //        }

            //    },
            //    error: function (xhr, status, error) {

            //        //clearForEdit();
            //        console.log(status);
            //        showFeedback("error", xhr.responseText, "System Information",
            //            "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
            //    }
            //});



            //ClearDataModalLocation();
            //$('#Searchlocation').modal('hide');

        });

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
            url: "/CompanyProfile/GetRevenue",
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
                message: xhr.responseText,
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

                //$('#tbrevenueModal tbody').on('click', 'tr', function () {
        //    // $(this).toggleClass('selected');
        //    if ($(this).hasClass('selected')) {
        //        $(this).removeClass('selected');
        //    }
        //    else {
        //        tbLocation.$('tr.selected').removeClass('selected');
        //        $(this).addClass('selected');
        //    }
        //});

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

    function validateform() {
        var hasError = true;
       
            var forms = document.getElementsByClassName('need-validate-checktyperegister');
            var validation = Array.prototype.filter.call(forms, function (form) {
                if ($('#hdrdtype').val() == "") {
                    event.preventDefault();
                    event.stopPropagation();

                }
                else {
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
                }
                form.classList.add('was-validated');
            });

        return hasError;
    }


        $('#btn_select_revenue').click(function () {
        //var value = tbRevenue.rows('.selected').data();
        //var titlename = value[0].vtitleName;
        //var companyname = value[0].vName;

        //$('#txtcompany_name_th').val(companyname);
        //if ($('#ddlprefixcompany_name_th option:selected').text() == titlename) {
        //    $('#ddlprefixcompany_name_th').text(titlename).change();
        //}
        //ClearDataModalRevenue();
        //$('#SearchRevenue').modal('hide');
    });

     
    $('#btn_OnSave_Modal').click(function () {

        var Error = validateform();

        if (!Error) {
            onSaveCompanyProfile();
        }

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

            //'use strict';
            //window.addEventListener('load', function () {
        //    // Fetch all the forms we want to apply custom Bootstrap validation styles to
        //    var forms = document.getElementsByClassName('needs-validation');
        //    // Loop over them and prevent submission
        //    var validation = Array.prototype.filter.call(forms, function (form) {
        //        form.addEventListener('submit', function (event) {
        //            if (form.checkValidity() === false) {
        //                event.preventDefault();
        //                event.stopPropagation();
        //            } else {
        //                onSave();
        //                event.preventDefault();
        //                event.stopPropagation();
        //            }
        //            form.classList.add('was-validated');
        //        }, false);
        //    });
        //}, false);


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
                url: '/CompanyProfile/Search',
                type: "POST",
                datatype: "JSON",
                //dataSrc:"",
                data: function (d) {
                    d.companyNameTh = $("#inputCompanynameTh").val(),
                        d.companyNameEn = $("#inputCompanynameEn").val(),
                        d.companyAilas = $("#inputCompanyAilas").val(),
                        d.taxId = $("#inputTaxId").val()

                }
            },
            columns: [

                {
                    data: null, width: "10%", render: function (data, type, row) {
                        return "<a href='#' onclick=openModal('edit','" + row.CompanyId + "');return false;  data-toggle='modal' data-target='.bd-addedit-modal-xl' data-original-title='แก้ไข'><svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-edit'><path d='M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7'></path><path d='M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z'></path></svg></a>";
                    }
                    // orderable: false,
                    //className: 'text-center'
                },
                { "data": "CompanyNameTh", "width": "20%" },
                { "data": "CompanyNameEn", "width": "20%" },
                { "data": "TaxId", "width": "15%" },
                { "data": "CreateDate", "width": "15%" },
                { "data": "ActivateDate", "width": "15%" },
                {
                    data: null, width: "10%", render: function (data, type, row) {
                        return "<span class='badge outline-badge-secondary shadow-none'>" + row.Status + "</span>";
                    }
                    // orderable: false,
                    // className: 'text-center'
                }
                //{ "data": "Status", "width": "10%" },

            ],
            "order": [[0, "desc"]],
            "stripeClasses": [],
            drawCallback: function () { $('.dataTables_paginate > .pagination').addClass(' pagination-style-13 pagination-bordered mb-5'); }

        });

        }

        function searchdata() {
        //  oTable.ajax.reload();
            inittbSearchResult();

            //var subcontract_type = $('#' + '@(ViewBag.subcontract_type)').val();
            //alert(subcontract_type);
        }

        function openModal(mode, companyId) {

        clearData();
            $('#hdMode').val(mode);
            $('#hdCompanyId').val(companyId);

            BindDDLTitle();
            BindRegion();
            BindDDLBank();
            BindDDlBankAccountType();
            BindDDLprovince();
            BindDDLdistrict();
            BindDDLsubdistrict();

            if (mode == "edit") {
        $("#txtCompanyCode").prop("disabled", true);
                $("#hTitleEdit").text('แก้ไข ข้อมูลบริษัท');

                getDataById(companyId);
            }
            else {
        $("#hTitleEdit").text('เพิ่ม ข้อมูลบริษัท');
                $("#txtCompanyCode").prop("disabled", false);

                // let form = $('#frNew');
                $("#frNew")[0].reset();
                //form.find('button').on('click', (event) => {
        //    form.find('input, select, textarea').val('');
        //});
    }


            $('#AddEditModal').modal('show');

        }

        function clearMainData() {
        $("#inputCompanynameTh").val('');
            $("#inputCompanynameEn").val('');
            $("#inputCompanyAilas").val('');
            $("#inputTaxId").val('');
        }

        function clearData() {

        $("[name=customRadioSucRegisEdit]").filter("[value='true']").prop("checked", false);
            $("#inputTax_id").val('');
            $("#inputCompany_alias").val('');
            $("#inputCompany_name_th").val('');
            $('#inputCompany_name_en').val('');
            $('#inputTax_id').val('');
            $("[name=vat_type]").filter("[value='true']").prop("checked", false);

        }

        function getDataById(companyId) {
        $.ajax({
            url: '/CompanyProfile/GetDataById',
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

                    }
                    else if (result.SubcontractProfileType == "Dealer") {
                        $('#rdoCompanyType2').prop('checked', true);
                        $('#hdrdtype').val(result.SubcontractProfileType);
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


                //$("#txtRegisNum").val(result.reqisNumber);

                //if (result.isActive == true) {
                //    $("[name=rdoActive]").filter("[value='true']").prop("checked", true);
                //} else {
                //    $("[name=rdoActive]").filter("[value='false']").prop("checked", true);
                //}

            },
            error: function (result) {

            }

        });
        }

        function GetAddress(companyId) {
        // inittbAddressResult(companyId);

        $.ajax({
            url: "/CompanyProfile/GetAddress",
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
                message: xhr.responseText,
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
            url: "/CompanyProfile/SaveDaftAddress",
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
                    message: xhr.responseText,
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

function onSaveCompanyProfile() {
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

            company.LocationCode= $('#txtlocationcode').val();
            company.LocationNameTh= $('#txtlocationname').val();
            company.LocationNameEn= $('#txtlocationname').val();

            company.BankAccountTypeId= $('#AccType option').filter(':selected').val();
            company.SubcontractProfileType= chksubcontract_type;
            company.CompanyTitleThId= company_title_name_th;
            company.CompanyTitleEnId= company_title_name_en;
            company.CompanyId = $('#hdCompanyId').val();

            console.log(company);
            $.ajax({
                url:'/CompanyProfile/OnSave',
                type: 'POST',
                data: {'model': company },
                   dataType: "json",
                   success: function (result) {
                       if (result.Response.Status) {
                           if ($('#hdMode').val() == 'edit') {
                               bootbox.alert(result.Response.Message);
                               $('#AddEditModal').modal('hide');
                           }
                           else {
                               clearData();
                           }

                           searchdata();
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
                    url: "/CompanyProfile/UploadFile",
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
            message: xhr.responseText,
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
                    message: xhr.responseText,
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
                    message: xhr.responseText,
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
                    message: xhr.responseText,
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
                    message: xhr.responseText,
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
                    message: xhr.responseText,
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
                    message: xhr.responseText,
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

function BindDDlBankAccountType() {
    
    $.ajax({
        type: "POST",
        url: "/CompanyProfile/GetDataBankAccountType",
        //data: { province_id: province },
        dataType: "json",
        success: function (data) {
     
            if (data != null) {
                $('#AccType').empty();

                $.each(data.response, function () {
                    $('#AccType').append($("<option></option>").val(this.Value).text(this.Text));
                });
            }


        },
        error: function (xhr, status, error) {
   
            //clearForEdit();
            //bootbox.alert({
            //    title: "System Information",
            //    message: "This action is not available.",
            //    size: "small",
            //    callback: function (result) {
            //        console.log('This was logged in the callback: ' + result);
            //    }
            //});
        }
    });
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