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
    }
};

