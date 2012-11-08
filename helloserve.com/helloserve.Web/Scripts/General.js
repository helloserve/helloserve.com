$(function () {
    General.init();
});

var General = {
    init: function () {
        DatePicker.toDatePicker($('.datePicker'), null);
        DatePicker.toDatePickerForDateRange($("#StartDate"), $("#EndDate"), {}, {});
        DatePicker.toDatePickerForDateRange($("#DateFrom"), $("#DateTo"), {}, {});

        //CKEditorSetup.loadFCK(1);
    }
};


/* Date Picker
-------------------------------------*/

var DatePicker = {

    //This function works for date ranges, like booking for leave.
    toDatePickerForDateRange: function (elemFrom, elemTo, optionsFrom, optionsTo) {

        var defaultsFrom = {
            maxDate: elemTo.val(),
            onSelect: function (selectedDate) {
                elemTo.datepicker("option", 'minDate', selectedDate);
            }
        };

        var defaultsTo = {
            minDate: elemFrom.val(),
            onSelect: function (selectedDate) {
                elemFrom.datepicker("option", 'maxDate', selectedDate);
            }
        };

        //set the FROM element to a datePicker
        var settings = $.extend(defaultsFrom, optionsFrom);
        DatePicker.toDatePicker(elemFrom, settings);

        //set the TO element to a datePicker
        settings = $.extend(defaultsTo, optionsTo);
        DatePicker.toDatePicker(elemTo, settings);

    },

    toDatePicker: function (elem, options) {

        var settings = $.extend(DatePicker.getDatePickerDefaults(), options);
        elem.datepicker(settings);

        elem.addClass('datePicker');
    },

    getDatePickerDefaults: function () {

        //debbie requested that all datepickers open up in the month of the membergroup's fiscal period
        var dateToShow = new Date($('input#FiscalPeriod').val());

        var defaults =
        {
            defaultDate: dateToShow,
            numberOfMonths: 1,
            changeYear: true,
            changeMonth: true,
            dateFormat: 'yy-mm-dd',
            speed: "",
            showAnim: 'slideDown',
            yearRange: "c-65:c",
            onChangeMonthYear: function (year, month, inst) {
                var thisDatePicker = "#" + $(this).attr('id');
                var currentdate = $(thisDatePicker).val();

                //get the Month in the same format (04 vs. 4) -- cant not get length of int
                if (month.toString().length === 1) {
                    month = "0" + month;
                }

                //format 2011-01-01
                var spiltdate = currentdate.split('-');
                currentDay = spiltdate[2];
                currentMonth = spiltdate[1];
                currentYear = spiltdate[0];

                var newDay = currentDay;
                var newMonth = (month != currentMonth) ? month : currentMonth;
                var newYear = (year != currentYear) ? year : currentYear;
                var newDate = newYear + "-" + newMonth + "-" + newDay;

                $(thisDatePicker).val(newDate);

            }
        }

        return defaults;

    }

};



/* ------ Rich Text Editor ------- */
/*
window.editors = new Array();

var CKEditorSetup = {

    loadFCK: function (height) {

        $('.crudFCK:visible').each(function () {
            var h = (!height ? 400 : height);
            if (h < 400)
                height = "400";
            else
                height = height - 930;

            var instance = CKEDITOR.instances[this.id];
            if (instance) {
                CKEDITOR.remove(instance);
            }
            CKEDITOR.BasePath = "/Content/ckeditor/";
            CKEDITOR.disableNativeSpellChecker = true;
            CKEDITOR.config.scayt_autoStartup = false;
            CKEDITOR.config.toolbar_Full =
            [
                ['NewPage', 'Preview', '-', 'Templates'],
            //['SpellChecker', 'Scayt'],
                ['Bold', 'Italic', 'Underline'],
                ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent'],
                ['Source'],
                '/',
                ['Subscript', 'Superscript'],
                ['Styles', 'Format', 'Font', 'FontSize'],
                ['TextColor', 'BGColor', '-', 'SpecialChar', 'Smiley', 'PasteFromWord', 'PasteText', '-', 'Image'],
            ];

            CKEDITOR.replace(this.id,
               {
                   filebrowserBrowseUrl: 'http://enabill.alacrity.co.za/Content/ckeditor/filemanager/browser/default/browser.html?Type=Image&Connector=%2FContent%2Fckeditor%2Ffilemanager%2Fconnectors%2Faspx%2Fconnector.aspx',
                   filebrowserImageBrowseUrl: 'http://enabill.alacrity.co.za/Content/ckeditor/filemanager/browser/default/browser.html?Type=Image&Connector=%2FContent%2Fckeditor%2Ffilemanager%2Fconnectors%2Faspx%2Fconnector.aspx',
                   filebrowserFlashBrowseUrl: 'http://enabill.alacrity.co.za/Content/ckeditor/filemanager/browser/default/browser.html?Type=Image&Connector=%2FContent%2Fckeditor%2Ffilemanager%2Fconnectors%2Faspx%2Fconnector.aspx',
                   //filebrowserUploadUrl: 'http://poinew.citgator.com/Content/ckeditor/filemanager/connectors/php/upload.php?Type=File',
                   //filebrowserImageUploadUrl: 'http://poinew.citgator.com/Content/ckeditor/filemanager/connectors/php/upload.php?Type=Image',
                   //filebrowserFlashUploadUrl: 'http://poinew.citgator.com/Content/ckeditor/filemanager/connectors/php/upload.php?Type=Flash'
                   height: height
               });
        });


        $('.crudFCKtall:visible').each(function () {
            var h = (!height ? 400 : height);
            if (h < 500)
                height = "400";
            else
                height = height - 440;
            var instance = CKEDITOR.instances[this.id];
            if (instance) {
                CKEDITOR.remove(instance);
            }
            CKEDITOR.BasePath = "/Content/ckeditor/";
            CKEDITOR.config.scayt_autoStartup = true;
            CKEDITOR.config.toolbar_Full =
            [
                ['NewPage', 'Preview', '-', 'Templates'],
                ['SpellChecker', 'Scayt'],
                ['Bold', 'Italic', 'Underline'],
                ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                ['NumberedList', 'BulletedList'],
                '/',
                ['Outdent', 'Indent'],
                ['Source'],
                ['Subscript', 'Superscript'],
                ['TextColor', 'BGColor', '-', 'SpecialChar', 'Smiley', 'PasteFromWord', 'PasteText', '-', 'Image'],
                '/',
                ['Styles', 'Format', 'Font', 'FontSize']
            ];
        });

        $('.crudFCKmini:visible').each(function () {
            var h = (!height ? 400 : height);
            if (h < 500)
                height = "400";
            else
                height = height - 475;
            var instance = CKEDITOR.instances[this.id];
            if (instance) {
                CKEDITOR.remove(instance);
            }
            CKEDITOR.BasePath = "/Content/ckeditor/";
            CKEDITOR.config.scayt_autoStartup = true;
            CKEDITOR.config.toolbar_Full =
            [
                ['Preview', 'Templates'],
                ['SpellChecker', 'Scayt'],
                ['Bold', 'Italic', 'Underline'],
                ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                ['NumberedList', 'BulletedList'],
                ['Outdent', 'Indent'],
                ['Source', 'Image'],
                '/',
                ['Styles', 'Format', 'Font', 'FontSize'],
                ['TextColor', 'BGColor'],
                ['SpecialChar', 'Smiley', 'PasteFromWord', 'PasteText'],
            ];

            CKEDITOR.replace(this.id,
               {
                   filebrowserBrowseUrl: 'http://enabill.alacrity.co.za/Content/ckeditor/filemanager/browser/default/browser.html?Type=Image&Connector=%2FContent%2Fckeditor%2Ffilemanager%2Fconnectors%2Faspx%2Fconnector.aspx',
                   filebrowserImageBrowseUrl: 'http://enabill.alacrity.co.za/Content/ckeditor/filemanager/browser/default/browser.html?Type=Image&Connector=%2FContent%2Fckeditor%2Ffilemanager%2Fconnectors%2Faspx%2Fconnector.aspx',
                   filebrowserFlashBrowseUrl: 'http://enabill.alacrity.co.za/Content/ckeditor/filemanager/browser/default/browser.html?Type=Image&Connector=%2FContent%2Fckeditor%2Ffilemanager%2Fconnectors%2Faspx%2Fconnector.aspx',
                   //filebrowserUploadUrl: 'http://poinew.citgator.com/Content/ckeditor/filemanager/connectors/php/upload.php?Type=File',
                   //filebrowserImageUploadUrl: 'http://poinew.citgator.com/Content/ckeditor/filemanager/connectors/php/upload.php?Type=Image',
                   //filebrowserFlashUploadUrl: 'http://poinew.citgator.com/Content/ckeditor/filemanager/connectors/php/upload.php?Type=Flash'
                   height: height
               });
        });

        $('.crudFCKtext:visible').each(function () {
            var h = (!height ? 400 : height);
            if (h < 500)
                height = "400";
            else
                height = height - 410;
            var instance = CKEDITOR.instances[this.id];
            if (instance) {
                CKEDITOR.remove(instance);
            }
            CKEDITOR.BasePath = "/Content/ckeditor/";
            CKEDITOR.config.scayt_autoStartup = true;
            CKEDITOR.config.toolbar_Full =
            [
            ];

            CKEDITOR.replace(obj.id,
            {
                height: height
            });
        });

    },

    CKupdate: function () {
        for (instance in CKEDITOR.instances)
            CKEDITOR.instances[instance].updateElement();
    }

};
*/
var slideShow = new function () {
    this.containerID = '',
    this.fullSource = '',
    this.thumbSource = '',
    this.items = [],
    this.thumbCount = 0,
    this.currentIndex = 0,
    this.height = 'auto',
    this.width = 500,
    this.PictureSource = function (item) {
        return this.fullSource + item;
    },
    this.ThumbSource = function (item) {
        return this.thumbSource + item;
    },
    this.GetHtml = function () {
        if (this.items.length < this.thumbCount)
            this.thumbCount = this.items.length;

        var targetWidth = 0;
        var targetHeight = 0;

        var bigPictureSize = '';
        if (this.items[this.currentIndex].Width > this.items[this.currentIndex].Height) {

            if (this.items[this.currentIndex].Width > this.width) {
                targetWidth = this.width;
            }
            else {
                targetWidth = this.items[this.currentIndex].Width;
            }

            var ratio = targetWidth / this.items[this.currentIndex].Width;
            targetHeight = Math.round(this.items[this.currentIndex].Height * ratio);

            if (targetHeight > this.height) {
                ratio = this.height / targetHeight;
                targetWidth = Math.round(targetWidth * ratio);
            }

            bigPictureSize = 'width="' + targetWidth + 'px" height="auto"';
        }
        else {
            bigPictureSize = 'width="auto" height="' + this.height + 'px"';
            targetHeight = this.height;
            var ratio = targetHeight / this.items[this.currentIndex].Height;
            targetWidth = this.items[this.currentIndex].Width * ratio;
        }

        //width="' + this.width + '"; 
        //var bigPicture = '<div id="mainFrame" style="position:relative; height=' + this.height + 'px"><a onclick="slideShow.Prev()"><div style="float:left; border:solid 1px; width:20px; height:' + this.height + 'px; position:absolute"/></a><img src="' + this.PictureSource(this.items[this.currentIndex].FileName) + '" alt="' + this.items[this.currentIndex].FileName + '" ' + bigPictureSize + '/><a onclick="slideShow.Next()"><div style="float:right; border:solid 1px; width:20px; height:' + this.height + 'px; left:' + (this.width - 20) + 'px; top:0px; position:absolute;"/></a></div>';
        var bigPicture = '<div id="mainFrame" style="position:relative; height=' + this.height + 'px"><a href="" style="text-decoration:none;" onclick="slideShow.Prev(); return false;"><div style="border:solid 1px; width:60px; height:' + targetHeight + 'px; top:0px; left:' + ((this.width / 2) - (targetWidth / 2) - 30) + 'px; position:absolute"/></a><img src="' + this.PictureSource(this.items[this.currentIndex].FileName) + '" alt="' + this.items[this.currentIndex].FileName + '" ' + bigPictureSize + '/><a href="" style="text-decoration:none;" onclick="slideShow.Next(); return false;"><div style="border:solid 1px; width:60px; height:' + targetHeight + 'px; left:' + (this.width - (this.width / 2) + (targetWidth / 2) - 30) + 'px; top:0px; position:absolute;"/></a></div>';

        var left = '<a href="" style="text-decoration:none;" onclick="slideShow.Prev(); return false;"><span id="scrollLeft" style="width:15px">L E F T</span></a>';
        var right = '<a href="" style="text-decoration:none;" onclick="slideShow.Next(); return false;"><span id="scrollRight" style="width:15px">R I G H T</span></a>';

        var thumbs = '';
        var index = Math.round(this.currentIndex - (this.thumbCount / 2));
        var i = index;
        if (i < 0)
            i += this.items.length;
        while (index <= Math.round(this.currentIndex + (this.thumbCount / 2))) {
            thumbs += '<span style="padding:2px"><a onclick="slideShow.Item(' + i + ')"><img src="' + this.ThumbSource(this.items[i].FileName) + '" alt="' + this.items[i].FileName + '" height="70" width="auto" style="border:solid 1px"></a></span>';
            index++;
            i++;
            if (i >= this.items.length)
                i -= this.items.length
        }
        var scroller = '<div id="scroller">' + left + thumbs + right + '</div>';

        var debug = '<div id="debug">width:' + this.width + ' height:' + this.height + ' targetwidth:' + targetWidth + ' targetheight:' + targetHeight + '</div>';

        var structure = '<div id="slideShow">' + bigPicture + scroller + debug + '</div>';

        $(this.containerID).html(structure);
    },
    this.Next = function () {
        this.currentIndex++;
        if (this.currentIndex >= this.items.length)
            this.currentIndex -= this.items.length;
        this.GetHtml();
    },
    this.Prev = function () {
        this.currentIndex--;
        if (this.currentIndex < 0)
            this.currentIndex += this.items.length;
        this.GetHtml();
    },
    this.Item = function (index) {
        this.currentIndex = index;
        this.GetHtml();
    },
    this.init = function (items, thumbnailCount, containerID, fullSource, thumbSource, startIndex) {
        this.items = items;
        this.thumbCount = thumbnailCount;
        this.containerID = containerID;
        this.fullSource = fullSource;
        this.thumbSource = thumbSource;
        this.currentIndex = startIndex;
        this.width = $(containerID).width();
        this.height = $(containerID).height();
        $(containerID).html(this.GetHtml());
    }
};
