/*!
 * Bootstrap v3.3.1 (http://getbootstrap.com)
 * Copyright 2011-2014 Twitter, Inc.
 * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
 */

/*!
 * Generated using the Bootstrap Customizer (http://getbootstrap.com/customize/?id=69f4eb728c41ce64d4fd)
 * Config saved to config.json and https://gist.github.com/69f4eb728c41ce64d4fd
 */
if("undefined"==typeof jQuery)throw new Error("Bootstrap's JavaScript requires jQuery");+function(t){var a=t.fn.jquery.split(" ")[0].split(".");if(a[0]<2&&a[1]<9||1==a[0]&&9==a[1]&&a[2]<1)throw new Error("Bootstrap's JavaScript requires jQuery version 1.9.1 or higher")}(jQuery),+function(t){"use strict";function a(a){return this.each(function(){var n=t(this),r=n.data("bs.tab");r||n.data("bs.tab",r=new e(this)),"string"==typeof a&&r[a]()})}var e=function(a){this.element=t(a)};e.VERSION="3.3.1",e.TRANSITION_DURATION=150,e.prototype.show=function(){var a=this.element,e=a.closest("ul:not(.dropdown-menu)"),n=a.data("target");if(n||(n=a.attr("href"),n=n&&n.replace(/.*(?=#[^\s]*$)/,"")),!a.parent("li").hasClass("active")){var r=e.find(".active:last a"),i=t.Event("hide.bs.tab",{relatedTarget:a[0]}),s=t.Event("show.bs.tab",{relatedTarget:r[0]});if(r.trigger(i),a.trigger(s),!s.isDefaultPrevented()&&!i.isDefaultPrevented()){var o=t(n);this.activate(a.closest("li"),e),this.activate(o,o.parent(),function(){r.trigger({type:"hidden.bs.tab",relatedTarget:a[0]}),a.trigger({type:"shown.bs.tab",relatedTarget:r[0]})})}}},e.prototype.activate=function(a,n,r){function i(){s.removeClass("active").find("> .dropdown-menu > .active").removeClass("active").end().find('[data-toggle="tab"]').attr("aria-expanded",!1),a.addClass("active").find('[data-toggle="tab"]').attr("aria-expanded",!0),o?(a[0].offsetWidth,a.addClass("in")):a.removeClass("fade"),a.parent(".dropdown-menu")&&a.closest("li.dropdown").addClass("active").end().find('[data-toggle="tab"]').attr("aria-expanded",!0),r&&r()}var s=n.find("> .active"),o=r&&t.support.transition&&(s.length&&s.hasClass("fade")||!!n.find("> .fade").length);s.length&&o?s.one("bsTransitionEnd",i).emulateTransitionEnd(e.TRANSITION_DURATION):i(),s.removeClass("in")};var n=t.fn.tab;t.fn.tab=a,t.fn.tab.Constructor=e,t.fn.tab.noConflict=function(){return t.fn.tab=n,this};var r=function(e){e.preventDefault(),a.call(t(this),"show")};t(document).on("click.bs.tab.data-api",'[data-toggle="tab"]',r).on("click.bs.tab.data-api",'[data-toggle="pill"]',r)}(jQuery);