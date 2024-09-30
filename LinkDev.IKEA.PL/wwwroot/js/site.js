// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let SearchInput = document.getElementById("searchInp");

SearchInput.addEventListener("keyup", () => {
    var searchValue = SearchInput.value;

    var xhr = new XMLHttpRequest();

    xhr.open("GET", `https://localhost:7042/Employee?search=${searchValue}`);

    xhr.send();

    xhr.onreadystatechange = function () {
        if (xhr.readyState == XMLHttpRequest.DONE) {
            if (xhr.status == 200) {
                document.getElementById("employeeList").innerHTML = xhr.responseText;
                 
            }
           
            else {
                alert('something else other than 200 was returned');
            }
        }
    }
});
    