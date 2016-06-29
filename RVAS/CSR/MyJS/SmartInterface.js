/// <reference path="/CSR/plugins/jQuery/jQuery-2.1.4.js" />

$(document).ready(function () {
    $(".numeric").on('keyup', function () {
        this.value = this.value.replace(/[^0-9]/, '');
        $(this).val(this.value);
    });
    $(".numericAmount").on('keyup', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
        $(this).val(ReformatNumberWithCommas(this.value));
    });
    $(".numericAmountDecimal").on('keypress', function (e) {
        var nkeycode = e.charCode || e.keyCode;
        var chDecimalDot = String.fromCharCode(nkeycode).toLowerCase();
        if (chDecimalDot === ('.').toLowerCase()) {
            var dotArray = this.value.toString().split('.');
            if (dotArray.length > 1) {
                return false;
            }
            else if (this.value == 0) {
                this.value = '0.';
                return false;
            }
        }
    });
    $(".numericAmountDecimal").on('keyup', function (e) {
        this.value = this.value.replace(/[^0-9\.]/g, '');
        $(this).val(ReformatNumberWithCommas(this.value));
    });
});

function ReformatNumberWithCommas(InputNumber) {
    //Seperates the components of the number
    var n = InputNumber.toString().split(".");
    //Comma-fies the first part
    n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    //Combines the two sections
    return n.join(".");
}


function trimall(sString) {
    while (sString.substring(0, 1) == ' ') {
        sString = sString.substring(1, sString.length);
    }
    while (sString.substring(sString.length - 1, sString.length) == ' ') {
        sString = sString.substring(0, sString.length - 1);
    }
    return sString;
}



function ConvertToPlainText(arrEntry) {

    var divVar = document.createElement("div");


    divVar.innerHTML = arrEntry[0];
    arrEntry[0] = divVar.innerText;




}

function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}

function numbersonly(e, decimal) {
    var key;
    var keychar;

    if (window.event) {
        key = window.event.keyCode;
    }
    else if (e) {
        key = e.which;
    }
    else {
        return true;
    }
    keychar = String.fromCharCode(key);

    if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
        return true;
    }
    else if ((("0123456789").indexOf(keychar) > -1)) {
        return true;
    }
    else if (decimal && (keychar == ".")) {
        return true;
    }
    else
        return false;
}



function addCommas(nStr) {




    while (nStr.indexOf(",") != -1) {
        nStr = nStr.replace(",", "");

    }

    var i;
    var nValue = "";
    for (i = 0; i < nStr.length; i++) {

        if (!isNaN(nStr.substr(i, 1))) {
            nValue = nValue + nStr.substr(i, 1).toString();


        }
    }
    nStr = nValue;

    nStr += ''; var x = nStr.split('.'); var x1 = x[0]; var x2 = x.length > 1 ? '.' + x[1] : ''; var rgx = /(\d+)(\d{3})/; while (rgx.test(x1)) { x1 = x1.replace(rgx, '$1' + ',' + '$2'); }
    nStr = x1 + x2;
    return nStr
}


function RemoveCommas(nStr) {


    while (nStr.indexOf(",") != -1) {
        nStr = nStr.replace(",", "");

    }


    if (isNaN(nStr))
        return "0";


    return nStr;
}



function getQuerystring(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}
