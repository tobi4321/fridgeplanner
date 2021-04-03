
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
    var unit = $('#fridgeUnit :selected').text();
    var date = document.querySelector("#fridgeExpireDate").value;
    var myJsDate = new Date(date.slice(0, 4), date.slice(5, 7) - 1, date.slice(8, 10));
    var correctDate = date + "T00:00:00.000Z";
    var myDotNetDate = myJsDate.toISOString();
    var outputString = '{ "Name": ' + '"' + name + '"' + ',"Amount": ' + '"' + amount + '"' + ',"Unit": ' + '"' + unit + '"' + ',"ExpiryDate" :' + '"' + correctDate + '"' + '}';

    postDataWithContentAndDataType("/Fridge/AddItem/", false, outputString, 'application/json', 'json')
        .done(function (fridgeItems) {
            $("#FridgeList").html(fridgeItems);
            $('.modal-backdrop').hide();
        });
}

function deleteFridgeItem(id) {
    var data = {
        Id: id
    }
    postData("/Fridge/DeleteItem/", false, data).done(function (fridgeItems) {
        $("#FridgeList").html(fridgeItems);
        $('.modal-backdrop').hide();
    });
}

function editFridgeItem(id)
{
    $.ajax({
        type: "Post",
        url: "/Fridge/GetEditFridgeModal/",
        cache: false,
        data: {
            Id: id
        }
    }).done(function (fridgeItemEditModal) {
        $("#fridgeEditDiv").html(fridgeItemEditModal);
        $("#fridgeItemEditModal").modal()
    });
}

function updateFridgeItem(id) {
    var fridgeName = $('#fridgeEditName').val()
    var fridgeAmount = $('#fridgeEditAmount').val()
    var fridgeUnit = $('#fridgeEditUnit :selected').text();
    var date = document.querySelector("#fridgeEditExpireDate").value;
    var correctDate = date + "T00:00:00.000Z";

    var data = {
        Id: id,
        name: fridgeName,
        amount: fridgeAmount,
        unit: fridgeUnit,
        expiry: correctDate
    }

    postData("/Fridge/UpdateFridgeItem/", false, data)
        .done(function (fridgeItems) {
            $("#FridgeList").html(fridgeItems);
            $('.modal-backdrop').hide();
        });
}

function addShoppingItem() {
    var name = document.querySelector("#shoppingName").value;
    var amount = document.querySelector("#shoppingAmount").value;
    var unit = $('#shoppingUnit :selected').text();

    var outputString = '{ "Name": ' + '"' + name + '"' + ',"Amount": ' + '"' + amount + '"' + ',"Unit": ' + '"' + unit + '"' + '}';

    postDataWithContentAndDataType("/Shopping/AddItem/", false, outputString, 'application/json', 'json')
        .done(function (shoppingViewModel) {
            $("#ShoppingList").html(shoppingViewModel);
            $('.modal-backdrop').hide();
        });
}
function deleteShoppingItem(id) {
    var data = {
        Id: id
    }
    postData("/Shopping/Delete/", false, data)
        .done(function (shoppingViewModel) {
            $("#ShoppingList").html(shoppingViewModel);
            $('.modal-backdrop').hide();
        });
}

function editShoppingItem(id) {
    $.ajax({
        type: "Post",
        url: "/Shopping/GetEditShoppingModal/",
        cache: false,
        data: {
            Id: id
        }
    }).done(function (shoppingItemEditModal) {
        $("#shoppingEditDiv").html(shoppingItemEditModal);
        $("#shoppingItemEditModal").modal()
    });
}

function updateShoppingItem(id) {
    var shoppingName = $('#shoppingEditName').val()
    var shoppingAmount = $('#shoppingEditAmount').val()
    var shoppingUnit = $('#shoppingEditUnit :selected').text();
    var data = {
        Id: id,
        name: shoppingName,
        amount: shoppingAmount,
        unit: shoppingUnit
    }

    postData("/Shopping/UpdateShoppingItem/", false, data)
        .done(function (shoppingViewModel) {
            $("#ShoppingList").html(shoppingViewModel);
            $('.modal-backdrop').hide();
        });
}

function updateRecipeDetail(id) {
    $.ajax({
        type: "Post",
        url: "/Recipe/GetRecipeDetail/",
        cache: false,
        data: {
            Id: id
        }
    }).done(function (recipeDetailPartial) {
        $("#recipeDetail").html(recipeDetailPartial);
    });
}
function addToCart(id) {
    $.ajax({
        type: "Post",
        url: "/Recipe/AddToCart/",
        cache: false,
        data: {
            Id: id
        }
    }).done(function () {
        $("#recipeToCartModal").modal()
    });
}
function showRecipeHomeDetail(id) {
    $.ajax({
        type: "Post",
        url: "/Fridge/GetRecipeDetail/",
        cache: false,
        data: {
            Id: id
        }
    }).done(function (recipeDetailModal) {
        $("#recipeDetailView").html(recipeDetailModal);
        $("#recipeDetailModal").modal();
    });
}
function addRecipe() {
    var recipeName = $('#addRecipeName').val()
    var recipeDescription = $('#addRecipeDescription').val()

    var outputString = '{ "Name": ' + '"' + recipeName + '"' + ',"Description": ' + '"' + recipeDescription + '"'  + '}';
    postDataWithContentAndDataType("/Recipe/AddRecipe/", false, outputString, 'application/json', 'json')
        .done(function (msg) {
            $('.modal-backdrop').hide();
            editRecipePage(msg);
        })
        .fail(function (msg) {
            $('.modal-backdrop').hide();
            alert(msg.responseText);
        });
}
function editRecipePage(id) {
    console.log(id);
    window.location.href = "Recipe/EditRecipeOverview/" + id;
}
function editRecipe(id)
{
    var recipeName = $('#editRecipeName').val()
    var recipeDescription = $('#editRecipeDescription').val()
    var data = {
        Id: id,
        name: recipeName,
        description: recipeDescription,
    }

    postData("/Recipe/EditRecipe/", false, data)
        .done(function (msg) {

            // go to recipe edit page of element with id
            window.location.href =  msg;
            $('.modal-backdrop').hide();
        });
}

function addRecipeItem(id)
{
    var name = document.querySelector("#recipeItemName").value;
    var amount = document.querySelector("#recipeItemAmount").value;
    var unit = $('#recipeItemUnit :selected').text();

    var outputString = '{ "Name": ' + '"' + name + '"' + ',"Amount": ' + '"' + amount + '"' + ',"Unit": ' + '"' + unit + '"' + '}';

    postDataWithContentAndDataType("/Recipe/AddRecipeItem/" + id, false, outputString, 'application/json', 'json')
        .done(function (msg) {
            window.location.href = msg;
            $('.modal-backdrop').hide();
        });
}
function deleteRecipeItem(recipeId, recipeItemId) {
    var data = {
        RecipeId: recipeId,
        RecipeItemId: recipeItemId
    }
    postData("/Recipe/DeleteRecipeItem/", false, data)
        .done(function (msg) {
            window.location.href = msg;
            $('.modal-backdrop').hide();
        });
}
function deleteRecipeStep(recipeId, recipeStepId) {
    var data = {
        RecipeId: recipeId,
        RecipeStepId: recipeStepId
    }
    postData("/Recipe/DeleteRecipeStep/", false, data)
        .done(function (msg) {
            window.location.href = msg;
            $('.modal-backdrop').hide();
        });
}
function addRecipeStep(id) {

    var name = document.querySelector("#recipeStepName").value;
    var stepNumber = document.querySelector("#recipeStepNumber").value;
    var text = document.querySelector("#recipeStepText").value;

    var outputString = '{ "Name": ' + '"' + name + '"' + ',"StepNumber": ' + '"' + stepNumber + '"' + ',"Text": ' + '"' + text + '"' + '}';

    postDataWithContentAndDataType("/Recipe/AddRecipeStep/" + id, false, outputString, 'application/json', 'json')
        .done(function (msg) {
            window.location.href = msg;
            $('.modal-backdrop').hide();
        });
}
function updateRecipeItem(id,recipeId) {
    var recipeItemName = $('#recipeItemEditName'+id).val()
    var recipeItemAmount = $('#recipeItemEditAmount'+id).val()
    var recipeItemUnit = $('#recipeItemEditUnit'+id+' :selected').text();

    var data = {
        RecipeItemId: id,
        Id: recipeId,
        name: recipeItemName,
        amount: recipeItemAmount,
        unit: recipeItemUnit
    }

    postData("/Recipe/UpdateRecipeItem/", false, data)
        .done(function (msg) {
            window.location.href = msg;
            $('.modal-backdrop').hide();
        });
}
function updateRecipeStep(id, recipeId) {
    var recipeStepName = $('#recipeStepEditName'+id).val()
    var recipeStepNumber = $('#recipeStepEditNumber'+id).val()
    var recipeStepText = $('#recipeStepEditText'+id).val()

    var data = {
        RecipeStepId: id,
        Id: recipeId,
        name: recipeStepName,
        number: recipeStepNumber,
        text: recipeStepText
    }

    postData("/Recipe/UpdateRecipeStep/", false, data)
        .done(function (msg) {
            window.location.href = msg;
            $('.modal-backdrop').hide();
        });
}

function openNutritionInfo(id) {
    $('#waitModal').modal('show');
    $.ajax({
        type: "Post",
        url: "/Recipe/GetNutritionInfo/",
        cache: false,
        data: {
            Id: id
        }
    }).done(function (recipeNutritionModal) {
        $("#recipeNutritionView").html(recipeNutritionModal);
        $('.modal-backdrop').hide();
        $('#waitModal').modal('hide');
        $('#recipeNutritionModal').modal('show');
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