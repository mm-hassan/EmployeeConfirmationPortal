<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LockScreen.aspx.cs" Inherits="EmployeeConfirmationPortal.LockScreen" %>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
   <%--<title>Login - AK Employee Confirmation Portal</title>--%>

  <!-- Google Font: Source Sans Pro -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="Assest/plugins/fontawesome-free/css/all.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="Assest/dist/css/adminlte.min.css">
      <style>
     body {
           /* Preload the background image */
      background-image: url('Assest/dist/img/pic1.jpg');
      /* Other background styles */
  background-size: cover; /* This ensures that the background image covers the entire viewport */
  background-position: center; /* This centers the background image */
  display: flexbox;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  margin: 0; /* Remove default body margin */
        }

        /* Add your existing styles here */
        @keyframes moveArrow {
            0% {
                transform: translateX(0);
            }
            50% {
            }
            100% {
                transform: translateX(0);
            }
        }

        .arrow-animation {
            animation: moveArrow 2s infinite;
        }
            .lockscreen-wrapper {
      background: rgba(255, 255, 255, 0.8);

      box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
    }
    </style>

</head>
<body class="hold-transition lockscreen">
  <div class="background-container"></div>
<!-- Automatic element centering -->
<div class="lockscreen-wrapper shadow-lg p-4 rounded">
  <div class="lockscreen-logo">
    <a href="#"><b>Alkaram</b>Textile Mills</a>
  </div>
  <!-- User name -->
  <div class="lockscreen-name">Employee Confirmation Portal</div><br /><br /><br />

  <!-- START LOCK SCREEN ITEM -->
  <div class="lockscreen-item shadow-lg">
    <!-- lockscreen image -->
    <div class="lockscreen-image">
      <%--<img src="Assest/dist/img/user1-128x128.jpg" alt="User Image">--%>
        <img src="Assest/dist/img/loginlogo128128.jpg" alt="User Image">
    </div>
    <!-- /.lockscreen-image -->

    <!-- lockscreen credentials (contains the form) -->
    <form id="loginForm" class="lockscreen-credentials" runat="server">
      <div class="input-group shadow-lg">
          <asp:TextBox ID="txt_EmployeeCode" runat="server" class="form-control" placeholder="Employee Code"></asp:TextBox>
          <asp:TextBox ID="txt_EmployeePin" runat="server" class="form-control" placeholder="ESS Pin" TextMode="Password"></asp:TextBox>
<%--        <input type="text" class="form-control" placeholder="Emp Code" runat="server" ID="txt_EmployeeCode" required>  
        <input type="password" class="form-control" placeholder="ESS Pin" runat="server" ID="txt_EmployeePin" required>--%>
        <div class="input-group-append">
           <%-- <asp:LinkButton ID="btn_login" runat="server" OnClick="btnLogin_Click" class="btn">
               <i class="fas fa-arrow-right text-muted"></i>
            </asp:LinkButton>--%>

          <button type="button" id="btnLogin" class="btn" runat="server" onserverclick="btnLogin_Click">
            <i class="fas fa-arrow-right text-muted arrow-animation"></i>
          </button>
        </div>
      </div>
    </form>
    <!-- /.lockscreen credentials -->

  </div>
  <!-- /.lockscreen-item -->
  <div class="help-block text-center pt-5">
    Enter your ESS credentials to retrieve your session.
  </div>
  <div class="text-center">
<%--    <a href="#" class="text-danger" id="ermsj">Use this application on Google Chrome for the best experience</a>--%>
          <a href="#" class="text-danger" id="ermsj"></a>
  </div>
  <div class="lockscreen-footer text-center pt-3">
    Copyright &copy; 2023 <b><a href="#" class="text-black">BT Department</a></b><br>
    All rights reserved
  </div>
</div>
<!-- /.center -->

<!-- jQuery -->
<script src="Assest/plugins/jquery/jquery.min.js"></script>
<!-- Bootstrap 4 -->
<script src="Assest/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
      <script>
          // Get the input element
          var inputElement = document.getElementById("txt_EmployeeCode");

          // Set the autocomplete attribute to "off"
          inputElement.setAttribute("autocomplete", "off");

          var browserName = "";
          var userAgent = navigator.userAgent;
          if (userAgent.indexOf("Firefox") > -1) {
              browserName = "Mozilla Firefox";
          } else if (userAgent.indexOf("Opera") > -1 || userAgent.indexOf("OPR") > -1) {
              browserName = "Opera";
          } else if (userAgent.indexOf("Trident") > -1 || userAgent.indexOf("MSIE") > -1) {
              browserName = "Microsoft Internet Explorer";
          } else if (userAgent.indexOf("Edge") > -1) {
              browserName = "Microsoft Edge";
          } else if (userAgent.indexOf("Chrome") > -1) {
              browserName = "Google Chrome";
          } else if (userAgent.indexOf("Safari") > -1) {
              browserName = "Apple Safari";
          } else {
              browserName = "Unknown Browser";
          }
          // console.log("Browser Name: " + browserName);
          if (browserName != "Google Chrome") {
              document.getElementById('ermsj').innerHTML = 'Run This Application On Google Chrome For Better Experience.';
              //alert('Some functionalities may not work on this browser !');
          }


    </script>

           

    <!-- jQuery -->
<script src="Assest/plugins/jquery/jquery.min.js"></script>
<!-- Bootstrap 4 -->
<script src="Assest/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
<script>
    // Function to handle form submission on Enter key press
    document.getElementById('loginForm').addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault(); // Prevent default form submission
            document.getElementById('btnLogin').click(); // Simulate click on login button
        }
    });
</script>


</body>
</html>
