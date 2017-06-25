$(function () {
    var defaultSection = "architectural";
    var galleryDetailsUrl = "/getgallerydetails";

    var $fotoramaDiv = $('#fotorama').fotorama();
    var fotorama = $fotoramaDiv.data('fotorama');

    // Function to load the gallery into the slider
    var loadGallery = function (section, callback) {
        var onSuccess = function (response) {
            fotorama.load(response);
            fotorama.show(0);
            if (callback)
                callback();
        }

        var url = galleryDetailsUrl + "?section=" + section;
        $.getJSON(url, onSuccess);
    }

    // Apply click handler to pills
    $('.nav-pills li a').click(function () {
        var $a = $(this)
        var section = $a.attr('section');

        // Callback for when the new gallery section is loaded
        var onLoad = function () {
            $a.closest('ul').find('li').removeClass('active');
            $a.closest('li').addClass('active');
        }

        loadGallery(section, onLoad);
        return false;
    });

    // Load gallery on page load
    loadGallery(defaultSection);
});