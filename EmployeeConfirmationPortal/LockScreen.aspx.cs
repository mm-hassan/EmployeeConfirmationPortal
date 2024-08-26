using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeConfirmationPortal
{
    public partial class LockScreen : System.Web.UI.Page
    {
        Database db = new Database();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_EmployeeCode.Focus();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string EmployeeCode = txt_EmployeeCode.Text.ToString().Trim();
                string EmployeePin = txt_EmployeePin.Text.ToString();
                //string UserCode, EmployeeID, EmployeeNAme, EmployeeDesig, Role_ID;

                DataTable dt = new DataTable();
                string query = "SELECT B.EMP_CD, B.EMP_NAME, HRM_CODE_DESC('DEPARTMENT', B.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', B.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, A.PIN_CD FROM HRM_EMP_PIN A, HRM_EMPLOYEE B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = '" + EmployeeCode + "' AND A.PIN_CD = '" + EmployeePin + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    //EmployeeID = dr["EMP_CD"].ToString();
                    //DataTable dt1 = new DataTable();
                    //string query1 = "SELECT B.EMP_CD, B.EMP_NAME, HRM_CODE_DESC('DEPARTMENT', B.HRM_DEPARTMENT_CD, NULL, NULL, NULL) DEPARTMENT_NAME, HRM_CODE_DESC('DESIGNATION', B.HRM_DESIGNATION_CD, NULL, NULL, NULL) DESIGNATION_NAME, A.PIN_CD FROM HRM_EMP_PIN A, HRM_EMPLOYEE B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = '" + EmployeeCode + "' AND A.PIN_CD = '" + EmployeePin + "'";
                    //dt = db.GetData(query1);
                    //if (dt1.Rows.Count > 0)
                    //{
                    //    DataRow drr = dt1.Rows[0];
                    Session["EmployeeCode"] = dr["EMP_CD"].ToString();
                    Session["EmployeeName"] = dr["EMP_NAME"].ToString();
                    Session["EmployeeDesignation"] = dr["DESIGNATION_NAME"].ToString();
                    Session["EmployeeDepartment"] = dr["DEPARTMENT_NAME"].ToString();
                    //Session["RoleID"] = drr["ROLE_ID"].ToString();
                    //Session["RoleName"] = drr["ROLE_NAME"].ToString();
                    Response.Redirect("Index.aspx");
                    //}

                }
                else
                {
                    string script = "alert('Invalid credentials');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", script, true);
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {

        }
    }
}