using System;
using System.Configuration;

namespace GridViewServerSidePagination
{
    public partial class Employee : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

        protected void PageSize_Changed(object sender, EventArgs e)
        {
            gvProfile.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        }
    }
}