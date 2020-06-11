$(window).on('load', function(){
    $('.relate-item').matchHeight();
});

var solutionApp = angular.module("solution-detail", ['ngSanitize','slickCarousel', 'ngRoute']);

solutionApp.config(function($sceDelegateProvider) {
  $sceDelegateProvider.resourceUrlWhitelist([
    'self',
    'https://www.youtube.com/**'
  ]);
});

solutionApp.controller("solutionCtrl", [ "$scope" ,"$location", "$timeout", "$sce", function ($scope,$location, $timeout,$sce) {

    $scope.getIframeSrc = function(src) {
      return 'https://www.youtube.com/embed/' + src;
    };

    //====================================
    // Slick 1
    //====================================
    $scope.slickConfig1Loaded = true;
    $scope.updateNumber1 = function () {
      $scope.slickConfig1Loaded = false;
      $scope.number1[2] = '123';
      $scope.number1.push(Math.floor((Math.random() * 10) + 100));
      $timeout(function () {
        $scope.slickConfig1Loaded = true;
      }, 5);
    };
    $scope.slickCurrentIndex = 0;
    $scope.slickConfig = {
      slidesToShow: 3,
        slidesToScroll: 3,
        rows: 1,
        autoplay: true,
        autoplaySpeed: 5000,
        arrows: true,
        dots: true,
        nextArrow: '<button type="button" class="slick-next n"><img src="../assets/revamp/img/solution-detail/arrow-right.png"></button>',
        prevArrow: '<button type="button" class="slick-prev p"><img src="../assets/revamp/img/solution-detail/arrow-left.png"></button>',
        responsive: [{
            breakpoint: 576,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    };
    //====================================
    // Slick 2
    //====================================
    $scope.slickConfig2Loaded = true;
    $scope.updateNumber2 = function () {
      $scope.slickConfig2Loaded = false;
      $scope.number2[2] = '123';
      $scope.number2.push(Math.floor((Math.random() * 10) + 100));
      $timeout(function () {
        $scope.slickConfig1Loaded = true;
      }, 5);
    };
    $scope.slickCurrentIndex = 0;
    $scope.slickConfig2 = {
        slidesToShow: 3,
        slidesToScroll: 3,
        rows: 1,
        arrows: false,
        dots: true,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [{
            breakpoint: 576,
            settings: {
                dots: false,
                slidesToShow: 1,
                slidesToScroll: 1,
                centerMode: true
            }
        }]
    };

    //====================================
    // Slick 3
    //====================================
    $scope.slickConfig3Loaded = true;
    $scope.updateNumber1 = function () {
        $scope.slickConfig3Loaded = false;
        $scope.number1[2] = '123';
        $scope.number1.push(Math.floor((Math.random() * 10) + 100));
        $timeout(function () {
            $scope.slickConfig3Loaded = true;
        }, 5);
    };
    $scope.slickCurrentIndex = 0;
    $scope.slickConfig = {
        slidesToShow: 1,
        slidesToScroll: 1,
        adaptiveHeight: true,
        autoplay: true,
        autoplaySpeed: 5000,
        rows: 1,
        arrows: true,
        dots: true,
        nextArrow: '<button type="button" class="slick-next"><img src="../assets/revamp/img/solution-detail/arrow-right.png"></button>',
        prevArrow: '<button type="button" class="slick-prev"><img src="../assets/revamp/img/solution-detail/arrow-left.png"></button>',
        responsive: [{
            breakpoint: 576,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    };


    $scope.solutionData = solutionData;
    $scope.videoList = videosList;
    $scope.categoryData = categoryTypeData;

    function parseURLParams(url) {
        var queryStart = url.indexOf("?") + 1,
            queryEnd   = url.indexOf("#") + 1 || url.length + 1,
            query = url.slice(queryStart, queryEnd - 1),
            pairs = query.replace(/\+/g, " ").split("&"),
            parms = {}, i, n, v, nv;

        if (query === url || query === "") return;

        for (i = 0; i < pairs.length; i++) {
            nv = pairs[i].split("=", 2);
            n = decodeURIComponent(nv[0]);
            v = decodeURIComponent(nv[1]);
            if(nv[0] == 'business'){
                if (nv[1] == 'sme'){
                    $scope.business_type = 'ธุรกิจขนาดเล็ก';
                    $scope.business_link = 'sme';
                }
                else if (nv[1] == 'enterprise'){
                    $scope.business_type = 'ธุรกิจขนาดใหญ่';
                    $scope.business_link = 'enterprise';
                }
            }
            if(nv[0] == 'type'){
                if (nv[1] == 'finance'){
                    $scope.solution_type = 'การเงิน';
                    $scope.solution_link = 'finance';
                }
                else if (nv[1] == 'gov'){
                    $scope.solution_type = 'ราชการ';
                    $scope.solution_link = 'government';
                }
                else if (nv[1] == 'transportation'){
                    $scope.solution_type = 'คมนาคม ขนส่ง';
                    $scope.solution_link = 'transportation';
                }
                else if (nv[1] == 'commuit'){
                    $scope.solution_type = 'สื่อสาร และไอที';
                    $scope.solution_link = 'telecommunication-and-it';
                }
                else if (nv[1] == 'hotel'){
                    $scope.solution_type = 'โรงแรม และสันทนาการ';
                    $scope.solution_link = 'hotel-and-entertainment';
                }
                else if (nv[1] == 'health'){
                    $scope.solution_type = 'การบริการด้านสุขภาพ และงานสังคมสงเคราะห์';
                    $scope.solution_link = 'health-services-and-social-work';
                }
                else if (nv[1] == 'buservice'){
                    $scope.solution_type = 'บริการทางธุรกิจ';
                    $scope.solution_link = 'business-services';
                }
                else if (nv[1] == 'construction'){
                    $scope.solution_type = 'การก่อสร้าง และอสังหาริมทรัพย์';
                    $scope.solution_link = 'construction-and-real-estate';
                }
                else if (nv[1] == 'auto'){
                    $scope.solution_type = 'อุตสาหกรรมยานยนต์';
                    $scope.solution_link = 'automotive';
                }
                else if (nv[1] == 'energy'){
                    $scope.solution_type = 'พลังงาน และสาธารณูปโภค';
                    $scope.solution_link = 'energy-and-utilities';
                }
                else if (nv[1] == 'sysintegrate'){
                    $scope.solution_type = 'ซิสเต็มอินติเกรเตอร์';
                    $scope.solution_link = 'system-integrator';
                }
                else if (nv[1] == 'retail'){
                    $scope.solution_type = 'การขายส่ง ขายปลีก';
                    $scope.solution_link = 'wholesale-retail';
                }
                else if (nv[1] == 'manufacturing'){
                    $scope.solution_type = 'การผลิต';
                    $scope.solution_link = 'manufacturing';
                }
                else if (nv[1] == 'education'){
                    $scope.solution_type = 'การศึกษา';
                    $scope.solution_link = 'education';
                }
            }
            if(nv[0] == 'category'){
                if (nv[1] == 'mobility'){
                    $scope.category_type = 'mobility';
                    $scope.category_type_thai = 'โซลูชันด้าน Mobility';
                }
                else if (nv[1] == 'communication'){
                    $scope.category_type = 'communication';
                    $scope.category_type_thai = 'โซลูชันทางการสื่อสาร';
                }
                else if (nv[1] == 'transportation'){
                    $scope.category_type = 'transportation';
                    $scope.category_type_thai = 'โซลูชันด้านการขนส่ง';
                }
                else if (nv[1] == 'productivity'){
                    $scope.category_type = 'productivity';
                    $scope.category_type_thai = 'โซลูชันเพื่อประสิทธิภาพ';
                }
                else if (nv[1] == 'marketing'){
                    $scope.category_type = 'marketing';
                    $scope.category_type_thai = 'โซลูชันทางการตลาด';
                }
                else if (nv[1] == 'network'){
                    $scope.category_type = 'network';
                    $scope.category_type_thai = 'โซลูชันด้านเน็ตเวิร์ค';
                }
                else if (nv[1] == 'iot'){
                    $scope.category_type = 'iot';
                    $scope.category_type_thai = 'โซลูชันด้าน IoT';
                }
                else if (nv[1] == 'cloud'){
                    $scope.category_type = 'cloud';
                    $scope.category_type_thai = 'โซลูชันด้าน Cloud';
                }
                else if (nv[1] == 'security'){
                    $scope.category_type = 'security';
                    $scope.category_type_thai = 'โซลูชันด้านความปลอดภัย';
                }
                else if (nv[1] == 'vertical'){
                    $scope.category_type = 'vertical';
                    $scope.category_type_thai = 'โซลูชันเฉพาะธุรกิจ';
                }
                else if (nv[1] == 'recommended'){
                    $scope.category_type = 'recommended';
                    $scope.category_type_thai = 'โซลูชันแนะนำ';
                }
            }

            if (!parms.hasOwnProperty(n)) parms[n] = [];
            parms[n].push(nv.length === 2 ? v : null);
        }
        return parms;
    }

    $scope.test = parseURLParams(location.href);
}]);
