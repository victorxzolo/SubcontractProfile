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
        table.clear().draw();
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
            { "data": "address_type" },
            { "data": "address" },
            { "data": "" },
            { "data": "" }
        ],
    });

    $('#btnaddaddress').click(function () {
        var val = [];
        $(':checkbox:checked').each(function (i) {
            val[i] = $(this).val();
        });
        console.log(val);
    });


/*************************************/
});




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