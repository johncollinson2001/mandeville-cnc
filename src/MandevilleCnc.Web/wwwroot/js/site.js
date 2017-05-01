$(function () {
    var $window = $(window);
    var $navbar = $('nav.navbar');
    var $navbarToggle = $navbar.find('.navbar-toggle');
    var $forms = $('form');
    var $modal = $('.modal');

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

        // The vertical px value on which the navbar styling is complete
        var navbarStylingCompleteOn = initialWindowHeight - 250; // 250 = fairly arbitrary value

        // Compute new values relative to how far the user has scrolled
        var newBackgroundAlpha = scrolledAmount >= navbarStylingCompleteOn
            ? 1 : Math.pow((1 / navbarStylingCompleteOn) * scrolledAmount, 0.3); // Math.pow introduces a parabola curve to the calculation
        var newPaddingTop = scrolledAmount >= navbarStylingCompleteOn
            ? 0 : Math.round(initialNavbarPaddingTop - ((initialNavbarPaddingTop / navbarStylingCompleteOn) * scrolledAmount));

        // Set new css on the navbar
        $navbar
            .css('backgroundColor', 'rgba(' + initialNavbarBackgroundR + ',' + initialNavbarBackgroundG + ',' + initialNavbarBackgroundB + ',' + newBackgroundAlpha + ')')
            .css('paddingTop', newPaddingTop);
    });

    // Add class to the navbar when the toggle has been clicked, which allows css to transition the bg colour
    $navbarToggle.click(function () {
        $navbar.toggleClass('mdv-navbar-menu-open');
    });

    // Add async submission to forms
    $forms.each(function () {
        $(this).submit(function (e) {
            var submitButton = $(this).find('button[type=submit]');

            // Store the value of the submit button in order to reset state later
            var originalSubmitButtonText = submitButton.html();

            // Disable submit button and re-label it
            submitButton.attr('disabled', 'disabled');
            submitButton.html('Submitting...');

            // Resets the submit to its original state
            var resetSubmit = function () {
                submitButton.removeAttr('disabled');
                submitButton.html(originalSubmitButtonText);
            }

            // Callback for success
            var onSuccess = function () {
                // Reset the form
                $(this).trigger("reset");

                // Reset the submit button state
                resetSubmit();

                // Open modal
                $modal.find('.modal-body p').html('Thanks for your message, we\'ll be in touch.');
                $modal.find('.modal-title').html('Success');
                $modal.modal('show');
            }

            // Callback for failure
            var onFail = function () {
                // Reset the submit button state
                resetSubmit();

                // Open modal
                $modal.find('.modal-body p').html('Sorry, something went wrong there. Rest assured, we\'ve logged the problem and will investigate it urgently.');
                $modal.find('.modal-title').html('Oh dear :(');
                $modal.modal('show');
            }

            // Post form asynchronously
            $.post(
                $(this).attr('action'), // Post to the forms action
                $(this).serialize() // Serialize the form data
            )
            .done(onSuccess)
            .fail(onFail);

            // Prevent default
            return false;
        });
    });


    // FORM VALIDATION????



    // Force a scroll event on page load
    window.scrollTo(0, 0);
});