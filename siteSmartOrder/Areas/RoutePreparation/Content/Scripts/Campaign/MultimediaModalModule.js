
function multimediaModalModuleFunct($, window) {
    var multimediaModalModule = {};
    var element = $('multimedia-modal');
    var json = element.attr('multimedias');
    var multimediaModal = element.find('#multimediaModal');
    var multimediaList = multimediaModal.find('ul.multimedias');
    var errorMessage = element.find('.error-message');
    var multimedias = $.parseJSON(json);

    multimediaModalModule.multimedias = [];

    function getMultimedia(id) {
        return $.grep(multimediaModalModule.multimedias, get)[0];

        function get(multimedia) {
            return multimedia.Id === id;
        }
    }

    function changeMultimedia(id, value) {
        var multimedia = getMultimedia(id);
        if (!multimedia) return;
        multimedia.domElement.prop('checked', value);    
    }

    multimediaModalModule.showCatalog = function () {
        multimediaModal.modal('show');
    };

    multimediaModalModule.checkMultimedia = function (id) {
        changeMultimedia(id, true);
    };

    multimediaModalModule.uncheckMultimedia = function (id) {
        changeMultimedia(id, false);
    };

    multimediaModalModule.showErrorMessage = function (message) {
        errorMessage.html(message);
        errorMessage.show();
    };

    multimediaModalModule.hideErrorMessage = function () {
        errorMessage.hide();
        errorMessage.html('');
    };

    multimediaModalModule.filterByType = function (type) {
        $.each(multimediaModalModule.multimedias, function (index, multimedia) {
            var parent = multimedia.domElement.parents('li.multimedia');

            if (multimedia.MultimediaType === type) {
                multimedia.isVisible = true;
                parent.show();
            } else {
                multimedia.isVisible = false;
                parent.hide();            
            }
        });
    }

    multimediaModalModule.moveToBeginning = function (id) {
        var multimedia = getMultimedia(id);

        if (!multimedia) return;

        var parent = multimedia.domElement.parents('li.multimedia');

        multimediaList.prepend(parent);
    };

    multimediaModalModule.addMultimediaToCatalog = function (multimedia) {
        var thumbnail = multimedia.IsImage
                ? '<img src="' + multimedia.Path + '"/>'
                : '<i class="fa fa-film"></i>'

        var li = $('<li class="multimedia"></li>')
            .append('<button type="button" class="remove-button hide"><i class="fa fa-times-circle"></i></button>' +
                    '<input type="checkbox" />' +
                    '<a href="#">' +
                        thumbnail +
                        '<p>' + multimedia.Name + '</p>' +
                    '</a>');

        var checkbox = li.find('input[type="checkbox"]');
        var anchor = li.find('a');
        var button = li.find('.remove-button');
        
        anchor.on('click', showLightBox);
        button.on('click', removeMultimedia);

        element.find('.multimedias').append(li);
        multimedia.domElement = checkbox;
        multimediaModalModule.multimedias.push(multimedia);

        function showLightBox(event) {
            event.preventDefault();
            var multimediaLightBox = $('<div id="multimediaLightBox"></div>');
            var mediaElement = createMediaElement();
            var closeButton = $('<button type="button" class="close">&times;</button>');
            var body = $('body');
            var bodyOverflow = $('body').css('overflow');

            mediaElement.addClass('media-element');
            closeButton.on('click', closeMultimediaLightBox);

            multimediaLightBox
                .append(mediaElement)
                .append(closeButton);
            body.append(multimediaLightBox);
            body.css('overflow', 'hidden');

            function createMediaElement() {
                return multimedia.IsImage ? createImageElement() : createVideoElement();

                function createImageElement() {
                    return $('<img/>', {
                        src: multimedia.Path
                    });
                }

                function createVideoElement() {
                    return $('<video></video>', {
                        id: 'multimediaLightBoxVideo',
                        src: multimedia.Path,
                        type: 'video/mp4',
                        controls: true,
                        autoplay: true
                    });
                }
            }

            function closeMultimediaLightBox() {
                disposeVideo();

                multimediaLightBox.remove();
                body.css('overflow', bodyOverflow);

                function disposeVideo() {
                    var video = document.getElementById('multimediaLightBoxVideo');

                    if (!video) return;

                    video.pause();
                    video.src = '';
                    video.load();
                }
            }
        }

        function removeMultimedia() {
            $.ajax({
                url: '/Portal/RoutePreparation/Campaign/DeleteMultimedia/' + multimedia.Id,
                type: 'DELETE'
            }).done(onDone);

            function onDone() {
                multimedia.domElement.parents('li.multimedia').remove();
                deleteFromCampaign();

                for (var index = 0; index < multimediaModalModule.multimedias.length; index++) {
                    var currentMultimedia = multimediaModalModule.multimedias[index];

                    if (currentMultimedia.Id === multimedia.Id) {
                        multimediaModalModule.multimedias.splice(index, 1);
                        break;
                    }
                }

                function deleteFromCampaign() {
                    var campaignMultimedia = getFromCampaign(multimedia);
                    if (!campaignMultimedia) return;
                    MultimediaModule.removeMultimediaFromCampaign(campaignMultimedia);
                }
            }
        }
    };

    $.each(multimedias, function (index, multimedia) {        
        multimediaModalModule.addMultimediaToCatalog(multimedia);
    });

    multimediaModal.on('hidden', multimediaModalModule.hideErrorMessage);

    multimediaModal.modal({
        backdrop: 'static',
        show: false
    }); 

    window.MultimediaModalModule = multimediaModalModule;
}

$(document).ready(function () {
    multimediaModalModuleFunct(jQuery, window);
});