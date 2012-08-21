$(function () {
    General.init();
});

var General = {
    init: function () {
        DatePicker.toDatePicker($('.datePicker'), null);
        DatePicker.toDatePickerForDateRange($("#StartDate"), $("#EndDate"), {}, {});
        DatePicker.toDatePickerForDateRange($("#DateFrom"), $("#DateTo"), {}, {});

        CKEditorSetup.loadFCK(1);
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



/* ------ Rish Text Editor ------- */
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