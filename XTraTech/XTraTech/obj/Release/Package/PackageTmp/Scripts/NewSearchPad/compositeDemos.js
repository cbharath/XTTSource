jQuery.noConflict();
jQuery.fn.exists = function () { return this.length > 0; }
jQuery(document).ready(function ($) {
    /* Common Functionality */
    //Numeric Check
    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }
    //Watermark 
    $(".watermark").each(function () {
        if ($(this).val() == "") {
            $(this).val(jQuery(this).attr("title")).addClass("watermarked");
        }
        $(this).focus(function () {
            if (jQuery(this).val() == jQuery(this).attr("title")) {
                jQuery(this).val("").removeClass("watermarked");
            }
        });
        $(this).blur(function () {
            if ($(this).val() == "") {
                $(this).val($(this).attr("title")).addClass("watermarked");
            }
        });
    });
    //Accordion
    $(".entry-inner-bottom-info").accordion({
        collapsible: true,
        heightStyle: "content"
    });
    //Tabs
    $(".tabs-outer-wrap").tabs();
    //Tool Tips
    $(".bottom-content-widgets li .info-icon-wrap").tooltip({
        items: "[title]",
        content: function () {
            return $(this).attr("title");
        }
    });

    /* Demos Index Functionality */
    //Demos Right Nav
    $(".entry-listing-link").live("click", function () {
        var newDemo = $(this).attr("rel");
        $(".demo-frame").attr("src", newDemo);
    });
    $(".entry-listing-nav").accordion({
        collapsible: true,
        heightStyle: "content",
        active: false
    });
});