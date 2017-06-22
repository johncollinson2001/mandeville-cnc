$(function () {
    // Vars
    var slideWidth, sliderListWidth;

    // Elements
    var $slider = $('#mdv-slider');
    var $sliderList = $slider.find('ul');
    var $sliderListItems = $sliderList.find('li');

    // How many slides
    var slideCount = $sliderListItems.length;

    // Sets the widths of the html elements in order for smooth scrolling
    function setWidths() {
        slideWidth = $slider.parent().width();
        sliderListWidth = slideCount * slideWidth;

        $slider.css({ width: slideWidth });
        $sliderList.css({ width: sliderListWidth, marginLeft: -slideWidth });
        $sliderListItems.css({ width: slideWidth });
        $sliderList.find('li:last-child').prependTo($sliderList);
    }

    // Moves the slider left
    function moveLeft() {
        $sliderList.animate({
            left: +slideWidth
        }, 200, function () {
            $sliderList.find('li:last-child').prependTo($sliderList);
            $sliderList.css('left', '');
        });
    };

    // Moves the slider right
    function moveRight() {
        $sliderList.animate({
            left: -slideWidth
        }, 200, function () {
            $sliderList.find('li:first-child').appendTo($sliderList);
            $sliderList.css('left', '');
        });
    };

    // Handlers
    $('a.mdv-slider-control-prev').click(function () {
        moveLeft();
        return false;
    });

    $('a.mdv-slider-control-next').click(function () {
        moveRight();
        return false;
    });

    $(window).resize(function () {
        setWidths();
    });

    // Init
    // ...

    // Set the initial widths
    setWidths();

    // Auto start slider
    setInterval(function () {
        moveRight();
    }, 5000);

    $slider.fadeIn();
});