function multimediaFileUploaderFunct($, window) {
    var multimediaFileUploader = {};
    var parentElement = $('multimedia');
    var element = $('multimedia-file-uploader');
    var typeSelector = parentElement.find('#CampaignMultimediaType');    
    var uploadButton = element.find('#btn_uploadMultimedia');    
    var fileInput = element.find('#Files');
    var errorMessage = element.find('.error-message');

    function uploadMultimedia() {
        fileInput.trigger('click');
    }

    function changeType() {
        var type = MultimediaModule.getMediaType();

        if (type === MultimediaModule.images.value) {
            fileInput.attr("accept", MultimediaModule.images.allowedTypes);
            fileInput.attr("multiple", "multiple");
        } else {
            fileInput.attr("accept", MultimediaModule.videos.allowedTypes);
            fileInput.removeAttr("multiple");
        }
    }

    function onFilesChanged() {
        MultimediaModule.removeCreatedMultimedias();

        var files = this.files;        
        var availableMultimediaSpace = MultimediaModule.getAvailableMultimediaSpace();

        if (!files || files.length === 0) {
            uploadButton.show();
            return;
        }

        if (files.length > availableMultimediaSpace) {
            errorMessage.html(MultimediaModule.errors.limitReached);
            errorMessage.show();
            uploadButton.show();
            this.value = null;
            return;
        }     

        $.each(files, function (index, file) {
            var reader = new FileReader();
            reader.onload = onload;
            reader.readAsDataURL(file);

            function onload() {
                MultimediaModule.addMultimediaToCampaign({
                    Id: 0,
                    Name: file.name,
                    MultimediaType: MultimediaModule.mediaType                  
                });
            }
        });
                                       
        errorMessage.hide();
        errorMessage.html('');
    }

    uploadButton.on('click', uploadMultimedia);    
    fileInput.on('change', onFilesChanged);
    typeSelector.on('change', changeType);  

    changeType();

    window.MultimediaFileUploader = multimediaFileUploader;     
}

$(document).ready(function () {
    multimediaFileUploaderFunct(jQuery, window);
});