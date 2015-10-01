<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SpecialEventsAdmin.aspx.cs" Inherits="NewFolder1_SpecialEventsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <table class="nav-justified">
        <tr>
            <td>
                <table align="center" style="width: 70%">
                    <tr>
                        <td align="right" style="width:50%">select an Event;</td>
                        <td style="height: 24px">
                            <asp:DropDownList ID="SpecialEventList" runat="server" AppendDataBoundItems ="True" DataSourceID="ODSSpecialEvents" DataTextField="Description" DataValueField="EventCode">
                           <asp:ListItem Value ="z">Select event</asp:ListItem>
                                 </asp:DropDownList>
&nbsp;<asp:LinkButton ID="FetchRegistrations" runat="server">Fatch Registration</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 22px"></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                      
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="ReservationList" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ODSReservations">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:BoundField DataField="ReservationDate" DataFormatString="{0:MMM dd,yyyy}" HeaderText="Date" SortExpression="ReservationDate">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CustomerName" HeaderText="Name" SortExpression="CustomerName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumberInParty" HeaderText="Size" SortExpression="NumberInParty">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ContactPhone" HeaderText="Phone" SortExpression="ContactPhone">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ReservationStatus" HeaderText="Status" SortExpression="ReservationStatus">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    no data to display at this time
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#FF9999" />
                                <PagerSettings FirstPageText="start" LastPageText="end" Mode="NumericFirstLast" PageButtonCount="4" Position="TopAndBottom" />
                            </asp:GridView>
                           </td>
                    
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan=" "2" align = "center">
                <asp:DetailsView ID="Reservation" runat="server" Height="50px" Width="125px" AllowPaging="True" AutoGenerateRows="False" DataSourceID="ODSReservations">
                    <EmptyDataTemplate>
                        no data display at this time
                    </EmptyDataTemplate>
                    <Fields>
                        <asp:BoundField DataField="CustomerName" HeaderText="Name" />
                        <asp:BoundField DataField="ReservationDate" DataFormatString="{0:MMM dd,yyyy h:mm tt}" HeaderText="Date" />
                        <asp:BoundField DataField="NumberInParty" HeaderText="Size" />
                        <asp:BoundField DataField="ContactPhone" HeaderText="Phone" />
                        <asp:BoundField DataField="ReservationStatus" HeaderText="Status" />
                    </Fields>
                </asp:DetailsView>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ODSSpecialEvents" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SpecialEvents_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSReservations" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetReservationByEventCode" TypeName="eRestaurantSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="SpecialEventList" Name="eventcode" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

