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

        if (!check()) {
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
        }
     
    });
});

function check() {
    var errorMessage = $("#idmsAlert");
    $('.toast').toast('hide');
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
                //errorMessage.show();
                $('.toast').toast('show');
                //errorMessage.html("<p>กรุณาระบุข้อมูลให้ครบถ้วน</p>").show();
            }
            if ($this.val() != "" || $this.val() !=0) {
                $this.removeClass("inputError");
            }
            else {
                return true;
            }
        }); //Input

    return hasError; 
  
}