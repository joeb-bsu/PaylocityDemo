<%@ Page Title="Hire Joe" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Joe Bell.</h3>
    <address>
        227 E Poplin St.<br />
        Kuna, ID. 83634<br />
        <abbr title="Phone">P:</abbr>
        208.631.0817
    </address>

    <address>
        <strong>email 1:</strong> <a href="mailto:emailjoebell@gmail.com">emailjoebell@gmail.com</a><br />
        <strong>email 2:</strong> <a href="mailto:josephbell@u.boisestate.edu">josephbell@u.boisestate.edu</a>
    </address>
</asp:Content>
