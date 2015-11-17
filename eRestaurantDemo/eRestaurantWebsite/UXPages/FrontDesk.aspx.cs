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
    protected void MockLastBillingDateTime_Click(object sender, EventArgs e)
    {
     AdminController sysmgr = new AdminController();
     DateTime info = sysmgr.GetLastBillDateTime();
     SearchDate.Text = info.ToString("yyyy-MM-dd");//"yyyy.MM.dd"
     SearchTime.Text = info.ToString("HH:mm");//"hh:mm:ss"
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
                DateTime when = DateTime.Parse(SearchDate.Text).Add(TimeSpan.Parse(SearchTime.Text));

                //standard call to insert a record into the database
                AdminController sysmgr = new AdminController();
                sysmgr.SeatCustomer(when, byte.Parse(tablenumber), int.Parse(numberinparty),
                                         int.Parse(waiterid));
                //refresh the gridview
                SeatingGridView.DataBind();

            }, "Customer Seated", "New walk-in customer has been saved");
    
    }
}