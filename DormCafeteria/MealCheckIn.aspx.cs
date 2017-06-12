using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    public TimeSpan breakfastBegin = new TimeSpan(4, 00, 00);
    public TimeSpan breakfastEnd = new TimeSpan(11, 00, 00);
    public TimeSpan lunchBegin = new TimeSpan(11, 00, 01);
    public TimeSpan lunchEnd = new TimeSpan(15, 00, 00);
    public TimeSpan DinnerBegin = new TimeSpan(15, 00, 01);
    public TimeSpan DinnerEnd = new TimeSpan(23, 00, 00);

    public int StaffMinRange = 1000;
    public int StaffMaxRange = 1999;
    public int RefMinRange = 2000;
    public int RefMaxRange = 2999;
    public int MedicalMinRange = 3000;
    public int MedicalMaxRange = 3999;

    string ConnString = ConfigurationManager.ConnectionStrings["ConnDormCafeteria"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack)
        {
            lblConfirmation.Text = "";
            int WristbandID = 0;
            if (txtWristbandIDNumber.Text != "")
            {
                WristbandID = Convert.ToInt32(txtWristbandIDNumber.Text);
            }
            int StaffTypeID = 0;
            int MealTypeID = 0;


            if (WristbandID < 1000)
            {
                lblMessage.Text = "Please enter a valid wristband number";
            }
            else if (WristbandID <= 1999) // STAFF range
            {
                StaffTypeID = 1;
            }
            else if (WristbandID <= 2999) // REFS range
            {
                StaffTypeID = 2;
            }
            else if (WristbandID <= 3999) // MEDICAL range
            {
                StaffTypeID = 3;
            }
            else lblMessage.Text = "Please enter a valid wristband number";


            if ((DateTime.Now.TimeOfDay > breakfastBegin) && (DateTime.Now.TimeOfDay < breakfastEnd))
            {
                //lblConfirmation.Text = "Breakfast punch accepted.";
                MealTypeID = 1;
            }
            else if ((DateTime.Now.TimeOfDay > lunchBegin) && (DateTime.Now.TimeOfDay < lunchEnd))
            {
                //lblConfirmation.Text = "Lunch punch accepted.";
                MealTypeID = 2;
            }
            else if ((DateTime.Now.TimeOfDay > DinnerBegin) && (DateTime.Now.TimeOfDay < DinnerEnd))
            {
                //lblConfirmation.Text = "Dinner punch accepted.";
                MealTypeID = 3;
            }
            else
            {
                //lblConfirmation.Text = "Cafeteria Closed";
            }

            if ((WristbandID != 0) && (StaffTypeID != 0) && (MealTypeID != 0)) // if the user entered a valid wristband id and it is breakfeast lunch or dinner
            {

                SqlConnection conn = new SqlConnection(ConnString);
                var cmd = new SqlCommand("InsertMeal", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("MealDateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("MealTypeID", MealTypeID);
                cmd.Parameters.AddWithValue("StaffTypeID", StaffTypeID);
                cmd.Parameters.AddWithValue("MealWristbandNumber", WristbandID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                lblConfirmation.Text = "Punch accepted.";
                lblMessage.Text = "";
                txtWristbandIDNumber.Text = "";
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblConfirmation.ClientID + "').style.display='none'\",3000)</script>");
            //ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblConfirmation.ClientID + "').innerHTML=''\",3000)</script>");
            if ((MealTypeID == 0) && (StaffTypeID != 0))// they entered a valid wristband but it isn't time to eat
            {
                lblMessage.Text = "Punch rejected.  It isn't time to eat.";
            }
            txtWristbandIDNumber.Text = "";
        }
    }
}
