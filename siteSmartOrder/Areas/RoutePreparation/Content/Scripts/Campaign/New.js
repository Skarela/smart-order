
    validateNewCampaign();

function validateNewCampaign() {
    $("#Form").validateForm({
        Name: true,
        Description: true,
        ValidDates: true,
        ValidSurvey: true,
        ApplyDialog: true,
        AjaxSubmit: true,
        Action: campaign_Create,
        New: campaign_New,
        Index: campaign_Index
    });
}

