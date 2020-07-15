﻿$(document).ready(function () {

    $("#smartwizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {
        //alert("You are on step "+stepNumber+" now");
        if (stepPosition === 'first') {
            $("#prev-btn").addClass('disabled');
        } else if (stepPosition === 'final') {
           // $("#next-btn").addClass('disabled');
            $("#next-btn").addClass('disabled');
            $("#next-btn").hide();
            $('#btnregis').show();
        } else {
            $("#prev-btn").removeClass('disabled');
            $("#next-btn").removeClass('disabled');
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


    $('#btnregis').click(function () {

    });



/*Step1*/


 

    var tbLocation = $('#tblocationModal').DataTable({
        ordering: true,
        select: true,
        retrieve: true,
        paging: true,
        destroy: true,
        searching: false,
        //scrollY: 400,
        //processing: true,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        columns: [
            { "data": "company_name_th" },
            { "data":"location_code"},
            { "data": "location_name_th" },
            { "data": "distribution_channel" },
            { "data": "channel_sale_group" }
        ],
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
        if ($(this).attr("value") == "N") {
            $("#divdealer").hide('slow');
            $('#divnewsubcontract').show('slow');
        }
    });

    $('#btn_select_location').click(function () {
        var value = tbLocation.rows('.selected').data();
        console.log(value[0]);
        console.log(value[0].location_code)
        $('#txtlocationcode').val(value[0].location_code);
        $('#txtlocationname').val(value[0].location_name_th);
        $('#txtdistribution').val(value[0].distribution_channel);
        $('#txtchannelsalegroup').val(value[0].channel_sale_group);
        ClearDataModalLocation();
        $('#Searchlocation').modal('hide');

    });

    $('#chktypeD').on("change", function () {
        if ($(this).attr("value") == "D") {
            $("#divnewsubcontract").hide('slow');
            $("#divdealer").show('slow');
        }
    });

    $('#btn_search_modal').click(function () {
        var value = {
            company_name_th: $('#txtjuristicTmodal').val(),
            company_name_en: $('#txtjuristicEmodal').val(),
            company_alias: $('#txtbussinessmodal').val(),
            company_code: $('#txtbussinesscodemodal').val(),
            location_name_th: $('#txtlocationnameTHmodal').val(),
            location_name_en: $('#txtlocationnameTHmodal').val(),
            location_code: $('#txtlocationcodemodal').val(),
            distribution_channel: $('#ddldistributionModal option').filter(':selected').val(),
            channel_sale_group: $('#ddlchannelsalegroupModal option').filter(':selected').val(),
            PageIndex: 1,
            PageSize: 10,
            Sort: '',
            Filter:''

        }

        $.ajax({
            type: "POST",
            url: "/Account/SearchLocation",
            data: { model: value },
            dataType: "json",
            success: function (data) {
                tbLocation.clear().draw();
                BindDatatable(tbLocation, data.response)

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

    function BindDatatable(table, datamodel) {
        //console.log("BindDatatable");
        //console.log(datamodel);
        //table.data(datamodel);
        
        table.rows.add(datamodel).draw();

    }

    function ClearDataModalLocation() {
        $('#txtjuristicTmodal').val('')
        $('#txtjuristicEmodal').val('')
        $('#txtbussinessmodal').val('')
        $('#txtbussinesscodemodal').val('')
        $('#txtlocationnameTHmodal').val('')
        $('#txtlocationnameTHmodal').val('')
        $('#txtlocationcodemodal').val('')
        $('#ddldistributionModal option').filter(':selected').val('')
        $('#ddlchannelsalegroupModal option').filter(':selected').val('')
        tbLocation.clear().draw();
    }

/*************************************/


/*Step2*/
    BindDDLprovince();
    BindDDLdistrict();
    BindDDLsubdistrict();
    var tbaddressstep2 = $('#tbaddressstep2').DataTable({
        ordering: true,
        select: true,
        retrieve: true,
        paging: true,
        destroy: true,
        searching: false,
        //scrollY: 400,
        //processing: true,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        columns: [
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
        //columnDefs: [
        // {
        //    "targets": -2,
        //    "data": null,
        //    "defaultContent": "<button class='btn-border btn-green edit_btn'><i class='fa fa-edit icon'></i><span>แก้ไข</span></button>"
        //},
        //    {
        //        "targets": -1,
        //        "data": null,
        //        "defaultContent": "<button class='btn-border btn-black delete_btn'><i class='fa fa-trash icon'></i><span>ลบ</span></button>"
        //    }
        //]
    });

    $('#btnaddaddress').click(function () {
       
        var stuff = [];

        $(':checkbox:checked').each(function (i) {
            //val[i] = $(this).val();
            //val[i] = $(this).parent().text().trim();

            var data = {
                address_type_id: $(this).val(),
                address_type_name: $(this).parent().text().trim(),
                country: $('#ddlcountry option').filter(':selected').val(),
                zip_code: $('#ddlzipcode option').filter(':selected').val(),
                house_no: $('#txthomenumber').val(),
                moo: $('#txtVillageNo').val(),
                village_name: $('#txtvillage').val(),
                building: $('#txtbuilding').val(),
                floor: $('#txtfloor').val(),
                room_no: $('#txtroom').val(),
                soi: $('#txtsoi').val(),
                road: $('#txtroad').val(),
                sub_district_id: $('#ddlsubdistrict option').filter(':selected').val(),
                sub_district_name: $('#ddlsubdistrict option').filter(':selected').text(),
                district_id: $('#ddldistrict option').filter(':selected').val(),
                district_name: $('#ddldistrict option').filter(':selected').text(),
                province_id: $('#ddlprovince option').filter(':selected').val(),
                province_name: $('#ddlprovince option').filter(':selected').text(),
                region_id: $('#ddlzone option').filter(':selected').val()
            }
            stuff.push(data);
            
        });
       
    
        $.ajax({
            type: "POST",
            url: "/Account/SaveDaftAddress",
            data: { daftdata: stuff},
            dataType: "json",
            success: function (data) {

                console.log(data);
                if (data.status) {

                    var val = []
                    val = ConcatstrAddress(data.response);
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

    $('#ddlprovince').change(function () {

        BindDDLdistrict($('#ddlprovince option').filter(':selected').val());
    });
    $('#ddldistrict').change(function () {
        BindDDLsubdistrict($('#ddldistrict option').filter(':selected').val());
    });

    $('#tbaddressstep2').on('click', 'tbody .edit_btn', function () {
        var data_row = tbaddressstep2.row($(this).closest('tr')).data();
        $.ajax({
            type: "POST",
            url: "/Account/GetDaftAddress",
            data: { address_type_id: data_row.address_type_id },
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

                        $('#ddlprovince option').filter(':selected').val(this.province_id)
                        $('#ddlprovince').trigger('change');

                        $('#ddldistrict option').filter(':selected').val(this.district_id)
                        $('#ddldistrict').trigger('change');

                        $('#ddlsubdistrict option').filter(':selected').val(this.sub_district_id)

                        $('#ddlzipcode option').filter(':selected').val(this.zip_code)

                        $('#ddlzone option').filter(':selected').val(this.region_id)

                        $('#txthomenumber').val(this.house_no)
                        $('#txtVillageNo').val(this.moo)
                        $('#txtvillage').val(this.village_name)
                        $('#txtbuilding').val(this.building)
                        $('#txtfloor').val(this.floor)
                        $('#txtroom').val(this.room_no)
                        $('#txtsoi').val(this.soi)
                        $('#txtroad').val(this.road)

                        var addr_id = this.address_type_id
                        $(':checkbox').each(function (i) {
                           
                            if (addr_id == $(this).val()) {
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
            data: { address_type_id: data_row.address_type_id },
            dataType: "json",
            success: function (data) {

                console.log(data);
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

            var strnumber = this.house_no != '' && this.house_no != null ? this.house_no : '';
            var strvillage = this.village_name != '' && this.village_name != null ? $('#txtvillage').parent().parent().text().split(":")[0].trim() + ' ' + this.village_name : '';
            var strvillageno = this.moo != '' && this.moo != null ? $('#txtVillageNo').parent().parent().text().split(":")[1].trim() + ' ' + this.moo : '';

            var strbuilding = this.building != '' && this.building != null ? $('#txtbuilding').parent().parent().text().split(":")[0].trim() + ' ' + this.building : '';
            var strfloor = this.floor != '' && this.floor != null ? $('#txtfloor').parent().parent().text().split(":")[0].trim() + ' ' + this.floor : '';
            var strroom = this.room_no != '' && this.room_no != null ? $('#txtroom').parent().parent().text().split(":")[1].trim() + ' ' + this.room_no : '';
            var strsoi = this.soi != '' && this.soi != null ? $('#txtsoi').parent().parent().text().split(":")[0].trim() + ' ' + this.soi : '';

            var strroad = this.road != '' && this.road != null ? $('#txtroad').parent().parent().text().split(":")[0].trim() + ' ' + this.road : '';
            var strsubdistrict = this.sub_district_id != '' && this.sub_district_id != null ? $('#ddlsubdistrict').parent().parent().text().split(":")[0].trim() + ' ' +
                this.sub_district_name : '';
            var strdistrict = this.district_id != 0 && this.district_id != null ? $('#ddldistrict').parent().parent().text().split(":")[0].trim() + ' ' +
                this.district_name : '';
            var strprovince = this.province_id != 0 && this.province_id != null ? $('#ddlprovince').parent().parent().text().split(":")[0].trim() + ' ' +
                this.province_name : '';

            var strzipcode = this.zip_code != '' && this.zip_code != null ? $('#ddlzipcode').parent().parent().text().split(":")[0].trim() + ' ' +
                this.zip_code : '';

            var strdata = {
                address_type_id: this.address_type_id,
                address_type: this.address_type_name,
                address: strnumber.trim() + ' ' + strvillage.trim() + ' ' + strvillageno.trim() + ' ' + strbuilding.trim() + ' ' + strfloor.trim() + ' ' +
                    strroom.trim() + ' ' + strsoi.trim() + ' ' + strroad.trim() + ' ' + strsubdistrict.trim() + ' ' + strdistrict.trim() + ' ' +
                    strprovince.trim() + ' ' + strzipcode.trim(),

            };

       val.push(strdata);
        });
        return val;
    }

/*************************************/


/*Step3*/

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

/*************************************/
});

function BindDDLprovince() {

    $.ajax({
        type: "POST",
        url: "/Account/DDLsubcontract_profile_province",
        dataType: "json",
        success: function (data) {

            $.each(data.response, function () {
                $('#ddlprovince').append($("<option></option>").val(this.province_id).text(this.province_name));
            });

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
            $('#ddldistrict').empty();
            $.each(data.response, function () {
                $('#ddldistrict').append($("<option></option>").val(this.district_id).text(this.district_name));
            });

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
            $('#ddlsubdistrict').empty();
            $('#ddlzipcode').empty();
            //$('#ddlzipcode').append($("<option></option>").val(0).text('--Select Zip Code--'));
            $.each(data.response, function () {
               
                    $('#ddlsubdistrict').append($("<option></option>").val(this.sub_district_id).text(this.sub_district_name));
                    $('#ddlzipcode').append($("<option></option>").val(this.zip_code).text(this.zip_code));
                
               
            });

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