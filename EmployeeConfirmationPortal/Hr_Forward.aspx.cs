using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;


namespace EmployeeConfirmationPortal
{
    public partial class Hr_Forward : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode;
        string EmployeeUCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();
            if (!IsPostBack)
            {
                LoadDepartments();
                div_EmployeeDetails.Visible = false;
            }
        }


        public void getSession()
        {
            try
            {
                if (Session["EmployeeCode"] != null)
                {
                    EmployeeCode = Session["EmployeeCode"].ToString();
                    DataTable dt = new DataTable();
                    string query = "SELECT * from users where EMP_CD = '" + EmployeeCode + "'";
                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Session["EmployeeUserCode"] = dr["USR_CD"].ToString();
                        EmployeeUCode = Session["EmployeeUserCode"].ToString();
                    }
                }
                else
                {
                    Response.Redirect("LockScreen.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        private void LoadDepartments()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = null;
                string query = "SELECT a.division_cd, a.division_name FROM HRM_DIVISION_MST a WHERE EXISTS ( SELECT 1 FROM HRM_EMPLOYEE b LEFT JOIN hrm_emp_confirmation c ON b.emp_cd = c.emp_cd WHERE a.division_cd = b.hrm_division_cd AND b.confirmation_date IS NULL AND b.emp_status = 'A' AND b.appointment_date < ADD_MONTHS(SYSDATE, -2.5) AND b.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND c.emp_cd IS NULL )";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "Please click on 'view' to process the request.";
                    Label1.ForeColor = System.Drawing.Color.Green;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();



                }
                else
                {
                    Label1.Text = "No any pending request.";
                    Label1.ForeColor = System.Drawing.Color.Red;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


        protected void lbl_View_Click(object sender, EventArgs e)
        {
            try
            {
                string divcd = (sender as LinkButton).CommandArgument;
                string[] Ids = divcd.Split('-');
                string Div_Cd = Ids[0].ToString();
                loadInfo(Div_Cd);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }




        public void loadInfo(string Div_Cd)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT a.appointment_date, (select r.reg_name from regions r where r.reg_cd=a.reg_cd) Region_Name, hrm_code_desc('CADRE', a.HRM_CADRE_CD, NULL, NULL, NULL) cadre_name, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) Designation_Name, hrm_code_desc('DIVISION', a.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', a.HRM_UNIT_CD, a.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) Department_Name, hrm_code_desc('SECTION', a.HRM_SECTION_CD, NULL, NULL, NULL) section_name, a.emp_cd, a.emp_name, a.reg_cd FROM hrm_employee a where a.hrm_division_cd= '" + Div_Cd + "' AND a.confirmation_date IS NULL AND NOT EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd=F.EMP_CD) AND a.appointment_date < ADD_MONTHS(SYSDATE, -2.5) AND a.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND a.emp_status='A'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    lbl_GridMsg.Text = "Please click on 'Forward' to forward the request.";
                    lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                    div_EmployeeDetails.Visible = true;
                    gv_PendingRequests.DataSource = dt;
                    gv_PendingRequests.DataBind();

                }
                else
                {
                    lbl_GridMsg.Text = "No any pending request.";
                    lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('No Pending Data !');", true);
                    div_EmployeeDetails.Visible = false;
                    // No data found for the selected value, clear the GridView
                    gv_PendingRequests.DataSource = null;
                    gv_PendingRequests.DataBind();
                    LoadDepartments();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }





        protected void lbl_Forward_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string empcd = (sender as LinkButton).CommandArgument;
                string[] Ids = empcd.Split('-');
                string REQ_ID = Ids[0].ToString();
                string insertDetailQuery = "INSERT INTO HRM_EMP_CONFIRMATION (REQUEST_ID, REQUEST_DATE, EMP_CD, CONFIRMATION_STATUS, L$USR_IN, L$in_Date) VALUES ((SELECT NVL(MAX(REQUEST_ID), 0) + 1 FROM HRM_EMP_CONFIRMATION), SYSDATE, '" + REQ_ID + "', 'F', '" + EmployeeUCode + "', SYSDATE)";
                string resultDetail = db.PostData(insertDetailQuery);
                if (resultDetail == "Done")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                    div_EmployeeDetails.Visible = false;
                    // No data found for the selected value, clear the GridView
                    gv_PendingRequests.DataSource = null;
                    gv_PendingRequests.DataBind();
                    LoadDepartments();

                    empcd = REQ_ID;


                    GetHod(empcd);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
                    LoadDepartments();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }




        public void GetHod(string empcd)
        {
            try
            {
                string employeename = "";
                string hodemail = "";
                string hodname = "";
                DataTable dt = new DataTable();
                string querry = "SELECT e.line_manager_cd, e.line_manager_NAME, a.e_mail, a.emp_name, (SELECT F.EMP_NAME FROM HRM_EMPLOYEE F WHERE F.EMP_CD= '" + empcd + "') AS EMPLNAME  FROM hrm_employee_info_view e INNER JOIN HRM_EMPLOYEE a ON e.line_manager_cd=a.emp_cd where e.EMP_CD='" + empcd + "'";
                dt = db.GetData(querry);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        employeename = ToTitleCase(dr["EMPLNAME"].ToString());
                        hodemail = dr["e_mail"].ToString().ToLower();
                        hodname = ToTitleCase(dr["emp_name"].ToString());

                    }
                    EmailIntimationHOD(hodemail, hodname, employeename);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }




        public void EmailIntimationHOD(string hodemail, string hodname, string employeename)
        {
            try
            {
                SendResponse res = new SendResponse();


                string sub = string.Empty;
                sub += "Request for Employee Confirmation - " + employeename + "";
                string eBody = string.Empty;
                eBody += "Dear " + hodname + ",<br /><br />";
                eBody += "It is to inform you that a request for the confirmation of " + employeename + " has been initiated by our HR department. " + "<br />"
                    + "Kindly review the details at your earliest convenience." + "<br />"
                    + "Thank you for your cooperation." + "<br />"
                    + "This is an automated message for confirmation of Employee Approval by HOD. " + "<br /><br />"
                    + "<strong>**System Generated Email**</strong>";


                res.SendConfirmEmail(hodemail, eBody, sub);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }


        public static string ToTitleCase(string str)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(str.ToLower());
        }
    }
}