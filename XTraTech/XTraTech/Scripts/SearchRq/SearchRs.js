$(document).ready(function (e) {
    $('.sts').hide();
    $('.price_pop,.aa,.extra').fadeOut(0);
    $('.oneway_result').each(function () {
        var a = $(this).find('.row').size();
        var b = $(this).find('.row').height() + 15;

        var h = $('.newDiv').height();
        var heiy = $('.price_hld').height();

        if (a == 1) {
            $(this).css('height', (a * b) * 2);
            var h = $('.newDiv').height();
            $('.price_hld').css('height', h);
        }
        else if (a > 2) {
            $(this).css('height', '106px');
            var h = $('.newDiv').height();
            $('.price_hld').css('height', h);
        }        
        else {
            $(this).css('height', (a * b));
            if (h > heiy) {
                $('.price_hld').css('height', h);
            }
            else {
                $('.newDiv').css('height', heiy);
            }
        }
        if (h > heiy) {
            var h1 = $('.price_hld').height();
            var h2 = $('.oneway_result').height();
            var h3 = $('.price_hld p:last-child').height();
            $('.price_hld p:last-child').css('margin-top', (h2 - (h3 / 2)));
        }
        else {
            $('.price_hld p:last-child').css('margin-top', '0px');
        }
        //$(this).css('height', (a * b));

    });

    $('.book_btn').click(function () {

        $(this).toggleClass('selected');

        var s = $(this).parent().find('.sts').prop('checked');
        if (s == false) {
            $(this).parent().find('.sts').prop('checked', true);
        }
        else {
            $(this).parent().find('.sts').prop('checked', false);
        }
        //        if ($(this).parent().find('.sts').prop('checked') == true) {
        //            $(this).parent().find('.sts').prop('checked', false);
        //        }
    });
    $('.linkFarerules').click(function () {

        $(this).toggleClass('selected');
        $(this).parent().find('.farerules').prop('checked', true);


    });

    $('.info').mouseover(function () {

        $(this).parent().find('.price_pop').fadeIn('slow')
        $(this).parent().find('.aa').fadeIn('slow')

    });

    $('.info').mouseout(function () {

        $('.price_pop').fadeOut('slow')
        $('.aa').fadeOut('slow')

    });


    $('.fsc_val').mouseover(function () {

        $(this).parent().parent().find('.extra').fadeIn('slow')

    });

    $('.fsc_val').mouseout(function () {

        $('.extra').fadeOut('slow')


    });


    $('#btn_Submit').on('click', function () {




        var FSC = [];
        $('.sts:checked').each(function () {
            FSC.push($(this).attr("value"));
        });

        if (FSC.length > 1) {
            alert('Please select only one option');
            $('.overBg').remove();
            return false;
        }
        else if (FSC.length == 0) {
            alert('Please select an option');
            $('.overBg').remove();
            return false;
        }
        else {
            var wid = $(window).width();
            var hei = $(window).height();

            $('body').append('<div class="overBg"><p><img src="/Images/ajax-loader.gif" /> Loading please wait...</p></div>')

            $('.overBg').css({ 'width': wid, 'height': hei })

            $.ajax({
                type: "POST",
                url: "/Search/RevalidateSelectedItineraries",
                data: { SelectedFSC: FSC },
                datatype: "json",
                success: function () {
                  alert('lall')
                    window.location.href = "/PassengerDetails"
                },
                traditional: true,
                error: function () {
                    $('.overBg').remove();
                    $('#dialog_content').html('There is an error in revalidation. Please search again.');
                    $('#dialog-modal').dialog({
                        width: 600,
                        height: 300,
                        modal: true
                    });
                }

            });
        }
    });

    $('.linkFarerules').on('click', function () {

        var FSCr = [];

        $('input:checked').each(function () {
            FSCr.push($(this).attr("value"));
        });

        $.ajax({
            type: "POST",
            url: "/Search/FareRules",
            data: { FscForFareRules: FSCr },
            datatype: "json",
            traditional: true,
            success: function (result) {
                $('.overBg').remove()
                $('#dialog_content').html(result);
                $('#dialog-modal').dialog({
                    width: 600,
                    height: 500,
                    modal: true
                });
            },
            error: function () {
                $('.overBg').remove();
                $('#dialog_content').html('Fare Rules not available');
                $('#dialog-modal').dialog({
                    width: 600,
                    height: 300,
                    modal: true
                });
            }
        });
        $(this).toggleClass();
        $(this).parent().find('.farerules').prop('checked', false);
    });

    $('.farerules').hide();

    $(document).ajaxStart(function () {
        // show loader on start
        $("#progressbar").css("display", "block");

    }).ajaxSuccess(function () {
        // hide loader on success
        $("#progressbar").css("display", "none");
    });

    $('#btn_tripDetails').on('click', function () {
        window.location.href = "/Booking/TripDetails";
    });


    $('.linkFarerules').click(function () {
        var wid = $(window).width();
        var hei = $(window).height();

        $('body').append('<div class="overBg"><img src="/Images/ajax-loader.gif" /><p> Loading please wait...</p></div>')

        $('.overBg').css({ 'width': wid, 'height': hei })

    })

//    $("#draggable").draggable();

});

function modifySearch() {
    window.location.href = "/Search/Index";
}

//Loader
function loader() {

    var wid = $(window).width();
    var hei = $(window).height();
    var str1 = '<div class="overBgOthers"><p><img src="/Images/progress_bar.gif" /> Patience is a virtue...</p></div>';
    $('body').append(str1)

    $('.overBgOthers').css({ 'width': wid, 'height': hei })
}



