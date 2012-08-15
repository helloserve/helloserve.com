var Admin = {
    CancelAction: function () {
        $('#adminAction').hide();
    },
    AddFeature: function () {
        $('#adminAction').load('/Admin/AddFeature', function () {
            $('#adminAction').show();
        });
    },
    EditFeature: function (form) {
        var featureID = $('#AdminEditFeatureSelection').val();
        if (typeof (featureID) != "string")
            return false;

        $('#adminAction').load('/Admin/EditFeature', { id: featureID }, function () {
            $('#adminAction').show();
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
    }
}