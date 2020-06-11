$(window).ready(function() {
	$("#hero-header").slick({
        lazyLoad: "ondemand",
        arrows: true,
        autoplay: true,
        autoplaySpeed: 8000,
        dots: true,
        adaptiveHeight:false,
        responsive: [{
            breakpoint: 480,
            settings: {
                dots: false
            }
        }]
    });

    $("#hero-header-m").slick({
        lazyLoad: "ondemand",
        arrows: true,
        dots: false,
        autoplay: true,
        autoplaySpeed: 8000,
        adaptiveHeight:false,
    });

    $(".carousel-container").slick({
        lazyLoad: "ondemand",
        slidesToShow: 4,
        slidesToScroll: 4,
        autoplay: false,
        autoplaySpeed: 4000,
        dots: true,
        infinite:true,
        responsive: [
        	{
	        	breakpoint: 991,
	            settings: {
	                slidesToShow: 3,
	        		slidesToScroll: 1,
	        		// dots:true
	        	}
            },
            {
	            breakpoint: 767,
	            settings: {
	                slidesToShow: 2,
	        		slidesToScroll: 1,
	        		centerMode:true,
	        		// dots:true,
	        		autoplaySpeed: 7000,
	            }
        	},
        	{
	            breakpoint: 575,
	            settings: {
	                slidesToShow: 1,
	        		slidesToScroll: 1,
	        		centerMode:true,
	        		// dots:true,
	        		centerPadding:'60px',
	        		autoplaySpeed: 7000,
	            }
        	},
        	{
	            breakpoint: 359,
	            settings: {
	                slidesToShow: 1,
	        		slidesToScroll: 1,
	        		centerMode:true,
	        		// dots:true,
	        		centerPadding:'40px',
	        		autoplaySpeed: 7000,
	            }
        	}
        ]
    });

    $('.solution-groups-wrapper').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        asNavFor: '#solutions-header',
        swipe: false
    });

    $('#solutions-header').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        asNavFor: '.solution-groups-wrapper',
        focusOnSelect: true,
        swipe: false,
        initialSlide: 4,
        appendArrows: $("#solutions > .container"),
        responsive: [{
            breakpoint: 767,
            settings: {
                slidesToShow: 2,
                arrows: true
            }
        }]
    });

    $("#solution-recommended .slide-container").slick({
        slidesToShow: 3,
        slidesToScroll: 3,
        arrows: false,
        dots: true,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [{
            breakpoint: 767,
            settings: {
                centerMode: true,
                slidesToShow: 2,
                slidesToScroll: 2
            }
        }, {
            breakpoint: 480,
            settings: {
                dots: false,
                centerMode: true,
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    });

    $('#home').find('.solution-group').slick({
        slidesToShow: 3,
        slidesToScroll: 3,
        arrows: false,
        dots: true,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [{
            breakpoint: 767,
            settings: {
                centerMode: true,
                slidesToShow: 2,
                slidesToScroll: 2
            }
        }, {
            breakpoint: 480,
            settings: {
                dots: false,
                centerMode: true,
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    });

    $("#business-selector").slick({
        arrows: true,
        dots: true,
        slidesToShow: 4,
        slidesToScroll: 4,
        prevArrow: '<div class="slick-prev"><img src="https://business.ais.co.th/assets/revamp/img/industry/industry-arrow-prev.png"></div>',
        nextArrow: '<div class="slick-next"><img src="https://business.ais.co.th/assets/revamp/img/industry/industry-arrow-next.png"></div>',
        rows: 2,
        responsive: [{
            breakpoint: 480,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
                rows: 1
            }
        }]
    });

    var solutionGroup = $('#solution').find(".solution-group");

    solutionGroup.slick({
        slidesToShow: 3,
        slidesToScroll: 3,
        autoplay: true,
        autoplaySpeed: 5000,
        arrows: false,
        dots: true,
        responsive: [{
            breakpoint: 767,
            settings: {
                centerMode: true,
                slidesToShow: 2,
                slidesToScroll: 2
            }
        }, {
            breakpoint: 480,
            settings: {
                dots: false,
                centerMode: true,
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    });

    $("body#home #videoTestimonialWrapper").slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 5000,
        adaptiveHeight: true,
        arrows: true,
        dots: false,
        nextArrow: '<button type="button" class="slick-next">' +
        '<img src="assets/revamp/img/solution-detail/arrow-right.png"></button>',
        prevArrow: '<button type="button" class="slick-prev">' +
        '<img src="assets/revamp/img/solution-detail/arrow-left.png"></button>',
        responsive: [
            {
                breakpoint: 991,
                settings: {
                    autoplay: true,
                    slidesToShow: 2,
                    arrows: true,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 767,
                settings: {
                    autoplay: true,
                    slidesToShow: 1,
                    arrows: true,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 576,
                settings: {
                    autoplay: false,
                    autoplaySpeed: 3000,
                    arrows: true,
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    });

    $('.solution-filter-dropdown').find('a').on('show.bs.tab', function(event) {
        $('#sol-filter').html($(this).html());
    });
    $('.solution-filter-dropdown').find('a').on('shown.bs.tab', function(event) {
        $(this).parent().removeClass('active');
        if ($(window).width() < 520) {
            $('.sol-item').matchHeight({
                remove: true
            });
            $('.mobile-slick').slick('unslick');
            $('.sol-item').matchHeight();
            $('.mobile-slick').slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                arrows: true,
                dots: false
            });
        }
        $( '.solution-filter-result slick' ).slick('slickGoTo', 0, true);
    });
    setTimeout(function() {
        $("#loader .loading-img").css('opacity','1');
    },1500);

    $('.privilege-item').matchHeight();
});

$(window).load(function() {
    setTimeout(function() {
        $("#loader").fadeOut();
    }, 20);

    $('.test2').remove();

    $('.slide-touch').scroll(function() {
        $(this).children('.hand-touch').fadeOut(300);
    });

    if ($(".sol-item").length) {
        $(".sol-item").matchHeight();
    }

    var newsItem = $("#newsActivitiesWrapper").find(".ng-scope");
    setTimeout(function () {
        if (newsItem.length) {
            newsItem.matchHeight();
        }
    }, 100);


    if ($(window).width() < 520) {
        $(".benefit-wrapper").slick();
        $(".mobile-slick").slick();
    }

    $("#sme").find("#videoTestimonialWrapper").slick({
        adaptiveHeight: true
    });
    $("#enterprise").find("#videoTestimonialWrapper").slick({
        adaptiveHeight: true
    });

    function updateSolutionsDesc(id) {
        var desc = "";
        switch (id) {
            case "1":
                desc = "เทคโนโลยีที่ตอบโจทย์การดำเนินธุรกิจในยุคดิจิตอลซึ่งบริษัทไม่หยุดนิ่งอยู่กับที่ และโทรศัพท์มือถือกลายเป็นอุปกรณ์ที่ติดตัวลูกค้าอยู่ตลอดเวลา";
                desc_en = "Cutting-edge technology for businesses in the digital age that mobile device is a crucial part of customer's daily life.";
                break;
            case "2":
                desc = "เพราะการสื่อสารเป็นปัจจัยพื้นฐานสำคัญของความสำเร็จ บริษัทจึงจำเป็นต้องมีเทคโนโลยีและเครื่องมือที่ช่วยผลักดันให้ธุรกิจก้าวไปข้างหน้าได้อย่างมั่นใจ";
                desc_en = "Communication is business's key to success. We offer you reliable communication tools and technologies that will confidently and sustainably drive your business forward.";
                break;
            case "3":
                desc = "เพิ่มประสิทธิภาพในการทำงานของพนักงานในองค์กรด้วยเทคโนโลยีต่างๆ ที่จะช่วยให้การประสานงานภายในองค์กรสะดวกและรวดเร็วยิ่งขึ้น";
                desc_en = "Increasing employees' productivity and business performance through a variety of technologies that will improve internal collaboration.";
                break;
            case "4":
                desc = "เครื่องมือสำหรับการทำกิจกรรมส่งเสริมการตลาด และสร้างความสัมพันธ์กับลูกค้าของธุรกิจในยุคดิจิตอลที่สามารถเข้าถึงกลุ่มเป้าหมายที่ต้องการได้";
                desc_en = "Marketing support tools for creating customer awareness and loyalty in this digital age, enabling you to directly reach your target groups.";
                break;
            case "5":
                desc = "เครือข่ายที่มีความเสถียรสูงสุดเพื่อความต่อเนื่องในการดำเนินธุรกิจของคุณ และพร้อมปรับเปลี่ยนไปตามความต้องการใช้งานในแต่ละช่วงเวลา";
                desc_en = "A trusted network with highest stability that ensures your business continuity and ready to be adjusted to business's demand changing.";
                break;
            case "6":
                desc = "โซลูชันการเชื่อมต่อข้อมูลบนเครือข่ายไร้สายระหว่างอุปกรณ์กับศูนย์กลาง โดยอาศัยแพล็ทฟอร์มสำหรับบริหารจัดการรูปแบบการเชื่อมต่อตามลักษณะของธุรกิจ";
                desc_en = "The best-quality wireless network solution for data transmitting between your IoT devices and processing center. Together with AIS IoT Platform, your business requirements can be fulfilled.";
                break;
            case "7":
                desc = "คลาวด์แพล็ทฟอร์มที่พร้อมด้วยหลากหลายบริการ ตั้งแต่โครงสร้างพื้นฐาน (IaaS) ไปจนถึงระบบฐานข้อมูล ระบบประมวลผลต่างๆ ระบบสำรองข้อมูล (PaaS) และซอฟท์แวร์/ แอพพลิเคชั่น ต่างๆ (SaaS) เพื่อความรวดเร็วและความยืดหยุ่นในการใช้งาน ในภาระต้นทุนที่บริหารจัดการได้อย่างมีประสิทธิภาพ";
                desc_en = "A complete Cloud Platform with a variety of services, ranging from Infrastructure as a Service (IaaS), databases, data processing, backup, Platform as a Service (PaaS) to Applications/ Software as a Service (SaaS). All efficient solutions come with flexibility, agility and cost effectiveness.";
                break;
            case "8":
                desc = "เพิ่มความมั่นใจในด้านความปลอดภัยและความเป็นส่วนตัวของข้อมูลขององค์กรให้มากยิ่งขึ้นด้วยบริการด้านความปลอดภัยต่างๆ ทั้งบนอุปกรณ์เคลื่อนที่และเครือข่ายของคุณ";
                desc_en = "Create peace of mind for you in business data security and privacy with our highest level of security solutions on both of your network and end-point mobile devices.";
                break;
            case "9":
                desc = "บริการพิเศษสำหรับแต่ละเฉพาะธุรกิจ ด้วยคุณสมบัติที่จะช่วยเพิ่มประสิทธิภาพและรองรับการเจริญเติบโตของธุรกิจทั้งขนาดเล็กและใหญ่";
                desc_en = "Special solutions for specific businesses to increase productivity, gain efficiency and boost growth for both small and large businesses.";
                break;
            default:
                desc = "พบกับโซลูชันพิเศษที่เราคัดสรรมาเพื่อคุณและธุรกิจของคุณ";
                desc_en = "Selective solutions for you and your business.";
        }
        $("#solution-desc").find("#desc").text(desc);
        $("#solution-desc").find("#desc.en").text(desc_en);
        return true;
    }

    var recSol = $("#solutions-header-recommended .solution-recommended");
    var recSolW = $("#solution-recommended-wrapper");
    var solHead = $("#solutions-header .solution-item");
    var solW = $("#solutions-wrapper");
    var solutions = $("#solution-desc");
    var solutionArrow = $("#solutions").find(".slick-arrow");
    var className = "hai-pai-kon";
    var solutionGroup = $('#solution').find(".solution-group");


    solHead.click(function() {
        if (!(recSolW.hasClass(className))) {
            solW.removeClass(className);
            recSolW.addClass(className);
        }
        if (solW.hasClass(className)) {
            recSolW.removeClass(className);
            solW.removeClass(className);
        }
        if (!(solutions.hasClass("normalSolutionActive"))) {
            solutions.addClass("normalSolutionActive");
        }
        // $('.sol-recommended').slick('unslick');
        $('#solution-recommended-wrapper .solution-groups-wrapper').slick('unslick');
        updateSolutionsDesc($(this).attr("descid"));
    });

    recSol.click(function() {
        if (!(solW.hasClass(className))) {
            solW.addClass(className);
        }
        if (recSolW.hasClass(className)) {
            $(".sol-recommended").slick('unslick');
            recSolW.removeClass(className);
            $('.sol-recommended').find('.sol-item').matchHeight(sx());
            function sx() {
                $(".sol-recommended").slick({
                    slidesToShow: 3,
                    arrows: false,
                    dots: true,
                    slidesToScroll: 3,
                    responsive: [{
                        breakpoint: 767,
                        settings: {
                            centerMode: true,
                            slidesToShow: 2,
                            slidesToScroll: 2
                        }
                    }, {
                        breakpoint: 480,
                        settings: {
                            dots: false,
                            centerMode: true,
                            slidesToShow: 1,
                            slidesToScroll: 1
                        }
                    }]
                });
            }
        }
        if (solutions.hasClass("normalSolutionActive")) {
            solutions.removeClass("normalSolutionActive");
        }
        updateSolutionsDesc($(this).attr("descid"));
    });

    solutionArrow.click(function() {
        if (!(recSolW.hasClass(className))) {
            solW.removeClass(className);
            recSolW.addClass(className);
        }
        if (solW.hasClass(className)) {
            recSolW.removeClass(className);
            solW.removeClass(className);
        }
        var currentSlickItem = $("#solutions-header").find(".slick-current");
        if (!(solutions.hasClass("normalSolutionActive"))) {
            solutions.addClass("normalSolutionActive");
        }
        updateSolutionsDesc(currentSlickItem.attr("descid"));
    });

    var filterSelect = $("#enterprise-page .solution-filter li a"),
        filterBtn = $("#enterprise-page .solution-filter button");

    filterSelect.click(function() {
        event.preventDefault();
        filterBtn.text($(this).text());
    });
    

    $('#newsActivities').find('.nav-pills li a').on('shown.bs.tab', function (e) {
        var activeTab = $(e.target).attr('href');
        var prevTab = $(e.relatedTarget).attr('href');

        $(activeTab).find('.slider').slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 5000,
            adaptiveHeight: true,
            arrows: true,
            dots: false,
            nextArrow: '<button type="button" class="slick-next">' +
            '<img src="assets/revamp/img/solution-detail/arrow-right.png" alt="next"></button>',
            prevArrow: '<button type="button" class="slick-prev">' +
            '<img src="assets/revamp/img/solution-detail/arrow-left.png" alt="prev"></button>',
            responsive: [
                {
                    breakpoint: 991,
                    settings: {
                        autoplay: true,
                        slidesToShow: 2,
                        arrows: true,
                        slidesToScroll: 1
                    }
                },
                {
                    breakpoint: 767,
                    settings: {
                        autoplay: true,
                        slidesToShow: 1,
                        arrows: true,
                        slidesToScroll: 1
                    }
                },
                {
                    breakpoint: 576,
                    settings: {
                        autoplay: false,
                        autoplaySpeed: 3000,
                        arrows: true,
                        slidesToShow: 1,
                        slidesToScroll: 1
                    }
                }
            ]
        });

        $(prevTab).find('.slider').slick('unslick');
    });

    var shareTitle = document.title;

    if($("#news-activities-content")) {
        $(".share-twitter").attr("href", "https://twitter.com/share?text="+shareTitle);
        $(".share-facebook").attr("href", "https://www.facebook.com/sharer/sharer.php?u="+document.location);
        $(".line-it-button").attr("data-url", ""+document.location);
    }

});


pribizupApp = angular.module("privileges-bizup", []);
var bizupData = [
	{
        "title": "SME & LINE BUSINESS FOR BIZ UP",
        "thumb": "smelineThumb.jpg",
        "link": "smeline.html"
    },
    {
        "title": "ส่วนลด 10% สำหรับค่าบริการแม่บ้านออนไลน์ทำความสะอาดบ้าน",
        "thumb": "beneatThumb.jpg",
        "link": "beneat.html"
    }
];
<!-- เพิ่มหน้ารวม -->
var priApp = angular.module("privileges", []);
var mockData = [
	{
        "title": "รับฟรี! เครื่องดื่มร้อน-เย็นที่ร่วมรายการ 1 แก้ว",
        "thumb": "punthai.jpg",
        "link": "punthaicoffee.html"  
    },{
        "title": "รับส่วนลด 30% บริการให้เช่าตู้นิรภัยทุกขนาด(M, L, XL) พร้อมฟรีค่าประกันรายปี",
        "thumb": "mountainsafe.jpg",
        "link": "mountainsafe.html"
    },{
        "title": "รับส่วนลด 300 บาท  เมื่อซื้อ “กล่องสำเร็จรูป”  ครบ 3,000 บาทขึ้นไป",
        "thumb": "nppbox.jpg",
        "link": "nppbox.html"
    },{
        "title": "รับส่วนลด 1,500 บาท  เมื่อสั่งซื้อ “กล่องสั่งผลิต” ครบ 15,000 บาทขึ้นไป",
        "thumb": "nppboxmadetoorder.jpg",
        "link": "nppbyorder.html"
    },{
        "title": "รับส่วนลด 10% (ค่าบริการพื้นที่ ค่าหยิบ ค่าแพ็คสินค้า) คุณขาย เราแพ็คส่ง",
        "thumb": "sokochan.jpg",
        "link": "sokochan.html"
    },{
        "title": "รับส่วนลดเพิ่ม 10 บาท/กล่อง เมื่อส่งพัสดุกับ Nim Express",
        "thumb": "nimexpress.jpg",
        "link": "nimexpress.html"
    },{
        "title": "รับสิทธิ์สัมมนาฟรีในหัวข้อ เข้าสู่ยุค Digital ง่ายๆ แบบมือโปรสำหรับเจ้าของกิจการธุรกิจรายย่อย",
        "thumb": "msig.jpg",
        "link": "msigseminar.html"
    },{
        "title": "รับส่วนลด 1,000 บาท เมื่อสมัคร Pro Package หรือ Unlimited Package แบบรายปี",
        "thumb": "page365.jpg",
        "link": "page365.html"
    },{
        "title": "รับส่วนลด 3,000 บาท เมื่อสมัครหลักสูตร Super Restaurant Manager รุ่น 3 ",
        "thumb": "wnc.jpg",
        "link": "wcn.html"
    },{
        "title": "รับส่วนลดเพิ่ม 2,500 บาท สำหรับการใช้โปรแกรมบัญชี PEAK",
        "thumb": "peak.png",
        "link": "peakaccount.html"
    },{
        "title": "รับสิทธิประโยชน์ด้านประกันกลุ่ม และ เบี้ยประกันราคาพิเศษ จากเมืองไทยประกันชีวิต",
        "thumb": "mtl.jpg",
        "link": "muangthai.html"
    },{
        "title": "รับส่วนลด สั่งซื้อหมอนหนุนยางพารา และ Topper ราคาพิเศษ(ไม่มีขั้นต่ำ)",
        "thumb": "thailatexmanindividual.jpg",
        "link": "thailatexmanindividual.html"
    },{
        "title": "สั่งทำหมอนยางพาราแบรนด์ตนเองราคาส่วนลดพิเศษ แถมฟรี  Label Tag Logo (ขั้นต่ำ 100 ใบ)",
        "thumb": "thailatexmanbusiness.jpg",
        "link": "thailatexmanbusiness.html"
    },{
        "title": "ลดเพิ่ม  10% สำหรับโปรแกรมตรวจสุขภาพ Smart Check up 18 รายการ",
        "thumb": "phyathai2.jpg",
        "link": "phyathai2.html"
    },{
        "title": "รับส่วนลด 50% เมื่อเปิดร้านค้าออนไลน์ U-Commerce กับ TARAD.COM",
        "thumb": "tarad.jpg",
        "link": "tarad.html"
    },{
        "title": "สร้างหน้าเว็บพิเศษฟรีบน www.yellowpages.co.th",
        "thumb": "yellowpages.jpg",
        "link": "yellowpages.html"
    },{
        "title": "รับส่วนลดค่าโฆษณาบน YellowPages มูลค่า 5,000 บาท",
        "thumb": "yellowpages5000.jpg",
        "link": "yellowpagesdiscount.html"
    },{
        "title": "รับสิทธิ์เข้าร่วมสัมมนาฟรีที่ K ONLINESHOP SPACE สูงสุดจำนวน 2 ที่นั่ง (รับจำนวนจำกัด)",
        "thumb": "kos.jpg",
        "link": "kos.html"
    },{
        "title": "รับส่วนลดห้องพักซูพีเรียร์ และแพ็กเกจสัมมนา & ปาร์ตี้ราคาสุดพิเศษ จากโรงแรมลองบีช ชะอำ",
        "thumb": "longbeach.jpg",
        "link": "longbeach.html"
    },{
        "title": "รับส่วนลดห้องพักซูพีเรียร์ และแพ็กเกจปาร์ตี้ราคาสุดพิเศษ จากโรงแรมธารามันตรา ชะอำ รีสอร์ท",
        "thumb": "taramantra.jpg",
        "link": "taramantra.html"
    },{
        "title": "รับส่วนลด ราคาพิเศษ บริการล้างแอร์ติดผนัง",
        "thumb": "fixzy1.jpg",
        "link": "fixzy.html"
    },{
        "title": "รับส่วนลด ราคาพิเศษ บริการแม่บ้านมือโปร",
        "thumb": "fixzy2.jpg",
        "link": "fixzymaid.html"
    },{
        "title": "รับฟรี! Hardware Thermal Printer เมื่อสมัครใช้บริการครั้งแรก มูลค่ารวมกว่า 8,000 บาท",
        "thumb": "chococrm.png",
        "link": "ChocoCRM.html"
    },{
        "title": "รับคูปองส่วนลด 50% เมื่อซื้อสินค้าตั้งแต่ 200 บาทขึ้นไป",
        "thumb": "farmsuk.png",
        "link": "farmsuk.html"
    },{
        "title": "รับส่วนลดคอร์ส เตรียมความพร้อมทักษะบุคลากรด้านดิจิทัลเทคโนโลยี เพียง 3,900 บาท",
        "thumb": "ftpi.jpg",
        "link": "ftpi.html"
    },{
        "title": "รับส่วนลดพิเศษสำหรับการจองตั๋วเครื่องบินภายในประเทศ และต่างประเทศ",
        "thumb": "penguinT1.png",
        "link": "penguinT.html"
    },{
        "title": "ใช้งานฟรี 15 วัน และส่วนลด 10% ระยะเวลาสูงสุด 12 เดือน เฉพาะแพ็กเกจ 12 เดือน",
        "thumb": "zort.jpg",
        "link": "zort.html"
    },{
        "title": "จองและใช้รถทันใจ ผ่านมือถือ‎ รับสิทธิพิเศษใช้รถฟรี 20 ชม. (*ไม่รวมค่าระยะทาง)",
        "thumb": "haupcar.jpg",
        "link": "haupcar.html"
    },{
        "title": "รับฟรี! ส่วนลดการจองขนส่งออนไลน์ 10%  จาก SHIPPOP",
        "thumb": "shippop.jpg",
        "link": "shippop.html"
    },{
        "title": "รับฟรี! ส่วนลดค่ากรีนฟี มูลค่า 200 บาท",
        "thumb": "Golfdigg.jpg",
        "link": "Golfdigg.html"
    },{
        "title": "รับส่วนลด 10% สำหรับการใช้บริการแม่บ้านทำความสะอาดบ้านทั่วไป",
        "thumb": "beneat.jpg",
        "link": "beneat.html"
    },{
        "title": "รับส่วนลด 1,500 บาท เมื่อจองวงดนตรีจาก MYBAND",
        "thumb": "myband.jpg",
        "link": "myBand.html"
    },{
        "title": "รับฟรี! คูปองส่วนลด มูลค่า 10 บาท(เมื่อซื้อสินค้าครบ 40 บาทขึ้นไป)",
        "thumb": "familymart.jpg",
        "link": "familymart.html"
    },
	{
        "title": "รับสิทธิ์เข้าร่วมงาน SME Transformation มูลค่า 5,000 บาท ฟรี!",
        "thumb": "wecosystem.jpg",
        "link": "wecosystem.html"
    },
	

 
  
/*    {
        "title": "ดีต่อใจ! เพื่อคนทำธุรกิจ เปิดเบอร์ใหม่หรือย้ายค่าย พร้อมส่วนลดสุดพิเศษจากพาร์ทเนอร์",
        "thumb": "deetorjai_thumb.jpg",
        "link": "deetorjai.html"
    },
    {
        "title": "คุ้ม 2 ต่อ!!!   สำหรับลูกค้า  OfficeMate ที่เปิดเบอร์ใหม่ หรือย้ายค่ายเบอร์เดิม",
        "thumb": "office-mate_thumb.jpg",
        "link": "officemate.html"
    },
	{
        "title": "คุ้ม 2 ต่อ!!! สำหรับลูกค้า Yellowpages ที่เปิดเบอร์ใหม่ หรือย้ายค่ายเบอร์เดิม",
        "thumb": "yellow-pages_thumb.jpg",
        "link": "yellow_pages.html"
    },*/
	{
        "title": "รายชื่อผู้ที่ได้รับรางวัล จากการตอบแบบสำรวจ (เดือนละ 10 รางวัล)",
        "thumb": "congratulation2.jpg",
        "link": "congratulations.html"
    }
];
// <!-- เพิ่มหน้ารวม smepackage-->
var packApp = angular.module("smepackage", []);
var packData = [

	//{
        //"title": "DIY",
        //"thumb": "",
        //"link": "#"  //remark #DIY
   // },
	{
        "title": "แพ็กเกจหลากหลาย สำหรับคนทำงาน Work Package",
        "thumb": "workpackage.jpg",
        "link": "https://business.ais.co.th/th/workpackage/"  //remark #Work Package
    },
	{
        "title": "POWER Boost อัพสปีด ทั้งดาวน์โหลดและอัปโหลด เร็ว แรง ทั้งวัน",
        "thumb": "fibre.jpg",
        "link": "https://business.ais.co.th/th/workpackage/#worksolutions" //remark #Fibre
    },
	{
        "title": "ON TOP Talk เพิ่มค่าโทรได้ทุกที่ ทุกเวลา ตามการใช้งาน",
        "thumb": "ontop.jpg",
        "link": "https://business.ais.co.th/th/workpackage/#workontop"  //remark #Ontop
    },
	{
        "title": "On Spot ตอบสนองทุกความต้องการ แพ็กเกจเสริมพร้อมใช้ สำหรับลูกค้าองค์กร",
        "thumb": "onspot.jpg",
        "link": "https://business.ais.co.th/on-spot-package.html" //remark #Fibre
    }
];

// <!-- เพิ่มหน้ารวม smesolution-->
var solApp = angular.module("smesolution", []);
var solData = [

	{
        "title": "SME Smart MPBX ตู้สาขาโทรศัพท์แบบไร้สายใช้เบอร์โทรศัพท์มือถือเป็นเบอร์กลางของบริษัท",
        "thumb": "sme_smart_mpbx.png",
        "link": "https://business.ais.co.th/th/sme-package#sme_smart_mpbk"  //remark #SME Smart MPBX
    },
	{
        "title": "SME Smart Messaging ช่องทางโฆษณาสุดประหยัด ส่งข้อความหาลูกค้าได้รวดเร็ว",
        "thumb": "sme_smart_massaging.png",
        "link": "https://business.ais.co.th/th/sme-package#package-sme-smart" //remark #SME Smart Messaging
    }
];

priApp.controller("priCtrl", function($scope) {
    $scope.mockData = mockData;
});

packApp.controller("packCtrl", function($scope) {
    $scope.packData = packData;
});

solApp.controller("solCtrl", function($scope) {
    $scope.solData = solData;
});

var newsApp = angular.module("newsApp", []);
newsApp.controller("newsCtrl", function($scope) {
    $scope.news = news;
});

var typeSelector = angular.module("typeSelector", ['ngSanitize']);
typeSelector.config(function($sceDelegateProvider) {
    $sceDelegateProvider.resourceUrlWhitelist(['self', 'https://www.youtube.com/**']);
});

typeSelector.controller("typeSelectCtrl",["$scope",  function($scope, $sce) {
    $scope.video_load = false;
    $scope.getIframeSrc = function(src) {
        return 'https://www.youtube.com/embed/' + src;
    };
    $scope.solutionType = solutionCategoryData;
    $scope.solutionTypeSme = solutionCategoryDataSme;
    $scope.videos = videosList;
}]);

var solutionsSec = angular.module("solutionsSec", [ 'ngSanitize']);
solutionsSec.controller("solSecCtrl",[ "$scope", function($scope) {
    $scope.solutionData = solutionData;
    $scope.categoryData = categoryTypeData;
    $scope.videos = videosList;
    $scope.news = news;
    $scope.solutionCategoryData = solutionCategoryData;
    $scope.solutionCategoryDataSme = solutionCategoryDataSme;
    $scope.slickConfig1Loaded = true;
    $scope.updateNumber1 = function() {
        $scope.slickConfig1Loaded = false;
        $scope.number1[2] = '123';
        $scope.number1.push(Math.floor((Math.random() * 10) + 100));
        $timeout(function() {
            $scope.slickConfig1Loaded = true;
        }, 5);
    };
    $scope.slickCurrentIndex = 0;
    $scope.slickConfig = {
        slidesToShow: 3,
        slidesToScroll: 3,
        autoplay: true,
        autoplaySpeed: 5000,
        rows: 1,
        arrows: false,
        dots: true,
        responsive: [{
            breakpoint: 576,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
                centerMode: true,
                dots: false
            }
        }]
    };

    $(document).ready(function() {
        //$('.sol-item').parent().matchHeight();
        $('a[data-toggle="tab"]').on('shown.bs.tab', function(event) {
            $('slick').slick('unslick');
            $('slick').slick({
                slidesToShow: 3,
                slidesToScroll: 3,
                autoplay: true,
                autoplaySpeed: 5000,
                rows: 1,
                arrows: false,
                dots: true,
                responsive: [{
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1,
                        centerMode: true
                    }
                }]
            });
            $('.sol-item').matchHeight();
        });
    });
}]);

$('.gototop').click(function() {
    $('html, body').animate({
        scrollTop: ($("html").offset().top)
    }, 1000);
});

var videoListApp = angular.module("vdoListApp", ['ngSanitize']);
videoListApp.controller("vdoListCtrl", [ "$scope", function($scope) {
    $scope.videos = videosList;
}]);

$( "#package" ).hover(
  function() {
    $('.package-list-wrapper').addClass('shown');
  }, function() {
    $('.package-list-wrapper').removeClass('shown');
    $('.package-thumb').removeClass('hover');
  }
);


$( "#package-thumb-1" ).hover(function() {
	$('.arrow').removeClass('to2');
	$('.arrow').removeClass('to3');
	$('.arrow').removeClass('to4');
	$('.arrow').removeClass('to5');
	$('.package-thumb').removeClass('hover');
  	$('#package-thumb-1').addClass('hover');
	$('.package-list-wrapper').css('display','none');
  	$('#package-list-1').css('display','block');
  	$('.arrow').addClass('to1');
});
$( "#package-thumb-2" ).hover(function() {
	$('.arrow').removeClass('to1');
	$('.arrow').removeClass('to3');
	$('.arrow').removeClass('to4');
	$('.arrow').removeClass('to5');
	$('.package-thumb').removeClass('hover');
  	$('#package-thumb-2').addClass('hover');
	$('.package-list-wrapper').css('display','none');
  	$('#package-list-2').css('display','block');
  	$('.arrow').addClass('to2');
});
$( "#package-thumb-3" ).hover(function() {
	$('.arrow').removeClass('to1');
	$('.arrow').removeClass('to2');
	$('.arrow').removeClass('to4');
	$('.arrow').removeClass('to5');
	$('.package-thumb').removeClass('hover');
  	$('#package-thumb-3').addClass('hover');
	$('.package-list-wrapper').css('display','none');
  	$('#package-list-3').css('display','block');
  	$('.arrow').addClass('to3');
});
$( "#package-thumb-4" ).hover(function() {
	$('.arrow').removeClass('to1');
	$('.arrow').removeClass('to2');
	$('.arrow').removeClass('to3');
	$('.arrow').removeClass('to5');
	$('.package-thumb').removeClass('hover');
  	$('#package-thumb-4').addClass('hover');
	$('.package-list-wrapper').css('display','none');
  	$('#package-list-4').css('display','block');
  	$('.arrow').addClass('to4');
});
$( "#package-thumb-5" ).hover(function() {
	$('.arrow').removeClass('to1');
	$('.arrow').removeClass('to2');
	$('.arrow').removeClass('to3');
	$('.arrow').removeClass('to4');
	$('.package-thumb').removeClass('hover');
  	$('#package-thumb-5').addClass('hover');
	$('.package-list-wrapper').css('display','none');
  	$('#package-list-5').css('display','block');
  	$('.arrow').addClass('to5');
});
