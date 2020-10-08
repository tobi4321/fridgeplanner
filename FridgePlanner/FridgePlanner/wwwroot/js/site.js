function editTP(id) {
    window.location.href = "/Edit/" + id
}
function trackTraining(id) {
    window.location.href = "/Track/Training/" + id
}

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

function addTP() {
    var name = $('#TPname').val()
    var data = {
        TPname: name
    }

    postData("/Trainingplan/CreateTP", false, data)
        .done(function (msg) {
            editTP(msg);
        })
        .fail(function (msg) {
            alert(msg.responseText);
        });
}
function deleteTP(id) {
    var data = {
        TrainingPlanId: id
    }
    postData("/Trainingplan/Delete", false, data).done(function (msg) {
        $('.modal').modal('hide');
        $('.modal-backdrop').remove() // removes the grey overlay.
        $("#allTrainingplans").html(msg);
    });
}
function addExercise(id) {
    var exname = $('#exerciseName').val()
    var exdesc = $('#exerciseDescription').val()
    var data = {
        TrainingPlanId: id,
        exName: exname,
        exDesc: exdesc
    }

    postData("/Trainingplan/AddExercise/", false, data).done(function (msg) {
        $("#exercises").html(msg);
    }).fail(function (msg) {
        alert(msg.responseText);
    });
}
function deleteExercise(trainingPlanId, exerciseName) {
    var data = {
        TrainingPlanId: trainingPlanId,
        exName: exerciseName
    }

    postData("/TrainingPlan/DeleteExercise/", false, data).done(function (partialView) {
        $('#exercises').html(partialView);
    });
}
function updateExercise(trainingPlanId, exname) {
    var newexname = $('#exerciseName' + exname).val()
    var newexdesc = $('#exerciseDescription' + exname).val()

    var data = {
        TrainingPlanId: trainingPlanId,
        OldExName: exname,
        NewExName: newexname,
        NewExDesc: newexdesc
    }

    postData("/Trainingplan/UpdateExercise/", false, data)
        .done(function (msg) {
            $('#modalExerEdit' + exname).modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $("#exercises").html(msg);
        }).fail(function (msg) {
            alert(msg.responseText);
        });
}
function addAttribute(trainingPlanId, exname) {
    var attrKey = $("#attrKey" + exname).val()
    var attrVal = $("#attrValue" + exname).val()
    var data = {
        TrainingPlanId: trainingPlanId,
        ExerciseName: exname,
        attrKey: attrKey,
        attrValue: attrVal
    }
    postData("/Trainingplan/AddAttribute/", false, data)
        .done(function (msg) {
            $("#attributes" + exname).html(msg);
        }).fail(function (msg) {
            alert(msg.responseText);
        });
}
function deleteAttribute(exname, attrKey) {
    var data = {
        ExName: exname,
        attrKey: attrKey
    }
    postData("/Trainingplan/DeleteAttribute/", false, data).done(function (msg) {
        $("#attributes" + exname).html(msg);
    });
}
function updateAttribute(exname, oldattrkey) {
    var newkey = $('#attrUpdateKey' + exname + oldattrkey).val()
    var newval = $('#attrUpdateValue' + exname + oldattrkey).val()
    var data = {
        ExName: exname,
        OldKey: oldattrkey,
        NewKey: newkey,
        NewValue: newval
    }

    postData("/Trainingplan/UpdateAttribute/", false, data)
        .done(function (msg) {
            $('#modalUpdateAttr' + exname + oldattrkey).modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $("#attributes" + exname).html(msg);
        }).fail(function (msg) {
            alert(msg.responseText);
        });
}

function trackWeight() {
    window.location.href = "/Track/TrackWeight";
}
function addWeight()
{
    var weightValue = document.getElementById("weightValue").value;
    $.ajax({
        type: "Post",
        url: "/Track/AddWeight",
        cache: false,
        data: {
            w: weightValue
        }
    }).done(function (weights) {
        $("#OverviewWeight").html(weights);
        $('.modal-backdrop').hide();
    });
}
function overViewTP() {
    window.location.href = "/Overview";
}
//UserManagement
function deleteUser(userId) {
    var data = {
        Id: userId
    }
    postData("/Admin/DeleteUser/", false, data)
        .done(function (msg) {
            $("#userTableSection").html(msg);
        });
}

function editUser(userId) {
    // open edit view
    var data = {
        Id: userId
    };
    postData("/Admin/GetRoleModal/", false, data)
        .done(function (msg) {
            $("#RoleModal").html(msg);
            $('select').selectpicker();
            $("#EditRolesModal").modal();
        });
}

function updateUserRoles(userId) {
    var selected = $('#roleSelect').val();
    var data = {
        Id: userId,
        Roles: selected
    }

    postData("/Admin/UpdateUserRoles/", false, data).done(function (msg) {
        $("#userTableSection").html(msg);
    });
}

function searchUser() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("searchUserInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("UserTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

// Manage Account
function editAccountUsername() {
    document.getElementById("UserNameInput").disabled = false;
    var x = document.getElementById("userBtn");
    x.style.display = "none";
}
function editAccountEmail() {
    document.getElementById("emailInput").disabled = false;
    var x = document.getElementById("emailBtn");
    x.style.display = "none";
}
function editAccountDate() {
    document.getElementById("BirthDateInput").disabled = false;  
    var x = document.getElementById("birthdateBtn");
    x.style.display = "none";
}
function editAccountPassword() {
    var x = document.getElementById("PasswordInput");
    x.style.display = "block";
    var x = document.getElementById("passwordBtn");
    x.style.display = "none";
}

function updateAccount() {
    var userName = $("#UserNameInput").val();
    var email = $("#emailInput").val();
    var date = $("#BirthDateInput").val();
    var password = $("#passWInput").val();
    var passwordConf = $("#passWCheckInput").val();
    
    var data = {
        userName: userName,
        email: email,
        sDate: date,
        password: password,
        passwordConf: passwordConf
    }
    console.log("userName:" + userName + "email:" + email + " | date:" + date + " | password:" + password + " | passwordConfirm:" + passwordConf);
    console.log(data);
    postData("SubmitAccountChanges", false, data).done(
        function () {
            closeManageAccount()
        }
    ).fail(
        function () {
            alert("Input is invalid")
        }
    )
}

function closeManageAccount() {
    window.location.href = "/";
}

// News section
function overViewNews() {
    window.location.href = "/News/Overview";
}
function createNewsItem() {
    var newsTitle = $('#newsTitle').val()
    var newsDesc = $('#newsDescription').val()
    var newsCategory = $('#newsCategory :selected').text();
    if ($('#newsVisibility :selected').text() == "Visible") {
        var newsVisibility = true;
    } else {
        var newsVisibility = false;
    }
    console.log(newsCategory);
    console.log(newsVisibility);
    $.ajax({
        type: "Post",
        url: "/News/Create",
        cache: false,
        data: {
            title: newsTitle,
            desc: newsDesc,
            image: null,
            category: newsCategory,
            visibility: newsVisibility
        }
    }).done(function (newsTable) {
        $("#newsTable").html(newsTable);
        $('#newsTitle').val("");
        $('#newsDescription').val("");

    });

}
function editNewsItem(id) {
    $.ajax({
        type: "Post",
        url: "/News/GetEditNewsModal/",
        cache: false,
        data: {
            NewsId: id
        }
    }).done(function (newsEditModal) {
        $("#newsEditDiv").html(newsEditModal);
        $("#newsEditModal").modal()
    });
}
function updateNewsItem(id) {
    var newsTitle = $('#newsEditTitle').val()
    var newsDesc = $('#newsEditDescription').val()
    var newsCategory = $('#newsEditCategory :selected').text();
    if ($('#newsEditVisibility :selected').text() == "Visible") {
        var newsVisibility = true;
    } else {
        var newsVisibility = false;
    }
    var data = {
        NewsId: id,
        title: newsTitle,
        desc: newsDesc,
        image: null,
        category: newsCategory,
        visibility: newsVisibility
    }

    postData("/News/UpdateNewsItem/", false, data).done(function (newsTable) {
        $("#newsTable").html(newsTable);
    });
}
function deleteNewsItem(id) {
    var data = {
        NewsId: id
    }
    postData("/News/Delete/", false, data).done(function (newsTable) {
        $("#newsTable").html(newsTable);
    });
}
// end News section

function addTraining(id) {
    var date = document.querySelector("#date").value;
    var time = document.querySelector("#time").value;
    var myJsDate = new Date(date.slice(0, 4), date.slice(5, 7) - 1, date.slice(8, 10), time.slice(0, 2), time.slice(3, 5));
    var myDotNetDate = myJsDate.toISOString();
    var outputString = '{ "TrainingplanId": '+ id + ',"date" :' + '"' + myDotNetDate + '"' + ',  "exercises": [';
    var outputExercises = '';
    var outputAttribute = '';

    fetchData("/api/TrainingPlanRest/" + id, false).done(function (tp) {
        for (e in tp.exercises) {
            outputExercises = outputExercises + '{  "name": "' + tp.exercises[e].name + '", "description": "' + tp.exercises[e].description + '", "attributes": [';
            for (a in tp.exercises[e].attributes) {
                outputAttribute = outputAttribute + '{ "key": "' + tp.exercises[e].attributes[a].key + '", "value": ' + $("#" + tp.exercises[e].name + tp.exercises[e].attributes[a].attributeId).val() + '},';
            }
            outputAttribute = outputAttribute.slice(0, -1);
            outputExercises = outputExercises + outputAttribute + ']},';
            outputAttribute = '';
        }
        outputExercises = outputExercises.slice(0, -1);
        outputString = outputString + outputExercises + ']}';


        postDataWithContentAndDataType("/Track/Add/", false, outputString, 'application/json', 'json')
            .done(function (trainingId) {
                window.location.href = "/Track/Stats/" + trainingId;
            })
    });
}
function discover() {
    window.location.href = "/Trainingplan/Discover";
}

function filterTrainingplans() {
    var table, tr, td, i, txtValue, display, data;
    var inputTitle, filterTitle, inputDescription, filterDescription, inputExercise, filterExercise, inputAuthor, filterAuthor;
    inputTitle = document.getElementById("searchTitle");
    filterTitle = inputTitle.value.toUpperCase();
    inputDescription = document.getElementById("searchDescription");
    filterDescription = inputDescription.value.toUpperCase();
    inputExercise = document.getElementById("searchExercise");
    filterExercise = inputExercise.value.toUpperCase();
    inputAuthor = document.getElementById("searchAuthor");
    filterAuthor = inputAuthor.value.toUpperCase();

    table = document.getElementById("TrainingplanTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        display = true;
        data = tr[i].getElementsByTagName("td")[0];
        if (data) {
            txtValue = data.textContent || data.innerText;
            if (txtValue.toUpperCase().indexOf(filterTitle) <= -1) {
                display = false;
            }
        }
        data = tr[i].getElementsByTagName("td")[1];
        if (data) {
            txtValue = data.textContent || data.innerText;
            if (txtValue.toUpperCase().indexOf(filterDescription) <= -1) {
                display = false;
            }
        }
        data = tr[i].getElementsByTagName("td")[2];
        if (data) {
            txtValue = data.textContent || data.innerText;
            if (txtValue.toUpperCase().indexOf(filterExercise) <= -1) {
                display = false;
            }
        }
        data = tr[i].getElementsByTagName("td")[3];
        if (data) {
            txtValue = data.textContent || data.innerText;
            if (txtValue.toUpperCase().indexOf(filterAuthor) <= -1) {
                display = false;
            }
        }
        if (display) {
            tr[i].style.display = "";
        } else {
            tr[i].style.display = "none";
        }
    }

    var ascending = true;
    var row = 0;
    var switcher = document.getElementById("sortOrder");
    switch (switcher.selectedIndex) {
        case 0:
            break;
        case 1:
            row = 0;
            ascending = true;
            break;
        case 2:
            row = 0;
            ascending = false;
            break;
        case 3:
            row = 1;
            ascending = true;
            break;
        case 4:
            row = 1;
            ascending = false;
            break;
        case 5:
            row = 2;
            ascending = true;
            break;
        case 6:
            row = 2;
            ascending = false;
            break;
        case 7:
            row = 4;
            ascending = true;
            break;
        case 8:
            row = 4;
            ascending = false;
            break;
        default:
            row = 0;
            ascending = true;
            break;
    }
    var rows, switching, i, x, y, shouldSwitch;
    switching = true;
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[row];
            y = rows[i + 1].getElementsByTagName("TD")[row];
            if (ascending) {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            } else {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

function upvote(trainingplanId, userId) {
    $.ajax({
        type: "Post",
        url: "/Trainingplan/Upvote/",
        cache: false,
        datatype: 'json',
        data: {
            Id: trainingplanId,
            UserId: userId
        }
    }).done(function (DiscoverTpPartial) {
        $("#DiscoverTpPartial").html(DiscoverTpPartial);
    });
}
function copyTP(trainingplanId) {
    window.location.href = "/Trainingplan/CopyTp/" + trainingplanId;
}
