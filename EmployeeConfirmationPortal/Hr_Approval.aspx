<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Hr_Approval.aspx.cs" Inherits="EmployeeConfirmationPortal.Hr_Approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <!DOCTYPE html>


    <title>HR Approval</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css"/>
    <link rel="stylesheet" type="text/css" href="path/to/toastr.css"/>

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/toastr@2.1.4/toastr.min.js"></script>

     <style>


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
         .btn.btn-dark {
        color: white; /* Set the desired text color */
    }
    .btn.btn-dark:hover {
        color: white; /* Set the same text color to maintain consistency */
    }
    .toast-info {
    background-color: #3498db;
}
        .toast-warning {
    background-color: #e67e22;
}

          .invisible-border {
        border: none;
    }
          tr:nth-child(even) {
        background-color: #f2f2f2;
    }

          #loadingIndicator {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(255, 255, 255, 0.7);
        z-index: 9999;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .spinner-border {
        width: 3rem;
        height: 3rem;
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
         

            <div class="form-title">
                <h1><strong>EMPLOYEE CONFIRMATION (HR Approvals)</strong><span><img
  src="Assest/dist/img/approval.jpg"
  alt="triangle with all three sides equal"
  height="77"
  width="150" /></span></h1>
            </div>






    <section class="content">
      
      <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
          <h2 class="card-title"><i class="bi bi-clipboard-data-fill"></i> Divisions</h2>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body p-1"> 
            <div class="card">
              <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">
                 <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped text-center"  AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" Width="85px" CommandName="View" CommandArgument='<%# Bind("division_cd") %>' OnClick="lbl_View_Click"><i class="bi bi-eye-fill"></i> View</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="division_cd" HeaderText="Division Code"/>
                                  <asp:BoundField DataField="division_name" HeaderText="Division" />
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
                    <asp:Label ID="Label1" runat="server" />
                </div>
            </div>

           
        </div>
      </div>

        </section>


        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="sub" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />
          <section id="div_EmployeeDetails" runat="server">
    <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
            <h2 class="card-title"><i class="bi bi-list-task"></i> Pending Requests</h2>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body p-3"> 
            <div class="card">
              <div class="card-body p-0" style="width: 100%; height: 100%; overflow: auto;">
                  <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center"  AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                   <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_Forward" type="button" Class="btn btn-dark" runat="server" Width="95px" Font-Size="Small" CommandName="ShowDetails" CommandArgument='<%# Bind("REQUEST_ID") %>' OnClick="lbl_Show_Click"><i class="bi bi-info-circle-fill"></i>  Details</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="REQUEST_ID" HeaderText="Request Id"/>
                                  <asp:BoundField DataField="emp_cd" HeaderText="Employee Code"/>
                                  <asp:BoundField DataField="emp_name" HeaderText="Employee Name" />
                                  <asp:BoundField DataField="Department_Name" HeaderText="Department" />
                                  <asp:BoundField DataField="Designation_Name" HeaderText="Designation"/>
                                  <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit"/>
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

     
    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="TextBox4" runat="server" Visible="false"></asp:TextBox>





















    <section id="Section1" runat="server">
        
        <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
            <h2 class="card-title"><i class="bi bi-check2-square"></i> Pending Approval</h2>
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
                        <strong>HOD Confirmation Status: </strong>
                        <asp:TextBox id="TextBox11" type="text" Size="18px" runat="server" CssClass="invisible-border"  ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>








                <tr >
                    <td colspan="1">
                        <strong>Region: </strong>
                        <asp:TextBox id="TextBox12" type="text" BackColor="#f2f2f2" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td colspan="2"> <strong>Division: </strong>
                    <asp:TextBox id="TextBox13" type="text" Size="18px" BackColor="#f2f2f2" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <strong>Unit: </strong>
                        <asp:TextBox id="TextBox14" type="text" Size="22px" BackColor="#f2f2f2" runat="server" CssClass="invisible-border"  ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>



                        <tr>
                             <td colspan="2"><strong>
                         Appointment Date: </strong>
                        <asp:TextBox id="TextBox15" type="text" Size="24px"  runat="server" CssClass="invisible-border" ReadOnly="true" ></asp:TextBox>
                    </td>
                             <td>
                        <strong>Request Id: </strong>
                         <asp:TextBox id="TextBox17" type="text" Size="4px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                            <td>
                        <strong>Request Date:</strong>
                        <asp:TextBox id="TextBox10" type="text" Size="24px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                        </tr>
                        
                        
                          <tr>
                    <td colspan="4">
                        <label for="Per_Remarks">Areas in which improved performance is required:</label>
                         <asp:TextBox ID="Per_Remarks" runat="server" type="text" Size="100%" BackColor="#f2f2f2" CssClass="invisible-border"  ReadOnly="true"></asp:TextBox>
                    </td>
                              </tr>
                        <tr>
                    <td colspan="4">
                        <label for="Train_Remarks">Trainings to support the probationer:</label>
                         <asp:TextBox ID="Train_Remarks" type="text" Size="100%" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                          
                </tr>
               <tr>
                     <td colspan="4">
                        <label for="Hod_Remarks">HOD Remarks:</label>
                         <asp:TextBox ID="Hod_Remarks" type="text" Size="100%" runat="server" BackColor="#f2f2f2" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
               </tr>
                         <tr>
                    <td colspan="4">
                        <label for="Probation Period">Probation Period Extension:</label>
                         <asp:TextBox ID="Prob_Period" type="text" Size="100%" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                          
                </tr>
                
                </table>
                    </div>       
            <div>             

        
                <asp:GridView ID="GridViewParam" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                
                                  <asp:BoundField DataField="detail_id" ItemStyle-Width="10%" HeaderText="Id" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"/> 
                                  <asp:BoundField DataField="detail_name" ItemStyle-Width="70%" HeaderText="Parameters" HeaderStyle-CssClass="text-center"/> 
                                   <asp:BoundField DataField="SCORE" ItemStyle-Width="70%" HeaderText="Score" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"/> 
                                  
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

                        
                <div class="row">
                <div class="col-md-12">
                    
                    <div class="input-group mb-3">
                      <div class="input-group-prepend">
                        <asp:Button ID="lbl_Approve_Button" ValidationGroup="sub" runat="server" class="btn btn-success" Font-Bold="true" Text="Approve" OnClientClick="return validateAndShowSpinner();" OnClick="lbl_Approve_Button_Click"></asp:Button>
                <asp:Button ID="lbl_Reject_Button" runat="server" class="btn btn-danger" Font-Bold="true" Text="Revert" ValidationGroup="sub" OnClientClick="return validateAndShowSpinner();" OnClick="lbl_Reject_Button_Click"></asp:Button>
                      </div>
                        <asp:TextBox type="text" ID="remarkstext" class="form-control" placeholder="Remarks from HR" aria-label="" aria-describedby="basic-addon1" runat="server" ></asp:TextBox>
                         <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="remarkstext" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Remarks field cannot be empty"  ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    </div>


                    
                </div>
                    </div>
                </div>


      
        </div>
            </div>


</section>

     <script type="text/javascript">
         function validateAndShowSpinner() {
             if (Page_ClientValidate('sub')) {
                 showLoadingSpinner();
                 return true;
             } else {
                 return false;
             }
         }

         function showLoadingSpinner() {
             document.getElementById('<%= pnlLoading.ClientID %>').style.display = 'block';
    }
</script>

<asp:Panel ID="pnlLoading" runat="server" CssClass="loading-panel">
    <div class="loading-spinner"></div>
    <p>Loading...</p>
</asp:Panel>
        </div>






    <script>

        function showSuccessToast() {
            console.log('showSuccessToast called');
            toastr.success('Approved! Employee confirmation process completed', 'Success');
        }

        function showRejectSuccessToast() {
            console.log('showRejectSuccessToast called');
            toastr.success('Reset! Request send back to HOD', 'Success');
        }

        function showErrorToast() {
            console.log('showErrorToast called');
            toastr.error('Not sucessfully approved!', 'Error');
        }

            function showRejectErrorToast() {
                console.log('showRejectErrorToast called');
                toastr.error('Error rejecting confirmation!', 'Error');
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

</script>
</asp:Content>
