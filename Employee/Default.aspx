<%@ Page Title="Employee Entry" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Employee_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
        <asp:Label ID="SuccessLabel" runat="server" Display="Dynamic" ></asp:Label>
    </p>

    <div class="form-horizontal">
        <h4>Add a new employee.</h4>
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
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server"  Text="Save & Add Another" CssClass="btn btn-default" OnClick="AddAnother_Click" />
                <p class="text-danger">
                <asp:Label ID="SuccessLab" runat="server" Display="Dynamic" ></asp:Label>
                </p>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server"  Text="Save & Add Dependent" CssClass="btn btn-default" OnClick="AddDependent_Click" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server"  Text="Save & Preview Costs" CssClass="btn btn-default" OnClick="PreviewCosts_Click" />
            </div>
        </div>

    </div>
</asp:Content>

