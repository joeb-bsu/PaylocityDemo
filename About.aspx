<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.
    </h2>
    <h3>A fun project to use for interviewing with paylocity.</h3>
    <p>This project is the second web application I've built.</p>
    <p>It is a first for working with Asp.Net technologies.</p>
    <p>This has also given me more experience working with Visual Studio.</p>
    <p>  </p>
    <p>There are feautres I would like to switch over to js or ajax to make</p>
    <p>them more dynamic on the webpage, but I'm not sure how to tie in my own js file.</p>
    <p>There is also a bit of ambiguity on the web about data sanitization for asp.</p>
    <p>Some sites recommend using regex features, other recommend an HtmlEncode class,</p>
    <p>while others recommend HtmlAttributeEncode.  Most references are old.  One reference</p>
    <p>said that it was automatically taken care of...  I don't want to trust that.</p>
    <p>This is one reference I want to look more into:</p>
    <p>https://blogs.msdn.microsoft.com/sfaust/2008/09/02/which-asp-net-controls-automatically-encodes/</p>
</asp:Content>
