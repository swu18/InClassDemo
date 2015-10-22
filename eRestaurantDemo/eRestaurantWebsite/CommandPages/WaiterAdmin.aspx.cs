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
        if (!Page.IsPostBack)
        {
            HireDate.Text = DateTime.Today.ToShortDateString();
            RefreshWaiterList("0");//set drop down list to the prompt(2015.10.21)
        }
    }

    protected void RefreshWaiterList(String selectedvalue) //2015.10.21

    { 
    
    //force the re-execution of the query for the drop down list
        WaiterList.DataBind(); //databind and evalbind()?
        // insert the prompt line into the drop down list data
        WaiterList.Items.Insert(0, "Select a waiter");
        // position the waiterlist to the desired row representing the waiter
        WaiterList.SelectedValue = selectedvalue;
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
    protected void WaiterInsert_Click(object sender, EventArgs e)
    {
        //inline version of using MessageUserControl
        MessageUserControl.TryRun(() =>
            //reminder of the code is what would have gone is 
            // in the external method of (processRequest(MethodName))
            {   
                // create instance first.
                Waiter item = new Waiter();
                item.FirstName = FirstName.Text;
                item.LastName = LastName.Text;
                item.Address = Address.Text;
                item.Phone = Phone.Text;
                item.HireDate = DateTime.Parse(HireDate.Text);
                //what about nullable fields
                if (string.IsNullOrEmpty(ReleaseDate.Text))
                {
                    item.ReleaseDate = null;
                }
                else
                {

                    item.ReleaseDate = DateTime.Parse(ReleaseDate.Text);
                }
                AdminController sysmgr = new AdminController();//connect to controoler
                WaiterID.Text = sysmgr.Waiters_Add(item).ToString();//use method to get waiterID
                MessageUserControl.ShowInfo("Waiter added.");// print message
                //WaiterList.DataBind();//drop down list to be refreshed
                RefreshWaiterList(WaiterID.Text);//201510.21
            }
        );
    }
    protected void WaiterUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(WaiterID.Text))
        { 
         MessageUserControl .ShowInfo("please select a waiter first before update.");
        }

        else
        {
            MessageUserControl.TryRun(() =>
            //reminder of the code is what would have gone is 
            // in the external method of (processRequest(MethodName))
            {
                // create instance first.
                Waiter item = new Waiter();
                item.WaiterID = int.Parse(WaiterID.Text);
                item.FirstName = FirstName.Text;
                item.LastName = LastName.Text;
                item.Address = Address.Text;
                item.Phone = Phone.Text;
                item.HireDate = DateTime.Parse(HireDate.Text);
                //what about nullable fields
                if (string.IsNullOrEmpty(ReleaseDate.Text))
                {
                    item.ReleaseDate = null;
                }
                else
                {

                    item.ReleaseDate = DateTime.Parse(ReleaseDate.Text);
                }
                AdminController sysmgr = new AdminController();//connect to controller
                sysmgr.Waiters_Update(item);//use method get waiter update
                MessageUserControl.ShowInfo("Waiter updated.");// print message
                //WaiterList.DataBind();//drop down list to be refreshed
                RefreshWaiterList(WaiterID.Text);//201510.21

            }
        );
        }
    }
}