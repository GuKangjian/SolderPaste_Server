// JavaScript Document

// collapse/expand

jQuery('body').on('click', '.portlet > .portlet-title > .tools > .collapse, .portlet .portlet-title > .tools > .expand', function (e) {
    e.preventDefault();
    var el = jQuery(this).closest(".portlet").children(".portlet-body");
    if (jQuery(this).hasClass("collapse")) {
        jQuery(this).removeClass("collapse").addClass("expand");
        el.slideUp(200);
    } else {
        jQuery(this).removeClass("expand").addClass("collapse");
        el.slideDown(200);
    }
});


jQuery('body').on('click', '.box > .collapse, .box > .expand', function (e) {
    e.preventDefault();
    var el = jQuery(this).closest(".portlet").children(".portlet-body");
    if (jQuery(this).hasClass("collapse")) {
        jQuery(this).removeClass("collapse").addClass("expand");
        el.slideUp(200);
    } else {
        jQuery(this).removeClass("expand").addClass("collapse");
        el.slideDown(200);
    }
});