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
    var origin = $('#CityPares_0__Origin').val();
    var destination = $('#CityPares_0__Destination').val();
    var onwarddate = $('#CityPares_0__DepartureDateTime').val();
    var returndate = $('#CityPares_0__ReturnDateTime').val();
    var str = '';
    if (returndate == '')
    {
        str = '<div class="overBg"><div style="color:#777; background: #fff; width:700px; height:300px; border: 2px groove #EEE; border-radius: 10px; margin: 0 auto; margin-top:100px;" align="center"><div><h1>Aggregating flights...</h1></div><div><img src="/Images/loader.gif" width="124" height="128" /></div><div><h2>OneWay Trip</h2></div><div><h3><span>From:  '+origin+'</span> - <span>To:  '+ destination+'</span></h3></div><div><h3><span>Travel on:  '+ onwarddate+' </span></h3></div></div></div>';
    }
    else
    {
        str = '<div class="overBg"><div style="color:#777; background: #fff; width:700px; height:300px; border: 2px groove #EEE; border-radius: 10px; margin: 0 auto; margin-top:100px;" align="center"><div><h1>Aggregating flights...</h1></div><div><img src="/Images/loader.gif" width="124" height="128" /></div><div><h2>Return Trip</h2></div><div><h3><span>From:  ' + origin + '</span> - <span>To:  ' + destination + '</span></h3></div><div><h3><span>Travel on:  ' + onwarddate + ' </span> - <span>Return on:  ' + returndate + ' </span></h3></div></div></div>';
    }
    var str1 = '<div class="overBg"><p><img src="/Images/ajax-loader.gif" /> Loading please wait...</p></div>';
    $('body').append(str)

    $('.overBg').css({ 'width': wid, 'height': hei })
}



