
var mybutton = document.getElementById("myBtn");

window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}
//return to top


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




//////////////darkmode



const darkSwitch = document.getElementById('darkSwitch');
window.addEventListener('load', () => {
    if (darkSwitch) {
        initTheme();
        darkSwitch.addEventListener('change', () => {
            resetTheme();
        });
    }
});

function initTheme() {
    const darkThemeSelected =
        localStorage.getItem('darkSwitch') !== null &&
        localStorage.getItem('darkSwitch') === 'dark';
    darkSwitch.checked = darkThemeSelected;
    if (darkThemeSelected) {
        document.body.setAttribute('data-theme', 'dark');
        $('.card').attr('style', 'background-color: #212121');
        $('.chiller-theme .sidebar-wrapper').attr('style', 'background-color: #212121');
        $('.navtop').attr('style', 'background-color: #212121 !important');
        $('.panel-title > a, .panel-title > a:active').attr('style', 'background-color: #212121 !important');
    }
    else {
        document.body.removeAttribute('data-theme');
        $('.card').attr('style', 'background-color: white');
        $('.chiller-theme .sidebar-wrapper').attr('style', 'background-color: white');
        $('.navtop').attr('style', 'background-color: white');
        $('.panel-title > a, .panel-title > a:active').attr('style', 'background-color: white');
    }
}


function resetTheme() {
    if (darkSwitch.checked) {
        document.body.setAttribute('data-theme', 'dark');
        $('.card').attr('style', 'background-color: #212121');
        $('.chiller-theme .sidebar-wrapper').attr('style', 'background-color: #212121');
        $('.navtop').attr('style', 'background-color: #212121 !important');
        $('.panel-title > a, .panel-title > a:active').attr('style', 'background-color: #212121 !important');
    }
    else {
        document.body.removeAttribute('data-theme');
        $('.card').attr('style', 'background-color: white');
        $('.chiller-theme .sidebar-wrapper').attr('style', 'background-color: white');
        $('.navtop').attr('style', 'background-color: white');
        $('.panel-title > a, .panel-title > a:active').attr('style', 'background-color: white');

        localStorage.removeItem('darkSwitch');
    }
}


//////////////darkmode