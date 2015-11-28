using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRestaurantSystem.BLL;
#endregion

public partial class UserControls_DateTimeMocker : System.Web.UI.UserControl
{
    //create properties that will allow external users
    //of this control to have access to the data
    //(date and time) on this control.
     public DateTime MockDate
    {
         get
        {
            //setup a variable to hold the date
             // this variable will be initialized to a default
            DateTime date = DateTime.MinValue;

             //possible override the default date with the
             //contents of the web control SearchDate
            DateTime.TryParse(SearchDate.Text, out date);

             //return the date value
            return date;
        }
         set
        {
            SearchDate.Text = value.ToString("yyyy-MM-dd");
        }
    }

     public TimeSpan MockTime
     {
         get
         {
             //setup a variable to hold the time
             // this variable will be initialized to a default
             TimeSpan time = TimeSpan.MinValue;

             //possible override the default time with the
             //contents of the web control SearchTime
             TimeSpan.TryParse(SearchTime.Text, out time);

             //return the time value
             return time;
         }
         set
         {
             SearchTime.Text = DateTime.Today.Add(value).ToString("HH:mm:ss");
         }
     }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void MockLastBillingDateTime_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        DateTime info = sysmgr.GetLastBillDateTime();
        SearchDate.Text = info.ToString("yyyy-MM-dd");
        SearchTime.Text = info.ToString("HH:mm");

    }
}