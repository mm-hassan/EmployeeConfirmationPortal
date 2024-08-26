using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeConfirmationPortal
{
    public partial class Master : System.Web.UI.MasterPage
    {
        Database db = new Database();

        string EmployeeCode, EmployeeName, EmployeeDesignation;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();
            linkSplit(); 
            if (!Page.IsPostBack)
            {
                hideDiv();
                lblUserName.Text = EmployeeName.ToString();
                lblUserDesignation.Text = EmployeeDesignation.ToString();
                checkUserRole(EmployeeCode);
                getImage(EmployeeCode);
            }
        }

        public void getSession()
        {
            if (Session["EmployeeCode"] != null)
            {
                EmployeeCode = Session["EmployeeCode"].ToString();
                EmployeeName = Session["EmployeeName"].ToString();
                EmployeeDesignation = Session["EmployeeDesignation"].ToString();
                //Role_ID = Session["RoleID"].ToString();
                //RoleName = Session["RoleName"].ToString();
            }
            else
            {
                Response.Redirect("LockScreen.aspx");
            }
        }

        public void linkSplit()
        {
            string[] Ids = HttpContext.Current.Request.Url.AbsoluteUri.Split('/');

            // Check if the array has at least three elements
            if (Ids.Length >= 3)
            {
                if (Ids[2].Contains("localhost"))
                {
                    // Check if the array has at least four elements before accessing Ids[3]
                    if (Ids.Length >= 4)
                    {
                        lbl_Tabname.InnerText = Ids[3].ToString().Replace(".aspx", "");
                    }
                    else
                    {
                        // Handle the case where there are not enough elements in the array
                        lbl_Tabname.InnerText = ""; // Or whatever default value you prefer
                    }
                }
                else
                {
                    // Check if the array has at least five elements before accessing Ids[4]
                    if (Ids.Length >= 5)
                    {
                        lbl_Tabname.InnerText = Ids[4].ToString().Replace(".aspx", "");
                    }
                    else
                    {
                        // Handle the case where there are not enough elements in the array
                        lbl_Tabname.InnerText = ""; // Or whatever default value you prefer
                    }
                }
            }
            else
            {
                // Handle the case where there are not enough elements in the array
                lbl_Tabname.InnerText = ""; // Or whatever default value you prefer
            }
        }


        public void getImage(string val)
        {
            try
            {
                byte[] bytes;
                string fileName;
                string orcconstring = db.getConnectionOracleCustom();
                OracleConnection orccon = new OracleConnection(orcconstring);
                orccon.Open();
                string msql = "select i.image from hrm_employee e, hrm_employee_image i where e.emp_cd = '" + val + "' and e.EMP_CD = i.emp_cd";
                OracleCommand cmd = new OracleCommand(msql, orccon);
                OracleDataReader odr = cmd.ExecuteReader();
                odr.Read();
                bytes = (byte[])odr["image"];

                //fileName = odr["emp_cd"].ToString();
                Image1.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(bytes);
                orccon.Close();

            }
            catch (Exception ex)
            {

            }
        }

        public void hideDiv()
        {
            //Div_Requester.Visible = false;
            Div_HOD.Visible = false;
            Div_HRTeam.Visible = false;
            Div_HRHead.Visible = false;
            Div_Admin.Visible = false;
            Div_IR.Visible = false;
            Div_BT.Visible = false;
            Div_Accounts.Visible = false;
            Div_Security.Visible = false;
            Div_Store.Visible = false;
            Div_Tms.Visible = false;
            Div_HR.Visible = false;
            Div_IROperations.Visible = false;
            Div_PayrollOperation.Visible = false;
            Div_Audit.Visible = false;
            //Div_Report.Visible = false;
            Div_PendingIntimation.Visible = false;
            Div_Finance.Visible = false;
        }

        public void checkUserRole(string EmployeeCode)
        {
            DataTable dt = new DataTable();
            string query = "SELECT T.ROLL FROM HRM_LIVE.HRM_EMP_CONFIRM_RIGHTS_VIEW T WHERE T.EMP_CD = '" + EmployeeCode + "'";
            dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Role = dr["ROLL"].ToString();

                    if (Role == "SUPER")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = true;
                        DH.Visible = true;
                        Status.Visible = true;
                        break;
                        //Div_Requester.Visible = true;
                        //Div_HOD.Visible = true;
                        //Div_HRTeam.Visible = true;
                        //Div_HRHead.Visible = true;
                        //Div_Admin.Visible = true;
                        //Div_IR.Visible = true;
                        //Div_BT.Visible = true;
                        //Div_Accounts.Visible = true;
                        //Div_Security.Visible = true;
                        //Div_Store.Visible = true;
                        //Div_Tms.Visible = true;
                        //Div_HR.Visible = true;
                        //Div_IROperations.Visible = true;
                        //Div_PayrollOperation.Visible = true;
                        //Div_Audit.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                        //Div_Finance.Visible = true;
                    }
                    if (Role == "IR")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                        //Div_Requester.Visible = false;
                        // Div_HOD.Visible = true;
                        // Div_HRTeam.Visible = true;
                        // Div_HRHead.Visible = true;
                        // Div_Admin.Visible = true;
                        // Div_IR.Visible = true;
                        // Div_BT.Visible = true;
                        // Div_Accounts.Visible = true;
                        // Div_Security.Visible = true;
                        // Div_Store.Visible = true;
                        // Div_Tms.Visible = true;
                        // Div_HR.Visible = true;
                        // Div_IROperations.Visible = true;
                        // Div_PayrollOperation.Visible = true;
                        // Div_Audit.Visible = true;
                        //// Div_Report.Visible = true;
                        // Div_PendingIntimation.Visible = true;
                        // Div_Finance.Visible = true;
                    }
                    else if (Role == "IR_OPE")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                        //Div_IR.Visible = true;
                        //Div_IROperations.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else if (Role == "EMPLOYEE")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                        //Div_Requester.Visible = true;
                        //Div_Report.Visible = true;
                    }
                    else if (Role == "LINE_MANAGER")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = true;
                        Status.Visible = true;
                        break;
                        //Div_HOD.Visible = true;
                        ////Div_Requester.Visible = true;
                        ////Div_Report.Visible = true;
                    }
                    else if (Role == "HOD")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = true;
                        Status.Visible = true;
                        break;
                        //Div_HOD.Visible = true;
                        ////Div_Requester.Visible = true;
                        ////Div_Report.Visible = true;
                    }
                    else if (Role == "HR_TA")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = true;
                        DH.Visible = false;
                        Status.Visible = true;
                        ////Div_Requester.Visible = true;
                        //Div_HRTeam.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else if (Role == "HR_HEAD")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = true;
                        DH.Visible = true;
                        Status.Visible = true;
                        ////Div_Requester.Visible = true;
                        //Div_HRTeam.Visible = true;
                        //Div_HRHead.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else if (Role == "BT")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                        //Div_BT.Visible = true;
                        //Div_Store.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else if (Role == "FIN")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                        //Div_Accounts.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else if (Role == "ADMIN")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                        //Div_Admin.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else if (Role == "HR")
                    {
                        HR_Request.Visible = true;
                        HR_Approve.Visible = true;
                        DH.Visible = false;
                        Status.Visible = true;
                        //Div_HR.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else if (Role == "ACCOUNTS")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                    }
                    else if (Role == "STORE")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                    }
                    else if (Role == "TMS")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                        //Div_Tms.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else if (Role == "PAY_OPE")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                        //Div_PayrollOperation.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else if (Role == "AUDIT")
                    {
                        HR_Request.Visible = false;
                        HR_Approve.Visible = false;
                        DH.Visible = false;
                        Status.Visible = false;
                        //Div_Audit.Visible = true;
                        ////Div_Report.Visible = true;
                        //Div_PendingIntimation.Visible = true;
                    }
                    else
                    {
                        Response.Write("<script>alert('You can't access this module please coordinate to Mubbashir ext# : 2224'); window.location.replace('LockScreen.aspx');</script>");
                    }
                }
            }
        }





        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("LockScreen.aspx");
        }
    }
}