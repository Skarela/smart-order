
function multimediaModuleFunct($, window) {
    var multimediaModule = {
        images: {
            value: 1,
            limit: 3,
            allowedTypes: "image/jpg, image/jpeg, image/gif, image/bmp, image/png"
        },
        videos: {
            value: 2,
            limit: 1,
            allowedTypes: "video/mp4"
        }
    };

    multimediaModule.errors = {
        limitReached: 'Limite de imágenes(' + multimediaModule.images.limit + ') o videos(' + multimediaModule.videos.limit + ') superado.'
    };

    var states = {
        created: 'created',
        added: 'added',
        removed: 'removed',
        unchanged: 'unchanged'
    };
    var maxMultimediaName = 25;
    var element = $('multimedia');
    var typeSelector = element.find('#CampaignMultimediaType');
    var previewMultimedia = element.find("#previewMultimedia");    
    var json = element.attr('campaign-multimedias');
    var campaignMultimedias = $.parseJSON(json);

    multimediaModule.campaignMultimedias = [];

    function updateState(multimedia, state) {
        var domElement = multimedia.domElement;
        var stateInput = domElement.find('input[type="hidden"][name$=".State"]');
        stateInput.val(state);
        multimedia.state = state;
    }

    function renameHiddenInputs() {
        $.each(multimediaModule.campaignMultimedias, function (index, campaignMultimedia) {
            var hiddenInputs = campaignMultimedia.domElement.find('input[name^="CampaignMultimedias"]');
            $.each(hiddenInputs, function (innerIndex, input) {
                var name = $(input).attr('name');
                name = name.replace(/\[.*?\]/g, '[' + index + ']');
                $(input).attr('name', name);
            });
        });
    }

    multimediaModule.getAvailableMultimediaSpace = function () {
        var activeMultimedias = $.grep(multimediaModule.campaignMultimedias, function (multimedia) {
            return multimedia.state !== states.removed && 
                multimedia.MultimediaType === multimediaModule.mediaType;
        });

        if(multimediaModule.mediaType === multimediaModule.images.value)
            return multimediaModule.images.limit - activeMultimedias.length;
        else
            return multimediaModule.videos.limit - activeMultimedias.length;
    }

    multimediaModule.getCampaignMultimedia = function (id) {
        return $.grep(multimediaModule.campaignMultimedias, getMultimedia)[0];

        function getMultimedia(multimedia) {
            return multimedia.Id === id;
        }
    };

    multimediaModule.addMultimediaToCampaign = function (multimedia) {
        var multimediasToRemove = $.grep(multimediaModule.campaignMultimedias, function (multimedia) {
            return multimedia.MultimediaType !== multimediaModule.mediaType;
        });

        $.each(multimediasToRemove, function (index, multimediaToRemove) {
            multimediaModule.removeMultimediaFromCampaign(multimediaToRemove);
        });

        if (multimedia.Id === 0) {
            multimedia.state = states.created;
            render(multimedia);
        }
        else {
            addOrUpdate();
        }

        function addOrUpdate() {
            var campaignMultimedia = multimediaModule.getCampaignMultimedia(multimedia.Id);

            if (!campaignMultimedia) {
                campaignMultimedia = multimedia.state === states.unchanged
                    ? $.extend({}, multimedia)
                    : $.extend({ state: states.added }, multimedia);
                render(campaignMultimedia);
            } else {
                changeState();
            }

            function changeState() {
                if (campaignMultimedia.state === states.removed) {
                    if (multimediaModule.getAvailableMultimediaSpace() > 0) {
                        updateState(states.unchanged);
                        campaignMultimedia.domElement.show();
                    }
                    else {
                        MultimediaModalModule.uncheckMultimedia(multimedia.Id);
                        multimediaModalModule.showErrorMessage(multimediaModule.errors.limitReached);
                    }
                }
            }
        }

        function render(multimedia) {
            var row = getRowElement(multimedia);

            multimedia.domElement = row;
            multimediaModule.campaignMultimedias.push(multimedia);
            previewMultimedia.append(row);
            previewMultimedia.show();
            renameHiddenInputs();
        }

        function getRowElement(multimedia) {
            var name = getName();
            var group = getGroupTemplate();

            return $('<div class="row campaign-multimedia"></div>')
            	.append('<div class="span4">' +
            		'<span class="label label-default" style="margin:5px;">' + name + '</span>' +
            		'</div>')
            	.append('<div class="span6">' +
            		'<div class="form-group">' +
            		group +
            		'<input type="hidden" name="CampaignMultimedias[].Id" value="' + multimedia.Id + '"/>' +
            		'<input type="hidden" name="CampaignMultimedias[].State" value="' + multimedia.state + '"/>' +
            		'<input type="hidden" name="CampaignMultimedias[].FileUploadedName" value="' + multimedia.Name + '"/>' +
            		'</div>' +
            		'</div>');

            function getName() {
                return multimedia.Name.length > maxMultimediaName
			 ? getTruncatedName()
			 : multimedia.Name;
            }

            function getTruncatedName() {
                var nameParts = multimedia.Name.split('.');
                var extension = nameParts[nameParts.length - 1];
                return multimedia.Name.substring(0, maxMultimediaName) + "... " + extension;
            }

            function getGroupTemplate() {
                return multimedia.state === states.created
            		? '<input type="hidden" name="CampaignMultimedias[].Group" placeholder="Agrupador" style="margin-bottom: 5px;"/>'
            		: '<span class="hide">' + (multimedia.Group || '') + '</span>';
            }
        }
    };

    multimediaModule.removeMultimediaFromCampaign = function (multimedia) {
        var campaignMultimedia = multimediaModule.getCampaignMultimedia(multimedia.Id);

        if (campaignMultimedia.state === states.unchanged) {
            updateState(campaignMultimedia, states.removed);
            campaignMultimedia.domElement.hide();
        }

        if (campaignMultimedia.state === states.added || campaignMultimedia.state === states.created) {
            campaignMultimedia.domElement.remove();

            for (var index = 0; index < multimediaModule.campaignMultimedias.length; index++) {
                var currentMultimedia = multimediaModule.campaignMultimedias[index];

                if (currentMultimedia.Id === campaignMultimedia.Id) {
                    multimediaModule.campaignMultimedias.splice(index, 1);
                    break;
                }
            }

            renameHiddenInputs();
        }

        MultimediaModalModule.hideErrorMessage();
    };

    multimediaModule.removeCreatedMultimedias = function () {
        var createdMultimedias = $.grep(multimediaModule.campaignMultimedias, function (multimedia) {
            return multimedia.state === states.created;
        });

        $.each(createdMultimedias, function (index, multimedia) {
            multimediaModule.removeMultimediaFromCampaign(multimedia);
        });
    };

    multimediaModule.getMediaType = function () {
        var value = typeSelector.val();
        return parseInt(value);
    };

    typeSelector.on('change', function () {
        multimediaModule.mediaType = multimediaModule.getMediaType();
        MultimediaModalModule.filterByType(multimediaModule.mediaType);
    });

    multimediaModule.mediaType = multimediaModule.getMediaType();
    MultimediaModalModule.filterByType(multimediaModule.mediaType);

    $.each(MultimediaModalModule.multimedias, function (index, multimedia) {
        multimedia.domElement.on('change', onMultimediaChange);

        function onMultimediaChange() {
            if (this.checked)
                tryToAdd();
            else
                multimediaModule.removeMultimediaFromCampaign(multimedia);

            function tryToAdd() {
                if (multimediaModule.getAvailableMultimediaSpace() > 0) {
                    multimediaModule.addMultimediaToCampaign(multimedia);                    
                }
                else {
                    MultimediaModalModule.uncheckMultimedia(multimedia.Id);
                    MultimediaModalModule.showErrorMessage(multimediaModule.errors.limitReached);
                }
            }
        }
    });

    $.each(campaignMultimedias, function (index, multimedia) {
        multimedia.state = states.unchanged;
        multimediaModule.addMultimediaToCampaign(multimedia);
        MultimediaModalModule.checkMultimedia(multimedia.Id);
        MultimediaModalModule.moveToBeginning(multimedia.Id);
    });

    window.MultimediaModule = multimediaModule;
}

$(document).ready(function() {
    multimediaModuleFunct(jQuery, window);
});
