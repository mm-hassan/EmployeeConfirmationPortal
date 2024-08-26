using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeConfirmationPortal
{
    public partial class Report : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode;
        string EmployeeUCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();

            //// Retrieve the feature flag state from application settings
            //bool featureEnabled = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["FeatureFlag"]);

            //// Check if the feature is enabled
            //if (featureEnabled)
            //{
            //    LoadDepartments();
            //}
            //else
            //{
            //    loadInfo("99");
            //}
            if (!IsPostBack)
            {
                LoadDepartments();
                div_EmployeeDetails.Visible = false;
                Section2.Visible = false;
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
                //string query = "SELECT a.division_cd, a.division_name FROM HRM_DIVISION_MST a WHERE EXISTS ( SELECT 1 FROM HRM_EMPLOYEE b LEFT JOIN hrm_emp_confirmation c ON b.emp_cd = c.emp_cd WHERE a.division_cd = b.hrm_division_cd AND b.confirmation_date IS NULL AND b.emp_status = 'A' AND b.appointment_date < ADD_MONTHS(SYSDATE, -2.5) AND b.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND c.emp_cd IS NULL )";
                string query = "SELECT a.division_cd, a.division_name, (SELECT COUNT(*) FROM HRM_EMPLOYEE b LEFT JOIN hrm_emp_confirmation c ON b.emp_cd = c.emp_cd WHERE a.division_cd = b.hrm_division_cd AND b.confirmation_date IS NULL AND b.emp_status = 'A' AND b.appointment_date < ADD_MONTHS(SYSDATE, -2.5) AND b.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND c.emp_cd IS NULL) AS num_pending_records, (SELECT COUNT(*) FROM HRM_EMPLOYEE b LEFT JOIN hrm_emp_confirmation c ON b.emp_cd = c.emp_cd WHERE a.division_cd = b.hrm_division_cd AND (c.confirmation_status = 'C' OR c.confirmation_status = 'A')) AS num_approved_records, (SELECT COUNT(*) FROM HRM_EMPLOYEE b LEFT JOIN hrm_emp_confirmation c ON b.emp_cd = c.emp_cd WHERE a.division_cd = b.hrm_division_cd AND c.confirmation_status != 'A') AS num_pending_hr_approval, (SELECT COUNT(*) FROM HRM_EMPLOYEE b LEFT JOIN hrm_emp_confirmation c ON b.emp_cd = c.emp_cd WHERE a.division_cd = b.hrm_division_cd AND c.confirmation_status = 'A') AS num_approved_hr_approval FROM HRM_DIVISION_MST a WHERE EXISTS (SELECT 1 FROM HRM_EMPLOYEE b LEFT JOIN hrm_emp_confirmation c ON b.emp_cd = c.emp_cd WHERE a.division_cd = b.hrm_division_cd AND b.confirmation_date IS NULL AND b.emp_status = 'A' AND b.appointment_date < ADD_MONTHS(SYSDATE, -2.5) AND b.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND c.emp_cd IS NULL)";
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "shownNoPendingToast", "shownNoPendingToast();", true);
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




        public void loadInfo(string divcd)
        {
            try
            {
                divcode.Text = divcd;
                DataTable dt = new DataTable();
                //DataTable dtt = new DataTable();
                // query = "SELECT a.appointment_date, (select r.reg_name from regions r where r.reg_cd=a.reg_cd) Region_Name, hrm_code_desc('CADRE', a.HRM_CADRE_CD, NULL, NULL, NULL) cadre_name, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) Designation_Name, hrm_code_desc('DIVISION', a.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', a.HRM_UNIT_CD, a.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) Department_Name, hrm_code_desc('SECTION', a.HRM_SECTION_CD, NULL, NULL, NULL) section_name, a.emp_cd, a.emp_name, a.reg_cd FROM hrm_employee a where a.hrm_division_cd= '" + Div_Cd + "' AND a.confirmation_date IS NULL AND NOT EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd=F.EMP_CD) AND a.appointment_date < ADD_MONTHS(SYSDATE, -2.5) AND a.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND a.emp_status='A'";


                string query = "SELECT distinct (hrm_code_desc('EMP_NAME', B.EMP_HOD, NULL, NULL, NULL)) HOD_NAME, B.Emp_Hod, (SELECT Count(Distinct(a.emp_cd)) FROM hrm_employee a, Hrm_Employee_Info_View C where a.confirmation_date IS NULL AND NOT EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd = F.EMP_CD) AND a.appointment_date < (SYSDATE - 87) AND a.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND a.emp_status = 'A' AND A.Hrm_Division_Cd = '" + divcd + "' AND A.EMP_CD = C.Emp_Cd AND C.EMP_HOD_CD = B.Emp_Hod) AS num_pending_records, (SELECT Count(Distinct(a.emp_cd)) FROM hrm_employee           a, Hrm_Employee_Info_View C, HRM_EMP_CONFIRMATION   D where EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd = F.EMP_CD) AND (D.CONFIRMATION_STATUS = 'C' or D.CONFIRMATION_STATUS='A') AND D.Confirmed_By_Hod = 'R'  AND A.Hrm_Division_Cd = '" + divcd + "' AND A.EMP_CD = C.Emp_Cd AND C.EMP_HOD_CD = B.Emp_Hod) AS num_approved_records, (SELECT Count(Distinct(a.emp_cd)) FROM hrm_employee           a, Hrm_Employee_Info_View C, HRM_EMP_CONFIRMATION   D WHERE EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd = F.EMP_CD) AND D.CONFIRMATION_STATUS = 'C' AND D.Confirmed_By_Hod = 'R' AND D.CONFIRMATION_STATUS != 'A' AND A.Hrm_Division_Cd = '" + divcd + "' AND A.EMP_CD = C.Emp_Cd AND C.EMP_HOD_CD = B.Emp_Hod) AS num_pending_hr_approval, (SELECT Count(Distinct(a.emp_cd)) FROM hrm_employee           a, Hrm_Employee_Info_View C, HRM_EMP_CONFIRMATION   D where EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd = F.EMP_CD) AND D.CONFIRMATION_STATUS = 'A' AND D.Confirmed_By_Hod = 'R' AND A.Hrm_Division_Cd = '" + divcd + "' AND A.EMP_CD = C.Emp_Cd AND C.EMP_HOD_CD = B.Emp_Hod) AS num_approved_hr_approval FROM HRM_DEPARTMENT_BUDGET B WHERE B.HRM_DIVISION_CD = '" + divcd + "' ";

               // string query = "SELECT DISTINCT V.EMP_HOD_CD, V.HOD_NAME, V.DIVISION_NAME, U.Usr_Cd, (SELECT COUNT(c.emp_name) FROM HRM_EMP_CONFIRMATION A INNER JOIN HRM_EMPLOYEE c ON A.emp_cd = c.emp_cd WHERE A.L$usr_In = U.Usr_Cd AND A.CONFIRMATION_STATUS != 'A') AS Approved_Request, (SELECT COUNT(a.emp_name) FROM hrm_employee a INNER JOIN hrm_employee_info_view e ON a.EMP_CD=e.emp_cd WHERE  a.confirmation_date IS NULL AND NOT EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd=F.EMP_CD) AND a.appointment_date < ADD_MONTHS(SYSDATE, -2.5) AND a.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND a.emp_status='A' AND e.EMP_HOD_CD = V.EMP_HOD_CD) AS Pending_Request FROM hrm_employee_info_view V INNER JOIN USERS U ON TO_CHAR(V.EMP_HOD_CD) = TO_CHAR(U.Emp_Cd) WHERE INSTR(v.DIVISION_NAME, '-') > 0 AND SUBSTR(v.DIVISION_NAME, 1, INSTR(v.DIVISION_NAME, '-') - 1) = '" + divcd + "' ";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    lbl_GridMsg.Text = "Please click on 'Show' to show the request.";
                    lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                    div_EmployeeDetails.Visible = true;
                    gv_PendingRequests.DataSource = dt;
                    gv_PendingRequests.DataBind();

                }
                else
                {
                    lbl_GridMsg.Text = "No any pending request.";
                    lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, GetType(), "shownNoPendingToast", "shownNoPendingToast();", true);
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

        protected void lbl_Show_Click(object sender, EventArgs e)
        {
            try
            {
                string divcd = (sender as LinkButton).CommandArgument;
                string[] Ids = divcd.Split('-');
                string hodcd = Ids[0].ToString();
        
                // Now you can use divisionCd and hodCd as needed
                loadreq(hodcd);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        public void loadreq(string hodcd)
        {
            try
            {
                DataTable dt = new DataTable();
                //string query = "SELECT DISTINCT V.EMP_HOD_CD, V.HOD_NAME, V.DIVISION_NAME, U.Usr_Cd, (SELECT COUNT(c.emp_name) FROM HRM_EMP_CONFIRMATION A INNER JOIN HRM_EMPLOYEE c ON A.emp_cd = c.emp_cd WHERE A.L$usr_In = U.Usr_Cd AND A.CONFIRMATION_STATUS != 'A') AS Approved_Request, (SELECT COUNT(a.emp_name) FROM hrm_employee a INNER JOIN hrm_employee_info_view e ON a.EMP_CD=e.emp_cd WHERE  a.confirmation_date IS NULL AND NOT EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd=F.EMP_CD) AND a.appointment_date < ADD_MONTHS(SYSDATE, -2.5) AND a.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND a.emp_status='A' AND e.EMP_HOD_CD = V.EMP_HOD_CD) AS Pending_Request FROM hrm_employee_info_view V INNER JOIN USERS U ON TO_CHAR(V.EMP_HOD_CD) = TO_CHAR(U.Emp_Cd) WHERE INSTR(v.DIVISION_NAME, '-') > 0 AND SUBSTR(v.DIVISION_NAME, 1, INSTR(v.DIVISION_NAME, '-') - 1) = '"+divcd+"' ";
                //string query = "SELECT Distinct a.emp_cd, a.emp_name, trunc(a.appointment_date) AS appointmentdate, (SELECT r.reg_name FROM regions r WHERE r.reg_cd = a.reg_cd) AS Region_Name, hrm_code_desc('CADRE', a.HRM_CADRE_CD, NULL, NULL, NULL) AS CADRE_NAME, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, hrm_code_desc('DIVISION', a.HRM_DIVISION_CD, NULL, NULL, NULL) AS DIVISION_NAME, hrm_code_desc('UNIT', a.HRM_UNIT_CD, a.HRM_DIVISION_CD, NULL, NULL) AS UNIT_NAME, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, hrm_code_desc('SECTION', a.HRM_SECTION_CD, NULL, NULL, NULL) AS SECTION_NAME, ( hrm_code_desc('EMP_NAME', B.EMP_HOD, NULL, NULL, NULL)) HOD_NAME, B.Emp_Hod FROM hrm_employee a, Hrm_Employee_Info_View C , HRM_DEPARTMENT_BUDGET B where  a.confirmation_date IS NULL AND a.appointment_date < ADD_MONTHS(SYSDATE, -2.5) AND a.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND a.emp_status='A' AND A.Hrm_Division_Cd='" + divcode.Text + "' AND B.EMP_HOD= '" + hodcd + "' AND A.EMP_CD=C.Emp_Cd  AND C.EMP_HOD_CD=B.Emp_Hod";
                string query = "SELECT Distinct a.emp_cd, a.emp_name, a.confirmation_date, (SELECT D.CONFIRMATION_STATUS FROM Hrm_Emp_Confirmation D WHERE a.emp_cd=D.Emp_Cd) AS CONFIRMATION_STATUS, trunc(a.appointment_date) AS appointmentdate, (SELECT r.reg_name FROM regions r WHERE r.reg_cd = a.reg_cd) AS Region_Name, hrm_code_desc('CADRE', a.HRM_CADRE_CD, NULL, NULL, NULL) AS CADRE_NAME, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS DESIGNATION_NAME, hrm_code_desc('DIVISION', a.HRM_DIVISION_CD, NULL, NULL, NULL) AS DIVISION_NAME, hrm_code_desc('UNIT', a.HRM_UNIT_CD, a.HRM_DIVISION_CD, NULL, NULL) AS UNIT_NAME, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, hrm_code_desc('SECTION', a.HRM_SECTION_CD, NULL, NULL, NULL) AS SECTION_NAME, (hrm_code_desc('EMP_NAME', B.EMP_HOD, NULL, NULL, NULL)) HOD_NAME, B.Emp_Hod FROM hrm_employee a, Hrm_Employee_Info_View C, HRM_DEPARTMENT_BUDGET  B where (a.confirmation_date IS NULL OR (a.confirmation_date >= TRUNC(SYSDATE) - INTERVAL '6' MONTH AND a.confirmation_date <= TRUNC(SYSDATE) AND EXISTS (SELECT Z.EMP_CD FROM HRM_EMP_CONFIRMATION Z WHERE Z.Emp_Cd = A.Emp_Cd))) AND a.appointment_date < (SYSDATE - 87) AND a.appointment_date >= TO_DATE('02/01/2024', 'MM/DD/YYYY')  AND A.Hrm_Division_Cd = '" + divcode.Text + "' AND B.EMP_HOD = '" + hodcd + "' AND A.EMP_CD = C.Emp_Cd AND C.EMP_HOD_CD = B.Emp_Hod";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    Label2.Text = "Please click on 'Forward' to forward the request.";
                    Label2.ForeColor = System.Drawing.Color.Green;
                    Section2.Visible = true;
                    GridView3.DataSource = dt;
                    GridView3.DataBind();

                }
                else
                {
                    Label2.Text = "No any pending request.";
                    Label2.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, GetType(), "shownNoPendingToast", "shownNoPendingToast();", true);
                    Section2.Visible = false;
                    // No data found for the selected value, clear the GridView
                    GridView3.DataSource = null;
                    GridView3.DataBind();
                    LoadDepartments();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                if (dr != null)
                {
                    Label label = e.Row.FindControl("lbl") as Label;
                    if (label != null)
                    {
                        string date = dr["confirmation_date"].ToString();
                        if (date != "")
                        {
                            label.Text = "Process Completed";
                        }
                        else
                        {
                            string status = dr["CONFIRMATION_STATUS"].ToString();
                            if (status == "C")
                            {
                                label.Text = "HOD Confirmed";
                                
                            }
                            else if (status == "F")
                            {
                                label.Text = "Hr Forwarded";
                                
                            }
                            else if (status == "A")
                            {
                                label.Text = "Process Completed";
                                
                            }
                            else
                            {
                                label.Text = "HOD Confirmation Pending";
                            }
                        }
                       
                    }
                }
            }
        }

    }
}