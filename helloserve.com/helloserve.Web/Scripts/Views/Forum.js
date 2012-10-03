var Forum = {
    MaintainForumPost: function (forum, category, topicID, postID) {
        if (typeof (postID) == "number") {
            Forum.postDialog = $('<div></div>').dialog({ autoOpen: false, minWidth: 1000, minHeight: 500, resizable: true, closeText: "" });
            Forum.postDialog.dialog('open');
            $.ajax({
                url: "/Forum/EditForumPost",
                type: "GET",
                data: { forum: forum, category: category, topicID: topicID, postID: postID },
                success: function (result) {
                    Forum.postDialog.html(result);
                    Forum.postDialog.dialog('open');
                }
            });
        }
        else {
            Forum.postDialog = $('<div></div>').dialog({ autoOpen: false, minWidth: 1000, minHeight: 500, resizable: true, closeText: "" });
            Forum.postDialog.dialog('open');
            $.ajax({
                url: "/Forum/CreateForumPost",
                type: "GET",
                data: { forum: forum, category: category, topicID: topicID },
                success: function (result) {
                    Forum.postDialog.html(result);
                    Forum.postDialog.dialog('open');
                }
            });
        }
    },
    PostForumPost: function () {
        var valid = $('#ForumMaintainForm').valid();
        if (!valid)
            return false;

        $.post('/Forum/PostForumPost', $('#ForumMaintainForm').serialize(), function (result) {
            if (result.IsError) {
                Forum.postDialog.html(result.Description);
            }
            else {
                Forum.postDialog.dialog('close');

                var forum = $('#TopicForumName').val();
                var category = $('#TopicCategoryName').val();
                var topicID = $('#TopicTopicID').val();
                var currentPage = $('#TopicCurrentPage').val();

                window.location = "/Forum/ForumTopic?forum=" + forum + "&category=" + category + "&topicID=" + topicID + "&page=" + currentPage;
            }

        });

        return false;
    },
    CancelForumPost: function () {
        Forum.postDialog.dialog('close');
    }
}