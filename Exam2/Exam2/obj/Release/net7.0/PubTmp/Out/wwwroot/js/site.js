// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(window).width() <= 960 && ($("main").removeClass("main-active"),
    $(".sidebar").removeClass("sidebar-active")),
    $("#sidebar-button").click(function (a) {
        a.stopPropagation(),
            $("main").css("transition", "0.5s ease-in-out"),
            $(".sidebar").css("transition", "0.5s ease-in-out"),
            $("main").hasClass("main-active") && $(".sidebar").hasClass("sidebar-active") ? ($("main").removeClass("main-active"),
                $(".sidebar").removeClass("sidebar-active")) : ($("main").addClass("main-active"),
                    $(".sidebar").addClass("sidebar-active"))
    }),
    $(".sidebar").click(function (a) {
        a.stopPropagation()
    }),
    $(".showmore-btn").click(function () {
        $(".showmore").toggleClass("d-none")
    }),
    $(".btn-close").click(function (a) {
        a.stopPropagation(),
            $("main").css("transition", "0.5s ease-in-out"),
            $(".sidebar").css("transition", "0.5s ease-in-out"),
            $("main").hasClass("main-active") && $(".sidebar").hasClass("sidebar-active") && ($("main").removeClass("main-active"),
                $(".sidebar").removeClass("sidebar-active"),
                $("#sidebar-button").removeClass("d-none"))
    }),
    $(window).resize(function () {
        $(window).width() <= 960 && ($("main").removeClass("main-active"),
            $(".sidebar").removeClass("sidebar-active"),
            $("main").css("transition", "0.5s ease-in-out"),
            $(".sidebar").css("transition", "0.5s ease-in-out"))
    }),
    $(".owl-carousel").owlCarousel({
        autoPlay: 1e3,
        loop: !0,
        mouseDrag: !1,
        items: 1,
        animateOut: "fadeOut"
    });
