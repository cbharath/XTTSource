$(document).ready(function (e) {

    $('.cloned').fadeOut(0)
    $('.oneway').click(function () {
        if ($(this).is(':checked')) {

            $('.cloned').fadeOut(0);
            $('.toggler_one').fadeOut('slow');

        }


    })


    $('.return').click(function () {
        if ($(this).is(':checked')) {
            $('.cloned').fadeOut(0);
            $('.toggler_one').fadeIn('slow');

        }


    });



    $('.multi').click(function () {
        if ($(this).is(':checked')) {


            $('.cloned').fadeIn(0);
            $('.toggler_one').fadeOut(0);
        }


    });


    $('.depart_date,.return_date').datepicker({ dateFormat: "dd, MM yy",
        numberOfMonths: 2,
        stepMonths: 2,
        minDate: +0,
        maxDate: "+331D"
    });
});

function loader() {
    var a = $('#CityPares_0__ReturnDateTime');
    var b = $('#CityPares_0__ReturnDateTime').val();
    var c = $('#CityPares_0__DepartureDateTime').val();
    if ((a != null && b != "" && c != "" && (new Date(b).getTime() > new Date(c).getTime())) || (b == "" && c != "")) {
        if ($('#CityPares_0__Origin').val() != $('#CityPares_0__Destination').val()) {

            var wid = $(window).width();
            var hei = $(window).height();

            $('body').append('<div class="overBg"><p><img href="~/images/ajax-loader.gif" /> Loading please wait...</p></div>')

            $('.overBg').css({ 'width': wid, 'height': hei })
        }
        else {
            alert('Check the origin destination');
            return false;
        }
    }
    else {
        alert('Invalid dates');
        return false;
    }
}