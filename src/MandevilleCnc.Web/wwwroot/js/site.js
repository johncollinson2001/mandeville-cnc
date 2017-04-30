﻿$(function () {
    var $window = $(window);
    var $navbar = $('nav.navbar');
    var $navbarToggle = $navbar.find('.navbar-toggle');

    // Capture initial css values to manipulate later
    var initialWindowHeight = $window.height();
    var initialNavbarPaddingTop = $navbar.css('paddingTop').replace('px', '');
    var initialNavbarBackgroundRgba = $navbar.css('backgroundColor').replace('rgba(', '').replace(')', '').split(',');
    var initialNavbarBackgroundR = initialNavbarBackgroundRgba[0];
    var initialNavbarBackgroundG = initialNavbarBackgroundRgba[1];
    var initialNavbarBackgroundB = initialNavbarBackgroundRgba[2];

    // Make navbar change when user scrolls
    $window.scroll(function () {        
        var scrolledAmount = $(this).scrollTop();

        // Compute new values relative to how far the user has scrolled
        var newBackgroundAlpha = scrolledAmount >= initialWindowHeight 
            ? 1 : Math.pow((1 / initialWindowHeight) * scrolledAmount, 0.3);
        var newPaddingTop = scrolledAmount >= initialWindowHeight
            ? 0 : Math.round(initialNavbarPaddingTop - ((initialNavbarPaddingTop / initialWindowHeight) * scrolledAmount));

        // Set new css on the navbar
        $navbar
            .css('backgroundColor', 'rgba(' + initialNavbarBackgroundR + ',' + initialNavbarBackgroundG + ',' + initialNavbarBackgroundB + ',' + newBackgroundAlpha + ')')
            .css('paddingTop', newPaddingTop);
    });

    // Add class to the navbar when the toggle has been clicked, which allows css to transition the bg colour
    $navbarToggle.click(function () {
        $navbar.toggleClass('mdv-navbar-menu-open');
    });

    // Force a scroll event on page load
    window.scrollTo(0, 0);
});