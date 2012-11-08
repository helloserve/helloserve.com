$(function () {
    $.tweets = [];
    Home.TweetFeed();
});

var Home = {
    DivHover: function (element) {
        $(element).removeClass('normal');
        $(element).addClass('hover');
    },
    DivNormal: function (element) {
        $(element).removeClass('hover');
        $(element).addClass('normal');
    },
    FeatureClick: function (featureID) {
        if (typeof (featureID) == 'number')
            window.location.href = '/Feature/Feature/' + featureID;

        if (typeof (featureID) == 'string')
            window.location.href = featureID;

        return false;
    },
    NewsClick: function (newsID) {
        if (typeof (newsID) != 'number')
            return false;

        window.location.href = '/News/News/' + newsID;
    },
    TweetFeed: function () {
        $.getJSON("https://api.twitter.com/1/statuses/user_timeline/helloserve.json?callback=?&count=10", function (data) {
            $.tweets = data;
            Home.DisplayTweets();
        });
    },
    DisplayTweets: function () {
        if ($.tweets.length == 0)
            return;

        var div = $('<div></div>');

        for (var i = 0; i < $.tweets.length; i++) {
            div.append('<div class="tweet">' + $.tweets[i].text.parseURL().parseUsername().parseHashtag() + '</div>');
        }

        $('#twitterFeed').html(div);
    },
};

//Twitter Parsers
String.prototype.parseURL = function() {
    return this.replace(/[A-Za-z]+:\/\/[A-Za-z0-9-_]+\.[A-Za-z0-9-_:%&~\?\/.=]+/g, function(url) {
        return url.link(url);
    });
};
String.prototype.parseUsername = function() {
    return this.replace(/[@]+[A-Za-z0-9-_]+/g, function(u) {
        var username = u.replace("@","")
        return u.link("http://twitter.com/"+username);
    });
};
String.prototype.parseHashtag = function() {
    return this.replace(/[#]+[A-Za-z0-9-_]+/g, function(t) {
        var tag = t.replace("#","%23")
        return t.link("http://search.twitter.com/search?q="+tag);
    });
};
function parseDate(str) {
    var v=str.split(' ');
    return new Date(Date.parse(v[1]+" "+v[2]+", "+v[5]+" "+v[3]+" UTC"));
}
