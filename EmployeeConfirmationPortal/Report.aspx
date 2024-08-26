<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="EmployeeConfirmationPortal.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html>


    <title>Pending Confirmations</title>
    
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="path/to/toastr.css"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css"/>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/toastr@2.1.4/toastr.min.js"></script>
    <style>


     body {
           
    font-family: 'Ubuntu',"Helvetica Neue",Helvetica,Arial,sans-serif;
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
    .switch {
    position: relative;
    display: inline-block;
    width: 60px;
    height: 34px;
}

.switch input {
    opacity: 0;
    width: 0;
    height: 0;
}

.slider {
    position: absolute;
    cursor: pointer;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: #ccc;
    -webkit-transition: .4s;
    transition: .4s;
}

.slider:before {
    position: absolute;
    content: "";
    height: 26px;
    width: 26px;
    left: 4px;
    bottom: 4px;
    background-color: white;
    -webkit-transition: .4s;
    transition: .4s;
}

input:checked + .slider {
    background-color: #2196F3;
}

input:focus + .slider {
    box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider:before {
    -webkit-transform: translateX(26px);
    -ms-transform: translateX(26px);
    transform: translateX(26px);
}

.slider.round {
    border-radius: 34px;
}

.slider.round:before {
    border-radius: 50%;
}


        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">

     <div class="form-title">
                <h1><strong>EMPLOYEE CONFIRMATION (REPORT)<span><img
  src="Assest/dist/img/report.jpg"
  alt="triangle with all three sides equal"
  height="87"
  width="130" /></span></strong></h1>
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
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" Width="95px" CommandName="Forward" CommandArgument='<%# Bind("division_cd") %>' OnClick="lbl_View_Click"><i class="bi bi-eye-fill"></i> View</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="division_cd" HeaderText="Division Code"/>
                                  <asp:BoundField DataField="division_name" HeaderText="Division" />
                                  <asp:BoundField DataField="num_pending_records" HeaderText="Pending Records" />
                                  <asp:BoundField DataField="num_approved_records" HeaderText="Approved Records" />
                                 <asp:BoundField DataField="num_pending_hr_approval" HeaderText="HR Approval Pending" />
                                  <asp:BoundField DataField="num_approved_hr_approval" HeaderText="HR Approved" />
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
            <h2 class="card-title"><i class="bi bi-stars"></i> Head Of Department</h2>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body p-3"> 
            <div class="card">
              <div class="card-body p-0" style="width: 100%; height: 500px; overflow: auto;">
                  <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center"  AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                  <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" Width="95px" CommandName="Forward" CommandArgument='<%# Bind("Emp_Hod") %>' OnClick="lbl_Show_Click"><i class="bi bi-info-circle-fill"></i> Show</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="Emp_Hod" HeaderText="HOD Employee Code" />
                                  <asp:BoundField DataField="HOD_NAME" HeaderText="HOD Name" />
                                   <asp:BoundField DataField="num_pending_records" HeaderText="HOD Pending Records" />
                                  <asp:BoundField DataField="num_approved_records" HeaderText="HOD Approved Records" />
                                 <asp:BoundField DataField="num_pending_hr_approval" HeaderText="HR Approval Pending" />
                                  <asp:BoundField DataField="num_approved_hr_approval" HeaderText="HR Approved" />

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











      <section id="Section2" runat="server">
    <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
            <h2 class="card-title"><i class="bi bi-table"></i> Total Request</h2>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body p-3"> 
            <div class="card">
              <div class="card-body p-0" style="width: 100%; height: 500px; overflow: auto;">
                  <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-striped text-center" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                  
                                
                                  
                                   <asp:BoundField DataField="emp_cd" HeaderText="Employee Code"/>
                                  <asp:BoundField DataField="emp_name" HeaderText="Employee Name" />

                                  <asp:TemplateField HeaderText="Confirmation Status">
                                      <ItemTemplate>
                                          <asp:Label ID="lbl" runat="server"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>

                                  <asp:BoundField DataField="appointmentdate" HeaderText="Appointment Date" />
                                  <asp:BoundField DataField="Region_Name" HeaderText="Region" />
                                 <asp:BoundField DataField="CADRE_NAME" HeaderText="Cadre" />

                                  <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation"/>
                                  <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />
                                  <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" />
                                  <asp:BoundField DataField="SECTION_NAME" HeaderText="Section" />
                                

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
                    <asp:Label ID="Label2" runat="server" />
                </div>
            </div>

           
        </div>
      </div>
    </section>


        <asp:TextBox runat="server" ID="divcode" visible="false"></asp:TextBox>


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
        </div>
</asp:Content>

