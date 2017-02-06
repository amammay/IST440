var currentBase = "";
var currentColor = "";
var currentVoltage = "";
var currentSound = "";

var lightModuleOptions = new Array('?? color', '?? type', '?? lens');

var skuWTL = "WTL";
var skuBase = "";
var skuVoltage = "";
var skuModulesLight = "";
var skuModuleSound = "";

var hoverComponentTip = "A component whose description would go here";
var cantAddBaseTip = "You must remove your current base module before adding a new one!";
var cantAddSoundTip = "You must remove your current sound module before adding a new one!";

/**
* Handle sorting events
*/
jQuery(function ($) {
    var panelList = $('#draggablePanelList');
    panelList.sortable({
        handle: '.panel-heading',
        update: function () {
            $('.panel', panelList).each(function (index, elem) {
                var $listItem = $(elem),
                    newIndex = $listItem.index();
            });
        },
        //SORTING OCCURRED, FIGURE OUT THE NEW SKU
        stop: function () { updateSKU(); }
    });
});

function updateSKU() {
    skuModulesLight = "";
    //Fetch all components we gave a sku (just the lights)
    var skuElements = document.querySelectorAll('[sku]');

    //Get the skus in reverse since it pulled them from top to bottom
    for (var i = skuElements.length - 1; i >= 0; i--) {
        skuModulesLight += skuElements[i].getAttribute('sku');
    }

    printSKU();
}

/**
* Prints the current SKU to the "sku" paragraph in the right half
*/
function printSKU() {
    var sku = skuWTL + " - " + skuBase + " - " + skuVoltage + skuModulesLight + skuModuleSound;

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
    document.getElementById("sku").innerHTML = sku;
}

function handleDeletion(deleteImage, type) {
    deleteImage.parentNode.parentNode.parentNode.parentNode.parentNode.removeChild(deleteImage.parentNode.parentNode.parentNode.parentNode);
    if (type == "base") {
        skuBase = "";
        document.getElementById("addBaseBtn").disabled = false;
        document.getElementById('addBaseBtn').title = "";
    } else if (type == "sound") {
        skuModuleSound = "";
        document.getElementById("addSoundBtn").disabled = false;
        document.getElementById('addBaseBtn').title = "";
    }
    updateSKU();
}

/**
*  Gets rid of the dummy gray text and shows text options in the right half 
*/
function hideFillerText() {
    document.getElementById('filler').style.display = 'none';
    document.getElementById('interactivePreviewSKU').style.display = 'block';
    document.getElementById('interactivePreviewTexts').style.display = 'block';
}

/**
*  Return true if the three options for light modules have been selected.
*/
function allLightChoicesSelected() {
    for (var i = 0; i < 3; i++) {
        if (lightModuleOptions[i].includes('?')) {
            return false;
        }
    }
    return true;
}

/**
*  Returns the appropriate image based on whether it's a base, light, or sound module.
*/
function createPreviewImage(type) {
    var img = document.createElement("img");
    img.setAttribute("height", "50");
    img.setAttribute("width", "50");
    img.className += " center-block";

    if (type === 'base') {
        img.id = 'baseImg';
        img.src = 'img/wtl/base.png';
    } else if (type == 'light') {
        img.id = "lightImg";
        img.src = 'img/wtl/towerLight' + currentColor + '.jpg';
    } else if (type == 'sound') {
        img.id = "soundImg";
        img.src = 'img/wtl/sound.jpg';
    } else if (type == 'voltage') {
        img.id = "voltageImg";
        img.src = "img/wtl/lightning.png";
    }

    img.title = hoverComponentTip;
     
    return img;
}

/**
*  Called when one of the options in the base dropdown is selected. Displays the preview table with the image and appropriate text.
*/
function baseChoiceClick(choice) {
    var selectionWrapperDiv = document.getElementById('baseSelectionWrapper');
    selectionWrapperDiv.style.display = "block";
    var selectionTextDiv = document.getElementById('baseSelectionText');
    selectionTextDiv.innerHTML = choice.text;
    selectionTextDiv.innerHTML = capitalizeFirstLetter(selectionTextDiv.innerHTML);
    currentBase = choice.text;

    var img = createPreviewImage('base');
    var selectionImageDiv = document.getElementById('baseSelectionImg');
    selectionImageDiv.innerHTML = "";
    selectionImageDiv.appendChild(img);
}

/**
*  Called when one of the options in the light dropdown is selected. Displays the preview table with the image and appropriate text.
*/
function lightChoiceClick(selection, whichDropdown) {
    //Store the selection for that dropdown in the array
	if (whichDropdown === 'color') {
		currentColor = selection.text;
		lightModuleOptions[0] = selection.text;
	} else if (whichDropdown === 'type') {
		lightModuleOptions[1] = selection.text;
	} else if (whichDropdown === 'lens') {
		lightModuleOptions[2] = selection.text;
	}
	
    //Enable the add button if all choices are selected
	if (allLightChoicesSelected()) {
	    document.getElementById('addLightBtn').disabled = false;
	}
	
    //Create the preview text/image
	var selectionWrapperDiv = document.getElementById('lightSelectionWrapper');
	selectionWrapperDiv.style.display = "block";
	var selectionTextDiv = document.getElementById('lightSelectionText');
	selectionTextDiv.innerHTML = lightModuleOptions[0] + ", " + lightModuleOptions[1] + ", " + lightModuleOptions[2] + " light module";
	selectionTextDiv.innerHTML = capitalizeFirstLetter(selectionTextDiv.innerHTML);
	var img = createPreviewImage('light');
	
	var selectionImageDiv = document.getElementById('lightSelectionImg');
	selectionImageDiv.innerHTML = "";
	selectionImageDiv.appendChild(img);
}

/**
*  Called when one of the options in the sound dropdown is selected. Displays the preview table with the image and appropriate text.
*/
function soundChoiceClick(choice) {
    var selectionWrapperDiv = document.getElementById('soundSelectionWrapper');
    selectionWrapperDiv.style.display = "block";
    var selectionTextDiv = document.getElementById('soundSelectionText');
    selectionTextDiv.innerHTML = choice.text + " sound module";
    selectionTextDiv.innerHTML = capitalizeFirstLetter(selectionTextDiv.innerHTML);
    currentSound = choice.text;

    var img = createPreviewImage('sound');
    var selectionImageDiv = document.getElementById('soundSelectionImg');
    selectionImageDiv.innerHTML = "";
    selectionImageDiv.appendChild(img);
}

/**
*  Called when one of the options in the voltage dropdown is selected. Displays the preview table with the image and appropriate text.
*/
function voltageChoiceClick(choice) {
    var selectionWrapperDiv = document.getElementById('voltageSelectionWrapper');
    selectionWrapperDiv.style.display = "block";
    var selectionTextDiv = document.getElementById('voltageSelectionText');
    selectionTextDiv.innerHTML = choice.text;
    currentVoltage = choice.text;

    var img = createPreviewImage('voltage');
    var selectionImageDiv = document.getElementById('voltageSelectionImg');
    selectionImageDiv.innerHTML = "";
    selectionImageDiv.appendChild(img);
}

/**
* Returns a new bootstrap draggable panel for the interactive display with a caption and image, given the appropriate type
*/
function createModulePanel(type) {
    var panel = document.createElement('li');
    panel.className += "panel panel-link";

    var panelHeader = document.createElement('div');
    panelHeader.className += "panel-heading";

    var panelHeaderRow = document.createElement('div');
    panelHeaderRow.className += "row row-eq-height";

    var panelHeaderLeftCol = document.createElement('div');
    panelHeaderLeftCol.className += "col-md-5";

    var panelHeaderRightCol = document.createElement('div');
    panelHeaderRightCol.className += "col-md-6";

    var delCol = document.createElement('div');
    delCol.className += "col-md-1";
    var deleteImage = document.createElement('img');
    deleteImage.className += "delImg";
    deleteImage.setAttribute("height", "25");
    deleteImage.setAttribute("width", "25");
    deleteImage.className += " center-block";
    deleteImage.src = "img/wtl/del.png";
    deleteImage.onclick = function deleteRow() { handleDeletion(deleteImage, type); }
    delCol.appendChild(deleteImage);

    if (type === 'base') {
        panelHeaderLeftCol.innerHTML = currentBase + " <span style='color:gray'>(always on bottom)</span>";
        panelHeaderLeftCol.innerHTML = capitalizeFirstLetter(panelHeaderLeftCol.innerHTML);
    } else if (type === 'light') {
        panelHeaderLeftCol.innerHTML = lightModuleOptions[0] + ", " + lightModuleOptions[1] + ", " + lightModuleOptions[2];
        panelHeaderLeftCol.innerHTML = capitalizeFirstLetter(panelHeaderLeftCol.innerHTML);
    } else if (type === 'sound') {
        panelHeaderLeftCol.innerHTML = currentSound + " sound module" + " <span style='color:gray'>(always on top)</span>";
        panelHeaderLeftCol.innerHTML = capitalizeFirstLetter(panelHeaderLeftCol.innerHTML);
    }
    panelHeaderRightCol.appendChild(createPreviewImage(type));
    panelHeaderRow.appendChild(panelHeaderLeftCol);
    panelHeaderRow.appendChild(panelHeaderRightCol);
    panelHeaderRow.appendChild(delCol);
    panelHeader.appendChild(panelHeaderRow);
    panel.appendChild(panelHeader);

    return panel;
}

/**
*  Called when the "add" button for the base is pressed. 
*  Adds a base module row to the interactivePreviewBase div.
*/
function addBase() {
    hideFillerText();
    document.getElementById('addBaseBtn').disabled = true;
    document.getElementById('addBaseBtn').title = cantAddBaseTip;
    document.getElementById('baseSelectionWrapper').style.display = "none";
    skuBase = getSkuFromComponentString(currentBase, "base");
    var newRow = createModulePanel('base');
    $('#interactivePreviewBase').append(newRow);
    printSKU();
}

/**
*  Called when the "add" button for the light module is pressed. 
*  Adds a light module row to the interactivePreviewBase div.
*/
function addLight() {
    hideFillerText();
    
    var thisLightSKU = getSkuFromComponentString(lightModuleOptions[1], "light"); //Add the light type
    thisLightSKU += getSkuFromComponentString(lightModuleOptions[2], "light"); //Add the lens type
    thisLightSKU += getSkuFromComponentString(lightModuleOptions[0], "light"); //Add the color
    skuModulesLight += thisLightSKU;

    var newRow = createModulePanel('light');
    newRow.setAttribute('sku', thisLightSKU);
    $('#draggablePanelList').prepend(newRow);
    printSKU();
}

/**
*  Called when the "add" button for the sound module is pressed. 
*  Adds a sound module row to the interactivePreviewBase div.
*/
function addSound() {
    hideFillerText();

    document.getElementById('addSoundBtn').disabled = true;
    document.getElementById('addSoundBtn').title = cantAddSoundTip;
    document.getElementById('soundSelectionWrapper').style.display = "none";
    skuModuleSound = getSkuFromComponentString(currentSound, "sound");
    var newRow = createModulePanel('sound');
    $('#interactivePreviewSound').append(newRow);
    printSKU();
}

/**
*  Called when the "add" button for the voltage is pressed. 
*  Changes the text in the table for voltage.
*/
function addVoltage() {
    hideFillerText();
    document.getElementById('tVoltage').innerHTML = currentVoltage;
    document.getElementById('voltageSelectionWrapper').style.display = "none";
    skuVoltage = getSkuFromComponentString(currentVoltage, "voltage");
    printSKU();
}

function getSkuFromComponentString(str, category) {
    //Rather than doing the switch on something like "Polycarbonate Panel Mount - Short", the switch
    //is done on the option that's provided in the HTML itself. This is to allow changes to the front end
    //option that's displayed without updating the switch statements.

    //It's divided into categories to deal with duplicate strings like "continuous" and reduce how often the document needs to pull elements.


    if (category == "base") {
        switch (str) {
            case document.getElementById("baseShort").innerHTML:
                return "50P1";
            case document.getElementById("baseTall").innerHTML:
                return "50P2";
            case document.getElementById("baseDirect").innerHTML:
                return "50P3";
        }
    } else if (category == "voltage") {
        switch (str) {
            case document.getElementById("voltage24").innerHTML:
                return "MC";
            case document.getElementById("voltage120").innerHTML:
                return "D";
            case document.getElementById("voltage240").innerHTML:
                return "F";
        }
    } else if (category == "light") {
        switch (str) {
            case document.getElementById("lightAmber").innerHTML:
                return "A";
            case document.getElementById("lightBlue").innerHTML:
                return "B";
            case document.getElementById("lightGreen").innerHTML:
                return "G";
            case document.getElementById("lightRed").innerHTML:
                return "R";
            case document.getElementById("lightWhite").innerHTML:
                return "W";

            case document.getElementById("lightContinuous").innerHTML:
                return "D";
            case document.getElementById("lightFlashing").innerHTML:
                return "F";
            case document.getElementById("lightRotary").innerHTML:
                return "R";

            case document.getElementById("lensStandard").innerHTML:
                return "";
            case document.getElementById("lensClear").innerHTML:
                return "C";
        }
    } else if (category == "sound") {
        switch (str) {
            case document.getElementById("soundContinuous").innerHTML:
                return "DS3";
            case document.getElementById("soundIntermittent").innerHTML:
                return "FS3";
        }
    }
}

//This is PURELY for testing purposes and should never be how sku validation is implemented. This should be a backend functionality or at least read from some file.
function skuIsValid(sku) {
    var skuCleaned = sku.replace(/ - /g, "\-").trim();
    var allowableSkusMegastring = "WTL-50P1-DDGFR WTL-50P1-DDR WTL-50P1-DDS1 WTL-50P1-DDRFS1 WTL-50P1-DDAFRDS1 WTL-50P1-DDGFAFRFS1 WTL-50P1-DDAFRFS1 WTL-50P1-DDG WTL-50P1-DDGDADR WTL-50P1-DDGDADRDS1 WTL-50P1-DDGDAFR WTL-50P1-DDGDR WTL-50P1-DDGDRDS1 WTL-50P2-MCDGDRDS1 WTL-50P2-MCDGDADRDS1 WTL-50P2-MCDGDAFR WTL-50P2-MCDGDAFRFS1 WTL-50P2-MCDR WTL-50P2-FRR WTL-50P2-MCDADRDS1 WTL-50P2-MCDGDR WTL-50P2-MCDGFRFS1 WTL-50P1-DDA WTL-50P2-MCDGFADRFS1 WTL-50P2-MCDGFAFR WTL-50P2-FDGDRFS1 WTL-50P2-FDG WTL-50P1-MCFAFS1 WTL-50P1-MCDAFS1 WTL-50P2-DDADR WTL-50P1-DDADGDR WTL-50P2-MCDADRDG WTL-50P1-DDADS1 WTL-50P2-MCDADRDGFS1 WTL-50P1-MCDADGDR WTL-50P2-DDA WTL-50P1-DDADGDRDB WTL-50P1-DFRDG WTL-50P2-MCDGDADRDW WTL-50P1-DFB WTL-50P1-MCRG WTL-50P2-MCRA WTL-50P2-MCFA WTL-50P2-MCFR WTL-50P2-MCDA WTL-50P2-MCFRFS1 WTL-50P1-MCFAFRFS1 WTL-50P1-MCDB WTL-50P1-DDRDG WTL-50P1-DDAFS1 WTL-50P1-MCFB WTL-50P1-MCDRDADG WTL-50P1-DFAFRDS1 WTL-50P1-MCDRDS1 WTL-50P1-MCFGFAFR WTL-50P1-MCDRDADGDS1 WTL-50P1-MCDADR WTL-50P1-DFADS1 WTL-50P1-DFAFS1 WTL-50P1-DDRDS1 WTL-50P2-FDADRDS1 WTL-50P1-MCDGDRDS1 WTL-50P1-MCDGDAFRFS1 WTL-50P1-MCDGDARR WTL-50P1-MCDGDAFR WTL-50P1-MCDGDR WTL-50P1-MCDGDADRFS1 WTL-50P1-MCDGDRFS1 WTL-50P1-MCDGFADR WTL-50P1-MCDGFADRFS1 WTL-50P1-MCDG WTL-50P1-MCDGDADRDS1 WTL-50P1-DRRFS1 WTL-50P1-DFRDS1 WTL-50P1-DFRFS1 WTL-50P1-DFS1 WTL-50P1-DRA WTL-50P1-DFR WTL-50P1-DFA WTL-50P1-DRR WTL-50P1-FDGFAFRFS1 WTL-50P1-FDG WTL-50P1-MCDGFAFRFS1 WTL-50P2-DDGFR WTL-50P1-MCDGFAFR WTL-50P2-DDGDRDS1 WTL-50P2-DFR WTL-50P2-DFRFS1 WTL-50P2-DRR WTL-50P2-DFA WTL-50P2-DDR WTL-50P1-MCDR WTL-50P2-DDGDARRDS1 WTL-50P1-MCDGRR WTL-50P1-MCDGFRFS1 WTL-50P1-MCDWDRDS1 WTL-50P1-MCDGRADRDS1 WTL-50P1-MCDGRAFR WTL-50P1-MCDGFR WTL-50P1-MCRR WTL-50P1-MCRRFS1 WTL-50P1-MCFRFS1 WTL-50P2-DDG WTL-50P2-DDGDAFR WTL-50P1-MCFS1 WTL-50P1-MCFA WTL-50P1-MCFR WTL-50P1-MCFRDS1 WTL-50P1-MCDADWDBDR WTL-50P1-DDGDBFR WTL-50P1-MCDGFBFR WTL-50P1-MCRBRARGRR WTL-50P1-MCFRDG WTL-50P1-DDRDA WTL-50P1-DFGFRDS1 WTL-50P1-DFWFAFBFR WTL-50P1-MCRB WTL-50P1-DDRDADS1 WTL-50P1-DDRDADGDS1 WTL-50P1-DFAFR WTL-50P1-DFGFR WTL-50P1-DDBFADRFS1 WTL-50P1-MCDGFB WTL-50P1-DFRFADGFS1 WTL-50P1-DFBFS1 WTL-50P1-DDAFARR WTL-50P2-MCDRDG WTL-50P1-DDRDADG WTL-50P2-MCDADS1 WTL-50P2-DDRDG WTL-50P2-MCFAFRFS1 WTL-50P1-DDWDGDB WTL-50P2-MCDADR WTL-50P2-DDADRDGDS1 WTL-50P1-MCRBRRRARG WTL-50P1-DFAFBFR WTL-50P1-MCRBRGRARR WTL-50P1-MCFGFAFRFS1 WTL-50P1-MCFRDGFS1 WTL-50P2-DFAFR WTL-50P2-DRB WTL-50P2-MCDRDS1 WTL-50P1-MCDRDGFS1 WTL-50P1-MCDRDG WTL-50P1-DDB WTL-50P2-MCDADBDGDR WTL-50P1-MCFAFR WTL-50P1-MCDA WTL-50P2-MCDGDBDADR WTL-50BP1 WTL-50BP2 WTL-50P1-MCDRDR WTL-50P1-DDWDGDBDR WTL-50P1-MCDGDADRDR WTL-50P1-DDGFARRDB WTL-50P1-DDRDADGFS1 WTL-50P1-DDADBDRDW WTL-50P1-MCDBDA WTL-50P1-DFBFW WTL-50P1-DDGDBDADR WTL-50P1-FDGDBFR WTL-50P1-DDRDADBDG WTL-50P1-FDRDBDG WTL-50P1-DDRDWDA WTL-50P1-MCDADBDGDR WTL-50P1-MCFG WTL-50P1-DFGFS1 WTL-50P1-MCDGDAFB WTL-50P1-FDADRFAFS1 WTL-50P1-MCDBFS1 WTL-50P1-DDBDADGDW WTL-50P2-MCDGDBDRDS1 WTL-50P1-DDBFADR WTL-50P1-MCFGFBFR WTL-50P1-MCDBDAFRDS1 WTL-50P1-MCFBFAFRFS1 WTL-50P1-MCDGDADR WTL-50P2-DDGDADR WTL-50P2-DDGDR WTL-50P2-DRBRRFS1 WTL-50P2-MCDGDADR WTL-50P2-MCDRDADG WTL-50P1-MCRADR WTL-50P1-MCRRRR WTL-50P1-MCRBDS1 WTL-50P1-MCFBFS1 WTL-50P1-MCDGFRRA WTL-50P1-DDBDRFA WTL-50P2-MCDGDADS1 WTL-50P1-DFRFA WTL-50P1-MCRRRBDS1 WTL-50P1-MCFBFAFR WTL-50P2-MCFAFBFR WTL-50P2-MCFAFG WTL-50P1-MCRBRRDS1 WTL-50P2-MCDRDBDA WTL-50P2-MCDRDBDADW WTL-50P1-MCFRDBFADG WTL-50P2-MCFBFS1 WTL-50P1-DFRDGDA WTL-50BP3 WTL-50BP1 WTL-50BP2 WTL-50P2-DDGRRFBRA WTL-50P1-MCDR WTL-50P2-MCDGDADR WTL-50P2-MCDRDADG WTL-50P1-MCFRFS3 WTL-50P2-MCDGFRFS3 WTL-50P2-MCDRDS3 WTL-50P2-FDG WTL-50P1-MCFR WTL-50P1-MCDRDADG WTL-50P1-MCFA WTL-50P2-MCDA WTL-50P1-MCFBFAFRFS3 WTL-50P2-MCFR WTL-50P2-MCFRFS3 WTL-50P1-MCDRDG WTL-50P2-MCDR WTL-50P2-MCFAFG WTL-50P1-DDADBRRDG WTL-50P1-DDWDGDB WTL-50P1-DFRFS3 WTL-50P1-MCDGDADR WTL-50P3-MCDGDADR WTL-50P3-DDWDGDBDR WTL-50P3-DDA WTL-50P3-DDGDR WTL-50P3-DDR WTL-50P3-DDRDADG WTL-50P3-DDRDG WTL-50P3-DDWDGDB WTL-50P3-DFR WTL-50P1-MCDGDB WTL-50P3-DFRFS3 WTL-50P3-DFWFAFBFR WTL-50P3-DRR WTL-50P3-MCDA WTL-50P3-MCDB WTL-50P3-MCDGDR WTL-50P3-MCDGFBFR WTL-50P3-MCDR WTL-50P3-MCDRDADG WTL-50P3-MCFA WTL-50P3-MCFR WTL-50P3-MCFRFS3 WTL-50P3-DDRDADG WTL-50P1-DDA WTL-50P1-DDGDADR WTL-50P1-DDGDR WTL-50P1-DDR WTL-50P1-DDRDADG WTL-50P1-DDRDG WTL-50P1-DDWDGDBDR WTL-50P1-DFR WTL-50P1-DFWFAFBFR WTL-50P1-DRR WTL-50P1-MCDA WTL-50P1-MCDB WTL-50P1-MCDGDR WTL-50P1-MCDGFBFR WTL-50P2-MCFBFS3 WTL-50P1-DFRDS3 WTL-50P1-DDGDADRDB WTL-50P1-DFRFADGFS3 WTL-50P1-MCDGDADRFS3 WTL-50P1-MCFRDS3 WTL-50P1-DDGDAFRFS3 WTL-50P2-MCDGDADRDS3 WTL-50P2-DFAFR WTL-50P1-FFA WTL-50P2-DDGDR WTL-50P1-DDADS3 WTL-50P3-DFA WTL-50P1-MCFAFS3 WTL-50P1-MCDADR WTL-50P1-MCRGRARRDS3 WTL-50P1-FFADS3 WTL-50P2-MCDG WTL-50P1-DFS3 WTL-50P1-MCDRDBDS3 WTL-50P2-MCFA WTL-50P1-DDADRDG WTL-50P2-MCFAFR WTL-50P3-MCFCBDS3 WTL-50P2-MCDADRDS3 WTL-50P1-MCDCADCGDCRDS3 WTL-50P2-DDRDADGDS3 WTL-50P3-MCDRDS3 WTL-50P3-MCFAFR WTL-50P1-MCDAFB WTL-50P1-MCFB WTL-50P1-MCRRFS3 WTL-50P1-MCFBFS3 WTL-50P1-MCFRFADCG WTL-50P1-DRARR WTL-50P2-FDRDRDR WTL-50P2-MCDRDADGDS3 WTL-50P1-MCRADR WTL-50P1-DFAFS3 WTL-50P2-DDGDB WTL-50P1-DFA WTL-50P2-DRRDAFS3 WTL-50P1-MCDGFAFRFS3 WTL-50P1-FDA WTL-50P1-MCDGFB WTL-50P2-DDADR WTL-50P1-FDA WTL-50P1-DDBDAFRDS3 WTL-50P1-MCFAFRDS3 WTL-50P1-MCDRDS3 WTL-50P2-DDR WTL-50P3-MCFAFRDGDS3 WTL-50P2-MCDGDR WTL-50P3-DDBDBDAFS3 WTL-50P1-DDWDGDAFRDS3 WTL-50P1-MCDGFADR WTL-50P1-MCDGDAFRFS3 WTL-50P1-MCDGDAFR WTL-50P2-DFRFS3 WTL-50P2-DDGFA WTL-50P1-DRRDS3 WTL-50P2-MCDRDBDADW WTL-50P2-MCDADR WTL-50P2-DFA WTL-50P2-DDGDADR WTL-50P1-DDRDS3 WTL-50P3-DDRDS3 WTL-50P1-DDGDADRDS3 WTL-50P1-MCFAFBFR WTL-50P2-DDBDRDS3 WTL-50P3-DFW WTL-50P2-FDADR WTL-50P1-MCDRDR WTL-50P3-FFGFAFR WTL-50P3-FDGDADR WTL-50P1-MCDGDRDBFS3 WTL-50P1-MCDGFCBRR WTL-50P1-MCDRDADBDM3 WTL-50P3-DDGDCADR";
    var allowableSkusArray = allowableSkusMegastring.split(" ");

    for (var i = 0; i < allowableSkusArray.length; i++) {
        console.log("Comparing " + skuCleaned + " to " + allowableSkusArray[i]);
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