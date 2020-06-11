;(function($){var _previousResizeWidth=-1,_updateTimeout=-1;var _parse=function(value){return parseFloat(value)||0;};var _rows=function(elements){var tolerance=1,$elements=$(elements),lastTop=null,rows=[];$elements.each(function(){var $that=$(this),top=$that.offset().top-_parse($that.css('margin-top')),lastRow=rows.length>0?rows[rows.length-1]:null;if(lastRow===null){rows.push($that);}else{if(Math.floor(Math.abs(lastTop-top))<=tolerance){rows[rows.length-1]=lastRow.add($that);}else{rows.push($that);}}lastTop=top;});return rows;};var _parseOptions=function(options){var opts={byRow:true,property:'height',target:null,remove:false};if(typeof options==='object'){return $.extend(opts,options);}if(typeof options==='boolean'){opts.byRow=options;}else if(options==='remove'){opts.remove=true;}return opts;};var matchHeight=$.fn.matchHeight=function(options){var opts=_parseOptions(options);if(opts.remove){var that=this;this.css(opts.property,'');$.each(matchHeight._groups,function(key,group){group.elements=group.elements.not(that);});return this;}if(this.length<=1&&!opts.target){return this;}matchHeight._groups.push({elements:this,options:opts});matchHeight._apply(this,opts);return this;};matchHeight.version='master';matchHeight._groups=[];matchHeight._throttle=80;matchHeight._maintainScroll=false;matchHeight._beforeUpdate=null;matchHeight._afterUpdate=null;matchHeight._rows=_rows;matchHeight._parse=_parse;matchHeight._parseOptions=_parseOptions;matchHeight._apply=function(elements,options){var opts=_parseOptions(options),$elements=$(elements),rows=[$elements];var scrollTop=$(window).scrollTop(),htmlHeight=$('html').outerHeight(true);var $hiddenParents=$elements.parents().filter(':hidden');$hiddenParents.each(function(){var $that=$(this);$that.data('style-cache',$that.attr('style'));});$hiddenParents.css('display','block');if(opts.byRow&&!opts.target){$elements.each(function(){var $that=$(this),display=$that.css('display');if(display!=='inline-block'&&display!=='inline-flex'){display='block';}$that.data('style-cache',$that.attr('style'));$that.css({'display':display,'padding-top':'0','padding-bottom':'0','margin-top':'0','margin-bottom':'0','border-top-width':'0','border-bottom-width':'0','height':'100px','overflow':'hidden'});});rows=_rows($elements);$elements.each(function(){var $that=$(this);$that.attr('style',$that.data('style-cache')||'');});}$.each(rows,function(key,row){var $row=$(row),targetHeight=0;if(!opts.target){if(opts.byRow&&$row.length<=1){$row.css(opts.property,'');return;}$row.each(function(){var $that=$(this),display=$that.css('display');if(display!=='inline-block'&&display!=='inline-flex'){display='block';}var css={'display':display};css[opts.property]='';$that.css(css);if($that.outerHeight(false)>targetHeight){targetHeight=$that.outerHeight(false);}$that.css('display','');});}else{targetHeight=opts.target.outerHeight(false);}$row.each(function(){var $that=$(this),verticalPadding=0;if(opts.target&&$that.is(opts.target)){return;}if($that.css('box-sizing')!=='border-box'){verticalPadding+=_parse($that.css('border-top-width'))+_parse($that.css('border-bottom-width'));verticalPadding+=_parse($that.css('padding-top'))+_parse($that.css('padding-bottom'));}$that.css(opts.property,(targetHeight-verticalPadding)+'px');});});$hiddenParents.each(function(){var $that=$(this);$that.attr('style',$that.data('style-cache')||null);});if(matchHeight._maintainScroll){$(window).scrollTop((scrollTop/htmlHeight)*$('html').outerHeight(true));}return this;};matchHeight._applyDataApi=function(){var groups={};$('[data-match-height], [data-mh]').each(function(){var $this=$(this),groupId=$this.attr('data-mh')||$this.attr('data-match-height');if(groupId in groups){groups[groupId]=groups[groupId].add($this);}else{groups[groupId]=$this;}});$.each(groups,function(){this.matchHeight(true);});};var _update=function(event){if(matchHeight._beforeUpdate){matchHeight._beforeUpdate(event,matchHeight._groups);}$.each(matchHeight._groups,function(){matchHeight._apply(this.elements,this.options);});if(matchHeight._afterUpdate){matchHeight._afterUpdate(event,matchHeight._groups);}};matchHeight._update=function(throttle,event){if(event&&event.type==='resize'){var windowWidth=$(window).width();if(windowWidth===_previousResizeWidth){return;}_previousResizeWidth=windowWidth;}if(!throttle){_update(event);}else if(_updateTimeout===-1){_updateTimeout=setTimeout(function(){_update(event);_updateTimeout=-1;},matchHeight._throttle);}};$(matchHeight._applyDataApi);$(window).bind('load',function(event){matchHeight._update(false,event);});$(window).bind('resize orientationchange',function(event){matchHeight._update(true,event);});})(jQuery);
//end jquery.matchHeight.js

//adobe
function parseVar(url,lang,action,data){
    var info ="";
    if(action ==="menuPopUp"){
      info=url+lang+data;
      //console.log(info+"##menupop");
    }else if(action ==="chLang"){
      info=url+lang+data;
      //console.log(info+"##chLang");
    }else if(action ==="social"){
      info=url+lang+data;
      //console.log(info+"##social");
    }else if(action ==="footer"){
      info=url+lang+data;
      //console.log(info+"##footer");
    }else if (action ==="store"){
      info =url+lang+data;
      //console.log(info+"##store");
    }

    digitalData.customLinkName=info;
    _satellite.track("custom-link-click");
}
//adobe
var shareURL, shareURLSTAT, shareURLSTATtw, shareTitle, shareDescription, shareIMG;
function share_fb(){
  var ShareURL = document.URL;
  var share_url = "https://www.facebook.com/sharer/sharer.php?u="+encodeURIComponent(ShareURL);
  newwindowfb = window.open(share_url,'sharer','toolbar=0,status=0,width=650,height=400,resizable=yes');
  if (window.focus) { newwindowfb.focus(); }
    parseVar(ul,pageLang,"menuPopUp","header_menu:share_icon:share:facebook");
  return false;
}
function share_tw(){
  var tw_url = document.URL;
  var TitleDesc;
  var description;
  var metas = document.getElementsByTagName('meta');
  for (var x=0,y=metas.length; x<y; x++) {
      if(metas[x].getAttribute('property'))   {
        if (metas[x].getAttribute('property').toLowerCase() ==="tw:description") {
          TitleDesc = metas[x];
        }
      }
  }
  //var tw_text = TitleDesc.content+" "+description.content;
  var tw_text = TitleDesc.content;
  newwindowtw = window.open('https://twitter.com/share?url=' + encodeURIComponent(tw_url) + '&text=' + encodeURIComponent(tw_text), 'sharer', 'toolbar=0, status=0, width=650, height=400');
  parseVar(ul,pageLang,"menuPopUp","header_menu:share_icon:share:twitter");
  //socialDetect(ul,pageLang,"share_icon:share:twitter");
  if (window.focus) { newwindowtw.focus(); }
  return false;
}
function share_go(){
  var shareURL =  document.URL;
  newwindowgo= window.open('https://plus.google.com/share?url=' + encodeURIComponent(shareURL) , 'sharer', 'toolbar=0, status=0, width=600, height=600');
  parseVar(ul,pageLang,"menuPopUp","header_menu:share_icon:share:googleplus");
  if (window.focus) { newwindowgo.focus(); }
  return false;
}
function share_line(){
  var ShareURL = document.URL;
  newwindowline = window.open("https://line.me/R/msg/text/?"+encodeURIComponent(ShareURL));
}
function share_lineit(){
  var tw_url  =document.URL;
  var TitleDesc;
  var description;
  var metas = document.getElementsByTagName('meta');
  for (var x=0,y=metas.length; x<y; x++) {
    if(metas[x].getAttribute('property'))   {
      if (metas[x].getAttribute('property').toLowerCase() ==="og:description") {
        description = metas[x];
      }else if (metas[x].getAttribute('property').toLowerCase() ==="og:title") {
        TitleDesc = metas[x];
      }
    }
  }
  var tw_text = TitleDesc.content;
  newwindowtw = window.open('https://line.me/R/msg/' + encodeURIComponent(tw_text) + encodeURIComponent(tw_url));
  parseVar(ul,pageLang,"menuPopUp","header_menu:share_icon:share:line");
  if (window.focus) { newwindowgo.focus(); }
  return false;
}

function checkemail_subscribe(email_subscribe) {
  //var email_subscribe = $("#e_news_subscribe").val();
  var email_pattern = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z]{2,4})+$/;
  if (email_pattern.test(email_subscribe)) {return true;} else {return false;}
}
function isIE( version, comparison ){
  var $div = $('<div style="display:none;"/>').appendTo($('body'));
  $div.html('<!--[if '+(comparison||'')+' IE '+(version||'')+']><a>&nbsp;</a><![endif]-->');
  var ieTest = $div.find('a').length;
  $div.remove();
  return ieTest;
}
function ValidUrl(str) {
  var pattern = new RegExp('^(https?:\\/\\/)?'+ // protocol
  '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|'+ // domain name
  '((\\d{1,3}\\.){3}\\d{1,3}))'+ // OR ip (v4) address
  '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*'+ // port and path
  '(\\?[;&a-z\\d%_.~+=-]*)?'+ // query string
  '(\\#[-a-z\\d_]*)?$','i'); // fragment locator
  if(!pattern.test(str)) {
    return false;
  } else {
    return true;
  }
}
function changeLang(){
// var filename = window.location.href.substr(window.location.href.lastIndexOf("/")+1);
  var url = document.URL;
  var value = url.substring(url.lastIndexOf('/') + 1);
  if( ValidUrl(value) || value.trim() ==='')
  {
      document.location = "../en/"+part_sme+value;
  }else{
      document.location = "../en/"+part_sme+value;
  }
  //parseVar(ul,pageLang,"chLang","header_menu:changetoeng");
}
function changeLangEn(){
//var filename = window.location.href.substr(window.location.href.lastIndexOf("/")+1);
  var url = document.URL;
  var value = url.substring(url.lastIndexOf('/') + 1);
  if( ValidUrl(value) || value.trim() ==='')
  {
      document.location = "../../"+part_sme+value;
  }else{
      document.location = "../../"+part_sme+value;
  }
  //parseVar(ul,pageLang,"chLang","header_menu:changetotha");
}

/* ----------------------------------
// Footer
---------------------------*/

function showSub(Tar){
  try{
    for (i = 1; i <= 5; i++) {

      var NowID =  'site_sub'+i;
      if(NowID !=  Tar){ document.getElementById(NowID).className = 'link_set';     }
    }
  }catch(e){}
  try{
    for (i = 1; i <= 4; i++) {

      var NowID =  'link_sub'+i;
      if(NowID !=  Tar){ document.getElementById(NowID).className = 'link_set'; }
    }
  }catch(e){}

  if( $( "#"+Tar ).hasClass( "link_set_current" )  === true ){
    document.getElementById(Tar).className = 'link_set';
  }else{
    document.getElementById(Tar).className = 'link_set_current';
  }
}

function footerscript() {

  $('#footer_sub_arrow').click(function(){
    if( $('.footer_shotcut').is(':visible')  ===false &&  $('.footer_sitemap').is(':visible')  ===false){
      $('.footer_sitemap').hide();
      $('.footer_shotcut').show();
      $('#footer_sub_menu1').addClass('active')
      $('#footer_sub_menu2').removeClass('active')
    }else{
      $('.footer_sitemap').hide();
      $('.footer_shotcut').hide();
    }
  });


$('.contact-newletter-std').click(function(){
	$(".contact-newletter").addClass("moveleft");
});

$('.contact-social-std').click(function(){
	$(".contact-social").addClass("moveleft");
});

$('.contact-newletter-close').click(function(){
	$(".contact-newletter").removeClass("moveleft");
});

$('.contact-social-close').click(function(){
	$(".contact-social").removeClass("moveleft");
});

  $('#footer_sub_menu0').click(function(){
    if( $('.footer_callcenter').is(':visible')  ===false ){
      $('.footer_sitemap').hide();
      $('.footer_shotcut').hide();
	  $('#ais_footer-contact').show();
	  $('.contact-social-mobile').hide();
      $('#footer_sub_menu0').addClass('active');
      $('#footer_sub_menu2').removeClass('active');
	  $('#footer_sub_menu1').removeClass('active');
	  $('#footer_sub_menu3').removeClass('active');
      //parseVar(ul,pageLang,"footer","footer:shortcut");

    }
  });

  $('#footer_sub_menu1').click(function(){
    if( $('.footer_shotcut').is(':visible')  ===false ){
      $('.footer_sitemap').hide();
      $('.footer_shotcut').show();
	  $('#ais_footer-contact').hide();
	  $('.contact-social-mobile').hide();
      $('#footer_sub_menu1').addClass('active');
      $('#footer_sub_menu2').removeClass('active');
	  $('#footer_sub_menu0').removeClass('active');
	  $('#footer_sub_menu3').removeClass('active');
      //parseVar(ul,pageLang,"footer","footer:shortcut");

    }
  });
  $('#footer_sub_menu2').click(function(){
    if( $('.footer_sitemap').is(':visible')  ===false ){
      $('.footer_sitemap').show();
      $('.footer_shotcut').hide();
	  $('#ais_footer-contact').hide();
	   $('.contact-social-mobile').hide();
      $('#footer_sub_menu1').removeClass('active');
	  $('#footer_sub_menu0').removeClass('active');
	  $('#footer_sub_menu3').removeClass('active');
      $('#footer_sub_menu2').addClass('active');
      //parseVar(ul,pageLang,"footer","footer:sitemap");
    }
  });

  $('#footer_sub_menu3').click(function(){
	  //alert("footer_sub_menu3");
    if( $('.contact-social-mobile').is(':visible')  ===false ){
	  $('.footer_sitemap').hide();
      $('.footer_shotcut').hide();
	  $('#ais_footer-contact').hide();
	   $('.contact-social-mobile').show();

      $('#footer_sub_menu1').removeClass('active');
	  $('#footer_sub_menu0').removeClass('active');
	  $('#footer_sub_menu2').removeClass('active');
      $('#footer_sub_menu3').addClass('active');
      //parseVar(ul,pageLang,"footer","footer:sitemap");
    }
  });
  $('.js-btn-toggle').on('click', function(){
    showSub(Tar);
  });

  $(window).on('load', function() {
      $('.js-overlay').on('click', function(){

          $('#nav-icon1').removeClass("open");
          $('.primary-nav').removeClass("nav-slidedown");
          $('.js-overlay').removeClass("is-visible");

          if( $( ".primary-nav" ).height() > 0){
              if ($(window).width() > 768) {
                  $(".js-custom-header").show();
                  $("#mainnav").show();
              }else{}
          }else{
              $(".js-custom-header").hide();
              $("#mainnav").hide();
          }

      });
  })

}

$('.menu-box').matchHeight();

footerscript();

function slideUlLi(id, subslide) {

	$('#' + subslide).slideToggle();
	var ul = $("#" + id).find('.expand_stat');

	if (ul.text() ==="+") {
		$(ul).text("-");
		$("#" + id).addClass('active');
	} else {
		$(ul).text("+");
		$("#" + id).removeClass('active');
	}

}
function one2call_logo(){}

function myFunction(x) {

	var mainnav = $('#mainnav');
	var primaryNav = $('.primary-nav');

    if (primaryNav.hasClass("nav-slidedown") === true) {
        $('.js-overlay').removeClass("is-visible");
    } else {
        $('.js-overlay').addClass("is-visible");
    }

    if (primaryNav.height() > 0) {
        if ($(window).width() > 768) {
            $(".js-custom-header").show();
            //mainnav.show();
        } else {
        }
    } else {
        $(".js-custom-header").hide();
        //mainnav.hide();
    }

    $('.pp_onlinestore').removeClass('expand'); //romove this class when open the menu

    x.classList.toggle("open");
    primaryNav.toggleClass("nav-slidedown");


    $(".primary-link-lv1").find('.expand_stat').text("+");
    $(".ais_personal-sub").find('.expand_stat').text("+");
    $(".ul-sub-lv1-mobile").hide();
    $(".ais_personal-sub").removeClass('active');
    $(".primary-link-lv1").removeClass('active');

    $(".mini-menu .js-custom-header .secondary-link-icon").toggle();
}

function menuOneLv(mlv1) {
	$('#ais_topbar #' + mlv1).hover(function() {
		$('.ais_topbar-primary-menu').removeClass('active');
		$('.ais_personal-sub').removeClass('active');
		$(".ul-sub-lv1").hide();
		$(this).addClass('active');
		$("#ul-main-menu").addClass('navinvester');
		$('.menu-box-wrap').hide();
	});

	$('#ais_topbar #' + mlv1).mouseleave(function() {
		$('#' + mlv1).removeClass('active');
		$("#ul-main-menu").removeClass('navinvester');
			startNav();
	});
}

function menuAboutLv() {
	//primary
	$('#ais_topbar #ais_topbar-primary4').hover(function() { //menu lv1
			$('#ais_topbar-primary1').removeClass('active');
			$('#ul-personal-sub').hide();
		$('.ais_topbar-primary-menu').removeClass('active'); //menu lv1 clear
		$('.ais_personal-sub').removeClass('active'); //menu lv2 clear
		$('.ais_customer-sub').removeClass('active');
		$(".ul-sub-lv1").hide();
		$('#ul-personal-sub').hide();
		$('.menu-box-wrap').hide();
		$('#ul-about-sub').show();
		$(this).addClass('active');
	});

	$('#ais_topbar #ais_topbar-primary4').mouseleave(function() {
		$('#ul-about-sub').hide();
		$('#ais_topbar-primary4').removeClass('active');
		startNav();
	});

	$('#ais_topbar #ul-about-sub').hover(function() {
		$('#ul-personal-sub').hide();
		$('#ais_topbar-primary1').removeClass('active');
		$('#ul-about-sub').show();
		$('#ais_topbar-primary4').addClass('active');
	});
	$('#ais_topbar #ul-about-sub').mouseleave(function() {
		$('.ais_topbar-primary-menu').removeClass('active');
		$("#ul-about-sub").hide();
		startNav();

	});
}

function menuTwoLv(mlv2) {
			//primary
			$('#ais_topbar #' + mlv2).hover(function() { //menu lv2
				$('.ais_personal-sub').removeClass('active');
				$('.ais_customer-sub').removeClass('active');
				$('.ais_investor-sub').removeClass('active');
				$(this).addClass('active');
				$('.menu-box-wrap').hide();
			});

			$("#ul-main-menu").mouseleave(function() {
						$('.ais_topbar-primary-menu').removeClass('active'); //menu lv1 clear
						$('.ais_personal-sub').removeClass('active'); //menu lv2 clear
						$('.ais_customer-sub').removeClass('active');
						$('.ais_investor-sub').removeClass('active');
						$(".ul-sub-lv1").hide();
						startNav();
			});
		}
function menuCunsumerLv(mlv1, barlv2, mlv2, mlv3) {
			//primary
			$('#ais_topbar #' + mlv1).hover(function() { //menu lv1
				$('.menu-box-wrap').hide();
					$('.ais_topbar-primary-menu').removeClass('active');
					$('.ul-sub-lv1').hide();
					$('#ais_topbar-primary1').removeClass('active');
					$('#ul-personal-sub').hide();
						$(this).addClass('active');
						$("#" + barlv2).show(); //bar lv2

				});
			$('#ais_topbar #' + mlv1).mouseleave(function() { //menu lv3
					$('.menu-box-wrap').hide();//
					$('.ais_topbar-primary-menu').removeClass('active');
					$(this).addClass('active');
					$('.ul-sub-lv1').hide();
					$("#" + barlv2).show(); //bar lv2
				});
			$('#' + mlv2).hover(function() { //menu lv2
				$('.ais_personal-sub').removeClass('active');
				$('.ais_customer-sub').removeClass('active');
				$('.menu-box-wrap').hide();
				$(this).addClass('active');
				$('#' + mlv1).addClass('active');//bar lv2----------
				$("#" + barlv2).show(); //bar lv2----------
				$("#" + mlv3).show(); //menu lv3
			});

			$('#' + mlv3).hover(function() { //menu lv3
				$('#ais_topbar #' + mlv2).addClass('active');
			});
			$('#ais_topbar #' + mlv3).mouseleave(function() { //menu lv3
						$('.ais_topbar-primary-menu').removeClass('active'); //menu lv1 clear
						$('.ais_personal-sub').removeClass('active'); //menu lv2 clear
						$('.ais_customer-sub').removeClass('active');
						$('#' + barlv2).hide();
						$('#' + mlv3).hide();
						startNav();
			});

		}
function menuBusinessLv(mlv1, barlv2, mlv2, mlv3) {
			//primary
			$('#ais_topbar #' + mlv1).hover(function() { //menu lv1
					$('.menu-box-wrap').hide();
					$('.ais_topbar-primary-menu').removeClass('active');
					$('.ul-sub-lv1').hide();
					$('#ais_topbar-primary1').removeClass('active');
					$('#ul-personal-sub').hide();
						$(this).addClass('active');
						$("#" + barlv2).show(); //bar lv2
			});

			$('#ais_topbar #' + mlv2).hover(function() { //menu lv2
					$('.ais_personal-sub').removeClass('active');
					$('.ais_customer-sub').removeClass('active');
					$('#' + mlv1).addClass('active');//bar lv2----------
					$("#" + barlv2).show(); //bar lv2----------
					$(this).addClass('active');
					$('.menu-box-wrap').hide();

					$('#ais_topbar-primary1').removeClass('active');
					$('#ul-personal-sub').hide();
					$("#" + mlv3).show(); //menu lv3
				});

			$('#ais_topbar #' + mlv3).hover(function() { //menu lv3
							//console.log("mlv3hover");
							$('.ais_personal-sub').removeClass('active');
							$('.ais_customer-sub').removeClass('active');
							$('#ul-personal-sub').hide();
							$('#ais_topbar-primary1').removeClass('active');
							$('#' + mlv1).addClass('active');//bar lv2----------
							$("#" + barlv2).show(); //bar lv2----------
							$('#ais_topbar #' + mlv2).addClass('active');
							$('.menu-box-wrap').hide();
							$("#" + mlv3).show(); //menu lv3

						});

						$('#ais_topbar #' + mlv3).mouseleave(function() { //menu lv3
							//console.log("mlv3mouseleave");
							$('.ais_topbar-primary-menu').removeClass('active'); //menu lv1 clear
							$('.ais_personal-sub').removeClass('active'); //menu lv2 clear
							$('.ais_customer-sub').removeClass('active');
							$('#' + barlv2).hide();
							$('#' + mlv3).hide();
							startNav();

						});

		}
function menuInvesterLv(mlv1, barlv2, mlv2, mlv3) {
			//primary

			$('#ais_topbar #' + mlv1).hover(function() { //menu lv1
					$('.menu-box-wrap').hide();
					$('.ais_topbar-primary-menu').removeClass('active');
					$('.ul-sub-lv1').hide();
					$('#ais_topbar-primary3').removeClass('active');
					$('#ul-personal-sub').hide();
						$(this).addClass('active');
						$("#" + barlv2).show(); //bar lv2
			});

			$('#ais_topbar #' + mlv2).hover(function() { //menu lv2
					$('.ais_personal-sub').removeClass('active');
					$('.ais_investor-sub').removeClass('active');
					$('#' + mlv1).addClass('active');//bar lv2----------
					$("#" + barlv2).show(); //bar lv2----------
					$(this).addClass('active');
					$('.menu-box-wrap').hide();

					$('#ais_topbar-primary1').removeClass('active');
					$('#ul-personal-sub').hide();
					$("#" + mlv3).show(); //menu lv3
				});

			$('#ais_topbar #' + mlv3).hover(function() { //menu lv3
							//console.log("mlv3hover");
							$('.ais_personal-sub').removeClass('active');
							$('.ais_investor-sub').removeClass('active');
							$('#ul-personal-sub').hide();
							$('#ais_topbar-primary1').removeClass('active');
							$('#' + mlv1).addClass('active');//bar lv2----------
							$("#" + barlv2).show(); //bar lv2----------
							$('#ais_topbar #' + mlv2).addClass('active');
							$('.menu-box-wrap').hide();
							$("#" + mlv3).show(); //menu lv3

							//console.log(barlv2 + "show");
							//console.log(mlv3 + "show");

						});

						$('#ais_topbar #' + mlv3).mouseleave(function() { //menu lv3
							//console.log("mlv3mouseleave");
							$('.ais_topbar-primary-menu').removeClass('active'); //menu lv1 clear
							$('.ais_personal-sub').removeClass('active'); //menu lv2 clear
							$('.ais_customer-sub').removeClass('active');
							$('#' + barlv2).hide();
							$('#' + mlv3).hide();
							startNav();

						});

		}
		function startNav(){
			$('#ais_topbar-primary1').addClass('active');

			if ($(window).width() <= 1000) {
				$("#ul-personal-sub").hide(); //bar lv2----------
			}else{
				$("#ul-personal-sub").show(); //bar lv2----------	}
			}
		}
		$( window ).resize(function() {
				if ($(window).width() > 768) {
					$(".js-custom-header").show();
				}else{
					$(".js-custom-header").hide();
				}
		});
		function expandSearch(){
          $('.js-search-input').on('focus', function(event){
            $(this).parents('#topbar_search').addClass('is-focus');
            //closeNav();
            //closepopmenu();
          }).on('focusout', function() {
            setTimeout(function(){
              $('.js-search-input').parents('#topbar_search').removeClass('is-focus');
            }, 100);
          });
      }
	  expandSearch();

function aistopbarScript(){
	  	menuCunsumerLv('ais_topbar-primary1', 'ul-personal-sub', 'ais_personal-sub-fibre', 'ul-personal-sub-fibre');
		menuCunsumerLv('ais_topbar-primary1', 'ul-personal-sub', 'ais_personal-sub-device', 'ul-personal-sub-device');
		menuCunsumerLv('ais_topbar-primary1', 'ul-personal-sub', 'ais_personal-sub-postpaid', 'ul-personal-sub-postpaid');
		menuCunsumerLv('ais_topbar-primary1', 'ul-personal-sub', 'ais_personal-sub-prepaid', 'ul-personal-sub-prepaid');
		menuCunsumerLv('ais_topbar-primary1', 'ul-personal-sub', 'ais_personal-sub-network', 'ul-personal-sub-network');
		menuCunsumerLv('ais_topbar-primary1', 'ul-personal-sub', 'ais_personal-sub-service', 'ul-personal-sub-service');
		menuCunsumerLv('ais_topbar-primary1', 'ul-personal-sub', 'ais_personal-sub-serenade', 'ul-personal-sub-serenade');
		menuCunsumerLv('ais_topbar-primary1', 'ul-personal-sub', 'ais_personal-sub-move_ais', 'ul-personal-sub-move_ais');

		menuBusinessLv('ais_topbar-primary2', 'ul-customer-sub', 'ais_customer-sub-big_business', 'ul-customer-sub-big_business');
		menuBusinessLv('ais_topbar-primary2', 'ul-customer-sub', 'ais_customer-sub-small_business', 'ul-customer-sub-sme');
		menuBusinessLv('ais_topbar-primary2', 'ul-customer-sub', 'ais_customer-sub-products', 'ul-customer-sub-device');

		//menuInvesterLv('ais_topbar-primary3', 'ul-invester-sub', 'ais_investor-sub-home', 'ul-investor-sub-home');
		menuInvesterLv('ais_topbar-primary3', 'ul-invester-sub', 'ais_investor-sub-corporate_overview', 'ul-investor-sub-corporate_overview');
		menuInvesterLv('ais_topbar-primary3', 'ul-invester-sub', 'ais_investor-sub-financial_report', 'ul-investor-sub-financial_report');
		menuInvesterLv('ais_topbar-primary3', 'ul-invester-sub', 'ais_investor-sub-stock_information', 'ul-investor-sub-stock_information');
		menuInvesterLv('ais_topbar-primary3', 'ul-invester-sub', 'ais_investor-sub-corporate_governance', 'ul-investor-sub-corporate_governance');
		menuInvesterLv('ais_topbar-primary3', 'ul-invester-sub', 'ais_investor-sub-news_events', 'ul-investor-sub-news_events');
		//menuInvesterLv('ais_topbar-primary3', 'ul-invester-sub', 'ais_investor-sub-contact_us', 'ul-investor-sub-contact_us');

		//menuTwoLv('ais_customer-sub-small_business');
		menuTwoLv('ais_customer-sub-device');
		menuTwoLv('ais_customer-sub-privilege');
		menuTwoLv('ais_customer-sub-contact');
		menuTwoLv('ais_customer-sub-home_business');

		menuTwoLv('ais_investor-sub-home');
		menuTwoLv('ais_investor-sub-contact_us');

		//menuOneLv('ais_topbar-primary3');

		menuAboutLv();

$( "#ais_personal-sub-entertain" )
	  .mouseenter(function() {
		  $( ".ais_personal-sub" ).removeClass('active');
		  $('.menu-box-wrap').hide();
	  });

$( "#ais_topbar-eservice" ).click(function() {

/*if( $( ".pp_onlinestore").hasClass( "expand" )  === true ){

	$('.js-overlay').removeClass("is-visible");
		if ($(window).width() > 768) {
				$(".js-custom-header").show();
			}else{}
  }else{

	$('.js-overlay').addClass("is-visible");
	$(".js-custom-header").hide();
  }*/
		 var info ="";
		 var lang = $("#ais_topbar-eservice").attr('lang');
	     info="business-"+lang+"-header_menu-ebusiness_menu";
	     digitalData.customLinkName=info;
	     _satellite.track("custom-link-click");

	 // $('.primary-nav').removeClass("nav-slidedown");
	 // $('#nav-icon1').removeClass("open");
	 // $( ".pp_onlinestore" ).toggleClass( "expand" );
});


$( "#pp_onlinestore" )
	  .mouseenter(function() {
	  })
	  .mouseleave(function() {
		$('.pp_onlinestore').removeClass('expand');
		$('.js-overlay').removeClass("is-visible");
	  });

}

//var RootDoc = document.domain;
var RootDoc = "www.ais.co.th";
var aismenupath;

function CheckMultiLang(multilanguage){
	if(multilanguage ==='no-en' || multilanguage ==='single-language'){
		setTimeout(function(){
			$( ".topbar_lang, .topbar_lang_mobile" ).hide();
			$('#ais_topbar').addClass('no-en');
		 },100)
		//console.log(multilanguage);
	}
}

//function ais_interface_setup(loadtheme, loadsite, loadlanguage, loadfile, loadLangButton, flag){
//ais_interface_setup('mini-menu','home','th');

var menufile
var menufile_en

function ais_interface_setup(loadtheme, loadsite, loadlanguage, multilanguage, loadfile, innerPage){

 	if(loadsite ==='home'){
		menufile = "aismenu.html";
		menufile_en = "aismenu_en.html";
	}else if(loadsite ==='postpaid'){
		menufile = "postpaidmenu.html";
		menufile_en = "postpaidmenu_en.html";
	}else if(loadsite ==='prepaid'){
		menufile = "prepaidmenu.html";
		menufile_en = "prepaidmenu_en.html";
	}else{}

	if(loadlanguage ==='th'){
        if(innerPage === true) {
            $("#ais_topbar").load("../base_interface/"+menufile, function() {
                aistopbarScript();
                CheckMultiLang(multilanguage);
            });
            $("#aisfooter").load( "../base_interface/aisfooter.html", footerscript);
        } else {
            $("#ais_topbar").load("base_interface/"+menufile, function() {
                aistopbarScript();
                CheckMultiLang(multilanguage);
            });
            $("#aisfooter").load( "base_interface/aisfooter.html", footerscript);
        }
	} else if(loadlanguage ==='en'){
        if(innerPage === true) {
            $("#ais_topbar").load("../../base_interface/"+menufile_en, function() {
                aistopbarScript();
                CheckMultiLang(multilanguage);
            });
            $("#aisfooter").load( "../../base_interface/aisfooter_en.html", footerscript);
        } else {
            $("#ais_topbar").load("../base_interface/"+menufile_en, function() {
                aistopbarScript();
                CheckMultiLang(multilanguage);
            });
            $("#aisfooter").load( "../base_interface/aisfooter_en.html", footerscript);
        }
	}

	if(loadtheme ==='full-menu'){
		//$('#ais_topbar').addClass('mini-menu');
	}

	if(loadtheme ==='mini-menu'){
		$('#ais_topbar').addClass('mini-menu');
	}

	if(loadtheme ==='custom-menu'){

		$('#ais_topbar').addClass('mini-menu');

		setTimeout(function(){
			//custom-only-slidemenu
			$( ".secondary-link-custom"  ).load( loadfile, function() {
				//console.log(loadtheme+" start");
				$('.secondary-link').hide();
				//custom-menu
				$('<div />', {'class':'js-custom-header'}).insertAfter('#ais_topbar-primary');
				$('.secondary-link-custom').clone().appendTo('.js-custom-header');
			});
			//custom-only-slidemenu
		}, 700);
	}

	if(loadtheme ==='custom-only-slidemenu'){
		$('#ais_topbar').addClass('mini-menu');
		setTimeout(function(){
			$( ".secondary-link-custom").load(loadfile, function() {
				$('.secondary-link').hide();
			});
		}, 1000);
	}

	if(loadtheme ==='custom-menu-icon'){
		//console.log(loadtheme);
		$('#ais_topbar').addClass('mini-menu');

		setTimeout(function(){
			$('<ul />', {'class':'secondary-link-icon'}).insertBefore( ".secondary-link-custom" );
			//custom-only-slidemenu
			$( ".secondary-link-icon"  ).load( loadfile, function() {
				//console.log(loadtheme+" start");
				$('.secondary-link').hide();

				//custom-menu
				$('<div />', {'class':'js-custom-header'}).insertAfter('#ais_topbar-primary');
				$('.secondary-link-icon').clone().appendTo('.js-custom-header');
			});
			//custom-only-slidemenu
		}, 700);
	}


/*setTimeout(function(){
	$('a[href="https://truehits.net/stat.php?login=ais"]').css("display","none");
}, 500);*/

}
/*!
 * smartbanner.js v1.9.1
 */
(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, "__esModule", {
  value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var Bakery = function () {
  function Bakery() {
    _classCallCheck(this, Bakery);
  }

  _createClass(Bakery, null, [{
    key: 'getCookieExpiresString',
    value: function getCookieExpiresString(hideTtl) {
      var now = new Date();
      var expireTime = new Date(now.getTime() + hideTtl);
      return 'expires=' + expireTime.toGMTString() + ';';
    }
  }, {
    key: 'getPathString',
    value: function getPathString(path) {
      return 'path=' + path + ';';
    }
  }, {
    key: 'bake',
    value: function bake(hideTtl) {
      var hidePath = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : null;

      document.cookie = 'smartbanner_exited=1; ' + (hideTtl ? Bakery.getCookieExpiresString(hideTtl) : '') + ' ' + (hidePath ? Bakery.getPathString(hidePath) : '');
    }
  }, {
    key: 'unbake',
    value: function unbake() {
      document.cookie = 'smartbanner_exited=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    }
  }, {
    key: 'baked',
    get: function get() {
      var value = document.cookie.replace(/(?:(?:^|.*;\s*)smartbanner_exited\s*\=\s*([^;]*).*$)|^.*$/, '$1');
      return value === '1';
    }
  }]);

  return Bakery;
}();

exports.default = Bakery;

},{}],2:[function(require,module,exports){
(function (global){
'use strict';

Object.defineProperty(exports, "__esModule", {
  value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var Detector = function () {
  function Detector() {
    _classCallCheck(this, Detector);
  }

  _createClass(Detector, null, [{
    key: 'platform',
    value: function platform() {
      if (/iPhone|iPad|iPod/i.test(window.navigator.userAgent)) {
        return 'ios';
      } else if (/Android/i.test(window.navigator.userAgent)) {
        return 'android';
      }
    }
  }, {
    key: 'userAgentMatchesRegex',
    value: function userAgentMatchesRegex(regexString) {
      return new RegExp(regexString).test(window.navigator.userAgent);
    }
  }, {
    key: 'jQueryMobilePage',
    value: function jQueryMobilePage() {
      return typeof global.$ !== 'undefined' && global.$.mobile !== 'undefined' && document.querySelector('.ui-page') !== null;
    }
  }, {
    key: 'wrapperElement',
    value: function wrapperElement() {
      var selector = Detector.jQueryMobilePage() ? '.ui-page' : 'html';
      return document.querySelectorAll(selector);
    }
  }]);

  return Detector;
}();

exports.default = Detector;

}).call(this,typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : typeof window !== "undefined" ? window : {})
},{}],3:[function(require,module,exports){
'use strict';

var _smartbanner = require('./smartbanner.js');

var _smartbanner2 = _interopRequireDefault(_smartbanner);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var smartbanner = void 0;

window.addEventListener('load', function () {
  smartbanner = new _smartbanner2.default();
  smartbanner.publish();
});

},{"./smartbanner.js":7}],4:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, "__esModule", {
  value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

require('./polyfills/array/from.js');

require('./polyfills/array/includes.js');

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function valid(name) {
  // TODO: validate against options dictionary
  return name.indexOf('smartbanner:') !== -1 && name.split(':')[1].length > 0;
}

function convertToCamelCase(name) {
  var parts = name.split('-');
  parts.map(function (part, index) {
    if (index > 0) {
      parts[index] = part.charAt(0).toUpperCase() + part.substring(1);
    }
  });
  return parts.join('');
}

var OptionParser = function () {
  function OptionParser() {
    _classCallCheck(this, OptionParser);
  }

  _createClass(OptionParser, [{
    key: 'parse',
    value: function parse() {
      var metas = document.getElementsByTagName('meta');
      var options = {};
      var optionName = null;
      Array.from(metas).forEach(function (meta) {
        var name = meta.getAttribute('name');
        var content = meta.getAttribute('content');
        if (name && content && valid(name) && content.length > 0) {
          optionName = name.split(':')[1];
          if (Array.from(optionName).includes('-')) {
            optionName = convertToCamelCase(optionName);
          }
          options[optionName] = content;
        }
      });
      return options;
    }
  }]);

  return OptionParser;
}();

exports.default = OptionParser;

},{"./polyfills/array/from.js":5,"./polyfills/array/includes.js":6}],5:[function(require,module,exports){
'use strict';

// Production steps of ECMA-262, Edition 6, 22.1.2.1
// Reference: https://people.mozilla.org/~jorendorff/es6-draft.html#sec-array.from
if (!Array.from) {
  Array.from = function () {
    var toStr = Object.prototype.toString;
    var isCallable = function isCallable(fn) {
      return typeof fn === 'function' || toStr.call(fn) === '[object Function]';
    };
    var toInteger = function toInteger(value) {
      var number = Number(value);
      if (isNaN(number)) {
        return 0;
      }
      if (number === 0 || !isFinite(number)) {
        return number;
      }
      return (number > 0 ? 1 : -1) * Math.floor(Math.abs(number));
    };
    var maxSafeInteger = Math.pow(2, 53) - 1;
    var toLength = function toLength(value) {
      var len = toInteger(value);
      return Math.min(Math.max(len, 0), maxSafeInteger);
    };

    // The length property of the from method is 1.
    return function from(arrayLike /*, mapFn, thisArg */) {
      // 1. Let C be the this value.
      var C = this;

      // 2. Let items be ToObject(arrayLike).
      var items = Object(arrayLike);

      // 3. ReturnIfAbrupt(items).
      if (arrayLike ===null) {
        throw new TypeError("Array.from requires an array-like object - not null or undefined");
      }

      // 4. If mapfn is undefined, then let mapping be false.
      var mapFn = arguments.length > 1 ? arguments[1] : void undefined;
      var T;
      if (typeof mapFn !== 'undefined') {
        // 5. else
        // 5. a If IsCallable(mapfn) is false, throw a TypeError exception.
        if (!isCallable(mapFn)) {
          throw new TypeError('Array.from: when provided, the second argument must be a function');
        }

        // 5. b. If thisArg was supplied, let T be thisArg; else let T be undefined.
        if (arguments.length > 2) {
          T = arguments[2];
        }
      }

      // 10. Let lenValue be Get(items, "length").
      // 11. Let len be ToLength(lenValue).
      var len = toLength(items.length);

      // 13. If IsConstructor(C) is true, then
      // 13. a. Let A be the result of calling the [[Construct]] internal method of C with an argument list containing the single item len.
      // 14. a. Else, Let A be ArrayCreate(len).
      var A = isCallable(C) ? Object(new C(len)) : new Array(len);

      // 16. Let k be 0.
      var k = 0;
      // 17. Repeat, while k < len… (also steps a - h)
      var kValue;
      while (k < len) {
        kValue = items[k];
        if (mapFn) {
          A[k] = typeof T === 'undefined' ? mapFn(kValue, k) : mapFn.call(T, kValue, k);
        } else {
          A[k] = kValue;
        }
        k += 1;
      }
      // 18. Let putStatus be Put(A, "length", len, true).
      A.length = len;
      // 20. Return A.
      return A;
    };
  }();
}

},{}],6:[function(require,module,exports){
'use strict';

if (!Array.prototype.includes) {
  Array.prototype.includes = function (searchElement /*, fromIndex*/) {
    'use strict';

    if (this ===null) {
      throw new TypeError('Array.prototype.includes called on null or undefined');
    }

    var O = Object(this);
    var len = parseInt(O.length, 10) || 0;
    if (len === 0) {
      return false;
    }
    var n = parseInt(arguments[1], 10) || 0;
    var k;
    if (n >= 0) {
      k = n;
    } else {
      k = len + n;
      if (k < 0) {
        k = 0;
      }
    }
    var currentElement;
    while (k < len) {
      currentElement = O[k];
      if (searchElement === currentElement || searchElement !== searchElement && currentElement !== currentElement) {
        // NaN !== NaN
        return true;
      }
      k++;
    }
    return false;
  };
}

},{}],7:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, "__esModule", {
  value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _optionparser = require('./optionparser.js');

var _optionparser2 = _interopRequireDefault(_optionparser);

var _detector = require('./detector.js');

var _detector2 = _interopRequireDefault(_detector);

var _bakery = require('./bakery.js');

var _bakery2 = _interopRequireDefault(_bakery);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var DEFAULT_PLATFORMS = 'android,ios';

var datas = {
  originalTop: 'data-smartbanner-original-top',
  originalMarginTop: 'data-smartbanner-original-margin-top'
};

function handleExitClick(event, self) {
  self.exit();
  event.preventDefault();
}

function handleJQueryMobilePageLoad(event) {
  if (!this.positioningDisabled) {
    setContentPosition(event.data.height);
  }
}

function addEventListeners(self) {
  var closeIcon = document.querySelector('.js_smartbanner__exit');
  closeIcon.addEventListener('click', function (event) {
    return handleExitClick(event, self);
  });
  if (_detector2.default.jQueryMobilePage()) {
    $(document).on('pagebeforeshow', self, handleJQueryMobilePageLoad);
  }
}

function removeEventListeners() {
  if (_detector2.default.jQueryMobilePage()) {
    $(document).off('pagebeforeshow', handleJQueryMobilePageLoad);
  }
}

function setContentPosition(value) {
  var wrappers = _detector2.default.wrapperElement();
  for (var i = 0, l = wrappers.length, wrapper; i < l; i++) {
    wrapper = wrappers[i];
    if (_detector2.default.jQueryMobilePage()) {
      if (wrapper.getAttribute(datas.originalTop)) {
        continue;
      }
      var top = parseFloat(getComputedStyle(wrapper).top);
      wrapper.setAttribute(datas.originalTop, isNaN(top) ? 0 : top);
      wrapper.style.top = value + 'px';
    } else {
      if (wrapper.getAttribute(datas.originalMarginTop)) {
        continue;
      }
      var margin = parseFloat(getComputedStyle(wrapper).marginTop);
      wrapper.setAttribute(datas.originalMarginTop, isNaN(margin) ? 0 : margin);
      wrapper.style.marginTop = value + 'px';
    }
  }
}

function restoreContentPosition() {
  var wrappers = _detector2.default.wrapperElement();
  for (var i = 0, l = wrappers.length, wrapper; i < l; i++) {
    wrapper = wrappers[i];
    if (_detector2.default.jQueryMobilePage() && wrapper.getAttribute(datas.originalTop)) {
      wrapper.style.top = wrapper.getAttribute(datas.originalTop) + 'px';
    } else if (wrapper.getAttribute(datas.originalMarginTop)) {
      wrapper.style.marginTop = wrapper.getAttribute(datas.originalMarginTop) + 'px';
    }
  }
}

var SmartBanner = function () {
  function SmartBanner() {
    _classCallCheck(this, SmartBanner);

    var parser = new _optionparser2.default();
    this.options = parser.parse();
    this.platform = _detector2.default.platform();
  }

  // DEPRECATED. Will be removed.

  _createClass(SmartBanner, [{
    key: 'publish',
    value: function publish() {
      if (Object.keys(this.options).length === 0) {
        throw new Error('No options detected. Please consult documentation.');
      }

      if (_bakery2.default.baked) {
        return false;
      }

      // User Agent was explicetely excluded by defined excludeUserAgentRegex
      if (this.userAgentExcluded) {
        return false;
      }

      // User agent was neither included by platformEnabled,
      // nor by defined includeUserAgentRegex
      if (!(this.platformEnabled || this.userAgentIncluded)) {
        return false;
      }

      var bannerDiv = document.createElement('div');
      document.querySelector('body').appendChild(bannerDiv);
      bannerDiv.outerHTML = this.html;
      if (!this.positioningDisabled) {
        setContentPosition(this.height);
      }
      addEventListeners(this);
    }
  }, {
    key: 'exit',
    value: function exit() {
      removeEventListeners();
      if (!this.positioningDisabled) {
        restoreContentPosition();
      }
      var banner = document.querySelector('.js_smartbanner');
      document.querySelector('body').removeChild(banner);
      _bakery2.default.bake(this.hideTtl, this.hidePath);
    }
  }, {
    key: 'originalTop',
    get: function get() {
      var wrapper = _detector2.default.wrapperElement()[0];
      return parseFloat(wrapper.getAttribute(datas.originalTop));
    }

    // DEPRECATED. Will be removed.

  }, {
    key: 'originalTopMargin',
    get: function get() {
      var wrapper = _detector2.default.wrapperElement()[0];
      return parseFloat(wrapper.getAttribute(datas.originalMarginTop));
    }
  }, {
    key: 'priceSuffix',
    get: function get() {
      if (this.platform === 'ios') {
        return this.options.priceSuffixApple;
      } else if (this.platform === 'android') {
        return this.options.priceSuffixGoogle;
      }
      return '';
    }
  }, {
    key: 'icon',
    get: function get() {
      if (this.platform === 'android') {
        return this.options.iconGoogle;
      } else {
        return this.options.iconApple;
      }
    }
  }, {
    key: 'buttonUrl',
    get: function get() {
      if (this.platform === 'android') {
        return this.options.buttonUrlGoogle;
      } else if (this.platform === 'ios') {
        return this.options.buttonUrlApple;
      }
      return '#';
    }
  }, {
    key: 'html',
    get: function get() {
      var modifier = !this.options.customDesignModifier ? this.platform : this.options.customDesignModifier;
      return '<div class="smartbanner smartbanner--' + modifier + ' js_smartbanner">\n      <a href="javascript:void();" class="smartbanner__exit js_smartbanner__exit"></a>\n      <div class="smartbanner__icon" style="background-image: url(' + this.icon + ');"></div>\n      <div class="smartbanner__info">\n        <div>\n          <div class="smartbanner__info__title">' + this.options.title + '</div>\n          <div class="smartbanner__info__author">' + this.options.author + '</div>\n          <div class="smartbanner__info__price">' + this.options.price + this.priceSuffix + '</div>\n        </div>\n      </div>\n      <a href="' + this.buttonUrl + '" target="_blank" class="smartbanner__button"><span class="smartbanner__button__label">' + this.options.button + '</span></a>\n    </div>';
    }
  }, {
    key: 'height',
    get: function get() {
      var height = document.querySelector('.js_smartbanner').offsetHeight;
      return height !== undefined ? height : 0;
    }
  }, {
    key: 'platformEnabled',
    get: function get() {
      var enabledPlatforms = this.options.enabledPlatforms || DEFAULT_PLATFORMS;
      return enabledPlatforms && enabledPlatforms.replace(/\s+/g, '').split(',').indexOf(this.platform) !== -1;
    }
  }, {
    key: 'positioningDisabled',
    get: function get() {
      return this.options.disablePositioning === 'true';
    }
  }, {
    key: 'userAgentExcluded',
    get: function get() {
      if (!this.options.excludeUserAgentRegex) {
        return false;
      }
      return _detector2.default.userAgentMatchesRegex(this.options.excludeUserAgentRegex);
    }
  }, {
    key: 'userAgentIncluded',
    get: function get() {
      if (!this.options.includeUserAgentRegex) {
        return false;
      }
      return _detector2.default.userAgentMatchesRegex(this.options.includeUserAgentRegex);
    }
  }, {
    key: 'hideTtl',
    get: function get() {
      return this.options.hideTtl ? parseInt(this.options.hideTtl) : false;
    }
  }]);

  return SmartBanner;
}();

exports.default = SmartBanner;

},{"./bakery.js":1,"./detector.js":2,"./optionparser.js":4}]},{},[3]);

var dv;
function checkDevice(){

  if(navigator.userAgent.match(/Android/i)){dv = "Android";}
  else if(navigator.userAgent.match(/BlackBerry/i)){dv = "BlackBerry";}
  else if(navigator.userAgent.match(/iPhone/i)){dv = "iPhone";}
  else if(navigator.userAgent.match(/iPad/i)){dv = "iPad";}
  else if(navigator.userAgent.match(/iPod/i)){dv = "iPod";}
  else if(navigator.userAgent.match(/Opera Mini/i)){dv = "Opera";}
  else if(navigator.userAgent.match(/IEMobile/i)){dv = "WindowsMobile";}
  if(dv ==="iPhone" || dv ==="iPad" || dv ==="iPod" || dv ==="Android" || dv ==="BlackBerry" || dv ==="Opera" || dv ==="WindowsMobile"){
			dv = "Mobile";

		}else{dv = "Desktop";

		}
		return dv

	}

$( document ).ready(function() {
	loadImages();
    setTimeout(function () {
        setCurrentMenu();
    }, 300);
});

function MM_preloadImages() { //v3.0
var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function loadImages() {
	//alert("start PRE-LOAD images");
	MM_preloadImages('https://business.ais.co.th/base_interface/images/icon_store.png',
	'https://business.ais.co.th/base_interface/images/icon_sprit.png',
	'https://business.ais.co.th/base_interface/images/icon_about.png',
	'https://business.ais.co.th/base_interface/images/icon_corporate.png',
	'https://business.ais.co.th/base_interfaceimages/icon_investor.png'
	);
	//alert("end PRE-LOAD images");
}

// Hide Header on on scroll down
//ask aunjai
var WidgetConfig;
function async_load(loadlanguage){
  WidgetConfig = {
    debug: true,
    domain: 'https://search.ais.co.th/aunjai-get-files',
    language: loadlanguage,
    fontHeaderSize : '22px',
    fontSize : '17px',
    saveQuestionURL: 'https://search.ais.co.th/api/ask-aunjai/save-question',
    liveChatURL: 'https://webchat1.smm.ais.co.th/livechat_aunjai/client.php',
    BecauseWeCare: 'https://crawl1.smm.ais.co.th/accfacebook/index.php/wecare_page?channel=aunjai',
    autocomplete: {
      source: 'https://search.ais.co.th/api/gsa/ask-aunjai'
    }
  };
  var s = document.createElement('script');
  s.type = 'text/javascript';
  s.async = true;
  s.src = 'https://search.ais.co.th/aunjai-get-files/js/widget.js';
  //s.src = 'http://www.ais.co.th/2017/js/widget.js';
  var x = document.getElementsByTagName('script')[0];
  x.parentNode.insertBefore(s, x);
}

//Active Menu
function setCurrentMenu() {
  var cUrl = window.location.href;
  cUrl = cUrl.split('/');
  var fUrl = cUrl[cUrl.length-1];
  var enUrl = cUrl[cUrl.length-3];
  var thUrl = cUrl[cUrl.length-2];
  if(enUrl === 'enterprise' || thUrl === 'enterprise') {
    $('a[href$="enterprise.html"]').addClass('current');
  } else if(enUrl === 'sme' || thUrl === 'sme') {
    $('a[href$="sme.html"]').addClass('current');
  } else if(enUrl === 'news-activities' || thUrl === 'news-activities') {
    $('a[href$="news-activities/index.html"]').addClass('current');
    $('a[href$="news-activities/en/index.html"]').addClass('current');
  } else if(thUrl === 'privileges') {
    $('a[href$="privileges.html"]').addClass('current');
  } else if(thUrl === 'solution') {
    $('a[href$="solutions.html"]').addClass('current');
  } else {
    $('a[href="'+fUrl+'"]').addClass('current');
  }
}

$(window).on('load', function() {
	$('#ul-click-solutions').find('.ais_about-sub').on('click touch', function () {
		$('#nav-icon1').removeClass("open");
		$('.primary-nav').removeClass("nav-slidedown");
		$('.js-overlay').removeClass("is-visible");

		if( $( ".primary-nav" ).height() > 0){
			if ($(window).width() > 768) {
				$(".js-custom-header").show();
			}else{}
		}else{
			$(".js-custom-header").hide();
		}
	});
});
