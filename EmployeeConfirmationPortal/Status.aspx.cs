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
    public partial class Status : System.Web.UI.Page
    {
        Database db = new Database();

        protected void Page_Load(object sender, EventArgs e)
        {
            emp_Details.Visible = false;
            if (!IsPostBack)
            {
                LoadDepartments();
                div_EmployeeDetails.Visible = false;
                emp_Details.Visible = false;
            }
        }

        private void LoadDepartments()
        {
            try
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                DataTable dt = new DataTable();
                dt = null;
                string query = "SELECT a.division_cd, a.division_name FROM HRM_DIVISION_MST a where EXISTS (SELECT b.hrm_division_cd FROM HRM_EMPLOYEE b where a.division_cd=b.hrm_division_cd AND b.confirmation_date IS NULL AND b.emp_status='A' AND b.appointment_date< ADD_MONTHS(SYSDATE, -2.5) AND b.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY'))";

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
                    gv_PendingRequests.DataSource = dt;
                    gv_PendingRequests.DataBind();
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
                string deptcd = (sender as LinkButton).CommandArgument;
                string[] Ids = deptcd.Split('-');
                string Dept_Cd = Ids[0].ToString();
                loadInfo(Dept_Cd);

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
                string query = "SELECT a.emp_cd, a.emp_name, (SELECT F.REQUEST_ID from hrm_emp_confirmation F WHERE a.emp_cd = F.EMP_CD) AS REQUEST_ID, (SELECT F.CONFIRMATION_STATUS from hrm_emp_confirmation F WHERE a.emp_cd = F.EMP_CD) AS CONFIRMATION_STATUS, (SELECT b.department_name FROM HRM_DEPARTMENT_mst b WHERE a.hrm_department_cd = b.department_cd) AS Department_Name, (SELECT c.designation_name FROM HRM_DESIGNATION_MST c WHERE a.hrm_designation_cd = c.designation_cd) AS Designation_Name, a.reg_cd FROM hrm_employee a where a.hrm_division_cd = '" + Div_Cd + "' AND a.confirmation_date IS NULL AND a.emp_status='A' AND a.appointment_date < (SYSDATE - 87) AND a.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND not EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE F.CONFIRMATION_STATUS = 'HR APPROVED' AND F.EMP_CD=a.emp_cd)";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {

                    lbl_GridMsg.Text = "Please click on 'Approve' to approve the request.";
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "shownNoPendingToast", "shownNoPendingToast();", true);
                    // No data found for the selected value, clear the GridView
                    gv_PendingRequests.DataSource = null;
                    gv_PendingRequests.DataBind();
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
                string empcode = (sender as LinkButton).CommandArgument;
                string Query = "SELECT e.appointment_date, e.HRM_CADRE_CD, e.CADRE_SUBCLASS_CD, e.emp_name, hrm_code_desc('CADRE', E.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, (select r.reg_name from regions r where r.reg_cd=e.reg_cd) Region_Name, hrm_code_desc('DESIGNATION', E.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('DIVISION', E.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', E.HRM_UNIT_CD, E.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', E.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', E.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, (SELECT F.CONFIRMED_BY_HOD from hrm_emp_confirmation F WHERE e.emp_cd = F.EMP_CD) AS CONFIRMATION_BY_HOD, (SELECT F.HOD_REMARKS from hrm_emp_confirmation F WHERE e.emp_cd = F.EMP_CD) AS HOD_REMARKS, (SELECT F.Hod_Confirm_Status from hrm_emp_confirmation F WHERE e.emp_cd = F.EMP_CD) AS Hod_Confirm_Status, (SELECT F.REQUEST_ID from hrm_emp_confirmation F WHERE e.emp_cd = F.EMP_CD) AS REQUEST_ID, (SELECT F.REQUEST_DATE from hrm_emp_confirmation F WHERE e.emp_cd = F.EMP_CD) AS REQUEST_DATE, (SELECT F.CONFIRMATION_STATUS from hrm_emp_confirmation F WHERE e.emp_cd = F.EMP_CD) AS CONFIRMATION_STATUS, (SELECT F.Performance_Remarks from hrm_emp_confirmation F WHERE e.emp_cd = F.EMP_CD) AS Performance_Remarks, (SELECT F.Training_Remarks from hrm_emp_confirmation F  WHERE e.emp_cd = F.EMP_CD) AS Training_Remarks, (SELECT F.PROBATION_EXT_PERIOD from hrm_emp_confirmation F  WHERE e.emp_cd = F.EMP_CD) AS PROBATION_EXT_PERIOD, e.reg_cd from hrm_employee E WHERE E.EMP_CD='" + empcode + "'";
                dt = db.GetData(Query);
                emp_Details.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TextBox1.Text = empcode;
                        TextBox2.Text = ToTitleCase(dr["emp_name"].ToString());
                        TextBox3.Text = ToTitleCase(dr["DEPARTMENT_NAME"].ToString());
                        TextBox4.Text = ToTitleCase(dr["DESIGNATION_NAME"].ToString());


                        TextBox13.Text = ToTitleCase(dr["Region_Name"].ToString());
                        TextBox14.Text = ToTitleCase(dr["DIVISION_NAME"].ToString());
                        TextBox15.Text = ToTitleCase(dr["UNIT_NAME"].ToString());
                        TextBox16.Text = ToTitleCase(dr["appointment_date"].ToString());


                        TextBox5.Text = dr["REQUEST_ID"].ToString();
                        TextBox6.Text = dr["REQUEST_DATE"].ToString();



                        string confstatus = dr["CONFIRMATION_STATUS"].ToString();
                        if (confstatus == "C")
                        {
                            TextBox7.Text = "HOD Confirmed";
                            TextBox18.Text = "Hr Approval Pending";
                        }
                        else if (confstatus == "F")
                        {
                            TextBox7.Text = "Hr Forwarded";
                            TextBox18.Text = "HOD Confirmation Pending";
                        }
                         else if (confstatus == "A")
                        {
                            TextBox7.Text = "Hr Approved";
                            TextBox18.Text = "Confirmation Status Completed";
                        }
                        else
                        {
                            TextBox7.Text = "Pending";
                            TextBox18.Text = "Hr Forward Pending";
                        }




                        string confbyhod = dr["CONFIRMATION_BY_HOD"].ToString();
                        if (confbyhod == "R")
                        {
                            TextBox8.Text = "HOD Reviewed";
                        }
                        else
                        {
                            TextBox8.Text = "Pending";
                        }




                        string hodconfstatus = dr["Hod_Confirm_Status"].ToString();
                        if (hodconfstatus== "S")
                        {
                            TextBox9.Text = "Satisfactory";
                        }
                        else if (hodconfstatus == "U")
                        {
                            TextBox9.Text = "Unsatisfactory";
                        }
                        else if (hodconfstatus == "P")
                        {
                            TextBox9.Text = "Probation Extended";
                        }
                        else
                        {
                            TextBox9.Text = "Pending";
                        }



                        string perfRemarks = ToTitleCase(dr["Performance_Remarks"].ToString());
                        if (perfRemarks == "")
                        {
                            TextBox12.Text = "Pending";
                        }
                        else
                        {
                            TextBox12.Text = perfRemarks;
                        }


                        string trainRemarks = ToTitleCase(dr["Training_Remarks"].ToString());
                        if (trainRemarks == "")
                        {
                            TextBox17.Text = "Pending";
                        }
                        else
                        {
                            TextBox17.Text = perfRemarks;
                        }

                        TextBox10.Text = ToTitleCase(dr["HRM_CADRE_CD"].ToString()) + " - " + ToTitleCase(dr["CADRE_SUBCLASS_CD"].ToString());
                        TextBox11.Text = ToTitleCase(dr["SECTION_NAME"].ToString());

                        Hod_Remarks.Text = dr["HOD_REMARKS"].ToString();

                        Probation_period.Text = dr["PROBATION_EXT_PERIOD"].ToString();
                        GetParam();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "shownNoPendingToast", "shownNoPendingToast();", true);
                }
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




        public void GetParam()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "select d.detail_id, d.detail_name, (select A.Score from HRM_EMP_CONFIRM_EVAL A Where d.detail_id=A.DETAIL_ID AND A.Req_Id= '" + TextBox5.Text + "' AND A.EMP_CD= '" + TextBox1.Text + "') AS SCORE from hrm_setup_detl d where d.seq_no=163";
                dt = db.GetData(query);
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    lbl_GridMsg.Text = "Please click on 'view' to process the request.";
                    lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                    GridViewParam.DataSource = dt;
                    GridViewParam.DataBind();
                }
                else
                {
                    lbl_GridMsg.Text = "No any pending request.";
                    lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, GetType(), "shownNoPendingToast", "shownNoPendingToast();", true);
                    GridViewParam.DataSource = dt;
                    GridViewParam.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


    }
}