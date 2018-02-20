<%@ Page Title="Dependent Entry" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Dependent_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
        <asp:Label ID="SuccessLabel" runat="server" Display="Dynamic" ></asp:Label>
    </p>

    <div class="form-horizontal">
        <h4>Add a new dependent.</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The first name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="LastName"  CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The Last Name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="SSN" CssClass="col-md-2 control-label">SSN</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="SSN" pattern="(?!000)([0-6]\d{2}|7([0-6]\d|7[012]))([ -])?(?!00)\d\d\3(?!0000)\d{4}" CssClass="form-control" ToolTip="###-##-####" >###-##-####</asp:TextBox>
                <asp:RequiredFieldValidator runat="server"  ControlToValidate="SSN"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The Social Secutity field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="StartDate" CssClass="col-md-2 control-label">Start Date</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="StartDate" TextMode="Date" CssClass="form-control" />

                <asp:RequiredFieldValidator runat="server" ControlToValidate="StartDate"
                    CssClass="text-danger" ErrorMessage="The StartDate field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Provider" CssClass="col-md-2 control-label">Provider</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Provider" runat="server" DataSourceID="EmployeeDB" DataTextField="EmpId" DataValueField="EmpId" Height="19px">
                </asp:DropDownList>
                <%--<asp:SqlDataSource ID="EmployeeDB" runat="server" ConnectionString="<%$ ConnectionStrings:DependentConnectionString %>" SelectCommand="SELECT [EmpSSN], [EmpFirstName], [EmpActive], [EmpLastName] + ',' + ' ' + [EmpFirstName] + ' ' + ' ' + [EmpSSN] AS EmpAll FROM [Employee] WHERE EmpActive = 1 ORDER BY EmpLastName"></asp:SqlDataSource>--%>
                <asp:SqlDataSource ID="EmployeeDB" runat="server" ConnectionString="<%$ ConnectionStrings:DependentConnectionString %>" SelectCommand="SELECT [EmpId] FROM [Employee] WHERE ([EmpActive] = @EmpActive)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="1" Name="EmpActive" Type="Byte" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>



        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server"  Text="Save &amp; Add Dependent from the SAME employee" CssClass="btn btn-default" OnClick="AddAnotherSame_Click" />
                <p class="text-danger">
                <asp:Label ID="SuccessLab" runat="server" Display="Dynamic" ></asp:Label>
                </p>
            </div>
        </div>
      
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server"  Text="Save &amp; Add Dependent for DIFFERENT employee" CssClass="btn btn-default" OnClick="AddDifferent_Click" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server"  Text="Save & Preview Costs" CssClass="btn btn-default" OnClick="PreviewCosts_Click" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Save and Enter Another EMPLOYEE" OnClick="BackToEmployee"/>
            </div>
        </div>
    </div>
</asp:Content>

