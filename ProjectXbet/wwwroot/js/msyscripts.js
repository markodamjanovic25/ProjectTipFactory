let LandingDefaultShown = true;
let LandingTipsnstatsShown = false;
let LandingAboutShown = false;



//Landing

$("#about-landing-show").on("click", function () {
        ShowAbout();
})
$("#tipsnstats-landing-show").on("click", function () {
    ShowTipsNStats();
})
$("#tipsnstats-landing-show-second").on("click", function () {
    ShowTipsNStats();
})
$("#about-landing-show-second").on("click", function () {
    ShowAbout();
})
$("#default-landing-show").on("click", function () {
    ShowDefaultLanding();
})


    function ShowTipsNStats() {
        if (!LandingTipsnstatsShown) {
            if (LandingAboutShown) {
                HideAbout();
            }
            if (LandingDefaultShown) {
                HideDefault();
            }
            $("#landing-tipsnstats-holder").addClass("landing-element-shown");
            $("#landing-tipsnstats-holder").addClass("landing-component-show");
            LandingTipsnstatsShown = true;
        }
        else {
            HideTipsNStats();
            ShowDefaultLanding();
        }
    }

    function ShowAbout() {
        if (!LandingAboutShown) {
            if (LandingTipsnstatsShown) {
                HideTipsNStats();
            }
            if (LandingDefaultShown) {
                HideDefault();
            }
            $("#landing-about-holder").addClass("landing-element-shown");
            $("#landing-about-holder").addClass("landing-component-show");
            LandingAboutShown = true;
        }
        else {
            HideAbout();
            ShowDefaultLanding();
        }
}
function ShowDefaultLanding() {
    $("#landing-default-holder").show();
    LandingDefaultShown = true;
}



    function HideTipsNStats() {
        $("#landing-tipsnstats-holder").removeClass("landing-element-shown");
        LandingTipsnstatsShown = false;
    }
    function HideAbout() {
        $("#landing-about-holder").removeClass("landing-element-shown");
        LandingAboutShown = false;
}
function HideDefault() {
    $("#landing-default-holder").hide();
    LandingDefaultShown = false;
}



    function CubeShowSide(cube, side) {
        let CubeToRoll = document.getElementById(cube + "-cube");
        CubeToRoll.className = "";
        CubeToRoll.classList.add("cube");
        CubeToRoll.classList.add("show-" + side);
    }

    //Landing

    //Nav
    function NavItemActivate(item) {
        div = document.getElementById(item);
        div.classList.add(item + "-active");


        svg = document.getElementById(item + "-svg");
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
        $("#nav-top-expanded").hide();

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
        $("#box-closed-" + boxPosition).css("display", "none");
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

    /*PAGE NAVBAR*/
    let StatsShown = "Tips";
    $("#btn-stats-tips-show").on("click", function () {
        switch (StatsShown) {
            case "Tips":
                break;
            case "LeagueStats":
                $("#stats-leaguestats-holder").hide();
                $("#btn-stats-leaguestats-show").removeClass("page-navbar-btn-active");
                ShowContent("#stats-tips-holder");
                $(this).addClass("page-navbar-btn-active");
                StatsShown = "Tips";
                break;
            case "TipStats":
                $("#stats-tipstats-holder").hide();
                $("#btn-stats-tipstats-show").removeClass("page-navbar-btn-active");
                ShowContent("#stats-tips-holder");
                $(this).addClass("page-navbar-btn-active");
                StatsShown = "Tips";
                break;
        }
    })
    $("#btn-stats-tipstats-show").on("click", function () {
        switch (StatsShown) {
            case "TipStats":
                break;
            case "Tips":
                $("#stats-tips-holder").hide();
                $("#btn-stats-tips-show").removeClass("page-navbar-btn-active");
                ShowContent("#stats-tipstats-holder");
                $(this).addClass("page-navbar-btn-active");
                StatsShown = "TipStats";
                break;
            case "LeagueStats":
                $("#stats-leaguestats-holder").hide();
                $("#btn-stats-leaguestats-show").removeClass("page-navbar-btn-active");
                ShowContent("#stats-tipstats-holder");
                $(this).addClass("page-navbar-btn-active");
                StatsShown = "TipStats";
                break;
        }
    })
    $("#btn-stats-leaguestats-show").on("click", function () {
        switch (StatsShown) {
            case "LeagueStats":
                break;
            case "Tips":
                $("#stats-tips-holder").hide();
                $("#btn-stats-tips-show").removeClass("page-navbar-btn-active");
                ShowContent("#stats-leaguestats-holder");
                $(this).addClass("page-navbar-btn-active");
                StatsShown = "LeagueStats";
                break;
            case "TipStats":
                $("#stats-tipstats-holder").hide();
                $("#btn-stats-tipstats-show").removeClass("page-navbar-btn-active");
                ShowContent("#stats-leaguestats-holder");
                $(this).addClass("page-navbar-btn-active");
                StatsShown = "LeagueStats";
                break;
        }
    })

    function ShowContent(item) {
        $(item).css("height", "5vh");
        $(item).show();
        $(item).animate({ height: "91vh" });
    }

