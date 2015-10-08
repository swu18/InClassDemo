<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CategoryMenuitems.aspx.cs" Inherits="SamplePages_CategoryMenuitems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Category Menu Items (Repeater)</h1>
     <div class =" row col-md-12">
         <asp:Repeater ID="MenuCategory" runat="server" DataSourceID="ODSCategoryMenuItems">
             <ItemTemplate>
                 <h3>
                     <%#Eval("Description") %>
                 </h3>
                 <div class =" well">

                     <asp:Repeater ID="MenuItem" runat="server"
                         DataSource ='<%#Eval ("MenuItems") %>'>

                         <ItemTemplate>
                             <h5>
                                 <%# Eval("Description") %>
                                 <%--<%# ((decimal)Eval("Price")).ToString("C") %>--%>
                                <span class =" badge"><%# Eval("Calories") %></span>
                                 <%#Eval("Comment") %>
                                  </h5>

                         </ItemTemplate>
                     </asp:Repeater>


                 </div>
             </ItemTemplate>
         </asp:Repeater>
         <asp:ObjectDataSource ID="ODSCategoryMenuItems" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MenuCategoryItems_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
     </div>
</asp:Content>

