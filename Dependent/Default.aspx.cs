using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Dependent_Default : System.Web.UI.Page
{
    private const double DependentBenefitCost = 500.00; //cost of benefits for a dependent for a year
    private const int TotalPayPeriods = 26; //total pay periods
    private const double DiscountPct = 0.10; //discount percentage
    private Boolean isDiscounted = false; //flag indicates is dependent is eligible for discount


    /// <summary>
    /// loads the page and populates messages and entry fields based on session variables
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpAdd"] != null && Session["EmpID"] != null && Session["EmpFN"] != null && Session["EmpLN"] != null)
        {
            SuccessLabel.Text = "SUCCESSFULLY added employee " + Session["EmpFN"].ToString() + " " + Session["EmpLN"].ToString();
            //SuccessLab.Text = "SUCCESSFULLY added employee " + Session["EmpFN"].ToString() + " " + Session["EmpLN"].ToString();
            Provider.SelectedValue = Session["EmpID"].ToString();
            LastName.Text = Session["EmpLN"].ToString();
            StartDate.Text = Session["Date"].ToString();
        }
        if (Session["DepAdd"] != null && Session["DepFN"] != null && Session["DepLN"] != null)
        {
            SuccessLabel.Text = "SUCCESSFULLY added dependent: " + Session["DepFN"].ToString() + " " + Session["DepLN"].ToString();
            SuccessLab.Text = "SUCCESSFULLY added dependent: " + Session["DepFN"].ToString() + " " + Session["DepLN"].ToString();
            if (Session["AddSame"] != null)
            {
                if (Session["AddSame"].ToString().Equals("true"))
                {
                    Provider.SelectedValue = Session["EmpID"].ToString();
                    LastName.Text = Session["DepLN"].ToString();
                    StartDate.Text = Session["Date"].ToString();
                }
            }
        }

        Session.Clear();
    }


    /// <summary>
    /// checks to see if the dependent is eligible for a discount based
    /// upon wether or not their name starts with the letter 'A'
    /// </summary>
    /// <returns>true if eligible for discount, false otherwise</returns>
    private bool checkForDiscount()
    {
        Boolean aPresent = false;
        String fName = FirstName.Text.ToUpper();
        if (fName.Length > 0)
        {
            if (fName[0].Equals('A'))
            {
                aPresent = true;
            }

        }
        return aPresent;
    }

    /// <summary>
    /// calculates the cost of the dependent's benefits prorated based on
    /// their starting date and decreased by the discount percentage if
    /// they are eligible
    /// </summary>
    /// <returns>the cost of the dependent's benefits</returns>
    private double calcBenefitCost()
    {
        double benefitCost = DependentBenefitCost;
        if (isDiscounted)
        {
            benefitCost = (1 - DiscountPct) * benefitCost;
        }
        double proratedBenCost = (benefitCost / TotalPayPeriods) * (TotalPayPeriods - GetStartingPayPeriod() + 1);
        return Math.Round(proratedBenCost, 2);
    }

    /// <summary>
    /// gets the payperiod for which the dependent will be added to the
    /// employee's benefit account;
    /// </summary>
    /// <returns>the starting payperiod number</returns>
    private int GetStartingPayPeriod()
    {
        DateTime dateObject = Convert.ToDateTime(StartDate.Text);
        CultureInfo currCulture = CultureInfo.CurrentCulture;
        int weekNum = currCulture.Calendar.GetWeekOfYear(dateObject, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        return (weekNum + 1) / 2;
    }

    /// <summary>
    /// adds the cost of employee benefits to the database
    /// </summary>
    /// <param name="connection"></param>
    /// <returns>the number of rows effected in the database</returns>
    private int DataBaseUpdateDependentBenefits(SqlConnection connection)
    {
        string updateBenefits = "UPDATE EmpBenefit SET EBenBalance += @bal, EBenNumDepDisc += @num WHERE EmpId = @EmpID";
        SqlCommand sqlCom = new SqlCommand(updateBenefits, connection);
        sqlCom.Parameters.AddWithValue("@bal", calcBenefitCost());
        sqlCom.Parameters.AddWithValue("@num", isDiscounted);
        sqlCom.Parameters.AddWithValue("@EmpId", Provider.SelectedValue);
        return sqlCom.ExecuteNonQuery();
    }

    /// <summary>
    /// inserts the dependent's information into the database
    /// </summary>
    /// <param name="newID"></param>
    /// <param name="connection"></param>
    /// <returns>the number of rows effected in the database</returns>
    private int DataBaseInsertDependent(Guid newID, SqlConnection connection)
    {
        string insertDependent = "INSERT INTO Depend (DepId, DepFirstName, DepLastName, DepSSN, DepAddDate, DepProvider, DepActive, DepBenDisc) VALUES(@DepId, @DepFirstName, @DepLastName, @DepSSN, @DepAddDate, @DepProvider, @DepActive, @DepBenDisc)";
        SqlCommand sqlCom = new SqlCommand(insertDependent, connection);
        sqlCom.Parameters.AddWithValue("@DepId", newID);
        sqlCom.Parameters.AddWithValue("@DepFirstName", FirstName.Text);
        sqlCom.Parameters.AddWithValue("@DepLastName", LastName.Text);
        sqlCom.Parameters.AddWithValue("@DepSSN", SSN.Text);
        sqlCom.Parameters.AddWithValue("@DepAddDate", StartDate.Text);
        sqlCom.Parameters.AddWithValue("@DepProvider", Provider.Text);
        sqlCom.Parameters.AddWithValue("@DepActive", true);
        sqlCom.Parameters.AddWithValue("@DepBenDisc", isDiscounted);
        return sqlCom.ExecuteNonQuery();
    }

    /// <summary>
    /// Adds a dependent based on the information retrieved from the webform
    /// </summary>
    private void AddDependent()
    {
        try
        {
            Guid newDepID = Guid.NewGuid();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DependentConnectionString"].ConnectionString);
            connection.Open();
            isDiscounted = checkForDiscount();
            int queryReturn1 = DataBaseInsertDependent(newDepID, connection);
            int queryReturn2 = DataBaseUpdateDependentBenefits(connection);
            if (queryReturn2 != 0 && queryReturn1 != 0)
            {
                Session["DepAdd"] = "true";
            }
            else
            {
                Session["DepNotAdd"] = "true";
            }
            connection.Close();
        }
        catch (Exception ex)
        {
            SuccessLabel.Text = "Error: Dependent NOT Saved - " + ex.ToString();
        }
        Response.Write("Your Registration is Successful");
    }

    /// <summary>
    /// Adds dependent information and directs the user to another form
    /// partially completed with the same provider(employee) information 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddAnotherSame_Click(object sender, EventArgs e)
    {
        AddDependent();
        Session["DepFN"] = FirstName.Text;
        Session["DepLN"] = LastName.Text;
        Session["EmpID"] = Provider.SelectedValue;
        Session["Date"] = StartDate.Text;
        Session["AddSame"] = "true";
        Response.Redirect("/Dependent");
    }

    /// <summary>
    /// Adds dependent information and directs the user to another blank
    /// form for entering another dependent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddDifferent_Click(object sender, EventArgs e)
    {
        AddDependent();
        Session["DepFN"] = FirstName.Text;
        Session["DepLN"] = LastName.Text;
        Session["EmpID"] = Provider.SelectedValue;
        Response.Redirect("/Dependent");
    }

    /// <summary>
    /// Adds dependent information and directs the user to preview
    /// the costs for the provider(employee)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PreviewCosts_Click(object sender, EventArgs e)
    {
        AddDependent();
         Session["DepFN"] = FirstName.Text;
        Session["DepLN"] = LastName.Text;
        Session["EmpID"] = Provider.SelectedValue;
        Response.Redirect("/BenefitCosts");
    }

    /// <summary>
    /// Adds dependent information and directs the user away to
    /// a blank form to enter another employee
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BackToEmployee(object sender, EventArgs e)
    {
        AddDependent();
        //update session vars
        Session["DepFN"] = FirstName.Text;
        Session["DepLN"] = LastName.Text;
        Session["EmpID"] = Provider.SelectedValue;
        Response.Redirect("/Employee");
    }
}