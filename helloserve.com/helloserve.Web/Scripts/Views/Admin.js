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
            var descr = $('.req_description', this).val();
            var link = $('.req_link', this).val();
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
    EditDownloads: function () {
        $('#adminAction').load('/Admin/EditDownloads', function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
        });
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
            var descr = $('.related_description', this).val();
            var link = $('.related_link', this).val();
            var icon = $('.related_icon', this).val();
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
    },
    ScanMedia: function () {
        var $dialog = $('<div style="background-colour:#FFF; border:solid 1px; text-align:center;vertial-align:middle"></div>').html("Scanning Media...").dialog({ autoOpen: false });
        $dialog.dialog('open');
        $.ajax({
            url: "/Admin/ScanMedia",
            type: "GET",
            success: function (result) {
                $dialog.dialog('close');
                $dialog.html(result.Description);
                $dialog.dialog('open');
            }
        });
    },
    MaintainMedia: function (elemID, folder) {
        $(elemID).load('/Admin/MaintainMedia', { folder: folder }, function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
        });
    },
    RefreshMedia: function (elemID, folder) {
        $(elemID).load('/Admin/RefreshMedia', { folder: folder });
    },
    RemoveMedia: function (id, path) {
        $('#adminAction').load('/Admin/RemoveMedia', { id: id, path: path });
    },
    AddForum: function () {
        $('#adminAction').load('/Admin/AddForum', function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
        });
    },
    EditForum: function () {
        var forumID = $('#AdminEditForumSelection').val();
        if (typeof (forumID) != "string")
            return false;

        $('#adminAction').load('/Admin/EditForum', { id: forumID }, function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
        });
    },
    SaveForum: function () {
        var isValid = $('#adminForumForm').valid();
        if (!isValid)
            return false;

        $.ajax({
            url: $('#adminForumForm').attr('action'),
            data: $('#adminForumForm').serialize(),
            type: 'POST',
            success: function (data) {
                if (data.IsError) {
                    $('#adminAction').html(data.Description);
                }
                else {
                    $('#adminAction').html(data.Description);
                }
            }
        });

        return false;
    },
    DeleteForum: function () {
        var forumID = $('#AdminEditForumSelection').val();
        if (typeof (forumID) != "string")
            return false;

        $('#adminAction').load('/Admin/DeleteForum', { id: forumID }, function () {
            $('#adminAction').hide();
            $('#adminFunctions').load('/Admin/AdminFunctions');
        });
    },
    AddForumCategory: function () {
        var forumID = $('#Forum_ForumID').val();

        $('#forumCategoryAction').load('/Admin/AddForumCategory/', { forum: forumID }, function () {
            $('#forumCategoryFunctions').hide();
            $('#forumCategoryAction').show();
        });
    },
    EditForumCategory: function () {
        var forumID = $('#Forum_ForumID').val();
        var categoryID = $('#AdminForumCategorySelection').val();
        if (typeof (categoryID) != "string")
            return false;

        $('#forumCategoryAction').load('/Admin/EditForumCategory/', { forum: forumID, id: categoryID }, function () {
            $('#forumCategoryFunctions').hide();
            $('#forumCategoryAction').show();
        });
    },
    SaveForumCategory: function () {
        var isValid = $('#adminForumCategoryForm').valid();
        if (!isValid)
            return false;

        var forumID = $('#Forum_ForumID').val();

        $.ajax({
            url: $('#adminForumCategoryForm').attr('action'),
            data: $('#adminForumCategoryForm').serialize(),
            type: 'POST',
            success: function (data) {
                if (data.IsError) {
                    $('#forumCategoryAction').html(data.Description);
                }
                else {
                    $('#adminAction').load('/Admin/EditForum', { id: forumID });
                }
            }
        });

        return false;
    },
    DeleteForumCategory: function () {
        var forumID = $('#Forum_ForumID').val();
        var categoryID = $('#AdminForumCategorySelection').val();
        if (typeof (categoryID) != "string")
            return false;

        $('#forumCategoryFunctions').load('/Admin/DeleteForumCategory/', { forum: forumID, id: categoryID }, function () {
            $('#adminAction').load('/Admin/EditForum', { id: forumID });
        });
    },
    CancelForumCategory: function () {
        $('#forumCategoryAction').hide();
        $('#forumCategoryFunctions').show();

        return false;
    },
    Users: function () {
        $('#adminAction').load('/Admin/Users/', function () {
            $('#adminFunctions').hide();
            $('#adminAction').show();
        });
    },
    DeleteUser: function (userid) {
        $('#adminAction').load('/Admin/DeleteUser/', { id: userid });
        return false;
    },
    SendActivation: function (userid) {
        $('#adminAction').load('/Admin/SendActivation/', { id: userid });
        return false;
    },
    SystemLog: function () {
        $.post('/Admin/SystemLog/', null, function (result) {
            $('#adminFunctions').hide();
            $('#adminAction').html(result);
            $('#adminAction').show();
            General.init();
        });
    },
    FilterSystemLog: function () {
        var filterDate = $('#Log_FilterDate').val();
        var filterCat = $('#Log_Category').val();
        var filterUser = $('#Log_FilterUser').val();
        var filterMsg = $('#Log_FilterMessage').val();
        var filterSrc = $('#Log_FilterSource').val();

        var data = { Date: filterDate, Category: filterCat, UserID: filterUser, Message: filterMsg, Source: filterSrc };

        $.post('/Admin/SystemLog/', data, function (result) {
            $('#adminAction').html(result);
            General.init();
        });
    },
    LinkDownloadable: function (featureID) {
        var dlID = $('#featureAvailableDownloadables').val();

        $('#featureDownloadables').load('/Admin/LinkDownloadable', { id: dlID, featureID: featureID });

        return false;
    },
    UnlinkDownloadable: function (id, featureID) {
        $('#featureDownloadables').load('/Admin/UnlinkDownloadable', { id: id, featureID: featureID });

        return false;
    }
}