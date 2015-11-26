using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespace
using eRestaurantSystem.BLL;
#endregion


public partial class User_Control_DataTimeMocker : System.Web.UI.UserControl
{
    public DateTime MockDate
    {
        get
        { 
                //create a datetime variable and assign it a default value
            DateTime date = DateTime.MinValue;

            //override the default with the contents of the
            //textbox SearchDate
            DateTime.TryParse(SearchDate.Text,out date);


            //pass back a date either the default or the textbox
            return date;
        }
        set 
        {
            SearchDate.Text = value.ToString("yyyy-mm-dd");
        }

    }

    public TimeSpan MockTime
    {
        get
        {
            //create a TimeSpan variable and assign it a default value
            TimeSpan time = TimeSpan.MinValue;

            //override the default with the contents of the
            //textbox SearchTime
            TimeSpan.TryParse(SearchTime.Text, out time);


            //pass back a date either the default or the textbox
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
        SearchDate.Text = info.ToString("yyyy-MM-dd");//"yyyy.MM.dd"
        SearchTime.Text = info.ToString("HH:mm");//"hh:mm:ss"
    }
}