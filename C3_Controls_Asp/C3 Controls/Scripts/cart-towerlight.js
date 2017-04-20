// Categories
const CATEGORY_STYLE = "item_style";
const CATEGORY_VOLTAGE = "item_voltage";
const CATEGORY_POSITION = "item_positions";
const CATEGORY_SOUND = "item_sound";

// Containers
const CONTAINER_STYLE = "container_style";
const CONTAINER_VOLTAGE = "container_voltage";
const CONTAINER_POSITION = "container_positions";
const CONTAINER_SOUND = "container_sound";

// Columns
const COLUMN_STYLE = "col_style";
const COLUMN_VOLTAGE = "col_voltage";
const COLUMN_POSITION = "col_positions";
const COLUMN_SOUND = "col_soundMoudle";

// Position Constraints
const MAX_POSITIONS = 5;
const LAST_ALLOWED_POSITIONS = [
    "Sound Module Continuous (80/100 dB)",
    "Sound Module Intermittent (80/100 dB)"
];

// Variables



var Cart = {
    price: 8,
    operator: "",
    module: "",
    base: "",
    voltage: "",
    position: "",
    sound: "",

    // Generates sku in the correct order
    generateSku: function() {
        return "Sku: " + this.operator + this.module + this.base + this.voltage + this.position + this.sound;
    },

    generatePrice: function() {
        return "Price: $" + this.price;
    },

    updatePrice: function(p) {
        this.price += parseFloat(p);
    },

    subtractPrice: function(p) {
        this.price -= parseFloat(p);
    },

    setOperator(o) {
        this.operator = o;
    },

    setModule(m) {
        this.module = "-" + m;
    },

    setBase(b) {
        this.base = "-" + b;
    },

    setVoltage(v) {
        this.voltage = "-" + v;
    },

    addPosition(p) {
        this.position += "-" + p;
    },
    setSound(s) {
        this.sound = "-" + s;
    }
};


/**
 * Sets up the initial data needed for cart.
 * @param _operator The operator sku
 * @param _module The module sku
*/
function setup(_operator, _module) {
    Cart.setOperator(_operator);
    Cart.setModule(_module);
    updateDisplay();
}


/**
 * Enables the drag-and-drop functionality.
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
 * Triggered when dragging a part. Transfers the id
 * of that part (Div Element).
 * @param ev Event data
*/
function dragPart(ev) {
    ev.dataTransfer.setData("part", ev.target.id);
}

function manualInputRetrieve() {
    var tempSkuArray = new Array();
    var operatorSku = "WTL";
    var diameterSku = "50";
    var baseMaterial = document.getElementById("baseMaterialSku").value;
    var voltage = document.getElementById("voltageSku").value;
    var position1 = document.getElementById("positionSku1").value;
    var position2 = document.getElementById("positionSku2").value;
    var position3 = document.getElementById("positionSku3").value;
    var position4 = document.getElementById("positionSku4").value;
    var position5 = document.getElementById("positionSku5").value;
    var position6 = document.getElementById("positionSku6").value;

    tempSkuArray.push(operatorSku);
    tempSkuArray.push(diameterSku);
    tempSkuArray.push(baseMaterial);
    tempSkuArray.push(voltage);
    tempSkuArray.push(position1);
    tempSkuArray.push(position2);
    tempSkuArray.push(position3);
    tempSkuArray.push(position4);
    tempSkuArray.push(position5);
    tempSkuArray.push(position6);

    return tempSkuArray;


}

/**
 *
 * Retrieve manually entered in sku and build the product
 */
function submitManaulSku() {

    var getInputs = manualInputRetrieve();

    var tempOp, tempDiam, tempBase, tempVolt, tempPos1, tempPos2, tempPos3, tempPos4, tempPos5, tempPos6;

    tempOp = getInputs[0];
    tempDiam = getInputs[1];
    tempBase = getInputs[2];
    tempVolt = getInputs[3];
    tempPos1 = getInputs[4];
    tempPos2 = getInputs[5];
    tempPos3 = getInputs[6];
    tempPos4 = getInputs[7];
    tempPos5 = getInputs[8];
    tempPos6 = getInputs[9];

    var searchBase = "[data-sku~=" + tempBase.toUpperCase() + "]";
    var searchVolt = "[data-sku~=" + "'" + tempVolt + "'" + "]";
    var searchPos1 = "[data-sku~=" + tempPos1.toUpperCase() + "]";
    var searchPos2 = "[data-sku~=" + tempPos2.toUpperCase() + "]";
    var searchPos3 = "[data-sku~=" + tempPos3.toUpperCase() + "]";
    var searchPos4 = "[data-sku~=" + tempPos4.toUpperCase() + "]";
    var searchPos5 = "[data-sku~=" + tempPos5.toUpperCase() + "]";
    var searchPos6 = "[data-sku~=" + tempPos6.toUpperCase() + "]";

    var childBaseQuery = document.querySelectorAll(searchBase);
    var childVoltQuery = document.querySelectorAll(searchVolt);
    var childPos1Query = document.querySelectorAll(searchPos1);
    var childPos2Query = document.querySelectorAll(searchPos2);
    var childPos3Query = document.querySelectorAll(searchPos3);
    var childPos4Query = document.querySelectorAll(searchPos4);
    var childPos5Query = document.querySelectorAll(searchPos5);
    var childPos6Query = document.querySelectorAll(searchPos6);

    var childBase = childBaseQuery[0];
    var childVoltage = childVoltQuery[0];
    var childPos1 = childPos1Query[0];
    var childPos2 = childPos2Query[0];
    var childPos3 = childPos3Query[0];
    var childPos4 = childPos4Query[0];
    var childPos5 = childPos5Query[0];
    var childPos6 = childPos6Query[0];
   
    var baseId = childBase.id;
    var voltageId = childVoltage.id;
    var pos1Id = childPos1.id;
    var pos2Id = childPos2.id;
    var pos3Id = childPos3.id;
    var pos4Id = childPos4.id;
    var pos5Id = childPos5.id;
    var pos6Id = childPos6.id;




    var containerBase = getContainer(baseId);
    var containerVoltage = getContainer(voltageId);
    var containerPos = getContainer(pos1Id);
    var containerSound = getContainer(pos6Id);





    // Add child to its proper container & set proper sku
    document.getElementById(containerBase).appendChild(childBase);
    childBase.dataset.selected = true;
    setProperSku(containerBase, childBase.dataset.sku);
    // Update the price of the cart
    Cart.updatePrice(childBase.dataset.price);


    // Add child to its proper container & set proper sku
    document.getElementById(containerVoltage).appendChild(childVoltage);
    childVoltage.dataset.selected = true;
    setProperSku(containerVoltage, childVoltage.dataset.sku);
    // Update the price of the cart
    Cart.updatePrice(childVoltage.dataset.price);




    ////////pos
    // Add child to its proper container & set proper sku
    document.getElementById(containerPos).appendChild(childPos1);
    childPos1.dataset.selected = true;
    setProperSku(containerPos, childPos1.dataset.sku);
    // Update the price of the cart
    Cart.updatePrice(childPos1.dataset.price);



    // Add child to its proper container & set proper sku
    document.getElementById(containerPos).appendChild(childPos2);
    childPos2.dataset.selected = true;
    setProperSku(containerPos, childPos2.dataset.sku);
    // Update the price of the cart
    Cart.updatePrice(childPos2.dataset.price);

    // Add child to its proper container & set proper sku
    document.getElementById(containerPos).appendChild(childPos3);
    childPos3.dataset.selected = true;
    setProperSku(containerPos, childPos3.dataset.sku);
    // Update the price of the cart
    Cart.updatePrice(childPos3.dataset.price);

    // Add child to its proper container & set proper sku
    document.getElementById(containerPos).appendChild(childPos4);
    childPos4.dataset.selected = true;
    setProperSku(containerPos, childPos4.dataset.sku);
    // Update the price of the cart
    Cart.updatePrice(childPos4.dataset.price);

    // Add child to its proper container & set proper sku
    document.getElementById(containerPos).appendChild(childPos5);
    childPos5.dataset.selected = true;
    setProperSku(containerPos, childPos5.dataset.sku);
    // Update the price of the cart
    Cart.updatePrice(childPos5.dataset.price);

    // Add child to its proper container & set proper sku
    document.getElementById(containerSound).appendChild(childPos6);
    childPos6.dataset.selected = true;
    setProperSku(containerSound, childPos6.dataset.sku);
    // Update the price of the cart
    Cart.updatePrice(childPos6.dataset.price);






    // Update text on screen
    updateDisplay();
    
   
}



/**
 * Triggered when item is dropped inside cart.
 * @param ev Event data
*/
function dropInCart(ev) {
    ev.preventDefault();

    // Get id and element being dragged
    var selectedId = ev.dataTransfer.getData("part");
    var child = document.getElementById(selectedId);

    // Get the container of the dragged item
    var container = getContainer(selectedId);

    // Check if item isn't a position item
    if (container != CONTAINER_POSITION) {
        if (hasItems(container)) {
            showWarningModal('Warning', 'You\'ve already selected an item!');

            // Check if should hide remove container
            checkRemoveContainer(child);
            return;
        }
        // Add child to its proper container & set proper sku
        document.getElementById(container).appendChild(child);
        child.dataset.selected = true;
        setProperSku(container, child.dataset.sku);


    } else {
        // Check if another position can be added
        if (allowAnotherPosition()) {
            // Add a new child to the proper container because multiple
            // positions  can be chosen
            document.getElementById(container).appendChild(copyTile(child));
            Cart.addPosition(child.dataset.sku);
        } else {
            showWarningModal('You can\'t select any more positions', 'You\'ve chosen ' +
                'a sound module or have all five positions selected!');

            // Check if should hide remove container
            checkRemoveContainer(child);
            return;
        }
    }

    // Update the price of the cart
    Cart.updatePrice(child.dataset.price);

    // Update text on screen
    updateDisplay();
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
        child.dataset.selected = 'false';

        // Check if child is a position item
        if (selectedId.indexOf(CATEGORY_POSITION) >= 0) {
            Cart.position = "";
            Cart.subtractPrice(child.dataset.price);
            document.getElementById(CONTAINER_POSITION).removeChild(child);
        }
        else {
            // Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }
    }

    // Show text updates
    updateDisplay();

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
    if (_id.indexOf(CATEGORY_STYLE) >= 0)
        return COLUMN_STYLE;
    else if (_id.indexOf(CATEGORY_VOLTAGE) >= 0)
        return COLUMN_VOLTAGE;
    else if (_id.indexOf(CATEGORY_POSITION) >= 0)
        return COLUMN_POSITION;
    else if (_id.indexOf(CATEGORY_SOUND) >= 0)
        return COLUMN_SOUND;
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
 * Gets the proper container the child should
 * be in.
 * @param _id The id of the item
 * @return Container
*/
function getContainer(_id) {
    if (_id.indexOf(CATEGORY_STYLE) >= 0)
        return CONTAINER_STYLE;
    else if (_id.indexOf(CATEGORY_VOLTAGE) >= 0)
        return CONTAINER_VOLTAGE;
    else if (_id.indexOf(CATEGORY_POSITION) >= 0)
        return CONTAINER_POSITION;
    else if (_id.indexOf(CATEGORY_SOUND) >= 0)
        return CONTAINER_SOUND;
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
        case CONTAINER_STYLE:
            Cart.setBase(_sku);
            break;
        case CONTAINER_VOLTAGE:
            Cart.setVoltage(_sku);
            break;
        case CONTAINER_SOUND:
            Cart.setSound(_sku)
            break;  
     
    }
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





/**
 * Checks if the parent container already has items in it.
 * @param _container Container of the child
 * @return Boolean
*/
function hasItems(_container) {
    return document.getElementById(_container).getElementsByClassName("metro").length > 0;
}


/**
 * Checks if the position container is allowed
 * to add any more items to it. 
 * 
 * Remember, only 5 positions can be selected
 * and nothing can be selected after a sound 
 * module is selected.
*/
function allowAnotherPosition() {
    // Get the children of the position container
    var container = document.getElementById(CONTAINER_POSITION);
    var children = container.getElementsByClassName("metro");
    var childTitle;

    // Check if any children have a sound module... as no more
    // parts can be added if the user selects a sound module
    for (var i = 0; i < children.length; i++) {
        childTitle = children[i].getElementsByTagName("img")[0].getAttribute("title");
        if (childTitle == LAST_ALLOWED_POSITIONS[0] ||
            childTitle == LAST_ALLOWED_POSITIONS[1]) {
            return false;
        }
    }
    // Cannot have more than 5 positions either!
    return children.length < MAX_POSITIONS;
}


/**
 * Create a new element by copying the properties
 * thereof an already existing element.
 * @param _child The element to copy
*/
function copyTile(_child) {
    var newChild = _child.cloneNode(true);
    var attr = newChild.getAttribute("id") + document.getElementById(CONTAINER_POSITION).getElementsByClassName("metro").length + 1;
    newChild.setAttribute("id", attr)
    newChild.setAttribute("data-selected", 'true');
    return newChild;
}


/**
 * Updates the sku and price text on the page
 * to reflect thereof its cart counterpart.
*/
function updateDisplay() {
    $("#title_text_price").html(Cart.generatePrice());
    $("#title_text_sku").html(Cart.generateSku());
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
 * Validates the user's selection and mocks a 
 * checkout proccess.
*/
function addToCart() {
    // Let's check if the user has selected everything we need to build the part
    if (!hasItems(CONTAINER_STYLE)) {
        showErrorModal('Missing Items', 'Please select a style!');
        return;
    }
    if (!hasItems(CONTAINER_VOLTAGE)) {
        showErrorModal('Missing Items', 'Please select a voltage!');
        return;
    }
    if (!hasItems(CONTAINER_POSITION)) {
        showErrorModal('Missing Items', 'Please select at least one position!');
        return;
    }
    showModal('Success', 'World Tower Light added to cart!');

    dateTimeChecker();
}
function dateTimeChecker() {

    // placing order and checks timestap to determine if same day shipping is availible
    var d = new Date();
    var hour = d.getHours();
    var day = d.getDay();

    //if (day == 1, day == 2, day == 3, day == 4, day == 5) {
    //    if (hour >= 8 == hour <= 18) {
    //        showModal("success!", "your item will be shipped today!");
    //        return;
    //    } else {
    //        showErrorModal("error", "Your item will ship the next business day");
    //        return;
    //    } 
    //} else {
    //    showErrorModal("error", "Your item will ship the next business day");
    //    return;
    //}


    switch (day) {
    case 0:
        showErrorModal("error", "Your item will ship the next business day");

        break;
    case 1:

        if (hour >= 8 == hour <= 18) {
            showModal("success!", "your item will be shipped today!");

        } else {
            showErrorModal("error", "Your item will ship the next business day");

        }
        break;
    case 2:

        if (hour >= 8 == hour <= 18) {
            showModal("success!", "your item will be shipped today!");

        } else {
            showErrorModal("error", "Your item will ship the next business day");

        }
        break;
    case 3:

        if (hour >= 8 == hour <= 18) {
            showModal("success!", "your item will be shipped today!");

        } else {
            showErrorModal("error", "Your item will ship the next business day");

        }
        break;
    case 4:

        if (hour >= 8 == hour <= 18) {
            showModal("success!", "your item will be shipped today!");

        } else {
            showErrorModal("error", "Your item will ship the next business day");

        }
        break;
    case 5:

        if (hour >= 8 == hour <= 18) {
            showModal("success!", "your item will be shipped today!");

        } else {
            showErrorModal("error", "Your item will ship the next business day");

        }
        break;

    case 6:
        showErrorModal("error", "Your item will ship the next business day");

    default:
        showModal("Success", "Your item will be ship Today");

        break;
    }
}

/**
 *
 * Function for removing a style 
 * @param {any} event
 */
function removeStyle(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "style";
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

        // Check if child is a position item
        if (selectedId.indexOf(CATEGORY_POSITION) >= 0) {
            Cart.position = "";
            Cart.subtractPrice(child.dataset.price);
            document.getElementById(CONTAINER_POSITION).removeChild(child);
        }
        else {
            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }
    }

    // Show text updates
    updateDisplay();
}
/**
 *
 * Function for removing a style 
 * @param {any} event
 */
function removeSoundModule(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "soundMoudle";
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

        // Check if child is a position item
        if (selectedId.indexOf(CATEGORY_POSITION) >= 0) {
            Cart.position = "";
            Cart.subtractPrice(child.dataset.price);
            document.getElementById(CONTAINER_POSITION).removeChild(child);
        }
        else {
            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }
    }

    // Show text updates
    updateDisplay();
}

/**
 *
 * Function for removing a style 
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

        // Check if child is a position item
        if (selectedId.indexOf(CATEGORY_POSITION) >= 0) {
            Cart.position = "";
            Cart.subtractPrice(child.dataset.price);
            document.getElementById(CONTAINER_POSITION).removeChild(child);
        }
        else {
            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }
    }

    // Show text updates
    updateDisplay();
}



/**
 *
 * Function for removing a style 
 * @param {any} event
 */
function removeLastPostion(event) {

    var item = findSelectedItems();
    //This is the specific substring we are searching for this would be the part of the object type
    var subString = "positions";
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

        // Check if child is a position item
        if (selectedId.indexOf(CATEGORY_POSITION) >= 0) {
            Cart.position = "";
            Cart.subtractPrice(child.dataset.price);
            document.getElementById(CONTAINER_POSITION).removeChild(child);
        }
        else {
            //// Append child to its original parent
            var parent = document.getElementById(correctColumn);
            parent.appendChild(child);

            setProperSku(getContainer(selectedId), "");
            Cart.subtractPrice(child.dataset.price);
        }
    }

    // Show text updates
    updateDisplay();
}