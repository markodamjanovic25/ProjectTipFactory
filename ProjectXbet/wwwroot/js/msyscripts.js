let bg = document.getElementById("bg-70-black");
let logo = document.getElementById("logoholderlanding");
let tipsnstatsheader = document.getElementById("tipsnstatsheader");
let aboutheader = document.getElementById("aboutheader");
let tipsnstatsholder = document.getElementById("tipsnstatsholder");
let aboutholder = document.getElementById("aboutholder");
let tipsnstatsShown = false;
let aboutShown = false;



//Landing
function LandingPageShow(element) {
  bg.classList.add("bg-70-black-totop");
  logo.classList.add("logo-holder-landing-totop");
  tipsnstatsheader.classList.add("tipsnstats-header-holder-totop");
  aboutheader.classList.add("about-header-holder-totop");
    document.getElementById("welcome-landing").classList.add("welcome-landing-totop");
  if (element == tipsnstatsheader) {
    element.classList.add("active");
    aboutheader.className = "";
    aboutheader.classList.add("about-header-holder");
    aboutheader.classList.add("about-header-holder-totop");
  } else {
    element.classList.add("active");
    tipsnstatsheader.className = "";
    tipsnstatsheader.classList.add("tipsnstats-header-holder");
    tipsnstatsheader.classList.add("tipsnstats-header-holder-totop");
  }
}

function ShowTipsNStats() {
    if (!tipsnstatsShown) {
        if (aboutShown) {
            HideAbout();
        }
        LandingPageShow(tipsnstatsheader);
        tipsnstatsholder.style.display = "block";
        tipsnstatsholder.classList.add("tipsnstats-show");
        tipsnstatsShown = true;
    }
    else {
        HideTipsNStats();
    }
}

function ShowAbout() {
    if (!aboutShown) {
        if (tipsnstatsShown) {
            HideTipsNStats();
        }
        LandingPageShow(aboutheader);
        aboutholder.style.display = "block";
        aboutholder.classList.add("about-show");
        aboutShown = true;
    }
    else {
        HideAbout();
    }
}

function HideTipsNStats() {
    tipsnstatsholder.style.display = "none";
    tipsnstatsShown = false;
}
function HideAbout() {
    aboutholder.style.display = "none";
    aboutShown = false;
}

function SafeShowSide(side) {
  let cube = document.getElementById("safe-cube");
  cube.className = "";
  cube.classList.add("cube");
  cube.classList.add("show-" + side);
}
function RiskyShowSide(side) {
  let cube = document.getElementById("risky-cube");
  cube.className = "";
  cube.classList.add("cube");
  cube.classList.add("show-" + side);
}
//Landing

//Nav
function NavItemActivate(item) {
  div = document.getElementById(item);
  svg = document.getElementById(item + "-svg");
  div.classList.add(item + "-active");
  svg.className = "nav-item-tips-svg-active";
}

let isNavExpanded = false;
function ExpandNavbar() {
    if (isNavExpanded) {
        ExpandNavbarReverse();
    } else {
        //let nav = document.getElementById("nav-top-expanded");
        //nav.style.display = "block";
        $("#nav-top-expanded").show();
        //let navItemsHolder = document.getElementById("nav-expanded-items-holder");
        //navItemsHolder.style.display = "flex";
        $("#nav-expanded-items-holder").css("display", "flex");

        $("#x-line-1").addClass("rotateZ45");
        $("#x-line-2").hide();
        $("#x-line-3").addClass("rotateZM45");
        isNavExpanded = true;
    }
}

function ExpandNavbarReverse() {
    //let nav = document.getElementById("nav-top-expanded");
    //nav.style.display = "none";
    $("#nav-top-expanded").hide();

    //let navItemsHolder = document.getElementById("nav-expanded-items-holder");
    //navItemsHolder.style.display = "none";

    $("nav-expanded-items-holder").hide();

    $("#x-line-1").removeClass("rotateZ45");
    $("#x-line-2").show();
    $("#x-line-3").removeClass("rotateZM45");
    isNavExpanded = false;
}
//Nav

//Betslip
let isBetslipExpanded = false;
function ExpandBetslip() {
    if (!isBetslipExpanded) {
        $("#betslip").fadeIn(900);
        isBetslipExpanded = true;
    } else {
        HideBetslip();
    }
}
function HideBetslip() {
    $("#betslip").hide();
    isBetslipExpanded = false;
}

$("#nav-top-betslip").on("click", function () {
    if (!isBetslipExpanded)
        $("#betslip-holder").show();
    else
        $("#betslip-holder").hide();
    ExpandBetslip();
})
//Betslip

//Random

//This method "opens" box that contains a random tip
function BoxMouseover(boxPosition) {
    $("#box-closed-" + boxPosition).css( "display", "none");
    $("#box-open-" + boxPosition).css("display", "inline");
}

//This method "opens" box that contains a random tip
function BoxMouseout(boxPosition) {
    $("#box-open-" + boxPosition).css("display", "none");
    $("#box-closed-" + boxPosition).css("display", "inline");
}



//Account
let isAccountBetsHolderShown = false;
let isAccountUpgradeHolderShown = false;

$("#show-bets").on("click", function () {
    if (!isAccountBetsHolderShown) {
        $("#account-bets-holder").css("display", "block");
        isAccountBetsHolderShown = true;
    }
    else {
        $("#account-bets-holder").css("display", "none");
        isAccountBetsHolderShown = false;
    }
});
$("#show-account-upgrade").on("click", function () {
    if (!isAccountUpgradeHolderShown) {
        $("#account-upgrade-holder").css("display", "block");
        isAccountUpgradeHolderShown = true;
    }
    else {
        $("#account-upgrade-holder").css("display", "none");
        isAccountUpgradeHolderShown = false;
    }
});

$("#ShowReceivedMessages").on("click", function () {
    $("#SentMessages").hide();
    $("#ReceivedMessages").show();
});

$("#ShowSentMessages").on("click", function () {
    $("#ReceivedMessages").hide();
    $("#SentMessages").show();
});

$("#ShowReceivedMessages").hover(
    function () {
    $(this).css("cursor", "pointer");
    });

$("#ShowSentMessages").hover(
    function () {
        $(this).css("cursor", "pointer");
    });



function ExpandReceivedMessage(ReceivedMessageNumber) {
    if ($("#received-message-expanded-" + ReceivedMessageNumber).css("display") == "none") {
        $("#received-message-short-" + ReceivedMessageNumber).hide();
        $("#received-message-expanded-" + ReceivedMessageNumber).show();
    }
    else {
        $("#received-message-short-" + ReceivedMessageNumber).show();
        $("#received-message-expanded-" + ReceivedMessageNumber).hide();
    }
}


function ExpandSentMessage(SentMessageNumber) {
    if ($("#sent-message-expanded-" + SentMessageNumber).css("display") == "none") {
        $("#sent-message-short-" + SentMessageNumber).hide();
        $("#sent-message-expanded-" + SentMessageNumber).show();
    }
    else {
        $("#sent-message-short-" + SentMessageNumber).show();
        $("#sent-message-expanded-" + SentMessageNumber).hide();
    }
}

function ExpandInvoice(InvoiceNumber) {
    if ($("#invoice-expanded-" + InvoiceNumber).css("display") == "none") {
        $("#invoice-short-" + InvoiceNumber).hide();
        $("#invoice-expanded-" + InvoiceNumber).show();
    }
    else {
        $("#invoice-short-" + InvoiceNumber).show();
        $("#invoice-expanded-" + InvoiceNumber).hide();
    }
}

//


//small screen
$("#bottom-nav-previous-tips").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#tips-holder").hide();
    $("#randomizer-holder").hide();
    $("#previous-tips-holder").show();
    $(this).addClass("nav-item-active");
})

$("#bottom-nav-tips-tips").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#previous-tips-holder").hide();
    $("#randomizer-holder").hide();
    $("#tips-holder").show();
    $(this).addClass("nav-item-active");
})

$("#bottom-nav-randomize-tips").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#previous-tips-holder").hide();
    $("#tips-holder").hide();
    $("#randomizer-holder").show();
    $(this).addClass("nav-item-active");
})

$("#bottom-nav-leagues-stats").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#stats-tips-holder").hide();
    $("#stats-tipstats-holder").hide();
    $("#stats-leaguestats-holder").show();
    $(this).addClass("nav-item-active");
})

$("#bottom-nav-tips-previous-stats").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#stats-leaguestats-holder").hide();
    $("#stats-tipstats-holder").hide();
    $("#stats-tips-holder").show();
    $(this).addClass("nav-item-active");
})

$("#bottom-nav-tips-stats").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#stats-tips-holder").hide();
    $("#stats-leaguestats-holder").hide();
    $("#stats-tipstats-holder").show();
    $(this).addClass("nav-item-active");
})

$("#bottom-nav-bets-account").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#account-user-holder").hide();
    $("#account-messages-holder").hide();
    $("#account-invoices-holder").hide();
    $("#account-bets-holder").show();
    $(this).addClass("nav-item-active");
})

$("#bottom-nav-messages-account").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#account-user-holder").hide();
    $("#account-invoices-holder").hide();
    $("#account-bets-holder").hide();
    $("#account-messages-holder").show();
    $(this).addClass("nav-item-active");
})
$("#bottom-nav-user-account").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#account-invoices-holder").hide();
    $("#account-bets-holder").hide();
    $("#account-messages-holder").hide();
    $("#account-user-holder").show();
    $(this).addClass("nav-item-active");
})
$("#bottom-nav-invoices-account").on("click", function () {
    $(".nav-item-active").removeClass("nav-item-active");
    $("#account-bets-holder").hide();
    $("#account-messages-holder").hide();
    $("#account-user-holder").hide();
    $("#account-invoices-holder").show();
    $(this).addClass("nav-item-active");
})
