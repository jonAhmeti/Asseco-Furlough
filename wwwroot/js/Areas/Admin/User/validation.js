$(function() {
    function displayBar(strength, missingTypes) {
        var statusColor = ["#8ACBDE", "#3CB043", "#663399", "#B90E0A", "#FFD700", "#000"];
        var statusText = ["Password is Common", "Password is Rare", "Password is Epic", "Password is Mythic", "Password is Legendary", "Password"];
        if (strength == 5) {
            $("[confirm-pass]").removeAttr("disabled");
        } else if (strength < 5) {
            $("[confirm-pass]").attr("disabled", "true");
        }
        switch (strength) {
            case 1:
                $("#password-strength span").css({
                    "width": "20%",
                    "background": statusColor[0]
                });
                $("[pass-label]").css({
                    "color": statusColor[0]
                }).text(statusText[0]);
                break;

            case 2:
                $("#password-strength span").css({
                    "width": "40%",
                    "background": statusColor[1]
                });
                $("[pass-label]").css({
                    "color": statusColor[1]
                }).text(statusText[1]);
                break;

            case 3:
                $("#password-strength span").css({
                    "width": "60%",
                    "background": statusColor[2]
                });
                $("[pass-label]").css({
                    "color": statusColor[2]
                }).text(statusText[2]);
                break;

            case 4:
                $("#password-strength span").css({
                    "width": "80%",
                    "background": statusColor[3]
                });
                $("[pass-label]").css({
                    "color": statusColor[3]
                }).text(statusText[3]);
                break;

            case 5:
                $("#password-strength span").css({
                    "width": "100%",
                    "background": statusColor[4]
                });
                $("[pass-label]").css({
                    "color": statusColor[4]
                }).text(statusText[4]);
                break;

            case 0:
                $("#password-strength span").css({
                    "width": "0",
                    "background": statusColor[5]
                });
                $("[pass-label]").css({
                    "color": statusColor[5]
                }).text(statusText[5]);
        }
    }

    $("[data-strength]").after("<div id=\"password-strength\" class=\"strength\"><span></span></div>");
    $("[data-strength]").focus(function() {
      $("#password-strength").css({
        "transition": "all 0.3s",
        "height": "4px",
        "margin-top": "-4px"
      });
    }).blur(function() {
      var password = $(this).val();
      if(password.length == 0) {
        $("#password-strength").css({
          "transition": "all 0.3s",
          "height": "0px",
          "margin-top": "0px"
        });
      }
    });

    $("[confirm-pass]").parent().on("click", function() {
      if($("[confirm-pass]").prop("disabled")) {
        alert("baka");
      }
    });

    $("[data-strength]").keyup(function() {
      var password = $(this).val();
      var getStrength = setStrength(password);
      displayBar(getStrength.strength, getStrength.missingTypes);
    });
    
    $("[confirm-pass]").keyup(function() {
      var confirmPass = $(this).val();
      confirmPassword(confirmPass);
    });

    function confirmPassword(confirmPass) {
      var password = $("[data-strength]").val();
      if (confirmPass == password) {
        $("#userForm").find("input[type=submit]").removeAttr("disabled")
      } else {
        $("#userForm").find("input[type=submit]").attr("disabled", "true")
      }
    }

    //password visibility functionality
    const passwordInput = $("#Password");

    const showPassword = $("#showPassword");
    $(showPassword).on('click', function () {
        let visibility = $(this).text();
        $(this).text(visibility == 'visibility' ? 'visibility_off' : 'visibility');
        $(passwordInput).attr('type', visibility == 'visibility' ? 'text' : 'password');
    });


});

function setStrength(password) {
    let strength = 0;
    let hasUpper = 0;
    let hasLower = 0;
    let hasNumeric = 0;
    let hasSymbol = 0;
    let passLength = 0;

    let missingTypes = "";

    for (var i = 0; i < password.length; i++) {
        if (isNumeric(password[i])) {
          hasNumeric = 1;
        } else {
          if (!password[i].match(/^(?=.*[#?!@$%^&*-])/)) {
            if (password[i] == password[i].toUpperCase()) {
              hasUpper = 1;
            }
            if (password[i] == password[i].toLowerCase()) {
              hasLower = 1;
            }
          }
        }//this is done because a number might return true for upperCase and lowerCase scenarios
        
        if (password[i].match(/^(?=.*[#?!@$%^&*-])/)) {
            hasSymbol = 1;
        }
        if (password.length >= 8 && password.length <= 32) {
            passLength = 1;
        }
    }

    if (hasUpper == 0 || hasLower == 0 || hasNumeric == 0 || password.length < 8 || !password.match(/^(?=.*[#?!@$%^&*-])/)) {
        missingTypes += (hasUpper == 0 ? "an upper case letter " : "") +
            (hasLower == 0 ? "a lower case letter " : "") +
            (hasNumeric == 0 ? "a number " : "") +
            (password.length < 8 ? "at least 8 characters " : "") +
            (!password.match(/^(?=.*[#?!@$%^&*-])/) ? "a symbol " : "");
    }

    strength = hasUpper + hasLower + hasNumeric + hasSymbol + passLength;
    return { strength: strength, missingTypes: missingTypes };
}

function isNumeric(str) {
    if (typeof str != "string") return false // we only process strings!  
    return !isNaN(str) && // use type coercion to parse the _entirety_ of the string (`parseFloat` alone does not do this)...
        !isNaN(parseFloat(str)) // ...and ensure strings of whitespace fail
}