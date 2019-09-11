

function yearFormatFromString(value) {
    if ($.trim(value) == '') {
        return "";
    }
    value = value.replace('T', ' ').replace(/-/g, "/").substr(0, 19);
    var datetime = new Date(value);
    var year = datetime.getFullYear();
    return year;
}

function monthFormatFromString(value) {
    if ($.trim(value) == '') {
        return "";
    }
    value = value.replace('T', ' ').replace(/-/g, "/").substr(0, 19);
    var datetime = new Date(value);
    var month = datetime.getMonth() + 1;
    if (month < 10) { month = "0" + month };
    return month;
}

function dayFormatFromString(value) {
    if ($.trim(value) == '') {
        return "";
    }
    value = value.replace('T', ' ').replace(/-/g, "/").substr(0, 19);
    var datetime = new Date(value);
    var day = datetime.getDate();
    if (day < 10) { day = "0" + day };
    return day;
}

function dateFormatFromGMTString(value) {
  
    if ($.trim(value) == '') {
        return "";
    }
 
    var datetime = new Date(value);
    var year = datetime.getFullYear();
    var month = datetime.getMonth() + 1;
    if (month < 10) { month = "0" + month };
    var day = datetime.getDate();
    if (day < 10) { day = "0" + day };
    return year + "-" + month + "-" + day;
}

function dateFormatFromString(value) {
    if ($.trim(value) == '') {
        return "";
    }
    value = value.replace('T',' ').replace(/-/g, "/").substr(0, 19);
    var datetime = new Date(value);
    var year = datetime.getFullYear();
    var month = datetime.getMonth() + 1;
    if (month < 10) { month = "0" + month };
    var day = datetime.getDate();
    if (day < 10) { day = "0" + day };
    return year + "-" + month + "-" + day;
}

function dateFormatFromDate(value) {
    var year = value.getFullYear();
    var month = value.getMonth() + 1;
    if (month < 10) { month = "0" + month };
    var day = value.getDate();
    if (day < 10) { day = "0" + day };
    return year + "-" + month + "-" + day;
}

function datetimeFormatFromString(value) {
    if ($.trim(value) == '') {
        return "";
    }
    value = value.replace('T', ' ').replace(/-/g, "/").substr(0, 19);
    var datetime = new Date(value);
    var year = datetime.getFullYear();
    var month = datetime.getMonth() + 1;
    if (month < 10) { month = "0" + month };
    var day = datetime.getDate();
    if (day < 10) { day = "0" + day };
    var hour = datetime.getHours();
    if (hour < 10) { hour = "0" + hour };
    var minute = datetime.getMinutes();
    if (minute < 10) { minute = "0" + minute };
    var second = datetime.getSeconds();
    if (second < 10) { second = "0" + second };
    return year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
}

function datetimeFormatFromDate(value) {
    var year = value.getFullYear();
    var month = value.getMonth() + 1;
    if (month < 10) { month = "0" + month };
    var day = value.getDate();
    if (day < 10) { day = "0" + day };
    var hour = value.getHours();
    if (hour < 10) { hour = "0" + hour };
    var minute = value.getMinutes();
    if (minute < 10) { minute = "0" + minute };
    var second = value.getSeconds();
    if (second < 10) { second = "0" + second };
    return year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
}

function dateFormatter(value, row, index) {
    return dateFormatFromString(value)
}

function datetimeFormatter(value, row, index) {
    return datetimeFormatFromString(value);
}

function indexFormatter(value, row, index) {
    return index + 1;
}

