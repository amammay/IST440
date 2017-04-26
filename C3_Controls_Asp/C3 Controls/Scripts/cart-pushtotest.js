// Categories
const CATEGORY_BASIC = "item_basic";
const CATEGORY_VOLTAGE = "item_voltage";
const CATEGORY_LAMP_COLOR = "item_lamp_color";
const CATEGORY_LENS_COLOR = "item_lens_color";
const CATEGORY_CLAMP_RING = "item_clamp_ring";
const CATEGORY_LENS_TYPE = "item_lens_type";
const CATEGORY_OPTION = "item_option";

// Containers
const CONTAINER_BASIC = "container_basic";
const CONTAINER_VOLTAGE = "container_voltage";
const CONTAINER_LAMP_COLOR = "container_lamp_color";
const CONTAINER_LENS_COLOR = "container_lens_color";
const CONTAINER_CLAMP_RING = "container_clamp_ring";
const CONTAINER_LENS_TYPE = "container_lens_type";
const CONTAINER_OPTION = "container_option";

// Columns
const COLUMN_BASIC = "col_basic";
const COLUMN_VOLTAGE = "col_voltage";
const COLUMN_LAMP_COLOR = "col_lamp";
const COLUMN_LENS_COLOR = "col_lens";
const COLUMN_CLAMP_RING = "col_clamp";
const COLUMN_LENS_TYPE = "col_lens";
const COLUMN_OPTION = "col_option";

// Parent Columns (for filtering)
const SLIDE_VOLTAGE = "slide_voltages";
const SLIDE_LAMP_COLOR = "slide_lamp_colors";
const SLIDE_LENS_TYPE = "slide_lens_type";

// Cart Object
var Cart = {
    price: 0,
    operator: "",
    basicOperator: "",
    voltage: "",
    lampColor: "",
    lensColor: "",
    clampRing: "",
    lensType: "",
    option: "",

    getSku: function () {
        return "Sku: " + this.basicOperator + this.operator + + this.voltage +
            this.lampColor +  this.clampRing + this.lensType + this.lensColor + this.option;
    },

    getPrice: function () {
        return "Price: $" + this.price;
    },

    subtractPrice: function (p) {
        this.price -= parseFloat(p);
    },

    updatePrice: function (p) {
        this.price += parseFloat(p);
    },

    setBasic: function (b) {
        this.basicOperator = b;
    },

    setOperator: function (o) {
        this.operator = o;
    },

    setVoltage: function (v) {
        this.voltage = v;
    },

    setLampColor: function (l) {
        this.lampColor = l;
    },

    setClampRing: function (c) {
        this.clampRing = "-" + c;
    },

    setLensType: function (l) {
        this.lensType = l;
    },

    setLensColor: function (l) {
        this.lensColor = l;
    },

    setOption: function (o) {
        this.option = "-" + o;
    }
};


/**
 * Sets up the cart object.
 * @param _operator The sku of the operator part
*/
function setup(_operator) {
    Cart.setOperator(_operator);
    displayCartUpdates();
}

/**
 * Allows item to be dragged/dropped.
 * @param ev Event data
*/
function allowDrop(ev) {
    ev.preventDefault();

    // Check if item is selected to show remove container
    var child = document.getElementById(ev.target.id);
    if (child.dataset.selected == 'true') {
        $("#remove_container").removeClass("hidden");
    }
}

/**
 * Stores element's id when being dragged.
 * @param ev Event data
*/
function dragPart(ev) {
    ev.dataTransfer.setData("part", ev.target.id);
}


function manualInputRetrieve() {
    var tempSkuArray = new Array();
    var pttOperatorSku = document.getElementById("pttOperatorSku").value.toUpperCase();
    if (pttOperatorSku == "") {
        showErrorModal("Warning, please provide a input for the Operator");
        return null;
    }

    var voltageBasedOperatorSku = document.getElementById("voltageBasedOperatorSku").value.toUpperCase();
    if (pttOperatorSku == "") {
        showErrorModal("Warning, please provide a input for the voltage");
        return null;
    }
    var lampTypeSku = document.getElementById("lampTypeSku").value.toUpperCase();
    if (pttOperatorSku == "") {
        showErrorModal("Warning, please provide a input for the lamp type");
        return null;
    }
    var clampRingSku = document.getElementById("clampRingSku").value.toUpperCase();
    if (pttOperatorSku == "") {
        showErrorModal("Warning, please provide a input for the clamp ring");
        return null;
    }
    var lensTypeSku = document.getElementById("lensTypeSku").value.toUpperCase();
    if (pttOperatorSku == "") {
        showErrorModal("Warning, please provide a input for the lens type");
        return null;
    }
    var lensColorSku = document.getElementById("lensColorSku").value.toUpperCase();
    if (pttOperatorSku == "") {
        showErrorModal("Warning, please provide a input for the lens color");
        return null;
    }
    var optionsSku = document.getElementById("optionsSku").value.toUpperCase();
    if (pttOperatorSku == "") {
        showErrorModal("Warning, please provide a input for the options");
        return null;
    }

    tempSkuArray.push(pttOperatorSku);
    tempSkuArray.push(voltageBasedOperatorSku);
    tempSkuArray.push(lampTypeSku);
    tempSkuArray.push(clampRingSku);
    tempSkuArray.push(lensTypeSku);
    tempSkuArray.push(lensColorSku);
    tempSkuArray.push(optionsSku);

    return tempSkuArray;
}


function submitManualSkuPtt() {
    var getInputs = manualInputRetrieve();


    for (var i = 0; i < getInputs.length; i++) {

        //temp value for checking to see if the input is empty
        var checkValue = getInputs[i];

        //serach for a matching data sku that was inputed 
        var searchItem = "[data-sku~=" + "'" + checkValue.toUpperCase() + "'" + "]";

        //query for a child
        var childQuery = document.querySelectorAll(searchItem);
        var id = childQuery[0].id;
        var container = getContainer(id);

        // Add child to proper container
        document.getElementById(container).appendChild(childQuery[0]);

        // Set the correct sku for child
        setProperSku(container, childQuery[0].dataset.sku);

        // Filter other tiles using the container and the name of the item
        applyContraints(container, id);

        // Set the data selected to true
        childQuery[0].dataset.selected = true;
        Cart.updatePrice(childQuery[0].dataset.price);

        // Update text on screen
        displayCartUpdates();

    }

}


/**
 * When item is dropped in cart container.
 * @param ev Event data
*/
function dropInCart(ev) {
    ev.preventDefault();

    // Get id of dragged item and the element
    var selectedId = ev.dataTransfer.getData("part");
    var child = document.getElementById(selectedId);

    // Get the container the item should be placed in
    var container = getContainer(selectedId);

    // Check if user already picked an item for this category
    if (hasItems(container)) {
        showWarningModal('Warning', 'You\'ve already selected an item!');
        checkRemoveContainer();
        return;
    }

    // Add child to proper container
    document.getElementById(container).appendChild(child);

    // Set the correct sku for child
    setProperSku(container, child.dataset.sku);

    // Filter other tiles using the container and the name of the item
    applyContraints(container, selectedId);

    // Set the data selected to true
    child.dataset.selected = true;
    Cart.updatePrice(child.dataset.price);

    // Update text on screen
    displayCartUpdates();

}

/**
 * When item is clicked add to cart.
 * @param ev Event data
*/
function clickInCart(ev) {
    ev.preventDefault();

    // Get id of dragged item and the element
    var selectedId = ev.currentTarget.id;
    var child = document.getElementById(selectedId);

    // Get the container the item should be placed in
    var container = getContainer(selectedId);

    // Check if user already picked an item for this category
    if (hasItems(container)) {
        showWarningModal('Warning', 'You\'ve already selected an item!');
        checkRemoveContainer();
        return;
    }

    // Add child to proper container
    document.getElementById(container).appendChild(child);

    // Set the correct sku for child
    setProperSku(container, child.dataset.sku);

    // Filter other tiles using the container and the name of the item
    applyContraints(container, selectedId);

    // Set the data selected to true
    child.dataset.selected = true;
    Cart.updatePrice(child.dataset.price);

    // Update text on screen
    displayCartUpdates();

}


/**
 * Triggered when an item is dropped inside 
 * the remove container.
 * @param e Event data
*/
function dropInRemoveContainer(e) {
    var selectedId = e.dataTransfer.getData("part");
    var child = document.getElementById(selectedId);
    var correctColumn = getSelectionColumn(selectedId) +
        "_" + getProductName(selectedId);

    // Make sure the child is a selected child
    if (child.dataset.selected == 'true') {
        // Set child as not selected anymore
        child.dataset.selected = false;

        // Append child to its original parent
        var parent = document.getElementById(correctColumn);
        parent.appendChild(child);

        setProperSku(getContainer(selectedId), "");
        Cart.substractPrice(child.dataset.price);
    }

    // Show text updates
    displayCartUpdates();

    // Make remove container invisible
    $("#remove_container").addClass("hidden");
}


/**
 * Makes the remove container hidden if 
 * it is shown and if child is selected.
 * @param _child Child
*/
function checkRemoveContainer() {
    var removeContainer = $("#remove_container");
    if (!removeContainer.hasClass("hidden"))
        removeContainer.addClass("hidden");
}


/**
 * Get the original column the item
 * was dragged from.
 * @param _id The id of the child item
*/
function getSelectionColumn(_id) {
    if (_id.indexOf(CATEGORY_BASIC) >= 0)
        return COLUMN_BASIC;
    else if (_id.indexOf(CATEGORY_VOLTAGE) >= 0)
        return COLUMN_VOLTAGE;
    else if (_id.indexOf(CATEGORY_LAMP_COLOR) >= 0)
        return COLUMN_LAMP_COLOR;
    else if (_id.indexOf(CATEGORY_LENS_COLOR) >= 0)
        return COLUMN_LENS_COLOR;
    else if (_id.indexOf(CATEGORY_CLAMP_RING) >= 0)
        return COLUMN_CLAMP_RING;
    else if (_id.indexOf(CATEGORY_LENS_TYPE) >= 0)
        return COLUMN_LENS_TYPE;
    else if (_id.indexOf(CATEGORY_OPTION) >= 0)
        return COLUMN_OPTION;
    else
        return null;
}


/**
 * Get the name of the child item 
 * from its id.
 * @param _id The id of the child item
*/
function getProductName(_id) {
    var name = _id.split("_");
    var newName = name[2];
    if (name.length > 3) {
        for (var i = 3; i < name.length; i++) {
            newName += "_" + name[i];
        }
    }
    return newName;
}


/**
 * Finds the correct container a child
 * should belong in by using its id.
 * @param _id Child's id
 * @return Container to which the child should go into
*/
function getContainer(_id) {
    if (_id.indexOf(CATEGORY_BASIC) >= 0)
        return CONTAINER_BASIC;
    else if (_id.indexOf(CATEGORY_VOLTAGE) >= 0)
        return CONTAINER_VOLTAGE;
    else if (_id.indexOf(CATEGORY_LAMP_COLOR) >= 0)
        return CONTAINER_LAMP_COLOR;
    else if (_id.indexOf(CATEGORY_LENS_COLOR) >= 0)
        return CONTAINER_LENS_COLOR;
    else if (_id.indexOf(CATEGORY_CLAMP_RING) >= 0)
        return CONTAINER_CLAMP_RING;
    else if (_id.indexOf(CATEGORY_LENS_TYPE) >= 0)
        return CONTAINER_LENS_TYPE;
    else if (_id.indexOf(CATEGORY_OPTION) >= 0)
        return CONTAINER_OPTION;
    else
        return null;
}

/**
 * Sets the correct sku in the cart object
 * based on the child's container.
 * @param _container Container of the child
 * @param _sku The sku to insert
*/
function setProperSku(_container, _sku) {
    switch (_container) {
    case CONTAINER_BASIC:
        Cart.setBasic(_sku);
        break;
    case CONTAINER_VOLTAGE:
        Cart.setVoltage(_sku);
        break;
    case CONTAINER_LAMP_COLOR:
        Cart.setLampColor(_sku);
        break;
    case CONTAINER_LENS_COLOR:
        Cart.setLensColor(_sku);
        break;
    case CONTAINER_CLAMP_RING:
        Cart.setClampRing(_sku);
        break;
    case CONTAINER_LENS_TYPE:
        Cart.setLensType(_sku);
        break;
    case CONTAINER_OPTION:
        Cart.setOption(_sku);
        break;
    }
}

/**
 * Determines what constraints should be
 * applied.
 * @param _container Child's container
 * @param _id Child's id
*/
function applyContraints(_container, _id) {
    switch (_container) {
    case CONTAINER_BASIC:
        filterVoltages(_id);
        break;
    case CONTAINER_VOLTAGE:
        filterLampColors(_id);
        break;
    case CONTAINER_CLAMP_RING:
        filterLensTypes(_id);
        break;
    }
}

/**
 * Determines what constraints should be
 * removed.
 * @param _container Child's container
 * @param _id Child's id
*/
function removeContraints(_container, _id) {
    switch (_container) {
    case CONTAINER_BASIC:
        unFilterVoltages(_id);
        break;
    case CONTAINER_VOLTAGE:
        unFilterLampColors(_id);
        break;
    case CONTAINER_CLAMP_RING:
        unFilterLensTypes(_id);
        break;

    }
}

/**
 * Filters the correct voltages based on
 * the child's id. Id should be one of the
 * basic operators.
 * @param _id Child's id
*/
function filterVoltages(_id) {
    var tiles = getTiles(SLIDE_VOLTAGE);
    var allowed;
    switch (_id) {
    case "item_basic_full_voltage":
        allowed = ['6V AC/DC', '12V AC/DC', '24V AC/DC', '120V AC/DC'];
        break;
    case "item_basic_transformer_(50/60_hz)":
        allowed = ['120V AC', '240V AC', '277V AC', '480V AC'];
        break;
    case "item_basic_resistor":
        allowed = ['120V AC/DC', '240V AC/DC', '480V AC/DC'];
        break;
    }
    filter(tiles, allowed);
}

/**
 * Filters the correct lamp colors based on
 * the child's id. Id should be one of the
 * voltages.
 * @param _id Child's id
*/
function filterLampColors(_id) {
    var tiles = getTiles(SLIDE_LAMP_COLOR);
    var allowed = ['No Lamp', 'Clear Incandescent', 'Amber LED',
        'Blue LED', 'Green LED', 'Red LED', 'White LED'];
    switch (_id) {
    case "item_voltage_6v_ac/dc":
    case "item_voltage_120v_ac":
    case "item_voltage_240v_ac":
    case "item_voltage_277v_ac":
    case "item_voltage_480v_ac":
        allowed.push('Clear Flashing Incandescent');
        break;
    case "item_voltage_120v_ac/dc":
    case "item_voltage_240v_ac/dc":
    case "item_voltage_480v_ac/dc":
        allowed.push('Neon Green');
        allowed.push('Neon Red');
        break;
    }
    filter(tiles, allowed);
}

/**
 * Filters the correct lens type based on
 * the child's id. Id should be one of the
 * clamp rings.
 * @param _id Child's id
*/
function filterLensTypes(_id) {
    switch (_id) {
    case "item_clamp_ring_aluminum_(type_4)":
        document.getElementById("col_lens_type_guarded_illuminated_lens").hidden = true;
        document.getElementById("col_lens_type_shrouded_illuminated_mushroom_lens").hidden = true;
        break;
    }
}


/**
 * Filters out tiles where the title attribute of
 * the image element doesn't match any of the allowed
 * names.
 * @param _tiles Collection of tiles to filter
 * @param _allowed Collection of names to not filter
*/
function filter(_tiles, _allowed) {
    for (var i = 0; i < _tiles.length; i++) {
        var found = false;
        var title = _tiles[i].getElementsByTagName("img")[0].getAttribute("title");
        for (var j = 0; j < _allowed.length; j++) {
            if (title == _allowed[j]) {
                found = true;
                break;
            }
        }
        if (!found)
            _tiles[i].hidden = true;
    }
}


/**
 * unFilters the correct voltages based on
 * the child's id. Id should be one of the
 * basic operators.
 * @param _id Child's id
*/
function unFilterVoltages(_id) {
    var tiles = getTiles(SLIDE_VOLTAGE);
    var allowed;
    switch (_id) {
    case "item_basic_full_voltage":
        allowed = ['120V AC/DC', '240V AC/DC', '480V AC/DC', '6V AC/DC', '12V AC/DC', '24V AC/DC', '120V AC/DC', '120V AC', '240V AC', '277V AC', '480V AC'];
        break;
    case "item_basic_transformer_(50/60_hz)":
        allowed = ['120V AC/DC', '240V AC/DC', '480V AC/DC', '6V AC/DC', '12V AC/DC', '24V AC/DC', '120V AC/DC', '120V AC', '240V AC', '277V AC', '480V AC'];
        break;
    case "item_basic_resistor":
        allowed = ['120V AC/DC', '240V AC/DC', '480V AC/DC', '6V AC/DC', '12V AC/DC', '24V AC/DC', '120V AC/DC', '120V AC', '240V AC', '277V AC', '480V AC'];
        break;
    }
    unfilter(tiles, allowed);
}

/**
 * unFilters the correct lamp colors based on
 * the child's id. Id should be one of the
 * voltages.
 * @param _id Child's id
*/
function unFilterLampColors(_id) {
    var tiles = getTiles(SLIDE_LAMP_COLOR);
    var allowed = ['No Lamp', 'Clear Incandescent', 'Amber LED',
        'Blue LED', 'Green LED', 'Red LED', 'White LED', 'Clear Flashing Incandescent', 'Neon Green', 'Neon Red'];
    switch (_id) {
    case "item_voltage_6v_ac/dc":
    case "item_voltage_120v_ac":
    case "item_voltage_240v_ac":
    case "item_voltage_277v_ac":
    case "item_voltage_480v_ac":
    case "item_voltage_120v_ac/dc":
    case "item_voltage_240v_ac/dc":
    case "item_voltage_480v_ac/dc":
        break;
    }
    unfilter(tiles, allowed);
}

/**
 * unFilters the correct lens type based on
 * the child's id. Id should be one of the
 * clamp rings.
 * @param _id Child's id
*/
function unFilterLensTypes(_id) {
    switch (_id) {
    case "item_clamp_ring_aluminum_(type_4)":
        document.getElementById("col_lens_type_guarded_illuminated_lens").hidden =false;
        document.getElementById("col_lens_type_shrouded_illuminated_mushroom_lens").hidden = false;
        break;
    }
}



/**
 * unFilters out tiles when removing a item
 * @param _tiles Collection of tiles to filter
 * @param _allowed Collection of names to not filter
*/
function unfilter(_tiles, _allowed) {
    for (var i = 0; i < _tiles.length; i++) {
        for (var j = 0; j < _allowed.length; j++) {
            _tiles[i].hidden = false;
        }
    }
}

/**
 * Gets a collection of children from all columns
 * in a slide.
 * @param _slide Slide to get children from
 * @return Collection of tiles
*/
function getTiles(_slide) {
    var container = document.getElementById(_slide);
    var columns = container.getElementsByClassName("metro-column");
    var children = new Array();
    for (var i = 0; i < columns.length; i++) {
        var items = columns[i].getElementsByClassName("metro");
        for (var j = 0; j < items.length; j++)
            children.push(items[j]);
    }
    return children;
}

/**
 * Checks if a container already has items in it.
 * @param _container Container to check
 * @return Boolean
*/
function hasItems(_container) {
    var container = document.getElementById(_container);
    return container.getElementsByClassName("metro").length > 0;
}

/**
 * Shows a normal modal.
 * @param _title Title of the modal
 * @param _content Content of the modal
*/
function showModal(_title, _content) {
    var modal = $("#custom_modal");
    var modalTitle = document.getElementById("modal_title");
    var modalContent = document.getElementById("modal_body");
    var modalHeader = $("#dialog_header");
    if (modalHeader.hasClass("part-modal-warning"))
        modalHeader.removeClass("part-modal-warning");
    if (modalHeader.hasClass("part-modal-error"))
        modalHeader.removeClass("part-modal-error");
    modalTitle.innerHTML = _title;
    modalContent.innerHTML = _content;
    modal.modal();
}

/**
 * Shows a warning modal.
 * @param _title Title of the modal
 * @param _content Content of the modal
*/
function showWarningModal(_title, _content) {
    var modal = $("#custom_modal");
    var modalTitle = document.getElementById("modal_title");
    var modalContent = document.getElementById("modal_body");
    var modalHeader = $("#dialog_header");
    if (modalHeader.hasClass("part-modal-error"))
        modalHeader.removeClass("part-modal-error");
    if (!modalHeader.hasClass("part-modal-warning"))
        modalHeader.addClass("part-modal-warning");
    modalTitle.innerHTML = _title;
    modalContent.innerHTML = _content;
    modal.modal();
}

/**
 * Shows an error modal.
 * @param _title Title of the modal
 * @param _content Content of the modal
*/
function showErrorModal(_title, _content) {
    var modal = $("#custom_modal");
    var modalTitle = document.getElementById("modal_title");
    var modalContent = document.getElementById("modal_body");
    var modalHeader = $("#dialog_header");
    if (modalHeader.hasClass("part-modal-warning"))
        modalHeader.removeClass("part-modal-warning");
    if (!modalHeader.hasClass("part-modal-error"))
        modalHeader.addClass("part-modal-error");
    modalTitle.innerHTML = _title;
    modalContent.innerHTML = _content;
    modal.modal();
}

/**
 * Updates the sku and price text on the page
 * to reflect thereof its cart counterpart.
*/
function displayCartUpdates() {
    $("#title_text_price").html(Cart.getPrice());
    $("#title_text_sku").html(Cart.getSku());
}


/**
 * Finds all divs with the 'metro' class that are
 * selected.
 * @return Array of selected divs
*/
function findSelectedItems() {
    var items = document.getElementsByClassName("metro");
    var selItems = new Array();
    for (var i = 0; i < items.length; i++) {
        if (items[i].dataset.selected == 'true')
            selItems.push(items[i]);
    }
    return selItems;
}

/**
 * Shows a modal with all the selected items.
*/
function showSelectedItems() {
    var content = "";
    var img, title, price;
    var items = findSelectedItems();
    for (var i = 0; i < items.length; i++) {
        img = items[i].getElementsByTagName("img")[0];
        title = img.getAttribute("title");
        price = items[i].dataset.price;
        content += "Item: " + title + "<br />" + "Cost: $" + price + "<br /><br />";
    }
    content += "<b>Total Price: </b>$" + Cart.price;
    showModal('Product Details', content);
}


/*
 * Functions to show details for the individual parts 
 */
function showOperatorDesc() {
    showModal('Operator Description', 'Choose from one of three options (Full Voltage, Transformer, Resistor) that you wish ' +
        'to have in use for your light unit. Please note that your selection will limit the available voltages.' +
        '<br/><br/><b>Prices</b>' + '<br/>Full Voltage: $46.00' + '<br/>Transformer: $68.00' + '<br/>Resistor: $57.00');
}

function showVoltageDesc() {
    showModal('Voltage Description', 'The voltage intensity that you would like to have for your product. Please note that this will limit the lamp colors ' +
        'you will have available.' + '<br/><br/><b>Prices</b>' + '<br/>There is no additional cost for any voltage option selected.');
}

function showLampTypeDesc() {
    showModal('Lamp Type/Color Description', 'The lamp type (if none) or color that you wish to use in the position of your lamp.' +
        ' <br/><br/><b>Prices</b>' + "<br/>LED (Any color): $18.00" + '<br/>Incandescent: $7.10' + '<br/>Neon Green or Red: $0.00' + 
        '<br/>No Lamp: -$1.80');
}

function showClampRingDesc() {
    showModal('Clamp Ring Description', 'The clamp ring type you would like to have for your product. Please note that if Aluminum is selected, ' +
        'it will limit your available lens types.' + '<br/><br/><b>Prices</b>' + '<br/>Black Polyester (Type 4x): $0.00' +
        '<br/>Aluminum (Type 4): $4.30');
}

function showLensTypeDesc() {
    showModal('Lens Type Description', 'The lens type you wish your product to have.' + '<br/><br/><b>Prices</b>' +
        '<br/>Illuminated Push Button Lens: $24.00' + '<br/>Illuminated Push Button Mushroom Lens: $36.00' +
        '<br/>Guarded Illuminated Push Button Lens: $36.00' + '<br/>Shrouded Illuminated Push Button Mushroom Lens: $48.00');
}

function showLensColorDesc() {
    showModal('Lens Color Description', 'The color of the lens for the lens type that you wish your product to have.' +
        '<br/><br/><b>Prices</b>' + '<br/>There is no additional cost for any lens color selected.');
}

function showOptionsDesc() {
    showModal('Options Description', 'An option to have a case added to your product to help prevent accidental bumping.' +
        '<br/><br/><b>Prices</b>' + '<br/>None: $0.00' + '<br/>IP20 Guard: $12.00');
}


/**
 *
 * Function for removing an operator
 * @param {any} event
 */
function removeOperator(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "basic";
    var selectedId, child, correctColumn, parent;

    for (var i = 0; i < item.length; i++) {

        var tempId = item[i].id;

        if (tempId.includes(subString)) {
            child = item[i];
            selectedId = tempId;
            correctColumn = getSelectionColumn(selectedId) +
                "_" + getProductName(selectedId);
        }
    }

    // Make sure the child is a selected child
    if (child.dataset.selected == 'true') {
        // Set child as not selected anymore
        child.dataset.selected = 'false';

            //// Append child to its original parent
            parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            var price = child.dataset.price;
            Cart.subtractPrice(price);
    
    }

    //Logic for remvoing a applied filter to a component line
    var container = getContainer(selectedId);
    removeContraints(container, selectedId);

    // Show text updates
    displayCartUpdates();
}

/**
 *
 * Function for removing an operator
 * @param {any} event
 */
function removeVoltage(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "voltage";
    var selectedId, child, correctColumn;

    for (var i = 0; i < item.length; i++) {

        var tempId = item[i].id;

        if (tempId.includes(subString)) {
            child = item[i];
            selectedId = tempId;
            correctColumn = getSelectionColumn(selectedId) +
                "_" + getProductName(selectedId);
        }
    }

    // Make sure the child is a selected child
    if (child.dataset.selected == 'true') {
        // Set child as not selected anymore
        child.dataset.selected = 'false';

            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
    }

    //Logic for remvoing a applied filter to a component line
    var container = getContainer(selectedId);
    removeContraints(container, selectedId);

    // Show text updates
    displayCartUpdates();
}

/**
 *
 * Function for removing an operator
 * @param {any} event
 */
function removeLamp(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "lamp_color";
    var selectedId, child, correctColumn;

    for (var i = 0; i < item.length; i++) {

        var tempId = item[i].id;

        if (tempId.includes(subString)) {
            child = item[i];
            selectedId = tempId;
            correctColumn = getSelectionColumn(selectedId) +
                "_" + getProductName(selectedId);
        }
    }

    // Make sure the child is a selected child
    if (child.dataset.selected == 'true') {
        // Set child as not selected anymore
        child.dataset.selected = 'false';
            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }
    

    // Show text updates

    displayCartUpdates();
}

/**
 *
 * Function for removing a clamp
 * @param {any} event
 */
function removeClamp(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "clamp";
    var selectedId, child, correctColumn;

    for (var i = 0; i < item.length; i++) {

        var tempId = item[i].id;

        if (tempId.includes(subString)) {
            child = item[i];
            selectedId = tempId;
            correctColumn = getSelectionColumn(selectedId) +
                "_" + getProductName(selectedId);
        }
    }

    // Make sure the child is a selected child
    if (child.dataset.selected == 'true') {
        // Set child as not selected anymore
        child.dataset.selected = 'false';
            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }

    //Logic for remvoing a applied filter to a component line
    var container = getContainer(selectedId);
    removeContraints(container, selectedId);

    // Show text updates
    displayCartUpdates();
}

/**
 *
 * Function for removing an operator
 * @param {any} event
 */
function removeLensType(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "lens_type";
    var selectedId, child, correctColumn;

    for (var i = 0; i < item.length; i++) {

        var tempId = item[i].id;

        if (tempId.includes(subString)) {
            child = item[i];
            selectedId = tempId;
            correctColumn = getSelectionColumn(selectedId) +
                "_" + getProductName(selectedId);
        }
    }

    // Make sure the child is a selected child
    if (child.dataset.selected == 'true') {
        // Set child as not selected anymore
        child.dataset.selected = 'false';

            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }
    

    // Show text updates
    displayCartUpdates();
}

/**
 *
 * Function for removing a lens color
 * @param {any} event
 */
function removeLensColor(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "lens_color";
    var selectedId, child, correctColumn;

    for (var i = 0; i < item.length; i++) {

        var tempId = item[i].id;

        if (tempId.includes(subString)) {
            child = item[i];
            selectedId = tempId;
            correctColumn = getSelectionColumn(selectedId) +
                "_" + getProductName(selectedId);
        }
    }

    // Make sure the child is a selected child
    if (child.dataset.selected == 'true') {
        // Set child as not selected anymore
        child.dataset.selected = 'false';

            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }

    // Show text updates
    displayCartUpdates();
}

/**
 *
 * Function for removing an option
 * @param {any} event
 */
function removeOptions(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "option";
    var selectedId, child, correctColumn;

    for (var i = 0; i < item.length; i++) {

        var tempId = item[i].id;

        if (tempId.includes(subString)) {
            child = item[i];
            selectedId = tempId;
            correctColumn = getSelectionColumn(selectedId) +
                "_" + getProductName(selectedId);
        }
    }

    // Make sure the child is a selected child
    if (child.dataset.selected == 'true') {
        // Set child as not selected anymore
        child.dataset.selected = 'false';

            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }

    // Show text updates
    displayCartUpdates();
}

/**
 * Validates the user's selection and mocks a 
 * checkout proccess.
*/
function addToCart() {
    // Let's check if the user has selected everything we need to build the part
    if (!hasItems(CONTAINER_BASIC)) {
        showErrorModal('Missing Items', 'Please select a basic operator!');
        return;
    }
    if (!hasItems(CONTAINER_VOLTAGE)) {
        showErrorModal('Missing Items', 'Please select a voltage!');
        return;
    }
    if (!hasItems(CONTAINER_LAMP_COLOR)) {
        showErrorModal('Missing Items', 'Please select a lamp color!');
        return;
    }
    if (!hasItems(CONTAINER_LENS_COLOR)) {
        showErrorModal('Missing Items', 'Please select a lens color!');
        return;
    }
    if (!hasItems(CONTAINER_CLAMP_RING)) {
        showErrorModal('Missing Items', 'Please select a clamp ring!');
        return;
    }
    if (!hasItems(CONTAINER_LENS_TYPE)) {
        showErrorModal('Missing Items', 'Please select a lens type!');
        return;
    }
    if (!hasItems(CONTAINER_OPTION)) {
        showErrorModal('Missing Items', 'Please select a option!');
        return;
    }
    showModal('Success', 'Push-to-Test item added to cart!');
}