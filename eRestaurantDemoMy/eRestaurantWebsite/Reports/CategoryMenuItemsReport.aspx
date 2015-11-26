<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CategoryMenuItemsReport.aspx.cs" Inherits="Reports_CategoryMenuItemReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Category Menu items Report Sample</h1>


    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1000px">
        <LocalReport ReportPath="Reports\CategoryMenuItems.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="CategoryMenuItemsDS" />
            </DataSources>
        </LocalReport>
</rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetReportCategoryMenuItems" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>

    </asp:Content>
