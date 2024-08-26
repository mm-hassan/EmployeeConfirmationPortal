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
    public partial class HOD_Review : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode;
        string EmployeeUCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();
            if (!IsPostBack) // Check if it's not a postback to avoid re-binding on each postback
            {
                Section1.Visible = false;
                loadPendingRequests();

                // Check if a success message is stored in session
        if (Session["SuccessMessage"] != null)
        {
            string successMessage = Session["SuccessMessage"].ToString();
            // Display the success message using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
            // Remove the session variable to prevent showing the message again on subsequent visits
            Session.Remove("SuccessMessage");
        }
            }
        }

        string getroll()
        {
            string roll = "";
            string finalroll = "";
            try
            {
                DataTable dt = new DataTable();
                DataTable dtt = new DataTable();
                dt = null;

                string query = "SELECT T.ROLL FROM HRM_LIVE.HRM_EMP_CONFIRM_RIGHTS_VIEW T WHERE T.EMP_CD= '" + EmployeeCode + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drr in dt.Rows)
                    {
                        roll = drr["ROLL"].ToString();
                        if (roll == "SUPER")
                        {
                            finalroll = roll;
                            break;
                        }
                        else if (roll == "HOD")
                        {
                            finalroll = roll;
                            break;
                        }

                        else
                        {
                            DataRow firstRow = dt.Rows[0]; // Access the first row directly
                            finalroll = firstRow["ROLL"].ToString();
                        }


                    }
                }
                else
                {
                    string queryy = "SELECT * FROM Hrm_Setup_Detl A WHERE A.Seq_No=121 AND A.Detail_Name='" + EmployeeCode + "' ";
                    dtt = db.GetData(queryy);
                    foreach (DataRow dr in dtt.Rows)
                    {
                        if (dr["Detail_Name"].ToString() != "")
                        {
                            finalroll = "SUPER";

                        }
                    }

                }


            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
            return finalroll;
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
                        Session["EmpCode"] = dr["EMP_CD"].ToString();


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

        public void loadPendingRequests()
        {
            try
            {
                Per_Remarks.Text = "";
                Train_Remarks.Text = "";
                radSatisfactory.Checked = false;
                radNonSatisfactory.Checked = false;
                radProbExtended.Checked = false;
                string roll= getroll();
                if (roll == "SUPER")
                {
                     DataTable dt = new DataTable();
                    dt = null;
                    gv_PendingRequests.DataSource = null;
                    //string query = "select a.*, (SELECT b.emp_name FROM HRM_EMPLOYEE b where a.emp_cd=b.emp_cd ) AS EMP_NAME from HRM_EMP_CONFIRMATION a INNER JOIN hrm_employee_info_view e ON e.EMP_CD=a.emp_cd WHERE a.CONFIRMED_BY_HOD is NULL ORDER BY a.request_id";
                    string query = "SELECT a.appointment_date, (select r.reg_name from regions r where r.reg_cd=a.reg_cd) Region_Name, hrm_code_desc('CADRE', a.HRM_CADRE_CD, NULL, NULL, NULL) cadre_name, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) Designation_Name, hrm_code_desc('DIVISION', a.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', a.HRM_UNIT_CD, a.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) Department_Name, hrm_code_desc('SECTION', a.HRM_SECTION_CD, NULL, NULL, NULL) section_name, a.emp_cd, a.emp_name, a.reg_cd FROM hrm_employee a INNER JOIN hrm_employee_info_view e ON a.EMP_CD=e.emp_cd where  a.confirmation_date IS NULL AND NOT EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd=F.EMP_CD) AND a.appointment_date < (SYSDATE - 87) AND a.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND a.emp_status='A' ";
                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        lbl_GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                        gv_PendingRequests.DataSource = dt;
                        gv_PendingRequests.DataBind();



                    }
                    else
                    {

                        lbl_GridMsg.Text = "No any pending request.";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_PendingRequests.DataSource = dt;
                        gv_PendingRequests.DataBind();
                    }
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = null;
                    gv_PendingRequests.DataSource = null;
                    //string query = "select a.*, (SELECT b.emp_name FROM HRM_EMPLOYEE b where a.emp_cd=b.emp_cd ) AS EMP_NAME from HRM_EMP_CONFIRMATION a INNER JOIN hrm_employee_info_view e ON e.EMP_CD=a.emp_cd WHERE a.CONFIRMED_BY_HOD is NULL and e.line_manager_cd='" + EmployeeCode + "'  ORDER BY a.request_id";
                    string query = "SELECT a.appointment_date, (select r.reg_name from regions r where r.reg_cd=a.reg_cd) Region_Name, hrm_code_desc('CADRE', a.HRM_CADRE_CD, NULL, NULL, NULL) cadre_name, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) Designation_Name, hrm_code_desc('DIVISION', a.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', a.HRM_UNIT_CD, a.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) Department_Name, hrm_code_desc('SECTION', a.HRM_SECTION_CD, NULL, NULL, NULL) section_name, a.emp_cd, a.emp_name, a.reg_cd FROM hrm_employee a INNER JOIN hrm_employee_info_view e ON a.EMP_CD=e.emp_cd where a.confirmation_date IS NULL AND NOT EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd=F.EMP_CD) AND a.appointment_date < (SYSDATE - 87) AND a.appointment_date >= TO_DATE('09/01/2023', 'MM/DD/YYYY') AND a.emp_status='A' and (e.EMP_HOD_CD='" + EmployeeCode + "' OR e.line_manager_cd='" + EmployeeCode + "')";

                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        lbl_GridMsg.Text = "Please click on 'view' to process the request.";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                        gv_PendingRequests.DataSource = dt;
                        gv_PendingRequests.DataBind();



                    }
                    else
                    {

                        lbl_GridMsg.Text = "No any pending request.";
                        lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                        gv_PendingRequests.DataSource = dt;
                        gv_PendingRequests.DataBind();
                    }
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
                string reqid = (sender as LinkButton).CommandArgument;
                string[] Ids = reqid.Split('-');
                string Empcode = Ids[0].ToString();
                loadInfo(Empcode);
                GetParam();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }




        public void loadInfo(string Empcode)
        {
            try
            {

                DataTable dt = new DataTable();
                //string query = "SELECT trunc(c.appointment_date) AS appointmentdate, c.emp_name, c.reg_cd, (select r.reg_name from regions r where r.reg_cd=c.reg_cd) Region_Name, hrm_code_desc('CADRE', c.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, hrm_code_desc('DESIGNATION', c.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('DIVISION', c.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', c.HRM_UNIT_CD, c.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', c.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', c.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME,  a.* FROM HRM_EMP_CONFIRMATION A INNER JOIN HRM_EMPLOYEE c ON A.emp_cd=c.emp_cd  WHERE c.Emp_Cd=  '" + Empcode + "' AND A.CONFIRMED_BY_HOD is NULL";
                string query = "SELECT a.CADRE_SUBCLASS_CD,(select r.reg_name from regions r where r.reg_cd=a.reg_cd) Region_Name,  a.emp_cd, a.emp_name, a.reg_cd, trunc(a.appointment_date) AS appointmentdate, hrm_code_desc('CADRE', a.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('DIVISION', a.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', a.HRM_UNIT_CD, a.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', a.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME, a.* FROM hrm_employee a where a.EMP_CD='" + Empcode + "' AND a.confirmation_date IS NULL";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    Section1.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        //TextBox1.Text = dr["REQUEST_ID"].ToString();
                        TextBox2.Text = dr["EMP_CD"].ToString();
                        TextBox3.Text = ToTitleCase(dr["emp_name"].ToString());
                        TextBox6.Text = ToTitleCase(dr["DEPARTMENT_NAME"].ToString());
                        TextBox7.Text = ToTitleCase(dr["DESIGNATION_NAME"].ToString());
                        TextBox8.Text = ToTitleCase(dr["HRM_CADRE_CD"].ToString()) + " - " + ToTitleCase(dr["CADRE_SUBCLASS_CD"].ToString());
                        TextBox9.Text = ToTitleCase(dr["SECTION_NAME"].ToString());
                        //TextBox10.Text = dr["REQUEST_DATE"].ToString();


                     



                        TextBox12.Text = ToTitleCase(dr["Region_Name"].ToString());
                        TextBox13.Text = ToTitleCase(dr["DIVISION_NAME"].ToString());
                        TextBox14.Text = ToTitleCase(dr["UNIT_NAME"].ToString());
                        TextBox15.Text = dr["appointmentdate"].ToString();
                        TextBox16.Text = dr["reg_cd"].ToString();





                        TextBox4.Text = dr["L$USR_IN"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


       

        protected void btnSubmitPerformanceStatus_Click(object sender, EventArgs e)
        {
            try
            {

                string confirmationStatus = GetConfirmationStatus();
                if (string.IsNullOrEmpty(confirmationStatus))
                    return;

                if (!ValidateRemarks())
                    return;

                //if (InsertConfirmationDetail(confirmationStatus) && UpdateConfirmationDetail(confirmationStatus))
                if (InsertConfirmationDetail(confirmationStatus))
                {
                    InsertEvaluationDetails();
                    GetHr(confirmationStatus);
                    Section1.Visible = false;
                    gv_PendingRequests.DataBind();
                    // Redirect to another page to prevent form resubmission
                    Session["SuccessMessage"] = "Your success message here";
                    Response.Redirect("HOD_Review.aspx");
                    Session["SuccessMessage"] = "Your success message here";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }


        private string GetConfirmationStatus()
        {
            if (radSatisfactory.Checked)
                return "S";
            else if (radNonSatisfactory.Checked)
                return "U";
            else if (radProbExtended.Checked)
                return "P";
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showWarningToast();", true);
                return "";
            }
        }

        private bool ValidateRemarks()
        {
            if (string.IsNullOrEmpty(Per_Remarks.Text) && string.IsNullOrEmpty(Train_Remarks.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", " showRemarksWarningToast();", true);
                return false;
            }
            return true;
        }

        private bool InsertConfirmationDetail(string confirmationStatus)
        {
            string probationExtension = string.Empty;

            // Check if the probation extension checkbox is checked
            if (radProbExtended.Checked)
            {
                // Get the selected probation extension period from the dropdown
                probationExtension = ddlProbationExtension.SelectedValue;
            }

            // Construct the SQL query with the probation extension period
            string insertDetailQuery = "INSERT INTO HRM_EMP_CONFIRMATION (REQUEST_ID, REQUEST_DATE, EMP_CD, CONFIRMATION_STATUS, L$USR_IN, L$in_Date, HOD_CONFIRM_STATUS, CONFIRMATION_DATE, CONFIRMED_BY_HOD, PERFORMANCE_REMARKS, TRAINING_REMARKS, HOD_REMARKS, PROBATION_EXT_PERIOD) VALUES ((SELECT NVL(MAX(REQUEST_ID), 0) + 1 FROM HRM_EMP_CONFIRMATION), SYSDATE, '" + TextBox2.Text + "', 'C', '" + EmployeeUCode + "', SYSDATE, '" + confirmationStatus + "', SYSDATE, 'R', '" + Per_Remarks.Text + "', '" + Train_Remarks.Text + "', '" + TextBox1.Text + "', '" + probationExtension + "')";
    

            //string insertDetailQuery = "INSERT INTO HRM_EMP_CONFIRMATION (REQUEST_ID, REQUEST_DATE, EMP_CD, CONFIRMATION_STATUS, L$USR_IN, L$in_Date) VALUES ((SELECT NVL(MAX(REQUEST_ID), 0) + 1 FROM HRM_EMP_CONFIRMATION), SYSDATE, '" + TextBox2.Text + "', 'F', '" + EmployeeUCode + "', SYSDATE)";
            //string insertDetailQuery = "INSERT INTO HRM_EMP_CONFIRMATION (REQUEST_ID, REQUEST_DATE, EMP_CD, CONFIRMATION_STATUS, L$USR_IN, L$in_Date, HOD_CONFIRM_STATUS, CONFIRMATION_DATE, CONFIRMED_BY_HOD, PERFORMANCE_REMARKS, TRAINING_REMARKS, HOD_REMARKS) VALUES ((SELECT NVL(MAX(REQUEST_ID), 0) + 1 FROM HRM_EMP_CONFIRMATION), SYSDATE, '" + TextBox2.Text + "', 'C', '" + EmployeeUCode + "', SYSDATE, '" + confirmationStatus + "', SYSDATE, 'R', '" + Per_Remarks.Text + "', '" + Train_Remarks.Text + "', '" + TextBox1.Text + "')";
            string resultDetail = db.PostData(insertDetailQuery);
            if (resultDetail != "Done")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", " showWarningToast();", true);
                return false;
            }
            return true;
        }



        private void HandleError(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
        }

        public void GetHr(string confirmationStatus)
        {
            try
            {

                string hrid = "";
                string hrname = "";
                string hr = TextBox4.Text;
                DataTable dt = new DataTable();
                string query = "SELECT B.EMP_NAME, B.e_Mail, A.DETAIL_NAME FROM Hrm_Setup_Detl A JOIN HRM_EMPLOYEE B ON TO_CHAR(B.EMP_CD) = A.DETAIL_NAME WHERE A.Seq_No = 121 AND B.EMP_STATUS = 'A'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        hrid = dr["e_Mail"].ToString().ToLower();
                        hrname = ToTitleCase(dr["Emp_Name"].ToString());
                        //hrid = "muhammad.mubbashir@alkaram.com";
                        EmailIntimationHOD(hrid, hrname, confirmationStatus);
                    }
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }

        }

        private void InsertEvaluationDetails()
        {
            foreach (GridViewRow row in GridViewParam.Rows)
            {
                string detailId = row.Cells[0].Text;
                TextBox textBox = row.Cells[1].FindControl("Param1") as TextBox;
                string score = textBox.Text;
                InsertEval(detailId, score);
            }
        }




        public void EmailIntimationHOD(string hrid, string hrname, string confirmationStatus)
        {
            try
            {
                
                string name = "";
                string empcd = "";
                string reqid = "";
                string hodstatus = "";

                if (confirmationStatus == "S")
                {
                    hodstatus = "Satisfactory";
                }
                else if (confirmationStatus == "U")
                {
                    hodstatus = "Unsatisfactory";
                }
                else if (confirmationStatus == "P")
                {
                    hodstatus = "Probation Extended";
                }
                else
                {
                    hodstatus = "Pending";
                }

                DataTable dt = new DataTable();

                string querry = "SELECT A.EMP_NAME from HRM_EMPLOYEE A where A.EMP_CD = '" + EmployeeCode + "'";
                dt = db.GetData(querry);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TextBox5.Text = ToTitleCase(dr["EMP_NAME"].ToString());
                    }
                }

                DataTable dtt = new DataTable();
                string query = "SELECT A.*, (SELECT B.Emp_Name FROM Hrm_Employee B where a.emp_cd=b.emp_cd) AS Name from HRM_EMP_CONFIRMATION A where A.emp_cd= '" + TextBox2.Text + "'";
                dtt = db.GetData(query);
                if (dtt.Rows.Count > 0)
                {
                    foreach (DataRow drr in dtt.Rows)
                    {
                        reqid = drr["REQUEST_ID"].ToString();
                        name = ToTitleCase(drr["Name"].ToString());
                        empcd = drr["EMP_CD"].ToString();
                        
                    }
                }



                SendResponse res = new SendResponse();

                string HODName = TextBox5.Text;
                string sub = string.Empty;
                sub += "Confirmation of Employee Approval by HOD";
                string eBody = string.Empty;
                eBody += "Dear " + hrname + ",<br /><br />";
                eBody += " The request for employee confirmation of " + name + " (" + empcd + ") has been reviewed and approved by " + HODName + ". " + "<br />"

                    + "HOD Confirm Status: " + hodstatus + ".<br /><br /> "


                    + "This is an automated message for confirmation of Employee Approval by HOD. " + "<br /><br />"
                    + "<strong>**System Generated Email**</strong>";

                //hrid = "muhammad.mubbashir@alkaram.com";
                res.SendConfirmEmail(hrid, eBody, sub);
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
            DataTable dt = new DataTable();
                string query = "select d.detail_id, d.detail_name from hrm_setup_detl d where d.seq_no=163";
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
                    GridViewParam.DataSource = dt;
                    GridViewParam.DataBind();
                }
            }



        private void InsertEval(string DetailId, string Score)
        {
            try
            {
                string reqid = GetRequestId(TextBox2.Text);
                string insertEvalQuery = "INSERT INTO HRM_EMP_CONFIRM_EVAL (EMP_CD, DETAIL_ID, SCORE, L$USR_IN, L$IN_DATE, REQ_ID)" + " VALUES ('" + TextBox2.Text + "', '" + DetailId + "', '" + Score + "', '" + EmployeeUCode + "', SYSDATE, '" + reqid + "')";
                string resultEval = db.PostData(insertEvalQuery);
                if (resultEval == "Done")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Evaluation not submitted successfully !');", true);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


        private string GetRequestId(string empCode)
        {
            string requestIdQuery = "SELECT REQUEST_ID FROM HRM_EMP_CONFIRMATION WHERE EMP_CD = '" + empCode + "' ORDER BY REQUEST_ID DESC";
            DataTable dt = db.GetData(requestIdQuery);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["REQUEST_ID"].ToString();
            }
            return "0";
        }




    }
}