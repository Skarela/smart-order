function multimediaModalFilterFunct($, window) {
    var multimediaModalFilter = {};
    var parentElement = $('multimedia-modal');
    var element = $('multimedia-modal-filter');
    var multimediaModal = parentElement.find('#multimediaModal');
    var searchBox = element.find('#searchBox');

    function reset() {
        searchBox.val('');
        multimediaModalFilter.search();
    }

    multimediaModalFilter.search = function () {
        var inputValue = searchBox.val().toUpperCase();
        $.each(MultimediaModalModule.multimedias, function (index, multimedia) {
            if (!multimedia.isVisible) return;
            var name = multimedia.Name.toUpperCase();
            var includeValue = name.includes(inputValue);
            var parent = multimedia.domElement.parents('li.multimedia');

            if (includeValue)
                parent.show();
            else
                parent.hide();
        });
    };

    multimediaModal.on('hidden', reset);

    window.MultimediaModalFilter = multimediaModalFilter;
}
$(document).ready(function () {
    multimediaModalFilterFunct(jQuery, window);
});