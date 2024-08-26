<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HOD_Review.aspx.cs" Inherits="EmployeeConfirmationPortal.HOD_Review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>HOD Review</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="path/to/toastr.css"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css"/>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/toastr@2.1.4/toastr.min.js"></script>

    <style>
        .invisible-border {
        border: none;
    }

           tr:nth-child(even) {
        background-color: #f2f2f2;
    }
         


    .performance-status label {
        font-size: 18px;
        color: #333;
        margin-right: 10px;
    }

    .option-container {
        display: flex;
        justify-content: space-around;
        margin-top: 10px;
    }

    .option {
        display:flexbox;
        cursor: pointer;
        transition: transform 0.2s ease-in-out;
    }

    .option:hover {
        transform: scale(1.1);
    }



    body {
           font-family: 'Roboto', sans-serif;
            background-color: #f8f9fa;
        }
        .content-wrapper {
            padding:10px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            background-color: #fff;
        }
        h1 {
           text-align: center;
            color: #007bff;
            margin-bottom: 30px;
            font-size: 1.5rem;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-weight: 700;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 30px;
            border-radius: 5px;
            overflow: hidden;
            box-shadow: 0 0 12px rgba(0, 0, 0, 0.1);

            
        }
        th{
            color:white;
            background-color: #343a40;
        }
        th, td {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: left;
        }
                

.performance-status {
            max-width: 805px;
            margin: auto;
            background-color: #fff;
             text-align: center;
            padding: 20px;
            border-radius: 12px;
            margin-bottom: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        }

        .option-container {
            display: flexbox;
            justify-content: space-between;
            margin-bottom: 15px;
        }

        .option {
            flex: 1;
            text-align: center;
        }

        .custom-textarea {
            width: 100%;
            resize: none;
        }

        .btn-submit {
            width: 100%;
        }


        .toast-info {
    background-color: #3498db;
}
        .toast-warning {
    background-color: #e67e22;
}
      


         .evaluation-card {
    width: 100%;
    height: 300px;
    border: 1px solid #ccc;
    border-radius: 8px;
    overflow-y: auto;
    padding: 20px;
    box-sizing: border-box;
    background-color: #f9f9f9;
}

.card-heading {
    text-align: center;
    font-size: 1.5em;
    margin-bottom: 15px;
    color: #007bff;

}

.legend-list {
    list-style-type: none;
    padding: 0;
    margin: 0;
}

.legend-list li {
    margin-bottom: 8px;
}

.legend-list li strong {
    margin-right: 5px;
}








/* Adjustments for small screens */
        @media only screen and (max-width: 600px) {
            .performance-status {
                width: 100%;
            }

            .option-container {
                display: flexbox;
                flex-direction: column;
            }

            .option {
                margin-bottom: 10px;
            }

            table {
                width: 100%;
            }

                table td {
                    display: block;
                    width: 100%;
                }

            .table-container {
                overflow-x: auto;
            }

            .evaluation-card {
                margin-top: 20px;
                text-align: center;
            }

            .legend-list {
                padding-left: 0;
                list-style: none;
            }

                .legend-list li {
                    margin-bottom: 10px;
                }

            .table-responsive {
                overflow-x: auto;
            }
 }

          .option-container {
        position: relative; /* Positioning context for overlay */
    }

     .radio-overlay {
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: rgba(255, 255, 255, 0); /* Transparent */
        z-index: 1; /* On top of the radio buttons */
    }

    .readonly-radio input[type="radio"] {
        position: relative; /* Ensure correct stacking */
        z-index: 5; /* Behind the overlay */
    }

        /* Adjustments for very small screens  */
@media only screen and (max-width: 400px) {
  .table-container {
    font-size: 0.8rem;  /* Adjust font size for smaller screens */
  }         


}

/* Add this CSS in your CSS file or within <style> tags in your ASP.NET page */

.loading-panel {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(255, 255, 255, 0.7); /* semi-transparent white background */
    display: none; /* hide by default */
    z-index: 9999; /* make sure it's on top of other elements */
}

.spinner {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    border: 4px solid rgba(0, 0, 0, 0.1);
    border-left-color: #3498db;
    border-radius: 50%;
    width: 50px;
    height: 50px;
    animation: spin 1s linear infinite; /* spinning animation */
}

.loading-text {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    color: #333;
    font-size: 18px;
    font-weight: bold;
}

@keyframes spin {
    to {
        transform: rotate(360deg);
    }
}
.loading-panel {
        display: none;
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(255, 255, 255, 0.9);
        z-index: 9999;
    }

    .loading-spinner {
        border: 4px solid #f3f3f3; /* Light grey */
        border-top: 4px solid #3498db; /* Blue */
        border-radius: 50%;
        width: 50px;
        height: 50px;
        animation: spin 1s linear infinite;
        margin: auto;
        margin-top: 45vh; /* Adjust vertical position */
    }

    @keyframes spin {
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }

    .loading-panel p {
        text-align: center;
        font-size: 20px;
        color: #555;
        margin-top: 20px;
    }

        </style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="content-wrapper">
          


          <!-- Employee Confirmation Section -->
         <div class="row">
             <div class="col-md-12">

                 <div class="form-title">
                     <h1><strong>EMPLOYEE CONFIRMATION (HOD Review)<span><img
                         src="Assest/dist/img/hod.jpg"
                         alt="triangle with all three sides equal"
                         height="87"
                         width="130" /></span></strong></h1>
                 </div>

                 <section class="content">

                     <div class="card" id="div_EmployeeDetails">
                         <div class="card-header" style="background-color: #363940; color: white;">
                             <h2 class="card-title"><i class="bi bi-list-task"></i>Pending Request</h2>
                             <div class="card-tools">
                                 <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                     <i class="fas fa-minus"></i>
                                 </button>
                             </div>
                         </div>
                         <!-- Card Body -->

                         <div class="card-body p-1">
                             <div class="card">
                                 <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">

                                     <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center" AutoGenerateColumns="false">
                                         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                         <Columns>
                                             <asp:TemplateField HeaderText="Action">
                                                 <ItemTemplate>
                                                     <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" Width="85px" CommandName="Forward" CausesValidation="false" CommandArgument='<%# Bind("EMP_CD") %>' OnClick="lbl_View_Click"><i class="bi bi-eye-fill"></i> View</asp:LinkButton>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <%--<asp:BoundField DataField="REQUEST_ID" HeaderText="Request Id" /> --%>
                                             <asp:BoundField DataField="EMP_CD" HeaderText="Employee Code" />
                                             <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" />
                                             <%--<asp:BoundField DataField="REQUEST_DATE" HeaderText="Request Date" />--%>
                                         </Columns>
                                         <EditRowStyle BackColor="#999999" />
                                         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                         <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                         <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                         <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                         <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                         <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                         <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                         <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                     </asp:GridView>

                                 </div>
                             </div>
                         </div>
                         <div class="card-footer">

                             <div class="row">
                                 <div class="col-8">
                                 </div>
                                 <div class="col-4 text-right">
                                     <asp:Label ID="lbl_GridMsg" runat="server" />
                                 </div>
                             </div>

                         </div>
                     </div>


                 </section>
             </div>
         </div>



         <!-- Evaluation Form Section -->
         <div class="row">
             <div class="col-md-12">
                 <section id="Section1" runat="server">
                     <!-- Evaluation Form Card -->
                     <div class="card">
                         <div class="card-header" style="background-color: #363940; color: white;">
                             <h2 class="card-title"><i class="bi bi-check2-square"></i>Evaluation Form (3 Month Review)</h2>
                             <div class="card-tools">
                                 <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
                                     <i class="fas fa-minus"></i>
                                 </button>
                             </div>
                         </div>
       


        <div class="card-body p-3">
            
                <div class="card-body p-0" style="width: 100%;  overflow: auto;">

                 
                    <table class="tracking-table">
                       

                <tr>
                    <td colspan="1"><strong>
                         Employee Code: </strong>
                        <asp:TextBox id="TextBox2" type="text" Size="18px"  runat="server" CssClass="invisible-border" ReadOnly="true" ></asp:TextBox>
                    </td>
                    
                    <td colspan="3">
                        <strong>Employee Name: </strong>
                        <asp:TextBox id="TextBox3" type="text"  runat="server" Size="60px" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                  
                   
                </tr>
                <tr>
                    <td>
                        <strong>Department: </strong>
                         <asp:TextBox id="TextBox6" type="text" BackColor="#f2f2f2" Size="30px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    
                    <td colspan="2">
                        <strong>Designation: </strong>
                        <asp:TextBox id="TextBox7" type="text" BackColor="#f2f2f2" Size="28px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    
                    <td>
                        <strong>Region Code:</strong>
                        <asp:TextBox id="TextBox16" type="text" Size="17px" BackColor="#f2f2f2" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    
                    
                    
                </tr>


                 <tr >
                    <td colspan="1">
                        <strong>Cadre: </strong>
                        <asp:TextBox id="TextBox8" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td colspan="2"> <strong>Section: </strong>
                    <asp:TextBox id="TextBox9" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <%--<strong>Confirmation Status: </strong>
                        <asp:TextBox id="TextBox11" type="text" Size="18px" runat="server" CssClass="invisible-border"  ReadOnly="true"></asp:TextBox>--%>

                        <strong>Unit: </strong>
                        <asp:TextBox id="TextBox14" type="text" Size="18px" runat="server" CssClass="invisible-border"  ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="1">
                        <strong>Region: </strong>
                        <asp:TextBox id="TextBox12" type="text" BackColor="#f2f2f2" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td colspan="2"> <strong>Division: </strong>
                    <asp:TextBox id="TextBox13" type="text" Size="18px" BackColor="#f2f2f2" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <strong>
                         Appointment Date: </strong>
                        <asp:TextBox id="TextBox15" type="text" Size="18px" BackColor="#f2f2f2"  runat="server" CssClass="invisible-border" ReadOnly="true" ></asp:TextBox>
                    </td>
                </tr>



                        
                
                </table>
                    </div>


                <blockquote>
                    • Please complete the following form providing a report on the above mentioned probationer’s employment progress at the end of 2.5 months.</blockquote>
                <blockquote>
                    • Indicate areas for improvement / training needs (If any).</blockquote>


            <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />
       
            <table>
    <tr>
        <td>
            <div>
                <asp:GridView ID="GridViewParam" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="detail_id" ItemStyle-Width="10%" HeaderText="Id" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                        <asp:BoundField DataField="detail_name" ItemStyle-Width="70%" HeaderText="Parameters" HeaderStyle-CssClass="text-center" />
                        <asp:TemplateField HeaderText="Score" ItemStyle-Width="16px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:TextBox id="Param1" type="number" min="1" max="5" Size="4px" CssClass="text-center" MaxLength="1" runat="server" onchange="calculateTotal()"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="Param1" ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Score field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="Param1" Display="Dynamic" ErrorMessage="Score should be between 1 to 5" ForeColor="Red" MaximumValue="5" MinimumValue="1" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </td>
        <td>
            <div class="evaluation-card">
                <h2 class="card-heading"><i class="bi bi-bookmarks-fill"></i> Evaluation Score</h2>
                <div class="card-body">
                    <ul class="legend-list">
                        <li><strong>1:</strong> Below Average</li>
                        <li><strong>2:</strong> Average</li>
                        <li><strong>3:</strong> Good</li>
                        <li><strong>4:</strong> Very Good</li>
                        <li><strong>5:</strong> Excellent</li>
                    </ul>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="TotalScoreLabel" runat="server" Text="Total Score: 0/25"></asp:Label>
        </td>
    </tr>
</table>
          <script>
            
              function calculateTotal() {
                  var totalScore = 0;
                  var textBoxes = document.querySelectorAll("#<%= GridViewParam.ClientID %> input[type='number']");
    textBoxes.forEach(function (textBox) {
        totalScore += parseInt(textBox.value) || 0;
    });

    var obtainedScore = totalScore;

                  // Determine which checkboxes to select based on the obtained score
    var chkSatisfactory = document.getElementById("<%= radSatisfactory.ClientID %>");
    var chkNonSatisfactory = document.getElementById("<%= radNonSatisfactory.ClientID %>");
                  var chkProbExtended = document.getElementById("<%= radProbExtended.ClientID %>");

                  // Hide probation extension options by default
                  var probationOptions = document.getElementById("probationOptions");
                  probationOptions.style.display = "none";

                  if (obtainedScore < 8) {
                      chkSatisfactory.checked = false;
                      chkNonSatisfactory.checked = true;
                      chkProbExtended.checked = false;
                  } else if (obtainedScore < 12) {
                      chkSatisfactory.checked = false;
                      chkNonSatisfactory.checked = false;
                      chkProbExtended.checked = true;
                  } else {
                      chkSatisfactory.checked = true;
                      chkNonSatisfactory.checked = false;
                      chkProbExtended.checked = false;
                  }

                  // Show probation extension options if "Probation Extended" checkbox is checked
                  if (chkProbExtended.checked) {
                      probationOptions.style.display = "block";
                  }

                  // Update the total score label
                  var totalPossibleScore = 25;
                  document.getElementById("<%= TotalScoreLabel.ClientID %>").innerText = "Total Score: " + obtainedScore + "/" + totalPossibleScore;
}




             
</script>

                <asp:TextBox ID="TextBox4" runat="server" ReadOnly="true" Size="55px" style="outline:none; border:none;" type="text" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="TextBox5" runat="server" Size="55px" type="text" style="outline:none; border:none;" ReadOnly="true" Visible="false"></asp:TextBox>




            <div class="performance-status">
    <label style="color: #007bff; font-size: 23px;"><i class="bi bi-award-fill"></i> Performance Status</label>

    <div class="option-container">
        <!-- Overlay div to prevent user interaction -->
        <div class="radio-overlay"></div>

        <div class="option">
            <asp:RadioButton ID="radSatisfactory" runat="server" GroupName="satisfactionGroup" Text="Satisfactory" />
        </div>

        <div class="option">
            <asp:RadioButton ID="radNonSatisfactory" runat="server" GroupName="satisfactionGroup" Text="Unsatisfactory" />
        </div>

        <div class="option">
            <asp:RadioButton ID="radProbExtended" runat="server" GroupName="satisfactionGroup" Text="Probation Extended" />
        </div>
    </div>




    <table>
        <tr>
            <td>
                <label for="Per_Remarks">Areas in which improved performance is required:</label>
            </td>
            <td>
                <label for="Train_Remarks">Trainings to support the probationer:</label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="Per_Remarks" runat="server" Rows="2" TextMode="MultiLine" Columns="35"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="Per_Remarks" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Score field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="Train_Remarks" runat="server" Rows="2" TextMode="MultiLine" Columns="35"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="Train_Remarks" ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Score field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
<!-- Probation extension options -->
<div id="probationOptions" style="display: none; ; padding-bottom: 30px;">
    <label style="color: #007bff">Select Probation Extension Period:</label>
    <asp:DropDownList ID="ddlProbationExtension" runat="server" CssClass="form-control">
        <asp:ListItem Value="1 Month">1 Month</asp:ListItem>
        <asp:ListItem Value="2 Months">2 Months</asp:ListItem>
        <asp:ListItem Value="3 Months">3 Months</asp:ListItem>
    </asp:DropDownList>
</div>
                
    <dl>

        <dd>Note: If performance continues to remain unsatisfactory, the appointment may be terminated.</dd>
    </dl>

    <div class="input-group mb-3">
        <div class="input-group-prepend">
 <asp:Button ID="Button1" runat="server" class="btn btn-success" Font-Bold="true" 
                    OnClick="btnSubmitPerformanceStatus_Click" 
                    OnClientClick="return validateAndShowSpinner()" 
                    Text="Submit" />        </div>
        <asp:TextBox type="text" ID="TextBox1" class="form-control" placeholder="HOD Remarks" aria-label="" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="TextBox1" ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="HOD Remarks field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
    </div>

                
</div>

   


          






                    <!-- Include Bootstrap JS and Popper.js -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
            
        </div>

    <br />





        </div>
</section>
</div>
             <script type="text/javascript">
                 function validateAndShowSpinner() {
                     if (Page_ClientValidate()) {
                         showLoadingSpinner();
                         return true;
                     }
                     return false;
                 }

                 function showLoadingSpinner() {
                     document.getElementById('<%= pnlLoading.ClientID %>').style.display = 'block';
    }
</script>

<asp:Panel ID="pnlLoading" runat="server" CssClass="loading-panel" style="display: none;">
    <div class="loading-spinner"></div>
    <p>Loading...</p>
</asp:Panel>
        </div>
    </div>





















         <script>

    function showSuccessToast() {
        console.log('showSuccessToast called');
        toastr.success('Request sucessfully submitted!', 'Success');
    }

    function showErrorToast() {
        console.log('showErrorToast called');
        toastr.error('Request not sucessfully submitted!', 'Error');
    }

    function showDatabaseErrorToast() {
        console.log('showErrorToast called');
        toastr.error('Database backend error!', 'Error');
    }

    function showInfoToast() {
        console.log('showErrorToast called');
        toastr.info('This is an information message', 'Info');

    }

    function showWarningToast() {
        console.log('showErrorToast called');
        toastr.warning('Warning: Please select a checkbox!', 'Warning');

    }

    function showRemarksWarningToast() {
        console.log('showErrorToast called');
        toastr.warning('Warning: Please enter remarks!', 'Warning');

    }

</script>







    </div>
</asp:Content>
