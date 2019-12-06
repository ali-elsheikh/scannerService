var scanner = {
    url: "http://localhost:8080/",
    action: "",
    data: {},
    imgCropX: 0,
    imgCropY: 0,
    imgCropWidth: 0,
    imgCropHeight: 0,
    mouseDown: true,
    connect: function (op) {
        switch (op) {
            case "checkService":
                scanner.action = "home/checkService";
                scanner.data = "";
                scanner.callAjax("checkService");
                break;
            case "getScanners":
                scanner.action = "home/getScannerList";
                scanner.data = "";
                scanner.callAjax("getScanners");
                break;
            case "beginScan":
                if (document.getElementById("ddlSrc").value == "-1") {
                    alert("اختر مصدر المسح");
                    return false;
                }
                scanner.action = "home/getDocument";
                scanner.data = scanner.getScannerSettings();
                scanner.callAjax("beginScan");
                break;
            case "nextImage":
                scanner.action = "home/nextImage";
                scanner.data = "";
                scanner.callAjax("nextImage");
                break;
            case "editImg":
                scanner.action = "home/editImage";
                scanner.callAjax("editImg");
                break;
        }
    },
    callAjax: function (op) {
        $.ajax({
            url: scanner.url + scanner.action,
            type: "POST",
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            crossDomain: true,
            cache: false,
            async: false,
            data: scanner.data,
            success: function (result) {
                var res = eval("(" + result + ")");
                switch (op) {
                    case "checkService":
                        if (res == "1")
                            scanner.connect("getScanners");
                        break;
                    case "getScanners":
                        scanner.getScannersRes(res);
                        break;
                    case 'beginScan':
                    case 'nextImage':
                        res = eval("(" + res + ")");
                        if (res != "") {
                            if (res.hasOwnProperty("data")) {
                                if (res.data != "") {
                                    //var temp = scanner.base64ToArrayBuffer(data);
                                    scanner.openFile(res.data);
                                    scanner.connect('nextImage');
                                }
                            } else if (res.hasOwnProperty("ex"))
                                alert(res.ex);
                        }
                        break;
                    case "editImg":
                        res = eval("(" + res + ")");
                        if (res != "") {
                            if (res.hasOwnProperty("data")) {
                                if (res.data != "") { scanner.editImgRes(res.data) };
                            } else if (res.hasOwnProperty("ex"))
                                alert(res.ex);
                        }
                        break;
                }
                res = null;
            },
            error: function (error) { }
        });//end of $.ajax
    },
    getScannersRes: function (res) {
        var op = "<option value='-1'>اختر من القائمة</option>";
        for (var i = 0, len = res.length; i < len; i++) {
            if (res[i] != "")
                op += "<option value='" + res[i] + "'>" + res[i] + "</option>";
        }
        document.getElementById("ddlSrc").innerHTML = op;
    },
    getScannerSettings: function () {
        var obj = {
            ui: document.getElementById("chkShowUI").checked,
            adf: document.getElementById("chkADF").checked,
            colors: function () {
                var elmnts = document.getElementsByName("radColors");
                for (var i = 0; i < elmnts.length; i++) {
                    if (elmnts[i].checked)
                        return elmnts[i].value;
                }
            }(),
            format: function () {
                var elmnts = document.getElementsByName("radFormat");
                for (var i = 0; i < elmnts.length; i++) {
                    if (elmnts[i].checked)
                        return elmnts[i].value;
                }
            }(),
            resolution: document.getElementById("ddlResolution").value,
            src: document.getElementById("ddlSrc").value,
            duplex: document.getElementById("chkDuplex").checked,
            pageSize: document.getElementById("ddlPageSize").value
        }
        return JSON.stringify(obj);
    },
    base64ToArrayBuffer: function (base64) {
        var binaryString = window.atob(base64);
        var binaryLen = binaryString.length;
        var bytes = new Uint8Array(binaryLen);
        for (var i = 0; i < binaryLen; i++) {
            var ascii = binaryString.charCodeAt(i);
            bytes[i] = ascii;
        }
        return bytes;
    },
    openFile: function (src) {
        //var blob = new Blob([src], { type: "image/png" });
        //var objectURL = URL.createObjectURL(blob);
        sessionStorage.setItem('image_' + sessionStorage.length, src);
        var img = document.createElement('img');
        img.name = "scannedImg";
        img.src = "data:image/png;base64," + src;
        img.index = document.getElementById("scannedImgs").childElementCount;
        img.onclick = scanner.popUpImg;
        document.getElementById("scannedImgs").appendChild(img);
    },
    popUpImg: function (event) {
        var img = document.createElement('img')
        img.src = (event.target.src) ? (event.target.src) : '#';
        img.index = (event.target.index) ? (event.target.index) : 0;
        img.draggable = false;
        img.onmousedown = function (event) { scanner.onMouseDown(event); };
        img.onmousemove = function (event) { scanner.onMouseMove(event); };
        img.onmouseup = function (event) { scanner.onMouseUp(event); };
        imgPanel.innerHTML = "";
        imgPanel.appendChild(img);
        imgEditor.classList.toggle("popup");
        divPopUp.hidden = !divPopUp.hidden;

    },
    rotateImg: function (degree) {
        var index = document.getElementById("imgPanel").children[0].index;
        var src = document.getElementById("imgPanel").children[0].src.split(',')[1];
        scanner.data = JSON.stringify({
            bytes: src,
            rotate: degree,
            edit: 'rotate'
        });
        scanner.connect("editImg");
    },
    cropImage: function () {
        var index = document.getElementById("imgPanel").children[0].index;
        var src = document.getElementById("imgPanel").children[0].src.split(',')[1];
        scanner.data = JSON.stringify({
            bytes: src,
            cropX: scanner.imgCropX,
            cropWidth: Math.abs(scanner.imgCropX - scanner.imgCropWidth),
            cropY: scanner.imgCropY,
            cropHeight: Math.abs(scanner.imgCropY - scanner.imgCropHeight),
            edit: 'crop'
        });
        scanner.connect("editImg");
    },
    editImgRes: function (src) {
        var img = document.createElement('img');
        img.name = "scannedImg";
        img.src = "data:image/png;base64," + src;
        img.index = document.getElementById("imgPanel").children[0].index;
        img.draggable = false;
        img.onmousedown = function (event) { scanner.onMouseDown(event); };
        img.onmousemove = function (event) { scanner.onMouseMove(event); };
        img.onmouseup = function (event) { scanner.onMouseUp(event); };
        document.getElementById("imgPanel").innerHTML = "";
        document.getElementById("imgPanel").appendChild(img);
    },
    onMouseDown: function (event) {
        scanner.mouseDown = true;
        scanner.imgCropX = (event.layerX - event.target.offsetLeft);
        scanner.imgCropY = (event.layerY - event.target.offsetTop);
        var cropArea = document.getElementsByClassName("rectangle");
        if (cropArea.length > 0) {
            document.getElementById("imgPanel").removeChild(cropArea[0]);
        }
        var element = document.createElement('div');
        element.className = 'rectangle'
        element.style.left = ((event.layerX)) + 'px';
        element.style.top = ((event.layerY)) + 'px';
        document.getElementById("imgPanel").appendChild(element);
        document.getElementById("imgPanel").style.cursor = "crosshair";
    },
    onMouseMove: function (event) {
        if (scanner.mouseDown) {
            var element = document.getElementsByClassName("rectangle");
            if (element.length > 0) {
                element = element[0];
                scanner.imgCropWidth = (event.layerX - event.target.offsetLeft);
                scanner.imgCropHeight = (event.layerY - event.target.offsetTop);
                element.style.width = Math.abs(scanner.imgCropX - scanner.imgCropWidth) + 'px';
                element.style.height = Math.abs(scanner.imgCropY - scanner.imgCropHeight) + 'px';
                element.style.left = scanner.imgCropX + 'px';
                element.style.top = scanner.imgCropY + 'px';
            }
        }
    },
    onMouseUp: function (event) {
        scanner.mouseDown = false;
        document.getElementById("imgPanel").style.cursor = "default";
    },
    saveEdited: function () {
        if (document.getElementById("imgPanel").getElementsByTagName("img").length > 0) {
            const editedImg = document.getElementById("imgPanel").getElementsByTagName("img")[0];
            let srcImgs = document.getElementById("scannedImgs").getElementsByTagName("img");
            srcImgs[editedImg.index].src = editedImg.src;
            sessionStorage.setItem('image_' + editedImg.index, editedImg.src.split(',')[1]);
            imgPanel.innerHTML = "";
            imgEditor.classList.toggle("popup");
            divPopUp.hidden = !divPopUp.hidden;
        }
    }
}
window.onload = function () {
    scanner.connect("checkService");
    sessionStorage.clear();
}
