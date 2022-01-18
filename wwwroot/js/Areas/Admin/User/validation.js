$(function() {
    function passwordCheck(password) {
      if (password.length == 0) {
        strength = 0;
      }
      else if (password.length != 0) {
        if (password.match(/^(?=.*[a-z])(?=.*[A-Z])/)) {
          if (password.match(/^(?=.*[0-9])/)) {
            if (password.match(/^.{8,32}$/)) {
              if (password.match(/^(?=.*[#?!@$%^&*-])/)) {
                strength = 5;
              } else {
                strength = 4;
              }
            } else {
              strength = 3;
            }
          } else {
            strength = 2;
          }
        } else {
          strength = 1;
        }
      }
      displayBar(strength);
    }
  
    function displayBar(strength) {
      var statusColor = ["#de1616", "#FFA200", "#06bf06"];
      var statusText = ["Password Weak", "Password Moderate", "Password Strong"];
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
          $("#password-strength-status").css({
            "color": statusColor[0]
          }).text(statusText[0]);
          break;
  
        case 2:
          $("#password-strength span").css({
            "width": "40%",
            "background": statusColor[0]
          });
          $("#password-strength-status").css({
            "color": statusColor[0]
          }).text(statusText[0]);
          break;
  
        case 3:
          $("#password-strength span").css({
            "width": "60%",
            "background": statusColor[0]
          });
          $("#password-strength-status").css({
            "color": statusColor[0]
          }).text(statusText[0]);
          break;
  
        case 4:
          $("#password-strength span").css({
            "width": "80%",
            "background": statusColor[1]
          });
          $("#password-strength-status").css({
            "color": statusColor[1]
          }).text(statusText[1]);
          break;
  
        case 5:
          $("#password-strength span").css({
            "width": "100%",
            "background": statusColor[2]
          });
          $("#password-strength-status").css({
            "color": statusColor[2]
          }).text(statusText[2]);
          break;
  
        case 0:
          $("#password-strength span").css({
            "width": "0",
            "background": statusColor[0]
          });
          $("#password-strength-status").css({
            "color": statusColor[0]
          }).text(statusText[0]);
      }
    }
    $("[data-strength]").after("<div id=\"password-strength-container\"></div>")
    $("#password-strength-container").append("<div id=\"password-strength-status\" class=\"float-end\">Password Weak</div>")
    $("#password-strength-status").after("<div id=\"password-strength\" class=\"strength\"><span></span></div>")
    $("[data-strength]").focus(function() {
      $("#password-strength").css({
        "transition": "margin 0.3s",
        "height": "4px",
        "margin-top": "-4px"
      });
      $("#password-strength-container").css({
        "margin-top": "-28px",
      });
      $("#password-strength-status").css({
        "display": "block",
        "margin": "-4px 8px 4px"
      });
    }).blur(function() {
      $("#password-strength").css({
        "transition": "all 0.3s",
        "height": "0px",
        "margin-top": "0px"
      });
      $("#password-strength-container").css({
        "margin-top": "0px",
      });
      $("#password-strength-status").css({
        "display": "none",
        "margin": "0px 8px 4px"
      });
    });
  
    $("[data-strength]").keyup(function() {
      strength = 0;
      var password = $(this).val();
      passwordCheck(password);
    });
    $("[confirm-pass]").keyup(function() {
      var confirmPass = $(this).val();
      confirmPassword(confirmPass);
    });

    function confirmPassword(confirmPass) {
      var password = $("[data-strength]").val();
      if (confirmPass == password) {
        $("#userCreateForm").find("input[type=submit]").removeAttr("disabled")
      } else {
        $("#userCreateForm").find("input[type=submit]").attr("disabled", "true")
      }
    }
});