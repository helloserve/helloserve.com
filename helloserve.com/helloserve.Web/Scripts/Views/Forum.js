var Forum = {
    MaintainForumPost: function (forum, category, topicID, postID) {
        if (typeof (postID) == "number") {
            Forum.postDialog.dialog('open');
            $.ajax({
                url: "/Forum/EditForumPost",
                type: "GET",
                data: { forum: forum, category: category, topicID: topicID, postID: postID },
                success: function (result) {
                    $('#dlgcontent').html(result);
                    $('#dlg').dialog({ width: 1000, height: 510, closeText: "", resizable: false });
                }
            });
        }
        else {
            $.ajax({
                url: "/Forum/CreateForumPost",
                type: "GET",
                data: { forum: forum, category: category, topicID: topicID },
                success: function (result) {
                    $('#dlgcontent').html(result);
                    $('#dlg').dialog({ width: 1000, height: 510, closeText: "", resizable: false });
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
                $('#dlg').dialog('close');

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
        $('#dlg').dialog('close');
    }
}