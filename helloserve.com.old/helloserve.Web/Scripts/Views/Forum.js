$(function() {
    var offset = $('.forumPostAnchor').offset();
    var scrollto = offset.top - 50; // fixed_top_bar_height = 50px
    $('html, body').animate({ scrollTop: scrollto }, 0);
});

var Forum = {
    Posting: false,
    PostingID: null,
    MaintainForumPost: function (forum, category, topicID, postID) {
        if (Forum.Posting)
            Forum.CancelForumPost(Forum.PostingID);

        Forum.Posting = true;
        Forum.PostingID = postID;

        if (typeof (postID) == "number") {
            //Forum.postDialog.dialog('open');
            $.ajax({
                url: "/Forum/EditForumPost",
                type: "GET",
                data: { forum: forum, category: category, topicID: topicID, postID: postID },
                success: function (result) {
                    //$('#dlgcontent').html(result);
                    //$('#dlg').dialog({ width: 1000, height: 510, closeText: "", resizable: false });
                    var container = "#Post_" + postID;
                    $(container).html(result);
                    $('#forumPostAccess').hide();
                }
            });
        }
        else {
            $.ajax({
                url: "/Forum/CreateForumPost",
                type: "GET",
                data: { forum: forum, category: category, topicID: topicID },
                success: function (result) {
                    //$('#dlgcontent').html(result);
                    //$('#dlg').dialog({ width: 1000, height: 510, closeText: "", resizable: false });
                    $('#forumPost').html(result);
                    $('#forumPostAccess').hide();
                }
            });
        }
    },
    DeleteForumPost: function (forum, category, topicID, postID) {
        $.get('/Forum/DeleteForumPost', { forum: forum, category: category, topicID: topicID, postID: postID }, function (result) {
            if (result.IsError) {

            }
            else {
                var container = "#Post_" + postID;
                $(container).remove();
            }
        });
    },
    PostForumPost: function (postID) {
        var valid = $('#ForumMaintainForm').valid();
        if (!valid)
            return false;

        $.post('/Forum/PostForumPost', $('#ForumMaintainForm').serialize(), function (result) {
            if (result.IsError) {
                $('#dlg-content').html(result.Description);
                $('#dlg').dialog('open');
            }
            else {
                //$('#dlg').dialog('close');
                Forum.Posting = false;

                if (typeof (postID) == "number") {
                    var container = "#Post_" + postID;
                    $(container).html(result);
                    $('#forumPostAccess').show();
                }
                else {
                    //                    var forum = $('#TopicForumName').val();
                    //                    var category = $('#TopicCategoryName').val();
                    //                    var topicID = $('#TopicTopicID').val();
                    //                    var currentPage = $('#TopicCurrentPage').val();

                    //                    $('#forumPost').html("");
                    //                    $('#forumPostAccess').show();

                    //                    window.location = "/Forum/ForumTopic?forum=" + forum + "&category=" + category + "&topicID=" + topicID + "&page=" + currentPage;

                    postID = $(result).filter(':input#PostID').val();
                    var postDiv = $('<div class="forumTopicPost" id="Post_' + postID + '"></div>');
                    postDiv.html(result);
                    $('#forumTopicPosts').append(postDiv);
                    Forum.CancelForumPost();
                }
            }

        });

        return false;
    },
    CancelForumPost: function (forum, category, topicID, postID) {
        //$('#dlg').dialog('close');
        Forum.Posting = false;
        if (typeof (postID) == "number") {
            var container = "#Post_" + postID;

            $(container).load('/Forum/LoadForumPost', { forum: forum, category: category, topicID: topicID, postID: postID });
        }
        else {
            $('#forumPost').html("");
        }

        $('#forumPostAccess').show();
    }
}