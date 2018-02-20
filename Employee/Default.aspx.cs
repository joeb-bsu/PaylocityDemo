using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class Employee_Default : System.Web.UI.Page
{
    private const double InitialBenefitCost = 2000.00; //cost of employee benefits per year
    private const int TotalPayPeriods = 26;
    private Boolean isDiscounted = false; //flag to determine if discount applied to benefits
    private const double Discount = 0.10; //discount rate

    
    /// <summary>
    /// Loads the page to add employees.  Creates messages based on session variables
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EmpAdd"] != null && Session["EmpFN"] != null && Session["EmpLN"] != null)
        {
            SuccessLabel.Text = "SUCCESSFULLY added " + Session["EmpFN"].ToString() + " " + Session["EmpLN"].ToString();
            SuccessLab.Text = "SUCCESSFULLY added " + Session["EmpFN"].ToString() + " " + Session["EmpLN"].ToString();
        }
        if (Session["EmpNotAdd"] != null)
        {
            SuccessLabel.Text = "FAILED to add " + Session["EmpFN"].ToString() + " " + Session["EmpLN"].ToString() + " FAILED";
            SuccessLab.Text = "FAILED to add " + Session["EmpFN"].ToString() + " " + Session["EmpLN"].ToString() + " FAILED";
        }
        Session.Clear();
    }


    /// <summary>
    /// Creates a new employee using entries from the webpage
    /// and redirects the user back to a blank form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddAnother_Click(object sender, EventArgs e)
    {
        try
        {
            AddNewEmployee();
        }
        catch (Exception ex)
        {
            Response.Write("Error: Employee NOT Saved - " + ex.ToString());
        }
        Response.Redirect("/Employee");
    }

    /// <summary>
    /// Adds a new employee to the database given information from the webpage
    /// </summary>
    private void AddNewEmployee()
    {
        Guid newEmpID = Guid.NewGuid();
        Guid newBenID = Guid.NewGuid();
        isDiscounted = CheckForDiscount();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DependentConnectionString"].ConnectionString);
        connection.Open();
        Session["EmpFN"] = FirstName.Text;
        Session["EmpLN"] = LastName.Text;
        Session["EmpID"] = newEmpID.ToString();
        int queryReturn1 = DataBaseInsertEmployee(newEmpID, connection);
        int queryReturn2 = DataBaseUpdateBenefits(newEmpID, newBenID, connection);
        if (queryReturn2 != 0 && queryReturn1 != 0)
        {
            Session["EmpAdd"] = "true";
        }
        else
        {
            Session["EmpNotAdd"] = "true";
        }
        connection.Close();
    }

    /// <summary>
    /// Adds the initial cost of benefits to the employee's benefit account balance
    /// </summary>
    /// <param name="newEmpID"></param>
    /// <param name="newBenID"></param>
    /// <param name="connection"></param>
    /// <returns>The number of rows effected in the database table</returns>
    private int DataBaseUpdateBenefits(Guid newEmpID, Guid newBenID, SqlConnection connection)
    {
        string updateBenefits = "INSERT INTO EmpBenefit (EBenId, EmpId, EBenBalance, EBenDisc)  VALUES (@id, @empId, @bal, @disc)";
        SqlCommand sqlCom2 = new SqlCommand(updateBenefits, connection);
        sqlCom2.Parameters.AddWithValue("@id", newBenID.ToString());
        sqlCom2.Parameters.AddWithValue("@empId", newEmpID.ToString());
        sqlCom2.Parameters.AddWithValue("@bal", GetInitBenefitBal(StartDate.Text));
        sqlCom2.Parameters.AddWithValue("@disc", isDiscounted);
        return sqlCom2.ExecuteNonQuery();
    }

    /// <summary>
    /// Inserts employee information into the employee database
    /// </summary>
    /// <param name="newEmpID"></param>
    /// <param name="connection"></param>
    /// <returns>The number of rows effected in the database table</returns>
    private int DataBaseInsertEmployee(Guid newEmpID, SqlConnection connection)
    {
        string insertEmployee = "INSERT INTO Employee (EmpId, EmpFirstName, EmpLastName, EmpSSN, EmpStartDate, EmpActive ) VALUES(@ID, @first, @last, @soc, @date, @active)";
        SqlCommand sqlCom = new SqlCommand(insertEmployee, connection);
        sqlCom.Parameters.AddWithValue("@ID", newEmpID.ToString());
        sqlCom.Parameters.AddWithValue("@first", FirstName.Text);
        sqlCom.Parameters.AddWithValue("@last", LastName.Text);
        sqlCom.Parameters.AddWithValue("@soc", SSN.Text);
        sqlCom.Parameters.AddWithValue("@date", StartDate.Text);
        sqlCom.Parameters.AddWithValue("@active", true);
        return sqlCom.ExecuteNonQuery();
    }

    /// <summary>
    /// checks to see if employee is eligible for a discount
    /// by seeing if an 'A' is the first letter in their name
    /// </summary>
    /// <returns>true if eligible, false otherwise</returns>
    private bool CheckForDiscount()
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
    /// calculates the employee's initial benefits prorated based on their starting
    /// date and decreased by the discount percentage if they are eligible
    /// their starting date
    /// </summary>
    /// <param name="startDate"></param>
    /// <returns>The initial cost for their benefits</returns>
    private object GetInitBenefitBal(String startDate)
    {
        double cost = InitialBenefitCost;
        if(isDiscounted)
        {
            cost = InitialBenefitCost * (1 - Discount);
        }
        double proratedBenCost = (cost / TotalPayPeriods) * (TotalPayPeriods - GetStartingPayPeriod(startDate) + 1);
        return Math.Round(proratedBenCost, 2);
    }

    /// <summary>
    /// calculates the current pay period (assuming 26 per year)
    /// </summary>
    /// <returns>the current pay period</returns>
    private int GetCurrentPayPeriod()
    {
        CultureInfo currCulture = CultureInfo.CurrentCulture;
        int weekNum = currCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        return (weekNum + 1) / 2;
    }

    /// <summary>
    /// calculates the pay period of the employee's start date
    /// </summary>
    /// <param name="date"></param>
    /// <returns>the pay period number</returns>
    private int GetStartingPayPeriod(String date)
    {
        DateTime dateObject = Convert.ToDateTime(date);
        CultureInfo currCulture = CultureInfo.CurrentCulture;
        int weekNum = currCulture.Calendar.GetWeekOfYear(dateObject, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        return (weekNum + 1) / 2;

    }

    /// <summary>
    /// Adds an employee to the database, and redirects
    /// the user to enter dependents for that employee
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddDependent_Click(object sender, EventArgs e)
    {
        try
        {
            AddNewEmployee();
        }
        catch (Exception ex)
        {
            Response.Write("Error: Employee NOT Saved - " + ex.ToString());
        }
        if(Session["EmpNotAdd"] != null)
        {
            Response.Redirect("/Employee");
        } else
        {
            Session["Date"] = StartDate.Text;
            Response.Redirect("/Dependent");
        }
    }

    /// <summary>
    /// Adds an employee to the Database and then redirects
    /// the user to preview the costs and payment schedule
    /// for that employee's benefits
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PreviewCosts_Click(object sender, EventArgs e)
    {
        try
        {
            AddNewEmployee();
        }
        catch (Exception ex)
        {
            Response.Write("Error: Employee NOT Saved - " + ex.ToString());
        }
        Response.Redirect("/BenefitCosts");
    }
}
