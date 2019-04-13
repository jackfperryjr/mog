$(document).ready(function() {
    // $(".project-button").click(function() {
    //     closeTabs();
    //     $("#projects:hidden").fadeIn();
    //     /*
    //     $('html,body').animate({
    //         scrollTop: $(".moogle-container").offset().top},
    //         'slow');
    //     */
    // });

    $(".about-button").click(function() {
        closeTabs();
        $("#about:hidden").fadeIn();
        /*
        $('html,body').animate({
            scrollTop: $("#about").offset().top},
            'slow');
        */
    });

    // $(".contact-button").click(function() {
    //     closeTabs();
    //     $("#contact:hidden").fadeIn();
    //     /*
    //     $('html,body').animate({
    //         scrollTop: $("#contact").offset().top},
    //         'slow');
    //     */
    // });

    // $(".link-button").click(function() {
    //     closeTabs();
    //     $("#links:hidden").fadeIn();
    //     /*
    //     $('html,body').animate({
    //         scrollTop: $("#contact").offset().top},
    //         'slow');
    //     */
    // });
});

function closeTabs() {
    //$("#contact:visible").hide();
    $("#about:visible").hide();
    //$("#projects:visible").hide();
    //$("#links:visible").hide();
}

$('#search').keyup(function() {
    let searchField = $(this).val().toLowerCase();
    //if(searchField === '') {
      //  closeTabs();
        //return;
    //}
    if (searchField == "about") {
        closeTabs();
        $("#about:hidden").show();
    }
    // if (searchField == "contact") {
    //     closeTabs();
    //     $("#contact:hidden").show();
    // }
    // if (searchField == "projects") {
    //     closeTabs();
    //     $("#projects:hidden").show();
    // }
    // if (searchField == "links") {
    //     closeTabs();
    //     $("#links:hidden").show();
    // }
});