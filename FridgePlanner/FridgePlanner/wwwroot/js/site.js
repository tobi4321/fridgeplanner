function fetchData(dataUrl, useCache) {
    return $.ajax({
        type: "Get",
        url: dataUrl,
        cache: useCache
    });
}

function postDataWithContentAndDataType(dataUrl, useCache, data, contenttype, datatype) {
    return $.ajax({
        type: "Post",
        url: dataUrl,
        cache: useCache,
        datatype: datatype,
        contentType: contenttype,
        data: data
    });
}

function postData(dataUrl, useCache, data) {
    return $.ajax({
        type: "Post",
        url: dataUrl,
        cache: useCache,
        data: data
    })
}


function addFridgeItem() {
    var name = document.querySelector("#fridgeName").value;
    var amount = document.querySelector("#fridgeAmount").value;
    var unit = document.querySelector("#fridgeUnit").value;
    var date = document.querySelector("#fridgeExpireDate").value;
    var myJsDate = new Date(date.slice(0, 4), date.slice(5, 7) - 1, date.slice(8, 10));
    var correctDate = date + "T00:00:00.000Z";
    var myDotNetDate = myJsDate.toISOString();
    var outputString = '{ "Name": ' + '"' + name + '"' + ',"Amount": ' + '"' + amount + '"' + ',"Unit": ' + '"' + unit + '"' + ',"ExpiryDate" :' + '"' + correctDate + '"' + '}';

    postDataWithContentAndDataType("/Home/AddItem/", false, outputString, 'application/json', 'json')
        .done(function (fridgeItems) {
            $("#FridgeList").html(fridgeItems);
            $('.modal-backdrop').hide();
        });
}

function deleteFridgeItem(id) {
    var data = {
        Id: id
    }
    postData("/Home/Delete/", false, data).done(function (fridgeItems) {
        $("#FridgeList").html(fridgeItems);
        $('.modal-backdrop').hide();
    });
}

function addShoppingItem() {
    var name = document.querySelector("#shoppingName").value;
    var amount = document.querySelector("#shoppingAmount").value;
    var unit = document.querySelector("#shoppingUnit").value;

    var outputString = '{ "Name": ' + '"' + name + '"' + ',"Amount": ' + '"' + amount + '"' + ',"Unit": ' + '"' + unit + '"' + '}';

    postDataWithContentAndDataType("/Shopping/AddItem/", false, outputString, 'application/json', 'json')
        .done(function (shoppingViewModel) {
            $("#ShoppingList").html(shoppingViewModel);
            $('.modal-backdrop').hide();
        });
}



/* Open when someone clicks on the span element */
function openNav() {
    document.getElementById("myNav").style.width = "100%";
}

/* Close when someone clicks on the "x" symbol inside the overlay */
function closeNav() {
    document.getElementById("myNav").style.width = "0%";
} 