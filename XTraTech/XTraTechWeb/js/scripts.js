

tjq(document).ready(function() {
    // UI Form Element
    tjq('.cloned').fadeOut(0)
    tjq('.oneway').click(function () {
        if (tjq(this).is(':checked')) {

            tjq('.cloned').fadeOut(0);
            tjq('.toggler_one').fadeOut('slow');

        }


    })


    tjq('.return').click(function () {
        if (tjq(this).is(':checked')) {
            tjq('.cloned').fadeOut(0);
            tjq('.toggler_one').fadeIn('slow');

        }


    });



    tjq('.multi').click(function () {
        if (tjq(this).is(':checked')) {


            tjq('.cloned').fadeIn(0);
            tjq('.toggler_one').fadeOut(0);
        }


    });
    //$('.oneway').click(function () {
    //    $('#JourneyType').val("One way")


    //    if ($('#CityPares_1__Origin').val() != "") {
    //        $('#CityPares_0__Origin').val($('#CityPares_1__Origin').val());
    //    }

    //    if ($('#CityPares_1__Destination').val() != "") {
    //        $('#CityPares_0__Destination').val($('#CityPares_1__Destination').val());
    //    }

    //    if ($('#dp1429505733647').val() != "") {
    //        $('#dp1429591406735').val($('#dp1429505733647').val());
    //    }


    //});

    //$('.return').click(function () {
    //    $('#JourneyType').val("Round trip")


    //    if ($('#CityPares_0__Origin').val() != "") {
    //        $('#CityPares_1__Origin').val($('#CityPares_0__Origin').val());
    //    }

    //    if ($('#CityPares_0__Destination').val() != "") {
    //        $('#CityPares_1__Destination').val($('#CityPares_0__Destination').val());
    //    }

    //    if ($('#dp1429591406735').val() != "") {
    //        $('#dp1429505733647').val($('#dp1429591406735').val());
    //    }


    //});

    //$('.multi').click(function () {
    //    $('#JourneyType').val("Multi trip")
    //});



    tjq("input[data-autocomplete]").focus(function () {

        tjq(this).each(function () {

            tjq(this).autocomplete({
                source: function (request, response) {
                    tjq.ajax({
                       
                        dataType: "json",
                        data: {
                            q: request.term
                        },
                         url:  "http://" + window.location.host + "/Search/AirportList?term=" + request.term,
                        success: function (data) {
                            
                            response(data);

                         
                            
                        }
                    });
                }
            });

        });
    });

    //tjq("CurrencyChange").focus(function () {

    //    tjq(this).each(function () {

    //        tjq(this).autocomplete({
    //            source: function () {
    //                tjq.ajax({

    //                    dataType: "json",
    //                    data: {
    //                        q: request.term
    //                    },
    //                    url: "http://" + window.location.host + "/Search/ChangeCurrency?tocurrency=" + request.term,
    //                    success: function (data) {

                          



    //                    }
    //                });
    //            }
    //        });

    //    });
    //});

 
    //$('.currency_dropdown li a').click(function (e) {
    //    e.preventDefault(); // <------------------ stop default behaviour of button
        
    //    var curr = $(this).attr('id');
    //    var url = "";
    //    var comparepath = "/Search/SearchResults";
    //    var path = "/Passenger/Index";
    //    if (window.location.pathname.toLowerCase() == comparepath.toLowerCase())
    //    {
    //        url = "http://" + window.location.host + "/Search/ChangeCurrency?tocurrency=" + $(this).attr('id');
    //    }
    //   else if (window.location.pathname.toLocaleLowerCase() == path.toLocaleLowerCase()) {

    //        url = "http://" + window.location.host + "/Search/ChangeCurrencyofselectedOption";
    //    }
    //    else {
    //        url =  "http://" + window.location.host + "/Search/ChangeCurrency?tocurrency=" + $(this).attr('id');
    //    }
    //    $.ajax({
    //        url: url,
    //        type: "POST",
    //        beforeSend: function () {
                
    //                $('#loader').show();

    //        },
    //        success: function (result) {
                 
    //            $('.selectedcurr').text(curr)

    //            var comparepath = "/Search/SearchResults";
    //            if (window.location.pathname.toLocaleLowerCase() == comparepath.toLocaleLowerCase())
    //            {
    //                window.location.reload();
    //            }
    //        },

    //        complete: function () {
    //           $('#loader').hide();
         
    //        }
            
    //    });
    //});

    //Validation scripts.
    tjq("#Airsearch").validationEngine({
        scroll: false,
        promptPosition: "bottomLeft",
        onValidationComplete: function (form, status) {
            if (status == true) {
                tjq('#loader').show();
                setstorage();
                return true;
            }
        }
    });

    tjq("#LoginForm").validationEngine({ autoHidePrompt: true });

    tjq("#price-range").slider({
        range: true,
        min: 0,
        max: 1000,
        values: [100, 800],
        slide: function (event, ui) {
            tjq(".min-price-label").html("$" + ui.values[0]);
            tjq(".max-price-label").html("$" + ui.values[1]);
        }
    });
    tjq(".min-price-label").html("$" + tjq("#price-range").slider("values", 0));
    tjq(".max-price-label").html("$" + tjq("#price-range").slider("values", 1));

    function convertTimeToHHMM(t) {
        var minutes = t % 60;
        var hour = (t - minutes) / 60;
        var timeStr = (hour + "").lpad("0", 2) + ":" + (minutes + "").lpad("0", 2);
        var date = new Date("2014/01/01 " + timeStr + ":00");
        var hhmm = date.toLocaleTimeString(navigator.language, { hour: '2-digit', minute: '2-digit' });
        return hhmm;
    }
    tjq("#flight-times").slider({
        range: true,
        min: 0,
        max: 1440,
        step: 5,
        values: [360, 1200],
        slide: function (event, ui) {

            tjq(".start-time-label").html(convertTimeToHHMM(ui.values[0]));
            tjq(".end-time-label").html(convertTimeToHHMM(ui.values[1]));
        }
    });
    tjq(".start-time-label").html(convertTimeToHHMM(tjq("#flight-times").slider("values", 0)));
    tjq(".end-time-label").html(convertTimeToHHMM(tjq("#flight-times").slider("values", 1)));
 
    

});
function loader() {

    var wid = tjq(window).width();
    var hei = tjq(window).height();
    var origin = tjq('#CityPares_0__Origin').val();
    var destination = tjq('#CityPares_0__Destination').val();
    var onwarddate = tjq('#CityPares_0__DepartureDateTime').val();
    var returndate = tjq('#CityPares_0__ReturnDateTime').val();
    var str = '';
    if (returndate == '') {
        str = '<div class="overBg"><div style="color:#777; background: #fff; width:700px; height:300px; border: 2px groove #EEE; border-radius: 10px; margin: 0 auto; margin-top:100px;" align="center"><div><h1>Aggregating flights...</h1></div><div><img src="/Images/loader.gif" width="124" height="128" /></div><div><h2>OneWay Trip</h2></div><div><h3><span>From:  ' + origin + '</span> - <span>To:  ' + destination + '</span></h3></div><div><h3><span>Travel on:  ' + onwarddate + ' </span></h3></div></div></div>';
    }
    else {
        str = '<div class="overBg"><div style="color:#777; background: #fff; width:700px; height:300px; border: 2px groove #EEE; border-radius: 10px; margin: 0 auto; margin-top:100px;" align="center"><div><h1>Aggregating flights...</h1></div><div><img src="/Images/loader.gif" width="124" height="128" /></div><div><h2>Return Trip</h2></div><div><h3><span>From:  ' + origin + '</span> - <span>To:  ' + destination + '</span></h3></div><div><h3><span>Travel on:  ' + onwarddate + ' </span> - <span>Return on:  ' + returndate + ' </span></h3></div></div></div>';
    }
    var str1 = '<div class="overBg"><p><img src="/Images/ajax-loader.gif" /> Loading please wait...</p></div>';
    tjq('body').append(str)

    tjq('.overBg').css({ 'width': wid, 'height': hei })
}
