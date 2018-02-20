using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BenefitCosts_Default : System.Web.UI.Page
{
    private string empNumber = "00";
    private string empName = "";
    private double empPayment = 0.0;
    private string empNumDep = "";
    private string employeeDisc = "";
    private string dependDisc = "";
    private int startWeek = 26;
    private const int periods = 26 + 1; //total pay periods
    private double benBalance = 0.0;


    /// <summary>
    /// loads the page, populating fields based on employee id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EmpID"] != null)
        {
            empNumber = Session["EmpID"].ToString();
            Provider.SelectedValue = Session["EmpID"].ToString();
        }

        if(IsPostBack)
        {
            empNumber = Provider.SelectedValue;
        }

        UpdateLabels();
        LoadGraph();

        Session.Clear();
    }//end of page load

    /// <summary>
    /// updates the labels on the webpage
    /// </summary>
    private void UpdateLabels()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DependentConnectionString"].ConnectionString);
        connection.Open();
        SqlGetPayment(connection);
        SqlGetName(connection);
        SqlGetEmpDisc(connection);
        SqlGetDepDisc(connection);
        SqlGetNumDep(connection);
        BenPayment.Text = "Expected Payment: " + String.Format("{0:c}", empPayment.ToString());
        NameLabel.Text = "Name: " + empName;
        EmpDisc.Text = "Employee Discount: " + employeeDisc;
        DepDisc.Text = "Dependent Discounts: " + dependDisc;
        DepNum.Text = "Dependents: " + empNumDep;
        connection.Close();

    }

    /// <summary>
    /// retrieves the number of dependents from the database
    /// </summary>
    /// <param name="connection"></param>
    private void SqlGetNumDep(SqlConnection connection)
    {
        string numDep = "SELECT COUNT(*) FROM Depend WHERE DepProvider = @EID";
        SqlCommand sqlCom = new SqlCommand(numDep, connection);
        sqlCom.Parameters.AddWithValue("@EID", empNumber);

        empNumDep = sqlCom.ExecuteScalar().ToString();
    }

    /// <summary>
    /// retrieves number of employee's dependents eligible for a discount
    /// </summary>
    /// <param name="connection"></param>
    private void SqlGetDepDisc(SqlConnection connection)
    {
        string depDisc = "SELECT EBenNumDepDisc from EmpBenefit where EmpId = @EID";
        SqlCommand sqlCom = new SqlCommand(depDisc, connection);
        sqlCom.Parameters.AddWithValue("@EID", empNumber);
        dependDisc = sqlCom.ExecuteScalar().ToString();
    }

    /// <summary>
    /// retrieves flag from DB shoing if employee is eligible for a discount
    /// </summary>
    /// <param name="connection"></param>
    private void SqlGetEmpDisc(SqlConnection connection)
    {
        string empDisc = "SELECT EBenDisc from EmpBenefit WHERE EmpId = @EID";
        SqlCommand sqlCom = new SqlCommand(empDisc, connection);
        sqlCom.Parameters.AddWithValue("@EID", empNumber);

        string yesNo =  sqlCom.ExecuteScalar().ToString();
        if (yesNo.Equals("True"))
        {
            employeeDisc = "Yes";
        }
        else
        {
            employeeDisc = "No";
        }
    }

    /// <summary>
    /// retrieves the employee's first and last name from the database
    /// </summary>
    /// <param name="connection"></param>
    private void SqlGetName(SqlConnection connection)
    {
        string getName = "SELECT EmpFirstName +' ' + EmpLastName AS EName from Employee WHERE EmpID = @EID";
        SqlCommand sqlCom = new SqlCommand(getName, connection);
        sqlCom.Parameters.AddWithValue("@EID", empNumber);

        empName = sqlCom.ExecuteScalar().ToString();
    }

    /// <summary>
    /// retrieves the estimated payment for employee benefits to
    /// be taken out of the employee's check each period
    /// </summary>
    /// <param name="connection"></param>
    private void SqlGetPayment(SqlConnection connection)
    {
        string getBalance = "SELECT EBenBalance FROM EmpBenefit WHERE EmpId = @EID";
        string getStartDate = "SELECT EmpStartDate FROM Employee WHERE EmpId = @EID";
        SqlCommand sqlCom = new SqlCommand(getBalance, connection);
        sqlCom.Parameters.AddWithValue("@EID", empNumber);
        benBalance = Convert.ToDouble(sqlCom.ExecuteScalar().ToString());
        sqlCom = new SqlCommand(getStartDate, connection);
        sqlCom.Parameters.AddWithValue("@EID", empNumber);
        startWeek = GetStartingPayPeriod(sqlCom.ExecuteScalar().ToString());
        empPayment = Math.Round(benBalance / (periods - startWeek), 2);
    }

    /// <summary>
    /// retrieves the employee's start date from the database
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    private int GetStartingPayPeriod(String date)
    {
        DateTime dateObject = Convert.ToDateTime(date);
        CultureInfo currCulture = CultureInfo.CurrentCulture;
        int weekNum = currCulture.Calendar.GetWeekOfYear(dateObject, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        return (weekNum + 1) / 2;
    }

    /// <summary>
    /// Loads the payment schedule graph on the webpage with
    /// information relevant to the selected employee.
    /// </summary>
    private void LoadGraph()
    {
        
        double balance = benBalance;
        int idx = 0;
        double[] payment = new double[27];


        RN.Text = startWeek.ToString();
        A1.Text = String.Format("{0:c}", balance);
        payment[idx] = Math.Round(balance / (periods - startWeek), 2);
        B1.Text = String.Format("{0:c}", payment[idx]);

        balance = balance - payment[0];
        if (balance > 0)
        {
            TableRow2.Visible = true;
            idx++; startWeek++;
            RN2.Text = startWeek.ToString();
            A2.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B2.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow2.Visible = false;
        }

        balance = balance - payment[1];
        if (balance > 0)
        {
            TableRow3.Visible = true;
            idx++; startWeek++;
            RN3.Text = startWeek.ToString();
            A3.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B3.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow3.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow4.Visible = true;
            idx++; startWeek++;
            RN4.Text = startWeek.ToString();
            A4.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B4.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow4.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow5.Visible = true;
            idx++; startWeek++;
            RN5.Text = startWeek.ToString();
            A5.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B5.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow5.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow6.Visible = true;
            idx++; startWeek++;
            RN6.Text = startWeek.ToString();
            A6.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B6.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow6.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow7.Visible = true;
            idx++; startWeek++;
            RN7.Text = startWeek.ToString();
            A7.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B7.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow7.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow8.Visible = true;
            idx++; startWeek++;
            RN8.Text = startWeek.ToString();
            A8.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B8.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow8.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow9.Visible = true;
            idx++; startWeek++;
            RN9.Text = startWeek.ToString();
            A9.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B9.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow9.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow10.Visible = true;
            idx++; startWeek++;
            RN10.Text = startWeek.ToString();
            A10.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B10.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow10.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow11.Visible = true;
            idx++; startWeek++;
            RN11.Text = startWeek.ToString();
            A11.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B11.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow11.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow12.Visible = true;
            idx++; startWeek++;
            RN12.Text = startWeek.ToString();
            A12.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B12.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow12.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow13.Visible = true;
            idx++; startWeek++;
            RN13.Text = startWeek.ToString();
            A13.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B13.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow13.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow14.Visible = true;
            idx++; startWeek++;
            RN14.Text = startWeek.ToString();
            A14.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B14.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow14.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow15.Visible = true;
            idx++; startWeek++;
            RN15.Text = startWeek.ToString();
            A15.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B15.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow15.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow16.Visible = true;
            idx++; startWeek++;
            RN16.Text = startWeek.ToString();
            A16.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B16.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow16.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow17.Visible = true;
            idx++; startWeek++;
            RN17.Text = startWeek.ToString();
            A17.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B17.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow17.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow18.Visible = true;
            idx++; startWeek++;
            RN18.Text = startWeek.ToString();
            A18.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B18.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow18.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow19.Visible = true;
            idx++; startWeek++;
            RN19.Text = startWeek.ToString();
            A19.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B19.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow19.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow20.Visible = true;
            idx++; startWeek++;
            RN20.Text = startWeek.ToString();
            A20.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B20.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow20.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow21.Visible = true;
            idx++; startWeek++;
            RN21.Text = startWeek.ToString();
            A21.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B21.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow21.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow22.Visible = true;
            idx++; startWeek++;
            RN22.Text = startWeek.ToString();
            A22.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B22.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow22.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow23.Visible = true;
            idx++; startWeek++;
            RN23.Text = startWeek.ToString();
            A23.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B23.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow23.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow24.Visible = true;
            idx++; startWeek++;
            RN24.Text = startWeek.ToString();
            A24.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B24.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow24.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow25.Visible = true;
            idx++; startWeek++;
            RN25.Text = startWeek.ToString();
            A25.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B25.Text = String.Format("{0:c}", payment[idx]);
        }
        else
        {
            TableRow25.Visible = false;
        }

        balance = balance - payment[idx];
        if (balance > 0)
        {
            TableRow26.Visible = true;
            idx++; startWeek++;
            RN26.Text = startWeek.ToString();
            A26.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B26.Text = String.Format("{0:c}", payment[idx]);
        } else
        {
            TableRow26.Visible = false;
        }
        balance = balance - payment[idx];

        if (balance > 0)
        {
            TableRow27.Visible = true;
            idx++; startWeek++;
            RN27.Text = startWeek.ToString();
            A27.Text = String.Format("{0:c}", balance);
            payment[idx] = Math.Round(balance / (periods - startWeek), 2);
            B27.Text = String.Format("{0:c}", payment[idx]);
        } else
        {
            TableRow27.Visible = false;
        }

RNTotal.Text = "TOTAL";
        double total = 0;
        foreach (double p in payment)
        {
            total += p;
        }
        Total.Text = String.Format("{0:c}", total);
    }




}