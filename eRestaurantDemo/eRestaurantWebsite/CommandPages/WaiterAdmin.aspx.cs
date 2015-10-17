using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


#region Additional Namespaces
using eRestaurantSystem.BLL;//Control
using eRestaurantSystem.DAL.Entities;//Entity
using EatIn.UI; // delegate processrequest


#endregion



public partial class CommandPages_WaiterAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void CheckException(object sender, ObjectDataSourceStatusEventArgs e)
    {

        MessageUserControl.HandleDataBoundException(e);
    }
    protected void FetchWaiter_Click(object sender, EventArgs e)
    {
        //to properly interfact with our MessageUserControl
        //We will delegte the execution of this Click Event
        //under the MessageUserControl.
        if(WaiterList.SelectedIndex == 0)
        {
          //issue our own error message
            MessageUserControl.ShowInfo("please select a waiter to process.");
        }
        else
        {
          //execute the necessary standard lookup code under the 
          //control of the MessageUserControl
        MessageUserControl.TryRun((ProcessRequest)GetWaiterInfo);
        }
    }

    public void GetWaiterInfo()
    {
    // a standard look up process
        AdminController sysmgr = new AdminController();
        var waiter = sysmgr.GetWaiterByID(int.Parse(WaiterList.SelectedValue));
        WaiterID.Text = waiter.WaiterID.ToString();
        FirstName.Text = waiter.FirstName;
        LastName.Text = waiter.LastName;
        Address.Text = waiter.Address;
        Phone.Text = waiter.Phone;
        HireDate.Text = waiter.HireDate.ToShortDateString();

        //null field check
        if(waiter.ReleaseDate.HasValue)
        {
         ReleaseDate.Text = waiter.ReleaseDate.ToString();
        
        }
        else
        {
        ReleaseDate.Text = "";
        
        }

    }
}