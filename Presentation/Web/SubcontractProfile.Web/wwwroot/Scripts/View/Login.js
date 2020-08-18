$(document).ready(function () {

    //$('.toggle-password').click(function () {
    //    $(this).children().toggleClass('fa-eye-slash fa-eye');
    //    let input = $(this).prev();
    //    $('#txtpassword').attr('type', $('#txtpassword').attr('type') === 'password' ? 'text' : 'password');

    //});
    $("#chkshowpass").change(function () {
        $(this).is(':checked') ? $('#txtpassword').attr('type', 'text') : $('#txtpassword').attr('type', 'password');

    });


    $('#btnsignin').click(function (event) {


            var forms = document.getElementsByClassName('needs-validation');
            // Loop over them and prevent submission
            var validation = Array.prototype.filter.call(forms, function (form) {
               // form.addEventListener('submit', function (event) {
                    if (check()) {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                    else {
                        Loading();
                        var modelpass = {
                            username: $('#txtusername').val(),
                            password: $('#txtpassword').val(),
                            keepme: $('#chkkeep').is(':checked') ? true : false,
                            Language: $('#ddlLanguage option').filter(':selected').val()
                        }
                        $.ajax({
                            type: "POST",
                            url: "/Account/Login",
                            data: {model: modelpass},
                            dataType: "json",
                            async: false,
                            success: function (data) {

                                if (data.Response.Status) {
                                    window.location.href = data.redirecturl;
                                }
                                else {
                                    showFeedback("error", data.Response.Message, "System Information",
                                        "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");
                                }

                            },
                            error: function (response) {
                                Loading(0);
                                //clearForEdit();
                                showFeedback("error", "This action is not available.", "System Information",
                                    "<button type='button' class='btn-border btn-black' data-dismiss='modal' id='btncancelpopup'><i class='fa fa-ban icon'></i><span>Cancel</span></button >");


                            }
                        });
                    }
                    //else {
                        form.classList.add('was-validated');
                    //}
                    
               // }, false);
            });
     
    });
});

function check() {
    var errorMessage = $("#idmsAlert");
    $('.toast').toast('hide');
        var hasError = false;

        $(".form-control.inputValidation").each(function () {
            var $this = $(this);
            var fieldvalue = $this.val();

            if (fieldvalue == "") {
              
                hasError = true;
              
            }
        }); //Input

    return hasError; 
  
}