// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.

///////////////multiselect




jQuery(function ($) {

    $(".sidebar-dropdown > a").click(function () {
        $(".sidebar-submenu").slideUp(200);
        if (
            $(this)
                .parent()
                .hasClass("active")
        ) {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .parent()
                .removeClass("active");
        } else {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .next(".sidebar-submenu")
                .slideDown(200);
            $(this)
                .parent()
                .addClass("active");
        }
    });

    $("#close-sidebar").click(function () {
        $(".page-wrapper").removeClass("toggled");
    });
    $("#show-sidebar").click(function () {
        $(".page-wrapper").addClass("toggled");
    });




});


//editor



function showPreview() {
    var htmlCode = document.getElementById("htmlCode").value;
    var cssCode = "<style>" + document.getElementById("cssCode").value + "</style>";
    var jsCode = "<scri" + "pt>" + document.getElementById("jsCode").value + "</scri" + "pt>";
    var frame = document.getElementById("preview-window").contentWindow.document;
    frame.open();
    frame.write(htmlCode + cssCode + jsCode);
    frame.close();
}



////////// pagin filter//////////


$(document).ready(function () {
    $("#myTable").DataTable();
})



//////////////ShowAlert


//$(document).ready(function () {
//    $('.showAlert').click(function () {
//        $('.alert').show();
//    })
//});




//////////////slide






//////////////Endslide