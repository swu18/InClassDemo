using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRestaurantSystem.BLL;   //controller
using eRestaurantSystem.DAL.Entities;  //entity
using EatIn.UI;  //delegate ProcessRequest
#endregion

public partial class CommandPages_WaiterAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            RefreshWaiterList("0");
            HireDate.Text = DateTime.Today.ToShortDateString();
        }
    }

    protected void RefreshWaiterList(string selectedvalue)
    {
        //force a requery of the drop down list
        WaiterList.DataBind();
        //insert of the prompt line
        WaiterList.Items.Insert(0, "Select a waiter");
        //position on a waiter in the list
        WaiterList.SelectedValue = selectedvalue;
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void FetchWaiter_Click(object sender, EventArgs e)
    {
        //to properly interface with our MessageUserControl
        //we will delegate the execution of this Click event
        //under the MessageUserControl
        if (WaiterList.SelectedIndex == 0)
        {
            //issue our own error message
            MessageUserControl.ShowInfo("Please select a waiter to process.");
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
        //a standard lookup process
        AdminController sysmgr = new AdminController();
        var waiter = sysmgr.GetWaiterByID(int.Parse(WaiterList.SelectedValue));
        WaiterID.Text = waiter.WaiterID.ToString();
        FirstName.Text = waiter.FirstName;
        LastName.Text = waiter.LastName;
        Address.Text = waiter.Address;
        Phone.Text = waiter.Phone;
        HireDate.Text = waiter.HireDate.ToShortDateString();
        //null field check
        if (waiter.ReleaseDate.HasValue)
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
        //this example is using the TryRun inline
        MessageUserControl.TryRun(() =>
            {
                Waiter item = new Waiter();
                item.FirstName = FirstName.Text;
                item.LastName = LastName.Text;
                item.Address = Address.Text;
                item.Phone = Phone.Text;
                item.HireDate = DateTime.Parse(HireDate.Text);
                item.ReleaseDate = null;
                AdminController sysmgr = new AdminController();
                WaiterID.Text = sysmgr.Waiters_Add(item).ToString();
                MessageUserControl.ShowInfo("Waiter added.");
                RefreshWaiterList(WaiterID.Text);
            }
        );
    }
    protected void WaiterUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(WaiterID.Text))
        {
            MessageUserControl.ShowInfo("Please select a waiter to update.");

        }
        else
        {
            MessageUserControl.TryRun(() =>
            {
                Waiter item = new Waiter();
                item.WaiterID = int.Parse(WaiterID.Text);
                item.FirstName = FirstName.Text;
                item.LastName = LastName.Text;
                item.Address = Address.Text;
                item.Phone = Phone.Text;
                item.HireDate = DateTime.Parse(HireDate.Text);
                if (string.IsNullOrEmpty(ReleaseDate.Text))
                {
                    item.ReleaseDate = null;
                }
                else
                {
                    item.ReleaseDate = DateTime.Parse(ReleaseDate.Text);
                }
                
                AdminController sysmgr = new AdminController();
                sysmgr.Waiters_Update(item);
                MessageUserControl.ShowInfo("Waiter updated.");
                RefreshWaiterList(WaiterID.Text);
            }
            );
        }
    }
}