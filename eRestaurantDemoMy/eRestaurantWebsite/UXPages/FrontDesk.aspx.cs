using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespace// only for web page
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
        // 20151116
        //extract the table number, Number in party and the waiter ID
        //from the gridview.
        //we will also create the time from the MockDateTime controls at the top of this 
        //form. (Typically you would use DateTime .Today for current datetime)
    
        //Once the data is collected then it will be sent to the BLL for precessing
        //the command will be done under the control of the MessageUserControl
        //so if there is an error, the MUC can handle it.
        //We will use the in-line MUC TryRun techique

        MessageUserControl.TryRun(() =>
            {
                //Obtain the selected gridview row
                GridViewRow agvrow = SeatingGridView.Rows[e.NewSelectedIndex];
                // accessing a web control on the gridview row 
                //uses  .FindControl("XXX") as datatype
                string tablenumber = (agvrow.FindControl("TableNumber") as Label).Text;
                string numberinparty = (agvrow.FindControl("NumberInParty") as TextBox).Text;
                string waiterid = (agvrow.FindControl("WaiterList") as DropDownList).SelectedValue;
                DateTime when = Mocker.MockDate.Add(Mocker.MockTime);

                //standard call to insert a record into the database
                AdminController sysmgr = new AdminController();
                sysmgr.SeatCustomer(when, byte.Parse(tablenumber), int.Parse(numberinparty),
                                         int.Parse(waiterid));
                //refresh the gridview
                SeatingGridView.DataBind();

            }, "Customer Seated", "New walk-in customer has been saved");
    
    }
    protected void ReservationSummaryListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        // this is the method which will gather the seating 
        // information for reservations and pass to the BLL
        // for processing
        
        // no processing will be done unless the e.CommandName is 
        // equal to "Seat"

        if (e.CommandName.Equals.("Seat"))
        {
           // execution of the code will be under the control 
           // of the MessageUserControl
            MessageUserControl.TryRun(() =>
                {
                  //1. gather necessary data from the web controls
                  int reservationid = int.Parse(e.CommandArgument.ToString());
                  int waiterid = int.Parse(WaiterDropDownList.SelectedValue);
                  DateTime when = Mocker.MockDate.Add(Mocker.MockTime);
                  //2. we need to collect possible multiple values
                  // from the ListBox control which contains 
                  // the selected tables to be assigned to this
                  // group of customers
                
                    List<byte> selectedTables = new List<byte>();

                 //3.walk throuth the ListBox row by row
                    foreach (ListItem item_tableid in ReservationTableListBox.Items)
                    {
                       if (item_tableid.Selected)
                       {
                       selectedTables.Add(byte.Parse(item_tableid.Text.Replace("Table ","")));                          )
                                                  
                       }
                                        
                    }

                    //4.with all data gathered, connect to your 
                    //library controller, and send data for 
                    //processing

                    AdminController sysmgr = new AdminController();
                    sysmgr.SeatCustomer(when, reservationid,selectedTables,waiterid);


                    //5.Refresh the page(Screen)
                    SeatingGridView.DataBind();
                    //6.Refresh the reservation Repeater
                    ReservationsRepeater.DataBind();
                    ReservationTableListBox.DataBind();

                },"Customer Seated","Reservation customer has arrived and has been seated");

                       
        
        }

    }

    protected bool ShowReservationSeating()
    {
 
        bool anyReservationToday = false;
 
        // call the BLL to indicate if there are any reservations
        //today

        MessageUserControl(() =>
        {
              DateTime when = Mocker.MockerDate.Add(Mocker.MockTime);
              AdminController sysmgr = new AdminController();
              anyReservationsToday = sysmgr.ReservationForToday(when);

        });
        return anyReservationToday;
               
    }
}