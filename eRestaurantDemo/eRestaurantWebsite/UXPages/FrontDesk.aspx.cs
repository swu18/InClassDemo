using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRestaurantSystem.BLL;
using eRestaurantSystem.DAL.Entities;
using eRestaurantSystem.DAL.DTOs;
using eRestaurantSystem.DAL.POCOs;
#endregion

public partial class UXPages_FrontDesk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void SeatingGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        MessageUserControl.TryRun(() =>
            {
                GridViewRow agvrow = SeatingGridView.Rows[e.NewSelectedIndex];
                //accessing a web control on the girdview row usings .FindControl("xxx") as datatype
                string tablenumber = (agvrow.FindControl("TableNumber") as Label).Text;
                string numberinparty = (agvrow.FindControl("NumberInParty") as TextBox).Text;
                string waiterid = (agvrow.FindControl("WaiterList") as DropDownList).SelectedValue;
                var when = Mocker.MockerDate.Add(Mocker.MockerTime);

                //standard call to insert a record into the database
                AdminController sysmgr = new AdminController();
                sysmgr.SeatCustomer(when, byte.Parse(tablenumber), int.Parse(numberinparty), int.Parse(waiterid));

                SeatingGridView.DataBind();

            }, "Customer Seated", "New walk-in customer has been saved");

    }
    protected void ReservationSummaryListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        //this is the method which will gather the seating information for reservations 
        //and pass to the BLL for processing

        //no processing will be done unless the e.CommandName is equal to "Seat"
        if (e.CommandName.Equals("Seat"))
        {
            //execution of the code will be under the control of the MessageControl
            MessageUserControl.TryRun(() =>
                {
                    //gather the necessary data from the web controls
                    int reservationid = int.Parse(e.CommandArgument.ToString());
                    int waiterid = int.Parse(WaiterDropDownList.SelectedValue);
                    DateTime when = Mocker.MockerDate.Add(Mocker.MockerTime);

                    //we need to collect possible multiple values from the ListBox control which contains
                    //the selected tables to be assigned to this group of customers
                    List<byte> selectedTables = new List<byte>();

                    //walk through the ListBox row by row
                    foreach(ListItem item_tableid in ReservationTableListBox.Items)
                    {
                        if (item_tableid.Selected)
                        {
                            selectedTables.Add(byte.Parse(item_tableid.Text.Replace("Table  ", "")));
                        }
                    }

                    //with all data gathered, connect to your library controller, and send data for processing
                    AdminController sysmgr = new AdminController();
                    sysmgr.SeatCustomer(when, reservationid, selectedTables, waiterid);
                    
                    //Refresh the page
                    SeatingGridView.DataBind();
                    ReservationsRepeater.DataBind();
                    ReservationTableListBox.Items.Clear();
                    ReservationTableListBox.DataBind();


                }, "Customer Seated", "Reservation customer has arrived and has been seated");
        }
    }

    protected bool ShowReservationSeating()
    {
        bool anyReservationsToday = false;
        //calll the BLL to indicate if there are any reservations today
        MessageUserControl.TryRun(() =>
            {
                DateTime when = Mocker.MockerDate.Add(Mocker.MockerTime);
                AdminController sysmgr = new AdminController();
                anyReservationsToday = sysmgr.ReservationsForToday(when);
            });
        return anyReservationsToday;
    }
}