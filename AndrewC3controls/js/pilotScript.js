var opTypeOptions = new Array('?? operator', '?? voltage');
var lensOptions = new Array('?? clamp ring', '?? lens type', 'Red');
var opTypeAdded = false;
var currentLamp = '';
var fullVoltageChoice = ["6V AC/DC", "12V AC/DC", "24V AC/DC", "120V AC/DC"];
var transformerChoice = ["120V AC","240V AC","277V AC","480V AC"];
var resistorChoice = ["120V AC/DC", "240V AC/DC", "480V AC/DC"];

var skuPTT = "PTT";
var skuOperator = "";
var skuVoltage = "";
var skuLampType = "";
var skuClamp = "";
var skuLensType = "";
var skuLensColor = "";
var skuOptions = "";

var hoverComponentTip = "A component whose description would go here";

/**
* Prints the current SKU to the "sku" paragraph in the right half
*/
function printSKU() {
    var skuText = document.getElementById('sku');
    skuText.innerHTML = "";
    
    var sku = skuOperator + skuPTT + skuVoltage + skuLampType + " - " + skuClamp + skuLensType + skuLensColor + " - " + skuOptions;
    sku = sku.replace(" -  - ", " - ");
    if (sku.substring(sku.length - 3, sku.length - 1) == " -") {
        sku = sku.substring(0, sku.length - 2);
    }

    if (skuIsValid(sku)) {
        document.getElementById("sku").style.color = "green";
        document.getElementById("orderableText").innerText = "This product has been assembled before";
        document.getElementById("addProductBtn").className = "";
        document.getElementById("addProductBtn").className += "btn btn-success center-block";
        document.getElementById("addProductBtn").innerText = "Order";
    } else {
        document.getElementById("sku").style.color = "#ec971f";
        document.getElementById("orderableText").innerText = "*This product requires engineer approval";
        document.getElementById("addProductBtn").className = "";
        document.getElementById("addProductBtn").className += "btn btn-warning center-block";
        document.getElementById("addProductBtn").innerText = "Submit SKU";
    }
    skuText.innerHTML = sku;
}

function allOpTypeChoicesSelected() {
    for (var i = 0; i < 2; i++) {
        if (opTypeOptions[i].includes('?')) {
            return false;
        }
    }
    return true;
}

function allLensChoicesSelected() {
    for (var i = 0; i < 3; i++) {
        if (lensOptions[i].includes('?')) {
            return false;
        }
    }
    return true;
}

function createPreviewImage(type) {
    var img = document.createElement('img');
    img.className += " center-block";
    
    if (type == "pilotLight") {
        img.src = "img/ptt/pilotLight" + lensOptions[2] + ".jpg";
        img.width = 260;
        img.height = 150;
        return img;
    }
    else if (type == 'opVolt' || type == 'options') {
        img.src = "img/ptt/wrench.png";
    } else if (type == 'lamp') {
        img.src = "img/ptt/lamp" + currentLamp.replace(/ /g, '').replace('LED', '') + ".png";
    } else if (type == 'lens') {
        img.src = "img/ptt/lens" + lensOptions[2] + ".png";
    }
    img.alt = img.src;
    img.width = 50;
    img.height = 50;
    img.title = hoverComponentTip;
    
    return img;
}

/**
*  Called when either the operator type or voltage dropdown menu items are clicked. Assigns selected options to the
*  opTypeOptions array and fills the preview table appropriately.
*/
function opTypeChoiceClick(choice, whichDropdown) {
    
    //Store the selection for that dropdown in the array
    if (whichDropdown == 'operator') {
        opTypeOptions[0] = choice.text;
        opTypeOptions[1] = "?? voltage";
    } else if (whichDropdown == 'voltage') {
        opTypeOptions[1] = choice.text;
    } 

    //Populate voltage dropdown
    createVoltageDropdownItems(choice);

    //Fill the preview table
    var selectionWrapperDiv = document.getElementById('opTypeSelectionWrapper');
    selectionWrapperDiv.style.display = "block";
    var selectionTextDiv = document.getElementById('opTypeSelectionText');
    selectionTextDiv.innerHTML = opTypeOptions[0] + " operator, " + opTypeOptions[1] + " pilot light";
    var img = createPreviewImage('opVolt');
    var selectionImageDiv = document.getElementById('opTypeSelectionImg');
    selectionImageDiv.innerHTML = "";
    selectionImageDiv.appendChild(img);

    //Enable the add button if all choices are selected
    if (allOpTypeChoicesSelected()) {
        document.getElementById('addOpTypeBtn').disabled = false;
    } else {
        document.getElementById('addOpTypeBtn').disabled = true;
    }
}

function lensChoiceClick(choice, whichDropdown) {

    //Store the selection for that dropdown in the array
    if (whichDropdown == 'clamp') {
        lensOptions[0] = choice.text;
    } else if (whichDropdown == 'lensType') {
        lensOptions[1] = choice.text;
    } else if (whichDropdown == 'lensColor') {
        lensOptions[2] = choice.text;
    }

    //Fill the preview table
    var selectionWrapperDiv = document.getElementById('lensSelectionWrapper');
    selectionWrapperDiv.style.display = "block";
    var selectionTextDiv = document.getElementById('lensSelectionText');
    if (lensOptions[0].includes('?')) {
        selectionTextDiv.innerHTML = lensOptions[0] + " , " + lensOptions[1] + ", " + lensOptions[2] + " lens";
    } else if (lensOptions[0].includes('None')) {
        selectionTextDiv.innerHTML = "No clamp ring, " + lensOptions[1] + ", " + lensOptions[2] + " lens";
    } else {
        selectionTextDiv.innerHTML = lensOptions[0] + " clamp ring, " + lensOptions[1] + ", " + lensOptions[2] + " lens";
    }
    selectionTextDiv.innerHTML = capitalizeFirstLetter(selectionTextDiv.innerHTML);

    var img = createPreviewImage('lens');
    var selectionImageDiv = document.getElementById('lensSelectionImg');
    selectionImageDiv.innerHTML = "";
    selectionImageDiv.appendChild(img);

    //Enable the add button if all choices are selected
    if (allLensChoicesSelected()) {
        document.getElementById('addLensBtn').disabled = false;
    }
}

/**
*  Called by opTypeChoiceClick to fill the voltage dropdown with valid options
*/
function createVoltageDropdownItems(choice) {
    //Set this array to the correct list of voltages
    var voltageArray = [];
    if (choice.text == "Full Voltage") {
        voltageArray = fullVoltageChoice;
    } else if (choice.text == "Transformer(50/60 Hz)") {
        voltageArray = transformerChoice;
    } else {
        voltageArray = resistorChoice;
    }

    //Loop through it and create a list item in the voltage dropdown for each
    var voltageDropdown = document.getElementById('voltageDropDown');
    voltageDropdown.innerHTML = "";
    for (var i = 0; i < voltageArray.length; i++) {
        var listItem = document.createElement("li");
        var listItemHyperLink = document.createElement("a");
        listItemHyperLink.innerHTML = voltageArray[i];
        listItemHyperLink.id = voltageArray[i];
        listItemHyperLink.href = "#";
        listItemHyperLink.onclick = function () { opTypeChoiceClick(this, 'voltage') };
        listItem.appendChild(listItemHyperLink);
        voltageDropDown.appendChild(listItem);
    }
}

/**
*  Called when either the operator type or voltage dropdown menu items are clicked. Assigns selected options to the
*  opTypeOptions array and fills the preview table appropriately.
*/
function lampChoiceClick(choice) {
    currentLamp = choice.text;

    //Fill the preview table
    var selectionWrapperDiv = document.getElementById('lampSelectionWrapper');
    selectionWrapperDiv.style.display = "block";
    var selectionTextDiv = document.getElementById('lampSelectionText');
    selectionTextDiv.innerHTML = choice.text + " lamp";
    var img = createPreviewImage('lamp');
    var selectionImageDiv = document.getElementById('lampSelectionImg');
    selectionImageDiv.innerHTML = "";
    selectionImageDiv.appendChild(img);
}

function optionsChoiceClick(choice) {

    //Fill the preview table
    var selectionWrapperDiv = document.getElementById('optionsSelectionWrapper');
    selectionWrapperDiv.style.display = "block";
    var selectionTextDiv = document.getElementById('optionsSelectionText');
    selectionTextDiv.innerHTML = "IP20 guards";
    var img = createPreviewImage('options');
    var selectionImageDiv = document.getElementById('optionsSelectionImg');
    selectionImageDiv.innerHTML = "";
    selectionImageDiv.appendChild(img);
}

function addOpType() {
    document.getElementById('opTypeSelectionWrapper').style.display = "none";
    document.getElementById('tOpType').innerHTML = opTypeOptions[0];
    document.getElementById('tVoltage').innerHTML = opTypeOptions[1];
    skuOperator = getSkuFromComponentString(opTypeOptions[0], "operator");
    skuVoltage = getSkuFromComponentString(opTypeOptions[1], "voltage");
    printSKU();
}

function addLens() {
    var pilotLightImg = createPreviewImage("pilotLight");
    document.getElementById('previewImgDiv').innerHTML = "";
    document.getElementById('previewImgDiv').appendChild(pilotLightImg);
    document.getElementById('lensSelectionWrapper').style.display = "none";
    document.getElementById('tClamp').innerHTML = lensOptions[0];
    document.getElementById('tLensType').innerHTML = lensOptions[1];
    document.getElementById('tLensColor').innerHTML = lensOptions[2];
    skuClamp = getSkuFromComponentString(lensOptions[0], "lens");
    skuLensType = getSkuFromComponentString(lensOptions[1], "lens");
    skuLensColor = getSkuFromComponentString(lensOptions[2], "lens");
    printSKU();
}

function addLamp() {
    document.getElementById('lampSelectionWrapper').style.display = "none";
    document.getElementById('tLamp').innerHTML = currentLamp;
    skuLampType = getSkuFromComponentString(currentLamp, "lamp");
    printSKU();
}

function addOptions() {
    document.getElementById('optionsSelectionWrapper').style.display = "none";
    document.getElementById('tOptions').innerHTML = "IP20 Guards";
    skuOptions = getSkuFromComponentString("IP20 Guards", "options");
    printSKU();
}

function getSkuFromComponentString(str, category) {

    if (category == "operator") {
        switch (str) {
            case document.getElementById("opFV").innerHTML:
                return "FV";
            case document.getElementById("opT").innerHTML:
                return "TF";
            case document.getElementById("opR").innerHTML:
                return "RL";

        }
    } else if (category == "voltage") {
        var tokens = str.split("V");
        console.log("returning " + tokens[0] + " for voltage");
        return tokens[0];
    } else if (category == "lamp") {
        switch (str) {
            case document.getElementById("lampCFI").innerHTML:
                return "I";
            case document.getElementById("lampCI").innerHTML:
                return "I";
            case document.getElementById("lampAmber").innerHTML:
                return "LA";
            case document.getElementById("lampBlue").innerHTML:
                return "LB";
            case document.getElementById("lampGreen").innerHTML:
                return "LG";
            case document.getElementById("lampRed").innerHTML:
                return "LR";
            case document.getElementById("lampWhite").innerHTML:
                return "LW";
            case document.getElementById("lampNG").innerHTML:
                return "NG";
            case document.getElementById("lampNR").innerHTML:
                return "NR";
            default:
                return "NL";
        }
    } else if (category == "lens") {
        switch (str) {
            case document.getElementById("clampAluminum").innerHTML:
                return "M";
            case document.getElementById("clampPolyester").innerHTML:
                return "X";
            case document.getElementById("clampNone").innerHTML:
                return "";

            case document.getElementById("lensIlluminated").innerHTML:
                return "IPBC";
            case document.getElementById("lensIlluminatedMushroom").innerHTML:
                return "IPBCM";
            case document.getElementById("lensGuardedIlluminated").innerHTML:
                return "GIPBC";
            case document.getElementById("lensShroudedIlluminatedMushroom").innerHTML:
                return "SIPBCM";

            case document.getElementById("lensAmber").innerHTML:
                return "AR";
            case document.getElementById("lensBlue").innerHTML:
                return "BE";
            case document.getElementById("lensGreen").innerHTML:
                return "GN";
            case document.getElementById("lensClear").innerHTML:
                return "CR";
            case document.getElementById("lensRed").innerHTML:
                return "RD";
            case document.getElementById("lensWhite").innerHTML:
                return "WE";
            case document.getElementById("lensYellow").innerHTML:
                return "YW";
        }
    } else if (category == "options") {
        return "IP20";
    }

    return "";
}

function skuIsValid(sku) {
    var skuCleaned = sku.replace(/ - /g, "\-").trim();
    var allowableSkusMegastring = "FVPTT120 FVPTT120-IPBC FVPTT120-IPBCAR FVPTT120-IPBCBE FVPTT120-IPBCGN FVPTT120-IPBCRD FVPTT120-IPBCWE FVPTT120-IPBCYW FVPTT120LA FVPTT120LA-IPBCAR FVPTT120LB FVPTT120LB-IPBCBE FVPTT120LB-IPBCCR FVPTT120LG FVPTT120LG-IPBC FVPTT120LG-IPBCGN FVPTT120LG-IPBCMGN FVPTT120LR FVPTT120LR-IPBC FVPTT120LR-IPBCMRD FVPTT120LR-IPBCRD FVPTT120LW FVPTT120LW-IPBCAR FVPTT120LW-IPBCBE FVPTT120LW-IPBCGN FVPTT120LW-IPBCRD FVPTT120LW-IPBCWE FVPTT120LW-IPBCYW FVPTT120NL-IPBC FVPTT120NR-IPBCRD FVPTT12LR-IPBCRD FVPTT24-IPBCAR FVPTT24-IPBCBE FVPTT24-IPBCGN FVPTT24-IPBCRD FVPTT24-IPBCWE FVPTT24-IPBCYW FVPTT24LA-IPBCAR FVPTT24LA-IPBCYW FVPTT24LB-IPBCBE FVPTT24LG-IPBCGN FVPTT24LR-IPBCRD FVPTT24LW-IPBCAR FVPTT24LW-IPBCBE FVPTT24LW-IPBCGN FVPTT24LW-IPBCMRD FVPTT24LW-IPBCRD FVPTT24LW-IPBCWE FVPTT24LW-IPBCYW FVPTT48LA-IPBCAR FVPTT48LG-IPBCGN FVPTT48LR-IPBCRD FVPTT48LW-IPBCWE FVPTT6LR-IPBCRD FVPTT120-IPBCCR FVPTT120LA-IPBCYW FVPTT120LW-IPBC FVPTT120LW-IPBCCR FVPTT120NG-IPBCGN FVPTT120-SIPBCMRD FVPTT12LA-IPBCAR FVPTT24-IPBC FVPTT24LG-IPBCGN-IP20 FVPTT24LR-IPBCRD-IP20 FVPTT48LB-IPBCBE FVPTT24LR-GIPBCRD FVPTT24LR-GIPBCCR FVPTT24LG-GIPBCGN FVPTT24LG-GIPBCCR FVPTT24LB-GIPBCCR FVPTT12LW-GIPBCCR FVPTT120LW-GIPBCRD FVPTT120LR-GIPBCRD FVPTT120LG-GIPBCGN FVPTT120LA-GIPBCAR FVPTT120-GIPBCRD FVPTT120-AIPBCAR FVPTT120-AIPBCBE FVPTT120-AIPBCGN FVPTT120-AIPBCRD FVPTT120-AIPBCWE FVPTT120LA-AIPBCAR FVPTT120LB-AIPBCBE FVPTT120LG-AIPBCGN FVPTT120LR-AIPBCRD FVPTT120LW-AIPBCAR FVPTT120LW-AIPBCGN FVPTT120LW-AIPBCRD FVPTT120LW-AIPBCWE FVPTT120LW-AIPBCYW FVPTT120NL-AIPBCGN FVPTT120NL-AIPBCRD FVPTT120NR-AIPBCRD FVPTT12LR-AIPBCRD-IP20 FVPTT24-AIPBCAR FVPTT24-AIPBCBE FVPTT24-AIPBCGN FVPTT24-AIPBCRD FVPTT24LA-AIPBCAR FVPTT24LB-AIPBCBE FVPTT24LG-AIPBCGN FVPTT24LR-AIPBCRD FVPTT24LW-AIPBCGN FVPTT24LW-AIPBCRD FVPTT24LW-AIPBCWE FVPTT24LA-GIPBCAR FVPTT24LW-GIPBCWE FVPTT120LW-IPBCGN-IP20 FVPTT120LW-GIPBCWE FVPTT120LW-IPBCRD-IP20 FVPTT120LG-IPBCGN-IP20 FVPTT6F-IPBCRD FVPTT120LR-AIPBCRD-IP20 FVPTT120LR-IPBCRD-IP20 FVPTT120LW-IPBCWE-IP20 FVPTT120LG-AIPBCGN-IP20 FVPTT120LA-AIPBCAR-IP20 RLPTT125DLW-IPBCWE RLPTT125DLW-IPBCCR RLPTT120I RLPTT48LW-IPBCCR RLPTT48LG-IPBCGN RLPTT48LR-IPBCRD RLPTT24DLW-IPBCWE RLPTT24DLW-IPBCCR RLPTT24DLA-IPBCAR RLPTT24DLB-IPBCBE RLPTT24DLR-IPBCRD RLPTT48DLB-IPBCBE RLPTT48DLA-IPBCAR RLPTT48DLR-IPBCRD RLPTT24DLG-IPBCGN RLPTT48DLW-IPBCWE RLPTT48DLG-IPBCGN RLPTT120-IPBCBE RLPTT120-IPBCGN RLPTT120-IPBCRD RLPTT120-IPBCWE RLPTT120-IPBCYW RLPTT120LA-IPBCAR RLPTT120LB-IPBCBE RLPTT120LG-IPBCGN RLPTT120LR RLPTT120LR-IPBCRD RLPTT120LW-IPBCCR RLPTT120LW-IPBCWE RLPTT120LW-IPBCYW RLPTT125DLA-IPBCAR RLPTT125DLA-IPBCWE RLPTT125DLA-IPBCYW RLPTT125DLB-IPBCBE RLPTT125DLB-IPBCBE RLPTT125DLB-IPBCWE RLPTT125DLG-IPBCGN RLPTT125DLR-IPBCRD RLPTT125DLW-IPBCCR RLPTT125DLW-IPBCWE RLPTT240LA-IPBCAR RLPTT240LB-IPBCBE RLPTT240LG-IPBCGN RLPTT240LR-IPBCRD RLPTT240LW-IPBCWE RLPTT240NG RLPTT240NR RLPTT480NG RLPTT480NR RLPTT120-AIPBCGN RLPTT120I-AIPBCAR RLPTT120I-AIPBCGN RLPTT120I-AIPBCRD RLPTT120LG-AIPBCGN RLPTT120LG-AIPBCMGN RLPTT120LG-GIPBCGN RLPTT120LR-AIPBCRD RLPTT120LR-GIPBCRD RLPTT120LW-AIPBCWE RLPTT120LW-AIPBCYW RLPTT125DLA-AIPBCAR RLPTT125DLB-AIPBCBE RLPTT125DLG-AIPBCGN RLPTT125DLR-AIPBCRD RLPTT125DLW-AIPBCYW RLPTT240LB-AIPBCBE RLPTT240LR-AIPBCRD RLPTT240LW-AIPBCWE RLPTT480LA-AIPBCAR RLPTT48DLA-AIPBCAR RLPTT48DLB-AIPBCBE RLPTT48DLG-AIPBCGN RLPTT48DLR-AIPBCRD RLPTT48DLW-AIPBCWE RLPTT120LW-IPBCGN RLPTT120LW-AIPBCRD RLPTT120LW-AIPBCGN RLPTT120LW-AIPBCAR RLPTT120LW-IPBCRD RLPTT120LA-AIPBCAR RLPTT120LB-AIPBCBE RLPTT140LA-IPBCAR TFPTT120 TFPTT120F TFPTT120F-IPBCRD TFPTT120-IPBCAR TFPTT120-IPBCBE TFPTT120-IPBCGN TFPTT120-IPBCRD TFPTT120-IPBCWE TFPTT120-IPBCYW TFPTT120LR TFPTT120LR-IPBCRD TFPTT120LR-IPBCWE TFPTT120LW TFPTT120LW-IPBCAR TFPTT120LW-IPBCBE TFPTT120LW-IPBCGN TFPTT120LW-IPBCRD TFPTT120LW-IPBCWE TFPTT240 TFPTT240F TFPTT240-IPBCAR TFPTT240-IPBCGN TFPTT240-IPBCRD TFPTT240-IPBCWE TFPTT240LR-IPBCRD TFPTT277 TFPTT277LG-IPBCGN TFPTT480 TFPTT480F TFPTT480-IPBCGN TFPTT480LG-IPBCGN TFPTT120LA TFPTT120LA-IPBCAR TFPTT120LA-IPBCCR TFPTT120LA-IPBCYW TFPTT120LB-IPBCBE TFPTT120LG TFPTT120LG-IPBCGN TFPTT240LG-IPBCGN TFPTT120-AIPBCAR TFPTT120-AIPBCBE TFPTT120-AIPBCGN TFPTT120-AIPBCRD TFPTT120-AIPBCWE TFPTT120F-AIPBCRD TFPTT120-GIPBCAR TFPTT120-GIPBCBE TFPTT120-GIPBCGN TFPTT120-GIPBCRD TFPTT120-GIPBCWE TFPTT120LA-AIPBCAR TFPTT120LB-AIPBCBE TFPTT120LG-AIPBCCR TFPTT120LG-AIPBCGN TFPTT120LR-AIPBCCR TFPTT120LR-AIPBCRD TFPTT120LR-AIPBCRD-IP20 TFPTT120LR-GIPBCRD TFPTT120LW-AIPBCWE TFPTT120LW-AIPBCYW TFPTT120LW-GIPBCAR TFPTT120LW-GIPBCRD TFPTT120LW-GIPBCWE TFPTT240LR-AIPBCRD TFPTT120LA-AIPBCAR-IP20 TFPTT120LW-AIPBCWE-IP20 TFPTT120F-IPBCWE TFPTT120LG-AIPBCGN-IP20 TFPTT120LB-AIPBCBE-IP20 TFPTT120-AIPBCMGN";
    var allowableSkusArray = allowableSkusMegastring.split(" ");

    for (var i = 0; i < allowableSkusArray.length; i++) {
        if (skuCleaned == allowableSkusArray[i]) {
            return true;
        }
    }

    return false;
}

function capitalizeFirstLetter(str) {
    var newString = str.toLowerCase();
    newString = newString.charAt(0).toUpperCase() + newString.substring(1);
    return newString;
}