$(document).ready(function () {

    /*Step1*/

    $('#btnsearchlocation').click(function () {
        $('#Searchlocation').modal('show');
    });

    $('#chktype').on("change", function () {
        if ($(this).attr("value") == "N") {
            $("#divdealer").hide('slow');
            $('#divnewsubcontract').show('slow');
        }
        else if ($(this).attr("value") == "D") {
            $("#divnewsubcontract").hide('slow');
            $("#divdealer").show('slow');
        }
    });

    $('#btn_search_modal').click(function () {

    });

    /*************************************/

});