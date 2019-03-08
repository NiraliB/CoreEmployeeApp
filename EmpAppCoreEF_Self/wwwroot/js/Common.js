
function ValidationRequired(ctrlName, ValMessage, MesPosition) {
    ClearMessage(ctrlName);
    if ($(ctrlName).val() == '' || $(ctrlName).val() == null) {
        $(ctrlName).closest('.form-group').addClass('has-error');
        PrintMes(ctrlName, ValMessage, MesPosition);
        return false;
    }
    return true;
}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function ClearMessage(ctrlName) {
    $(ctrlName).closest('.form-group').removeClass('has-error');
    $(ctrlName).next('.help-block').remove();
    $(ctrlName).prev('.help-block').remove();
}

function PrintMes(ctrl, Message, ValPosition) {
    if (ValPosition == 'after') {
        $('<span class="help-block help-block-error">' + Message + '</span>').insertAfter($(ctrl));
    }
    else {
        $('<span class="help-block help-block-error">' + Message + '</span>').insertBefore($(ctrl));
    }

}
