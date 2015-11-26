<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuCategoryItemReport.aspx.cs" Inherits="Reports_MenuCategoryItemReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1>MenuCategory Items Report</h1>
    <asp:ObjectDataSource ID="ODSReport" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetReportCategoryMenuItems" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
    <LocalReport ReportPath="Reports\CategoryMenuItems.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="ODSReport" Name="CategoryMenuItemDS" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
</asp:Content>

