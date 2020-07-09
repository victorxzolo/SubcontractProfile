$(document).ready(function () {

    //$('.toggle-password').click(function () {
    //    $(this).children().toggleClass('fa-eye-slash fa-eye');
    //    let input = $(this).prev();
    //    $('#txtpassword').attr('type', $('#txtpassword').attr('type') === 'password' ? 'text' : 'password');

    //});
    $("#chkshowpass").change(function () {
        $(this).is(':checked') ? $('#txtpassword').attr('type', 'text') : $('#txtpassword').attr('type', 'password');

    });


    $('#btnsignin').click(function () {
        var model = {
            username:$('#txtusername').val(),
            password:$('#txtpassword').val()
        }
        $.ajax({
            type: "POST",
            url: "/Account/Login",
            data: {
                model: JSON.stringify(model)
            },
            dataType: "json",
            async: false,
            success: function (data) {
                window.location.href = data.redirecturl;

            },
            error: function (response) {
                //Loading(0);
                //clearForEdit();
                showFeedback("error", "This action is not available.", "System Information",
                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");

              
            }
        });
    });
    
});