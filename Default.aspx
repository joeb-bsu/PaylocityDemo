<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <h2><%: Title %>.</h2>

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>Select a Task.</h4>
                    <hr />
                     <p><a href="/Employee" class="btn btn-primary btn-lg">Add Employee &raquo;</a></p>
                     <p><a href="/Dependent" class="btn btn-primary btn-lg">Add Dependents &raquo;</a></p>
                     <p><a href="/BenefitCosts" class="btn btn-primary btn-lg">Preview Costs &raquo;</a></p>

                    <p><a href="/Contact" class="btn btn-primary btn-lg btn-hire" id="hire">Hire Joe &raquo;</a></p>

                </div>
            </section>
        </div>
    </div>







</asp:Content>

