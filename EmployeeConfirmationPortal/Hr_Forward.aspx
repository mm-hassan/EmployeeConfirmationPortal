<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Hr_Forward.aspx.cs" Inherits="EmployeeConfirmationPortal.Hr_Forward" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!DOCTYPE html>


    <title>Pending Confirmations</title>
    
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="path/to/toastr.css"/>

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


        </style>
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="content-wrapper">
         

            <div class="form-title">
                <h1><strong>EMPLOYEE CONFIRMATION (HR Review)</strong></h1>
            </div>






    <section class="content">
      
      <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
          <h2 class="card-title">Divisions</h2>
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
                                           <asp:LinkButton ID="lbl_View" type="button" Class="btn btn-dark" runat="server" Width="75px" CommandName="Forward" CommandArgument='<%# Bind("division_cd") %>' OnClick="lbl_View_Click">View</asp:LinkButton>
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
            <h2 class="card-title">Pending Request</h2>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body p-3"> 
            <div class="card">
              <div class="card-body p-0" style="width: 100%; height: 600px; overflow: auto;">
                  <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped text-center"  AutoGenerateColumns="false">
                              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                              <Columns>
                                  <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_Forward" type="button" Class="btn btn-dark" runat="server" Width="75px" Font-Size="Small" CommandName="Forward" CommandArgument='<%# Bind("emp_cd") %>' OnClick="lbl_Forward_Click">Forward</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="emp_cd" HeaderText="Employee Code"/>
                                  <asp:BoundField DataField="emp_name" HeaderText="Employee Name" />
                                  <asp:BoundField DataField="Department_Name" HeaderText="Department" />
                                  <asp:BoundField DataField="Designation_Name" HeaderText="Designation"/>
                                  <asp:BoundField DataField="section_name" HeaderText="Section"/>
                                  <asp:BoundField DataField="cadre_name" HeaderText="Cadre"/>
                                  <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit"/>
                                  <asp:BoundField DataField="appointment_date" HeaderText="Appointment Date"/>
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
   <script>
       function showSuccessToast() {
           console.log('showSuccessToast called');
           toastr.success('Request sucessfully forwarded!', 'Success');
       }

       function showErrorToast() {
           console.log('showErrorToast called');
           toastr.error('Request not sucessfully forwarded!', 'Error');
       }

       function showDatabaseErrorToast() {
           console.log('showErrorToast called');
           toastr.error('Database backend error!', 'Error');
       }
</script>
</asp:Content>
