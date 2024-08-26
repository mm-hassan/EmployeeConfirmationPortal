using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;



namespace EmployeeConfirmationPortal
{
    public partial class Hr_Approval : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode;
        string EmployeeUCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            //EmailIntimationHOD("Mubbashir", "muhammad.mubbashir@alkaram.com", "S", "9075802");
            getSession();
            
            Section1.Visible = false;

            if (!IsPostBack)
            {
                LoadDepartments();
                div_EmployeeDetails.Visible = false;

                if (Session["SuccessMessage"] != null)
                {
                    string successMessage = Session["SuccessMessage"].ToString();
                    // Display the success message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("SuccessMessage");
                }

                if (Session["RejectMessage"] != null)
                {
                    string successMessage = Session["RejectMessage"].ToString();
                    // Display the success message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showRejectSuccessToast", "showRejectSuccessToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("RejectMessage");
                }

                if (Session["RejectErrorMessage"] != null)
                {
                    string successMessage = Session["RejectErrorMessage"].ToString();
                    // Display the success message using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showRejectErrorToast", "showRejectErrorToast();", true);
                    // Remove the session variable to prevent showing the message again on subsequent visits
                    Session.Remove("RejectErrorMessage");
                }


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
                // Log the exception or handle it accordingly
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
                string query = "SELECT a.division_cd, a.division_name FROM HRM_DIVISION_MST a where EXISTS (SELECT b.hrm_division_cd FROM HRM_EMPLOYEE b , hrm_emp_confirmation c where b.emp_cd=c.emp_cd and c.confirmed_by_hod is not null and c.confirmation_status!='A' and a.division_cd=b.hrm_division_cd AND b.confirmation_date IS NULL )";
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




        public void loadInformation(string Req_Id)
        {
            try
            {

                DataTable dt = new DataTable();
                string query = "SELECT trunc(c.appointment_date) AS appointmentdate,c.HRM_CADRE_CD, c.CADRE_SUBCLASS_CD, c.emp_name, c.reg_cd, (select r.reg_name from regions r where r.reg_cd=c.reg_cd) Region_Name, hrm_code_desc('CADRE', c.HRM_CADRE_CD, NULL, NULL, NULL) CADRE_NAME, hrm_code_desc('DESIGNATION', c.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, hrm_code_desc('DIVISION', c.HRM_DIVISION_CD, NULL, NULL, NULL) DIVISION_NAME, hrm_code_desc('UNIT', c.HRM_UNIT_CD, c.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, hrm_code_desc('DEPARTMENT', c.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, hrm_code_desc('SECTION', c.HRM_SECTION_CD, NULL, NULL, NULL) SECTION_NAME,  a.* FROM HRM_EMP_CONFIRMATION A INNER JOIN HRM_EMPLOYEE c ON A.emp_cd=c.emp_cd  WHERE A.REQUEST_ID=  '" + Req_Id + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    Section1.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        TextBox17.Text = dr["REQUEST_ID"].ToString();
                        TextBox2.Text = dr["EMP_CD"].ToString();
                        TextBox3.Text = ToTitleCase(dr["emp_name"].ToString());
                        TextBox6.Text = ToTitleCase(dr["DEPARTMENT_NAME"].ToString());
                        TextBox7.Text = ToTitleCase(dr["DESIGNATION_NAME"].ToString());
                        TextBox8.Text = ToTitleCase(dr["HRM_CADRE_CD"].ToString()) + " - " + ToTitleCase(dr["CADRE_SUBCLASS_CD"].ToString());
                        TextBox9.Text = ToTitleCase(dr["SECTION_NAME"].ToString());
                        TextBox10.Text = dr["REQUEST_DATE"].ToString();
                        
                        
                        string hodconfstatus = dr["Hod_Confirm_Status"].ToString();
                        if (hodconfstatus == "S")
                        {
                            TextBox11.Text= "Satisfactory";
                        }
                        else if (hodconfstatus == "U")
                        {
                            TextBox11.Text = "Unsatisfactory";
                        }
                        else if (hodconfstatus == "P")
                        {
                            TextBox11.Text = "Probation Extended";
                        }



                        TextBox12.Text = ToTitleCase(dr["Region_Name"].ToString());
                        TextBox13.Text = ToTitleCase(dr["DIVISION_NAME"].ToString());
                        TextBox14.Text = ToTitleCase(dr["UNIT_NAME"].ToString());
                        TextBox15.Text = dr["appointmentdate"].ToString();
                        TextBox16.Text = dr["reg_cd"].ToString();

                        Per_Remarks.Text = ToTitleCase(dr["Performance_Remarks"].ToString());
                        Train_Remarks.Text = ToTitleCase(dr["Training_Remarks"].ToString());

                        TextBox4.Text = dr["L$USR_IN"].ToString();

                        Hod_Remarks.Text = dr["HOD_REMARKS"].ToString();

                        Prob_Period.Text = dr["PROBATION_EXT_PERIOD"].ToString();
                    }
                }
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
                string query = "SELECT (select r.reg_name from regions r where r.reg_cd=a.reg_cd) Region_Name, hrm_code_desc('UNIT', a.HRM_UNIT_CD, a.HRM_DIVISION_CD, NULL, NULL) UNIT_NAME, a.emp_cd, a.emp_name, (SELECT F.Performance_Remarks from hrm_emp_confirmation F WHERE a.emp_cd = F.EMP_CD) AS Performance_Remarks, (SELECT F.Training_Remarks from hrm_emp_confirmation F  WHERE a.emp_cd = F.EMP_CD) AS Training_Remarks, (SELECT F.REQUEST_ID from hrm_emp_confirmation F WHERE a.emp_cd=F.EMP_CD) AS REQUEST_ID, (SELECT F.HOD_CONFIRM_STATUS from hrm_emp_confirmation F WHERE a.emp_cd=F.EMP_CD) AS HOD_CONFIRM_STATUS, (SELECT F.CONFIRMATION_STATUS from hrm_emp_confirmation F WHERE a.emp_cd=F.EMP_CD) AS CONFIRMATION_STATUS, (SELECT b.department_name FROM HRM_DEPARTMENT_mst b WHERE a.hrm_department_cd=b.department_cd) AS Department_Name, (SELECT c.designation_name FROM HRM_DESIGNATION_MST c WHERE a.hrm_designation_cd=c.designation_cd) AS Designation_Name, a.reg_cd FROM hrm_employee a where a.hrm_division_cd= '" + Div_Cd + "' AND a.confirmation_date IS NULL AND EXISTS (SELECT F.EMP_CD FROM HRM_EMP_CONFIRMATION F WHERE a.emp_cd=F.EMP_CD AND F.CONFIRMED_BY_HOD IS NOT NULL AND F.CONFIRMATION_STATUS!='A')";
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



      



        public static string ToTitleCase(string str)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(str.ToLower());
        }



        public void EmailIntimationHOD(string empname, string EmailId, string hodconfirmstatus, string empcode)
        {
            try
            {
                SendResponse res = new SendResponse();

                string EmailHodCC = "";
                string EmailLineManagerCC = "";
                string Designation = "";
                string Department= "";
                string gender = "";
                string Probation_Ext_Period = "";
                
                //For Hr Email in CC
                string EmailHrCC = "";
                string SelectQuery = "SELECT A.e_Mail FROM HRM_EMPLOYEE A WHERE A.Emp_Cd='" + EmployeeCode + "'";
                DataTable dtt = new DataTable();
                dtt = db.GetData(SelectQuery);
                if (dtt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtt.Rows)
                    {
                        EmailHrCC = (dr["e_Mail"].ToString()).ToLower();
                    }
                }

                //For HOD and Line Manager EMAIL IN CC
                string HODQuery = "SELECT e.EMP_HOD_CD, e.line_manager_cd, a.emp_cd, (SELECT L.e_Mail FROM hrm_employee l WHERE e.EMP_HOD_CD=l.emp_cd) mailHod, (SELECT L.e_Mail FROM hrm_employee l WHERE e.line_manager_cd=l.emp_cd) mailLineManager FROM hrm_employee a INNER JOIN hrm_employee_info_view e ON a.EMP_CD=e.emp_cd where a.emp_cd='" +empcode+ "'";
                DataTable HODdt = new DataTable();
                HODdt = db.GetData(HODQuery);
                if (HODdt.Rows.Count > 0)
                {
                    foreach (DataRow dr in HODdt.Rows)
                    {
                        EmailHodCC = (dr["mailHod"].ToString()).ToLower();
                        EmailLineManagerCC = (dr["mailLineManager"].ToString()).ToLower();
                    }
                }
                if(EmailId=="")
                {
                    EmailId = EmailLineManagerCC;
                }

                if (hodconfirmstatus == "S")
                {
                    string Query = "SELECT sysdate, a.gender, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, a.APPOINTMENT_DATE, a.Hrm_Designation_Cd, ADD_MONTHS(a.APPOINTMENT_DATE, 3)- 1 AS PROB_COMP_DATE, ADD_MONTHS(a.APPOINTMENT_DATE, 3) AS EFFECTIVE_DATE, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS Designation_Name FROM Hrm_Employee A WHERE A.Emp_Cd = '" + empcode + "' ";
                    DataTable dat = new DataTable();
                    dat = db.GetData(Query);
                    if (dat.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dat.Rows)
                        {
                            DateTime sysdate = (DateTime)dr["sysdate"];
                            string sysdateString = sysdate.ToString("dd-MM-yyyy");

                            DateTime sysdateyear = (DateTime)dr["sysdate"];
                            string sysdateyearString = sysdate.ToString("yyyy");

                            //sysdatee = dr["sysdate"].ToString();
                            DateTime APPOINTMENTDATE = (DateTime)dr["APPOINTMENT_DATE"];
                            string APPOINTMENTDATESTRING = APPOINTMENTDATE.ToString("dd-MMMM-yyyy");
                            DateTime PROB_COMPDATE = (DateTime)dr["PROB_COMP_DATE"];
                            string PROB_COMPDATESTRING = PROB_COMPDATE.ToString("dd-MMMM-yyyy");
                            DateTime EFFECTIVEDATE = (DateTime)dr["EFFECTIVE_DATE"];
                            string EFFECTIVEDATESTRING = EFFECTIVEDATE.ToString("dd-MMMM-yyyy");
                            Designation = dr["Designation_Name"].ToString();
                            Department= dr["DEPARTMENT_NAME"].ToString();
                            gender = dr["gender"].ToString();
                            if (gender == "M") 
                            {
                                gender = "Mr. ";
                            }
                            else if (gender == "F")
                            {
                                gender = "Ms. ";
                            }

                            string abovetext = "Ref: AKTM/HR/E#" + empcode + "/" + sysdateyearString + "                                                                                                                       Dated: " + sysdateString + "";

                            string belowabovetext =
                                "<span>" + empname + "</span><br />" +
                                "<span>" + empcode + "</span><br />" +
                                "<span>" + Designation + "</span><br />" +
                                "<span>" + Department + "</span><br /><br />";


                            string sub = "Confirmation of Services";
                            string eBody = "<div style='text-align:justify;'>" +
               "<strong>Dear " + gender + " " + empname + ",</strong><br /><br />" +
               "Congratulations!<br /><br />" +
               "This is with reference to the review of your performance during the probation period from <br />" +
               "<strong>" + APPOINTMENTDATESTRING + "</strong> to <strong>" + PROB_COMPDATESTRING + "</strong>, " +
               "we are pleased to inform you that your services are being confirmed as '" + Designation + "' with effect from " +
               "<strong>" + EFFECTIVEDATESTRING + ".</strong><br /><br />" +
               "All the other terms and conditions of your employment remain unchanged.<br /><br />" +
               "This reflects on your efforts and valuable contribution in your current role and we look forward to the same level of enthusiasm in your future assignments.<br /><br />" +
               "Wishing you the very best in your career ahead with Alkaram Textile Mills.<br /><br />" +
               "<strong>Best wishes,</strong><br /><strong>Head of HR & Admin</strong><br /><br /><br /><br />" +
               "<strong>*This is a system generated document and does not require a signature*</strong>" +
               "</div>";

                                    //"<strong>_______________________________</strong><br /><br />" +
                                    //"<strong>Head of Human Resources & Admin</strong>";


                            //string sub = "Permanent Employee Confirmation";
                            //string eBody = "Dear '" + empname + "',<br /><br />Congratulations on completing your probationary period at Alkaram Textile Mills! " +
                            //        "We are delighted to confirm your permanent status based on your outstanding commitment and contributions. " +
                            //        "This automated message solidifies your official position as a permanent employee. Your impactful contributions make a difference, " +
                            //        "and we believe in your continued success with us. Your permanent employment is confirmed, and the terms and conditions from your " +
                            //        "appointment letter remain unchanged. Best wishes for your future endeavors with Alkaram Textile Mills.<br /><br />Regards,<br /><br /><strong>HR Department</strong>";

                            //EmailId = "muhammad.mubbashir@alkaram.com";
                            byte[] pdfBytes = GeneratePDF(abovetext, sub, eBody, belowabovetext);
                            res.SendEmailWithAttachment(EmailId, sub, pdfBytes, eBody, EmailHrCC, EmailHodCC, EmailLineManagerCC);
                        }
                    }
                }
                else if (hodconfirmstatus == "U")
                {
                    string Query = "SELECT sysdate, a.gender, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, a.APPOINTMENT_DATE, a.Hrm_Designation_Cd, ADD_MONTHS(a.APPOINTMENT_DATE, 3)- 1 AS PROB_COMP_DATE, ADD_MONTHS(a.APPOINTMENT_DATE, 3) AS EFFECTIVE_DATE, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS Designation_Name FROM Hrm_Employee A WHERE A.Emp_Cd = '" + empcode + "' ";
                    DataTable dat = new DataTable();
                    dat = db.GetData(Query);
                    if (dat.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dat.Rows)
                        {
                            DateTime sysdate = (DateTime)dr["sysdate"];
                            string sysdateString = sysdate.ToString("dd-MM-yyyy");

                            DateTime sysdateyear = (DateTime)dr["sysdate"];
                            string sysdateyearString = sysdate.ToString("yyyy");

                            //sysdatee = dr["sysdate"].ToString();
                            DateTime APPOINTMENTDATE = (DateTime)dr["APPOINTMENT_DATE"];
                            string APPOINTMENTDATESTRING = APPOINTMENTDATE.ToString("dd-MMMM-yyyy");
                            DateTime PROB_COMPDATE = (DateTime)dr["PROB_COMP_DATE"];
                            string PROB_COMPDATESTRING = PROB_COMPDATE.ToString("dd-MMMM-yyyy");
                            DateTime EFFECTIVEDATE = (DateTime)dr["EFFECTIVE_DATE"];
                            string EFFECTIVEDATESTRING = EFFECTIVEDATE.ToString("dd-MMMM-yyyy");
                            Designation = dr["Designation_Name"].ToString();

                            Department = dr["DEPARTMENT_NAME"].ToString();
                            gender = dr["gender"].ToString();
                            if (gender == "M")
                            {
                                gender = "Mr. ";
                            }
                            else if (gender == "F")
                            {
                                gender = "Ms. ";
                            }

                            string abovetext = "Ref: AKTM/HR/E#" + empcode + "/" + sysdateyearString + "                                                                                                                       Dated: " + sysdateString + "";


                            string belowabovetext =
                                                            "<span>" + empname + "</span><br />" +
                                                            "<span>" + empcode + "</span><br />" +
                                                            "<span>" + Designation + "</span><br />" +
                                                            "<span>" + Department + "</span><br /><br />";



                            string sub = "Letter of Termination";
                            string eBody = "<div style='text-align:justify;'>" +
               "<strong>Dear " + gender + " " + empname + ",</strong><br /><br />" +
               "You were appointed in terms of Letter of Appointment dated: " + APPOINTMENTDATESTRING + ", " +
               "terms and conditions of which were duly accepted by you; wherein your Probationary Period will be completing on " + PROB_COMPDATESTRING + ".<br /><br />" +
               "Your performance during the probation period is unsatisfactory. Therefore, under the circumstances and in accordance with clause 4 of your Letter of Appointment, the Management is compelled to terminate your services with immediate effect and you stand relieved from your services.<br /><br />" +
               "You are advised to collect your legal dues from the payroll section after completing your exit clearance.<br /><br />" +
               "<strong>Regards,</strong><br /><strong>Head of HR & Admin</strong><br /><br /><br /><br />" +
               "<strong>*This is a system-generated document and does not require signature*</strong>" +
               "</div>";



                            byte[] pdfBytes = GeneratePDF(abovetext, sub, eBody, belowabovetext);
                            res.SendEmailWithAttachment(EmailId, sub, pdfBytes, eBody, EmailHrCC, EmailHodCC, EmailLineManagerCC);
                        }   
                    }
                }

                else if (hodconfirmstatus == "P")
                {
                    string Query = "SELECT (SELECT v.probation_ext_period FROM HRM_EMP_CONFIRMATION v WHERE v.emp_cd= '" + empcode + "') AS Probation_Ext_Period, sysdate, a.gender, hrm_code_desc('DEPARTMENT', a.HRM_DEPARTMENT_CD, NULL, NULL, NULL) AS DEPARTMENT_NAME, a.APPOINTMENT_DATE, a.Hrm_Designation_Cd, ADD_MONTHS(a.APPOINTMENT_DATE, 3)- 1 AS PROB_COMP_DATE, ADD_MONTHS(a.APPOINTMENT_DATE, 3) AS EFFECTIVE_DATE, hrm_code_desc('DESIGNATION', a.HRM_DESIGNATION_CD, NULL, NULL, NULL) AS Designation_Name FROM Hrm_Employee A WHERE A.Emp_Cd = '" + empcode + "' ";
                    DataTable dat = new DataTable();
                    dat = db.GetData(Query);
                    if (dat.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dat.Rows)
                        {
                            DateTime sysdate = (DateTime)dr["sysdate"];
                            string sysdateString = sysdate.ToString("dd-MM-yyyy");

                            DateTime sysdateyear = (DateTime)dr["sysdate"];
                            string sysdateyearString = sysdate.ToString("yyyy");

                            //sysdatee = dr["sysdate"].ToString();
                            DateTime APPOINTMENTDATE = (DateTime)dr["APPOINTMENT_DATE"];
                            string APPOINTMENTDATESTRING = APPOINTMENTDATE.ToString("dd-MMMM-yyyy");
                            DateTime PROB_COMPDATE = (DateTime)dr["PROB_COMP_DATE"];
                            string PROB_COMPDATESTRING = PROB_COMPDATE.ToString("dd-MMMM-yyyy");
                            DateTime EFFECTIVEDATE = (DateTime)dr["EFFECTIVE_DATE"];
                            string EFFECTIVEDATESTRING = EFFECTIVEDATE.ToString("dd-MMMM-yyyy");
                            Designation = dr["Designation_Name"].ToString();
                            Probation_Ext_Period = dr["Probation_Ext_Period"].ToString();



                            string abovetext = "Ref: AKTM/HR/E#" + empcode + "/" + sysdateyearString + "                                                                                                                       Dated: " + sysdateString + "";

                            string belowabovetext =
                                                            "<span>" + empname + "</span><br />" +
                                                            "<span>" + empcode + "</span><br />" +
                                                            "<span>" + Designation + "</span><br />" +
                                                            "<span>" + Department + "</span><br /><br />";


                            string sub = "Extension of Probationary Period";
                            string eBody = "<div style='text-align:justify;'>" +
               "<strong>Dear " + gender + " " + empname + ",</strong><br /><br />" +
               "This is with reference to the review of your performance during the probation period from <strong>" + APPOINTMENTDATESTRING + "</strong> to <strong>" + PROB_COMPDATESTRING + "</strong>, " +
               "we regretfully inform you that your performance has not been up to the required level. Hence, the management has decided to extend your probationary period for " + Probation_Ext_Period + ".<br /><br />" +
               "You are advised to improve on your skills and job performance areas which have already been discussed with you by your line manager. Your performance shall be reviewed again before the end of the stipulated time and you shall be notified accordingly.<br /><br />" +
               "Looking forward to your improved performance.<br /><br />" +
               "<strong>Best wishes, </strong><br /><strong>Head of HR & Admin </strong><br /><br /><br /><br />" +
               "<strong>*This is a system-generated document and does not require a signature*</strong>" +
               "</div>";


                            byte[] pdfBytes = GeneratePDF(abovetext, sub, eBody, belowabovetext);
                            //EmailId = "muhammad.mubbashir@alkaram.com";
                            res.SendEmailWithAttachment(EmailId, sub, pdfBytes, eBody, EmailHrCC, EmailHodCC, EmailLineManagerCC);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                

                //Console.WriteLine("Email error " + ex.Message);
               
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);

            }

        }


        public byte[] GeneratePDF(string titleText, string bodyText)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // Set up fonts and styles
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font titleFont = new Font(baseFont, 16, Font.BOLD, BaseColor.BLACK);
                Font bodyFont = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);

                // Add title
                Paragraph title = new Paragraph(titleText, titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;
                document.Add(title);

                // Parse HTML content and add body text
                using (StringReader sr = new StringReader(bodyText))
                {
                    HTMLWorker htmlWorker = new HTMLWorker(document);
                    htmlWorker.Parse(sr);
                }

                document.Close();
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
                return null; // Or handle the error as appropriate
            }
        }



        private void ParseHTMLbelowContent(Document document, string eBody)
        {
            try
            {
                using (StringReader sr = new StringReader(eBody))
                {
                    // Create a StyleSheet to define styles
                    StyleSheet styles = new StyleSheet();

                    // Define a custom font style
                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font bodyFont = new Font(baseFont, 9, Font.NORMAL, BaseColor.BLACK);

                    // Apply the font style to body elements
                    styles.LoadTagStyle(HtmlTags.BODY, HtmlTags.SIZE, "9pt");
                    styles.LoadTagStyle(HtmlTags.BODY, HtmlTags.FACE, baseFont.PostscriptFontName);
                    styles.LoadTagStyle(HtmlTags.BODY, HtmlTags.COLOR, "#" + BaseColor.BLACK.ToArgb().ToString("X6").Substring(2));

                    // Set the paragraph style to remove spacin

                    // Create the HTMLWorker with the defined StyleSheet
                    List<IElement> elements = HTMLWorker.ParseToList(sr, styles);

                    foreach (IElement element in elements)
                    {
                        document.Add(element);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('parse error')", true);
            }
        }





        public byte[] GeneratePDF(string abovetext, string sub, string eBody, string belowabovetext)
        {
            MemoryStream stream = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            try
            {
                SetupFonts(document);
                AddHeader(document);
                AddTitleAbove(document, abovetext);
                ParseHTMLbelowContent(document, belowabovetext);
                AddTitle(document, sub);
                
                ParseHTMLContent(document, eBody);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('generate pdf error !')", true);                
            }
            finally
            {
                document.Close();
            }

            return stream.ToArray();
        }

        private void SetupFonts(Document document)
        {
            try
            {
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font titleFont = new Font(baseFont, 13, Font.BOLD, BaseColor.BLACK);
                Font bodyFont = new Font(baseFont, 9, Font.NORMAL, BaseColor.BLACK);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('font setup error !')", true);                
            }
        }

        private void AddHeader(Document document)
        {
            try
            {
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font titleFont = new Font(baseFont, 13, Font.BOLD, BaseColor.BLACK);
                Font bodyFont = new Font(baseFont, 9, Font.NORMAL, BaseColor.BLACK);

                PdfPTable headerTable = new PdfPTable(3);
                headerTable.WidthPercentage = 100;
                // Add Logo

                //string logoPath = "D:/Mubbashir Projects/EmployeeConfirmationPortal/EmployeeConfirmationPortal/EmployeeConfirmationPortal/images/logo.PNG";

                string logoPath = "D:/liveApps/ECP/Assest/dist/img/logo.png";
                //string logoPath = "E:/Mubbashir Projects/publish files/ECP(1)/Assest/dist/img/logo.png";
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                logo.ScaleToFit(500f, 500f);
                PdfPCell logoCell = new PdfPCell(logo);
                logoCell.Border = PdfPCell.NO_BORDER;
                logoCell.Padding = 0;
                headerTable.AddCell(logoCell);

                // Add Company Name
                PdfPCell companyNameCell = new PdfPCell(new Phrase("", titleFont));
                companyNameCell.Border = PdfPCell.NO_BORDER;
                companyNameCell.Colspan = 2;
                headerTable.AddCell(companyNameCell);

                document.Add(headerTable);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('add header header !')", true);                
            }
        }

        private void AddTitleAbove(Document document, string abovetext)
        {
            try
            {
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font titleFont = new Font(baseFont, 13, Font.BOLD, BaseColor.BLACK);
                Font bodyFont = new Font(baseFont, 9, Font.NORMAL, BaseColor.BLACK);

                Paragraph titleabove = new Paragraph(abovetext, bodyFont);
                titleabove.SpacingBefore = 20f;
                titleabove.SpacingAfter = 20f;
                document.Add(titleabove);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Add title above error !')", true);                
            }
        }

        private void AddTitle(Document document, string sub)
        {
            try
            {
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font titleFont = new Font(baseFont, 13, Font.BOLD, BaseColor.BLACK);
                Font bodyFont = new Font(baseFont, 9, Font.NORMAL, BaseColor.BLACK);

                // Create a Chunk with the title text and underline it
                Chunk titleChunk = new Chunk(sub, titleFont);
                titleChunk.SetUnderline(0.5f, -1.5f); // Thickness and y-position of the underline

                // Create a Paragraph and add the Chunk to it
                Paragraph title = new Paragraph();
                title.Add(titleChunk);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;

                document.Add(title);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('add title error')", true);
            }
        }


        private void ParseHTMLContent(Document document, string eBody)
        {
            try
            {
                using (StringReader sr = new StringReader(eBody))
                {
                    HTMLWorker htmlWorker = new HTMLWorker(document);
                    htmlWorker.Parse(sr);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('parse error')", true);                
            }
        }



      


        protected void lbl_Show_Click(object sender, EventArgs e)
        {
            try
            {
                string reqid = (sender as LinkButton).CommandArgument;
                string[] Ids = reqid.Split('-');
                string Req_Id = Ids[0].ToString();
                loadInformation(Req_Id);
                GetParam();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }


     


         protected void lbl_Reject_Button_Click(object sender, EventArgs e)
        {
            try
            {
                // Delete inserted data
                string deleteEvalQuery = "DELETE FROM HRM_EMP_CONFIRM_EVAL WHERE EMP_CD = '" + TextBox2.Text + "'";
                string deleteConfirmationQuery = "DELETE FROM HRM_EMP_CONFIRMATION WHERE EMP_CD = '" + TextBox2.Text + "'";
                string resultDeleteEval = db.PostData(deleteEvalQuery);
                string resultDeleteConfirmation = db.PostData(deleteConfirmationQuery);
        
                // Update confirmation status in HRM_EMPLOYEE table
                string updateConfirmationStatusQuery = "UPDATE HRM_EMPLOYEE SET CONFIRMATION_DATE = NULL WHERE emp_cd = '" + TextBox2.Text + "'";
                string resultUpdateStatus = db.PostData(updateConfirmationStatusQuery);

                if (resultDeleteEval == "Done" && resultDeleteConfirmation == "Done" && resultUpdateStatus == "Done")
                {
                    GetHod(TextBox2.Text);
                    Session["RejectMessage"] = "Your success message here";
                    Response.Redirect("Hr_Approval.aspx");
                    Session["RejectMessage"] = "Your success message here";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showRejectSuccessToast", "showRejectSuccessToast();", true);
                }
                else
                {
                    Session["RejectErrorMessage"] = "Your success message here";
                    Response.Redirect("Hr_Approval.aspx");
                    Session["RejectErrorMessage"] = "Your success message here";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showRejectErrorToast", "showRejectErrorToast();", true);
                }
                remarkstext.Text = "";
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
                     EmailRejectHOD(hodemail, hodname, employeename);
                 }
                 // No data found for the selected value, clear the GridView
                 gv_PendingRequests.DataSource = null;
                 gv_PendingRequests.DataBind();
                 LoadDepartments();
             }
             catch (Exception ex)
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
             }
         }

        
         public void EmailRejectHOD(string hodemail, string hodname, string employeename)
         {
             try
             {
                 SendResponse res = new SendResponse();


                 string sub = string.Empty;
                 sub += "Request for Employee Confirmation - " + employeename + "";
                 string eBody = string.Empty;
                 eBody += "Dear " + hodname + ",<br /><br />";
                 eBody += "It is to inform you that a request for the confirmation of " + employeename + " has been reversed by HR department." + "<br />"
                     + "Kindly review the details again at your earliest convenience." + "<br /><br />"
                     + "HR Remarks: '"+remarkstext.Text+"'" + "<br />"
                     + "Thank you for your cooperation." + "<br /><br />"
                     + "This is an automated message for confirmation of Employee Approval by HOD. " + "<br /><br />"
                     + "<strong>**System Generated Email**</strong>";


                 res.SendConfirmEmail(hodemail, eBody, sub);
             }
             catch (Exception ex)
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
             }

         }

        protected void lbl_Approve_Button_Click(object sender, EventArgs e)
        {
            try
            {
                string insertDetailQuery = "UPDATE HRM_EMP_CONFIRMATION SET CONFIRMATION_STATUS= 'A', L$USR_UP = '" + EmployeeUCode + "', L$UP_DATE = SYSDATE, HR_REMARKS='" + remarkstext.Text + "' WHERE REQUEST_ID = '" + TextBox17.Text + "' ";
                string resultDetail = db.PostData(insertDetailQuery);
                if (resultDetail == "Done")
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessToast", "showSuccessToast();", true);
                    div_EmployeeDetails.Visible = false;
                    string SelectQuery = "SELECT A.EMP_CD, (SELECT B.e_Mail FROM HRM_EMPLOYEE B WHERE A.EMP_CD = B.Emp_Cd) AS EMAIL, (SELECT B.Emp_Name FROM HRM_EMPLOYEE B WHERE A.EMP_CD = B.Emp_Cd) AS EMP_NAME, (SELECT D.HOD_CONFIRM_STATUS FROM HRM_EMP_CONFIRMATION D WHERE REQUEST_ID = '" + TextBox17.Text + "') AS HOD_CONFIRM_STATUS, (SELECT SUBSTR(D.Probation_Ext_Period, 1, 1) FROM HRM_EMP_CONFIRMATION D WHERE REQUEST_ID = '" + TextBox17.Text + "') AS Probation_Ext_Period FROM HRM_EMP_CONFIRMATION A WHERE REQUEST_ID = '" + TextBox17.Text + "'";
                    //SELECT A.EMP_CD, (SELECT B.e_Mail FROM HRM_EMPLOYEE B WHERE A.EMP_CD=B.Emp_Cd) AS EMAIL, (SELECT B.Emp_Name FROM HRM_EMPLOYEE B WHERE A.EMP_CD=B.Emp_Cd) AS EMP_NAME, (SELECT D.HOD_CONFIRM_STATUS FROM HRM_EMP_CONFIRMATION D WHERE REQUEST_ID= '" + TextBox17.Text + "' ) AS HOD_CONFIRM_STATUS FROM HRM_EMP_CONFIRMATION A WHERE REQUEST_ID='" + TextBox17.Text + "' ";
                    DataTable dtt = new DataTable();
                    dtt = db.GetData(SelectQuery);
                    if (dtt.Rows.Count > 0)
                    {
                        string hodconfirmstatus = "";
                        string empname = "";
                        string EmailId = "";
                        string empcode = "";
                        string ext_per= "";
                        foreach (DataRow dr in dtt.Rows)
                        {

                            empname = ToTitleCase(dr["EMP_NAME"].ToString());
                            EmailId = (dr["EMAIL"].ToString()).ToLower();
                            empcode = dr["EMP_CD"].ToString();

                            hodconfirmstatus = dr["HOD_CONFIRM_STATUS"].ToString();
                        
                        }

                        if (hodconfirmstatus == "S")
                        {
                            string insertconfdate = "UPDATE HRM_EMPLOYEE SET CONFIRMATION_DATE=(SELECT ADD_MONTHS(e.appointment_date, 3) AS Confirm_Date FROM HRM_EMPLOYEE E WHERE E.Emp_Cd = '" + empcode + "') WHERE emp_cd='" + empcode + "'";
                            string resultDate = db.PostData(insertconfdate);
                            if (resultDate == "Done")
                            {
                                EmailIntimationHOD(empname, EmailId, hodconfirmstatus, empcode);
                                Session["SuccessMessage"] = "Your success message here";
                                Response.Redirect("Hr_Approval.aspx");
                                Session["SuccessMessage"] = "Your success message here";
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
                            }
                        }
                        else
                        {
                            EmailIntimationHOD(empname, EmailId, hodconfirmstatus, empcode);
                            Session["SuccessMessage"] = "Your success message here";
                            Response.Redirect("Hr_Approval.aspx");
                            Session["SuccessMessage"] = "Your success message here";
                        }

                        //else if (hodconfirmstatus == "P")
                        //{
                        //    string insertconfdate = "UPDATE HRM_EMPLOYEE SET CONFIRMATION_DATE=(SELECT ADD_MONTHS(e.appointment_date, 3+"+ ext_per +") AS Confirm_Date FROM HRM_EMPLOYEE E WHERE E.Emp_Cd = '" + empcode + "') WHERE emp_cd='" + empcode + "'";
                        //    string resultDate = db.PostData(insertconfdate);
                        //    if (resultDate == "Done")
                        //    {
                        //        EmailIntimationHOD(empname, EmailId, hodconfirmstatus, empcode);
                        //        Session["SuccessMessage"] = "Your success message here";
                        //        Response.Redirect("Hr_Approval.aspx");
                        //        Session["SuccessMessage"] = "Your success message here";
                        //    }
                        //    else
                        //    {
                        //        ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
                        //    }
                        //}
                    }


                    // No data found for the selected value, clear the GridView
                    remarkstext.Text="";
                    gv_PendingRequests.DataSource = null;
                    gv_PendingRequests.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showErrorToast();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }



        public void GetParam()
        {
             try
            {
                DataTable dt = new DataTable();
                string query = "select d.detail_id, d.detail_name, (select A.Score from HRM_EMP_CONFIRM_EVAL A Where d.detail_id=A.DETAIL_ID AND A.Req_Id= '" + TextBox17.Text + "' AND A.EMP_CD= '" + TextBox2.Text + "') AS SCORE from hrm_setup_detl d where d.seq_no=163";
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
        
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }

    }
}