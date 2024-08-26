<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Status.aspx.cs" Inherits="EmployeeConfirmationPortal.Status" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css"/>
      <style>
         .invisible-border {
        border: none;
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
         .btn.btn-dark {
        color: white; /* Set the desired text color */
    }
    .btn.btn-dark:hover {
        color: white; /* Set the same text color to maintain consistency */
    }

    table,
thead,
tbody,
tfoot,
tr,
th,
td {
        
  padding: 10px;
  
}
     tr:nth-child(even) {
        background-color: #f2f2f2;
    }
    colgroup {
  	width: 250px;
}
        </style>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
         

          <div class="form-title">
                <h1><strong>EMPLOYEE CONFIRMATION (Status)</strong><span><img
  src="Assest/dist/img/statuss.jpg"
  alt="triangle with all three sides equal"
  height="87"
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
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" Width="95px" CommandName="View" CommandArgument='<%# Bind("division_cd") %>' OnClick="lbl_View_Click"><i class="bi bi-eye-fill"></i> View</asp:LinkButton>
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
              <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">
                  <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center"  AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                  <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_Forward" type="button" Class="btn btn-dark" runat="server" Width="85px" Font-Size="Small" CommandName="View" CommandArgument='<%# Bind("emp_cd") %>' OnClick="lbl_Forward_Click"><i class="bi bi-info-circle-fill"></i> Details</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  
                                  <asp:BoundField DataField="emp_cd" HeaderText="Employee Code"/>
                                  <asp:BoundField DataField="emp_name" HeaderText="Employee Name" />
                                  <asp:BoundField DataField="Department_Name" HeaderText="Department" />
                                  <asp:BoundField DataField="Designation_Name" HeaderText="Designation"/>
                                  
                                  <asp:BoundField DataField="reg_cd" HeaderText="Reg Code"/>
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







        <section id="emp_Details" runat="server">
    <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
            <h2 class="card-title"><i class="bi bi-check2-square"></i> Employee Evaluation</h2>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body p-3"> 
          
              <div class="card-body p-0" style="width: 100%; height: 100%; overflow: auto;">
     
                  <table class="tracking-table">
                       

                <tr>
                    <td colspan="1"><strong>
                         Employee Code: </strong>
                        <asp:TextBox id="TextBox1" type="text" Size="18px"  runat="server" CssClass="invisible-border" ReadOnly="true" ></asp:TextBox>
                    </td>
                    
                    <td colspan="2">
                        <strong>Employee Name: </strong>
                        <asp:TextBox id="TextBox2" type="text"  runat="server" Size="40px" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                   <td colspan="2">
                        <strong>Appointment Date: </strong>
                        <asp:TextBox id="TextBox16" type="text"  runat="server" Size="20px" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <strong>Department: </strong>
                         <asp:TextBox id="TextBox3" type="text" Size="25px" BackColor="#f2f2f2" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    
                    <td colspan="2">
                        <strong>Designation: </strong>
                        <asp:TextBox id="TextBox4" type="text" Size="28px" runat="server" BackColor="#f2f2f2" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    
                     <td>
                        <strong>Request Id: </strong>
                         <asp:TextBox id="TextBox5" type="text" Size="4px" runat="server" BackColor="#f2f2f2" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                   
                    
                    
                </tr>
                      <tr>
                          <td colspan="1">
                        <strong>Cadre: </strong>
                        <asp:TextBox id="TextBox10" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                          <td colspan="2"> <strong>Section: </strong>
                    <asp:TextBox id="TextBox11" type="text" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                          <td>
                        <strong>Request Date:</strong>
                        <asp:TextBox id="TextBox6" type="text" Size="17px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                      </tr>

                        <tr>
                          <td colspan="1">
                        <strong>Region: </strong>
                        <asp:TextBox id="TextBox13" type="text"  BackColor="#f2f2f2" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                          <td colspan="2"> <strong>Division: </strong>
                    <asp:TextBox id="TextBox14" type="text"  BackColor="#f2f2f2" Size="18px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                          <td>
                        <strong>Unit:</strong>
                        <asp:TextBox id="TextBox15" type="text"  BackColor="#f2f2f2" Size="17px" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                      </tr>

                         <tr >
                    <td colspan="5">
                        <strong>Current Status: </strong>
                        <asp:TextBox id="TextBox18" type="text" Size="100%" runat="server"  CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>

                </tr>



                <tr >
                    <td colspan="5">
                        <strong>Confirmation Status: </strong>
                        <asp:TextBox id="TextBox7" type="text" Size="100%" runat="server" BackColor="#f2f2f2"  CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>

                </tr>
                
                <tr>
                    <td colspan="5"> <strong>Confirmed by Hod: </strong>
                    <asp:TextBox id="TextBox8" type="text" Size="100%" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                    
                   
                </tr>
                <tr>
                     <td colspan="5">
                        <strong>Hod Confirm Status: </strong>
                        <asp:TextBox id="TextBox9" type="text" Size="100%" BackColor="#f2f2f2" runat="server" CssClass="invisible-border"  ReadOnly="true"></asp:TextBox>
                    </td>
                    
                </tr>
                      <tr>
                     <td colspan="5">
                        <strong>Areas in which improved performance is required: </strong>
                        <asp:TextBox id="TextBox12" type="text" Size="100%" runat="server" CssClass="invisible-border"  ReadOnly="true"></asp:TextBox>
                    </td>
                    
                </tr>
                      <tr>
                     <td colspan="5">
                        <strong>Trainings to support the probationer: </strong>
                        <asp:TextBox id="TextBox17" type="text" Size="100%" runat="server" BackColor="#f2f2f2" CssClass="invisible-border"  ReadOnly="true"></asp:TextBox>
                    </td>
                    
                </tr>
                      <tr>
                          <td colspan="5">
                        <label for="Hod_Remarks">HOD Remarks:</label>
                         <asp:TextBox ID="Hod_Remarks" type="text" Size="100%" runat="server" CssClass="invisible-border" ReadOnly="true"></asp:TextBox>
                    </td>
                      </tr>
                      <tr>
                     <td colspan="5">
                        <strong>Probation Period Extension: </strong>
                        <asp:TextBox id="Probation_period" type="text" Size="100%" runat="server" BackColor="#f2f2f2" CssClass="invisible-border"  ReadOnly="true"></asp:TextBox>
                    </td>
                    
                </tr>


            </table>


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




                  </div>
                </div>
            </div>
       
            </section>
    </div>



    <script>
        function showSuccessToast() {
            console.log('showSuccessToast called');
            toastr.success('Request sucessfully forwarded!', 'Success');
        }

        function showErrorToast() {
            console.log('showErrorToast called');
            toastr.error('Request not sucessfully forwarded!', 'Error');
        }

        function shownNoPendingToast() {
            console.log('shownNoPendingToast called');
            toastr.error('No Pending Data!', 'Error');
        }

        function showDatabaseErrorToast() {
            console.log('showErrorToast called');
            toastr.error('Database backend error!', 'Error');
        }




</script>


</asp:Content>
