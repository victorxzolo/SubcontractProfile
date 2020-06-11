function NavMenuClick() {

    $("#nav-icon1").on("click", function () {
        console.log($('.primary-nav').height());
        if ($(".primary-nav").height() > 0) {
            if ($(window).width() > 992) {
                $("#mainnav").show();

            } else {

            }
        } else {

            $("#mainnav").hide();
        }
    });

    $("#ais_topbar-eservice").click(function () {
        if ($(".pp_onlinestore").hasClass("expand") === true) {
            if ($(window).width() > 992) {
                $("#mainnav").hide();
            } else {
            }
        } else {
            if ($(window).width() > 992) {
                $("#mainnav").show();
            } else {
                $("#mainnav").hide();
            }
        }
    });

    $("#pp_onlinestore").on('mouseenter', function () {

    }).on('mouseleave', function () {
        if ($(window).width() > 992) {
            $("#mainnav").show();
        } else {

        }
    });

}

var urlCustomer = "https://business.ais.co.th/"

$(window).load(function() {
    $('body').append('\
        <link rel="stylesheet" type="text/css" href="' + urlCustomer + 'customer-feedback/css/screen-main.css">\
        <script src="' + urlCustomer + 'customer-feedback/js/jquery-validate.js"></script>\
        <script src="' + urlCustomer + 'customer-feedback/js/feedback_scripts.js"></script>\
        <div class="modal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" id="modalFeedBackForm">\
            <div class="modal-dialog modal-cus-size" role="document">\
                <div class="modal-content bg-content">\
                    <div class="header-box"></div>\
                    <button type="button" class="close alert-box-close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>\
                    <div class="section-content-customer-feedback">\
                        <div class="row clear-margin">\
                            <div class="col-md-12">\
                                <div class="row clear-margin">\
                                    <div class="col-md-12">\
                                        <div class="title" style="padding-top:10px;">\
                                            <h4>\
                                                <p class="txtheder">\
                                                    ติดต่อเพื่อขอรับข้อมูลโซลูชั่นจากเรา\
                                                </p>\
                                                <small style="font-size:16px;">กรุณากรอกข้อมูลที่ระบุตามด้านล่างให้ครบถ้วน เพื่อให้เราทราบและบริการแก่คุณได้อย่างดีที่สุด</small>\
                                            </h4>\
                                        </div>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                        <div class="row clear-margin">\
                            <div class="col-md-12">\
                                <div class="col-md-12">\
                                    <hr />\
                                </div>\
                            </div>\
                        </div>\
                        <div class="row clear-margin">\
                            <div class="col-md-12">\
                                <form name="customer_feedback_information" id="customer_feedback_information" method="post" action="">\
                                    <div class="row clear-margin">\
                                        <div class="col-md-6  col-sm-6">\
                                            <div class="form-group label-floating">\
                                                <input type="text" name="name" id="name" class="form-control" placeholder="ชื่อ - นามสกุล*" autocomplete="off" />\
                                            </div>\
                                        </div>\
                                        <div class="col-md-6 col-sm-6">\
                                            <div class="form-group label-floating">\
                                                <input type="number" name="tel" id="tel" class="form-control" placeholder="หมายเลขโทรศัพท์* (ตัวอย่าง:081xxxxxxx หรือ 02xxxxxxx)" maxlength="10" autocomplete="off" pattern="[0-9]*" />\
                                                <span class="help-block help-msg"></span>\
                                                <span class="help-err bmd-help-tel"></span>\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row clear-margin">\
                                        <div class="col-md-6 col-sm-6">\
                                            <div class="form-group label-floating">\
                                                <input type="email" name="email" id="email" class="form-control" placeholder="อีเมล*" autocomplete="off" />\
                                                <span class="help-err bmd-help-email"></span>\
                                            </div>\
                                        </div>\
                                        <div class="col-md-6 col-sm-6" id="dd-dep">\
                                            <div class="form-group label-floating">\
                                                <select name="department" class="form-control dropdown-style" id="department">\
                                                    <option value="">แผนก / ตำแหน่ง</option>\
                                                    <option value="Accounting / Finance" data-flag="1">Accounting / Finance</option>\
                                                    <option value="Admin &amp; HR &amp; Human Development" data-flag="2">Admin &amp; HR &amp; Human Development</option>\
                                                    <option value="Engineering" data-flag="3">Engineering</option>\
                                                    <option value="Information Technology (IT)" data-flag="4">Information Technology (IT)</option>\
                                                    <option value="Marketing / Public Relations" data-flag="5">Marketing / Public Relations</option>\
                                                    <option value="Media &amp; Advertising" data-flag="6">Media &amp; Advertising</option>\
                                                    <option value="Merchandising &amp; Purchasing" data-flag="7">Merchandising &amp; Purchasing</option>\
                                                    <option value="Store / Warehouse" data-flag="8">Store / Warehouse</option>\
                                                    <option value="Secretary &amp; Consultancy" data-flag="9">Secretary &amp; Consultancy</option>\
                                                    <option value="Other (กรุณาระบุ)" data-flag="10">Other (กรุณาระบุ)</option>\
                                                </select>\
                                            </div>\
                                        </div>\
                                        <div class="col-md-3 col-sm-3" id="field-other" style="display:none;">\
                                            <div class="form-group label-floating">\
                                                <input type="text" name="otherdep" id="otherdep" class="form-control" placeholder="Other (ระบุข้อมูล)" disabled autocomplete="off" />\
                                                <span class="bmd-help-otherdep"></span>\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row clear-margin">\
                                        <div class="col-md-6 col-sm-6">\
                                            <div class="form-group label-floating">\
                                                <input type="text" name="company" id="company" class="form-control" placeholder="ชื่อบริษัท" autocomplete="off" />\
                                            </div>\
                                        </div>\
                                        <div class="col-md-6 col-sm-6">\
                                            <div class="form-group label-floating">\
                                                <input type="text" name="businessType" id="businessType" class="form-control" placeholder="ประเภทธุรกิจ" autocomplete="off" />\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row clear-margin box-business">\
                                        <div class="col-md-2 col-sm-2" style="padding-right:0px;">\
                                            <label for="size" class="control-label title-radio text">ขนาดธุรกิจ*</label>\
                                        </div>\
                                        <div class="col-md-8 col-sm-8">\
                                            <div class="form-group label-floating">\
                                                <div class="btn-group " data-toggle="buttons">\
                                                    <label class="btn radio-box">\
                                                        <input type="radio" name="size" value="ธุรกิจขนาดย่อม">\
                                                        <i class="fa fa-circle-o " style="font-size:16px;"></i>\
                                                        <i class="fa fa-dot-circle-o " style="font-size:16px;"></i>\
                                                        <span> ธุรกิจขนาดย่อม</span>\
                                                    </label>\
                                                    <label class="btn radio-box">\
                                                        <input type="radio" name="size" value="ธุรกิจขนาดใหญ่">\
                                                        <i class="fa fa-circle-o " style="font-size:16px;"></i>\
                                                        <i class="fa fa-dot-circle-o " style="font-size:16px;"></i>\
                                                        <span> ธุรกิจขนาดใหญ่</span>\
                                                    </label>\
                                                </div>\
                                                <span class="bmd-help-size"></span>\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row clear-margin">\
                                        <a name="title-sulotion-top"></a>\
                                        <div class="col-md-12 col-sm-10">\
                                            <label class="control-label title" id="title-sulotion" style="cursor:pointer;">แพ็กเกจและโซลูชั่นของเรา ที่คุณสนใจ*</label>&nbsp;&nbsp;&nbsp;\
                                            <span class="bmd-help-solution"></span>\
                                        </div>\
                                    </div>\
                                    <div class="row clear-margin ">\
                                        <div class="col-md-12 box-dd-solution col-sm-12 ">\
                                            <div class="form-group label-floating">\
                                                <div class="form-control dropdown-style" id="dd-solution">\
                                                    <div class="txt-cus-select">กรุณาเลือกโซลูชั่นที่สนใจ</div>\
                                                </div>\
                                            </div>\
                                        </div>\
                                        <div class="col-md-12  col-sm-12 section-content-solution-area">\
                                        <div class="col-md-12  col-sm-12 section-content-solution">\
                                            <div class="box-souotion" id="box-souotion">\
                                                <div class="row clear-margin group-chk-solution">\
                                                    <div class="col-md-12 col-sm-12">\
                                                        <label class="control-label title-2">โซลูชั่นด้านการเคลื่อนที่</label>\
                                                    </div>\
                                                    <div class="col-md-12 col-sm-12 ">\
                                                        <div class="box-sub-solution">\
                                                                <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                    <div class="box-chk-dis"></div>\
                                                                    <div class="btn-group content-solution" data-toggle="buttons">\
                                                                        <label class="btn  check-box ">\
                                                                            <input class="chk-solution" type="checkbox" name="productInterest[]" value="MPBX" >\
                                                                            <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                            <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                            <span>MPBX</span>\
                                                                        </label>\
                                                                    </div>\
                                                                </div>\
                                                                <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                    <div class="box-chk-dis"></div>\
                                                                    <div class="btn-group content-solution" data-toggle="buttons">\
                                                                        <label class="btn  check-box">\
                                                                            <input class="chk-solution" type="checkbox" name="productInterest[]" value="Live &amp; Talk">\
                                                                            <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                            <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                            <span>Live &amp; Talk</span>\
                                                                        </label>\
                                                                    </div>\
                                                                </div>\
                                                                <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                    <div class="box-chk-dis"></div>\
                                                                    <div class="btn-group content-solution" data-toggle="buttons">\
                                                                        <label class="btn  check-box ">\
                                                                            <input class="chk-solution" type="checkbox" name="productInterest[]" value="Unified Communication" >\
                                                                            <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                            <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                            <span>Unified Communication</span>\
                                                                        </label>\
                                                                    </div>\
                                                                </div>\
                                                                <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                    <div class="box-chk-dis"></div>\
                                                                    <div class="btn-group content-solution" data-toggle="buttons">\
                                                                        <label class="btn  check-box ">\
                                                                            <input class="chk-solution" type="checkbox" name="productInterest[]" value="mForm" >\
                                                                            <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                            <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                            <span>mForm</span>\
                                                                        </label>\
                                                                    </div>\
                                                                </div>\
                                                                <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                    <div class="box-chk-dis"></div>\
                                                                    <div class="btn-group content-solution" data-toggle="buttons">\
                                                                        <label class="btn  check-box ">\
                                                                            <input class="chk-solution" type="checkbox" name="productInterest[]" value="Bulk SMS" >\
                                                                            <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                            <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                            <span>Bulk SMS</span>\
                                                                        </label>\
                                                                    </div>\
                                                                </div>\
                                                                <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                    <div class="box-chk-dis"></div>\
                                                                    <div class="btn-group content-solution" data-toggle="buttons">\
                                                                        <label class="btn  check-box ">\
                                                                            <input class="chk-solution" type="checkbox" name="productInterest[]" value="Office 365" >\
                                                                            <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                            <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                            <span>Office 365</span>\
                                                                        </label>\
                                                                    </div>\
                                                                </div>\
                                                                <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                    <div class="box-chk-dis"></div>\
                                                                    <div class="btn-group content-solution" data-toggle="buttons">\
                                                                        <label class="btn  check-box ">\
                                                                            <input class="chk-solution" type="checkbox" name="productInterest[]" value="Enterprise Mobility Management (EMM)" >\
                                                                            <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                            <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                            <span>Enterprise Mobility Management (EMM)</span>\
                                                                        </label>\
                                                                    </div>\
                                                                </div>\
                                                                <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                    <div class="box-chk-dis"></div>\
                                                                    <div class="btn-group content-solution" data-toggle="buttons">\
                                                                        <label class="btn  check-box ">\
                                                                            <input class="chk-solution" type="checkbox" name="productInterest[]" value="Food Solutions" >\
                                                                            <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                            <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                            <span>Food Solutions</span>\
                                                                        </label>\
                                                                    </div>\
                                                                </div>\
                                                        </div>\
                                                    </div>\
                                                </div>\
                                                            <div class="row clear-margin group-chk-solution">\
                                                                <div class="col-md-12 col-sm-12">\
                                                                    <label class="control-label title-2">โซลูชั่นด้านการติดต่อสื่อสาร</label>\
                                                                </div>\
                                                                <div class="col-md-12 col-sm-12 ">\
                                                                    <div class="box-sub-solution">\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Corporate Fixed Line" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Corporate Fixed Line</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="MPBX" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>MPBX</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Live &amp; Talk" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Live &amp; Talk</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Unified Communication" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Unified Communication</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="mForm" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>mForm</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Bulk SMS" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Bulk SMS</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="mTargeting" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>mTargeting</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Office 365" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Office 365</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                    </div>\
                                                                </div>\
                                                            </div>\
                                                            <div class="row clear-margin group-chk-solution">\
                                                                <div class="col-md-12 col-sm-12">\
                                                                    <label class="control-label title-2">โซลูชั่นผลิตภาพและการประสานงาน</label>\
                                                                </div>\
                                                                <div class="col-md-12 col-sm-12 ">\
                                                                    <div class="box-sub-solution">\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="MPBX" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>MPBX</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Live &amp; Talk" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Live &amp; Talk</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Unified Communication" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Unified Communication</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="mForm" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>mForm</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Smart messaging" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Smart messaging</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Office 365" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Office 365</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="AIS WeCloud" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>AIS WeCloud</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Mobile Track and Trace" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Mobile Track and Trace</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Enterprise Mobility Management (EMM)" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Enterprise Mobility Management (EMM)</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Smart Tracking" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Smart Tracking</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Mobile Pro Delivery" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Mobile Pro Delivery</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Flow Account" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Flow Account</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Food Solutions" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Food Solutions</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                    </div>\
                                                                </div>\
                                                            </div>\
                                                            <div class="row clear-margin group-chk-solution">\
                                                                <div class="col-md-12 col-sm-12">\
                                                                    <label class="control-label title-2">โซลูชั่นทางการตลาดดิจิตอล</label>\
                                                                </div>\
                                                                <div class="col-md-12 col-sm-12 ">\
                                                                    <div class="box-sub-solution">\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Smart messaging" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Smart messaging</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Bulk SMS" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Bulk SMS</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="mTargeting" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>mTargeting</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Reward Platform" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Reward Platform</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                    </div>\
                                                                </div>\
                                                            </div>\
                                                            <div class="row clear-margin group-chk-solution">\
                                                                <div class="col-md-12 col-sm-12">\
                                                                    <label class="control-label title-2">โซลูชั่นด้านเครือข่าย</label>\
                                                                </div>\
                                                                <div class="col-md-12 col-sm-12 ">\
                                                                    <div class="box-sub-solution">\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Corporate Internet" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Corporate Internet</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Domestic Data Circuit" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Domestic Data Circuit</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                    </div>\
                                                                </div>\
                                                            </div>\
                                                            <div class="row clear-margin group-chk-solution">\
                                                                <div class="col-md-12 col-sm-12">\
                                                                    <label class="control-label title-2">โซลูชั่นด้าน IoT/ M2M</label>\
                                                                </div>\
                                                                <div class="col-md-12 col-sm-12 ">\
                                                                    <div class="box-sub-solution">\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Mobile Track and Trace" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Mobile Track and Trace</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Smart Tracking" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Smart Tracking</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                    </div>\
                                                                </div>\
                                                            </div>\
                                                            <div class="row clear-margin group-chk-solution">\
                                                                <div class="col-md-12 col-sm-12">\
                                                                    <label class="control-label title-2">โซลูชั่นด้าน Cloud</label>\
                                                                </div>\
                                                                <div class="col-md-12 col-sm-12 ">\
                                                                    <div class="box-sub-solution">\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="MPBX" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>MPBX</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Live &amp; Talk" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Live &amp; Talk</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Unified Communication" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Unified Communication</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Smart messaging" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Smart messaging</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Enterprise Cloud (VMware)" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Enterprise Cloud (VMware)</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Office 365" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Office 365</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="AIS WeCloud" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>AIS WeCloud</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Enterprise Mobility Management (EMM)" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Enterprise Mobility Management (EMM)</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Mobile Pro Delivery" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Mobile Pro Delivery</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Flow Account" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Flow Account</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Food Solutions" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Food Solutions</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                    </div>\
                                                                </div>\
                                                            </div>\
                                                            <div class="row clear-margin group-chk-solution">\
                                                                <div class="col-md-12 col-sm-12">\
                                                                    <label class="control-label title-2">โซลูชั่นด้านความปลอดภัย</label>\
                                                                </div>\
                                                                <div class="col-md-12 col-sm-12 ">\
                                                                    <div class="box-sub-solution">\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                            <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Enterprise Mobility Management (EMM)" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Enterprise Mobility Management (EMM)</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                    </div>\
                                                                </div>\
                                                            </div>\
                                                            <div class="row clear-margin group-chk-solution">\
                                                                <div class="col-md-12 col-sm-12">\
                                                                    <label class="control-label title-2">โซลูชั่นเฉพาะธุรกิจ</label>\
                                                                </div>\
                                                                <div class="col-md-12 col-sm-12 ">\
                                                                    <div class="box-sub-solution">\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Mobile Track and Trace" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Mobile Track and Trace</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Smart Tracking" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Smart Tracking</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Mobile Pro Delivery" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Mobile Pro Delivery</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                            <div class="col-md-4 col-sm-6 col-xs-12 chk-box-area">\
                                                                                <div class="box-chk-dis"></div>\
                                                                                <div class="btn-group content-solution" data-toggle="buttons">\
                                                                                    <label class="btn  check-box ">\
                                                                                        <input class="chk-solution" type="checkbox" name="productInterest[]" value="Food Solutions" >\
                                                                                        <i class="fa fa-square-o " style="font-size:18px;"></i>\
                                                                                        <i class="fa fa-check-square-o " style="font-size:18px;"></i>\
                                                                                        <span>Food Solutions</span>\
                                                                                    </label>\
                                                                                </div>\
                                                                            </div>\
                                                                    </div>\
                                                                </div>\
                                                            </div>\
                                            </div>\
                                            <div id="btn-sel-solution-area" class="row clear-margin">\
                                                <div class="col-md-12 col-sm-12">\
                                                    <div class="pull-right clear-solution-box">\
                                                        <input type="button" class="btn btn-default " value="ยกเลิกทั้งหมด" id="btnClearAll" />\
                                                        <input type="button" class="btn btn-default " value="ดูโซลูชั่นที่เลือก" id="btnCloseBox" />\
                                                    </div>\
                                                </div>\
                                            </div>\
                                        </div>\
                                        </div>\
                                    </div>\
                                    <div class="row clear-margin">\
                                        <div class="col-md-12 text-center div-btn-sumit-form col-sm-12">\
                                            <button id="btn-send" class="btn btn-success" >ส่งข้อความ</button>\
                                        </div>\
                                    </div>\
                                    <div class="row clear-margin">\
                                        <div class="col-md-12 col-sm-12">\
                                            &nbsp;\
                                        </div>\
                                    </div>\
                                </form>\
                            </div>\
                        </div>\
                    </div>\
                </div><!-- /.modal-content -->\
            </div><!-- /.modal-dialog -->\
        </div>\
        <div class="modal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" id="modalFeedBackFormAlert">\
            <div class="modal-dialog modal-md" role="document">\
                <div class="modal-content bg-content">\
                    <div class="header-box"></div>\
                    <div class="section-content-customer-feedback-alert">\
                        <div class="row">\
                            <div class="col-md-12">\
                                <button type="button" class="close alert-box-close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>\
                            </div>\
                        </div>\
                        <div class="row">\
                            <div id="txt-msg" class="col-md-12 text-center alert-message db_heavent_cond">กรุณารอสักครู่ <br>ระบบกำลังดำเนินการ...</div>\
                        </div>\
                    </div>\
                </div><!-- /.modal-content -->\
            </div><!-- /.modal-dialog -->\
        </div>\
        <script>\
        jQuery(document).ready(function($){\
            jQuery(document).on("click", "#dd-solution, .txt-cus-select", function (event) {\
                $(".box-dd-solution").hide();\
                $(".section-content-solution-area").show();\
                $(".content-solution .check-box").parent().parent().show();\
                $("#btn-sel-solution-area").show();\
                $(".group-chk-solution").show();\
                $(".box-chk-dis").hide();\
                document.location = "#title-sulotion-top";\
            });\
            jQuery(document).on("change", "#department", function () {\
                if ($("#department option:selected").attr("data-flag") == "10") {\
                    $("#dd-dep").removeClass("col-md-6 col-sm-6").addClass("col-md-3 col-sm-3");\
                    $("#otherdep").prop("disabled", false);\
                    $("#field-other").show();\
                    $("#field-other").addClass("field-other");\
                    $("#dd-dep").addClass("dd-dep");\
                } else {\
                    $("#dd-dep").removeClass("col-md-3 col-sm-3").addClass("col-md-6 col-sm-6");\
                    $("#field-other").hide();\
                    $("#otherdep").prop("disabled", true).val("");\
                    $("#field-other").removeClass("field-other");\
                    $("#dd-dep").removeClass("dd-dep");\
                }\
            });\
            jQuery(document).on("click", ".btn-group.content-solution", function () {\
                var chkNum = 0;\
                $("input[name=\'productInterest[]\']:checkbox").each(function () {\
                    var ischecked = $(this).is(":checked");\
                    if (ischecked) {\
                        chkNum = chkNum + 1;\
                    }\
                });\
                if (chkNum == 0) {\
                    $(\'.bmd-help-solution\').html(\'<label id="productInterest[]-error" class="error" for="productInterest[]" style="">กรุณาเลือกโซลูชั่น</label>\');\
                }else{\
                    $(\'.bmd-help-solution\').html(\'\');\
                }\
            });\
            jQuery(document).on("click", "#btnClearAll", function () {\
                $("input[name=\'productInterest[]\']").prop("checked", false);\
                $(".check-box").removeClass("active");\
                document.location = "#title-sulotion-top";\
            });\
            jQuery(document).on("click", "#btnCloseBox", function () {\
                var chkNum = 0;\
                $("input[name=\'productInterest[]\']:checkbox").each(function () {\
                    var ischecked = $(this).is(":checked");\
                    if (ischecked) {\
                        chkNum = chkNum + 1;\
                    }\
                });\
                if (chkNum == 0) {\
                    $(\'.bmd-help-solution\').html(\'<label id="productInterest[]-error" class="error" for="productInterest[]" style="">กรุณาเลือกโซลูชั่น</label>\');\
                }else{\
                    $(\'.bmd-help-solution\').html(\'\');\
                    $(".box-dd-solution").show();\
                    $("#btn-sel-solution-area").hide();\
                    $(".content-solution .check-box").parent().parent().hide();\
                    $(".content-solution .check-box.active").parent().parent().show();\
                    $(".group-chk-solution").each(function() {\
                        var checked = false;\
                        $(this).find(".btn.check-box.active").each(function() {\
                            checked = true;\
                        });\
                        if (!checked) {\
                            $(this).css("display", "none");\
                        }\
                    });\
                    $(".chk-box-area").each(function() {\
                        if ($(this).css("display") == "block"){\
                            $(this).find(".box-chk-dis").css("display","block");\
                        }\
                    });\
                }\
                document.location = "#title-sulotion-top";\
            });\
            $("#tel").keypress(function (e) {\
                if(this.value.length==10) return false;\
                if ($(this).val() == "") {\
                    if (e.keyCode != 48) {\
                        e.preventDefault();\
                    }\
                }\
                var keycode = e.which;\
                if (!(e.shiftKey == false && ((keycode >= 48 && keycode <= 57)))) {\
                    e.preventDefault();\
                }\
            });\
            jQuery(document).on("click", ".callCustomerFeedback", function (event) {\
                if ($(this).attr("data-feedback")) {\
                    var cusArray = $(this).attr("data-feedback").split(",");\
                    var numItemchk = 1;\
                    var chkNumShow = 0;\
                    $(".btn.check-box").each(function() {\
                        if (jQuery.inArray(numItemchk.toString(), cusArray) >= 0){\
                            $(this).find("input[name=\'productInterest[]\']").prop("checked", true);\
                            $(this).addClass("active");\
                            chkNumShow = chkNumShow + 1;\
                        }\
                        numItemchk = numItemchk + 1;\
                    });\
                    if (chkNumShow > 0) {\
                        $(\'.bmd-help-solution\').html(\'\');\
                        $(".box-dd-solution").show();\
                        $("#btn-sel-solution-area").hide();\
                        $(".content-solution .check-box").parent().parent().hide();\
                        $(".content-solution .check-box.active").parent().parent().show();\
                        $(".group-chk-solution").each(function() {\
                            var checked = false;\
                            $(this).find(".btn.check-box.active").each(function() {\
                                checked = true;\
                            });\
                            if (!checked) {\
                                $(this).css("display", "none");\
                            }\
                        });\
                        $(".chk-box-area").each(function() {\
                            if ($(this).css("display") == "block"){\
                                $(this).find(".box-chk-dis").css("display","block");\
                            }\
                        });\
                        $(".section-content-solution-area").show();\
                    }\
                }\
                $("#modalFeedBackForm").modal("show");\
            });\
        });\
        </script>\
        ');
    //$('#modalFeedBackForm').modal('show');
});