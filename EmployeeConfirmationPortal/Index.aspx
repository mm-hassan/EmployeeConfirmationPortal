<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EmployeeConfirmationPortal.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Portfolio Page</title>

    <script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>

    
    
    <style>
        @import url(https://fonts.googleapis.com/css?family=Dancing+Script|Ubuntu|Roboto:400,700);

/*html, body{
  height: 100%;
}*/

body{
  font-family: "Ubuntu", sans-serif;
}

#splash{


  background-image: url('Assest/dist/img/pic2.jpg');
  background-size: cover;
  background-position: center;
  height: 100%;
  padding-top: 30%;
  padding-bottom: 55px;
  color: #fff;
  font-size: 3.5em;
  text-shadow: 0px 4px 3px rgba(0,0,0,0.4),
               0px 8px 13px rgba(0,0,0,0.5),
               0px 18px 23px rgba(0,0,0,0.1);
}




  #splash h1{
    font-size: 2em;
  }

  #splash a{
    color: #fff;
  }

  #splash a:hover{
    color: #e7e7e7;
  }




hr{
  width: 30%;
  color: white;
  background-color: white;
}
    </style>
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
      <div class="content-wrapper">
         
       
       <!-- Start Splash Section  -->
    <a class="anchor" id="home"></a>
    <div id="splash" class="container-fluid">
      <div class="row">
        <div class="col-lg-12 text-center">
          <h1>Welcome To</h1>
          <hr />
          <p>
            Employee Confirmation Portal
          </p>
          
        </div>
      </div>
    </div>
    <!-- End Splash Section  -->

   

    <script type="text/javascript">
        $(document).ready(function () {
            $(".navbar-nav li").click(function () {
                $(".navbar-nav li").removeClass("active");
                $(this).addClass("active");
            }
            );

            $('.navbar-nav a').click(function (event) {
                event.preventDefault();
                $('html, body').animate({
                    scrollTop: $($.attr(this, 'href')).offset().top
                }, 500);
            });

        }
        );
    </script>
   

    

         </div>
            </div>
        </div>
</asp:Content>
