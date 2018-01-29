eventToClickCollapsible();
ShowModalGallery();

function eventToClickCollapsible() {
    $('.collapsible .panel-title').on('click', function () {
        var $this = $(this).closest('.panel').find('.collapse-button');
        if (!$this.hasClass('panel-collapsed')) {
            $this.closest('.panel').find('.panel-body').slideUp();
            $this.addClass('panel-collapsed');
            $this.find('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');
        } else {
            $this.closest('.panel').find('.panel-body').slideDown();
            $this.removeClass('panel-collapsed');
            $this.find('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');
        }
    });
}

