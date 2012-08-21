$(function () {
    Admin.LoadFeatureStuff();
    Admin.LoadNewsStuff();
});

var Admin = {
    CancelAction: function () {
        $('#adminAction').hide();
    },
    AddFeature: function () {
        $('#adminAction').load('/Admin/AddFeature', function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
            General.init();
        });
    },
    EditFeature: function (form) {
        var featureID = $('#AdminEditFeatureSelection').val();
        if (typeof (featureID) != "string")
            return false;

        $('#adminAction').load('/Admin/EditFeature', { id: featureID }, function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
            General.init();
        });
    },
    SaveFeature: function () {
        var isValid = $('#adminFeatureForm').valid();
        if (!isValid)
            return false;

        $.ajax({
            url: $('#adminFeatureForm').attr('action'),
            data: $('#adminFeatureForm').serialize(),
            type: 'POST',
            success: function (data) {
                if (data.IsError) {
                    $('#adminAction').html(data.Description);
                }
                else {
                    $('#adminAction').hide();
                    $('#adminFunctions').show();
                    $('#adminFunctions').load('/Admin/AdminFunctions');
                }
            }
        });
    },
    DeleteFeature: function () {
        var featureID = $('#AdminEditFeatureSelection').val();
        if (typeof (featureID) != "string")
            return false;

        $('#adminAction').load('/Admin/DeleteFeature', { id: featureID }, function () {
            $('#adminAction').hide();
            $('#adminFunctions').load('/Admin/AdminFunctions');
        });
    },
    LoadFeatureStuff: function () {
        var featureID = $('#AdminEditFeatureSelection').val();
        if (typeof (featureID) != "string")
            return false;

        $('#adminFunctionsFeatureBlogs').load('/Admin/LoadFeatureBlogPosts', { id: featureID });
    },
    AddBlogPost: function () {
        var featureID = $('#AdminEditFeatureSelection').val();
        if (typeof (featureID) != "string")
            return false;

        $('#adminAction').load('/Admin/AddBlogpost', { id: featureID }, function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
            General.init();
        });
    },
    AddNews: function () {
        $('#adminAction').load('/Admin/AddNews', function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
            General.init();
        });
    },
    EditNews: function () {
        var newsID = $('#AdminEditNewsSelection').val();
        if (typeof (newsID) != "string")
            return false;

        $('#adminAction').load('/Admin/EditNews', { id: newsID }, function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
            General.init();
        });
    },
    DeleteNews: function () {
        var newsID = $('#AdminEditNewsSelection').val();
        if (typeof (newsID) != "string")
            return false;

        $('#adminAction').load('/Admin/DeleteNews', { id: newsID }, function () {
            $('#adminAction').hide();
            $('#adminFunctions').load('/Admin/AdminFunctions');
        });
    },
    LoadNewsStuff: function () {
        var newsID = $('#AdminEditNewsSelection').val();
        if (typeof (newsID) != "string")
            return false;
    },
    EditRequirements: function () {
        $('#adminAction').load('/Admin/EditRequirements', function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
        });
    },
    AddRequirement: function () {
        var descr = $('#req_add_description').val();
        var link = $('#req_add_link').val();
        var req = JSON.stringify({
            RequirementID: null,
            Description: descr,
            Link: link,
            Icon: null
        });

        $.ajax({
            url: '/Admin/AddRequirement/',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: req,
            success: function (result) {
                $('#adminAction').html(result);
            }
        });
    },
    RemoveRequirement: function (id) {
        $('#adminAction').load('/Admin/RemoveRequirement/', { id: id });
    },
    AttachRequirement: function (featureID) {
        var reqID = $('#RequirementSelection').val();
        $('#featureRequirements').load('/Admin/AttachRequirement', { featureID: featureID, id: reqID });
    },
    DetachRequirement: function (featureid, id) {
        $('#featureRequirements').load('/Admin/DetachRequirement', { featureID: featureid, id: id });
    },
    SaveRequirements: function () {
        var requirements = [];
        $($.find('.requirementRow')).each(function () {
            var requirementID = $(this).attr('id').replace('requirement_', '');
            var descr = $('#req_' + requirementID + '_description').val();
            var link = $('#req_' + requirementID + '_link').val();
            var req = {
                RequirementID: parseInt(requirementID),
                Description: descr,
                Link: link,
                Icon: null
            };
            requirements.push(req);
        });
        $.ajax({
            url: '/Admin/SaveRequirements/',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(requirements),
            success: function (result) {
                $('#adminAction').html(result);
            }
        });
        return false;
    },
    AddRelated: function (featureID) {
        var descr = $('#related_add_description').val();
        var link = $('#related_add_link').val();
        var icon = $('#related_add_icon').val();
        var rel = { RelatedLinkID: 0,
            FeatureID: featureID,
            Description: descr,
            Link: link,
            Icon: icon
        }

        $.ajax({
            url: '/Admin/AddRelated',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(rel),
            success: function (result) {
                $('#featureRelated').html(result);
            }
        });
    },
    RemoveRelated: function (featureID, id) {
        $('#featureRelated').load('/Admin/RemoveRelated', { featureID: featureID, id: id });
    },
    SaveRelated: function (featureID) {
        var relateds = [];
        $($.find('.relatedRow')).each(function () {
            var relatedID = $(this).attr('id').replace('related_', '');
            var descr = $('#related_' + relatedID + '_description').val();
            var link = $('#related_' + relatedID + '_link').val();
            var icon = $('#related_' + relatedID + '_icon').val();
            var rel = { RelatedLinkID: parseInt(relatedID),
                FeatureID: featureID,
                Description: descr,
                Link: link,
                Icon: icon
            };
            relateds.push(rel);
        });

        $.ajax({
            url: '/Admin/SaveRelated',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(relateds),
            success: function (result) {
                $('#featureRelated').html(result);
            }
        });
    }
}