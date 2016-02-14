function isInt(value) {
    return !isNaN(value) &&
        parseInt(Number(value)) == value &&
        !isNaN(parseInt(value, 10));
}

function formatString(format) {
    var args = Array.prototype.slice.call(arguments, 1);
    return format.replace(/{(\d+)}/g, function(match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
}

function loadScript(src, callback) {
    console.log('loading ' + src);
    var s,
        r,
        t;
    r = false;
    s = document.createElement('script');
    s.type = 'text/javascript';
    s.src = src;
    s.onload = s.onreadystatechange = function() {
        if (!r && (!this.readyState || this.readyState == 'complete')) {
            r = true;
            if (callback !== undefined) {
                callback();
            }
        }
    };
    t = document.getElementsByTagName('script')[0];
    t.parentNode.insertBefore(s, t);
}

function CheckFloatInput(e, decSeparator) {
    console.log(e);
    if ($.inArray(e.keyCode, [8, 9, 27, 13, 110]) !== -1 ||
        // Allow: Decimal Period
        (e.key == decSeparator) ||
        // Allow: Ctrl+A
        (e.keyCode == 65 && e.ctrlKey === true) ||
        // Allow: Ctrl+C
        (e.keyCode == 67 && e.ctrlKey === true) ||
        // Allow: Ctrl+X
        (e.keyCode == 88 && e.ctrlKey === true) ||
        // Allow: home, end, left, right
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}

function setupForm(form, saveButton, errorPanel, decSeparator) {

    form.find('input[data-editor="datetime"]').each(function(i, el) {
        $(el).val($('#' + $(el).data('val')).val());
    });
    form.find('input[data-editor="datetime"]').datetimepicker({
        format: 'l LT'
    }).on('dp.hide', function(e) {
        $('#' + $(e.target).data('val')).val(e.date.format());
    });

    form.find('input[data-editor="date"]').datetimepicker({
        format: 'l'
    }).on('dp.hide', function(e) {
        $(e.target).attr('data-value', e.date.format());
    });
    form.find('input[data-editor="time"]').clockpicker({
        autoclose: true,
        afterHide: function() {
            console.log('hidden');
        }
    });
    form.find('input[data-editor="float"]').keydown(function(e) {
        return window.CheckFloatInput(e, decSeparator);
    });

}
