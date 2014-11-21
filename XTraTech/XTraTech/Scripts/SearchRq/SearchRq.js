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


    $('.depart_date,.return_date').datepicker({ dateFormat: "d M, y",
        numberOfMonths: 2,
        stepMonths: 2,
        minDate: +0,
        maxDate: "+331D"
    });

    //autocomplete
    setTimeout(function () {
        $(".origin,.destination").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Search/AirportList",
                    type: "POST",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: "(" + item.airportCode + ")," + item.airportName,
                                value: "(" + item.airportCode + ")," + item.airportName
                            }
                        }))
                    },
                    messages: {
                        noResults: "", results: ""
                    }
                })
            }
        });
    }, 500)
});

//Loader
function loader() {

    var wid = $(window).width();
    var hei = $(window).height();

    $('body').append('<div class="overBg"><p><img src="/Images/ajax-loader.gif" /> Loading please wait...</p></div>')

    $('.overBg').css({ 'width': wid, 'height': hei })
}



