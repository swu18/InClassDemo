<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WaiterAdmin.aspx.cs" Inherits="CommandPages_WaiterAdmin" %>

<%@ Register Src="~/User Control/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <br /><br /><br />
    
    <h1>Waiter Admin CURD</h1>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:Label ID="Label1" runat="server" Text="WaiterName"></asp:Label>
    <asp:DropDownList ID="WaiterList" runat="server" DataSourceID="ODSWaiterList" DataTextField="FullName" DataValueField="WaiterID">
        <asp:ListItem Value ="0"> Selected a Waiter</asp:ListItem>

    </asp:DropDownList>
    
     <asp:LinkButton ID="FetchWaiter" runat="server" OnClick="FetchWaiter_Click">LinkButton</asp:LinkButton>
    <asp:ObjectDataSource ID="ODSWaiterList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Waiter_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>

    <table align="center" style="width: 73%">
        <tr>
            <td style="height: 22px; width: 107px">ID</td>
            <td style="height: 22px">
                <asp:Label ID="WaiterID" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 107px">FirstName</td>
            <td>
                <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 107px">LastName</td>
            <td>
                <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 107px">Address</td>
            <td>
                <asp:TextBox ID="Address" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 107px">Phone</td>
            <td>
                <asp:TextBox ID="Phone" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 107px">Hire Date</td>
            <td>
                <asp:TextBox ID="HireDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 107px">Release Date</td>
            <td>
                <asp:TextBox ID="ReleaseDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 107px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 107px">
                <asp:LinkButton ID="WaiterInsert" runat="server" OnClick="WaiterInsert_Click" >Insert</asp:LinkButton>
            </td>
            <td>
                <asp:LinkButton ID="WaiterUpdate" runat="server" OnClick="WaiterUpdate_Click">Update</asp:LinkButton>
            </td>
        </tr>
    </table>

</asp:Content>

