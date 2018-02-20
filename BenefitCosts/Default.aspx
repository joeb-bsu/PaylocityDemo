<%@ Page Title="Benefit Cost" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="BenefitCosts_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
        <asp:Label ID="SuccessLabel" runat="server" Display="Dynamic" ></asp:Label>
    </p>
    <div class="form-horizontal">
        <h4>Benefit Costs.</h4>
        <hr />
        <%----------------------------------%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Provider" CssClass="col-md-2 control-label">Employee Number</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Provider" runat="server" DataSourceID="EmployeeDB" DataTextField="EmpId" DataValueField="EmpId" Height="19px" AutoPostBack="True">
                </asp:DropDownList>
                <asp:SqlDataSource ID="EmployeeDB" runat="server" ConnectionString="<%$ ConnectionStrings:DependentConnectionString %>" SelectCommand="SELECT [EmpId] FROM [Employee] WHERE ([EmpActive] = @EmpActive)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="1" Name="EmpActive" Type="byte" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" ID="NameLabel" AssociatedControlID="Provider" CssClass="col-md-2 control-label"></asp:Label>
            <div class="col-md-12">
                <asp:Label runat="server" ID="BenPayment" AssociatedControlID="Provider" CssClass="col-md-2 control-label"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" ID="DepNum" AssociatedControlID="Provider" CssClass="col-md-2 control-label">Employee Discount: Yes</asp:Label>
            <div class="col-md-12">
                <asp:Label runat="server" ID="DepDisc" AssociatedControlID="Provider" CssClass="col-md-2 control-label">Dependent Discounts: --</asp:Label>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" ID="EmpDisc" AssociatedControlID="Provider" CssClass="col-md-2 control-label">Employee Discount: ---</asp:Label>
 <%--           <div class="col-md-12">
                <asp:Label runat="server" ID="Label2" AssociatedControlID="Provider" CssClass="col-md-2 control-label">Dependent Discounts: --</asp:Label>
            </div>--%>
        </div>
        <div class="form-group">
        <%---------------------------------------%>
        <asp:Table ID="Table1" runat="server" GridLines="Horizontal" BorderColor="#E7E7FF" CellPadding="5" BorderStyle="Ridge" BorderWidth="2" CellSpacing="1" Caption="Benefit Payment Schedule" HorizontalAlign="Center">
            <asp:TableHeaderRow HorizontalAlign="Center" BackColor="#E7E7FF">
                <asp:TableHeaderCell>Pay Period</asp:TableHeaderCell>
                <asp:TableHeaderCell>Beginning Balance</asp:TableHeaderCell>
                <asp:TableHeaderCell>Payment</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN"></asp:TableCell>
                <asp:TableCell runat="server" ID="A1"></asp:TableCell>
                <asp:TableCell runat="server" ID="B1"></asp:TableCell>
            </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN2"></asp:TableCell>
                <asp:TableCell runat="server" ID="A2"></asp:TableCell>
                <asp:TableCell runat="server" ID="B2"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN3"></asp:TableCell>
                <asp:TableCell runat="server" ID="A3"></asp:TableCell>
                <asp:TableCell runat="server" ID="B3"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow4" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN4"></asp:TableCell>
                <asp:TableCell runat="server" ID="A4"></asp:TableCell>
                <asp:TableCell runat="server" ID="B4"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow5" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN5"></asp:TableCell>
                <asp:TableCell runat="server" ID="A5"></asp:TableCell>
                <asp:TableCell runat="server" ID="B5"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow6" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN6"></asp:TableCell>
                <asp:TableCell runat="server" ID="A6"></asp:TableCell>
                <asp:TableCell runat="server" ID="B6"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow7" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN7"></asp:TableCell>
                <asp:TableCell runat="server" ID="A7"></asp:TableCell>
                <asp:TableCell runat="server" ID="B7"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow8" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN8"></asp:TableCell>
                <asp:TableCell runat="server" ID="A8"></asp:TableCell>
                <asp:TableCell runat="server" ID="B8"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow9" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN9"></asp:TableCell>
                <asp:TableCell runat="server" ID="A9"></asp:TableCell>
                <asp:TableCell runat="server" ID="B9"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow10" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN10"></asp:TableCell>
                <asp:TableCell runat="server" ID="A10"></asp:TableCell>
                <asp:TableCell runat="server" ID="B10"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow11" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN11"></asp:TableCell>
                <asp:TableCell runat="server" ID="A11"></asp:TableCell>
                <asp:TableCell runat="server" ID="B11"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow12" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN12"></asp:TableCell>
                <asp:TableCell runat="server" ID="A12"></asp:TableCell>
                <asp:TableCell runat="server" ID="B12"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow13" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN13"></asp:TableCell>
                <asp:TableCell runat="server" ID="A13"></asp:TableCell>
                <asp:TableCell runat="server" ID="B13"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow14" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN14"></asp:TableCell>
                <asp:TableCell runat="server" ID="A14"></asp:TableCell>
                <asp:TableCell runat="server" ID="B14"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow15" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN15"></asp:TableCell>
                <asp:TableCell runat="server" ID="A15"></asp:TableCell>
                <asp:TableCell runat="server" ID="B15"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow16" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN16"></asp:TableCell>
                <asp:TableCell runat="server" ID="A16"></asp:TableCell>
                <asp:TableCell runat="server" ID="B16"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow17" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN17"></asp:TableCell>
                <asp:TableCell runat="server" ID="A17"></asp:TableCell>
                <asp:TableCell runat="server" ID="B17"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow18" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN18"></asp:TableCell>
                <asp:TableCell runat="server" ID="A18"></asp:TableCell>
                <asp:TableCell runat="server" ID="B18"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow19" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN19"></asp:TableCell>
                <asp:TableCell runat="server" ID="A19"></asp:TableCell>
                <asp:TableCell runat="server" ID="B19"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow20" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN20"></asp:TableCell>
                <asp:TableCell runat="server" ID="A20"></asp:TableCell>
                <asp:TableCell runat="server" ID="B20"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow21" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN21"></asp:TableCell>
                <asp:TableCell runat="server" ID="A21"></asp:TableCell>
                <asp:TableCell runat="server" ID="B21"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow22" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN22"></asp:TableCell>
                <asp:TableCell runat="server" ID="A22"></asp:TableCell>
                <asp:TableCell runat="server" ID="B22"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow23" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN23"></asp:TableCell>
                <asp:TableCell runat="server" ID="A23"></asp:TableCell>
                <asp:TableCell runat="server" ID="B23"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow24" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN24"></asp:TableCell>
                <asp:TableCell runat="server" ID="A24"></asp:TableCell>
                <asp:TableCell runat="server" ID="B24"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow25" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN25"></asp:TableCell>
                <asp:TableCell runat="server" ID="A25"></asp:TableCell>
                <asp:TableCell runat="server" ID="B25"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow26" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN26"></asp:TableCell>
                <asp:TableCell runat="server" ID="A26"></asp:TableCell>
                <asp:TableCell runat="server" ID="B26"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow27" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="RN27"></asp:TableCell>
                <asp:TableCell runat="server" ID="A27"></asp:TableCell>
                <asp:TableCell runat="server" ID="B27"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow28" runat="server" HorizontalAlign="Center" BackColor="#E7E7FF">
                <asp:TableCell runat="server" ID="RNTotal"></asp:TableCell>
                <asp:TableCell runat="server" ID="A28"></asp:TableCell>
                <asp:TableCell runat="server" ID="Total"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </div>


    </div>





</asp:Content>

