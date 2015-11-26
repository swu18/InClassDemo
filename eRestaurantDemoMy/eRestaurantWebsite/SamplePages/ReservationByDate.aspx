<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReservationByDate.aspx.cs" Inherits="SamplePages_ReservationByDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1> Special Events&nbsp; Reservations by date </h1>
    <table align="center" style="width: 70%">
        <tr>
            <td align = "right"> Enter Reservation Date (mm/dd/yyyy):  <asp:TextBox ID="ReservationDateArg" runat="server"  
             ToolTip = "Format mm/dd/yyyy" Text ="01/01/1990"></asp:TextBox>
            <asp:LinkButton ID="FatchReservation" runat="server">Fatch Reservation</asp:LinkButton></td>
            
            <td>&nbsp;</td>
        </tr>

        <tr>
             <td colspan =" 2">&nbsp;</td>

        </tr>

        
        <tr>
            <td colspan =" 2"><div class =" row col-md-12">
                <asp:Repeater ID="EventReservationList" runat="server" DataSourceID="ObjectDataSource1">
                    <ItemTemplate>
                        <h3><%# Eval("Description") %></h3>
                        <asp:Repeater ID="ReservationList" runat="server" 
                            DataSource = '<%# Eval("Reservation") %>'>
                           
                            <ItemTemplate>
                                <h4>
                                    <%# Eval("CustomerName") %>
                                    <%# Eval("ContactPhone") %>
                                </h4>
                            </ItemTemplate>
                        </asp:Repeater>

                    </ItemTemplate>
                 </asp:Repeater>

            </div></td>
            
         </tr>
        </table>
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetReservationByDate" TypeName="eRestaurantSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ReservationDateArg" Name="reservationdate" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

